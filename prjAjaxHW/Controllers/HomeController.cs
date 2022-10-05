using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using prjAjaxHW.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace prjAjaxHW.Controllers
{
    public class HomeController : Controller
    {

        private readonly DemoContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _host;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment host, DemoContext context)
        {
            _context = context;
            _logger = logger;
            _host = host;
        }

        public IActionResult Index()
        {
            return View();
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
        public IActionResult Register(Member member, IFormFile File1) //接收資料
        {
            if (File1 != null)
            {
                string filePath = Path.Combine(_host.WebRootPath, "uploads", File1.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    File1.CopyTo(fileStream);
                }
                byte[] imgByte = null;
                using (var memoryStream = new MemoryStream())
                {
                    File1.CopyTo(memoryStream);
                    imgByte = memoryStream.ToArray();
                }
                member.FileName = File1.FileName;
                member.FileData = imgByte;
            }
            _context.Members.Add(member);
            _context.SaveChanges();
            string res = "會員新增成功！";

            return Content(res, "text/plain");
        }

    }
}
