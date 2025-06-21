using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.FeedbackApi.Models;

public class Feedbacks
{
    [Key]
    public int FeedbackId { get; set; }

    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    public int PatientId { get; set; }
    public Patient Patient { get; set; }

    public string Comments { get; set; }
    [Range(1, 5)]
    public int Rating { get; set; }
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    public int AppointmentId { get; set; }
    
    public Appointment Appointment { get; set; }
}