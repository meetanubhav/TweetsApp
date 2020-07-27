using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace DemoApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            var id = HttpContext.Session.GetInt32("userId");
            if (id > 0)
            {
                var userdetails = await _context.Userdetails.FirstOrDefaultAsync(m => m.Id == id);
                UserTweetVM ut = new UserTweetVM(){
                    UserId = userdetails.Id,
                    UserName = userdetails.Name,
                };
                ViewBag.AllTweets = await _context.TweetModels.Take(10).ToListAsync();
                return View(ut);
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
        public IActionResult Registration()
        {
            ViewData["Message"] = "Registration Page";

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userdetails = await _context.Userdetails
                .SingleOrDefaultAsync(m => m.Email == model.Email && m.Password == model.Password);
                if (userdetails == null)
                {
                    ModelState.AddModelError("Password", "Invalid login attempt.");
                    return View("Index");
                }
                HttpContext.Session.Clear();
                HttpContext.Session.SetInt32("userId",userdetails.Id);
                ViewBag.AllTweets = await _context.TweetModels.Take(10).ToListAsync();
                return RedirectToAction("Index", "Account");
            }
                return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegistrationViewModel model)
        {

            if (ModelState.IsValid)
            {
                Userdetails user = new Userdetails
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    Mobile = model.Mobile

                };
                _context.Add(user);
                await _context.SaveChangesAsync();

            }
            else
            {
                return View("Registration");
            }
            return RedirectToAction("Index", "Account");
        }
         [HttpPost]
        public async Task<ActionResult> PostTweet(UserTweetVM tModel)
        {
            TweetModel tm = new TweetModel(){
                UserId = tModel.UserId,
                UserName = tModel.UserName,
                Tweets = tModel.Tweets
            };
            _context.Add(tm);
            await _context.SaveChangesAsync();
           return RedirectToAction("Index",new { id = tModel.UserId});

        }
        // public List<TweetModel> GetAllTweets()
        // {
        //     List<TweetModel> uTweets =  _context.TweetModels.ToListAsync();
        //     return(uTweets);
        // }

    }
}
