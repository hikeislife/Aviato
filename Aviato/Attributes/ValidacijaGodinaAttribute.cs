using System;
using System.ComponentModel.DataAnnotations;

namespace Aviato.Attributes
{
    public class ValidacijaGodinaAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {   
            int minimum = Convert.ToInt32(DateTime.Now.AddYears(-64).Year);
            int maximum = Convert.ToInt32(DateTime.Now.AddYears(-18).Year);
            int current = Convert.ToInt32(value);
            if (value == null)
            {
                return false;
            }
            if( current < minimum || current > maximum)
            {
                return false;
            }
            return true;
        }
    }
}