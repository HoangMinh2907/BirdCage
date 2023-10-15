
using BusinessObject.Models;
using DataAccess.CarInformationsRepo;
using DataAccess.CustomersRepo;
using DataAccess.ManufacturersRepo;
using DataAccess.RentingDetailsRepo;
using DataAccess.RentingTransactionsRepo;
using DataAccess.SupplierRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICarInforRepository, CarInforRepository>();
builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
builder.Services.AddScoped<ISupplyRepository, SupplyRepository>();
builder.Services.AddScoped<IRentingTransRepository, RentingTransRepository>();
builder.Services.AddScoped<IRentingDetailRepository, RentingDetailRepository>();
builder.Services.AddScoped<FUCarRentingManagementContext>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();
