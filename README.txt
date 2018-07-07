Challenge Statement

social-login is a login with facebook and google.

Prerequisites:
- Any IDE
- .NET Core SDK 2.1.4

=====================================
Development Environment
=====================================

MSSQL:
- social-login application require a app_id to login with social. Make sure to update the file "appsettings.json" file.

social-login application:
- execute these commands.

dotnet restore
dotnet build
dotnet run

- The application will be listening on http://localhost:5000