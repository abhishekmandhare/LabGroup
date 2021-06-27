using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TFNAPI.Infrastructure.Data.Repositories;
using TFNAPI.Infrastructure.Data.Interfaces;
using TFNAPI.Infrastructure.Data.Interfaces.Repositories;
using TFNAPI.Core.Interfaces.UseCases;
using TFNAPI.Core.UseCases;
using Microsoft.EntityFrameworkCore;
using TFNAPI.Infrastructure.Data.Context;

namespace TFNAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsPolicy",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                    });
            });
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddLogging(config =>
            {
                config.AddDebug();
                config.AddConsole();
            });
            services.AddScoped<ILinkedAttemptUseCase, LinkedAttemptsUseCase>();
            services.AddScoped<ITFNValidatorUseCase, TFNValidatorUseCase>();
            services.AddScoped<IWeightingFactorRepository, WeightFactorRepository>();
            services.AddScoped<ILinkedAttemptRepository, LinkedAttemptRepository>();

            services.AddDbContext<TFNDBContext>(options => options.UseInMemoryDatabase(databaseName: "TFN"));
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

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TFN API");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
