using HospitalManagement.FeedbackApi.Data;
using HospitalManagement.FeedbackApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.FeedbackApi.Controllers
{
    public class FeedbackViewController : Controller
    {
        private readonly AppDbContext _context;

        public FeedbackViewController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Submit(int patientId)
        {
            var appointments = _context.Appointments
                .Include(a => a.Doctor)
                .Where(a => a.PatientId == patientId)
                .ToList();

            ViewBag.PatientId = patientId;
            ViewBag.Appointments = appointments;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(int appointmentId, int rating, string comments, int patientId)
        {
            Console.WriteLine($"[DEBUG] AppointmentId: {appointmentId}, Rating: {rating}, PatientId: {patientId}");

            var appointment = _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefault(a => a.AppointmentId == appointmentId);

            if (appointment == null)
            {
                Console.WriteLine("Appointment not found.");
                return BadRequest("Appointment does not exist.");
            }

            if (appointment.IsApproved == false)
            {
                return BadRequest("Appointment is not approved so Feedback can't be submitted.");
            }

            Console.WriteLine($"✅ Found Appointment with DoctorId: {appointment.DoctorId}, PatientId: {appointment.PatientId}");

                
            var feedback = new Feedbacks
            {
                AppointmentId = appointment.AppointmentId,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                Rating = rating,
                Comments = comments,
                SubmittedAt = DateTime.UtcNow
            };

            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
            return RedirectToAction("Success", new { patientId });
        }

        [HttpGet]
        public IActionResult Success(int patientId)
        {
            ViewBag.PatientId = patientId;
            return View();
        }

        [HttpGet]
        public IActionResult ViewFeedbacks(int patientId)
        {
            var feedbacks = _context.Feedbacks
                .Include(f => f.Doctor)
                .Where(f => f.PatientId == patientId)
                .ToList();

            ViewBag.PatientId = patientId;
            return View(feedbacks);
        }

        [HttpGet]

        public IActionResult DoctorViewFeedbacks(int doctorId)
        {
            var Docfeedbacks = _context.Feedbacks
                .Include(f => f.Patient)
                .Where(f => f.DoctorId == doctorId)
                .ToList();
            ViewBag.DoctorId = doctorId;
            return View(Docfeedbacks);
        }
    }
}
