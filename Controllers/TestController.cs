using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api1.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace api1.Controllers
{
    /// <summary>
    /// Test API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class TestController : ControllerBase
    {
        public AppSettings AppSettings { get; }

        public TestController(IOptionsSnapshot<AppSettings> options)
        {
            this.AppSettings = options.Value;
        }

        /// <summary>
        /// 取得應用程式設定
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public ActionResult<AppSettings> GetAppSettings()
        {
            return AppSettings;
        }
    }
}