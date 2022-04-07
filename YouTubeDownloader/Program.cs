using Microsoft.EntityFrameworkCore;
using YouTubeDownloader.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SongsDbContext>(options =>
{
    options.UseSqlite("Data Source=songs.db");
});
// Get SongsDbContext from services provider.
// var dbContext = builder.Services.BuildServiceProvider().GetService<SongsDbContext>();
// dbContext.Database.Migrate();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRouting();
app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.Run();