using System;
using System.Collections.Generic;

namespace ItersTutoriov1.Models
{
    public partial class TbCourses
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string SubTitle { get; set; }
        public string CourseCode { get; set; }
        public string CourseBrief { get; set; }
        public string CoverImage { get; set; }
        public int CourseDuration { get; set; }
        public string CourseTags { get; set; }
        public string TargetAudiences { get; set; }
        public string CourseDetails { get; set; }
        public string KnowledgePoints { get; set; }
        public double Scores { get; set; }
        public bool? IsRecommended { get; set; }
        public bool IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime JoinDate { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
