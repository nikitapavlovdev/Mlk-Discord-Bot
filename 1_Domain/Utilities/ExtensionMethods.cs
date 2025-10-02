using System.Text;

namespace MlkAdmin.Core.Utilities.General
{
    public static class ExtensionMethods
    {
        public static ulong ConvertId(this string? strId)
        {

            if (string.IsNullOrEmpty(strId) || !ulong.TryParse(strId, out ulong id))
            {
                return 0;
            }

            return id;
        }

        public static void CompareChange(this StringBuilder stringBuilder, string fieldName, string? oldValue, string? newValue)
        {
            if(oldValue != newValue)
            {
                stringBuilder.AppendLine($"**{fieldName} изменен:**");
                stringBuilder.AppendLine($"> **Старое:** {oldValue ?? "-"}");
                stringBuilder.AppendLine($"> **Новое: ** {newValue ?? "-"}");
            }
        }
    } 
}
