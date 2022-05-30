using Blazorise;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Phoneshop.Domain.Abstractions;
using Phoneshop.Infrastructure.Extensions;
using Phoneshop.BlazorApp.Implementations;
using Blazorise.Bootstrap;
using Microsoft.Extensions.Options;
using System;

namespace Phoneshop.BlazorApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpContextAccessor();

            services.AddBlazorise(options =>
            {
                options.ChangeTextOnKeyPress = true;
            });

            services.AddBootstrapProviders();
            services.AddFontAwesomeIcons();

            string baseUrl = Configuration.GetValue<string>("ApiBaseUri") ?? "https://localhost:44361/api/";
            services.AddHttpClient(Options.DefaultName, client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });
            services.AddScoped<IHttpSession, BlazorHttpSession>();

            services.AddPhoneshopBlazorInfrastructure(baseUrl);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
