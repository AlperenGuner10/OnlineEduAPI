using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using OnlineEdu.API.Extensions;
using OnlineEdu.DataAccess.Context;
using System.Reflection;
using System.Text.Json.Serialization;
using OnlineEdu.Business.Configurations;
using System.Text;
using System.Security.Claims;
using OnlineEdu.Entity.Entities;
using OnlineEdu.Business.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddServiceExtensions(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<OnlineEduContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
    opt.UseLazyLoadingProxies();
});

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<OnlineEduContext>().AddErrorDescriber<CustomErrorDescriber>();

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<JwtTokenOptions>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience= true,
        ValidateLifetime = true,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Key)),
        ClockSkew = TimeSpan.Zero,
        NameClaimType = ClaimTypes.Name
    };
});

builder.Services.AddControllers().AddJsonOptions(x=>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

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
