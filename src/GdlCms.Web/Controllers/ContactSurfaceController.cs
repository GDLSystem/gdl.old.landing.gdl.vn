using System;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using GdlCms.Web.Helpers;
using GdlCms.Web.Services;
using GdlCms.Web.ViewModels;
using reCAPTCHA.MVC;
using Umbraco.Core.Logging;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace GdlCms.Web.Controllers
{
    public class ContactSurfaceController : SurfaceController
    {
        private readonly ISmtpService _smtpService;
        public ContactSurfaceController(ISmtpService smtpService)
        {
            _smtpService = smtpService;
        }
        
        [HttpGet]
        public ActionResult RenderForm()
        {
            ContactViewModel model = new ContactViewModel() {ContactFormId = CurrentPage.Id};
            return PartialView("~/Views/Partials/Contact/ContactForm.cshtml", model);
        }
        [HttpPost]
        public ActionResult RenderForm(ContactViewModel model)
        {
            return PartialView("~/Views/Partials/Contact/ContactForm.cshtml", model);
        }

        [HttpPost]
        [CaptchaValidator]
        public ActionResult SubmitForm(ContactViewModel model)
        {
            var emailBody = GetEmailBody(model);
            bool success = false;

            if (ModelState.IsValid)
            {
                success = _smtpService.SendEmail(model.Email, model.Phone, model.Name, emailBody);
            }

            var contactPage = UmbracoContext.Content.GetById(false, model.ContactFormId);

            var successMessage = contactPage.Value<IHtmlString>("contactFormSuccessMessage");
            var errorMessage = contactPage.Value<IHtmlString>("contactFormWarningMessage");

            return Json(new {success, message = success ? successMessage : errorMessage});
            // return PartialView("~/Views/Partials/Contact/Result.cshtml", success ? successMessage : errorMessage);
        }
    
        private string GetEmailBody(ContactViewModel model)
        {
            return RazorViewToStringRenderer.RenderViewToString(ControllerContext, "~/Views/Partials/Contact/MailContact.cshtml", model, true);
        }
    }
}