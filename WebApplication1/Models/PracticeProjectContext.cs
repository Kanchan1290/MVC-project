using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public partial class PracticeProjectContext : DbContext
{
    public PracticeProjectContext()
    {
    }

    public PracticeProjectContext(DbContextOptions<PracticeProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<StudentList> StudentLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=Conn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentList>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__StudentL__4D11D63CC631CD28");

            entity.ToTable("StudentList");

            entity.Property(e => e.StudentId).HasColumnName("studentId");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Faculty)
                .HasMaxLength(30)
                .HasColumnName("faculty");
            entity.Property(e => e.StudentName)
                .HasMaxLength(50)
                .HasColumnName("studentName");
            entity.Property(e => e.StudentPassword)
                .HasMaxLength(50)
                .HasColumnName("studentPassword");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
