using System;
using System.Collections.Generic;

namespace RequestManagementAPI.Models
{
    public partial class MstRole
    {
        public int Id { get; set; }
        public string? Role { get; set; }
        public int? ParentRoleId { get; set; }
    }
}
