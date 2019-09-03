using ItersTutoriov1.Models;
using ItersTutoriov1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ItersTutoriov1.Controllers
{
    [Route("api/[controller]")]
    public class SigninController : Controller
    {
        private ISigninService _userService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public SigninController(ISigninService userService, IStringLocalizer<SharedResource> localizer)
        {
            _userService = userService;
            _localizer = localizer;
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Signin signin)
        {
            var user = _userService.Authenticate(signin.UserName, signin.Password);

            if (user == null)
                return BadRequest(new { message = _localizer["IncorrectUsernamePassword"] });

            return Ok(user);
        }
    }
}
