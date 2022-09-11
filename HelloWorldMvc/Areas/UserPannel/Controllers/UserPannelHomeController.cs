using Microsoft.AspNetCore.Mvc;

namespace HelloWorldMvc.Areas.UserPannel.Controllers
{
    public class UserPannelHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
