namespace Prescription_and_Doctor_Visit_Management_System.Models
{
    public class NewPrescriptionDetail
    {
        public int NewPrescriptionDetailID { get; set; }
        public int NewPrescriptionID { get; set; }
        public string MedicineName { get; set; }

        // Navigation property
        public NewPrescription NewPrescription { get; set; }
    }
}
