using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiServer
{
    public enum DriverRegStatus
    {
        notRegistered, nameInput, phoneInput, autoInput, photoInput, completed
    }

    public class DriverRegistrar
    {
        //public static string CheckRegStatus(string message, Driver driver)
        //{
            //switch (driver.RegStatus)
            //{
            //    case DriverRegStatus.nameInput:
            //        if (NameIsCorrect(message, out string answer))
            //        {
            //            driver.Name = message;
            //            driver.RegStatus = DriverRegStatus.autoInput;
            //            return answer;
            //        }
            //        else
            //        {
            //            return answer;
            //        }

            //    case DriverRegStatus.autoInput:
            //        if (AutoIsCorrect(message, out string answer2))
            //        {
            //            driver.Automobile = message;
            //            driver.RegStatus = DriverRegStatus.phoneInput;
            //            return answer2;
            //        }
            //        else
            //        {
            //            return answer2;
            //        }

            //    case DriverRegStatus.phoneInput:
            //        if (PhoneIsCorrect(message, out string answer3))
            //        {
            //            driver.PhoneNumber = message;
            //            driver.RegStatus = DriverRegStatus.photoInput;
            //            return answer3;
            //        }
            //        else
            //        {
            //            return answer3;
            //        }

            //    case DriverRegStatus.photoInput:
            //        driver.RegStatus = DriverRegStatus.completed;
            //        return "Сделаем вид, что вы прислали фото, а потом добавим эту функцию";

            //    default: return "Вы успешно прошли регистрацию";
        //    }
        //}

        private static bool NameIsCorrect (string name, out string answer)
        {
            if (name == null || name == "")
            {
                answer = "Вы ничего не ввели, введите имя";
                return false;
            }
            else if (name.Length < 3)
            {
                answer = "Слишком короткое имя, попробуйте еще";
                return false;
            }
            else
            {
                answer = "Отлично! теперь опишите Ваш автомобиль,\n" +
                    "например, \"Серебристая KIA RIO А159УР1232\"";
                return true;
            }
        }

        private static bool AutoIsCorrect(string auto, out string answer)
        {
            if (auto == null || auto == "")
            {
                answer = "Вы ничего не ввели, введите Ваш автомобиль";
                return false;
            }
            else if (auto.Length < 10)
            {
                answer = "Слишком коротко, Опишите подробней, например, \"Серебристая KIA RIO А159УР1232\"";
                return false;
            }
            else
            {
                answer = "Отлично! теперь введите Ваш номер телефона";
                return true;
            }
        }

        private static bool PhoneIsCorrect(string phone, out string answer)
        {
            if (phone == null || phone == "")
            {
                answer = "Вы ничего не ввели, введите Ваш номер телефона";
                return false;
            }
            else if (phone.Length < 10)
            {
                answer = "Слишком короткий номер, попробуйте еще";
                return false;
            }
            else if (phone.Length > 12)
            {
                answer = "Слишком длинный номер, попробуйте еще";
                return false;
            }
            else
            {
                answer = "Отлично! Остался последний шаг. Загрузите свое фото";
                return true;
            }
        }
    }
}
