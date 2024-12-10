using System;
using System.Collections.Generic;

namespace RequestManagementAPI.Models
{
    public partial class VwUser
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int? OrganizationId { get; set; }
        public string? Organization { get; set; }
        public int? DivisionId { get; set; }
        public string? Division { get; set; }
        public int? RoleId { get; set; }
        public string? Role { get; set; }
        public int? ParentRoleId { get; set; }
    }
}
