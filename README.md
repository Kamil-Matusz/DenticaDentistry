# DenticaDentistry
Dental salon with the possibility of booking services and operations on services. The project uses CQRS and clean architecture. Customers can make reservations for given services, while the administrator manages services and reservations.

### Stack & Technologies:
- C# 11
- .NET 6.0
- .NET JWT
- Entity Framework Core
- PostgreSQL
- Docker
- MailCatcher

# Swagger Documentation
![](/git_images/swagger_documentation.png)
![](/git_images/swagger_documentation2.png)

# Database Diagram
![](/git_images/database_diagram.png)

## Database
The project uses a Postgres database using Docker. Docker settings are in the docker-compose.yml file.
For the database to work properly, create a migration and apply the appropriate data in the file <b>appsettings.json</b> on line 16 <b>"connectionString"</b> change the database path to your database path.

## Docker
The project uses docker images:
- Postgres: A postgres database image that stores application data
- MailCatcher: capturing and displaying emails in the development environment

## Defaul accounts
| Login                 | Password   | Role |
|-----------------------|------------|------|
| admin@test.com        | password   | admin|
| johndoe@test.com      | passowrd   | user |

## Health Checks
The application includes a Health Check mechanism to ensure the health and availability of various components.</br>
Checked components :
- application operations
- database connection
HealthChecks run under the endpoint:</br>
<b>"healthCheckStatus"</b>




