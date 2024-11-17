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
    public class Distributor : RequesterAndBuyerUser
    {
        [Required]
        [PersonalData]
        [MaxLength(CompanyNameMaxLength)]
        public string CompanyName { get; set; } = null!;

        [Required]
        [PersonalData]
        [MaxLength(TaxNumberMaxLength)]
        public string TaxNumber { get; set; } = null!;


        [Required]
        [PersonalData]
        [MaxLength(MOLMaxLength)]
        public string MOL { get; set; } = null!;

        [Required]
        [PersonalData]
        [MaxLength(EmailMaxLength)]
        public string CompanyEmail { get; set;} = null!;


        [Required]
        [PersonalData]
        [MaxLength(PhoneMaxLength)]
        public string CompanyPhoneNumber { get; set; } = null!;

        [Required]
        [PersonalData]
        [MaxLength(AddressMaxLength)]
        public string BusinessAddress { get; set; } = null!;

        [PersonalData]
        public DateTime? LicenseExpirationDate { get; set; }
        
        [Required]
        [PersonalData]
        [Range(DiscountRateMin,DiscountRateMax)]
        public decimal DiscountRate { get; set; }



    }
}
