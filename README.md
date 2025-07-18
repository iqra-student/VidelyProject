#Vidly Movie Rental System 🎥
An ASP.NET Core MVC web application for managing a movie rental platform. The system provides role-based access, allowing both Admins and Customers to perform specific tasks like managing movies, customers, rentals, and performing live searches with pagination.

##🚀 Features##
🔐 Role-Based Access Control
**Admin:**
Manage Movies (CRUD)
Manage Customers (CRUD)
View Rentals (Admin doesn't rent movies)

**Customer:**
View Available Movies (with Genre)
Create New Rental (Rent a movie by entering the movie and customer name)

##🎯 Core Functionalities##
Login & Registration System (Admin and Customer roles)
Session-Based Authentication & Custom Middleware Protection

**Movie Management (Admin)**
Add, Update, Delete Movies
Live Search & Pagination in Movie List

Customer Management (Admin)**
**
Add, Update, Delete Customers
Age Restriction Logic (18+ required for movie rental)
Live Search & Pagination in Customer List
Rental System (Customer)
Rent Movies if they exist in the system
Rental History Managed in Database
Navigation Bar
Dynamic options based on role (Admin/Customer)

##🛠️ Technologies Used##
ASP.NET Core MVC
Entity Framework Core (EF Core)
SQL Server (Local DB)
Bootstrap 5 (Teal Theme Customization)
JavaScript & jQuery (Live Search)
Session Management & Custom Middleware
LINQ with Skip & Take for Pagination

##How to Run the Project##
Clone the repo from GitHub
Open the project in Visual Studio 2022+
Set the SQL Server Connection String in appsettings.json
Run the following commands in Package Manager Console:
Update-Database
Run the project (F5)

### Admin Login###
Email: admin@vidly.com
Password: admin123

**Screens**
Login / Register Page
Admin Dashboard: Movies, Customers
Customer Dashboard: Show Movies, Rent Movie

**Note**
This is a practice project designed to understand:
ASP.NET Core MVC structure
Middleware implementation
Role-based session control
CRUD operations and validations
Real-time search with AJAX
Pagination using Skip and Take

<img width="1366" height="652" alt="image" src="https://github.com/user-attachments/assets/8b05d22a-559f-40aa-8dc7-6ad66640d594" />
<img width="1366" height="650" alt="image" src="https://github.com/user-attachments/assets/bf284422-8b0a-4070-9cf0-0130c868b69c" />
<img width="1364" height="650" alt="image" src="https://github.com/user-attachments/assets/c29f5e29-1ea1-4a47-bef8-332c8c72db27" />
<img width="1366" height="702" alt="image" src="https://github.com/user-attachments/assets/65b6c133-f70c-4924-807a-7466cc9d37e9" />
<img width="1366" height="653" alt="image" src="https://github.com/user-attachments/assets/5bb095e2-6af7-4b9c-82db-6379d9f73069" />
<img width="1362" height="673" alt="image" src="https://github.com/user-attachments/assets/9f2e1a7f-adea-47a8-a316-7ea160d05f29" />
<img width="1366" height="652" alt="image" src="https://github.com/user-attachments/assets/bcb42155-0d3e-4f28-8ee0-98e68c748cf5" />
<img width="1366" height="701" alt="image" src="https://github.com/user-attachments/assets/6cc6d6ba-385a-49d9-b42f-54377c74639c" />
<img width="1366" height="703" alt="image" src="https://github.com/user-attachments/assets/ed8ba045-cc35-4742-a781-e0dec0be4997" />

