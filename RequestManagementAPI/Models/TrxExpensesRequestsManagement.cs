using System;
using System.Collections.Generic;

namespace RequestManagementAPI.Models
{
    public partial class TrxExpensesRequestsManagement
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public decimal Amount { get; set; }
        public string? Type { get; set; }
        public string Comment { get; set; } = null!;
        public bool Imported { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
