using System;
using System.Collections.Generic;

namespace RequestManagementAPI.Models
{
    public partial class VwExpensesRequestsManagement
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public string ReqNumber { get; set; } = null!;
        public decimal Amount { get; set; }
        public string TypeName { get; set; } = null!;
        public string Comment { get; set; } = null!;
        public bool Imported { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
