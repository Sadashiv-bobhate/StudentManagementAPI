  # StudentManagementAPI

  A Web API project built using ASP.NET Core 8 for managing student records.

  ## Technologies Used
  - ASP.NET Core 8
  - ADO.NET
  - SQL Server
  - JWT Authentication
  - Serilog
  - Swagger

  ## How to Setup

  1. Open SQL Server Management Studio and run `database.sql` to create the database
  2. Open `appsettings.json` and update the connection string with your server name:
     ```json
     "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=StudentManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
     ```
     Replace `YOUR_SERVER_NAME` with your SQL Server instance name (e.g. `localhost` or `DESKTOP-XYZ`)
  3. Open the solution in Visual Studio and press F5

  ## Login Details
  - Username: admin
  - Password: admin123

  ## How to Test APIs
  - Run the project
  - Open Swagger at https://localhost:{port}/swagger
  - Call `/api/Auth/login` to get the token
  - Click Authorize in Swagger and enter `Bearer {token}`
  - Then test all student endpoints

  ## API List
  - POST /api/Auth/login
  - GET /api/Student
  - GET /api/Student/{id}
  - POST /api/Student
  - PUT /api/Student/{id}
  - DELETE /api/Student/{id}