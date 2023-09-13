using API.Extentions;
using API.Middleware;

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

app.Run();
