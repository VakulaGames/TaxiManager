using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaxiServer.DB;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaxiServer
{
    public enum DriverRegStatus
    {
        notRegistered, nameInput, phoneInput, autoInput, photoInput, waitApproved, approwed, blocked
    }

    internal class DriverBot
    {
        public event Action<string> OnRegisteredEvent;

        private MainWindow _mainWindow;
        private DriversManager _driversManager;
        private DataBase _dataBase;
        private string _token = System.IO.File.ReadAllText(@"E:\C#\Taxi01BotData\driverToken.txt");
        private TelegramBotClient _botClient;
        private CancellationTokenSource _cancellationTokenSource;
        private ReceiverOptions _receiverOptions;
        private string _responseMessage;
        private ReplyKeyboardMarkup _currentKeyboard;
        private string _ID;
        private SqlCommandHandler _commandHandler;
        private DriverRegStatus _regStatus;
        private static string _photoPath = @"D:\Applications\TaxiManager\SaveData\DriverPhoto";
        private static string _photoPermission = ".jpeg";

        #region KeyBoards

        private ReplyKeyboardMarkup _startKeyboard = new(new[]
        {
            new KeyboardButton[] {"/start"}
        })
        {
            ResizeKeyboard = true
        };

        #endregion

        public DriverBot(MainWindow mainWindow, DataBase dataBase)
        {
            _mainWindow = mainWindow;
            _driversManager = new DriversManager(_mainWindow);
            _dataBase = dataBase;
            _botClient = new TelegramBotClient(_token);
            _cancellationTokenSource = new CancellationTokenSource();

            _receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            StartBotWorking();
        }

        private async void StartBotWorking()
        {
            _botClient.StartReceiving(
            HandleUpdatesAsync,
            HandleErrorAsync,
            _receiverOptions,
            cancellationToken: _cancellationTokenSource.Token);

            var me = await _botClient.GetMeAsync();
        }

        private async Task HandleUpdatesAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message)
            {
                _ID = update.Message.Chat.Id.ToString();
                _commandHandler = new SqlCommandHandler(_dataBase.sqlConnection, "Driver");
                _regStatus = await _commandHandler.GetRegStatus(_ID);
            }

            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await HandleMessage(botClient, update.Message);
                return;
            }

            if (update.Type == UpdateType.Message && update.Message?.Photo != null || update.Message?.Document != null)
            {
                await HandlePhotoAsync(botClient, update.Message);
                return;
            }

            if (update.Type == UpdateType.CallbackQuery)
            {
                await HandleCallbackQuery(botClient, update.CallbackQuery);
                return;
            }
        }

        private async Task HandlePhotoAsync(ITelegramBotClient botClient, Message message)
        {
            if(_regStatus == DriverRegStatus.photoInput)
            {
                string photoId;

                if (message.Photo != null)
                {
                    photoId = message.Photo.Last().FileId.ToString();
                }
                else if(message.Document != null)
                {
                    photoId = message.Document.FileId.ToString();
                }
                else
                {
                    photoId = null;
                }
                if (photoId != null)
                {
                    DownLoad(photoId, _photoPath);
                    _responseMessage = $"Отлично! Вы закончили регистрацию. " +
                        $"Теперь нужно дождаться проверки данных модератором. " +
                        $"Обычно это занимает не более одного часа";
                    await _commandHandler.EditRow(_ID, "RegStatus", 5);
                    OnRegisteredEvent.Invoke(_ID);
                }
                else
                {
                    _responseMessage = "Что-то пошло не так, попробуйте еще";
                }
            }
            else if (_regStatus == DriverRegStatus.approwed)
            {
                _responseMessage = "Если вы хотите сменить фото, Выберите пункт \"Редактировать профиль\" и далее \"Загрузить новое фото\"";
            }
            else
            {
                _responseMessage = "Спасибо, конечно, но я этого не просил";
            }

            await botClient.SendTextMessageAsync(_ID, _responseMessage ?? "", replyMarkup: _currentKeyboard ?? _startKeyboard);
        }

        private async Task HandleMessage(ITelegramBotClient botClient, Message message)
        {
            if (_regStatus == DriverRegStatus.approwed)
            {
                _responseMessage = "Вы успешно зарегистрировались!";
            }
            else
            {
                await Register( message);
            }
            
            await botClient.SendTextMessageAsync(_ID, _responseMessage??"", replyMarkup: _currentKeyboard??_startKeyboard);
        }

        private async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
            {
                if (callbackQuery.Data.StartsWith("buy"))
                {
                    await botClient.SendTextMessageAsync(
                        callbackQuery.Message.Chat.Id,
                        $"Вы хотите купить?"
                    );
                    return;
                }
                if (callbackQuery.Data.StartsWith("sell"))
                {
                    await botClient.SendTextMessageAsync(
                        callbackQuery.Message.Chat.Id,
                        $"Вы хотите продать?"
                    );
                    return;
                }
                await botClient.SendTextMessageAsync(
                    callbackQuery.Message.Chat.Id,
                    $"You choose with data: {callbackQuery.Data}"
                    );
                return;
            }

        private Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Ошибка телеграм АПИ:\n{apiRequestException.ErrorCode}\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            _mainWindow.ShowMessage(ErrorMessage);
            //Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private async Task Register(Message message)
        {
            switch (_regStatus)
            {
                case DriverRegStatus.notRegistered:
                    if (message.Text == "/start")
                    {
                        Driver driver = new Driver(_ID);
                        await _commandHandler.AddRow(driver);
                        _responseMessage = "Вы регистрируетесь водителем в службу такси №1\n" +
                            "Введите свое имя";
                        await _commandHandler.EditRow(_ID, "RegStatus", 1);
                    }
                    else
                    {
                        _responseMessage = "Добро пожаловать в службу такси №1\n" +
                            "Давайте зарегистрируем Вас водителем\n" +
                            "Для этого введите команду /start";
                        _currentKeyboard = _startKeyboard;
                    }
                    break;

                case DriverRegStatus.nameInput:
                    if (NameIsCorrect(message.Text, out _responseMessage))
                    {
                        await _commandHandler.EditRow(_ID, "Name", message.Text);
                        await _commandHandler.EditRow(_ID, "RegStatus", 2);
                    }
                    break;

                case DriverRegStatus.autoInput:
                    if (AutoIsCorrect(message.Text, out _responseMessage))
                    {
                        await _commandHandler.EditRow(_ID, "Automobile", message.Text);
                        await _commandHandler.EditRow(_ID, "RegStatus", 3);
                    }
                    break;

                case DriverRegStatus.phoneInput:
                    if(PhoneIsCorrect(message.Text, out _responseMessage))
                    {
                        await _commandHandler.EditRow(_ID, "PhoneNumber", message.Text);
                        await _commandHandler.EditRow(_ID, "RegStatus", 4);
                    }
                    break;

                case DriverRegStatus.photoInput:
                    {
                        _responseMessage = "Вы ничего не прислали, пришлите свое фото";
                    }

                    break;

                case DriverRegStatus.waitApproved:
                    {
                        _responseMessage = "Данные проверяются модератором. После успешной проверки," +
                            " Вы получите уведомление и сможете начать работу. А пока Вы можете прислать" +
                            " Ваше место расположение";
                    }

                    break;

                case DriverRegStatus.approwed:

                    break;

                default:
                    break;
            }
        }

        private async void DownLoad(string fileId, string path)
        {
            var file = await _botClient.GetFileAsync(fileId);
            FileStream fs = new FileStream(path + "/" + _ID + _photoPermission, FileMode.Create);
            await _botClient.DownloadFileAsync(file.FilePath, fs);
            fs.Close();

            fs.Dispose();
        }

        private static bool NameIsCorrect(string name, out string answer)
        {
            NameChecker checker = new NameChecker();

            if (checker.IsCorrectName(name, out answer))
            {
                answer = "Отлично! теперь опишите Ваш автомобиль,\n" +
                    "например, \"Серебристая KIA RIO А159УР1232\"";
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool AutoIsCorrect(string auto, out string answer)
        {
            AutomobileChecker checker = new AutomobileChecker();

            if (checker.IsCorrectAuto(auto, out answer))
            {
                answer = "Отлично! теперь введите Ваш номер телефона";
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool PhoneIsCorrect(string phone, out string answer)
        {
            PhoneNumberChecker checker = new PhoneNumberChecker();

            if (checker.IsCorrectPhoneNumber(phone, out answer))
            {
                answer = "Отлично! Остался последний шаг. Загрузите свое фото";
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
