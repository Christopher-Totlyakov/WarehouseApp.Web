// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using WarehouseApp.Common;
using WarehouseApp.Data.Models.Users;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static WarehouseApp.Common.EntityValidationConstants.Users;

namespace WarehouseApp.Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        //private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        //private readonly IEmailSender _emailSender;
        private readonly IPasswordValidator<ApplicationUser> _passwordValidator;

        public RegisterModel(
            IPasswordValidator<ApplicationUser> passwordValidator,

            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _userStore = userStore;
            //_emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            //_emailSender = emailSender;
            _passwordValidator = passwordValidator;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required]
            [StringLength(NameMaxLength,
         MinimumLength = NameMinLength,
         ErrorMessage = "The username must be between {2} and {1} characters.")]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [StringLength(EmailMaxLength,
                MinimumLength = EmailMinLength,
                ErrorMessage = "The email must be between {2} and {1} characters.")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Passwords do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string UserType { get; set; }

            // Полета за Customer
            [StringLength(NameMaxLength,
                MinimumLength = NameMinLength,
                ErrorMessage = "The first name must be between {2} and {1} characters.")]
            public string FirstName { get; set; }

            [StringLength(NameMaxLength,
                MinimumLength = NameMinLength,
                ErrorMessage = "The last name must be between {2} and {1} characters.")]
            public string LastName { get; set; }

            // Полета за Distributor
            [StringLength(CompanyNameMaxLength,
                MinimumLength = CompanyNameMinLength,
                ErrorMessage = "The company name must be between {2} and {1} characters.")]
            public string CompanyName { get; set; }

            
            [StringLength(TaxNumberMaxLength,
                MinimumLength = TaxNumberMinLength,
                ErrorMessage = "The tax number must be between {2} and {1} characters.")]
            public string TaxNumber { get; set; }

            [StringLength(MOLMaxLength,
                MinimumLength = MOLMinLength,
                ErrorMessage = "The MOL must be between {2} and {1} characters.")]
            public string MOL { get; set; }

            [EmailAddress]
            [StringLength(EmailMaxLength,
                MinimumLength = EmailMinLength,
                ErrorMessage = "The company email must be between {2} and {1} characters.")]
            public string CompanyEmail { get; set; }

            [StringLength(PhoneMaxLength,
                MinimumLength = PhoneMinLength,
                ErrorMessage = "The phone number must be between {2} and {1} characters.")]
            public string CompanyPhoneNumber { get; set; }

            [StringLength(AddressMaxLength,
                MinimumLength = AddressMinLength,
                ErrorMessage = "The address must be between {2} and {1} characters.")]
            public string BusinessAddress { get; set; }

            public DateTime? LicenseExpirationDate { get; set; }

            [Range(DiscountRateMin,
                DiscountRateMax,
                ErrorMessage = "The discount rate must be between {1} and {2}.")]
            public decimal DiscountRate { get; set; }

            [StringLength(AddressMaxLength,
                MinimumLength = AddressMinLength,
                ErrorMessage = "The factory location must be between {2} and {1} characters.")]
            public string FactoryLocation { get; set; }

            [StringLength(PreferredDeliveryMethodMaxLength,
                MinimumLength = PreferredDeliveryMethodMinLength,
                ErrorMessage = "The preferred delivery method must be between {2} and {1} characters.")]
            public string PreferredDeliveryMethod { get; set; }

            
            public DateTime StartWork { get; set; }

            public DateTime? EndWork { get; set; }

        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
           
            ApplicationUser user = null;
            if (Input.Password != null)
            {
                var passwordValidationResult = await _passwordValidator.ValidateAsync(_userManager, user, Input.Password);

                if (!passwordValidationResult.Succeeded)
                {
                    foreach (var error in passwordValidationResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            else 
            {
                ModelState.AddModelError(string.Empty, "Password is missing");
            }
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

			var userType = Input.UserType;

			switch (userType)
                {
                    case "Customer":
                    if (Input.UserName == null || Input.Email == null 
                        || Input.FirstName == null || Input.LastName == null)
					{
						return Page();
					}
                        user = new Customer
                        {
                            UserName = Input.UserName,
                            Email = Input.Email,
                            FirstName = Input.FirstName,
                            LastName = Input.LastName
                        };
                        break;

                    case "Distributor":
					if (Input.UserName == null || Input.Email == null
					   || Input.CompanyName == null || Input.TaxNumber == null
					   || Input.MOL == null || Input.CompanyEmail == null
					   || Input.CompanyPhoneNumber == null || Input.BusinessAddress == null
					   || Input.LicenseExpirationDate == null || Input.DiscountRate == null)
					{
						return Page();
					}
					user = new Distributor
                        {
                            UserName = Input.UserName,
                            Email = Input.Email,
                            CompanyName = Input.CompanyName,
                            TaxNumber = Input.TaxNumber,
                            MOL = Input.MOL,
                            CompanyEmail = Input.CompanyEmail,
                            CompanyPhoneNumber = Input.CompanyPhoneNumber,
                            BusinessAddress = Input.BusinessAddress,
                            LicenseExpirationDate = Input.LicenseExpirationDate,
                            DiscountRate = Input.DiscountRate
                        };
                        break;

                    case "Supplier":
					if (Input.UserName == null || Input.Email == null
					   || Input.CompanyName == null || Input.FactoryLocation == null
					   || Input.PreferredDeliveryMethod == null)
					{
						return Page();
					}
					user = new Supplier
                        {
                            UserName = Input.UserName,
                            Email = Input.Email,
                            CompanyName = Input.CompanyName,
                            factoryLocation = Input.FactoryLocation,
                            PreferredDeliveryMethod = Input.PreferredDeliveryMethod
                        };
                        break;

                    case "WarehouseWorker":
					if (Input.UserName == null || Input.Email == null
					   || Input.StartWork == null || Input.FirstName == null
					   || Input.LastName == null)
					{
						return Page();
					}
					user = new WarehouseWorker
                        {
                            UserName = Input.UserName,
                            Email = Input.Email,
                            StartWork = Input.StartWork,
                            EndWork = Input.EndWork,
                            FirstName = Input.FirstName,
                            LastName = Input.LastName,
                            
                        };
                        break;

                    default:
                        ModelState.AddModelError(string.Empty, "Invalid user type.");
                        return Page();
                }

               
            await _userStore.SetUserNameAsync(user, Input.UserName, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }


            return Page();
        }



        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
