using System.Globalization;
using System.Text;

namespace MlkAdmin.Core.Utilities.General
{
    public static class ExtensionMethods
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
    } 
}
