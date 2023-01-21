using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TMS.Core.Entities;
using TMS.Infrastructure.Data;

namespace TMS.App.Helpers
{
    public static class InitialSeeder
    {
        public static void Seed(this IHost host)
        {
            using var serviceScope = host.Services.CreateScope();

            using var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

            dbContext.Database.EnsureCreated();

            if (dbContext.Users.Any())
            {
                return;
            }

            AddUsers(dbContext);

            AddTasks(dbContext);
        }

        private static void AddUsers(AppDbContext dbContext)
        {
            var users = GetSeedData("Users");

            var random = new Random();

            foreach (var user in users.Skip(1))
            {
                var u = user.Split(',');

                var username = u[0].ToLower() + random.Next(10, 100).ToString();

                var newUser = new User()
                {
                    FirstName = u[0],
                    LastName = u[2],
                    Email = username + "@enterprise.com",
                    EmailConfirmed = true,
                    UserName = username,
                    NormalizedUserName = u[0].ToUpper() + u[2].ToUpper(),
                    NormalizedEmail = u[0].ToUpper() + u[2].ToUpper() + "@ENTERPRISE.COM",
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                var password = new PasswordHasher<User>();
                var hashed = password.HashPassword(newUser, "secret");
                newUser.PasswordHash = hashed;

                dbContext.Users.Add(newUser);
            }

            dbContext.SaveChanges();
        }

        private static void AddTasks(AppDbContext dbContext)
        {
            throw new Exception();
            var tasks = GetSeedData("Tasks");
        }

        private static string[] GetSeedData(string fileName)
        {
            var directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var basePath = Path.GetFullPath(Path.Combine(directoryPath!, @"..\..\..\.."));

            string fullPath = Path.Combine(basePath, @$"TMS.App\Helpers\seed\{fileName}.txt");

            string[] lines = File.ReadAllLines(fullPath);

            return lines;
        }
    }
}
