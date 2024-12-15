using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RequestManagementAPI.Models;
using RequestManagementAPI.Services;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Authorization;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RequestManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        // Constructor injection
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UserController>
        
        [HttpGet("GetVwUser")]
        public IActionResult GetVwUser(int userId, int page = 1, int pageSize = 10)
        {
            try
            {
                var userList = _userService.GetVwUser(userId, page, pageSize);
                int totalCount = _userService.GetVwUserTotalCountAll();
               
                var response = new
                {
                    Data = userList,
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                };

                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllMstOrganization")]
        public IActionResult GetAllMstOrganization()
        {
            try
            {
                List<MstOrganization> list = _userService.GetAllMstOrganization();
                return Ok(list);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllMstDivision")]
        public IActionResult GetAllMstDivision()
        {
            try
            {
                List<MstDivision> list = _userService.GetAllMstDivision();
                return Ok(list);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetMstRoleByDivisionId")]
        public IActionResult GetMstRoleByDivisionId(int divisionId)
        {
            try
            {
                List<MstRole> list = _userService.GetMstRoleByDivisionId(divisionId);
                return Ok(list);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("SaveNewUser")]
        public IActionResult SaveNewUser(MstUser user)
        {
            try
            {
                var data = _userService.CreateUser(user);

                return Ok(data);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //UPDATE
        [HttpGet("GetUserById/{id}")]
        public ActionResult<MstUser> GetUserById(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("SaveUpdateUser/{id}")]
        public IActionResult SaveUpdateUser(int id, [FromBody] MstUser updatedUser)
        {
            try
            {
                var updated = _userService.UpdateUser(id, updatedUser);
                return Ok(updated);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("SaveDeleteUser/{id}")]
        public IActionResult SaveDeleteUser(int id, [FromBody] MstUser deletedUser)
        {
            try
            {
                var deleted = _userService.DeleteUser(id, deletedUser);
                return Ok(deleted);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
