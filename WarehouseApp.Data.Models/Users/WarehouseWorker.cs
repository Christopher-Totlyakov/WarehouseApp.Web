using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WarehouseApp.Common.EntityValidationConstants.Users;

namespace WarehouseApp.Data.Models.Users
{
    public class WarehouseWorker : ApplicationUser
    {
        [Required]
        [PersonalData]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [PersonalData]
        [MaxLength(NameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        public DateTime StartWork { get; set; }

        
        public DateTime? EndWork { get; set; }


    }
}
