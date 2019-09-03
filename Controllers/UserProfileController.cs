using System;
using System.Linq;
using ItersTutoriov1.Helper;
using ItersTutoriov1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ItersTutoriov1.Controllers
{
    [Route("api/[controller]")]
    public class UserProfileController : Controller
    {
        private readonly ITERSTutoriov10Context _db;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private IHttpContextAccessor _accessor;

        public UserProfileController(ITERSTutoriov10Context db, IStringLocalizer<SharedResource> localizer, IHttpContextAccessor accessor)
        {
            _db = db;
            _localizer = localizer;
            _accessor = accessor;
        }

        public enum SubscriptionStatus
        {
            SubscriptionStatusUnverified = 1,
            SubscriptionStatusActive = 2,
            SubscriptionStatusInactive = 3,
            SubscriptionStatusUnapproved = 4
        }

        // GET api/userprofile/5
        [Authorize]
        [HttpGet("{id}")]
        public Profile Get(Guid id)
        {
            var tbSubscription = (from i in _db.TbSubscriptions
                where i.UniqueId == id
                select i).FirstOrDefault();

            var profile = new Profile();
            if (tbSubscription != null)
            {
                var tbSubscriptionStatuses = (from i in _db.TbSubscriptionStatuses
                    where i.SubscriptionStatusId == tbSubscription.SubscriptionStatusId
                    select i).FirstOrDefault();

                profile.UniqueId = tbSubscription.UniqueId;
                profile.Email = tbSubscription.Email;
                profile.FirstName = tbSubscription.FirstName;
                profile.LastName = tbSubscription.LastName;
                profile.BirthDate = tbSubscription.Birthdate;
                profile.WeChatId = tbSubscription.WeChatId;
                profile.PhoneNumber = tbSubscription.PhoneNumber;
                profile.MobileNumber = tbSubscription.MobileNumber;
                profile.FaxNumber = tbSubscription.FaxNumber;
                profile.Address = tbSubscription.Address;
                profile.City = tbSubscription.City;
                profile.State = tbSubscription.State;
                profile.PostCode = tbSubscription.PostCode;
                profile.CountryCode = tbSubscription.CountryCode;
                profile.CountryName = string.Empty;
                profile.SubscriptionStatusId = tbSubscription.SubscriptionStatusId;
                profile.SubscriptionStatusName = tbSubscriptionStatuses == null? string.Empty : tbSubscriptionStatuses.SubscriptionStatusName;
                profile.WantsNewsletter = tbSubscription.WantsNewsletter;
                profile.JoinDate = tbSubscription.JoinDate;
                profile.UpdatedTime = tbSubscription.UpdatedTime;
                profile.Title = tbSubscription.Title;
                profile.Portrait = tbSubscription.Portrait;
                profile.DisplayName = tbSubscription.DisplayName;
                profile.AboutMe = tbSubscription.AboutMe;
                profile.CurrentMe = tbSubscription.CurrentMe;
                profile.AllowDisplayRealName = tbSubscription.AllowDisplayRealName;
                profile.AllowProfilePublic = tbSubscription.AllowProfilePublic;
                profile.SubscribeNewsletter = tbSubscription.SubscribeNewsletter;
                profile.SubscribeNewContentReleases = tbSubscription.SubscribeNewContentReleases;
                profile.SubscribeFeatureUpdates = tbSubscription.SubscribeFeatureUpdates;
                profile.SubscribeTeacherEmails = tbSubscription.SubscribeTeacherEmails;
                profile.SubscribeContentSuggestions = tbSubscription.SubscribeContentSuggestions;
                profile.GroupId = tbSubscription.GroupId;
            }

            return profile;
        }

        // POST api/userprofile
        [HttpPost]
        public ApiReturnMessage Post([FromBody]Signup signup)
        {
            var tbSubscription = (from i in _db.TbSubscriptions
                where i.Email == signup.Email
                select i).FirstOrDefault();

            var apiReturnMessage = new ApiReturnMessage();
            if (tbSubscription == null)
            {
                var encryptedPassword = Security.EncryptPassword(signup.Password, out var salt);
                var uId = Guid.NewGuid();
                tbSubscription = new TbSubscriptions()
                {
                    UniqueId = uId,
                    Email = signup.Email,
                    Password = encryptedPassword,
                    Salt = salt,
                    GroupId = signup.Role,
                    WantsNewsletter = signup.WantsNewsletter,
                    SubscriptionStatusId = (int)SubscriptionStatus.SubscriptionStatusActive,
                    JoinDate = DateTime.Now,
                    IpAddress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    ActivationKey = uId + DateTime.Now.Ticks.ToBase36()
                };
                _db.TbSubscriptions.Add(tbSubscription);
                _db.SaveChanges();

                apiReturnMessage.Status = _localizer["StatusSuccess"];
                apiReturnMessage.Message = _localizer["AccountCreated"];
            }
            else
            {
                apiReturnMessage.Status = _localizer["StatusFail"];
                apiReturnMessage.Message = _localizer["EmailDoesExist"];
            }

            return apiReturnMessage;
        }

        // PUT api/userprofile/id
        [Authorize]
        [HttpPut("{id}")]
        public ApiReturnMessage Put(Guid id, [FromBody]Subscription subscription)
        {
            var tbSubscription = (from i in _db.TbSubscriptions
                where i.UniqueId == id
                select i).FirstOrDefault();

            // Check whether email is changed
            // TODO

            var apiReturnMessage = new ApiReturnMessage();
            if (tbSubscription != null)
            {
                if (subscription != null)
                {
                    tbSubscription.Email = subscription.Email;
                    tbSubscription.FirstName = subscription.FirstName;
                    tbSubscription.LastName = subscription.LastName;
                    tbSubscription.Birthdate = subscription.BirthDate;
                    tbSubscription.WeChatId = subscription.WeChatId;
                    tbSubscription.PhoneNumber = subscription.PhoneNumber;
                    tbSubscription.MobileNumber = subscription.MobileNumber;
                    tbSubscription.FaxNumber = subscription.FaxNumber;
                    tbSubscription.Address = subscription.Address;
                    tbSubscription.City = subscription.City;
                    tbSubscription.State = subscription.State;
                    tbSubscription.PostCode = subscription.PostCode;
                    tbSubscription.CountryCode = subscription.CountryCode;
                    tbSubscription.WantsNewsletter = subscription.WantsNewsletter;
                    tbSubscription.UpdatedTime = DateTime.Now;
                    tbSubscription.Title = subscription.Title;
                    tbSubscription.Portrait = subscription.Portrait;
                    tbSubscription.DisplayName = subscription.DisplayName;
                    tbSubscription.AboutMe = subscription.AboutMe;
                    tbSubscription.CurrentMe = subscription.CurrentMe;
                    tbSubscription.AllowDisplayRealName = subscription.AllowDisplayRealName;
                    tbSubscription.AllowProfilePublic = subscription.AllowProfilePublic;
                    tbSubscription.SubscribeNewsletter = subscription.SubscribeNewsletter;
                    tbSubscription.SubscribeNewContentReleases = subscription.SubscribeNewContentReleases;
                    tbSubscription.SubscribeFeatureUpdates = subscription.SubscribeFeatureUpdates;
                    tbSubscription.SubscribeTeacherEmails = subscription.SubscribeTeacherEmails;
                    tbSubscription.SubscribeContentSuggestions = subscription.SubscribeContentSuggestions;
                    tbSubscription.GroupId = subscription.GroupId;

                    _db.TbSubscriptions.Update(tbSubscription);
                    _db.SaveChanges();
                }

                apiReturnMessage.Status = _localizer["StatusSuccess"];
                apiReturnMessage.Message = _localizer["AccountUpdatedSuccess"];
            }
            else
            {
                apiReturnMessage.Status = _localizer["StatusFail"];
                apiReturnMessage.Message = string.Format(_localizer["AccountIdDoesNotExist"], id);
            }

            return apiReturnMessage;
        }
    }
}
