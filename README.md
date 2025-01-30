# 🏥 Prescription and Doctor Visit Management System

🔗 **GitHub Repository**: [SE4458-Final](https://github.com/tegemenozyurek/SE4458-Final)  
🎥 **Project Presentation**: [YouTube Video](https://www.youtube.com/watch?v=lsS7qUaI1J0)

## 📌 Project Overview

This system is designed for **prescription and doctor visit management** within **Saglik Bakanligi's** ecosystem. It allows:
- 👨‍⚕️ **Doctors** to create and manage prescriptions for patients.
- 🏪 **Pharmacies** to authenticate and process prescriptions.
- 💊 **Medicine Lookup API** for retrieving medicine data.
- 📩 **Queue-based notification system** for **incomplete prescriptions**.

---

## 🛠️ Technologies Used

| **Component**  | **Technology** |
|---------------|----------------|
| **Backend**  | ASP.NET Core |
| **Frontend** | HTML, CSS, JavaScript |
| **Database** | Azure SQL & MongoDB |
| **Queue Management** | RabbitMQ |
| **Caching** | Redis |
| **Deployment** | Azure App Services |

---

## 📂 Project Structure
📦 SE4458-Final ├── Backend # ASP.NET Core backend ├── Frontend # HTML, CSS, JavaScript frontend ├── python for nosql # Python scripts for NoSQL operations ├── .idea # IDE configuration files (ignore) ├── .venv # Virtual environment (ignore) ├── README.md # Project documentation ├── load_medicine_data.py # Medicine data import script ├── Github & Youtube L’nk.txt # Contains links to GitHub & video presentation ├── QUERY.sql # SQL queries for database setup ├── SE4458_Final_Instructions.pdf # Final project requirements
---
---

## 📑 Database Models

### **SQL Models**
```sql
CREATE TABLE Pharmacy (
    PharmacyID INT PRIMARY KEY,
    Name VARCHAR(255),
    Address TEXT,
    Phone VARCHAR(20),
    Username VARCHAR(100),
    PasswordHash VARCHAR(255)
);

CREATE TABLE Doctor (
    DoctorID INT PRIMARY KEY,
    Name VARCHAR(255),
    Specialization VARCHAR(255),
    Username VARCHAR(100),
    PasswordHash VARCHAR(255)
);

CREATE TABLE Patient (
    PatientID INT PRIMARY KEY,
    TC CHAR(11) UNIQUE,
    Name VARCHAR(255),
    DateOfBirth DATE,
    Address TEXT
);

CREATE TABLE Prescription (
    PrescriptionID INT PRIMARY KEY,
    PatientID INT REFERENCES Patient(PatientID),
    DoctorID INT REFERENCES Doctor(DoctorID),
    PharmacyID INT REFERENCES Pharmacy(PharmacyID) NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    Status VARCHAR(50) DEFAULT 'Incomplete'
);
