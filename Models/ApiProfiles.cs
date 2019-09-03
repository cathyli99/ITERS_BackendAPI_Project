using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItersTutoriov1.Models
{
    public class Subscription
    {
        public Guid UniqueId { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string WeChatId { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string FaxNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string GroupId { get; set; }
        public int SubscriptionStatusId { get; set; }
        public string SubscriptionStatusName { get; set; }
        public bool WantsNewsletter { get; set; }
        public DateTime JoinDate { get; set; }
        public string IpAddress { get; set; }
        public string ActivationKey { get; set; }
        public string PasswordResetKey { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string Notes { get; set; }
        public string Title { get; set; }
        public string Portrait { get; set; }
        public string DisplayName { get; set; }
        public string AboutMe { get; set; }
        public string CurrentMe { get; set; }
        public bool AllowDisplayRealName { get; set; }
        public bool AllowProfilePublic { get; set; }
        public bool SubscribeNewsletter { get; set; }
        public bool SubscribeNewContentReleases { get; set; }
        public bool SubscribeFeatureUpdates { get; set; }
        public bool SubscribeTeacherEmails { get; set; }
        public bool SubscribeContentSuggestions { get; set; }
    }

    public class Profile
    {
        public Guid UniqueId { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string WeChatId { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string FaxNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string GroupId { get; set; }
        public int SubscriptionStatusId { get; set; }
        public string SubscriptionStatusName { get; set; }
        public bool WantsNewsletter { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string Title { get; set; }
        public string Portrait { get; set; }
        public string DisplayName { get; set; }
        public string AboutMe { get; set; }
        public string CurrentMe { get; set; }
        public bool? AllowDisplayRealName { get; set; }
        public bool? AllowProfilePublic { get; set; }
        public bool? SubscribeNewsletter { get; set; }
        public bool? SubscribeNewContentReleases { get; set; }
        public bool? SubscribeFeatureUpdates { get; set; }
        public bool? SubscribeTeacherEmails { get; set; }
        public bool? SubscribeContentSuggestions { get; set; }
        public string Role { get; set; }
    }

    public class Signup
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool WantsNewsletter { get; set; }
    }
}
