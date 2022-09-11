using Core.Convertor;
using Core.DTOs;
using Core.Generator;
using Core.Security;
using Core.Sender;
using Core.Services.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Core.Convertor.ViewToString;
using Microsoft.Data.SqlClient.Server;

namespace HelloWorldMvc.Areas.Users.Controllers
{
    [Area("Users")]
    public class UserHomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IViewRenderService _render;
        public UserHomeController(IUserService service, IViewRenderService render)
        {
            _userService = service;
            _render = render;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region SignUp

        [Route("SignUp")]
        public IActionResult SignUp() => View();

        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp(SignUpViewModel signUp)
        {
            if (!ModelState.IsValid)
            {
                return View(signUp);
            }

            if (_userService.IsUsername(FixText.FixTexts(signUp.Username)))
            {
                ModelState.AddModelError("Username", "Username Exist!");
                return View(signUp);
            }
            if (_userService.IsEmail(FixText.FixTexts(signUp.Email)))
            {
                ModelState.AddModelError("Email", "Email Exist!");
                return View(signUp);
            }

            User user = new User()
            {
                Username = signUp.Username,
                Email = signUp.Email,
                Password = PasswordHashC.EncodePasswordMd5(signUp.Password),
                IsActive = false,
                ActiveCode = ActiveCodeGen.GenerateCode(),
                Created = DateTime.Now,
                // Avatar User is URL ?
            };
            _userService.Add(user);


            string Body = _render.RenderToStringAsync("Register", user);
            EmailSenders.Send(user.Email, "Register", Body);

            return View(signUp);
        }
        #endregion

        #region Active User

        public IActionResult Active(string id)
        {
            ViewBag.ActiveUser = _userService.ActiveUser(id);
            return View();
        }

        #endregion

        #region SignIn

        [Route("SignIn")]
        public IActionResult SignIn() => View();

        [Route("SignIn")]
        [HttpPost]
        public IActionResult SignIn(SignInViewModel signIn)
        {
            if (!ModelState.IsValid)
            {
                return View(signIn);
            }
            User user = _userService.FindUserByEmailOrUsername(signIn);
            if (user != null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name , user.Username),
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties()
                    {
                        IsPersistent = signIn.RememberMe
                    };
                    HttpContext.SignInAsync(principal, properties);

                    ViewBag.IsSuccess = true;
                    return View();
                }
                else
                {
                    ModelState.AddModelError("Eamil", "Your Email Address Is Not Active");
                }
                return View();
            }

            ModelState.AddModelError("Eamil", "Email Not Found Or Password Incorect");
            return View();

        }

        #endregion

        #region Forgot Password
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword() => View();

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgotPassword )
        {
            if (!ModelState.IsValid)
            {
                return View(forgotPassword);
            }

            // if useris exist 
            forgotPassword.Email = FixText.FixTexts(forgotPassword.Email);
            User user = _userService.GetUserByEmail(forgotPassword.Email);

            if(user == null)
            {
                ModelState.AddModelError("Email", "Email Not Found !");
                return View(forgotPassword);
            }


            // send him  email
            string Body = _render.RenderToStringAsync("ForgotView", user);
            EmailSenders.Send(user.Email, "Forgot", Body);
            ViewBag.IsSuccess = true;


            return View();
        }

        #endregion

        #region Reset Password

        [Route("ResetPassword")]
        public IActionResult ResetPassword(string id)
        {

            return View(new ResetPaswordViewModel()
            {
                ActiveCode = id,
            });
        }

        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(ResetPaswordViewModel resetPasword )
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasword);
            }

            User user = _userService.GetUserByActiveCode(resetPasword.ActiveCode);
            if(user == null)
            {
                return NotFound();
            }

            user.Password = PasswordHashC.EncodePasswordMd5(resetPasword.Password);
            _userService.Update(user);

            return RedirectToAction("SignIn");
        }

        #endregion

        #region SignOut

        [Route("SignOut")]
        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn");
        }

        #endregion
    }
}


    
