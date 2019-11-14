using Aviato.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Aviato.Attributes
{
    public class PosadaPoLetuAttribute : ValidationAttribute
    {
        private readonly string _vremeLeta;
        private readonly string _idLeta;

        public PosadaPoLetuAttribute(string VremeLeta, string IdLeta)
        {
            _vremeLeta = VremeLeta;
            _idLeta = IdLeta;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            var clanPosade = Convert.ToInt32(value);

            var vremeLeta = validationContext.ObjectType.GetProperty(_vremeLeta);
            var vreme = (DateTime)vremeLeta.GetValue(validationContext.ObjectInstance);
            var idLeta = validationContext.ObjectType.GetProperty(_idLeta);
            var id = (int)idLeta.GetValue(validationContext.ObjectInstance);
            List<int?> posada = ProveriZaposlene(vreme, id);

            if (posada.Contains(clanPosade))
            {
                return new ValidationResult(ErrorMessage);
            }

            else
            {
                return ValidationResult.Success;
            }
        }

        public static List<int?> ProveriZaposlene(DateTime vreme, int id)
        {
            List<Let> istovremeniLetovi = Let.IstovremeniLetovi(vreme, id);

            // Lista id-ijeva svih zaposlenih koji su na istovremenim letovima
            List<int?> vecAngazovani = new List<int?>();

            foreach (var podudarni in istovremeniLetovi)
            {
                if (!vecAngazovani.Contains(podudarni.Pilot) && podudarni.Pilot != null)
                {
                    vecAngazovani.Add(Convert.ToInt32(podudarni.Pilot));
                }
                else if (!vecAngazovani.Contains(podudarni.Kopilot) && podudarni.Kopilot != null)
                {
                    vecAngazovani.Add(podudarni.Kopilot);
                }
                else if (!vecAngazovani.Contains(podudarni.Stjuard1) && podudarni.Stjuard1 != null)
                {
                    vecAngazovani.Add(podudarni.Stjuard1);
                }
                else if (!vecAngazovani.Contains(podudarni.Stjuard2) && podudarni.Stjuard2 != null)
                {
                    vecAngazovani.Add(podudarni.Stjuard2);
                }
            }

            return vecAngazovani;
        }
    }
}