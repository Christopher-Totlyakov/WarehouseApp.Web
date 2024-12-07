using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data;
using WarehouseApp.Data.Models;
using WarehouseApp.Data.Models.Users;
using WarehouseApp.Data.Seeding;

namespace WarehouseApp.Web.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedProducts(this IApplicationBuilder app, string jsonPath)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateAsyncScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            Task.Run(async () =>
            {
                await DbSeeder.SeedProductsAsync(serviceProvider, jsonPath);
            })
                .GetAwaiter()
                .GetResult();

            return app;
        }

        public static IApplicationBuilder SeedCategoriesAsync(this IApplicationBuilder app, string jsonPath)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateAsyncScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            Task.Run(async () =>
            {
                await DbSeeder.SeedCategoriesAsync(serviceProvider, jsonPath);
            })
                .GetAwaiter()
                .GetResult();

            return app;
        }

        public static IApplicationBuilder SeedProductsWithCategoriesAsync(this IApplicationBuilder app, string jsonProductPath, string jsonCategoryPath)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateAsyncScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            Task.Run(async () =>
            {
                await DbSeeder.SeedProductsWithCategoriesAsync(serviceProvider, jsonProductPath, jsonCategoryPath);
            })
                .GetAwaiter()
                .GetResult();

            return app;
        }

        public static IApplicationBuilder SeedUsers(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateAsyncScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            UserManager<ApplicationUser> userManager = serviceProvider
                .GetRequiredService<UserManager<ApplicationUser>>();
            IUserStore<ApplicationUser> userStore = serviceProvider
                .GetRequiredService<IUserStore<ApplicationUser>>();

            Task.Run(async () =>
            {
                await SeedUserAsync(userManager, userStore, "customer1@test.com", "John", "q123456", "Customer", new Customer
                {
                    FirstName = "John",
                    LastName = "Doe"
                });

                await SeedUserAsync(userManager, userStore, "customer2@test.com", "Jane", "q123456", "Customer", new Customer
                {
                    FirstName = "Jane",
                    LastName = "Smith"
                });

                await SeedUserAsync(userManager, userStore, "distributor1@test.com", "DistribCorp", "q123456", "Distributor", new Distributor
                {
                    CompanyName = "DistribCorp",
                    TaxNumber = "123456789",
                    MOL = "Jane Doe",
                    CompanyEmail = "distributor@1.com",
                    CompanyPhoneNumber = "123-456-7890",
                    BusinessAddress = "123 Distribution Way",
                    LicenseExpirationDate = DateTime.UtcNow.AddYears(1),
                    DiscountRate = 10
                });

                await SeedUserAsync(userManager, userStore, "distributor2@test.com", "DistribLLC", "q123456", "Distributor", new Distributor
                {
                    CompanyName = "DistribLLC",
                    TaxNumber = "987654321",
                    MOL = "John Doe",
                    CompanyEmail = "distributor@2.com",
                    CompanyPhoneNumber = "098-765-4321",
                    BusinessAddress = "456 Supplier Rd",
                    LicenseExpirationDate = DateTime.UtcNow.AddYears(2),
                    DiscountRate = 15
                });

                await SeedUserAsync(userManager, userStore, "supplier1@test.com", "SupplyHouse", "q123456", "Supplier", new Supplier
                {
                    CompanyName = "SupplyHouse",
                    factoryLocation = "Factory A",
                    PreferredDeliveryMethod = "Truck"
                });

                await SeedUserAsync(userManager, userStore, "supplier2@test.com", "SupplierCo", "q123456", "Supplier", new Supplier
                {
                    CompanyName = "SupplierCo",
                    factoryLocation = "Factory B",
                    PreferredDeliveryMethod = "Air Freight"
                });

                await SeedUserAsync(userManager, userStore, "worker1@test.com", "Alice", "q123456", "WarehouseWorker", new WarehouseWorker
                {
                    FirstName = "Alice",
                    LastName = "Green",
                    StartWork = DateTime.UtcNow.AddMonths(-6)
                });

                await SeedUserAsync(userManager, userStore, "worker2@test.com", "Bob", "q123456", "WarehouseWorker", new WarehouseWorker
                {
                    FirstName = "Bob",
                    LastName = "Brown",
                    StartWork = DateTime.UtcNow.AddYears(-1),
                    EndWork = DateTime.UtcNow.AddMonths(-3)
                });
            }).GetAwaiter().GetResult();

            return app;
        }

        private static async Task SeedUserAsync(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            string email,
            string username,
            string password,
            string role,
            ApplicationUser user)
        {
            var existingUser = await userManager.FindByEmailAsync(email);
            if (existingUser != null) return;

            await userStore.SetUserNameAsync(user, username, CancellationToken.None);
            user.Email = email;

            IdentityResult result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error creating user {username}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            //bool roleExists = await userManager.IsInRoleAsync(user, role);
            //if (!roleExists)
            //{
            //    await userManager.AddToRoleAsync(user, role);
            //}
        }

        public static async Task<IApplicationBuilder> SeedMessagesAsync(this IApplicationBuilder app,IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<WarehouseDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!dbContext.Messages.Any())
            {
                var users = await userManager.Users.ToListAsync();
                var suppliers = users.Where(u => u is Supplier).ToList();
                var customers = users.Where(u => u is Customer).ToList();
                var distributors = users.Where(u => u is Distributor).ToList();
                var warehouseWorkers = users.Where(u => u is WarehouseWorker).ToList();

                var messages = new List<Message>
            {
                new Message
                {
                    SenderId = suppliers.First().Id,
                    ReceiverId = warehouseWorkers.FirstOrDefault()?.Id,
                    MessageType = "Order Update",
                    MessageContent = "Your recent order has been updated.",
                    SentDate = DateTime.UtcNow,
                    Status = "Sent",
                    SoftDelete = false
                },
                new Message
                {
                    SenderId = customers.First().Id,
                    ReceiverId = null, 
                    MessageType = "Feedback",
                    MessageContent = "I want to report an issue with the delivery.",
                    SentDate = DateTime.UtcNow,
                    Status = "Read",
                    SoftDelete = false
                },
                new Message
                {
                    SenderId = distributors.First().Id,
                    ReceiverId = warehouseWorkers.LastOrDefault()?.Id,
                    MessageType = "Inquiry",
                    MessageContent = "Do you have stock for the requested items?",
                    SentDate = DateTime.UtcNow,
                    Status = "Unread",
                    SoftDelete = false
                },
                new Message
                {
                    SenderId = customers.First().Id,
                    ReceiverId = warehouseWorkers.FirstOrDefault()?.Id,
                    MessageType = "Response",
                    MessageContent = "We have the requested stock available.",
                    SentDate = DateTime.UtcNow,
                    Status = "Sent",
                    SoftDelete = false
                }
            };

                await dbContext.Messages.AddRangeAsync(messages);

                await dbContext.SaveChangesAsync();
                return app;
            }
            return app;
        }

        public static async Task<IApplicationBuilder> SeedRequestsAsync(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WarehouseDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                if (dbContext.Requests.Any())
                {
                    return app; 
                }

                var products = await dbContext.Products.Take(10).ToListAsync();
                if (!products.Any())
                {
                    throw new Exception("No products found in the database. Seed products first.");
                }

                var requesters = await userManager.Users
                    .Where(u => u is RequesterAndBuyerUser)
                    .Cast<RequesterAndBuyerUser>()
                    .ToListAsync();

                if (!requesters.Any())
                {
                    throw new Exception("No requesters found in the database. Seed users first.");
                }

                var requests = new List<Request>();

                foreach (var requester in requesters)
                {
                    var request = new Request
                    {
                        RequesterId = requester.Id,
                        Status = "Pending",
                        RequestDate = DateTime.UtcNow,
                        ProcessedByWorkerId = null,
                        Note = null,
                        SoftDelete = false,
                        RequestProducts = products
                            .OrderBy(p => Guid.NewGuid()) 
                            .Take(3) 
                            .Select(p => new RequestProduct
                            {
                                ProductId = p.Id,
                                QuantityRequested = new Random().Next(1, 10), 
                                PriceUponRequest = p.Price
                            })
                            .ToList()
                    };

                    requests.Add(request);
                }

                dbContext.Requests.AddRange(requests);

                await dbContext.SaveChangesAsync();
            }

            return app;
        }

    }
}


