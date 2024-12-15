using System;
using System.Collections.Generic;

namespace RequestManagementAPI.Models
{
    public partial class MstMenu
    {
        public int Id { get; set; }
        public string? Menu { get; set; }
        public bool IsActive { get; set; }
    }
}
