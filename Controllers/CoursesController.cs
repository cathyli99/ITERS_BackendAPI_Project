using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ItersTutoriov1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ItersTutoriov1.Controllers
{
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private readonly ITERSTutoriov10Context _db;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CoursesController(ITERSTutoriov10Context db, IStringLocalizer<SharedResource> localizer)
        {
            _db = db;
            _localizer = localizer;
        }

        // GET: api/courses
        [HttpGet]
        public IEnumerable<ApiCourse> Get(string intro = null)
        {
            List<TbCourses> tbCourses;
            if (intro != null && intro.Equals(_localizer["latest"], StringComparison.CurrentCultureIgnoreCase))
            {
                tbCourses = (from i in _db.TbCourses
                    where i.IsActive && i.IsDeleted == null && (i.IsRecommended != null && i.IsRecommended == true)
                    orderby i.JoinDate descending
                    select i).Take(1).ToList();
            }
            else
            {
                tbCourses = (from i in _db.TbCourses
                    where i.IsActive && i.IsDeleted == null
                    orderby i.JoinDate descending
                    select i).ToList();
            }

            var apiCourses = new List<ApiCourse>();
            foreach (var c in tbCourses)
            {
                apiCourses.Add(new ApiCourse
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    SubTitle = c.SubTitle,
                    CourseCode = c.CourseCode,
                    CourseBrief = c.CourseBrief,
                    CoverImage = c.CoverImage,
                    CourseDuration = c.CourseDuration,
                    CourseTags = c.CourseTags,
                    TargetAudiences = c.TargetAudiences,
                    CourseDetails = c.CourseDetails,
                    KnowledgePoints = c.KnowledgePoints,
                    Scores = c.Scores,
                    IsRecommended = c.IsRecommended,
                    JoinDate = c.JoinDate,
                    ModifiedDate = c.ModifiedDate,
                    Instructors = "/api/coursesinstructors/" + c.CourseId,
                    Students = "/api/coursesubscriptions/" + c.CourseId
                });
            }
            return apiCourses;
        }

        // GET api/courses/5
        [HttpGet("{id}")]
        public ApiCourse Get(int id)
        {
            var tbCourse = (from i in _db.TbCourses
                where i.CourseId == id
                select i).FirstOrDefault();

            var apiCourse = new ApiCourse();
            if (tbCourse != null)
            {
                apiCourse.CourseId = tbCourse.CourseId;
                apiCourse.CourseName = tbCourse.CourseName;
                apiCourse.SubTitle = tbCourse.SubTitle;
                apiCourse.CourseCode = tbCourse.CourseCode;
                apiCourse.CourseBrief = tbCourse.CourseBrief;
                apiCourse.CoverImage = tbCourse.CoverImage;
                apiCourse.CourseDuration = tbCourse.CourseDuration;
                apiCourse.CourseTags = tbCourse.CourseTags;
                apiCourse.TargetAudiences = tbCourse.TargetAudiences;
                apiCourse.CourseDetails = tbCourse.CourseDetails;
                apiCourse.KnowledgePoints = tbCourse.KnowledgePoints;
                apiCourse.Scores = tbCourse.Scores;
                apiCourse.IsRecommended = tbCourse.IsRecommended;
                apiCourse.JoinDate = tbCourse.JoinDate;
                apiCourse.ModifiedDate = tbCourse.ModifiedDate;
                apiCourse.Instructors = "/api/coursesinstructors/" + tbCourse.CourseId;
                apiCourse.Students = "/api/coursesubscriptions/" + tbCourse.CourseId;
            }

            return apiCourse;
        }

        // POST api/courses
        [Authorize]
        [HttpPost]
        public void Post([FromBody]ApiCourse course)
        {
            var tbCourse = new TbCourses()
            {
                CourseName = course.CourseName,
                SubTitle = course.SubTitle,
                CourseCode = course.CourseCode,
                CourseBrief = course.CourseBrief,
                CoverImage = course.CoverImage,
                CourseDuration = course.CourseDuration,
                CourseTags = course.CourseTags,
                TargetAudiences = course.TargetAudiences,
                CourseDetails = course.CourseDetails,
                KnowledgePoints = course.KnowledgePoints,
                Scores = course.Scores,
                IsRecommended = course.IsRecommended,
                JoinDate = course.JoinDate == null ? DateTime.Now : (DateTime)course.JoinDate,
                IsActive = course.IsActive
            };
            _db.TbCourses.Add(tbCourse);
            _db.SaveChanges();

            Guid.TryParse(User.FindFirst(ClaimTypes.Name)?.Value, out var instructorId);
            var tbCourseAssignments = new TbCourseAssignments
            {
                CourseId = tbCourse.CourseId,
                InstructorId = instructorId
            };
            _db.TbCourseAssignments.Add(tbCourseAssignments);
            _db.SaveChanges();
        }

        // PUT api/courses/5
        [Authorize]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]ApiCourse course)
        {
            var tbCourse = (from i in _db.TbCourses
                            where i.CourseId == id
                            select i).FirstOrDefault();

            Guid.TryParse(User.FindFirst(ClaimTypes.Name)?.Value, out var modifier);
            if (tbCourse != null)
            {
                tbCourse.CourseId = id;
                tbCourse.CourseName = course.CourseName;
                tbCourse.SubTitle = course.SubTitle;
                tbCourse.CourseCode = course.CourseCode;
                tbCourse.CourseBrief = course.CourseBrief;
                tbCourse.CoverImage = course.CoverImage;
                tbCourse.CourseDuration = course.CourseDuration;
                tbCourse.CourseTags = course.CourseTags;
                tbCourse.TargetAudiences = course.TargetAudiences;
                tbCourse.CourseDetails = course.CourseDetails;
                tbCourse.KnowledgePoints = course.KnowledgePoints;
                tbCourse.Scores = course.Scores;
                tbCourse.IsRecommended = course.IsRecommended;
                tbCourse.IsActive = course.IsActive;
                tbCourse.Modifier = modifier;
                tbCourse.ModifiedDate = DateTime.Now;

                _db.TbCourses.Update(tbCourse);
                _db.SaveChanges();
            }
            else
            {
                //TODO
            }
        }

        // DELETE api/courses/5
        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var tbCourse = (from i in _db.TbCourses
                            where i.CourseId == id
                            select i).FirstOrDefault();

            Guid.TryParse(User.FindFirst(ClaimTypes.Name)?.Value, out var modifier);
            if (tbCourse != null)
            {
                tbCourse.CourseId = id;
                tbCourse.IsDeleted = true;
                tbCourse.Modifier = modifier;
                tbCourse.ModifiedDate = DateTime.Now;

                _db.TbCourses.Update(tbCourse);
                _db.SaveChanges();
            }
            else
            {
                //TODO
            }
        }
    }
}
