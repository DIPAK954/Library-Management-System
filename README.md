# 📚 Library Management System

A Library Management System built with ASP.NET Core MVC 8.0 and Entity Framework Core.
=======
The system helps librarians and students manage books, borrowing, fines, and reporting efficiently.

---

## 📸 Screenshots
### Admin Screens 
**Admin Dashboard**

![App Screenshot](<Library Management System/wwwroot/screens/Admin_Dashboard.png>)

**Book Management**

![App Screenshot](<Library Management System/wwwroot/screens/Books List.png>)
![App Screenshot](<Library Management System/wwwroot/screens/New Book.png>)
![App Screenshot](<Library Management System/wwwroot/screens/Book Details.png>)

**Issued Books**

![App Screenshot](<Library Management System/wwwroot/screens/Issued Books.png>)

**Book Requests**

![App Screenshot](<Library Management System/wwwroot/screens/Book Request List.png>)

**Users**

![App Screenshot](<Library Management System/wwwroot/screens/Student List.png>)
![App Screenshot](<Library Management System/wwwroot/screens/Student Detail.png>)

**Fines**

![App Screenshot](<Library Management System/wwwroot/screens/Admin Fine Managment.png>)

### Student Screens
**Student Dashboard**

![App Screenshot](<Library Management System/wwwroot/screens/Student Dashboard.png>)

**Browse Books**

![App Screenshot](<Library Management System/wwwroot/screens/Browse Books.png>)
![App Screenshot](<Library Management System/wwwroot/screens/Request Book.png>)

**My Books**

![App Screenshot](<Library Management System/wwwroot/screens/Student Book.png>)
![App Screenshot](<Library Management System/wwwroot/screens/Student Book Detail.png>)

**My Book Request**

![App Screenshot](<Library Management System/wwwroot/screens/Student Book Request.png>)

**My Fines**

![App Screenshot](<Library Management System/wwwroot/screens/Student Fine.png>)

### Login & Register

![App Screenshot](<Library Management System/wwwroot/screens/Library Login.png>)
![App Screenshot](<Library Management System/wwwroot/screens/Library Register.png>)

---

## 🚀 Features

### 🔑 Authentication & Roles
- User Authentication using ASP.NET Identity
- Role-based Access:
  - Admin (Librarian) – Manage books, students, fines, and reports
  - Student – Search books, borrow/return books, view fines

### 📖 Book Management
- Add, update, delete, and categorize books
- Track availability of books
- Upload book details with cover images

### 👨‍🎓 Student Management
- Manage student records
- Profile management (name, enrollment, department, phone, ID card)

### 📅 Borrowing & Returns
- Book issue and return tracking
- Due dates & late return detection
- Automatic fine calculation

### 💰 Fine Management
- Fine types:
  - Late Return
  - Lost Book

- Mark fine as paid
- Stop fine accumulation once returned

### 📊 Reports
- Daily / Monthly borrowing report
- Fine collection report
- Student activity logs

---

## 🛠 Tech Stack
- ASP.NET Core MVC 8.0
- Entity Framework Core
- SQL Server (Database)
- Bootstrap 5 + jQuery + DataTables
- ASP.NET Identity (Authentication & Roles)

---

## ⚙️ Setup Instructions

1. Clone the repository
2. Navigate into the project:
   ```bash
   cd LibraryManagementSystem
   ```

3. Update appsettings.json with your SQL Server connection string:
   ```bash
   "ConnectionStrings": {
   "DefaultConnection": "Server=.;Database=LibraryDB;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

4. Apply migrations and update the database:
   ```bash
   dotnet ef database update
   ```

5. Run the application:
   ```bash
   dotnet run
   ```

6. Default Roles:
- Admin Login
  - Email: admin@library.com
  - Password: Admin@123
- Student Login
  - Register via Sign Up form

---

## 👤 Author
 @DIPAK954

---

## 🤝 Contribution

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## 📜 License

This project is licensed under the MIT License
