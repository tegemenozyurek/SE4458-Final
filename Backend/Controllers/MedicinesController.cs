using Microsoft.AspNetCore.Mvc;
using Prescription_and_Doctor_Visit_Management_System.Models;
using Prescription_and_Doctor_Visit_Management_System.Services;

namespace Prescription_and_Doctor_Visit_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicinesController : ControllerBase
    {
        private readonly MedicineService _medicineService;

        public MedicinesController(MedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        // Get all medicines
        [HttpGet]
        public async Task<IActionResult> GetAllMedicines()
        {
            var medicines = await _medicineService.GetAllAsync();
            return Ok(medicines);
        }

        // Get medicine by name
        [HttpGet("{name}")]
        public async Task<IActionResult> GetMedicineByName(string name)
        {
            var medicine = await _medicineService.GetByNameAsync(name);
            if (medicine == null)
                return NotFound($"Medicine with name '{name}' not found.");
            return Ok(medicine);
        }

        // Search medicines by price range
        [HttpGet("search")]
        public async Task<IActionResult> SearchMedicinesByPrice([FromQuery] int minPrice, [FromQuery] int maxPrice)
        {
            var medicines = await _medicineService.GetByPriceRangeAsync(minPrice, maxPrice);
            return Ok(medicines);
        }

        // Search medicines by partial name (autocomplete-like)
        [HttpGet("search/partial")]
        public async Task<IActionResult> SearchMedicinesByPartialName([FromQuery] string partialName)
        {
            if (string.IsNullOrEmpty(partialName))
                return BadRequest("Partial name must be provided.");

            var medicines = await _medicineService.GetByPartialNameAsync(partialName);
            return Ok(medicines);
        }

        // Create a new medicine
        [HttpPost]
        public async Task<IActionResult> CreateMedicine([FromBody] Medicine newMedicine)
        {
            if (newMedicine == null || string.IsNullOrEmpty(newMedicine.Name))
                return BadRequest("Invalid medicine data.");

            newMedicine.CreatedAt = DateTime.Now; // Set creation date
            await _medicineService.CreateAsync(newMedicine);
            return CreatedAtAction(nameof(GetMedicineByName), new { name = newMedicine.Name }, newMedicine);
        }

        // Delete a medicine by name
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteMedicine(string name)
        {
            var result = await _medicineService.DeleteByNameAsync(name);
            if (!result)
                return NotFound($"Medicine with name '{name}' not found.");
            return NoContent();
        }

        // Add medicines to queue
        [HttpPost("queue")]
        public IActionResult AddMedicinesToQueue([FromBody] List<string> medicineNames)
        {
            if (medicineNames == null || !medicineNames.Any())
                return BadRequest("Medicine names are required");

            try
            {
                var rabbitMqService = new RabbitMqService();
                rabbitMqService.SendMedicineNamesToQueue(medicineNames);
                return Ok($"Successfully added {medicineNames.Count} medicines to queue");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to add medicines to queue: {ex.Message}");
            }
        }
    }
}
