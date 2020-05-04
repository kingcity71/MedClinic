using System;
using System.ComponentModel.DataAnnotations;

namespace MedClinic.Models.Attr
{
    public class DateMinAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)// Return a boolean value: true == IsValid, false != IsValid
        {
            if (DateTime.TryParse(value.ToString(), out DateTime date))
                return date > new DateTime(1920, 1, 1);
            return false; //Dates Greater than or equal to today are valid (true)
        }
    }
}
