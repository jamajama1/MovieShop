using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        // executes when user clicks on Register button in the view
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel requestModel)
        {
            // check if the model is valid
            if (!ModelState.IsValid)
            {
                return View(); 
            }

            // save the user registration information in the database
            // recieve the model from the view
            //sdfsdfsdfs
            var newUser = await _userService.RegisterUser(requestModel);
            return View();
        }

        // use method to display empty view
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        { 

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestModel requestModel)
        {
            var user = await _userService.LoginUser(requestModel);
            if (user == null)
            {

                // username/password is wrong
                // show message to user saying email/password is wrong
                return View();
            }

            // we create the cookie and store some information in the cookie with expiration time
            // we need to tell the asp.net app that we are going to use cookie based authentication and we can specify 
            // the details of the cookie such as name, duration, and where to redirect.

            // create all of the necessary claims inside claims object
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToShortDateString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // print out card
            // creating the cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return LocalRedirect("~/");

            // logout => 
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // invalidate the cookie and redirect to login
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
