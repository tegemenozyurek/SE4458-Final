namespace Prescription_and_Doctor_Visit_Management_System.Models
{
    public class PrescriptionDetail
    {
        public int PrescriptionDetailID { get; set; }
        public int PrescriptionID { get; set; }
        public Prescription? Prescription { get; set; }
        public string MedicineName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
