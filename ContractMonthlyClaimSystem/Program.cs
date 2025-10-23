using ContractMonthlyClaimSystem.Infrastructure.FileStorage;
using ContractMonthlyClaimSystem.Services.InMemory;
using ContractMonthlyClaimSystem.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
#if DEBUG
    .AddRazorRuntimeCompilation()
#endif
    ;

builder.Services.AddSingleton<ILecturerService, InMemoryLecturerService>();
builder.Services.AddSingleton<IClaimService, InMemoryClaimService>();
builder.Services.AddSingleton<IDocumentService, InMemoryDocumentService>();
builder.Services.AddSingleton<LocalFileStorage>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
