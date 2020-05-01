using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace hackday.Models
{
    public class Course
    {
        public long CourseId { get; set; }
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date)]
        public Nullable<DateTime> DeletedDate { get; set; }

        public ICollection<Lesson> Lessons { get; set; }

    }
}
