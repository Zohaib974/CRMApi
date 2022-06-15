﻿using CRMContracts;
using CRMEntities;
using CRMEntities.Models;
using CRMRepository;
using CRMServices.Implementation;
using CRMWebHost.Configurations;
using LoggerService;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebHost.Extensions
{
    public static class ServiceExtensions
    {
        //configure CORS
        public static void ConfigureCors(this IServiceCollection services) =>
                            services.AddCors(options =>
                            {
                                options.AddPolicy("CorsPolicy", builder =>
                                builder.AllowAnyOrigin() //WithOrigins("https://example.com")
                                .AllowAnyMethod()  //WithMethods("POST", "GET")
                                .AllowAnyHeader()); //WithHeaders("accept", "content-type")
                            });

        //Configure IIS
        public static void ConfigureIISIntegration(this IServiceCollection services) =>
                            services.Configure<IISOptions>(options =>
                            {
                                //with defaults
                            });

        //Configure NLog
        public static void ConfigureLoggerService(this IServiceCollection services) =>
                            services.AddScoped<ILoggerManager, LoggerManager>();

        //Configure DB Provider
        public static void ConfigureSqlContext(this IServiceCollection services,
        IConfiguration configuration) =>
                            services.AddDbContext<RepositoryContext>(opts =>
                            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")
                            //,b=> b.MigrationsAssembly("CRMWebHost")   //Change migration's folder to main project
                            ));

        //Configure repos
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
                           services.AddScoped<IRepositoryManager, RepositoryManager>();

        //Configure service manager
        public static void ConfigureServiceManager(this IServiceCollection services) =>
                           services.AddScoped<IServiceManager, ServiceManager>();
        //Configure custom output formatter
        public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder) =>
                            builder.AddMvcOptions(config => config.OutputFormatters.Add(new
                            CsvOutputFormatter()));

        //configure api versioing
        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                //opt.ApiVersionReader = new HeaderApiVersionReader("api-version");  //Header versioning
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
            });
        }
        //Configure caching
        public static void ConfigureResponseCaching(this IServiceCollection services) => services.AddResponseCaching();

        //Configure Marvin cache validation headers
        public static void ConfigureHttpCacheHeaders(this IServiceCollection services) =>
            services.AddHttpCacheHeaders(
                (expirationOpt) =>
                {
                    expirationOpt.MaxAge = 65;
                    expirationOpt.CacheLocation = CacheLocation.Private;
                },
                (validationOpt) =>
                {
                    validationOpt.MustRevalidate = true;
                });
        //Configure Asp Identity
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User,UserRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(UserRole), builder.Services);
            IdentityBuilder identityBuilder = builder.AddEntityFrameworkStores<RepositoryContext>()
                                                    .AddSignInManager<ApplicationSignInManager>()
                                                    .AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("jwtKey").Value))
                };
            });
        }
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CRM API",
                    Version = "v1"
                });
                s.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "CRM API",
                    Version = "v2"
                });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                     {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                        },
                        new List<string>()
                    }
                });
            });
        }

        public static void ConfigureDataProtectionToken(this IServiceCollection services, IConfiguration configuration)
        {
            var expiredInHours = int.Parse(configuration.GetSection("ResetPasswordLinkExpiryDurationHours").Value);
            services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(expiredInHours));
        }
    }
    public class ApplicationSignInManager : SignInManager<User>
    {
        public ApplicationSignInManager(UserManager<User> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<User> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<User>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<User> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }
    }
}
