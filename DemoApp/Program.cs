using DemoApp.Core;
using DemoApp.Persistence;
using DemoApp.Utilities.SecurityManagement;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AccessOptions>(opt => builder.Configuration.Bind(nameof(AccessOptions), opt));

builder.Host
    .UseSerilog((hostingContext, services, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(hostingContext.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console());

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddCore(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerGen(options =>
{
    #region Security definition
    options.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "Najjača aplikacija u galaksiji API!", 
        Version = "v1", 
        Description = "Ovo je najjača aplikacija u galaksiji i šire. Onaj koji ju koristi je najjači u galaksiji! (p.s. Sandi je kriv za ovu tvorevinu:))" 
    });
    options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = "X-Api-Key",
        Description = "Specify API Key",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" }
            },
            Array.Empty<string>()
        }
    });
    #endregion
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
app.MapControllers();

app.Run();