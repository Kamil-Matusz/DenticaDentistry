# DenticaDentistry
Dental salon with the possibility of booking services and operations on services. The project uses CQRS and clean architecture. Customers can make reservations for given services, while the administrator manages services and reservations.

### Stack & Technologies:
- C# 11
- .NET 6.0
- .NET JWT
- Entity Framework Core
- PostgreSQL
- Docker

# Swagger Documentation
![](/git_images/swagger_documentation.png)

# Database Diagram
![](/git_images/database_diagram.png)

## Database
The project uses a Postgres database using Docker. Docker settings are in the docker-compose.yml file.
For the database to work properly, create a migration and apply the appropriate data in the file <b>appsettings.json</b> on line 16 <b>"connectionString"</b> change the database path to your database path.

## Defaul Admin account
| Login                 | Password   |
|-----------------------|------------|
| admin@test.com        | password   |
| johndoe@test.com      | passowrd   |


