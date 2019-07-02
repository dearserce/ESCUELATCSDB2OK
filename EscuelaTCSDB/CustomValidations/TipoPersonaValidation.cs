using EscuelaTCSDB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.CustomValidations
{
    public class TipoPersonaValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var tp = (TipoPersona)validationContext.ObjectInstance;
            var tpd = TipoPersona.GetByDescripcion(tp.descripcion, tp.Id);
            return (tpd == null)
                ? ValidationResult.Success
                : new ValidationResult("Ya existe");
        }
    }
}