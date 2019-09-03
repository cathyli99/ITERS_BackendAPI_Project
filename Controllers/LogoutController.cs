using System;
using System.Linq;
using System.Security.Claims;
using ItersTutoriov1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItersTutoriov1.Controllers
{
    [Route("api/[controller]")]
    public class LogoutController : Controller
    {
        private readonly ITERSTutoriov10Context _db;
        public LogoutController(ITERSTutoriov10Context db)
        {
            _db = db;
        }

        // PUT api/logout
        [Authorize]
        [HttpPut]
        public void Put()
        {
            Guid.TryParse(User.FindFirst(ClaimTypes.Name)?.Value, out var userId);
            var tbSubscriptionTokens = (from i in _db.TbSubscriptionTokens
                where i.Id == userId.ToString() && i.ExpiresOn > DateTime.Now
                select i).FirstOrDefault();

            if (tbSubscriptionTokens != null)
            {
                tbSubscriptionTokens.ExpiresOn = DateTime.Now;

                _db.TbSubscriptionTokens.Update(tbSubscriptionTokens);
                _db.SaveChanges();
            }
        }
    }
}
