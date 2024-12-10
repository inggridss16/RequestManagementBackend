using System;
using System.Collections.Generic;

namespace RequestManagementAPI.Models
{
    public partial class TrxRequestsManagement
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public string? Description { get; set; }
        public string Applicant { get; set; } = null!;
        public string Owner { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string SubCategory { get; set; } = null!;
        public int Status { get; set; }
        public decimal Expenses { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
