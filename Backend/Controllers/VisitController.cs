using Microsoft.AspNetCore.Mvc;
using Prescription_and_Doctor_Visit_Management_System.Data;
using Prescription_and_Doctor_Visit_Management_System.Models;

namespace Prescription_and_Doctor_Visit_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VisitController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllVisits()
        {
            var visits = _context.Visits.ToList();
            return Ok(visits);
        }

        [HttpPost]
        public IActionResult CreateVisit([FromBody] Visit visit)
        {
            if (visit == null) return BadRequest();

            _context.Visits.Add(visit);
            _context.SaveChanges();
            return Ok(visit);
        }

        [HttpPut("{id}")]
        public IActionResult EditVisit(int id, [FromBody] Visit updatedVisit)
        {
            var visit = _context.Visits.FirstOrDefault(v => v.VisitID == id);
            if (visit == null) return NotFound();

            visit.PatientID = updatedVisit.PatientID;
            visit.DoctorID = updatedVisit.DoctorID;
            visit.VisitDate = updatedVisit.VisitDate;
            visit.Notes = updatedVisit.Notes;

            _context.SaveChanges();
            return Ok(visit);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVisit(int id)
        {
            var visit = _context.Visits.FirstOrDefault(v => v.VisitID == id);
            if (visit == null) return NotFound();

            _context.Visits.Remove(visit);
            _context.SaveChanges();
            return Ok();
        }
    }
}
