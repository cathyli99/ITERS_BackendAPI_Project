using System;
using System.Linq;
using ItersTutoriov1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ItersTutoriov1.Controllers
{
    [Route("api/[controller]")]
    public class PasswordResetController : Controller
    {
        private readonly ITERSTutoriov10Context _db;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public PasswordResetController(ITERSTutoriov10Context db, IStringLocalizer<SharedResource> localizer)
        {
            _db = db;
            _localizer = localizer;
        }

        // POST api/passwordreset
        [HttpPost]
        public ApiReturnMessage Post([FromBody]PasswordReset passwordReset)
        {
            var account = (from i in _db.TbSubscriptions
                where string.Equals(i.Email, passwordReset.Email, StringComparison.CurrentCultureIgnoreCase)
                select i).FirstOrDefault();

            if (account == null)
            {
                return new ApiReturnMessage()
                {
                    Status = _localizer["StatusFail"],
                    Message = _localizer["EmailDoesNotExist"]
                };
            }
            else
            {
                // Send Email
                // TOTO

                return new ApiReturnMessage()
                {
                    Status = _localizer["StatusSuccess"],
                    Message = _localizer["PasswordResetSucceeded"]
                };
            }
        }
    }
}
