using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using YouTubeDownloader.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// add wwwroot as a static file directory

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SongsDbContext>(options => { options.UseSqlite("Data Source=songs.db"); });
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
var app = builder.Build();
app.UseStaticFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
    app.UseMigrationsEndPoint();

}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRouting();
app.MapControllers();
// set default route to /Song
app.UseEndpoints(endpoints => { endpoints.MapControllerRoute("default", "{controller=Song}/{action=Index}/{id?}"); });

app.MapRazorPages();

app.Run();