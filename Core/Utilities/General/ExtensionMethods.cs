using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Discord_Bot.Core.Utilities.General
{
    static class ExtensionMethods
    {
        public static ulong ConvertId(this string? strId)
        {
            if (string.IsNullOrEmpty(strId) || !ulong.TryParse(strId, out ulong id))
            {
                throw new ArgumentException("Строка пустая");
            }

            return id;
        }

        public static string GetStringFromConfiguration(this string? configString)
        {
            if (string.IsNullOrEmpty(configString))
            {
                throw new ArgumentException("Строка пустая");
            }

            return configString;
        }

        private static bool ContainsEmoji(this string inputText)
        {
            Regex emojiRegex = new Regex(@"\p{Cs}|\p{So}");
            return emojiRegex.IsMatch(inputText);
        }

        public static bool NicknameIsValid(this string? inputText)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                throw new ArgumentException("NickameIsValid: строка пустая");
            }

            bool _inputNicknameIsNotValid = inputText.Any(char.IsWhiteSpace)
                                         || inputText.Any(char.IsUpper)
                                         || inputText.ContainsEmoji();

            if (_inputNicknameIsNotValid) { return false; }
            else { return true; }
        }

        public static bool NameIsValid(this string? inputText, out bool firstCharIsUpper)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                throw new ArgumentException("NameIsValid: строка пустая");
            }

            bool _inputNameIsNotValid = inputText.Any(char.IsWhiteSpace)
                                         || inputText.ContainsEmoji();

            inputText = inputText.Trim();

            if (char.IsUpper(inputText[0]))
            {
                firstCharIsUpper = true;
            }
            else { firstCharIsUpper = false; }

            if (_inputNameIsNotValid)
            {
                return false;
            }
            else { return true; }

        }

        public static string GetParseNickname(this string? inputText)
        {
            if(inputText == null || inputText == "")
            {
                return "anxloshara";
            }

            string[] nameSplit = inputText.Split(" ");

            return nameSplit[0];
        }

        public static bool DateOfBirthdayIsCorrect(this string? inputText, out DateTime date)
        {
            bool dateIsCorrect = DateTime.TryParseExact(inputText, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

            return dateIsCorrect;
        }

        public static string GetRandomCode(int len)
        {
            StringBuilder code = new();
            Random rand = new();

            char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

            for(int i = 0; i < len; i++)
            {
                int index = rand.Next(chars.Length);
                code.Append(chars[index]);
            }

            return code.ToString();
        }
    } 
}
