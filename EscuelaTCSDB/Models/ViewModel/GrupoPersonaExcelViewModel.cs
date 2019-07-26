using EscuelaTCSDB.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models.ViewModel
{
    public class GrupoPersonaExcelViewModel
    {
        [DisplayName("Archivo")]
        [FileExt(Allow = ".xls,.xlsx", ErrorMessage = "Solo selecciona un Archivo de Excel")]
        [Required(ErrorMessage = "Selecciona un Archivo")]
        public HttpPostedFileBase file { get; set; }
        public string rutaPlantilla { get; set; }
    }
}