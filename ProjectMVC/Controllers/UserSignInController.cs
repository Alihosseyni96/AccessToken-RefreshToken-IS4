using Microsoft.AspNetCore.Mvc;
using ProjectMVC.Data.Models.Models;
using ProjectMVC.Data.Models.Services;
using ProjectMVC.Data.Models.ViewModels;

namespace ProjectMVC.Controllers
{
    public class UserSignInController : Controller
    {
        private readonly IUserService _userService;
        private readonly ProjectMvcContext projectMvcContext;

        public UserSignInController(IUserService userService,ProjectMvcContext projectMvcContext)
        {
            _userService = userService;
            this.projectMvcContext = projectMvcContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UserSignIn(SignInViewModel signIn)
        {
            var xx = projectMvcContext.Users.ToList();
            var res = await _userService.UserSignIn(signIn.NationalCode);
            //get token
            //get date from api resources
            return View("UserInfo" , res);
        }
    }
}
