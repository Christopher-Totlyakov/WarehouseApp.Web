using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models.Users;
using WarehouseApp.Services.Mapping;

namespace WarehouseApp.Web.ViewModels.Admin
{
    public class AllUsersViewModel 
    {
        public string Id { get; set; } = null!;

        public string? Email { get; set; }

        public string UserType { get; set; } = null!;

        public bool IsPersonalDataDeleted { get; set; }

        public bool IsActivate { get; set; }

    }
}
