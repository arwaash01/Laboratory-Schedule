using Laboratory_Schedule.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Reflection;
using Laboratory_Schedule.Resource;



using Microsoft.Extensions.Options;
using Laboratory_Schedule;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<LocService>();
builder.Services.AddLocalization(opt => { opt.ResourcesPath = "Resource"; });


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddControllersWithViews().AddViewLocalization()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        {
            var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
            return factory.Create("a", assemblyName.Name);
        };
    });
builder.Services.AddRazorPages();
builder.Services.Configure<RequestLocalizationOptions>(
    options =>
    {
        var supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("en-US"),
            new CultureInfo("ar-SA")
        };
        options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;



        options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
    });
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

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
var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);
app.UseStaticFiles();

app.UseRouting();
app.UseCookiePolicy();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    //check
    var roles = new[] { "Admin", "Recep" };
    foreach (var role in roles)
    {
        var roleExist = await roleManager.RoleExistsAsync(role);
        if (!roleExist)
            await roleManager.CreateAsync(new IdentityRole(role));
    }


    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();



    var adminUser = await userManager.FindByEmailAsync("renad@gmail.com");
    if (adminUser == null)
    {
        var user = new IdentityUser
        {
            UserName = "renad@gmail.com",
            Email = "renad@gmail.com",
        };

        await userManager.CreateAsync(user, "A-123456a");
        await userManager.AddToRoleAsync(user, "Admin");
    }

    var recepUser = await userManager.FindByEmailAsync("omar@gmail.com");
    if (recepUser == null)
    {
        var user = new IdentityUser
        {
            UserName = "omar@gmail.com",
            Email = "omar@gmail.com",
        };
        await userManager.CreateAsync(user, "A-123456a");
        await userManager.AddToRoleAsync(user, "Recep");
    }
}
app.Run();
