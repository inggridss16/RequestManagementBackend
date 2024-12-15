using System;
using System.Collections.Generic;

namespace RequestManagementAPI.Models
{
    public partial class VwUser
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int OrganizationId { get; set; }
        public string? Organization { get; set; }
        public int DivisionId { get; set; }
        public string? Division { get; set; }
        public int RoleId { get; set; }
        public string? Role { get; set; }
        public int? ParentRoleId { get; set; }
        public int CreatedBy { get; set; }
        public string? CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string? UpdatedByUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
