using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api1.Models;
using api1.Helpers;
using Microsoft.AspNetCore.Http;

namespace api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public JwtHelpers JwtHelpers { get; }

        public LoginController(JwtHelpers jwtHelpers)
        {
            this.JwtHelpers = jwtHelpers;
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ActionResult<TokenResult> PostTokenResult(LoginViewModel user)
        {
            if (ValidateUser(user.Username, user.Password))
            {
                return new TokenResult() {
                  Token = JwtHelpers.GenerateToken(user.Username, 20)
                };
            }
            else
            {
                return BadRequest();
            }
        }

        private bool ValidateUser(string user, string pass)
        {
            return true;
        }
    }
}