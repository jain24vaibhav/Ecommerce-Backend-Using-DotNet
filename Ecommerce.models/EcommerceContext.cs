using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.models
{
    public class EcommerceContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                /* .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);*/
                IConfigurationRoot config = builder.Build();
                var connectionString = "Server=XIPL9377\\SQLEXPRESS; Database=xpx-ecommerce;Integrated Security=True; Trusted_Connection = True; MultipleActiveResultSets=true ";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public EcommerceContext(DbContextOptions options) : base(options)
        {

        }

        public EcommerceContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Department> departments { get; set; }
        public DbSet<Product> products { get; set; }
    }
}
