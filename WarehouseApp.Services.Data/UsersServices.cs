using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models.Users;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Services.Mapping;
using WarehouseApp.Web.ViewModels.Admin;

namespace WarehouseApp.Services.Data
{
    public class UsersServices : IUsersServices
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersServices(UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
        }

        public async Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync()
        {
            IEnumerable<ApplicationUser> allUsers = await this.userManager.Users.ToArrayAsync();

            ICollection<AllUsersViewModel> allUsersViewModel = new List<AllUsersViewModel>();

            foreach (var user in allUsers)
            {
                bool isPersonalDataDeleted = typeof(ApplicationUser)
                    .GetProperties()
                    .Where(p => Attribute.IsDefined(p, typeof(PersonalDataAttribute)))
                    .Any(p =>
                    {
                        var value = p.GetValue(user);
                        return (value is string strValue && strValue == "[Deleted]");
                    });

                allUsersViewModel.Add(new AllUsersViewModel()
                {
                    Id = user.Id.ToString(),
                    Email = user.Email,
                    UserType = user.GetType().Name,
                    IsPersonalDataDeleted = isPersonalDataDeleted,
                    IsActivate = user.IsActivate
                });
            }

            return allUsersViewModel;
        }
        public async Task<UserDetailsViewModel> GetUserByIdAsync(string userId)
        {
			Guid id;
			if (!Guid.TryParse(userId, out id))
			{
				return null!;
			}
			var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return null!;
            }

            var userType = user.GetType().Name;
            var details = new UserDetailsViewModel
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                UserType = userType
            };

            if (user is Customer customer)
            {
                details.AdditionalInfo["First Name"] = customer.FirstName;
                details.AdditionalInfo["Last Name"] = customer.LastName;
            }
            else if (user is Distributor distributor)
            {
                details.AdditionalInfo["Company Name"] = distributor.CompanyName;
                details.AdditionalInfo["Tax Number"] = distributor.TaxNumber;
                details.AdditionalInfo["MOL"] = distributor.MOL;
                details.AdditionalInfo["Company Email"] = distributor.CompanyEmail;
                details.AdditionalInfo["Phone Number"] = distributor.CompanyPhoneNumber;
                details.AdditionalInfo["Business Address"] = distributor.BusinessAddress;
                details.AdditionalInfo["License Expiration Date"] = distributor.LicenseExpirationDate?.ToString("yyyy-MM-dd") ?? "N/A";
                details.AdditionalInfo["Discount Rate"] = distributor.DiscountRate.ToString("F2");
            }
            else if (user is Supplier supplier)
            {
                details.AdditionalInfo["Company Name"] = supplier.CompanyName;
                details.AdditionalInfo["Factory Location"] = supplier.factoryLocation;
                details.AdditionalInfo["Preferred Delivery Method"] = supplier.PreferredDeliveryMethod;
            }
            else if (user is WarehouseWorker worker)
            {
                details.AdditionalInfo["First Name"] = worker.FirstName;
                details.AdditionalInfo["Last Name"] = worker.LastName;
                details.AdditionalInfo["Start Work"] = worker.StartWork.ToString("yyyy-MM-dd");
                details.AdditionalInfo["End Work"] = worker.EndWork?.ToString("yyyy-MM-dd") ?? "Still Working";
            }

            return details;
        }


        public async Task<bool> DeletePersonalDataAsync(string userId)
        {

			Guid id;
			if (!Guid.TryParse(userId, out id))
			{
				return false;
			}
			var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return false;
            }
            var userType = user.GetType();
            var personalDataProperties = userType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p =>
                    Attribute.IsDefined(p, typeof(PersonalDataAttribute)) &&
                    p.Name != nameof(ApplicationUser.Id) &&
                    p.Name != nameof(ApplicationUser.UserName) &&
                    p.Name != nameof(ApplicationUser.Email)
                );


            foreach (var property in personalDataProperties)
            {
                if (property.CanWrite)
                {
                    if (user is WarehouseWorker worker)
                    {
                        worker.EndWork = DateTime.UtcNow;
                    }
                    if (property.PropertyType == typeof(string))
                    {
                        property.SetValue(user, "[Deleted]");
                    }
                    else if (property.PropertyType.IsValueType)
                    {
                        property.SetValue(user, Activator.CreateInstance(property.PropertyType));
                    }
                    else
                    {
                        property.SetValue(user, null);
                    }
                }
            }

            var result = await this.userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task ChangeAccountAsync(string userId)
        {
			Guid id;
            if (!Guid.TryParse(userId, out id))
            {
                return;
            }
				var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return;
            }

            if (user.IsActivate == true)
            {
                user.IsActivate = false;
            }
            else
            {
                user.IsActivate = true;
            }
            await userManager.UpdateAsync(user);
        }

    }
}
