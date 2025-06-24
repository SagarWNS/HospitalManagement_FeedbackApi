using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement.FeedbackApi.Models;

public class Appointment
{
    public int AppointmentId { get; set; }
    public int DoctorId { get; set; }

    [ForeignKey("DoctorId")]
    public Doctor Doctor { get; set; }
    public int PatientId { get; set; }
    
    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }
    public DateTime AppointmentDate { get; set; }
    public bool IsApproved { get; set; }
    public Feedbacks Feedback { get; set; }
}