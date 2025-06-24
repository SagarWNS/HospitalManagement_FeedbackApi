using Microsoft.EntityFrameworkCore;
using HospitalManagement.FeedbackApi.Models;
namespace HospitalManagement.FeedbackApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Feedbacks> Feedbacks { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
        modelBuilder.Entity<Doctor>().HasData(new Doctor
        {
            DoctorId = 1,
            Name = "Dr. John",
            Specialization = "Cardiology"
        });

        
        modelBuilder.Entity<Patient>().HasData(new Patient
        {
            PatientId = 1,
            Name = "John Doe",
            Email = "john@example.com"
        });

        modelBuilder.Entity<Appointment>().HasData(
            new Appointment
            {
                AppointmentId = 1,
                PatientId = 1,
                DoctorId = 1,
                AppointmentDate = new DateTime(2025, 6, 20),
                IsApproved = true
            },
            new Appointment
            {
                AppointmentId = 2,
                PatientId = 1,
                DoctorId = 1,
                AppointmentDate = new DateTime(2025, 6, 19),
                IsApproved = false
            });
    }
}