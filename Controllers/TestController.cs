using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api1.Models;
using Microsoft.Extensions.Options;

namespace api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public AppSettings AppSettings { get; }

        public TestController(IOptions<AppSettings> options)
        {
            this.AppSettings = options.Value;
        }

        [HttpGet("")]
        public ActionResult<AppSettings> GetAppSettings()
        {
            return AppSettings;
        }
    }
}