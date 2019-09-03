using System;
using System.Collections.Generic;
using System.Linq;
using ItersTutoriov1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItersTutoriov1.Controllers
{
    [Route("api/[controller]")]
    public class CourseSubscriptionsController : Controller
    {
        private readonly ITERSTutoriov10Context _db;
        public CourseSubscriptionsController(ITERSTutoriov10Context db)
        {
            _db = db;
        }

        // GET api/coursesubscriptions/5
        [Authorize]
        [HttpGet("{id}")]
        public IEnumerable<Subscriber> Get(int id)
        {
            var tbCourseSubscriptions = (from i in _db.TbCourseSubscriptions
                join j in _db.TbSubscriptions
                    on i.StudentId equals j.UniqueId
                where i.CourseId == id
                orderby i.UpdateTime descending
                select new
                {
                    i.StudentId,
                    j.FirstName,
                    j.LastName,
                    i.CourseId,
                    i.SubscriptionStatus,
                    i.UpdateTime
                }).ToList();

            var subscribers = new List<Subscriber>();
            foreach (var courseSubscription in tbCourseSubscriptions)
            {
                subscribers.Add(new Subscriber
                {
                    StudentId = courseSubscription.StudentId,
                    FirstName = courseSubscription.FirstName,
                    LastName = courseSubscription.LastName,
                    CourseId = courseSubscription.CourseId,
                    SubscriptionStatus = Convert.ToBoolean(courseSubscription.SubscriptionStatus),
                    UpdateTime = courseSubscription.UpdateTime
                });
            }

            return subscribers;
        }
    }
}
