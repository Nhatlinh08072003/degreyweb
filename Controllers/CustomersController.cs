using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Degrey.Models;

namespace Degrey.Controllers
{
    public class CustomersController : Controller
    {
        private DegreyEntities db = new DegreyEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCus,NameCus,PhoneCus,EmailCus,UserName,Password")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }
        // Tạo view cho khách hàng Login
        public ActionResult LoginCus()
        {
            return View();
        }
        // Xử lý tìm kiếm UserName, password trong Customer và thông báo
        [HttpPost]
        public ActionResult LoginCus(Customer _cus)
        {
            // check là khách hàng cần tìm
            var check = db.Customers.Where(s => s.UserName == _cus.UserName && s.Password == _cus.Password).FirstOrDefault();
            if (check == null)  //không có KH
            {
                ViewBag.ErrorInfo = "Không có KH này";
                return View("LoginCus");
            }
            else
            {   // Có tồn tại KH -> chuẩn bị dữ liệu đưa về lại ShowCart.cshtml
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["IDCus"] = check.IDCus;
                Session["Passwod"] = check.Password;
                Session["NameCus"] = check.NameCus;
                Session["PhoneCus"] = check.PhoneCus;
                // Quay lại trang giỏ hàng với thông tin cần thiết
                return RedirectToAction("ShowCart", "ShoppingCart");
            }
        }

        // Regíter
        [HttpGet]
        public ActionResult RegisterCus()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterCus(Customer cus)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(cus.NameCus))
                {
                    ModelState.AddModelError(string.Empty, "Họ tên không được để trống");
                }
                if (string.IsNullOrEmpty(cus.PhoneCus))
                {
                    ModelState.AddModelError(string.Empty, "Số điện thoại không được để trống");
                }
                if (string.IsNullOrEmpty(cus.EmailCus))
                {
                    ModelState.AddModelError(string.Empty, "Email không được để trống");
                }
                if (string.IsNullOrEmpty(cus.UserName))
                {
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                }
                if (string.IsNullOrEmpty(cus.Password))
                {
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                }

                //kiểm tra xem có người nào đã đăng ký với tên đăng nhập này hay chưa
                var khachhang = db.Customers.FirstOrDefault(k => k.UserName == cus.UserName);
                if (khachhang != null)
                {
                    ModelState.AddModelError(string.Empty, "Đã có người đăng ký tên này");
                }

                if (ModelState.IsValid)
                {
                    db.Customers.Add(cus);
                    db.SaveChanges();
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("LoginCus");
        }

        public ActionResult LogOut()
        {
            Session["NameCus"] = null;
            return RedirectToAction("Home", "BookStore");
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        
       
        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCus,NameCus,PhoneCus,EmailCus,UserName,Password")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
