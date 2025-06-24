namespace HospitalManagement.FeedbackApi.Models;
public class Doctor
{
    public int DoctorId { get; set; }
    public string Name { get; set; }
    public string Specialization { get; set; }

    public ICollection<Feedbacks> Feedbacks { get; set; }
}