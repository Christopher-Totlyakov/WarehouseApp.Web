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
    public class Customer : RequesterAndBuyerUser
    {
        [Required]
        [PersonalData]
        public string FirstName { get; set; } = null!;

        [Required]
        [PersonalData]
        public string LastName { get; set; } = null!;

    }
}
