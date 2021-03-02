using jobsite.Authorization;
using jobsite.Models;
using jobsite.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite
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
            services.AddControllersWithViews(
                options => {
                    options.Filters.Add(typeof(IdentityContorllerFilter));
                    options.Filters.Add(typeof(IdentityPageFilter));
                    }
                );
            services.AddDbContext<JobContext>(options => options.UseSqlServer(Configuration.GetConnectionString("JobConn")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<JobContext>()
                .AddDefaultTokenProviders()
                .AddClaimsPrincipalFactory<ClaimsFactory>();

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = new PathString("/User/Account/Login");
            //});

            services.AddRazorPages();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsCandidate", policyBuilder
                    => policyBuilder.AddRequirements(
                        new CandidateRequirement()
                    ));
            });

            services.AddSingleton<IAuthorizationHandler, AdminHandler>();
            services.AddSingleton<IAuthorizationHandler, CandidateHandler>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "MyArea",
                    pattern: "{area:exists}/{controller}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapRazorPages();
            });
        }
    }
}
