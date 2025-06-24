using HospitalManagement.FeedbackApi.Data;
using HospitalManagement.FeedbackApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
//     var doctor = db.Doctors.FirstOrDefault();
//     var patient = db.Patients.FirstOrDefault();

//     if (doctor != null && patient != null && !db.Appointments.Any())
//     {
//         db.Appointments.AddRange(
//             new Appointment
//             {
//                 DoctorId = doctor.DoctorId,
//                 PatientId = patient.PatientId,
//                 AppointmentDate = DateTime.UtcNow.AddHours(1),
//                 IsApproved = true
//             },
//             new Appointment
//             {
//                 DoctorId = doctor.DoctorId,
//                 PatientId = patient.PatientId,
//                 AppointmentDate = DateTime.UtcNow.AddHours(2),
//                 IsApproved = false
//             }
//         );
//         db.SaveChanges();
//     }
// }

// Middleware pipeline
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Route config: works for both Views and APIs
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=FeedbackView}/{action=Submit}/{id?}");

app.MapControllers(); 
app.Run();
