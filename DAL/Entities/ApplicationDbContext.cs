using DAL.Abstract;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole,
        int, CustomUserLogin, CustomUserRole, CustomUserClaim>, IAppDBContext
    {
        public ApplicationDbContext() : base("Katuxa64Connection")
        {
            Database.SetInitializer<ApplicationDbContext>(new DBInitializer());
        }
        public ApplicationDbContext(string connString)
            : base(connString)
        {
            Database.SetInitializer<ApplicationDbContext>(new DBInitializer());
        }
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
