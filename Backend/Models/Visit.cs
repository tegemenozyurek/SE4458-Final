namespace Prescription_and_Doctor_Visit_Management_System.Models
{
    public class Visit
    {
        public int VisitID { get; set; }
        public int PatientID { get; set; }
        public Patient? Patient { get; set; }
        public int DoctorID { get; set; }
        public Doctor? Doctor { get; set; }
        public DateTime VisitDate { get; set; } = DateTime.Now;
        public string Notes { get; set; } = string.Empty;
    }
}
