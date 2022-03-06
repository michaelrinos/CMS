using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor;

namespace SportsStore {
    public class Startup {
        public Startup(IConfiguration configuration) => 
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            /*
            services.Configure<CookiePolicyOptions>(options => {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            // */
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                Configuration["Data:SportStoreProducts:ConnectionString"]));
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddTransient<IViewRepository, EFViewsRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc();
            services.Configure<RazorViewEngineOptions>(opts =>
                opts.FileProviders.Add(
                    new DatabaseFileProvider(Configuration.GetConnectionString("CMSConnection"))
                )
            );
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            } else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes => {
                /*
                routes.MapRoute(
                    name: null,
                    template: "DynamicContent/{contentItem}",
                    defaults: new {
                        controller = "Home",
                        action = "DynamicContent",
                        contentItem = "NotSpecified"
                    });
                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List" }
                );
                routes.MapRoute(
                    name: null,
                    template: "Page{productPage:int}",
                    defaults: new {
                        controller = "Product",
                        action = "List", productPage = 1
                    }
                );
                
                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new {
                        controller = "Product",
                        action = "List", productPage = 1
                    }
                );
                // */

                routes.MapRoute(
                    name: "Editor-M-L",
                    template: "Editor/Mode-{mode}/Location-{location}",
                    defaults: new {
                        controller = "Dynamic",
                        action = "Editor",
                    });

                routes.MapRoute(
                    name: "Editor-L-M",
                    template: "Editor/Location-{location}/Mode-{mode}",
                    defaults: new {
                        controller = "Dynamic",
                        action = "Editor",
                    });

                routes.MapRoute(
                    name: null,
                    template: "Editor/Mode-{mode}",
                    defaults: new {
                        controller = "Dynamic",
                        action = "Editor",
                    });
                routes.MapRoute(
                    name: null,
                    template: "Editor/Location-{location}",
                    defaults: new {
                        controller = "Dynamic",
                        action = "Editor",
                    });

                routes.MapRoute(
                    name: null,
                    template: "Editor",
                    defaults: new {
                        controller = "Dynamic",
                        action = "Editor",
                    });

                routes.MapRoute(
                    name: null,
                    template: "{action}",
                    defaults: new {
                        controller = "Dynamic",
                        action = "Index",
                    });

                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}",
                    defaults: new { 
                        controller = "Dynamic",
                        action = "Index" 
                    });
                // */
            });
            /*
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Product}/{action=List}/{id?}");
            });
            // */
            SeedData.EnsurePopulated(app, Configuration.GetConnectionString("CMSConnection"));
        }
    }
}
