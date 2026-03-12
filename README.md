# 📝 Smart Task Manager

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core%209-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![Status](https://img.shields.io/badge/Status-Live-success?style=for-the-badge)

**Smart Task Manager** is a full-featured **ASP.NET Core 9 MVC** application for managing personal tasks with priorities, statuses, deadlines, and a clean responsive UI.

---

## ⚡ TL;DR

- ✅ **Layered Architecture + Repository/Unit-of-Work** → maintainable & testable
- ✅ **ASP.NET Core Identity** → secure registration, login, and session management
- ✅ **Pagination, Sorting & Filtering** → efficient data retrieval with dynamic LINQ
- ✅ **Per-User Data Isolation** → each user sees only their own tasks
- ✅ **Docker containerization** → portable, deploy anywhere
- ✅ **Demo Ready** → pre-seeded user with sample tasks

---

## 🚀 Live Demo
The app is containerized and hosted on **Render** with a MonsterASP SQL Server database:

| | Link |
|---|---|
| 🌐 **Live App** | [smarttaskmanager-er43.onrender.com](https://smarttaskmanager-er43.onrender.com) |

> ⚠️ Render free tier spins down after inactivity — first request may take 30–60s.

---

## 🏗 Architecture

```
SmartTaskManager/
├── SmartTaskManager/         # MVC Controllers, Views, Middlewares
├── SmartTaskManagerCore/     # Entities, Interfaces, ViewModels, Helpers
└── SmartTaskManagerData/     # EF Core, Repositories, Services, Unit of Work
```

- **Layered Architecture:** Strict separation across MVC, Core, and Data layers
- **Design Patterns:** Repository Pattern & Unit of Work for clean data access

---

## 🛠 Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 9 MVC |
| ORM | Entity Framework Core |
| Database | Microsoft SQL Server (MonsterASP) |
| Auth | ASP.NET Core Identity |
| UI | Razor Views, Bootstrap 5, SweetAlert2 |
| Containerization | Docker |

---

## ✨ Features

- **Auth:** Register, Login, session management with ASP.NET Identity
- **Task Management:** Create, Edit, Delete, Mark as Done
- **Task Properties:** Title, Description, Status, Priority, Deadline
- **Data Handling:** Pagination, Sorting, and Filtering with dynamic LINQ
- **Per-User Isolation:** Each user sees and manages only their own tasks
- **Notifications:** SweetAlert2 toast messages for all actions

---

## 🗄️ Demo Access

- **Email:** `testuser@example.com`
- **Password:** `Test@123`

> Feel free to register your own account too.

---

## 🐳 Run with Docker

```bash
docker build -t smarttaskmanager .
docker run -d -p 8080:8080 --name smarttask-container smarttaskmanager
```

---

## ⚡ Quick Start (Local)

```bash
git clone https://github.com/AhmedTawhed/SmartTaskManager
cd SmartTaskManager
dotnet ef database update
dotnet run
```
