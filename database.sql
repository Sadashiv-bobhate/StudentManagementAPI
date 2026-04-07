 -- Create Database
  IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'StudentManagementDB')
  BEGIN
      CREATE DATABASE StudentManagementDB;
  END
  GO

  USE StudentManagementDB;
  GO

  -- Create Students Table
  IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Students')
  BEGIN
      CREATE TABLE Students (
          Id          INT IDENTITY(1,1) PRIMARY KEY,
          Name        NVARCHAR(100) NOT NULL,
          Email       NVARCHAR(150) NOT NULL,
          Age         INT NOT NULL,
          Course      NVARCHAR(100) NOT NULL,
          CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
      );
  END
  GO

  -- Create Users Table
  IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
  BEGIN
      CREATE TABLE Users (
          Id          INT IDENTITY(1,1) PRIMARY KEY,
          Username    NVARCHAR(100) NOT NULL UNIQUE,
          Password    NVARCHAR(255) NOT NULL,
          CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
      );
  END
  GO

  -- Insert default admin user
  IF NOT EXISTS (SELECT * FROM Users WHERE Username = 'admin')
  BEGIN
      INSERT INTO Users (Username, Password) VALUES ('admin', 'admin123');
  END
  GO