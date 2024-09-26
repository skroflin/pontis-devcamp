using DemoApp.Core;
using DemoApp.Persistence;
using Serilog;
using Microsoft.OpenApi.Models;
using DemoApp.api.Middleware;
using DemoApp.Utilities.SecurityManagement;
using Azure.Identity;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureKeyVault(
                    new Uri(builder.Configuration.GetSection("AzureKeyVaultOptions:Uri").Value!),
                    new DefaultAzureCredential(new DefaultAzureCredentialOptions()
                    {
                        ExcludeVisualStudioCredential = true,
                        ExcludeVisualStudioCodeCredential = true
                    }));

builder.Services.Configure<AccessOptions>(opt => builder.Configuration.Bind(nameof(AccessOptions), opt));

builder.Host.UseSerilog((hostingContext, services, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console());

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddCore(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Demo App API",
        Version = "v1",
        Description = "Demo App API Documentation"
    });

    options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = "X-Api-Key",
        Description = "Enter your API key",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "ApiKey";
    options.DefaultChallengeScheme = "ApiKey";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(builder => builder
    .WithOrigins(new string[] { "http://localhost:4200" })
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    );
}

app.UseHttpsRedirection();
app.UseHsts();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<AccessMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();