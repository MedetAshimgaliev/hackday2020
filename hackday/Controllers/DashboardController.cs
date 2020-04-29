using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using hackday.Models;
using hackday.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hackday.Controllers
{
    public class DashboardController : Controller
    {
        

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllCourses()
        {
            List<Course> list = new List<Course>();
            using (var entities = new ApplicationDbContext())
            {
                list = entities.Course.Where(c => c.DeletedDate == null).ToList();
            }

            return Json(list);
            
        }
    }
}
