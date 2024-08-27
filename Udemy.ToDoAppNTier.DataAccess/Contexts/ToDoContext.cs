using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.ToDoAppNTier.DataAccess.Configurations;
using Udemy.ToDoAppNTier.Entities.Domains;

namespace Udemy.ToDoAppNTier.DataAccess.Contexts
{
    public class ToDoContext : DbContext
    {
        //connection string dependency injection şeklinde kullanmak için.
        public ToDoContext(DbContextOptions<ToDoContext> options):base(options) 
        { 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WorkConfiguration());
        }
        public DbSet<Work> Works { get; set; }
    }
}
//biz context stringi doğrudan UI içindeki startup.cs'ye yazamayız. Bunun nedeni N-Tier Arch. akışı UI-BLL-DAL şeklinde çiğnemiş oluruz
//bundan dolayı bizim business katmanında yazıp uı katmanında business katmanında yazdığımız metodu çağırmalıyız.
//extension metot sayesinde bunu yapacağız.
