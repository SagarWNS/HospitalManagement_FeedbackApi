using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.FeedbackApi.Data;
using HospitalManagement.FeedbackApi.Models;
using HospitalManagement.FeedbackApi.Dtos;

namespace HospitalFeedbackSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FeedbackController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/feedback
        [HttpPost]
        public async Task<IActionResult> SubmitFeedback([FromBody] FeedbackDto dto)
        {
            var doctorExists = await _context.Doctors.AnyAsync(d => d.DoctorId == dto.DoctorId);
            var patientExists = await _context.Patients.AnyAsync(p => p.PatientId == dto.PatientId);

            if (!doctorExists || !patientExists)
                return NotFound("Doctor or Patient not found.");

            var appointment = await _context.Appointments.FirstOrDefaultAsync(a =>
                a.AppointmentId == dto.AppointmentId &&
                a.PatientId == dto.PatientId &&
                a.DoctorId == dto.DoctorId
                );

            if (appointment == null)
                return BadRequest("No approved appointment found for this doctor and patient.");

            if (!appointment.IsApproved)
                return BadRequest("Appointment is not approved. Cannot submit feedback.");

            var existingFeedback = await _context.Feedbacks
                .FirstOrDefaultAsync(f => f.AppointmentId == appointment.AppointmentId);

            if (existingFeedback != null)
                return BadRequest("Feedback for this appointment already exists.");

            var feedback = new Feedbacks
            {
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                AppointmentId = appointment.AppointmentId, // ✅ Assign FK
                Comments = dto.Comments,
                Rating = dto.Rating,
                SubmittedAt = DateTime.UtcNow
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Feedback submitted successfully" });
        }

        // GET: api/feedback/patient/1
        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatient(int patientId)
        {
            var feedbacks = await _context.Feedbacks
                .Where(f => f.PatientId == patientId)
                .Include(f => f.Doctor)
                .ToListAsync();

            return Ok(feedbacks);
        }

        // GET: api/feedback/doctor/1
        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetForDoctor(int doctorId)
        {
            var feedbacks = await _context.Feedbacks
                .Where(f => f.DoctorId == doctorId)
                .Include(f => f.Patient)
                .ToListAsync();

            return Ok(feedbacks);
        }
    }
}