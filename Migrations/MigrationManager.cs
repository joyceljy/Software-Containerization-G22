using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Blog.Models;
using Blog.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Migrations
{
    public static class MigrationManager
    {
        public static IWebHost MigrateDatabase(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<BlogPostsContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                        BlogPost b = new BlogPost {PostId = 1, Creator = "Me", Title = "Test", Body = "test body", Dt = new DateTime()};
                        int bC = appContext.BlogPosts.Count();
                        if (bC == 0){
                            appContext.BlogPosts.Add(b);
                            appContext.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return host;
        }        
    }
}