using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using posSystem;
using posSystem.Middlewares;
using Microsoft.Extensions.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog for file logging
ExcelPackage.LicenseContext = LicenseContext.Commercial;
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 10485760, retainedFileCountLimit: 7)
    .CreateLogger();

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
});


builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null))
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>());
});

// Add session configuration
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".posSystem.Session";
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IExcelService, ExcelService>();

// Configure Serilog logging
builder.Logging.ClearProviders(); // Clear default providers
builder.Logging.AddSerilog(); // Add Serilog for logging

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseCookieMiddleware();
app.UseAuthentication(); // Add authentication middleware if using authentication
app.UseAuthorization();

// Configure error handling middleware (optional)
// app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Apply migrations and create database (uncomment if needed)
// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//     context.Database.Migrate(); // Applies any pending migrations and creates the database if it does not exist
// }

app.Run();
