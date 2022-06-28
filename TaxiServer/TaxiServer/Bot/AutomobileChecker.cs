namespace TaxiServer
{
    internal class AutomobileChecker
    {
        public AutomobileChecker()
        {

        }

        public bool IsCorrectAuto(string text, out string answer)
        {
            if (text == null || text == "")
            {
                answer = "Вы ничего не ввели, укажите цвет, марку и гос номер авто";
                return false;
            }
            else if (text.Length < 10)
            {
                answer = "Слишком коротко, возможно вы указали не все данные, попробуйте еще";
                return false;
            }
            else if (text.Length > 50)
            {
                answer = "Опишите короче, используйте не более 50 символов";
                return false;
            }
            else if (text == "/start")
            {
                answer = "Вы уже в процессе регистрации. укажите цвет, марку и гос номер авто";
                return false;
            }

            answer = "";
            return true;
        }
    }
}
