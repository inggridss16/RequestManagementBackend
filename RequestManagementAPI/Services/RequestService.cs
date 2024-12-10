using Microsoft.EntityFrameworkCore;
using RequestManagementAPI.Models;
using System.Reflection.Metadata;


namespace RequestManagementAPI.Services
{
    public class RequestService
    {
        private readonly DbContextAssesment _context;

        public RequestService(DbContextAssesment context)
        {
            _context = context;
        }


        public MstUser GetMstUserById(int id)
        {
            var user = _context.MstUsers.Find(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            return user;
        }

        #region Request
        // Read (Get All)
        public List<MstUser> GetAllMstUser()
        {
            return _context.MstUsers.ToList();
        }
        public List<MstStatusRequestManagement> GetAllMstStatusRequestManagement()
        {
            return _context.MstStatusRequestManagements.ToList();
        }
        public List<MstCategoryRequestManagement> GetAllMstCategoryRequestManagement()
        {
            return _context.MstCategoryRequestManagements.ToList();
        }
        public List<MstSubCategoryRequestManagement> GetAllMstSubCategoryRequestManagement()
        {
            return _context.MstSubCategoryRequestManagements.ToList();
        }
        public List<TrxRequestsManagement> GetTrxRequestsManagement(int page = 1, int pageSize = 10)
        {
            return _context.TrxRequestsManagements
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public List<VwRequestsManagement> GetVwRequestsManagement(int? createdBy, int divisionId, int roleId, int page = 1, int pageSize = 10)
        {
            if (roleId == 1)
            {
                // Non-division User: Return all requests
                return _context.VwRequestsManagements
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                // Existing logic for other users
                return _context.VwRequestsManagements
                    .Where(r => r.CreatedBy == createdBy || r.ParentRoleIdupdatedBy == roleId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }

        public int GetVwRequestsManagementCount(int? createdBy, int divisionId, int roleId)
        {
            if (roleId == 1)
            {
                // Non-division User: Return all requests
                return _context.VwRequestsManagements
                    .Count();
            }
            else
            {
                // Existing logic for other users
                return _context.VwRequestsManagements
                    .Where(r => r.CreatedBy == createdBy || r.ParentRoleIdupdatedBy == roleId)
                    .Count();
            }

        }

        public VwRequestsManagement GetVwRequestsManagementById(int id)
        {
            var request = _context.VwRequestsManagements.FirstOrDefault(r => r.Id == id);
            if (request == null)
            {
                throw new KeyNotFoundException($"Request with ID {id} not found.");
            }
            return request;
        }

        // CREATE
        public TrxRequestsManagement CreateTrxRequestsManagement(TrxRequestsManagement request)
        {
            request.CreatedDate = DateTime.Now;
            _context.TrxRequestsManagements.Add(request);
            _context.SaveChanges();
            request.Number = "REQ-" + request.Id.ToString("D3");
            _context.SaveChanges();
            return request;
        }
        // UPDATE
        public TrxRequestsManagement UpdateTrxRequestsManagement(int id, TrxRequestsManagement updatedRequest)
        {
            var existingRequest = _context.TrxRequestsManagements.Find(id);
            if (existingRequest == null)
            {
                throw new KeyNotFoundException($"Request with ID {id} not found.");
            }

            existingRequest.UpdatedBy = updatedRequest.UpdatedBy;
            existingRequest.UpdatedDate = DateTime.Now;
            existingRequest.Description = updatedRequest.Description;
            existingRequest.Category = updatedRequest.Category;
            existingRequest.SubCategory = updatedRequest.SubCategory;
            existingRequest.Status = updatedRequest.Status;
            existingRequest.Expenses = updatedRequest.Expenses;

            _context.SaveChanges();
            return existingRequest;
        }
        // DELETE is 1
        public TrxRequestsManagement DeleteTrxRequestsManagement(int id, TrxRequestsManagement deletedRequest)
        {
            var existingRequest = _context.TrxRequestsManagements.Find(id);
            if (existingRequest == null)
            {
                throw new KeyNotFoundException($"Request with ID {id} not found.");
            }

            existingRequest.UpdatedBy = deletedRequest.UpdatedBy;
            existingRequest.UpdatedDate = DateTime.Now;
            existingRequest.IsDeleted = true;

            // delete expenses 
            var existingExpenses = _context.TrxExpensesRequestsManagements
                .Where(e => e.RequestId == id && e.IsDeleted == false) // Filter hanya yang IsDeleted == false
                .ToList();
            foreach (var expense in existingExpenses)
            {
                expense.IsDeleted = true;
                expense.UpdatedBy = deletedRequest.UpdatedBy;
                expense.UpdatedDate = DateTime.Now;
            }


            _context.SaveChanges();
            return existingRequest;
        }

        /*public bool DeleteTrxRequestsManagement(int id)
        {
            var request = _context.TrxRequestsManagements.Find(id);
            if (request == null)
            {
                return false;
            }

            _context.TrxRequestsManagements.Remove(request);
            _context.SaveChanges();
            return true;
        }*/

        #endregion


        #region Expenses

        public List<VwExpensesRequestsManagement> GetVwExpensesRequestsManagement(int idRequest, int page = 1, int pageSize = 10)
        {

            return _context.VwExpensesRequestsManagements
                .Where(r => r.RequestId == idRequest)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
        public List<MstTypeRequestManagement> GetAllMstTypeRequestManagement()
        {
            return _context.MstTypeRequestManagements.ToList();
        }
        

        public TrxExpensesRequestsManagement GetTrxExpensesRequestsManagementById(int id)
        {
            var expense = _context.TrxExpensesRequestsManagements.Find(id);
            if (expense == null)
            {
                throw new KeyNotFoundException($"expense with ID {id} not found.");
            }
            /*else
            {
                var request = _context.TrxExpensesRequestsManagements.Find(expense.RequestId);
            }*/
            return expense;
        }
        // CREATE
        public TrxExpensesRequestsManagement CreateTrxExpensesRequestsManagement(TrxExpensesRequestsManagement expense)
        {
            expense.CreatedDate = DateTime.Now; 
            _context.TrxExpensesRequestsManagements.Add(expense);
            _context.SaveChanges();
            UpdateTotalExpense(expense.RequestId); // Update total after adding
            
            return expense;
        }
        // UPDATE
        public TrxExpensesRequestsManagement UpdateTrxExpensesRequestsManagement(int id, TrxExpensesRequestsManagement updatedExpense)
        {
            var existingExpense = _context.TrxExpensesRequestsManagements.Find(id);
            if (existingExpense == null)
            {
                throw new KeyNotFoundException($"Expense with ID {id} not found.");
            }

            existingExpense.UpdatedBy = updatedExpense.UpdatedBy;
            existingExpense.UpdatedDate = DateTime.Now;
            existingExpense.Amount = updatedExpense.Amount;
            existingExpense.Comment = updatedExpense.Comment;
            existingExpense.Imported = updatedExpense.Imported;
            _context.SaveChanges();
            UpdateTotalExpense(existingExpense.RequestId); // Update total after updating
            
            return existingExpense;
        }
        // DELETE is 1
        public TrxExpensesRequestsManagement DeleteTrxExpensesRequestsManagement(int id, TrxExpensesRequestsManagement deletedExpense)
        {
            var existingExpense = _context.TrxExpensesRequestsManagements.Find(id);
            if (existingExpense == null)
            {
                throw new KeyNotFoundException($"Expense with ID {id} not found.");
            }

            existingExpense.UpdatedBy = deletedExpense.UpdatedBy;
            existingExpense.UpdatedDate = DateTime.Now;
            existingExpense.IsDeleted = true;
            _context.SaveChanges();
            UpdateTotalExpense(existingExpense.RequestId); // Update total after deleting
            
            return existingExpense;
        }
        #endregion


        #region Helper
        //Helper function to get total count for paging 
        public int GetVwRequestsManagementTotalCountAll()
        {
            return _context.VwRequestsManagements.Count();
        }
        public int GetTrxRequestsManagementTotalCount()
        {
            return _context.TrxRequestsManagements.Count();
        }
        public int GetVwRequestsManagementTotalCountByDivision(int divisionId)
        {
            return _context.VwRequestsManagements
                .Where(r => r.DivisionId == divisionId)
                .Count();
        }
        public int GetVwRequestsManagementTotalCount(int createdBy)
        {
            return _context.VwRequestsManagements
                .Where(r => r.CreatedBy == createdBy)
                .Count();
        }

        public int GetVwExpensesRequestsManagementTotalCount(int idRequest)
        {
            return _context.VwExpensesRequestsManagements
                .Where(r => r.RequestId == idRequest)
                .Count();
        }
        private void UpdateTotalExpense(int requestId)
        {
            var totalExpense = _context.TrxExpensesRequestsManagements
                                  .Where(e => e.RequestId == requestId && !e.IsDeleted)
                                  .Sum(e => e.Amount);

            var request = _context.TrxRequestsManagements.Find(requestId);
            if (request != null)
            {
                request.Expenses = totalExpense;
                _context.SaveChanges();
            }
        }
        #endregion


    }
}
