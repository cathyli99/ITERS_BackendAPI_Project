using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ItersTutoriov1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItersTutoriov1.Controllers
{
    [Route("api/[controller]")]
    public class SubscriptionCoursesController : Controller
    {
        private readonly ITERSTutoriov10Context _db;
        public SubscriptionCoursesController(ITERSTutoriov10Context db)
        {
            _db = db;
        }

        // GET: api/subscriptioncourses
        [Authorize]
        [HttpGet]
        public IEnumerable<ApiSubscriptionCourse> Get()
        {
            Guid.TryParse(User.FindFirst(ClaimTypes.Name)?.Value, out var studentId);
            var tbSubscriptionCourses = (from i in _db.TbCourseSubscriptions
                join j in _db.TbCourses
                    on i.CourseId equals j.CourseId
                where i.StudentId == studentId
                orderby i.UpdateTime descending
                select new
                {
                    i.StudentId,
                    i.CourseId,
                    j.CourseCode,
                    j.CourseName,
                    i.SubscriptionStatus,
                    i.UpdateTime
                }).ToList();

            var subscriptionCourses = new List<ApiSubscriptionCourse>();
            foreach (var subscriptioncourse in tbSubscriptionCourses)
            {
                subscriptionCourses.Add(new ApiSubscriptionCourse
                {
                    StudentId = subscriptioncourse.StudentId,
                    CourseId = subscriptioncourse.CourseId,
                    CourseCode = subscriptioncourse.CourseCode,
                    CourseName = subscriptioncourse.CourseName,
                    SubscriptionStatus = Convert.ToBoolean(subscriptioncourse.SubscriptionStatus),
                    SubscriptionStatusDesc = ((SubscriptionStatus)subscriptioncourse.SubscriptionStatus).ToString(),
                    UpdateTime = subscriptioncourse.UpdateTime
                });
            }

            return subscriptionCourses;
        }

        // POST api/subscriptioncourses
        [Authorize]
        [HttpPost]
        public void Post([FromBody]ApiSubscriptionCourse apiSubscriptionCourse)
        {
            _db.TbCourseSubscriptions.Add(new TbCourseSubscriptions()
            {
                StudentId = apiSubscriptionCourse.StudentId,
                CourseId = apiSubscriptionCourse.CourseId,
                SubscriptionStatus = Convert.ToByte(apiSubscriptionCourse.SubscriptionStatus),
                UpdateTime = DateTime.Now
            });
            _db.SaveChanges();
        }

        // PUT api/subscriptioncourses/id
        [Authorize]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]ApiSubscriptionCourse apiSubscriptionCourse)
        {
            var tbCourseSubscriptions = (from i in _db.TbCourseSubscriptions
                where i.StudentId == apiSubscriptionCourse.StudentId && i.CourseId == id
                select i).FirstOrDefault();

            if (tbCourseSubscriptions != null)
            {
                tbCourseSubscriptions.SubscriptionStatus = Convert.ToByte(apiSubscriptionCourse.SubscriptionStatus);
                tbCourseSubscriptions.UpdateTime = DateTime.Now;

                _db.TbCourseSubscriptions.Update(tbCourseSubscriptions);
                _db.SaveChanges();
            }
        }
    }
}
