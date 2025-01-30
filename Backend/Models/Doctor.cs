namespace Prescription_and_Doctor_Visit_Management_System.Models
{
    public class Doctor
    {
        public int DoctorID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
