using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HospitalManagement.FeedbackApi.Models;

public class Feedbacks
{
    [Key]
    public int FeedbackId { get; set; }

    public int AppointmentId { get; set; }

    [ForeignKey("AppointmentId")]
    public Appointment Appointment { get; set; }

    public int DoctorId { get; set; }
    [ForeignKey("DoctorId")]
    public Doctor Doctor { get; set; }

    public int PatientId { get; set; }
    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }

    public int Rating { get; set; }
    public string Comments { get; set; }
    public DateTime SubmittedAt { get; set; }
}
