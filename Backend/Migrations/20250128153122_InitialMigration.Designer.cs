﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Prescription_and_Doctor_Visit_Management_System.Data;

#nullable disable

namespace PrescriptionandDoctorVisitManagementSystem.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250128153122_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Prescription_and_Doctor_Visit_Management_System.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoctorID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DoctorID");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Prescription_and_Doctor_Visit_Management_System.Models.Patient", b =>
                {
                    b.Property<int>("PatientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PatientID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TC")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasKey("PatientID");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Prescription_and_Doctor_Visit_Management_System.Models.Pharmacy", b =>
                {
                    b.Property<int>("PharmacyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PharmacyID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PharmacyID");

                    b.ToTable("Pharmacies");
                });

            modelBuilder.Entity("Prescription_and_Doctor_Visit_Management_System.Models.Prescription", b =>
                {
                    b.Property<int>("PrescriptionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrescriptionID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorID")
                        .HasColumnType("int");

                    b.Property<int>("PatientID")
                        .HasColumnType("int");

                    b.Property<int?>("PharmacyID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PrescriptionID");

                    b.HasIndex("DoctorID");

                    b.HasIndex("PatientID");

                    b.HasIndex("PharmacyID");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("Prescription_and_Doctor_Visit_Management_System.Models.PrescriptionDetail", b =>
                {
                    b.Property<int>("PrescriptionDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrescriptionDetailID"));

                    b.Property<string>("MedicineName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PrescriptionID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("PrescriptionDetailID");

                    b.HasIndex("PrescriptionID");

                    b.ToTable("PrescriptionDetails");
                });

            modelBuilder.Entity("Prescription_and_Doctor_Visit_Management_System.Models.Visit", b =>
                {
                    b.Property<int>("VisitID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VisitID"));

                    b.Property<int>("DoctorID")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientID")
                        .HasColumnType("int");

                    b.Property<DateTime>("VisitDate")
                        .HasColumnType("datetime2");

                    b.HasKey("VisitID");

                    b.HasIndex("DoctorID");

                    b.HasIndex("PatientID");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("Prescription_and_Doctor_Visit_Management_System.Models.Prescription", b =>
                {
                    b.HasOne("Prescription_and_Doctor_Visit_Management_System.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Prescription_and_Doctor_Visit_Management_System.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Prescription_and_Doctor_Visit_Management_System.Models.Pharmacy", "Pharmacy")
                        .WithMany()
                        .HasForeignKey("PharmacyID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Doctor");

                    b.Navigation("Patient");

                    b.Navigation("Pharmacy");
                });

            modelBuilder.Entity("Prescription_and_Doctor_Visit_Management_System.Models.PrescriptionDetail", b =>
                {
                    b.HasOne("Prescription_and_Doctor_Visit_Management_System.Models.Prescription", "Prescription")
                        .WithMany()
                        .HasForeignKey("PrescriptionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prescription");
                });

            modelBuilder.Entity("Prescription_and_Doctor_Visit_Management_System.Models.Visit", b =>
                {
                    b.HasOne("Prescription_and_Doctor_Visit_Management_System.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Prescription_and_Doctor_Visit_Management_System.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });
#pragma warning restore 612, 618
        }
    }
}
