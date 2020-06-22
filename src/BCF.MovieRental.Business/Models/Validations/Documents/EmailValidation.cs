using System.Text.RegularExpressions;

namespace BCF.MovieRental.Business.Models.Validations.Documents
{
    public static class EmailValidation
    {
        public static bool Validate(this string email)
        {
            Regex rg = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return rg.IsMatch(email);
        }
    }
}