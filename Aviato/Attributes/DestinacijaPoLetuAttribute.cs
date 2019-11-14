using Aviato.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Aviato.Attributes
{
    public class DestinacijaPoLetuAttribute : ValidationAttribute
    {
        private readonly string _vremeLeta;
        private readonly string _idLeta;

        public DestinacijaPoLetuAttribute(string VremeLeta, string IdLeta)
        {
            _vremeLeta = VremeLeta;
            _idLeta = IdLeta;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            var destinacija = Convert.ToInt32(value);

            var vremeLeta = validationContext.ObjectType.GetProperty(_vremeLeta);
            var vreme = (DateTime)vremeLeta.GetValue(validationContext.ObjectInstance);
            var idLeta = validationContext.ObjectType.GetProperty(_idLeta);
            var id = (int)idLeta.GetValue(validationContext.ObjectInstance);
            List<int> destinacije = ProveriDestinacije(vreme, id);

            if (destinacije.Contains(destinacija))
            {
                return new ValidationResult(ErrorMessage);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
        
        public static List<int> ProveriDestinacije(DateTime vreme, int id)
        {
            List<Let> istovremeniLetovi = Let.IstovremeniLetovi(vreme, id);

            List<int> vecPutujemo = new List<int>();

            foreach (var podudarni in istovremeniLetovi)
            {
                if (vecPutujemo.Count() == 0)
                {
                    vecPutujemo.Add(podudarni.Destinacija);
                }
                else if (!vecPutujemo.Contains(podudarni.Destinacija))
                {
                    vecPutujemo.Add(podudarni.Destinacija);
                }
            }

            return vecPutujemo;
        }
    }
}