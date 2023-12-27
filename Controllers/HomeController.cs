using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Degrey.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
       
        public ActionResult Store()
        {
            return View();
        }
        public ActionResult GioiThieu()
        {
            return View();
        }   
        public ActionResult ThongTinSoHuu()
        {
            return View();
        }
        public ActionResult HuongDanDatHang()
        {
            return View();
        }
        public ActionResult ChinhSachBaoMat()
        {
            return View();
        }
        public ActionResult ChinhSachVaQuyDinh()
        {
            return View();
        }
    
    }
}