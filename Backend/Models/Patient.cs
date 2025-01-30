namespace Prescription_and_Doctor_Visit_Management_System.Models
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string TC { get; set; } = string.Empty; // 11 karakterlik zorunlu
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
