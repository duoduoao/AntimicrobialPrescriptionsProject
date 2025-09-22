# Antimicrobial Prescriptions System

## System Overview
The **AntimicrobialPrescriptions** system is a **.NET Web API** designed to manage antimicrobial prescriptions in healthcare environments.  
It provides secure endpoints for creating, updating, and tracking prescriptions across their lifecycle stages.  

---

## Functional Requirements
- Clinicians can create, update, and view antimicrobial prescriptions.  
- Capture details: Patient ID, antimicrobial name, dose, frequency, route, indication, start/end dates, prescriber info.  
- Infection Control users can review and discontinue prescriptions.  
- Track prescription status: **Active**, **Reviewed**, **Discontinued**.  
- Display reports grouped by:  **Drug**, **Indication**, **Duration**.   
- Role-based access control: **Clinician**, **Infection Control**.  
- Audit logging of changes to prescriptions.  

---

## Tech Stack
- **Backend**: .NET 9 Web API, Entity Framework Core, FluentValidation (TO DO), Serilog (TO DO), JWT authentication  
- **Frontend**: Angular 22  
- **Database**: SQL Server  

---

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

---

## Setup Instructions

### Backend
1. Clone the repository:  
git clone https://github.com/duoduoao/AntimicrobialPrescriptionsProject.git
cd AntimicrobialPrescriptions
 
2. Restore dependencies:  
dotnet restore
 
3. Configure `appsettings.json` (optional):  
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

---

## Backend Structure
The backend follows a **Clean Architecture** with four distinct layers:  
- **Domain** – Core business logic and entities  
- **Application** – Use cases and validation logic  
- **Infrastructure** – Database (EF Core), external services, repositories  
- **API** – Controllers, JWT authentication, middleware, entry point  


---

## Frontend Structure
The frontend is built with **Angular 22** and integrates with the API via **JWT tokens**.  

### Authentication and Roles
- **Clinician** – Can create, view, and manage prescriptions  
- **Infection Control** – Can review, discontinue prescriptions, and view reports  

### UI Capabilities
- **Role-based Actions**  
- **Clinician**  
 - View prescriptions  
 - Add prescription: create new prescription  
- **Infection Control**  
 - Review, discontinue, and manage prescriptions  
 - Reports: Generate grouped summaries (by antimicrobial name, indication, duration)  

- **Dashboard**  
- Central navigation hub for authenticated users  
- Provides access to prescription management, reviews, and reports

## Test Users (Hard-Coded)

For testing purposes, the system includes pre-defined users with roles:

| Username | Password   | Role             |
|----------|------------|------------------|
| alice    | password1  | Clinician        |
| bob      | password2  | InfectionControl |

> ⚠️ These accounts are for **testing only**.  
