using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore;
using WebApplication1;
var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.
builder.Services.Configure<Restaurant.Models.MongoSettings>(builder.Configuration.GetSection("MongoDBSettings"));
var mongoSettings = builder.Configuration.GetSection("MongoDBSettings").Get<Restaurant.Models.MongoSettings>();
var identityServerConfig = builder.Configuration.GetSection(nameof(IdentityServerSettings)).Get<IdentityServerSettings>();
// var jwtsecret = builder.Configuration.Get("ApplicationSettings:JWT_Secret");
builder.Services.AddAuthentication(cfg => {
    cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = "oidc";
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
.AddJwtBearer(options => 
{
    options.Authority="https://localhost:7138";
})
.AddOpenIdConnect(options => 
{
    options.Authority="https://localhost:7138";
    options.ClientId = "zorro";
    options.ResponseType = "code";
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("fullaccess");
    options.SaveTokens = true;
});

builder.Services.AddIdentity<ApplicationUser,ApplicationRole>()
.AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
    mongoSettings.ConnectionURI,mongoSettings.DatabaseName
);

builder.Services.AddIdentityServer(options => 
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.Authentication.CookieAuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    
})
.AddAspNetIdentity<ApplicationUser>()
.AddInMemoryApiScopes(identityServerConfig.ApiScopes)
.AddInMemoryApiResources(identityServerConfig.ApiResources)
.AddInMemoryClients(identityServerConfig.Clients)
.AddInMemoryIdentityResources(identityServerConfig.IdentityResources)
.AddDeveloperSigningCredential();
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.ConfigureExternalCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None; // Set SameSite mode
    // options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Ensure the cookie is only sent over HTTPS
    options.Cookie.IsEssential = true;
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None; // Set SameSite mode
    // options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Ensure the cookie is only sent over HTTPS
    options.Cookie.IsEssential = true;
});

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
// builder.Services.ConfigureApplicationCookie(options => options.Cookie.IsEssential = true);

// builder.Services.AddAuthorization(opts =>
// {
//     opts.AddPolicy("Deactivate", policy =>
//     {
//         policy.RequireRole("Admin");
//         policy.RequireAuthenticatedUser();
//         policy.RequireClaim("email");
//     });
// });
builder.Services.AddControllersWithViews(options => {
    options.Filters.Add(new MyCustomAuthorisation("Role","Admin"));
});
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Restaurant.Controllers.MongoService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
// app.UseCors(options => 
// {
//     options.WithOrigins("https://localhost:7138","https://127.0.0.1:7138").AllowAnyMethod().AllowAnyHeader();
    
//     // options.AllowCredentials();
// });

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
// app.UseCookiePolicy();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.Run();
