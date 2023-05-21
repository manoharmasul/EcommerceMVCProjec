using EcommerceProject.Context;
using EcommerceProject.Repository.Interface;
using EcommerceProject.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IUserAsyncRepository, UserAsyncRepository>();
builder.Services.AddScoped<IProductAsyncRepository, ProductAsyncRepository>();
builder.Services.AddScoped<ICartAsyncRepository, CartAsyncRepository>();
builder.Services.AddScoped<IOrderAsyncRepository, OrderAsycRepository>();
builder.Services.AddScoped<IAttendaceRepository, AttendanceRepository>();

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
