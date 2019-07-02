using EscuelaTCSDB.Models;
using EscuelaTCSDB.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.CustomValidations
{
    public class MateriaValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var materiaVMI = (MateriaViewModel)validationContext.ObjectInstance;
            var materia = Materia.GetByNombre(materiaVMI.nombre, materiaVMI.Id);
            return (materia == null)
                ? ValidationResult.Success
                : new ValidationResult("La materia ya existe");
        }
    }
}