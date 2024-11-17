using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models.Interfaces;

namespace WarehouseApp.Data.Models.Users
{
    public class Supplier : SenderMessageUser
    {
        [Required]
        [PersonalData]
        public string CompanyName { get; set; } = null!;
        
        [Required]
        [PersonalData]
        public string factoryLocation { get; set; } = null!;

        [Required]
        [PersonalData]
        public string PreferredDeliveryMethod { get; set; } = null!;

    }
}
