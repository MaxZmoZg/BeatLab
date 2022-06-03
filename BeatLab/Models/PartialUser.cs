using System;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace BeatLab
{
    public partial class User : IDataErrorInfo
    {
        private const string EmailPattern = @"\w.+@\w.+\.\w{2,3}";

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Last_Name_User))
                    if (string.IsNullOrWhiteSpace(Last_Name_User))
                        return "Введите фамилию";
                if (columnName == nameof(First_Name_User))
                    if (string.IsNullOrWhiteSpace(First_Name_User))
                        return "Введите имя";
                if (columnName == nameof(Age_User))
                    if (!Age_User.HasValue || Age_User.Value < 18)
                        return "Введите корректный возраст от 18 лет";
                if (columnName == nameof(Nickname_User))
                    if (string.IsNullOrWhiteSpace(Nickname_User))
                        return "Введите ник";
                if (columnName == nameof(Login))
                    if (string.IsNullOrWhiteSpace(Login))
                        return "Введите логин";
                if (columnName == nameof(Email_User))
                    if (string.IsNullOrWhiteSpace(Email_User) || !Regex.IsMatch(Email_User, EmailPattern))
                        return "Введите корректную почту";
                return null;
            }
        }

        public string Error
        {
            get
            {
                StringBuilder errors = new StringBuilder();
                foreach (var property in GetType().GetProperties(System.Reflection.BindingFlags.Public))
                    if (this[property.Name] != null)
                        errors.AppendLine(this[property.Name]);
                return errors.ToString();
            }
        }
    }
}