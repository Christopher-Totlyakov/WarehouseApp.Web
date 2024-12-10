using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using NuGet.Protocol.Core.Types;
using WarehouseApp.Data;
using WarehouseApp.Data.Models.Users;
using WarehouseApp.Services.Mapping;
using WarehouseApp.Web.ViewModels;
using WarehouseApp.Data.Repository.Interfaces;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Services.Data;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using WarehouseApp.Web.Infrastructure;
using WarehouseApp.Data.Seeding.DTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SQLServer");

builder.Services.AddDbContext<WarehouseDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
    {
		var s = options.Password.RequireDigit = builder.Configuration.GetValue<bool>("Identity:Password:RequireDigits");
		options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
		options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumerical");
		options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
		options.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
		options.Password.RequiredUniqueChars = builder.Configuration.GetValue<int>("Identity:Password:RequiredUniqueCharacters");

		options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
		options.SignIn.RequireConfirmedEmail = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");
		options.SignIn.RequireConfirmedPhoneNumber = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedPhoneNumber");

		options.User.RequireUniqueEmail = builder.Configuration.GetValue<bool>("Identity:User:RequireUniqueEmail");

	})
    .AddEntityFrameworkStores<WarehouseDbContext>()
    .AddRoles<IdentityRole<Guid>>()
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddDefaultTokenProviders();

builder.Services.AddDistributedMemoryCache(); 

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true; 
    options.Cookie.IsEssential = true; 
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login"; 
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.AccessDeniedPath = "/Error/403";
});

builder.Services.AddScoped<WarehouseDbContext>();
builder.Services.AddScoped<IRepository, WarehouseApp.Data.Repository.Repository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IMessageServices, MessageService>();
builder.Services.AddScoped<IUsersServices, UsersServices>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<ISalesService, SalesService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();


builder.Services.AddControllersWithViews(cfg => 
{
    cfg.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
}).AddViewOptions(options =>
	{
		options.HtmlHelperOptions.ClientValidationEnabled = true;
	});

builder.Services.AddRazorPages();

var app = builder.Build();

AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).Assembly, typeof(ImportProductDTO).Assembly );

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseStatusCodePagesWithReExecute("/Error/{0}");
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/Identity/Account/AccessDenied"))
    {
        context.Response.StatusCode = 403; 
        context.Request.Path = "/Error/403";
    }
    await next();
});

app.Map("/Error/{statusCode}", async context =>
{
    var statusCode = context.GetRouteValue("statusCode")?.ToString();

    var factory = context.RequestServices.GetRequiredService<IRazorViewEngine>();
    var tempDataProvider = context.RequestServices.GetRequiredService<ITempDataProvider>();
    var httpContextAccessor = context.RequestServices.GetRequiredService<IHttpContextAccessor>();
    var actionContext = new ActionContext(context, new RouteData(), new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());

    if (statusCode == "404" || statusCode == "403")
    {
        var viewName = statusCode == "404" ? "404" : "403";
        var result = factory.FindView(actionContext, viewName, false);

        if (result.View != null)
        {
            await using var writer = new StringWriter();
            var viewContext = new ViewContext(
                actionContext,
                result.View,
                new ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(), new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary())
                {
                    { "StatusCode", statusCode }
                },
                new TempDataDictionary(httpContextAccessor.HttpContext, tempDataProvider),
                writer,
                new HtmlHelperOptions()
            );

            await result.View.RenderAsync(viewContext);
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync(writer.ToString());
        }
        else
        {
            await context.Response.WriteAsync($"{statusCode} - Page Not Found");
        }
    }
    else
    {
        await context.Response.WriteAsync($"An error occurred. Status Code: {statusCode}");
    }
});




app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

string jsonProductsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, builder.Configuration.GetValue<string>("Seed:ProductsJson")!);
string jsonCategoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, builder.Configuration.GetValue<string>("Seed:CategoryJson")!);

if (app.Environment.IsDevelopment())
{
    app.SeedProducts(jsonProductsPath);
    app.SeedCategoriesAsync(jsonCategoryPath);
    app.SeedProductsWithCategoriesAsync(jsonProductsPath, jsonCategoryPath);
    app.SeedUsers();

    var scope = app.Services.CreateScope();
    await app.SeedMessagesAsync(scope.ServiceProvider);
    await app.SeedRequestsAsync(scope.ServiceProvider);
}

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
