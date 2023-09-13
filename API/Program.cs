using API.Data;
using API.Extentions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);



var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("AllowAngularOrigins");

// services.AddCors(options => 
// {
//     options.AddPolicy("AllowAngularOrigins",
//     builder =>
//     {
//         builder.WithOrigins(
//                             "http://localhost:4200"
//                             )
//                             .AllowAnyHeader()
//                             .AllowAnyMethod();
//     });
// });
app.UseCors(builder => builder.AllowAnyMethod().AllowAnyHeader()
.WithOrigins("http://localhost:4200"));


app.UseAuthorization();
app.MapControllers();
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
var context =  services.GetRequiredService<DataContext>();
await context.Database.MigrateAsync();
await Seed.SeedUsers(context);

}
catch(Exception ex){
    var logger  = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
