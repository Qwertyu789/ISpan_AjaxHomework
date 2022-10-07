using Microsoft.AspNetCore.Mvc;
using prjAjaxHW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjAjaxHW.Controllers
{
    public class HomeworkController : Controller
    {
        private readonly DemoContext _context;
        private readonly NorthwindContext _db;
        public HomeworkController(DemoContext context, NorthwindContext db)
        {
            _context = context;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult CheckName(string Name)
        {
            string res = "名字不可為空白！";
            if (!string.IsNullOrWhiteSpace(Name))
            {
                var a = _context.Members.Where(i => i.Name == Name).FirstOrDefault();
                if (a != null)
                    res = "此名稱已有人使用！";
                else
                    res = "此名稱可以使用！";

            }
            return Content(res, "text/plain", System.Text.Encoding.UTF8);
        }
        public IActionResult Address()
        {
            return View();
        }
        public IActionResult City()
        {
            var cities = _context.Addresses.Select(a => a.City).Distinct();
            return Json(cities);
        }
        public IActionResult Site(string city)
        {
            var sites = _context.Addresses.Where(a => a.City == city).Select(a => a.SiteId).Distinct();
            return Json(sites);
        }
        public IActionResult Road(string site)
        {
            var roads = _context.Addresses.Where(a => a.SiteId == site).Select(a => a.Road).Distinct();
            return Json(roads);
        }
        public IActionResult AutoComplete()
        {
            return View();
        }
        public IActionResult keyword(string keyword)
        {
            var res = _db.Products.Where(p => p.ProductName.ToUpper().Contains(keyword)).Select(p => p.ProductName);
            return Json(res);
        }
    }
}
