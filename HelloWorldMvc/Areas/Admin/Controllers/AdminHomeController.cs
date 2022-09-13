using Core.DTOs;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminHomeController : Controller
    {
        private readonly IAdminService _adminService ;
        public AdminHomeController( IAdminService admin)
        {
            _adminService = admin;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UsersList(int postId = 1 ,string filterUsername = "" , string filterEmail = "")
        {
            AdminUsersViewModel users = _adminService.GetUsers(postId , filterUsername , filterEmail);
            return View(users);
        }
    }
}
