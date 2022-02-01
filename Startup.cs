using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Blog.Models;
using Blog.Data;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Blog
{
	public class Startup
	{
        readonly string AllowCORS = "AllowCORS";
        
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
            services.AddControllers();
            services.AddCors(options => {
                options.AddPolicy(name: AllowCORS, builder => {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); 
                });
            });

            IConfigurationSection dbConfig = Configuration.GetSection("DBConnection");
            string connectionstring = "Host="+dbConfig["Host"]+";Port="+dbConfig["Port"]+";Username="+dbConfig["User"]+";Password="+dbConfig["Password"]+";Database="+dbConfig["Database"]+";";
            services.AddDbContext<BlogPostsContext>(options => options.UseNpgsql(connectionstring));

            services.AddScoped(typeof(IDataRepository<>), typeof(DataRepository<>));
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
            
            if (env.IsDevelopment())
            {          
                app.UseDeveloperExceptionPage();
            }
            //app.UseHttpsRedirection();
            app.UseCors(AllowCORS);

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
	}
}
