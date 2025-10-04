DisasterReliefApp

A web application built with ASP.NET Core 8 MVC and Entity Framework Core to manage disaster relief operations.
The system supports Identity authentication, role-based access, disaster incident reporting, volunteer coordination, and donation management.

🚀 Quick Start
Prerequisites
.NET 8 SDK
SQL Server (LocalDB, Azure SQL, or full SQL Server)
Setup Instructions

Clone or download the project source code.

Update the connection string in appsettings.json to point to your SQL Server instance.

Open a terminal in the project folder and run:

dotnet restore
dotnet ef database update
dotnet run


Navigate to the app in your browser:

https://localhost:5001

👤 Admin Account

An admin account is scaffolded with Identity for quick testing:

Email: admin@example.com
Password: Admin123!

Use this account to log in and access the admin dashboard.

🛠 Development Notes

The included migration is a simplified placeholder to get started.

For production or schema updates, create your own migration:

dotnet ef migrations add Initial
dotnet ef database update


This ensures Identity tables and all entity relationships are generated correctly for your environment.

📂 Features
Disaster Management: Track and report disaster incidents.
Donation Tracking: Manage donations of goods and money.
Volunteer Coordination: Register volunteers, assign tasks, and track involvement.
Role-Based Access: Admins manage disasters, donations, and volunteers.
Secure Authentication: ASP.NET Core Identity with role support.
📌 Future Improvements
Reporting dashboards with charts and statistics
Email notifications for new disaster reports and volunteer tasks
Enhanced UI styling with Bootstrap
⚖ License

This project is provided as a learning and starter template. Customize and extend it for your organization’s needs.
