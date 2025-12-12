# 📝 Smart Task Manager

A clean and user-friendly **ASP.NET Core MVC** application to manage tasks efficiently. Users can register, log in, create, update, and track tasks with priority, status, and deadlines. The project features a modern landing page, task cards UI, search, pagination, and sorting.

---

## ✨ Key Features
- User registration and login with Identity
- Task CRUD (Create, Read, Update, Delete)
- Task properties:
  - Title
  - Description
  - Status (Pending, In Progress, Completed)
  - Priority (Low, Medium, High)
  - Deadline
- Responsive UI with Bootstrap
- Search, Sorting, and Pagination for tasks
- Friendly landing page with feature cards
- SweetAlert2 notifications for success messages
- Simple and professional navbar and footer
- Task list filtered per user (each user sees only their own tasks)
- Seeded demo user with 12 sample tasks for testing

---

## 🗂 Tech Stack
- **Backend:** ASP.NET Core MVC 9, C#
- **Database:** SQL Server / Entity Framework Core
- **Frontend:** Razor Views, Bootstrap 5
- **Authentication:** ASP.NET Identity
- **Architecture:** MVC & Unit-of-Work pattern
- **Notifications:** SweetAlert2
- **Version Control:** Git & GitHub

---

## 🚀 Getting Started

### 1. Clone the repo:
```bash
git clone https://github.com/AhmedTawhed/SmartTaskManager
cd SmartTaskManager
```
### 2. Apply EF Core migrations:
```bash
dotnet ef database update
```
### 3. Run the application:
```bash
dotnet run
```

---
## 👤 Demo User
For testing, you can use the seeded user:
- **Username:** testuser
- **Password:** Test@123
- You can log in with this user to explore the task management features.
- Feel free to create your own account as well!

---

## 💡 Future Improvements
- Add task categories and labels
- Implement drag-and-drop for tasks
- Add email notifications for upcoming deadlines
- Multi-language support

---

## 🤝 Contributing
- Pull requests are welcome.
- Open an issue for suggestions or improvements.







