using Microsoft.AspNetCore.Mvc;
using Prescription_and_Doctor_Visit_Management_System.Data;
using Prescription_and_Doctor_Visit_Management_System.Models;

namespace Prescription_and_Doctor_Visit_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrescriptionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllPrescriptions()
        {
            var prescriptions = _context.Prescriptions.ToList();
            return Ok(prescriptions);
        }

        [HttpPost]
        public IActionResult CreatePrescription([FromBody] Prescription prescription)
        {
            if (prescription == null) return BadRequest();

            // Ziyaret kaydı oluştur
            var visit = new Visit
            {
                PatientID = prescription.PatientID,
                DoctorID = prescription.DoctorID,
                VisitDate = DateTime.Now,
                Notes = "Automatic visit record for prescription creation."
            };
            _context.Visits.Add(visit);

            // Reçeteyi kaydet
            _context.Prescriptions.Add(prescription);
            _context.SaveChanges();
            return Ok(new { prescription, visit });
        }

        [HttpPut("{id}")]
        public IActionResult EditPrescription(int id, [FromBody] Prescription updatedPrescription)
        {
            var prescription = _context.Prescriptions.FirstOrDefault(p => p.PrescriptionID == id);
            if (prescription == null) return NotFound();

            prescription.PatientID = updatedPrescription.PatientID;
            prescription.DoctorID = updatedPrescription.DoctorID;
            prescription.PharmacyID = updatedPrescription.PharmacyID;
            prescription.Status = updatedPrescription.Status;

            _context.SaveChanges();
            return Ok(prescription);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePrescription(int id)
        {
            var prescription = _context.Prescriptions.FirstOrDefault(p => p.PrescriptionID == id);
            if (prescription == null) return NotFound();

            _context.Prescriptions.Remove(prescription);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("search")]
        public IActionResult SearchPrescriptions([FromQuery] int? prescriptionId, [FromQuery] string? tc)
        {
            if (prescriptionId == null && string.IsNullOrEmpty(tc))
                return BadRequest("Prescription ID or TC must be provided.");

            var prescriptions = _context.Prescriptions.AsQueryable();

            if (prescriptionId != null)
                prescriptions = prescriptions.Where(p => p.PrescriptionID == prescriptionId);

            if (!string.IsNullOrEmpty(tc))
            {
                var patient = _context.Patients.FirstOrDefault(p => p.TC == tc);
                if (patient == null) return NotFound("Patient with the given TC not found.");
                prescriptions = prescriptions.Where(p => p.PatientID == patient.PatientID);
            }

            return Ok(prescriptions.ToList());
        }
    }
}
