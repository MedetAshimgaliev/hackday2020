using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using hackday.Models;
using hackday.Data;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllCourses()
        {
            //List<Course> list = new List<Course>();
            
            using (var entities = new ApplicationDbContext())
            {
                //list = entities.Course.Where(c => c.DeletedDate == null).ToList();
                var list = entities.Course.ToList();
                return Json(list);
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveCourse(Course item)
        {
            try
            {
                Course edit = new Course();
                using (var entities = new ApplicationDbContext())
                {
                    if (item.CourseId > 0)
                    {
                        edit = await entities.Course.FirstOrDefaultAsync(c => c.DeletedDate == null && c.CourseId == item.CourseId);
                    }
                    else
                    {
                        edit.CreatedDate = DateTime.Now;
                    }

                    edit.Title = item.Title;
                    edit.Description = item.Description;

                    entities.Course.Add(edit);
                    await entities.SaveChangesAsync();
                }

                return Json("done");
            }
            catch
            {
                return Json("error");
            }
        }


        
    }
}
