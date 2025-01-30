using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prescription_and_Doctor_Visit_Management_System.Data;
using Prescription_and_Doctor_Visit_Management_System.Models;
using System.Linq;

namespace Prescription_and_Doctor_Visit_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewPrescriptionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NewPrescriptionsController(AppDbContext context)
        {
            _context = context;
        }

        // POST /api/newprescriptions
        [HttpPost]
        public async Task<IActionResult> CreateNewPrescription([FromBody] NewPrescriptionRequest newPrescriptionRequest)
        {
            if (newPrescriptionRequest == null || newPrescriptionRequest.Medicines == null || !newPrescriptionRequest.Medicines.Any())
                return BadRequest("Prescription data or medicines are required.");

            // Create new prescription (no 'CreatedAt' field)
            var newPrescription = new NewPrescription
            {
                PatientID = newPrescriptionRequest.PatientID
            };

            _context.NewPrescriptions.Add(newPrescription);
            await _context.SaveChangesAsync(); // Save prescription first to get NewPrescriptionID

            // Add details for each medicine
            foreach (var medicine in newPrescriptionRequest.Medicines)
            {
                var newPrescriptionDetail = new NewPrescriptionDetail
                {
                    NewPrescriptionID = newPrescription.NewPrescriptionID,
                    MedicineName = medicine
                };

                _context.NewPrescriptionDetails.Add(newPrescriptionDetail);
            }

            await _context.SaveChangesAsync(); // Save all medicine details

            return CreatedAtAction(nameof(GetNewPrescriptionById), new { id = newPrescription.NewPrescriptionID }, newPrescription);
        }

        // Get new prescription by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNewPrescriptionById(int id)
        {
            var newPrescription = await _context.NewPrescriptions
                                                 .Include(np => np.PrescriptionDetails)
                                                 .FirstOrDefaultAsync(np => np.NewPrescriptionID == id);

            if (newPrescription == null)
                return NotFound();

            return Ok(newPrescription);
        }

        // Get prescriptions by PatientID
        [HttpGet("byPatient/{patientId}")]
        public async Task<IActionResult> GetPrescriptionsByPatientId(int patientId)
        {
            var prescriptions = await _context.NewPrescriptions
                                               .Where(np => np.PatientID == patientId)
                                               .Include(np => np.PrescriptionDetails)
                                               .ToListAsync();

            if (prescriptions == null || !prescriptions.Any())
                return NotFound();

            return Ok(prescriptions);
        }
    }

    // Model to handle new prescription request with medicines
    public class NewPrescriptionRequest
    {
        public int PatientID { get; set; }
        public List<string> Medicines { get; set; }
    }
}
