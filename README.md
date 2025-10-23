

# Contract Monthly Claim System (CMCS)

A web-based system built with **ASP.NET Core MVC** to streamline the submission, verification, and approval of monthly claims for contract lecturers.  
This project was developed as part of the **Programming 2B (PROG6212) Portfolio of Evidence**.

🌐 **Live Website**: [CMCS on Azure](https://giftofthegivers20251001185543.azurewebsites.net/)  
💻 **GitHub Repository**: [ContractMonthlyClaimSystem](https://github.com/Ntwalo/ContractMonthlyClaimSystem2025)

---

## 📖 Project Overview

The **Contract Monthly Claim System (CMCS)** replaces manual, paper-based claim processes with a secure, efficient, and user-friendly digital platform.  
It supports three main roles:

- **Lecturer** – Submit claims, upload supporting documents, and track claim status.  
- **Programme Coordinator** – Review pending claims and approve/reject them.  
- **Academic Manager** – Oversee claim approvals and ensure compliance.  

This prototype (Part 2) uses **in-memory services** for data storage (no database yet), with a clean architecture that can easily be extended to use Entity Framework and SQL Server in later phases.

---

## 🛠️ Tech Stack

- **Framework**: ASP.NET Core MVC (.NET 8)  
- **Frontend**: Razor Views, Bootstrap 5, JavaScript (for auto-calculation & validation)  
- **Storage**: In-memory services (Claims, Lecturers, Documents)  
- **File Handling**: Local file storage under `wwwroot/uploads/documents`  
- **Testing**: xUnit + Mo
