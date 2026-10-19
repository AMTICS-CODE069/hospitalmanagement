using DoctorApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching();
builder.Services.AddSession();
builder.Services.AddHttpClient();
builder.Services.AddScoped<DoctorService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Doctors/Index");
}

app.UseStaticFiles();
app.UseRouting();
app.UseResponseCaching();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Doctors}/{action=Index}");

app.Run();
