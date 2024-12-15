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
    public class PermissionController : ControllerBase
    {
        private readonly PermissionService _permissionService;

        // Constructor injection
        public PermissionController(PermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        // GET: api/<PermissionController>
        
        [HttpGet("GetVwRoleMenuPermission")]
        public IActionResult GetVwRoleMenuPermission(int userId, int page = 1, int pageSize = 10)
        {
            try
            {
                var roleMenuPermissionList = _permissionService.GetVwRoleMenuPermission(userId, page, pageSize);
                int totalCount = _permissionService.GetVwRoleMenuPermissionTotalCountAll();
               
                var response = new
                {
                    Data = roleMenuPermissionList,
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

        [HttpGet("GetMstRole")]
        public IActionResult GetMstRole()
        {
            try
            {
                List<MstRole> list = _permissionService.GetMstRole();
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

        [HttpGet("GetMstMenu")]
        public IActionResult GetMstMenu()
        {
            try
            {
                List<MstMenu> list = _permissionService.GetMstMenu();
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

        [HttpPost("SaveNewPermission")]
        public IActionResult SaveNewPermission(MstRoleMenuPermission permission)
        {
            try
            {
                var data = _permissionService.CreatePermission(permission);

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
        
        [HttpGet("GetPermissionById/{id}")]
        public ActionResult<MstRoleMenuPermission> GetPermissionById(int id)
        {
            try
            {
                var user = _permissionService.GetPermissionById(id);
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

        [HttpPut("SaveUpdatePermission/{id}")]
        public IActionResult SaveUpdatePermission(int id, [FromBody] MstRoleMenuPermission updatedPermission)
        {
            try
            {
                var data = _permissionService.UpdatePermission(id, updatedPermission);

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


    }
}
