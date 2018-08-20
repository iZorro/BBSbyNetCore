using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using NetCoreBBS.Entities;
using NetCoreBBS.ViewModels;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace NetCoreBBS.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        public UserManager<User> UserManager { get; }

        public SignInManager<User> SignInManager { get; }

        public AdminController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<AccountController> logger)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
