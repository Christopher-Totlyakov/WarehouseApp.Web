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
    public class Distributor : RequesterAndBuyerUser
    {
        [Required]
        [PersonalData]
        public string CompanyName { get; set; } = null!;

        [Required]
        [PersonalData]
        public string TaxNumber { get; set; } = null!;


        [Required]
        [PersonalData]
        public string MOL { get; set; } = null!;

        [Required]
        [PersonalData]
        public string CompanyEmail { get; set;} = null!;


        [Required]
        [PersonalData]
        public string CompanyPhoneNumber { get; set; } = null!;

        [Required]
        [PersonalData]
        public string BusinessAddress { get; set; } = null!;

        [PersonalData]
        public DateTime? LicenseExpirationDate { get; set; }
        
        [Required]
        [PersonalData]
        public decimal DiscountRate { get; set; }



    }
}
