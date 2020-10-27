using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api1.Models;

namespace api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public AppSettings AppSettings { get; }

        public TestController(IAppSettings appSettings)
        {
            this.AppSettings = appSettings as AppSettings;
        }

        [HttpGet("")]
        public ActionResult<AppSettings> GetAppSettings()
        {
            return AppSettings;
        }
    }
}