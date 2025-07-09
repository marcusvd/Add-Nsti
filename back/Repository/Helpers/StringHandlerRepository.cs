using System;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace Repository.Helpers
{
    public static class StringHandlerRepository
    {
        public static string RemoveAccentsNormalize(this string target)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var arrayOfString = target.Normalize(NormalizationForm.FormD).ToCharArray();

            foreach (char letter in arrayOfString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)

                    stringBuilder.Append(letter);
            }
            return stringBuilder.ToString().ToUpper();

        }
    }
}