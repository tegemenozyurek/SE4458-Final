using Microsoft.AspNetCore.Mvc;
using Prescription_and_Doctor_Visit_Management_System.Data;
using Prescription_and_Doctor_Visit_Management_System.Models;

namespace Prescription_and_Doctor_Visit_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionDetailController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrescriptionDetailController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllPrescriptionDetails()
        {
            var prescriptionDetails = _context.PrescriptionDetails.ToList();
            return Ok(prescriptionDetails);
        }

        [HttpPost]
        public IActionResult CreatePrescriptionDetail([FromBody] PrescriptionDetail detail)
        {
            if (detail == null) return BadRequest();

            _context.PrescriptionDetails.Add(detail);
            _context.SaveChanges();
            return Ok(detail);
        }

        [HttpPut("{id}")]
        public IActionResult EditPrescriptionDetail(int id, [FromBody] PrescriptionDetail updatedDetail)
        {
            var detail = _context.PrescriptionDetails.FirstOrDefault(d => d.PrescriptionDetailID == id);
            if (detail == null) return NotFound();

            detail.PrescriptionID = updatedDetail.PrescriptionID;
            detail.MedicineName = updatedDetail.MedicineName;
            detail.Quantity = updatedDetail.Quantity;
            detail.Price = updatedDetail.Price;

            _context.SaveChanges();
            return Ok(detail);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePrescriptionDetail(int id)
        {
            var detail = _context.PrescriptionDetails.FirstOrDefault(d => d.PrescriptionDetailID == id);
            if (detail == null) return NotFound();

            _context.PrescriptionDetails.Remove(detail);
            _context.SaveChanges();
            return Ok();
        }
    }
}
