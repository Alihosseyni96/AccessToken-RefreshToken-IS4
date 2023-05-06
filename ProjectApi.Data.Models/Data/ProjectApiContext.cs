using Microsoft.EntityFrameworkCore;
using ProjectApi.Data.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApi.Data.Models.Data
{
    public class ProjectApiContext : DbContext
    {
        public ProjectApiContext(DbContextOptions<ProjectApiContext> context) : base(context) 
        {

        }

        #region Tables

        public DbSet<Person> People { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasKey(e => e.Id);
            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e=> e.Id).ValueGeneratedOnAdd().IsRequired();
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(150);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(250);
                entity.Property(e => e.FatherName).IsRequired().HasMaxLength(150);
                entity.Property(e => e.NationalCode).IsRequired().HasMaxLength(10);
                entity.Property(e => e.BirthDate).IsRequired();
                entity.Property(e=> e.Address).HasMaxLength(850).IsRequired();

            });

            base.OnModelCreating(modelBuilder);
        }
    }

}
