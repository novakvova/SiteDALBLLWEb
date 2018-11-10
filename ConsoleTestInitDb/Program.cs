using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestInitDb
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var count=context.Set<ApplicationUser>().AsQueryable().Count();
            Console.WriteLine(count);
        }
    }
}
