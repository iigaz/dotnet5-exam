using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using XoDotNet.Main.Abstractions;
using XoDotNet.Main.Configuration;
using XoDotNet.Main.Services;

namespace XoDotNet.Main.ServicesExtensions;

public static class AddAuthExtension
{
    public static IServiceCollection AddAuth(this IServiceCollection services, JwtConfig jwtConfig)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtConfig.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey)),
                    ValidateIssuerSigningKey = true
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            path.StartsWithSegments("/games/hub"))
                            context.Token = accessToken;
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddSingleton<IJwtIssuerService>(_ => new JwtIssuerService(jwtConfig));
        return services;
    }
}