using System.Globalization;
using System.Text;
using Discord.WebSocket;

namespace MlkAdmin.Core.Utilities.General
{
    public static class ExtensionStaticMethods
    {
        public static ulong ConvertId(this string? strId)
        {
            if (string.IsNullOrEmpty(strId) || !ulong.TryParse(strId, out ulong id))
            {
                throw new ArgumentException("Строка пустая");
            }

            return id;
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
