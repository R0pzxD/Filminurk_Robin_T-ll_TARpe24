using Filminurk.Data;
using Filminurk.Models.UserComments;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class UserCommentsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        public UserCommentsController(FilminurkTARpe24Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var result = _context.UserComments
                .Select(d => new UserCommentsIndexViewModel
                {
                    CommentID = d.CommentID,
                    CommentBody = d.CommentBody,
                    IsHarmful = (int)d.IsHarmful,
                    CommentCreatedAt = d.CommentCreatedAt,

                });
            return View(result);
        }
    }
}
