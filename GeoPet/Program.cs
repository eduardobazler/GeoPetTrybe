using System.Text;
using System.Text.Json.Serialization;
using GeoPet.Services;
using GeoPet.Data;
using GeoPet.Services.PetService;
using GeoPet.Services.UserService;
using GeoPet.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddHttpClient<IGeoPetService, GeoPetService>();
builder.Services.AddDbContext<GeoPetContext>();
builder.Services.AddScoped<IGeoPetContext, GeoPetContext>();
builder.Services.AddScoped<IGeoPetRepository, GeoPetRepositorys>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtOptions =>
{
    var key = builder.Configuration.GetValue<string>("JwtConfig:Key");
    Console.WriteLine(key);
    var keyBytes = Encoding.ASCII.GetBytes(key);
    jwtOptions.SaveToken = true;
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuer = false,
    };
});
builder.Services.AddSingleton(typeof(IJwtTokenManager), typeof(JwtTokenManager));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }