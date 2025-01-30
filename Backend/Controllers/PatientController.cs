using Microsoft.AspNetCore.Mvc;
using Prescription_and_Doctor_Visit_Management_System.Data;
using Prescription_and_Doctor_Visit_Management_System.Models;

namespace Prescription_and_Doctor_Visit_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatientController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllPatients()
        {
            var patients = _context.Patients.ToList();
            return Ok(patients);
        }

        [HttpPost]
        public IActionResult CreatePatient([FromBody] Patient patient)
        {
            if (patient == null) return BadRequest();

            _context.Patients.Add(patient);
            _context.SaveChanges();
            return Ok(patient);
        }

        [HttpPut("{id}")]
        public IActionResult EditPatient(int id, [FromBody] Patient updatedPatient)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.PatientID == id);
            if (patient == null) return NotFound();

            patient.TC = updatedPatient.TC;
            patient.Name = updatedPatient.Name;
            patient.DateOfBirth = updatedPatient.DateOfBirth;
            patient.Address = updatedPatient.Address;

            _context.SaveChanges();
            return Ok(patient);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePatient(int id)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.PatientID == id);
            if (patient == null) return NotFound();

            _context.Patients.Remove(patient);
            _context.SaveChanges();
            return Ok();
        }
    }
}
