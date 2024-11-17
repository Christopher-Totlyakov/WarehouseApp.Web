using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models.Interfaces;
using static WarehouseApp.Common.EntityValidationConstants.Users;


namespace WarehouseApp.Data.Models.Users
{
    public class Supplier : SenderMessageUser
    {
        [Required]
        [PersonalData]
        [MaxLength(CompanyNameMaxLength)]
        public string CompanyName { get; set; } = null!;
        
        [Required]
        [PersonalData]
        [MaxLength(AddressMaxLength)]
        public string factoryLocation { get; set; } = null!;

        [Required]
        [PersonalData]
        [MaxLength(PreferredDeliveryMethodMaxLength)]
        public string PreferredDeliveryMethod { get; set; } = null!;

    }
}
