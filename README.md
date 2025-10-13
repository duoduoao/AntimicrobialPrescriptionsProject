# Antimicrobial Prescriptions Project

## Backend Overview
The **AntimicrobialPrescriptions** system is a **.NET Web API** designed to manage antimicrobial prescriptions in healthcare environments.  
It provides secure endpoints for creating, updating, and tracking prescriptions across their lifecycle stages.  
## Frontend Overview 
The **AntimicrobialPrescriptions.WebAngular** application is a healthcare managment system designed to facilitate the creation, review, and monitoring of antimicrobial prescriptions in clinical setting.The system implements a role-based workflow where clinicians can create and manage prescriptions, while infection control specialists can review and analyze prescription patterns for compliance and safety monitoring.  
 

---

## Functional Requirements
- Clinicians can create, update, and view antimicrobial prescriptions.  
- Capture details: Patient ID, antimicrobial name, dose, frequency, route, indication, start/end dates, prescriber info.  
- Infection Control users can review and discontinue prescriptions.  
- Track prescription status: **Active**, **Reviewed**, **Discontinued**.  
- Display reports grouped by:  **Drug**, **Indication**, **Duration**.   
- Role-based access control: **Clinician**, **Infection Control**.  (Bug Need Fix)
- Audit logging of changes to prescriptions.  (TO DO)

---

## Tech Stack
- **Backend**: .NET 9 Web API, Entity Framework Core, FluentValidation (TO DO), Serilog (TO DO), JWT authentication  
- **Frontend**: Angular 20  
- **Database**: SQL Server  

---
## Project Components and Key Files

| Component          | Purpose                                      | Key Files / Folder                                       |
|--------------------|----------------------------------------------|---------------------------------------------------------|
| **Backend API**       | Provides RESTful endpoints for prescription management | `src/AntimicrobialPrescriptions.API`                    |
| **Application Layer** | Contains business logic and use cases       | `src/AntimicrobialPrescriptions.Application`              |
| **Domain Layer**      | Defines core entities and business rules    | `src/AntimicrobialPrescriptions.Domain`                   |
| **Infrastructure Layer** | Manages data access and external services | `src/AntimicrobialPrescriptions.Infrastructure`           |
| **Angular Frontend**  | Implements user interface and client-side logic | `AntimicrobialPrescriptions.WebAngular`                   |

This modular architecture ensures clear separation of concerns, making the system maintainable, scalable, and testable.


## API Design

### Authentication
- `POST /api/Auth/login` – Provides JWT-based authentication.  

### Prescriptions
- `GET /api/Prescriptions` – Retrieve all prescriptions  
- `GET /api/Prescriptions/{id}` – Get a specific prescription  
- `POST /api/Prescriptions` – Create a new prescription  
- `PUT /api/Prescriptions/{id}` – Update an existing prescription  
- `POST /api/Prescriptions/{id}/review` – Mark prescription as reviewed  
- `POST /api/Prescriptions/{id}/discontinue` – Discontinue prescription  


## Clinician Workflow Steps

1. **Authentication**  
   Clinicians log in using their credentials via the endpoint
2. **Dashboard Access**  
   Upon successful login, clinicians access a centralized dashboard for managing prescriptions.
3. **View Prescriptions**  
   Clinicians can view the list of prescriptions with filtering options 
4. **Create Prescriptions**  
   New antimicrobial prescriptions can be submitted by clinicians 
5. **Update Prescriptions (TO DO)**  
   Existing prescriptions can be modified by clinicians via

## Infection Control Workflow Steps

1. **Authentication**  
   Infection Control users log in by sending credentials to the API endpoint
2. **Dashboard Access**  
   After authentication, users navigate to a specialized Infection Control interface/dashboard tailored to their role.
3. **Review Prescriptions**  
   Infection Control users review antimicrobial prescriptions by marking them as reviewed
4. **Discontinue Prescriptions**  
   Users can discontinue active prescriptions to stop antimicrobial treatment
5. **Generate Reports**  
   Generate summaries and reports grouped by drug, indication, or prescription duration to monitor antimicrobial use.


---

## Setup Instructions

### Backend
1. Clone the repository:  
git clone https://github.com/duoduoao/AntimicrobialPrescriptionsProject.git
cd AntimicrobialPrescriptions
 
2. Restore dependencies:  
dotnet restore
 
3. Configure `appsettings.json` (No need for Demo):  
- SQL Server database connection  
- JWT authentication settings
  
4. Run database migrations from the solution root:  
dotnet ef database update --project src/AntimicrobialPrescriptions.Infrastructure --startup-project src/AntimicrobialPrescriptions.API
 
5. Run the backend:  
cd src/AntimicrobialPrescriptions.API
dotnet run

API will be available at the configured port with CORS enabled for [http://localhost:4200](http://localhost:4200).  

### Frontend
1. Clone the repository:  
git clone https://github.com/duoduoao/AntimicrobialPrescriptionsProject.git
cd AntimicrobialPrescriptions.WebAngular

2. Install dependencies:  
npm install

3. Start development server:  
ng serve

App will run on [http://localhost:4200](http://localhost:4200).  

### Docker (TO DO)
- Build Docker images for both backend and frontend.  
- Use Docker Compose for running services with environment-based configurations.  

### CI/CD Pipeline  (TO DO)
- YAML pipeline for Azure DevOps / Git
  
---

### Future Enhancements 
The system has several planned improvements to improve deployment, maintainability, and monitoring:
- **Docker Containerization**: Automate build and deployment by containerizing both backend (.NET Core Web API) and frontend (Angular) applications for consistent environments and easy scalability.
- **CI/CD Pipeline**: Integrate Azure DevOps pipelines to automate continuous integration and continuous deployment, including building, testing, and deploying the full stack application.
- **Enhanced Logging**: Implement Serilog for structured and comprehensive logging to facilitate diagnostics and tracking of system behavior.
- **Input Validation**: Integrate FluentValidation to enhance input validation capabilities both on the API and frontend layers, ensuring data integrity and user feedback.
- **Audit Logging**: Develop a comprehensive audit logging mechanism to track changes to antimicrobial prescriptions and user actions, supporting compliance and traceability.

These enhancements will align the system with modern DevOps practices and enterprise-grade quality standards.


## Test Users (Hard-Coded)

For testing purposes, the system includes pre-defined users with roles:

| Username | Password   | Role             |
|----------|------------|------------------|
| alice    | password1  | Clinician        |
| bob      | password2  | InfectionControl |

> ⚠️ These accounts are for **testing only**.  
