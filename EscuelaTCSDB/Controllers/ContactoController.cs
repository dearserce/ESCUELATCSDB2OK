using EscuelaTCSDB.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace EscuelaTCSDB.Controllers
{
    public class ContactoController : Controller
    {
        // GET: Contacto
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EnviarCorreo(ContactoViewModel cvm) {
            if (!ModelState.IsValid) {
                return View("Index",cvm);
            }
            string email = String.IsNullOrEmpty(cvm.Correo) ? "Clasificado" : cvm.Correo;
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.From = new MailAddress("smtp.francisco@gmail.com");
            message.To.Add(new MailAddress("dearserce@gmail.com")); //replace with valid value
            message.Subject = "Your email subject";
            message.Body = string.Format(body, cvm.Nombre, email, cvm.Comentario);
            message.IsBodyHtml = true;
            using (var smtp = new SmtpClient())
            {
                smtp.Send(message);
                return RedirectToAction("Index");
            }
        }
    }
}