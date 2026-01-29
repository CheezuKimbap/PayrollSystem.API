using Microsoft.EntityFrameworkCore;
using PayrollSystem.API.Dtos;
using PayrollSystem.API.Models;

namespace PayrollSystem.API.Data
{
    public class PayrollDbContext : DbContext
    {
        public PayrollDbContext(DbContextOptions<PayrollDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeSummaryDto> EmployeeResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("tbl_Employee");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                
                entity.Property(e => e.EmployeeNumber).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.EmployeeNumber).IsUnique();
                
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.MiddleName).HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);

                entity.Property(e => e.DailyRate).HasColumnType("decimal(18,2)").HasDefaultValue(0);

                entity.Property(e => e.DeletedAt).HasDefaultValue(null);
                entity.Property(e => e.ModifiedAt).HasDefaultValue(null);

             

            });
        }
    }
}
