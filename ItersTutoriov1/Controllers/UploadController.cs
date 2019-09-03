using System;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

namespace ItersTutoriov1.Controllers
{
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IHttpContextAccessor _accessor;
        public IConfiguration _configuration { get; }
        public IHostingEnvironment _environment;
        public UploadController(IStringLocalizer<SharedResource> localizer, IHttpContextAccessor accessor, IConfiguration configuration, IHostingEnvironment environment)
        {
            _localizer = localizer;
            _accessor = accessor;
            _configuration = configuration;
            _environment = environment;
        }

        [Authorize]
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Post()
        {
            try
            {
                var file = Request.Form.Files[0];
                var envPath = _configuration["Settings:EnvironmentPath"];
                var uploadPath = _configuration["Settings:UploadPath"];
                var folderName = envPath.Replace("/", "\\") + uploadPath.Replace("/","\\");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var request = _accessor.HttpContext.Request;
                    var urlPath = $"{request.Scheme}://{request.Host.Value}/{uploadPath}/{fileName}";
                    return Ok(new { urlPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(500, _localizer["InternalServerError"]);
            }
        }
    }
}
