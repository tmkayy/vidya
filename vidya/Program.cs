using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using vidya.Data;
using vidya.Data.Models;
using vidya.Data.Repositories;
using vidya.Data.Seeder;
using vidya.Services.Data.ActivationKeys;
using vidya.Services.Data.Discounts;
using vidya.Services.Data.Games;
using vidya.Services.Data.Locations;
using vidya.Services.Data.SupportTickets;
using vidya.Services.Data.Users;
using vidya.Services.Mapping;
using vidya.ThirdParty.Services.Images;
using vidya.ThirdParty.Services.Payments;
using vidya.Web.DTOs.Games;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<VidyaDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(opt => opt.SignIn.RequireConfirmedEmail = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<VidyaDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IActivationKeyService, ActivationKeyService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ISupportTicketService, SupportTicketService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHostedService<DiscountBackgroundService>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<VidyaDbContext>();
    await new VidyaDbSeeder().SeedAsync(dbContext, scope.ServiceProvider);
}

AutoMapperConfig.RegisterMappings(typeof(GameDTO).Assembly);

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

app.UseAuthorization();

app.UseEndpoints(config =>
{
    config.MapControllerRoute
    (
        name: "areas",
        pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    config.MapDefaultControllerRoute();
    config.MapRazorPages();
});

app.Run();
