using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Web.ViewModels.Admin
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; } = null!;
        public string? Email { get; set; }
        public string UserType { get; set; } = null!;
        public Dictionary<string, string> AdditionalInfo { get; set; } = new(); 

    }
}
