using Degrey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Degrey.Controllers
{
   
    public class LoginUserController : Controller
    {
        DegreyEntities db = new DegreyEntities();
        // GET: LoginUser
        // Phương thức tạo view cho Login
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("LoginAdmin");
            }
            return View();
        }

        public ActionResult LoginAdmin()
        {
            return View();
        }

        // Xử lý tìm kiếm ID, password trong AdminUser và thông báo
        [HttpPost]
        public ActionResult LoginAdmin(AdminUser user)
        {
            var check = db.AdminUsers.Where(s => s.ID == user.ID && s.PasswordUser == user.PasswordUser).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "Sai Info";
                return View("Index");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["ID"] = user.ID;
                Session["Admin"] = check;
                Session["PasswodUser"] = user.PasswordUser;
                return View("Index");
            }
        }

        public ActionResult LogOut()
        {
            Session["Admin"] = null;
            return RedirectToAction("Home", "Home");
        }

    }

}
