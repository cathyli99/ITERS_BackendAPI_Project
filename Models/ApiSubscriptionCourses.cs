using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItersTutoriov1.Models
{
    public enum SubscriptionStatus
    {
        Subscribe = 1,
        Unsubscribe = 0
    }

    public class ApiSubscriptionCourse
    {
        public Guid StudentId { get; set; }
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public bool SubscriptionStatus { get; set; }
        public string SubscriptionStatusDesc { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
