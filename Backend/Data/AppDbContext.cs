using Microsoft.EntityFrameworkCore;
using Prescription_and_Doctor_Visit_Management_System.Models;
using System.Numerics;

namespace Prescription_and_Doctor_Visit_Management_System.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tablolar için DbSet tanımlamaları
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionDetail> PrescriptionDetails { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<NewPrescription> NewPrescriptions { get; set; }
        public DbSet<NewPrescriptionDetail> NewPrescriptionDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TC kimlik numarasının 11 karakter olmasını zorunlu kılma
            modelBuilder.Entity<Patient>()
                .Property(p => p.TC)
                .HasMaxLength(11)
                .IsRequired();

            // Prescription tablosu ilişkileri
            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Patient)
                .WithMany()
                .HasForeignKey(p => p.PatientID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Doctor)
                .WithMany()
                .HasForeignKey(p => p.DoctorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Pharmacy)
                .WithMany()
                .HasForeignKey(p => p.PharmacyID)
                .OnDelete(DeleteBehavior.SetNull);

            // PrescriptionDetail tablosu ilişkisi
            modelBuilder.Entity<PrescriptionDetail>()
                .HasOne(d => d.Prescription)
                .WithMany()
                .HasForeignKey(d => d.PrescriptionID)
                .OnDelete(DeleteBehavior.Cascade);

            // Visit tablosu ilişkileri
            modelBuilder.Entity<Visit>()
                .HasOne(v => v.Patient)
                .WithMany()
                .HasForeignKey(v => v.PatientID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Visit>()
                .HasOne(v => v.Doctor)
                .WithMany()
                .HasForeignKey(v => v.DoctorID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
