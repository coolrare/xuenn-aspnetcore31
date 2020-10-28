using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api1.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

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
        public ILogger<TestController> Logger { get; }

        public TestController(ILogger<TestController> logger, IOptionsSnapshot<AppSettings> options)
        {
            this.Logger = logger;
            this.AppSettings = options.Value;
        }

        /// <summary>
        /// 取得應用程式設定
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public ActionResult<AppSettings> GetAppSettings()
        {
            Logger.LogDebug(50, "This is {ID} item.", 123);

            return AppSettings;
        }
    }
}