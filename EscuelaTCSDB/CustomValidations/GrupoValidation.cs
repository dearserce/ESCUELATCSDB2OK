using EscuelaTCSDB.Models;
using EscuelaTCSDB.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.CustomValidations
{
    public class GrupoValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var grupoVMI = (GrupoViewModel)validationContext.ObjectInstance;
            var grupo = Grupo.GetByCodigo(grupoVMI.codigo, grupoVMI.Id);

            return (grupo == null)
                ? ValidationResult.Success
                : new ValidationResult("El Codigo se encuentra en uso");
        }
    }
}