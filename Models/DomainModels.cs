using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DisasterReliefApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
    }

    public enum SeverityLevel { Low = 1, Medium = 2, High = 3, Critical = 4 }

    public class Disaster
    {
        public int Id { get; set; }
        [Required, StringLength(150)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime OccurredAt { get; set; }
        [Required]
        public SeverityLevel Severity { get; set; }
        public string? EvidenceUrl { get; set; }
        public string ReportedById { get; set; }
        public ApplicationUser ReportedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum DonationCategory { Food = 0, Clothing = 1, MedicalSupplies = 2, Money = 3 }
    public enum DonationStatus { Pending = 0, Distributed = 1, Cancelled = 2 }

    public class Donation
    {
        public int Id { get; set; }
        public DonationCategory Category { get; set; }
        public string? Description { get; set; }
        public decimal? Amount { get; set; }
        public int? Quantity { get; set; }
        public string DonorId { get; set; }
        public ApplicationUser Donor { get; set; }
        public DonationStatus Status { get; set; } = DonationStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class Volunteer
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Skills { get; set; }
        public string Availability { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }

    public enum TaskStatus
    {
        Pending = 0,
        Assigned = 1,
        InProgress = 2,
        Completed = 3,
        Cancelled = 4
    }

    public class TaskAssignment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ScheduledAt { get; set; }

        public TaskStatus Status { get; set; } = TaskStatus.Pending;

        // Relationships
        public int? AssignedVolunteerId { get; set; }
        public Volunteer AssignedVolunteer { get; set; }

        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }
    }
}
