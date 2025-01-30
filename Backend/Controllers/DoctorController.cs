using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prescription_and_Doctor_Visit_Management_System.Data;
using Prescription_and_Doctor_Visit_Management_System.Models;

namespace Prescription_and_Doctor_Visit_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DoctorController(AppDbContext context)
        {
            _context = context;
        }

        // Get all doctors
        [HttpGet]
        public IActionResult GetAllDoctors()
        {
            var doctors = _context.Doctors.ToList();
            return Ok(doctors);
        }

        // Create a new doctor
        [HttpPost]
        public IActionResult CreateDoctor([FromBody] Doctor doctor)
        {
            if (doctor == null) return BadRequest();

            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            return Ok(doctor);
        }

        // Edit a doctor's details
        [HttpPut("{id}")]
        public IActionResult EditDoctor(int id, [FromBody] Doctor updatedDoctor)
        {
            var doctor = _context.Doctors.FirstOrDefault(d => d.DoctorID == id);
            if (doctor == null) return NotFound();

            doctor.Name = updatedDoctor.Name;
            doctor.Specialization = updatedDoctor.Specialization;
            doctor.Username = updatedDoctor.Username;
            doctor.PasswordHash = updatedDoctor.PasswordHash;

            _context.SaveChanges();
            return Ok(doctor);
        }

        // Delete a doctor
        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            var doctor = _context.Doctors.FirstOrDefault(d => d.DoctorID == id);
            if (doctor == null) return NotFound();

            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
            return Ok();
        }

        // Login a doctor (Username and Password check)
        [HttpPost("login")]
        public IActionResult Login([FromBody] DoctorLoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Username and password are required.");

            var doctor = _context.Doctors
                                 .FirstOrDefault(d => d.Username == request.Username && d.PasswordHash == request.Password);

            if (doctor == null)
                return Unauthorized("Invalid username or password.");

            return Ok(doctor);  // Here you can return a token or doctor details if needed
        }
    }

    // Model to handle login requests
    public class DoctorLoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
