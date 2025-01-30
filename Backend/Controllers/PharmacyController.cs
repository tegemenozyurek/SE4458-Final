using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prescription_and_Doctor_Visit_Management_System.Data;
using Prescription_and_Doctor_Visit_Management_System.Models;

namespace Prescription_and_Doctor_Visit_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PharmacyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PharmacyController(AppDbContext context)
        {
            _context = context;
        }

        // Get all pharmacies
        [HttpGet]
        public IActionResult GetAllPharmacies()
        {
            var pharmacies = _context.Pharmacies.ToList();
            return Ok(pharmacies);
        }

        // Create a new pharmacy
        [HttpPost]
        public IActionResult CreatePharmacy([FromBody] Pharmacy pharmacy)
        {
            if (pharmacy == null) return BadRequest();

            _context.Pharmacies.Add(pharmacy);
            _context.SaveChanges();
            return Ok(pharmacy);
        }

        // Edit a pharmacy's details
        [HttpPut("{id}")]
        public IActionResult EditPharmacy(int id, [FromBody] Pharmacy updatedPharmacy)
        {
            var pharmacy = _context.Pharmacies.FirstOrDefault(p => p.PharmacyID == id);
            if (pharmacy == null) return NotFound();

            pharmacy.Name = updatedPharmacy.Name;
            pharmacy.Address = updatedPharmacy.Address;
            pharmacy.Phone = updatedPharmacy.Phone;
            pharmacy.Username = updatedPharmacy.Username;
            pharmacy.PasswordHash = updatedPharmacy.PasswordHash;

            _context.SaveChanges();
            return Ok(pharmacy);
        }

        // Delete a pharmacy
        [HttpDelete("{id}")]
        public IActionResult DeletePharmacy(int id)
        {
            var pharmacy = _context.Pharmacies.FirstOrDefault(p => p.PharmacyID == id);
            if (pharmacy == null) return NotFound();

            _context.Pharmacies.Remove(pharmacy);
            _context.SaveChanges();
            return Ok();
        }

        // Login a pharmacy (Username and Password check)
        [HttpPost("login")]
        public IActionResult Login([FromBody] PharmacyLoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Username and password are required.");

            var pharmacy = _context.Pharmacies
                                   .FirstOrDefault(p => p.Username == request.Username && p.PasswordHash == request.Password);

            if (pharmacy == null)
                return Unauthorized("Invalid username or password.");

            return Ok(pharmacy);  // Here you can return a token or pharmacy details if needed
        }
    }

    // Model to handle login requests
    public class PharmacyLoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
