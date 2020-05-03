using System;
using Microsoft.AspNetCore.Mvc;

namespace hackday.Controllers
{
    public class CompletedSurvey : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
