using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using NordicDoor_Group15.Models;
using NordicDoor_Group15.Areas.Identity.Data;
using Constants = NordicDoor_Group15.Core.Constants;
using NordicDoor_Group15.Core.Repositories;
using NordicDoor_Group15.Repositories;
using Microsoft.Extensions.DependencyInjection.Extensions;

public class Program
{
    static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();


        var connection = Environment.GetEnvironmentVariable("CONNECTION");
        

        var secureconnectionString = string.IsNullOrEmpty(connection) ? builder.Configuration.GetConnectionString("ApplicationIdentityDbContextConnection") : connection;

        builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
    options.UseMySql(secureconnectionString, new MySqlServerVersion(new Version(10, 9, 12))));

        //To give access to IHttpContextAccessor for Audit Data with IAuditable
        //builder.Services.AddHttpContextAccessor();
        builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

        #region Authorization
        AddAuthorizationPolicies(ref builder);
        #endregion

        AddScoped(ref builder);

        var app = builder.Build();

        //This code below initalize seeding data for database at first time! It should be added top of the page "using NordicDoor_Group15.Models;"
        //using (var scope = app.Services.CreateScope())
        //{
        //    var services = scope.ServiceProvider;

        //    SeedData.Initialize(app);
        //}
        SeedData.Initialize(app);



        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication(); ;

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        app.Run();

    }

    static void AddAuthorizationPolicies(ref WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber"));
        });

        builder.Services.AddAuthorization(options =>
        {
            //options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Administrator"));
            //options.AddPolicy("RequireTeamManager", policy => policy.RequireRole("TeamManager"));

            options.AddPolicy(Constants.Policies.RequireAdmin, policy => policy.RequireRole(Constants.Roles.Administrator));
            options.AddPolicy(Constants.Policies.RequireTeamManager, policy => policy.RequireRole(Constants.Roles.TeamManager));
            options.AddPolicy(Constants.Policies.RequireUser, policy => policy.RequireRole(Constants.Roles.User));
        });
    }

    static void AddScoped(ref WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}