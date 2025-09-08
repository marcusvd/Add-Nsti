using System.Globalization;
using System.Text;

namespace Application.Services.Shared.Helpers;
    public static class StringHandlerServices
    {
        public static string RemoveAccentsAndNormalize(this string target)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var arrayOfString = target.Normalize(NormalizationForm.FormD).ToCharArray();

            foreach (char letter in arrayOfString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter)!= UnicodeCategory.NonSpacingMark)

                    stringBuilder.Append(letter);
            }
            return stringBuilder.ToString().ToUpper();

        }
    }