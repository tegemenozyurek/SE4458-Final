namespace Prescription_and_Doctor_Visit_Management_System.Models
{
    public class NewPrescription
    {
        public int NewPrescriptionID { get; set; }
        public int PatientID { get; set; }

        // Navigation property for Patient (not needed for database, but useful for related data)
        public Patient Patient { get; set; }

        // List of prescription details
        public List<NewPrescriptionDetail> PrescriptionDetails { get; set; }
    }
}
