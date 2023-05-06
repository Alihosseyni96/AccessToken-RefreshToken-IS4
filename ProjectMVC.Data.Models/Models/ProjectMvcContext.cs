using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectMVC.Data.Models.Models;

public partial class ProjectMvcContext : DbContext
{
    public ProjectMvcContext()
    {
    }

    public ProjectMvcContext(DbContextOptions<ProjectMvcContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Travel> Travels { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=ProjectMVC;Integrated Security=true;Encrypt=False;MultipleActiveResultSets=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Travel>(entity =>
        {
            entity.Property(e => e.DepartedDate).HasColumnType("datetime");
            entity.Property(e => e.Destenition).HasMaxLength(50);
            entity.Property(e => e.Origin).HasMaxLength(50);
            entity.Property(e => e.ReturnDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Travels)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Travels_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.NationalCode).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
