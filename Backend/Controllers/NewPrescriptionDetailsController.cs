using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prescription_and_Doctor_Visit_Management_System.Data;
using Prescription_and_Doctor_Visit_Management_System.Models;

namespace Prescription_and_Doctor_Visit_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewPrescriptionDetailsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NewPrescriptionDetailsController(AppDbContext context)
        {
            _context = context;
        }

        // Get details of a specific new prescription by its ID
        [HttpGet("{newPrescriptionID}")]
        public async Task<IActionResult> GetDetailsByPrescriptionId(int newPrescriptionID)
        {
            var details = await _context.NewPrescriptionDetails
                                         .Where(npd => npd.NewPrescriptionID == newPrescriptionID)
                                         .ToListAsync();

            if (!details.Any())
                return NotFound();

            return Ok(details);
        }

        // Create a new prescription detail
        [HttpPost]
        public async Task<IActionResult> CreateNewPrescriptionDetail([FromBody] NewPrescriptionDetail newPrescriptionDetail)
        {
            if (newPrescriptionDetail == null || newPrescriptionDetail.NewPrescriptionID == 0 || string.IsNullOrEmpty(newPrescriptionDetail.MedicineName))
                return BadRequest("Invalid prescription detail data.");

            _context.NewPrescriptionDetails.Add(newPrescriptionDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDetailsByPrescriptionId), new { newPrescriptionID = newPrescriptionDetail.NewPrescriptionID }, newPrescriptionDetail);
        }

        // Delete a prescription detail by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescriptionDetail(int id)
        {
            var detail = await _context.NewPrescriptionDetails
                                       .FirstOrDefaultAsync(npd => npd.NewPrescriptionDetailID == id);

            if (detail == null)
                return NotFound();

            _context.NewPrescriptionDetails.Remove(detail);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
