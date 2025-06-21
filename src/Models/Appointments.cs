using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.FeedbackApi.Models;

public class Appointment
{
    public int AppointmentId { get; set; }
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; }
    public int PatientId { get; set; }
    public Patient Patient { get; set; }
    public DateTime AppointmentDate { get; set; }
    public bool IsApproved { get; set; }
    public Feedbacks Feedback { get; set; }
}