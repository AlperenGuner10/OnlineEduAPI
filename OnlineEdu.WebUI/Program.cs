using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http.Headers;
using OnlineEdu.WebUI.Services.TokenServices;
using OnlineEdu.WebUI.Services.UserServices;
using OnlineEdu.WebUI.Validators;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddHttpClient("EduClient", config =>
{
	var tokenService = builder.Services.BuildServiceProvider().GetRequiredService<ITokenService>();
	var token = tokenService.GetUserToken;
	config.BaseAddress=new Uri("https://localhost:7189/api/");
	if (token != null)
	{
		config.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenService.GetUserToken);
	}
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
{
	opt.LoginPath="/Login/SignIn";
	opt.LogoutPath="/Login/LogOut";
	opt.AccessDeniedPath="/ErrorPage/AccessDenied";
	opt.Cookie.SameSite = SameSiteMode.Strict;
	opt.Cookie.HttpOnly = true;
	opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
	opt.Cookie.Name="OnlineEduCookie";
	opt.SlidingExpiration = true;
});


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
app.UseStaticFiles();

app.UseRouting();
app.UseStatusCodePagesWithReExecute("/ErrorPage/NotFound404/");
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
