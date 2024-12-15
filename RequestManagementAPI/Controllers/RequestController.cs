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
    public class RequestController : ControllerBase
    {
        private readonly RequestService _requestService;

        // Constructor injection
        public RequestController(RequestService requestService)
        {
            _requestService = requestService;
        }

        // GET: api/<RequestController>


        [HttpGet("GetMstUserById/{id}")]
        public ActionResult<MstUser> GetMstUserById(int id)
        {
            try
            {
                var user = _requestService.GetMstUserById(id);
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

        [HttpGet("GetAllMstUser")]
        public IActionResult GetAllMstUser()
        {
            List<MstUser> list = _requestService.GetAllMstUser(); // Call the Get All function
            return Ok(list); // Or return a JSON result if it's an API controller
        }
        [HttpGet ("GetAllMstStatusRequestManagement")]
        public IActionResult GetAllMstStatusRequestManagement()
        {
            List<MstStatusRequestManagement> list = _requestService.GetAllMstStatusRequestManagement(); // Call the Get All function
            return Ok(list); // Or return a JSON result if it's an API controller
        }
        
        [HttpGet ("GetAllMstCategoryRequestManagement")]
        public IActionResult GetAllMstCategoryRequestManagement()
        {
            List<MstCategoryRequestManagement> list = _requestService.GetAllMstCategoryRequestManagement(); 
            return Ok(list); 
        }
        
        [HttpGet ("GetAllMstSubCategoryRequestManagement")]
        public IActionResult GetAllMstSubCategoryRequestManagement()
        {
            List<MstSubCategoryRequestManagement> list = _requestService.GetAllMstSubCategoryRequestManagement();
            return Ok(list); 
        }

        [HttpGet ("GetTrxRequests")]
        public IActionResult GetTrxRequests(int page = 1, int pageSize = 10)
        {
            try
            {
                var requests = _requestService.GetTrxRequestsManagement(page, pageSize);
                var totalCount = _requestService.GetTrxRequestsManagementTotalCount(); // For pagination info

                // Create a response model with paging information if needed
                var response = new
                {
                    Data = requests,
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

        [HttpGet("GetVwRequestsManagement")]
        public IActionResult GetVwRequestsManagement(int createdBy, int divisionId, int roleId, int page = 1, int pageSize = 10)
        {
            try
            {
                var requests = _requestService.GetVwRequestsManagement(createdBy, divisionId, roleId, page, pageSize);
                int totalCount = _requestService.GetVwRequestsManagementCount(createdBy, divisionId, roleId);
               
                var response = new
                {
                    Data = requests,
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

        [HttpPost ("SaveNewRequest")]
        public IActionResult SaveNewRequest(TrxRequestsManagement request)
        {
            try
            {
                var data = _requestService.CreateTrxRequestsManagement(request);

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


        #region UPDATE

        [HttpGet("GetTrxRequestsManagementById/{id}")]
        public ActionResult<TrxRequestsManagement> GetVwRequestsManagementById(int id)
        {
            try
            {
                var request = _requestService.GetVwRequestsManagementById(id);
                return Ok(request);
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

        [HttpPut("SaveUpdateRequest/{id}")]
        public IActionResult SaveUpdateRequest(int id, [FromBody] TrxRequestsManagement updatedRequest)
        {
            try
            {
                var updated = _requestService.UpdateTrxRequestsManagement(id, updatedRequest);
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

        [HttpPut("SaveDeleteRequest/{id}")]
        public IActionResult SaveDeleteRequest(int id, [FromBody] TrxRequestsManagement deletedRequest)
        {
            try
            {
                var deleted = _requestService.DeleteTrxRequestsManagement(id, deletedRequest);
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

        #endregion


        #region Expenses
        [HttpGet("GetVwExpensesRequestsManagement")]
        public IActionResult GetVwExpensesRequestsManagement(int createdBy, int idRequest, int page = 1, int pageSize = 10)
        {
            try
            {
                var expenses = _requestService.GetVwExpensesRequestsManagement(createdBy, idRequest, page, pageSize);
                var totalCount = _requestService.GetVwExpensesRequestsManagementTotalCount(idRequest); // For pagination info

                // Create a response model with paging information if needed
                var response = new
                {
                    Data = expenses,
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

        [HttpGet("GetAllMstTypeRequestManagement")]
        public IActionResult GetAllMstTypeRequestManagement()
        {
            List<MstTypeRequestManagement> list = _requestService.GetAllMstTypeRequestManagement();
            return Ok(list);
        }

        [HttpPost("SaveNewExpense")]
        public IActionResult SaveNewExpense(TrxExpensesRequestsManagement expense)
        {
            try
            {
                var data = _requestService.CreateTrxExpensesRequestsManagement(expense);

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
        
        [HttpGet("GetTrxExpensesRequestsManagementById/{id}")]
        public ActionResult<TrxExpensesRequestsManagement> GetTrxExpensesRequestsManagementById(int id)
        {
            try
            {
                var request = _requestService.GetTrxExpensesRequestsManagementById(id);
                return Ok(request);
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
        
        [HttpPut("SaveUpdateExpense/{id}")]
        public IActionResult SaveUpdateExpense(int id, [FromBody] TrxExpensesRequestsManagement updatedExpense)
        {
            try
            {
                var updated = _requestService.UpdateTrxExpensesRequestsManagement(id, updatedExpense);
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
        
        [HttpPut("SaveDeleteExpense/{id}")]
        public IActionResult SaveDeleteExpense(int id, [FromBody] TrxExpensesRequestsManagement deletedExpense)
        {
            try
            {
                var deleted = _requestService.DeleteTrxExpensesRequestsManagement(id, deletedExpense);
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

        #endregion


        [HttpGet("ExportRequestsToExcel")]
        public IActionResult ExportRequestsToExcel()
        {
            try
            {
                // 1. Get data (for Non-division Managers, roleId == 1)
                // harcode user ID = 1 (alex)
                var requests = _requestService.GetVwRequestsManagement(1, 0, 1, 1, int.MaxValue); // Get all data for export

                // 2. Create Excel workbook and sheet
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("Requests");

                // 3. Create header row
                IRow headerRow = sheet.CreateRow(0);
                string[] headers = { "Number", "Description", "Applicant", "Owner", "Category", "SubCategory", "Status", "Expenses", "Created By", "Created Date" };
                for (int i = 0; i < headers.Length; i++)
                {
                    headerRow.CreateCell(i).SetCellValue(headers[i]);
                }

                // Create date format and style (only once, outside the loop)
                IDataFormat dataFormat = workbook.CreateDataFormat();
                ICellStyle dateStyle = workbook.CreateCellStyle();
                dateStyle.DataFormat = dataFormat.GetFormat("dd MMM yyyy"); // Your desired date format

                // 4. Populate data rows
                int rowNum = 1;
                foreach (var request in requests)
                {
                    IRow row = sheet.CreateRow(rowNum++);
                    row.CreateCell(0).SetCellValue(request.Number);
                    row.CreateCell(1).SetCellValue(request.Description);
                    row.CreateCell(2).SetCellValue(request.UserName);
                    row.CreateCell(3).SetCellValue(request.Owner);
                    row.CreateCell(4).SetCellValue(request.CategoryName);
                    row.CreateCell(5).SetCellValue(request.SubCategoryName);
                    row.CreateCell(6).SetCellValue(request.StatusName);
                    row.CreateCell(7).SetCellValue((double)request.Expenses); // Cast to double if needed
                    row.CreateCell(8).SetCellValue(request.CreatedByUser);

                    // Set CreatedDate value and apply the date style
                    row.CreateCell(9).SetCellValue((DateTime)request.CreatedDate);
                    row.GetCell(9).CellStyle = dateStyle;
                }

                // 5. Auto-size columns (optional)
                for (int i = 0; i < headers.Length; i++)
                {
                    sheet.AutoSizeColumn(i);
                }

                // 6. Save workbook to memory stream
                MemoryStream stream = new MemoryStream();
                workbook.Write(stream);

                // 7. Return the Excel file as a FileResult
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Requests.xlsx");
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
