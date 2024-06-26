using Microsoft.OpenApi.Models;
using MSA.Common.Contracts.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var srvUrlsSetting = builder.Configuration.GetSection(nameof(ServiceUrlsSetting)).Get<ServiceUrlsSetting>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var scheme = new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri($"{srvUrlsSetting?.IdentityServiceUrl}/connect/authorize"),
                TokenUrl = new Uri($"{srvUrlsSetting?.IdentityServiceUrl}/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                     { "bankapi.read", "Access read operations" },
                     { "bankapi.write", "Access write operations" }
                }
            }
        },
        Type = SecuritySchemeType.OAuth2
    };

    options.AddSecurityDefinition("OAuth", scheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Id = "OAuth", Type = ReferenceType.SecurityScheme }
            },
            new List<string>{}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthClientId("bank-swagger");
        options.OAuthScopes("profile", "openid");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();