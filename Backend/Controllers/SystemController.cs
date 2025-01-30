using Microsoft.AspNetCore.Mvc;
using Prescription_and_Doctor_Visit_Management_System.Data;

namespace Prescription_and_Doctor_Visit_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SystemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("notifications/incomplete-prescriptions")]
        public IActionResult NotifyIncompletePrescriptions()
        {
            var incompletePrescriptions = _context.Prescriptions
                .Where(p => p.Status == "Incomplete")
                .ToList();

            // Örnek: Bildirim oluşturma (gerçek e-posta gönderimi entegre edilebilir)
            foreach (var prescription in incompletePrescriptions)
            {
                var pharmacy = _context.Pharmacies.FirstOrDefault(p => p.PharmacyID == prescription.PharmacyID);
                if (pharmacy != null)
                {
                    // Burada gerçek bir e-posta gönderimi entegre edilebilir
                    Console.WriteLine($"Notification sent to {pharmacy.Name} for Prescription ID: {prescription.PrescriptionID}");
                }
            }

            return Ok(new { message = "Notifications processed.", count = incompletePrescriptions.Count });
        }
    }
}
