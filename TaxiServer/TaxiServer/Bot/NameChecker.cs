namespace TaxiServer
{
    internal class NameChecker
    {
        private char[] _letters;

        public NameChecker()
        {
            _letters = new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н',
                'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я', 'А',
                'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н',
                'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я',};
        }

        public bool IsCorrectName(string text, out string answer)
        {
            if (text == null || text == "")
            {
                answer = "Вы ничего не ввели, введите имя";
                return false;
            }
            else if (text.Length < 3)
            {
                answer = "Слишком короткое имя, попробуйте еще";
                return false;
            }
            else if (text.Length > 20)
            {
                answer = "Слишком длинное имя, используйте не более 20 символов";
                return false;
            }
            else if (text == "/start")
            {
                answer = "Вы уже в процессе регистрации. Введите свое имя";
                return false;
            }
            foreach (char c in text)
            {
                bool contain = false;
                foreach(char c2 in _letters)
                {
                    if (c == c2)
                    {
                        contain = true;
                        continue;
                    }
                }

                if (contain == false)
                {
                    answer = "Вы ввели недопустимый символ. Введите имя, " +
                        "используя только буквы русского алфавита";
                    return false;
                }
            }
            answer = "";
            return true;
        }
    }
}
