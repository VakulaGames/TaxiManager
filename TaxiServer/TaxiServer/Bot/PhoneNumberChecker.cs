namespace TaxiServer
{
    internal class PhoneNumberChecker
    {
        private char[] _numbers;

        public PhoneNumberChecker()
        {
            _numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9','+' };
        }

        public bool IsCorrectPhoneNumber(string text, out string answer)
        {
            if (text == null || text == "")
            {
                answer = "Вы ничего не ввели, введите Ваш номер телефона";
                return false;
            }
            else if (text.Length < 10)
            {
                answer = "Слишком короткий номер, попробуйте еще";
                return false;
            }
            else if (text.Length > 12)
            {
                answer = "Слишком длинный номер, попробуйте еще";
                return false;
            }
            else if (text == "/start")
            {
                answer = "Вы уже в процессе регистрации. введите Ваш номер телефона";
                return false;
            }

            int length = text.Length;
            for (int i = length - 1; i >= 0; i--)
            {
                bool contain = false;
                foreach (char c2 in _numbers)
                {
                    if (text[i] == c2)
                    {
                        contain = true;
                        continue;
                    }
                }
                if (contain == false)
                {
                    answer = "Вы ввели недопустимый символ. Введите номер, " +
                        "используя только цифры";
                    return false;
                }
            }

            answer = "";
            return true;
        }
    }
}
