using Microsoft.EntityFrameworkCore;
using Chapeau.Infrastructure.Data;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Chapeau.Core.Assistant;
using Chapeau.Infrastructure;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
    {
        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
    })
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "Chapeau";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = false;
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        };
    });



builder.Services.AddAuthorization();

// DbContext — dual provider (Sqlite local, PostgreSQL production)
var provider = builder.Configuration.GetValue<string>("DatabaseProvider");
if (provider == "Postgres")
{
    builder.Services.AddDbContext<ChapeauDbContext>(options =>
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("Postgres"),
            postgres => postgres.MigrationsAssembly(
                "Chapeau.Migrations.Postgres")));
}
else
{
    builder.Services.AddDbContext<ChapeauDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));
}

builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN";
});

builder.Services.AddSingleton<GeminiOptions>(_ =>
{
    var options = builder.Configuration
        .GetSection("Gemini")
        .Get<GeminiOptions>();

    if (options is null ||
        string.IsNullOrWhiteSpace(options.ApiKey) ||
        string.IsNullOrWhiteSpace(options.Model))
    {
        throw new InvalidOperationException(
            "Gemini API key and model must be configured.");
    }

    return options;
});


builder.Services.AddHttpClient<IAssistantService, GeminiAssistantService>(
    client =>
    {
        client.BaseAddress =
            new Uri("https://generativelanguage.googleapis.com/");

        client.Timeout = TimeSpan.FromSeconds(30);
    });

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode =
        StatusCodes.Status429TooManyRequests;

    options.AddFixedWindowLimiter("assistant", limiter =>
    {
        limiter.PermitLimit = 10;
        limiter.Window = TimeSpan.FromMinutes(1);
        limiter.QueueLimit = 0;
        limiter.AutoReplenishment = true;
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter();

app.MapControllers();

app.Run();