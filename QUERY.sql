-- Eğer tablolar varsa sil
DROP TABLE IF EXISTS Pharmacies;
DROP TABLE IF EXISTS Doctors;
DROP TABLE IF EXISTS Patients;
DROP TABLE IF EXISTS Prescriptions;
DROP TABLE IF EXISTS PrescriptionDetails;
DROP TABLE IF EXISTS Visits;

-- Eczane tablosu
CREATE TABLE Pharmacies (
    PharmacyID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    Phone NVARCHAR(15) NOT NULL,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL
);

-- Doktor tablosu
CREATE TABLE Doctors (
    DoctorID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Specialization NVARCHAR(255) NOT NULL,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL
);

-- Hasta tablosu
CREATE TABLE Patients (
    PatientID INT IDENTITY(1,1) PRIMARY KEY,
    TC NVARCHAR(11) NOT NULL UNIQUE,
    Name NVARCHAR(255) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Address NVARCHAR(255)
);

-- Reçete tablosu
CREATE TABLE Prescriptions (
    PrescriptionID INT IDENTITY(1,1) PRIMARY KEY,
    PatientID INT NOT NULL,
    DoctorID INT NOT NULL,
    PharmacyID INT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    Status NVARCHAR(50) DEFAULT 'Incomplete',
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID),
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID),
    FOREIGN KEY (PharmacyID) REFERENCES Pharmacies(PharmacyID)
);

-- Reçete detayları tablosu
CREATE TABLE PrescriptionDetails (
    PrescriptionDetailID INT IDENTITY(1,1) PRIMARY KEY,
    PrescriptionID INT NOT NULL,
    MedicineName NVARCHAR(255) NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (PrescriptionID) REFERENCES Prescriptions(PrescriptionID)
);

-- Doktor ziyaretleri tablosu
CREATE TABLE Visits (
    VisitID INT IDENTITY(1,1) PRIMARY KEY,
    PatientID INT NOT NULL,
    DoctorID INT NOT NULL,
    VisitDate DATETIME DEFAULT GETDATE(),
    Notes NVARCHAR(MAX),
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID),
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID)
);

CREATE TABLE NewPrescriptions (
    NewPrescriptionID INT IDENTITY(1,1) PRIMARY KEY,
    PatientID INT NOT NULL,
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID)
);

CREATE TABLE NewPrescriptionDetails (
    NewPrescriptionDetailID INT IDENTITY(1,1) PRIMARY KEY,
    NewPrescriptionID INT NOT NULL,
    MedicineName NVARCHAR(255) NOT NULL,
    FOREIGN KEY (NewPrescriptionID) REFERENCES NewPrescriptions(NewPrescriptionID)
);
