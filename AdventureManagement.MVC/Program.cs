using AdventureManagement.MVC.Service.Implement;
using AdventureManagement.MVC.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IParticipantService, ParticipantService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7191/api/"); 
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
