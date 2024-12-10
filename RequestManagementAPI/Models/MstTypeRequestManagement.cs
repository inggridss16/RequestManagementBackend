using System;
using System.Collections.Generic;

namespace RequestManagementAPI.Models
{
    public partial class MstTypeRequestManagement
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = null!;
    }
}
