using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TikToken.Database;
using TikToken.Interfaces;
using TikToken.Models;
using TikToken.Services;

var builder = WebApplication.CreateBuilder(args);

// Database Context.
builder.Services.AddDbContext<Context>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));
// Authentication config.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Secret"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
// Authorization Policies.
builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("admin", options => options.RequireRole(User.Roles.Admin.ToString()));
});
// Automapper.
var config = new MapperConfiguration(mc =>
    {
        mc.AddProfile(new MapperService());
    });

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
// Add services to the container.
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
