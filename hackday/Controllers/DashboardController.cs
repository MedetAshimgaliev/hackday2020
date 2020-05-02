using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using hackday.Models;
using hackday.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hackday.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            List<Course> list = new List<Course>();
            var entities = new ApplicationDbContext();
            list = entities.Course.Where(c => c.DeletedDate == null).ToList();
            return View(list);
        }

        public IActionResult CourseDetails(long id)
        {
            var entities = new ApplicationDbContext();
            var Course = entities.Course.Where(c => c.DeletedDate == null && c.CourseId == id).FirstOrDefault();
            return View(Course);
        }

        public IActionResult LessonDetails(long lessonId)
        {
            var entities = new ApplicationDbContext();
            var lesson = entities.Lesson.Where(l => l.DeletedDate == null && l.LessonId == lessonId).FirstOrDefault();
            return View(lesson);
        }

        public IActionResult AddLesson(long id)
        {
            ViewBag.CourseID = id;
            return PartialView();
        }

        public IActionResult AddCourse()
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
                return Json(list);
            }
        }

        [HttpGet]
        public JsonResult GetLessons(long courseId)
        {
            var entities = new ApplicationDbContext();
            var lessons = entities.Lesson.Where(l => l.DeletedDate == null & l.CourseId == courseId);
            return Json(lessons);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCourse(CreateCourse item)
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
                    edit.ImageUrl = UploadImage(item.Image);
                    entities.Course.Add(edit);
                    await entities.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return Json("error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveLesson(CreateLesson item)
        {
            try
            {
                Lesson edit = new Lesson();
                using (var entities = new ApplicationDbContext())
                {
                    if (item.LessonId > 0)
                    {
                        edit = await entities.Lesson.FirstOrDefaultAsync(l => l.DeletedDate == null && l.LessonId == item.LessonId);
                    }
                    else
                    {
                        edit.CreatedDate = DateTime.Now;
                    }

                    edit.CourseId = item.CourseId;
                    edit.Title = item.Title;
                    edit.Content = item.Content;
                    edit.Description = item.Description;
                    edit.FileLink = item.FileLink;
                    edit.VideoLink = item.VideoLink;
                    entities.Lesson.Add(edit);
                    await entities.SaveChangesAsync();
                }
                return RedirectToAction("CourseDetails", new { id = item.CourseId });
            }
            catch
            {
                return Json("error");
            }
        }

        [HttpPost]
        public string UploadImage(IFormFile image)
        {
            string SavePath = "";
            string ImageName = "";
            if (image != null)
            {
                ImageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", ImageName);
                using (var stream = new FileStream(SavePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
            }
            return ImageName;
        }

    }



    public class CreateCourse
    {
        public long CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public DateTime CreatedDate { get; set; }

    }

    public class CreateLesson
    {
        public long LessonId { get; set; }
        public long CourseId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }        
        public string FileLink { get; set; }
        public string VideoLink { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
