using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BletExamIdeas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BletExamIdeas.Controllers
{
    public class HomeController : Controller
    {
    private HomeContext _context;
 
    public HomeController(HomeContext context)
    {
        _context = context;
    }
    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("adduser")]
    public IActionResult AddUsertoDb(RegisterViewModel Models)
    {
        if(ModelState.IsValid)
        {
            User email = _context.users.SingleOrDefault( u =>u.Email== Models.Email);
            if(email != null)
            {
                ViewBag.ExsitEmail ="Email exsit!";
                return View("Index");
            }

            User NewUser = new User()
            {
                Name = Models.Name,
                Alias = Models.Alias,
                Email = Models.Email,
                Password = Models.Password,
                CreatedAt = Models.CreatedAt,
                UpdatedAt = Models.UpdatedAt
            };

            _context.Add(NewUser);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("loggedUserId",(int)NewUser.UserId);
            HttpContext.Session.SetString("loggedUserName",(string)NewUser.Alias);

            return RedirectToAction("UserDashboard");
            }

           else
           {

            return View("Index");
           }

        }

         [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View("login");
        }  

        [HttpPost]
        [Route("loginuser")]
        public IActionResult loginuser(Login Models)
        {
            if(ModelState.IsValid)
            {
                User returnedUser = _context.users.SingleOrDefault(u => u.Email ==Models.loginEmail);

                if(returnedUser == null)
                {
                    ViewBag.ErrorLogin = "This email doesn't exist. Please register an account.";
                    return View("Index");
                }

                else
                {
                    if(returnedUser.Password == Models.loginPassword)
                    {
                        HttpContext.Session.SetInt32("loggedUserId",(int)returnedUser.UserId);
                        HttpContext.Session.SetString("loggedUserName",(string)returnedUser.Alias);
                        return RedirectToAction("UserDashboard");
                    }
                    ViewBag.ErrorLogin = "Password is incorrect!";
                }
            }
            
            return View("Index");
            
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("UserDashboard")]
        public IActionResult UserDashboard()
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("loggedUserId");
            ViewBag.Username = HttpContext.Session.GetString("loggedUserName");
            List<Post>Allmessages = _context.posts.Include(u => u.PostLikedByUser).ThenInclude(g =>g.User).Include(c =>c.PostCreater).ToList();
            ViewBag.Allmessages = Allmessages;
            return View();
        }

        [HttpPost]
        [Route("addpost")]
        public IActionResult addpost(Post model)
        {
             if(ModelState.IsValid)
             {
                 Post NewPost = new Post
                 {
                 Massage = model.Massage,
                 PostCreaterId  = (int)HttpContext.Session.GetInt32("loggedUserId")
                 };
                 _context.Add(NewPost);
                 _context.SaveChanges();
              
             }
              return RedirectToAction("UserDashboard");
        }

        [HttpGet]
        [Route("delete/{PostId}")]
        public IActionResult Delete(int PostId)
        {
            List<Like>Creater = _context.likes.Where(c =>c.PostId ==PostId).ToList();
            foreach( var c in Creater)
            {
                _context.likes.Remove(c);
            }
            Post delete = _context.posts.Where(a => a.PostId ==PostId).SingleOrDefault();
            _context.posts.Remove(delete);
            _context.SaveChanges();
            return RedirectToAction("UserDashboard");

        }

        [HttpGet]
        [Route("Join/{PostId}")]
        public IActionResult Join(int PostId)
        {
            int? UserId = HttpContext.Session.GetInt32("loggedUserId");
            Like Newliker = new Like
            {
                UserId = (int)UserId,
               PostId = PostId
            };
            _context.Add(Newliker);
            _context.SaveChanges();
            return RedirectToAction("UserDashboard");

        }

        [HttpGet]
        [Route("showpost/{PostId}")]
        public IActionResult showpost(int PostId)
        {
            if(HttpContext.Session.GetInt32("loggedUserId")==null)
            return RedirectToAction("index");
            Post ShowPost = _context.posts.Include(a =>a.PostLikedByUser).ThenInclude(g =>g.User).Include(a =>a.PostCreater).Where(a =>a.PostId==PostId).SingleOrDefault();
            ViewBag.ShowPost = ShowPost;
            ViewBag.UserId = HttpContext.Session.GetInt32("loggedUserId");
            ViewBag.Username = HttpContext.Session.GetString("loggedUserName");
            
            return View();
        }

        [HttpGet]
        [Route("showuser/{PostCreaterId}")]
        
        public IActionResult showuser(int PostCreaterId)
        {
            if(HttpContext.Session.GetInt32("loggedUserId")==null)
            return RedirectToAction("index");
            User ShowUser = _context.users.Include(a =>a.MyPost).Include(b =>b.UserLikedPost).Where(c =>c.UserId ==PostCreaterId).SingleOrDefault();
            ViewBag.ShowUser = ShowUser;

            return View();
        }

    }
}
