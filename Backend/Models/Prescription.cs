namespace Prescription_and_Doctor_Visit_Management_System.Models
{
    public class Prescription
    {
        public int PrescriptionID { get; set; }
        public int PatientID { get; set; }
        public Patient? Patient { get; set; }
        public int DoctorID { get; set; }
        public Doctor? Doctor { get; set; }
        public int? PharmacyID { get; set; }
        public Pharmacy? Pharmacy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Incomplete";
    }
}
