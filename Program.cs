using HospitalManagement.FeedbackApi.Data;
using HospitalManagement.FeedbackApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Doctors.Any())
    {
        db.Doctors.Add(new Doctor { Name = "Dr. Smith", Specialization = "Cardiology" });
        db.Patients.Add(new Patient { Name = "Alice", Email = "alice@example.com" });
        db.SaveChanges();
    }
    if (!db.Appointments.Any())
    {
        var doctor = db.Doctors.First();
        var patient = db.Patients.First();
        var appointment = db.Appointments.First();

        db.Appointments.AddRange(
            new Appointment
            {
                AppointmentId = appointment.AppointmentId,
                DoctorId = doctor.DoctorId,
                PatientId = patient.PatientId,
                AppointmentDate = DateTime.UtcNow.AddHours(1),
                IsApproved = true
            },
            new Appointment
            {
                AppointmentId = appointment.AppointmentId,
                DoctorId = doctor.DoctorId,
                PatientId = patient.PatientId,
                AppointmentDate = DateTime.UtcNow.AddHours(2),
                IsApproved = false
            }
        );
        db.SaveChanges();
    }

}

app.UseAuthorization();

app.MapControllers();

app.Run();