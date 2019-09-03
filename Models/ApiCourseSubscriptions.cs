using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItersTutoriov1.Models
{
    public class Subscriber
    {
        public Guid StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CourseId { get; set; }
        public bool SubscriptionStatus { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
