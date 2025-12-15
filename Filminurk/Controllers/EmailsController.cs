using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class EmailsController : Controller
    {
        private readonly IEmailServices _emailServices;
        public EmailsController(IEmailServices emailServices)
        {
            _emailServices = emailServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendEmail(EmailViewModel vm)
        {
            var dto = new EmailDTO()
            {
                SendToThisAdress = vm.SendToThisAdress,
                EmailSubject = vm.EmailSubject,
                EmailContent = vm.EmailContent
            };
            _emailServices.SendEmail(dto);
            return RedirectToAction(Index));
        }
    }
}
