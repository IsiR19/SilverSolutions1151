using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Rotativa.AspNetCore;
using SilverSolutions1151.Data;
using SilverSolutions1151.Middleware.Services;
using SilverSolutions1151.Middleware.Services.Interfaces;
using SilverSolutions1151.Repository;
using SilverSolutions1151.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddTransient<IManufactureRepository,ManufactureRepository>();
builder.Services.AddTransient<IRawTobaccoService, TobaccoService>();
builder.Services.AddTransient<ITobaccoMixingService, TobaccoMixingService>();
builder.Services.AddTransient<IReadyStockService, ReadyStockService>();    
builder.Services.AddTransient<ISoldStockService,SoldStockService>();
builder.Services.AddTransient<ISalesRepository,SalesRepository>();
builder.Services.AddTransient<ISalesService,SalesService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*", "http://localhost:53360/");
                      });
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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.UseCors(MyAllowSpecificOrigins);


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

app.Run();

