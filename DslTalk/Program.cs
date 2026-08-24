using DslTalk.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

var edmBuilder =
    new ODataConventionModelBuilder();

edmBuilder
    .EntityType<Order>()
    .HasKey(x => x.OrderNumber);

edmBuilder.EntitySet<Order>("Orders");

builder.Services
    .AddControllersWithViews()
    .AddOData(options =>
    {
        options
            .Filter()
            .SetMaxTop(100)
            .AddRouteComponents(
                "odata",
                edmBuilder.GetEdmModel());
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
