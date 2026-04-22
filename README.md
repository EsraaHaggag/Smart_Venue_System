📌 Repository Description

Venue System is a robust and scalable backend application designed to manage venues, users, and booking workflows with a focus on clean architecture, security, and real-time performance.

The system is built following industry best practices, applying Clean Architecture principles to ensure separation of concerns and maintainability. It leverages the CQRS pattern using MediatR to decouple business logic and improve code organization.

Authentication and user management are implemented with a strong focus on security, including OTP-based password reset, validation layers, and protection against invalid or duplicate data (e.g., unique username and phone number checks).

To enhance user experience, the system integrates SignalR for real-time communication, enabling instant updates and notifications without the need for polling.

The project also emphasizes performance and scalability through proper use of Dependency Injection, efficient database handling, and modular service design.

🚀 Key Features & Highlights
🧱 Clean Architecture (Domain, Application, Infrastructure, API layers)
🔄 CQRS Pattern with MediatR (Commands & Queries separation)
🔐 Authentication & Authorization System
OTP-based password reset
Secure credential handling
✅ Advanced Validation
Unique username & phone validation
Request validation before processing
⚙️ Dependency Injection
Loosely coupled and testable services
🛢️ Database Integration
Entity Framework Core
Proper relationships & constraints (Foreign Keys)
⚡ Real-Time Features with SignalR
Instant notifications
Live system updates
🛡️ Error Handling & Stability
Centralized exception handling
🌐 RESTful API Design
Structured and scalable endpoints


🛠️ Tech Stack
ASP.NET Core
MediatR
Entity Framework Core
SignalR
SQL Server
Clean Architecture
🎯 Project Value

This project demonstrates the ability to design and implement a production-level backend system that is scalable, secure, and maintainable, while applying modern software engineering practices used in real-world applications.
