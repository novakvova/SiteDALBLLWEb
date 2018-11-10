using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    internal class DBInitializer : 
        CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //Ролі користувачів
            #region InitRoleName
            //Роль Admin
            context.Roles.AddOrUpdate(
                h => h.Id,   // Use Name (or some other unique field) instead of Id
                new CustomRole
                {
                    Id = 1,
                    Name = "Admin"
                });

            #endregion

            //Створення користувачів
            #region InitUser
            IUserStore<ApplicationUser, int> appStore = new CustomUserStore(context);
            var appManager = new UserManager<ApplicationUser, int>(appStore);
            var userAdmin = new ApplicationUser
            {
                Id=1,
                Email="admin@gmail.com",
                PasswordHash = appManager.PasswordHasher.HashPassword("123456Qwerty1-"),
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName= "admin@gmail.com"
            };
            context.Users.AddOrUpdate(
                u=>u.Id,
                userAdmin);

            var userRole = new CustomUserRole
            {
                RoleId = 1,
                UserId = 1
            };
            context.Set<CustomUserRole>().AddOrUpdate(
                u => new { u.UserId, u.RoleId },
                userRole);
            #endregion
            base.Seed(context);
        }
    }
}
