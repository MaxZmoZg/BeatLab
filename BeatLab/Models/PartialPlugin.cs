using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeatLab.Models.Entities
{
    public partial class Plugins : IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Name_Plugin))
                    if (string.IsNullOrWhiteSpace(Name_Plugin))
                        return "Введите название плагина";
                if (columnName == nameof(PriceString))
                    if (string.IsNullOrWhiteSpace(PriceString) || !int.TryParse(PriceString, out int price) || price <= 0)
                        return "Введите корректную цену";
                return null;
            }
        }

        public string PriceString { get; set; }

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

        [NotMapped]
        public bool IsLicenseAgreementAccepted { get; set; }
    }
}