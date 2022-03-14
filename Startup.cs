using EskhataDigital.Core.Validators;
using EskhataDigital.Infrastructure.Seeder;
using EskhataDigital.Services.AuthenticationSevice;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace EskhataDigital
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            #region Swagger Configuration

            services.AddSwaggerGen();
            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "EskahataDigital API"
                });
                // To Enable authorization using Swagger (JWT)
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });

            #endregion

            // Session
            services.AddDistributedMemoryCache();
            services.AddSession();

            // DB Context
            var connectionstring = Configuration.GetConnectionString("WebApiDatabase");
            services.AddDbContext<ApplicationContext>(options =>
                                                options.UseMySql(connectionstring, ServerVersion.AutoDetect(connectionstring)));

            // Custom Jwt authentication
            services.AddAuthenticationService();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        var context1 = services.GetRequiredService<ApplicationContext>();
                        SeedSampleData.SeedData(context1);
                    }
                    catch { }
                }

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSession();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseJwtAuthenticateMiddleware("/AuthenticationApi/Login", "/AuthenticationApi/Register", "/AuthenticationApi/Refresh");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
