using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swagger_API.Infrastructure.Context;
using Swagger_API.Infrastructure.CustomSettings;
using Swagger_API.Infrastructure.Repositories.Abstraction;
using Swagger_API.Infrastructure.Repositories.Concrete;
using Swagger_API.Mapper;
using Swagger_API.Models;

namespace Swagger_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddAutoMapper(typeof(NationalParkMapping));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<INationalParkRepository, EfNationalParkRepository>();
            services.AddScoped<IUserRepository, EfUserRepository>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("NationalParkAPISpec", new OpenApiInfo()
                {
                    Title = "National Park API",
                    Version = "V.1",
                    Description = "National Park API Documantation",
                    Contact = new OpenApiContact()
                    {
                        Email = "yagizcanseheri@gmail.com",
                        Name = "Yagizcan Seheri",
                        Url = new Uri("https://github.com/YagizcanSeheri")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT License",
                        Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                    }
                });

                //    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //    {
                //        Description = "JWT Authentication header using Bearer scheme",
                //        Name = "Authentication",
                //        In = ParameterLocation.Header,
                //        Type= SecuritySchemeType.ApiKey,
                //        Scheme="Brear"
                //    });
                //    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                //    {
                //        {
                //            new OpenApiSecurityScheme
                //            {
                //                Reference = new OpenApiReference
                //                {
                //                    Type = ReferenceType.SecurityScheme,
                //                    Id="Bearer"
                //                },
                //                Scheme ="oaauth 2.0",
                //                Name ="bearer",
                //                In = ParameterLocation.Header

                //            },
                //            new List<string>()
                //        }

                //    });

                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName()}.xml";

                var xmlCommentFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                options.IncludeXmlComments(xmlCommentFullPath);
                //});

                //var appSettingSections = Configuration.GetSection("AppSettings");
                //services.Configure<AppSettings>(appSettingSections);

                //var appSettings = appSettingSections.Get<AppSettings>();
                //var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);


                //services.AddAuthentication(options =>
                //{
                //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //})
                //.AddJwtBearer(options => {
                //    options.RequireHttpsMetadata = true;
                //    options.SaveToken = true;
                //    options.TokenValidationParameters = new TokenValidationParameters
                //    {
                //        ValidateIssuerSigningKey = true,
                //        IssuerSigningKey = new SymmetricSecurityKey(key),
                //        ValidateAudience = false,
                //        ValidateIssuer = false
                //    };
            });

            services.AddCors();

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/NationalParkAPISpec/swagger.json", "National Park API");
                    //options.RoutePrefix = String.Empty;
                });

            app.UseRouting();

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}

