using System;
using Microsoft.AspNetCore.Identity;

namespace hackday.Models
{
    public class CompletedSurvey
    {
        public int Id { get; set; }

        public string SurveyResult { get; set; }

        public IdentityUser User { get; set; }

        public string UserId { get; set; }
    }
}
