using HospitalManagement.FeedbackApi.Models;
namespace HospitalManagement.FeedbackApi.Dtos;

public class FeedbackDto
{
    public int AppointmentId { get; set; }
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public string Comments { get; set; }
    public int Rating { get; set; }
}
