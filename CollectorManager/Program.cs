using CollectorManager;
using CollectorManager.Data;
using CollectorManager.Data.Repository;
using CollectorManager.Services;
using CollectorManager.Services.AppContext;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRepository();

builder.Services.AddServices();

builder.Services.AddHttpContextAccessor();

builder.Services.AddCookieAuthentication();

builder.Services.AddScoped<IAppContext, WebAppContext>();

builder.Services.AddAutoMapper(typeof(MappingConfiguration));

builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    options.EnableForHttps = true;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
builder.Services.Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
await using (var context = scope.ServiceProvider.GetRequiredService<CollectorsDbContext>())
    await context.Database.MigrateAsync();

app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
