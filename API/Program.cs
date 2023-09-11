using System.Text;
using API.Data;
using API.Extentions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);



var app = builder.Build();
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
