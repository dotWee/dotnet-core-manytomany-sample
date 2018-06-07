using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

/*
 * Anlegen:
 * dotnet ef migrations add InitialCreate
 * dotnet ef database update
 *
 * Inhalt anschauen: mit sqlitebrowser
 * apt install sqlitebrowser
 */
namespace ManyToMany
{
    public class ManyToManyContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Dozent> Dozents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=manytomany;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bekanntschaft>()
                .HasKey(t => new { t.DozentId, t.StudentId });

            modelBuilder.Entity<Bekanntschaft>()
                .HasOne(pt => pt.Student)
                .WithMany(p => p.Bekanntschaften)
                .HasForeignKey(pt => pt.StudentId);

            modelBuilder.Entity<Bekanntschaft>()
                .HasOne(pt => pt.Dozent)
                .WithMany(t => t.Bekanntschaften)
                .HasForeignKey(pt => pt.DozentId);
        }
    }

    public class Student
    {
        public int StudentId { get; set; }
        public string Matrikelnummer { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }

        public List<Bekanntschaft> Bekanntschaften { get; set; }
    }

    public class Dozent
    {
        public int DozentId { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }

        public List<Bekanntschaft> Bekanntschaften { get; set; }
    }

    public class Bekanntschaft 
    {        
        public Dozent Dozent { get; set; }
        public int DozentId { get; set; }
     
        public Student Student { get; set; }
        public int StudentId { get; set; }
    }
}
