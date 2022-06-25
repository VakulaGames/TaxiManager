using System;
using System.Collections.Generic;
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
    internal class DriverBot
    {
        public event Action<Driver> OnNewDriverEvent;

        MainWindow _mainWindow;
        DriversManager _driversManager;
        DataBase _dataBase;
        private string _token = System.IO.File.ReadAllText(@"E:\C#\Taxi01BotData\driverToken.txt");
        private TelegramBotClient _botClient;

        public DriverBot(MainWindow mainWindow, DataBase dataBase)
        {
            _mainWindow = mainWindow;
            _driversManager = new DriversManager(_mainWindow);
            _dataBase = dataBase;

            StartBotWorking();
        }

        private async void StartBotWorking()
        {
            _botClient = new TelegramBotClient(_token);

            var cts = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            _botClient.StartReceiving(
            HandleUpdatesAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken: cts.Token);

            var me = await _botClient.GetMeAsync();
        }

        async Task HandleUpdatesAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await HandleMessage(botClient, update.Message);
                return;
            }

            if (update.Type == UpdateType.CallbackQuery)
            {
                await HandleCallbackQuery(botClient, update.CallbackQuery);
                return;
            }
        }

        async Task HandleMessage(ITelegramBotClient botClient, Message message)
        {


            if (message.Text == "/start")
            {
                SqlCommandHandler commandHandler = new SqlCommandHandler(_dataBase.sqlConnection, "Driver");
                Driver driver = new Driver(message.Chat.Id.ToString());
                commandHandler.AddRow(driver);
            }

            //    if (message.Text == "/start")
            //    {
            //        if(driver == null)
            //        {
            //            driver = new Driver(message.Chat.Id.ToString());
            //            OnNewDriverEvent.Invoke(driver);
            //            await botClient.SendTextMessageAsync(message.Chat.Id, $"Привет! Вы регистрируетесь в Такси №1\nНапишите свое имя");
            //            return;
            //        }

            //        else if (driver.RegStatus == DriverRegStatus.completed)
            //        {
            //            ReplyKeyboardMarkup keyboard = new(new[]
            //            {
            //                new KeyboardButton[] {"Редактировать профиль", "Показать статистику"}
            //            })
            //            {
            //                ResizeKeyboard = true
            //            };
            //            await botClient.SendTextMessageAsync(message.Chat.Id, $"Выберите действие");
            //            return;
            //        }

            //        else
            //        {
            //            //await botClient.SendTextMessageAsync(message.Chat.Id, DriverRegistrar.CheckRegStatus(message.Text, driver));
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        if (driver == null)
            //        {
            //            driver = new Driver(message.Chat.Id.ToString());
            //            OnNewDriverEvent.Invoke(driver);

            //            ReplyKeyboardMarkup keyboard = new(new[]
            //            {
            //                new KeyboardButton[] {"/start"}
            //            })
            //            {
            //                ResizeKeyboard = true
            //            };
            //            await botClient.SendTextMessageAsync(message.Chat.Id, $"Введите /start для начала работы");
            //            return;
            //        }

            //        else if (driver.RegStatus == DriverRegStatus.completed)
            //        {
            //            ReplyKeyboardMarkup keyboard = new(new[]
            //            {
            //                new KeyboardButton[] {"Редактировать профиль", "Показать статистику", "Уйти с линии"}
            //            })
            //            {
            //                ResizeKeyboard = true
            //            };
            //            await botClient.SendTextMessageAsync(message.Chat.Id, $"Выберите действие");
            //            return;
            //        }

            //        else
            //        {
            //            //await botClient.SendTextMessageAsync(message.Chat.Id, DriverRegistrar.CheckRegStatus(message.Text, driver));
            //            return;
            //        }
            //    }

        }

        async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
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

            Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
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
    }
}
