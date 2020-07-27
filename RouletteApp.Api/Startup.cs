using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RouletteApp.Business.Logic;
using RouletteApp.Core.Interfaces;
using RouletteApp.Core.Models;
using RouletteApp.Data.Context;
using RouletteApp.Data.Emtities;
using RouletteApp.Data.Repositories;
using System.Collections.Generic;

namespace RouletteApp.Api
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

            services.AddDbContext<AppDbContext>(
                opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                })
            );

            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
                opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });


            services.AddTransient<IRepository<Roulette>, AppRepository<Roulette>>();
            services.AddTransient<IRepository<Consumer>, AppRepository<Consumer>>();
            services.AddTransient<RouletteBll, RouletteBll>();
            services.AddTransient<BetRepository, BetRepository>();
            services.AddTransient<ResponseModel, ResponseModel>();
            services.AddTransient<MessageListModel, MessageListModel>();
            services.AddTransient<List<BetResponseModel>, List<BetResponseModel>>();
            services.AddTransient<BetResponseModel, BetResponseModel>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}