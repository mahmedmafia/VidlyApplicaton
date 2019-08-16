using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MIn18YrsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if (
                customer.MemberShipTypeId == MemberShipType.Unknown ||
                customer.MemberShipTypeId == MemberShipType.PayAsYouGo
                )
            {
                return ValidationResult.Success;
            }
            if (customer.Birthdate == null)
                return new ValidationResult("Bitthdate is Required");

            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;
            return age > 18
                ? ValidationResult.Success
                : new ValidationResult("Customer should Be at least 18 years old");
            

        }
    }
}