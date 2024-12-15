using System;
using System.Collections.Generic;

namespace RequestManagementAPI.Models
{
    public partial class VwRoleMenuPermission
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string? Menu { get; set; }
        public bool IsCreate { get; set; }
        public bool IsRead { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public int RoleId { get; set; }
        public string? Role { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
