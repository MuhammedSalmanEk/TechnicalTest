using System;
using Microsoft.EntityFrameworkCore;
using StudentTest.Models;

namespace StudentTest.Data
{
	public class ApplicationDbContext: DbContext
    {
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> Options):base(Options)
        {
        }

		public DbSet<MstStudent> MstStudents { get; set; }
        public DbSet<MstSubject> MstSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MstStudent>()
                .HasOne(s => s.Subject)
                .WithMany(m => m.MsStudent)
                .HasForeignKey(s => s.SubjectKey);

            base.OnModelCreating(modelBuilder);
        }

    }
}

