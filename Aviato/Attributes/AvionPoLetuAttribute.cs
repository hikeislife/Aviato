using Aviato.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Aviato.Attributes
{
    public class AvionPoLetuAttribute : ValidationAttribute
    {
        private readonly string _vremeLeta;
        private readonly string _idLeta;

        public AvionPoLetuAttribute(string VremeLeta, string IdLeta)
        {
            _vremeLeta = VremeLeta;
            _idLeta = IdLeta;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            var avion = Convert.ToInt32(value);

            var vremeLeta = validationContext.ObjectType.GetProperty(_vremeLeta);
            var vreme = (DateTime)vremeLeta.GetValue(validationContext.ObjectInstance);
            var idLeta = validationContext.ObjectType.GetProperty(_idLeta);
            var id = (int)idLeta.GetValue(validationContext.ObjectInstance);
            List<int> avioni = ProveriAvione(vreme, id);

            if (avioni.Contains(Convert.ToInt32(value)))
            {
                return new ValidationResult(ErrorMessage);
            }
            else
            {
                return ValidationResult.Success;
            }
        }

        public static List<int> ProveriAvione(DateTime vreme, int id)
        {
            List<Let> istovremeniLetovi = Let.IstovremeniLetovi(vreme,id);

            List<int> vecZauzeti = new List<int>();

            foreach (var podudarni in istovremeniLetovi)
            {
                if (vecZauzeti.Count() == 0)
                {
                    vecZauzeti.Add(podudarni.Avion);
                }
                else if (!vecZauzeti.Contains(podudarni.Avion))
                {
                    vecZauzeti.Add(podudarni.Avion);
                }
            }
            return vecZauzeti;
        }

    }
}