using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab3_AnropaDB.Model;

public partial class SchoolDbContext : DbContext
{
    public SchoolDbContext()
    {
    }

    public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DORSAY-DESKTOP\\SQLEXPRESS;Initial Catalog=SchoolDB;Integrated Security=True;;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassName);

            entity.Property(e => e.ClassName).HasMaxLength(10);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CourseName).HasMaxLength(15);
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentsId);

            entity.Property(e => e.EnrollmentsId)
                .ValueGeneratedNever()
                .HasColumnName("EnrollmentsID");
            entity.Property(e => e.Student)
                .HasMaxLength(50)
                .IsFixedLength();

            entity.HasOne(d => d.CourseNavigation).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.Course)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enrollments_Courses");

            entity.HasOne(d => d.Enrollments).WithOne(p => p.Enrollment)
                .HasForeignKey<Enrollment>(d => d.EnrollmentsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enrollments_Students");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.Property(e => e.GradeId).HasColumnName("GradeID");
            entity.Property(e => e.Grade1)
                .HasMaxLength(1)
                .HasColumnName("Grade");

            entity.HasOne(d => d.CourseNavigation).WithMany(p => p.Grades)
                .HasForeignKey(d => d.Course)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_Courses");

            entity.HasOne(d => d.StudentNavigation).WithMany(p => p.Grades)
                .HasForeignKey(d => d.Student)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_Students");

            entity.HasOne(d => d.TeacherNavigation).WithMany(p => p.Grades)
                .HasForeignKey(d => d.Teacher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_Staff");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.PersonalNumber)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.Position).HasMaxLength(10);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Class).HasMaxLength(10);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.PersonalNumber)
                .HasMaxLength(13)
                .IsUnicode(false);

            entity.HasOne(d => d.ClassNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.Class)
                .HasConstraintName("FK_Students_Classes");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
