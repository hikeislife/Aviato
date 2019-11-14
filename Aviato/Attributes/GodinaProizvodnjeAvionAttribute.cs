using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aviato.Attributes
{
    public class GodinaProizvodnjeAvionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            int godinaProizvodnje = Convert.ToInt32(value);
            int sadasnjeVreme = Convert.ToInt32(DateTime.Now.Year); 

            if (godinaProizvodnje > sadasnjeVreme)
            {
                return new ValidationResult(ErrorMessage);
            }

            else
            {
                return ValidationResult.Success;
            }
        }
    }
}