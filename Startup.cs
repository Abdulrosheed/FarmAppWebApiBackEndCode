using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstProject.Auth;
using FirstProject.BackGroundConfiguration;
using FirstProject.BackGroundTask;
using FirstProject.Context;
using FirstProject.Implementation.Repositories;
using FirstProject.Implementation.Services;
using FirstProject.Interfaces;
using FirstProject.Interfaces.Repositories;
using FirstProject.Interfaces.Services;
using FirstProject.MailBox;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FirstProject
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

            services.AddCors(options => options.AddPolicy("Mypolicy", builder =>
           {
               builder
                   .AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
           }));

            services.AddHttpContextAccessor();

            services.AddDbContext<ApplicationContext>(options =>
                    options.UseMySQL(Configuration.GetConnectionString("ConnectionContext")));

            services.AddControllers();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAdminService, AdminService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IFarmerRepository, FarmerRepository>();
            services.AddScoped<IFarmerService, FarmerService>();

            services.AddScoped<IFarmRepository, FarmRepository>();
            services.AddScoped<IFarmService, FarmService>();

            services.AddScoped<IFarmInspectorRepository, FarmInspectorRepository>();
            services.AddScoped<IFarmInspectorService, FarmInspectorService>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IFarmProduceRepository, FarmProduceRepository>();
            services.AddScoped<IFarmProduceService, FarmProduceService>();

            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IFarmReportRepository, FarmReportRepository>();
            services.AddScoped<IFarmReportService, FarmReportService>();

            services.AddScoped<IFarmProductRepository, FarmProductRepository>();
            services.AddScoped<IFarmProductService, FarmProductService>();

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyService, CompanyService>();

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IRequestService, RequestService>();

       

            services.AddScoped<IMailMessage, MailMessage>();

            services.AddHostedService<BackGroundTaskProject>();
            services.Configure<ReminderMailConfiguration>(Configuration.GetSection("ReminderMailConfig"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FirstProject", Version = "v1" });
            });
            var key = "This is our key that we are using to authorixe our user";

            services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(key));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
               {
                   x.RequireHttpsMetadata = false;
                   x.SaveToken = true;
                   x.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };

               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FirstProject v1"));
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("Mypolicy");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
