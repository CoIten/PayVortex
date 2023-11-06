using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PayVortex.Service.AuthAPI.Core.Interfaces.Repos;
using PayVortex.Service.AuthAPI.Core.Interfaces.Services;
using PayVortex.Service.AuthAPI.Core.Models;
using PayVortex.Service.AuthAPI.Core.Services;
using PayVortex.Service.AuthAPI.Infrastructure.Implementations;
using System.Text;

namespace PayVortex.Service.AuthAPI
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfraStructureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            return services;
        }

        public static IServiceCollection AddApplicationCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }

        public static IServiceCollection AddJwtAuthServices(this IServiceCollection services, IOptions<JwtOptions> jwtOptions)
        {
            var secret = jwtOptions.Value.Secret;
            var issuer = jwtOptions.Value.Issuer;
            var audience = jwtOptions.Value.Audience;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!))
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("Support", policy => policy.RequireRole("Support"));
            });
            return services;
        }

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
            return services;
        }
    }
}
