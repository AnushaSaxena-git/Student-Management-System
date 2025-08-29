using Crudbyme;
using Crudbyme.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Crudbyme.Controllers
{
    public class Admin : Controller
    {
        private readonly JwtTokenService _jwtTokenService;

        public Admin(JwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }
        public IActionResult Login()
        {
            return View(new LoginDto());
        }
    

[HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {
            // Validate user credentials 
            if (IsValidUser(loginDto.UserName, loginDto.Password))
            {
                // If valid, generate the token
                var token = _jwtTokenService.GenerateToken(loginDto.UserName);
                //return Ok(new TokenModel { Token = token });
                Response.Cookies.Append("JWT", token);
                return RedirectToAction("Index", "Home");
            }


            // If authentication fails, return Unauthorized
            return Unauthorized();
        }


        private bool IsValidUser(string username, string password)
        {
            return username == "anusha_saxena" && password == "123456";
            //return LoginDto.Any(user => user.UserName == username && user.Password == password);
            //return LoginDto.Any(LoginDto => LoginDto.UserName == username && LoginDto.Password == password);

        }
    }
}

//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;
//using Crudbyme.Dtos;
//using Crudbyme.Models;
//using Microsoft.EntityFrameworkCore;

//namespace Crudbyme.Controllers
//{
//    public class AuthController(IStudentRepository studentRepository) : Controller
//    {


//            [HttpGet]
//            public IActionResult Login()
//            {
//                return View();
//            }

//            // Handle login submission
//            [HttpPost]
//            public async Task<IActionResult> Login(LoginDto loginDto)
//            {

//            var user = await studentRepository
//                .GetQuery()
//                .FirstOrDefaultAsync(x => x.UserName == loginDto.UserName && x.Password == loginDto.Password);

//                if (user != null)
//                {
//                    var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.Name, loginDto.UserName),
//                new Claim(ClaimTypes.Role, loginDto.Password == "admin" ? "Admin" : "User")
//            };

//                    // Create identity and sign-in the user
//                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
//                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

//                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

//                    return RedirectToAction("Index", "Home"); // Redirect to home or another page
//                }

//                ViewBag.error = "Credentials not valid.";
//                return View();
//            }

//            // Logout logic
//            public async Task<IActionResult> Logout()
//            {
//                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
//                return RedirectToAction("Login");
//            }
//        }

//    }
//
//using Crudbyme.Dtos;
//using Microsoft.AspNetCore.Mvc;

//namespace Crudbyme.Controllers
//{
//    public class AuthController : Controller
//    {
//        private readonly JwtTokenService _jwtTokenService;

//        public AuthController(JwtTokenService jwtTokenService)
//        {
//            _jwtTokenService = jwtTokenService;
//        }
//        [HttpGet]
//        public IActionResult Login()
//        {
//            return View(new LoginDto());
//        }



//        //  [Route("api/[controller]")]

//        [HttpPost("Login")]
//        public IActionResult Login(LoginDto loginDto)
//        {
//            // Validate user credentials 
//            if (IsValidUser(loginDto.UserName, loginDto.Password))
//            {
//                // If valid, generate the token
//                var token = _jwtTokenService.GenerateToken(loginDto.UserName);
//                //return Ok(new TokenModel { Token = token });
//                return RedirectToAction("Index", "Home");
//            }


//            // If authentication fails, return Unauthorized
//            return Unauthorized();
//        }


//        private bool IsValidUser(string username, string password)
//        {
//            return username == "anusha_saxena" && password == "123456";
//        }
//    }
//}
