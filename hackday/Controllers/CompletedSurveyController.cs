using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using hackday.Data;
using hackday.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hackday.Controllers
{
    public class CompletedSurveyController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CompletedSurveyController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostSaveAsync(string data)
        {
            //var userId = User.GetUserId();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var surveyResult = await _dbContext.CompletedSurveys
                .Where(s => s.UserId == userId)
                .FirstOrDefaultAsync();
            if (surveyResult != null)
            {
                surveyResult.SurveyResult = data;
            }
            else
            {
                _dbContext.CompletedSurveys.Add(new CompletedSurvey
                {
                    SurveyResult = data,
                    UserId = userId
                });
            }
            await _dbContext.SaveChangesAsync();

            return new OkResult();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
