namespace HospitalManagement.FeedbackApi.Models;
public class Patient
{
    public int PatientId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public ICollection<Feedbacks> Feedbacks { get; set; }
}