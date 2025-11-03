using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DisasterReliefApp.Models;

namespace DisasterReliefApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Disaster> Disasters { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Disaster -> ApplicationUser
            builder.Entity<Disaster>()
                .HasOne(d => d.ReportedBy)
                .WithMany()
                .HasForeignKey(d => d.ReportedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Donation -> ApplicationUser
            builder.Entity<Donation>()
                .HasOne(d => d.Donor)
                .WithMany()
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Volunteer -> ApplicationUser
            builder.Entity<Volunteer>()
                .HasOne(v => v.User)
                .WithMany()
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // TaskAssignment -> Volunteer
            builder.Entity<TaskAssignment>()
                .HasOne(t => t.AssignedVolunteer)
                .WithMany()
                .HasForeignKey(t => t.AssignedVolunteerId)
                .OnDelete(DeleteBehavior.SetNull);

            // TaskAssignment -> ApplicationUser (creator)
            builder.Entity<TaskAssignment>()
                .HasOne(t => t.CreatedBy)
                .WithMany()
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Specify decimal precision for Donation.Amount
            builder.Entity<Donation>()
                .Property(d => d.Amount)
                .HasColumnType("decimal(18,2)");
        }
    }
}
