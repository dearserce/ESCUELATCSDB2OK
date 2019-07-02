using EscuelaTCSDB.Models;
using EscuelaTCSDB.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.CustomValidations
{
    public class PersonaEmailValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var personaViewModelInstance = (PersonaViewModel)  validationContext.ObjectInstance;
            var personaD = Persona.GetByEmail(personaViewModelInstance.email, personaViewModelInstance.Id);

            return (personaD == null)
                ? ValidationResult.Success
                : new ValidationResult("El Email se encuentra en uso");
        }
    }
}