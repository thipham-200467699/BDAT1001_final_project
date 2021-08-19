using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Video4_Authorization.AuthorizationRequirements;

namespace Video4_Authorization
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", config =>
                {
                    config.Cookie.Name = "Grandmas.Cookie";
                    config.LoginPath = "/Home/Authenticate";
                });

            services.AddAuthorization(config =>
            {
                /*var defaultAuthBuilder = new AuthorizationPolicyBuilder(new string[] { CookieAuthenticationDefaults.AuthenticationScheme });
                var defaultAuthPolicy = defaultAuthBuilder
                                        .RequireAuthenticatedUser()
                                        .RequireClaim(ClaimTypes.DateOfBirth)
                                        .Build();
                config.DefaultPolicy = defaultAuthPolicy;*/
                config.AddPolicy("Admin", policyBuilder => policyBuilder.RequireClaim(ClaimTypes.Role, "Admin"));
                config.AddPolicy("Claim.DoB", policyBuilder =>
                {
                    policyBuilder.AddRequirements(new CustomRequireClaim(ClaimTypes.DateOfBirth));
                });
            });

            services.AddScoped<IAuthorizationHandler, CustomRequireClaimHandler>();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // who are you?
            app.UseAuthentication();

            // are you allowed?
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
