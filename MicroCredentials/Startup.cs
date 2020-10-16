using System;
using MicroCredentials.Data.Persistence;
using MicroCredentials.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using MicroCredentials.Data.Models;
using System.Net.Http;
using Polly;
using Polly.Extensions.Http;
using MicroCredentials.Config;
using Autofac;
namespace MicroCredentials
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private ConfigurationSetting _configurationSetting;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var connectionString = Environment.GetEnvironmentVariable("SQLSERVER_Customer");
            //services.AddControllers();

            //if (string.IsNullOrEmpty(connectionString))
            //{
            var connectionString = Configuration.GetConnectionString("MicroCredentials1");
            //}

            services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<ICustomerDbContext, CustomerDbContext>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IGenericRepository<Customer>, GenericRepository<Customer>>();
            services.AddScoped<IGenericRepository<Quote>, GenericRepository<Quote>>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IQuoteService, QuoteService>();

            services.AddOptions();
            services.AddAuthentication();
            services.AddControllers();

            services.AddConsulConfig(Configuration);

            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = true;
                o.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });

            var option = new DbContextOptionsBuilder<CustomerDbContext>().UseSqlServer(connectionString).Options;
            using (var dbContext = new CustomerDbContext(option))
            {
                dbContext.Database.EnsureCreated();
            }

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseConsul(Configuration);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
              .HandleTransientHttpError()
              .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
              .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        }
        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }

    }
}
