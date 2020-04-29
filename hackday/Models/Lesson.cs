using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hackday.Models
{
    public class Lesson
    {
        public long LessonId { get; set; }

        public long CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        public string Title { get; set; }

        [MaxLength(500)]
        public string Content { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string FileLink { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string VideoLink { get; set; }


        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime DeletedDate { get; set; }
    }
}