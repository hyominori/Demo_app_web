using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Demo_app_web.Models;
using Demo_app_web.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Demo_app_web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private IUserRepository _userRepository;

    public HomeController(ILogger<HomeController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public IActionResult Index()
    {
        var users = _userRepository.GetAll();
        ViewBag.Welcome = "Welcome to my website";
        if (HttpContext.Session.GetInt32("IsLogin").HasValue)
        {
           ViewBag.IsLogin = HttpContext.Session.GetInt32("IsLogin");
        }
        else
        {
            ViewBag.IsLogin = 0;
        }
        return View(users);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddUser(User user)
    {
        if (!ModelState.IsValid)
        {
            try
            {
                var existedUser = _userRepository.GetUserByUsername(user.Username);
                if (existedUser == null)
                {
                    _userRepository.Add(user);
                    _userRepository.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("Username", "Username is existed");
                }
                    var users = _userRepository.GetAll();
                return View("Index", users);
            }
            catch (Exception ex)
            {
                var users = _userRepository.GetAll();
                ModelState.AddModelError("", ex.Message);
                return View("Index", users);
            }
        }
        else
        {
            var users = _userRepository.GetAll();
            ModelState.AddModelError("", "Invalid model");
            return View("Index", users);
        }
    }

    private bool CheckSession()
    {
        bool isLogin = false;
        if (HttpContext.Session.GetInt32("IsLogin") == null)
        {
            HttpContext.Session.SetInt32("IsLogin", 0);
            ViewBag.IsLogin = 0;

        }
        else
        {
            ViewBag.IsLogin = HttpContext.Session.GetInt32("IsLogin");
            if(ViewBag.IsLogin == 1)
            {
                isLogin = true;
            }
        }
        return isLogin;
    }
    public IActionResult DeleteUser (int id)
    {
        if (CheckSession())
        {
            try
            {
                var user = _userRepository.GetById(id);
                if (user != null)
                {
                    _userRepository.Delete(user);
                    _userRepository.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("Username", "User is not existed");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }
        var users = _userRepository.GetAll();
        return View("Index", users);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateUser(User user)
    {
        if (CheckSession())
        {
            try
            {
                _userRepository.Update(user);
                _userRepository.SaveChanges();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }
        
        var users = _userRepository.GetAll();
        return View("Index", users);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(User user)
    {
        try
        {
            var _user = _userRepository.GetUserByUsernameAndPassword(user.Username, user.Password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", _user.Id);
                HttpContext.Session.SetString("Username", _user.Username);
                HttpContext.Session.SetInt32("IsLogin", 1);

                var UserInfo =
                    new
                    {
                        user.Id,
                        user.Username,
                        user.Email
                    };
                HttpContext.Session.SetString("UserInfo", UserInfo.ToString());
                ViewBag.IsLogin = 1;
            }
            else
            {
                HttpContext.Session.SetInt32("IsLogin", 0);
                ViewBag.IsLogin = 0;
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }
        var users = _userRepository.GetAll();
        return View("Index", users);
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
