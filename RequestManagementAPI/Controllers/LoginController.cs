using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RequestManagementAPI.Models;
using RequestManagementAPI.Services;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Text;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RequestManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        // Constructor injection
        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        // GET: api/<LoginController>

        #region Auth
       
        [HttpPost("CheckLogin")]  
        public IActionResult CheckLogin([FromBody] MstUser loginRequest)
        {
            try
            {
                var user = _loginService.CheckLogin(loginRequest.UserName, loginRequest.Password);

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return Unauthorized(new { message = "Invalid username or password." }); // Return 401 Unauthorized
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // Return 400 with error message
            }
        }

        
        #endregion
    }
}
