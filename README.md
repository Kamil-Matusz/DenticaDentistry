# DenticaDentistry
Dental salon with the possibility of booking services and operations on services. The project uses CQRS and clean architecture. Customers can make reservations for given services, while the administrator manages services and reservations.

### Stack & Technologies:
- C# 11
- .NET 6.0
- .NET JWT
- FluentValidation
- Entity Framework Core
- PostgreSQL
- Docker
- MailCatcher

# Swagger UI
![](/git_images/swagger_documentation.png)
![](/git_images/swagger_documentation2.png)

# Swagger Documentation
The documentation in yaml format is located in the <b>swagger-documentatio.yaml</b> file<br/>
You can run it on the: <b>https://editor.swagger.io/</b> which will allow you to display it as Swagger UI

# Database Diagram
![](/git_images/database_diagram.png)

# Database
The project uses a Postgres database using Docker. Docker settings are in the <b>docker-compose.yml</b> file.
For the database to work properly, create a migration and apply the appropriate data in the file <b>appsettings.json</b> on line 16 <b>"connectionString"</b> change the database path to your database path.

## Docker
The project uses docker images:
- Postgres: A postgres database image that stores application data
- MailCatcher: capturing and displaying emails in the development environment

To create docker container type the following command:

```
docker-compose up -d
```

To run projectL
```
cd cd DenticaDentistry/DenticaDentistry.Api
dotnet run
```

## Defaul accounts
| Login                 | Password   | Role |
|-----------------------|------------|------|
| admin@test.com        | password   | admin|
| johndoe@test.com      | password   | user |

## Health Checks
The application includes a Health Check mechanism to ensure the health and availability of various components.HealthChecks run under the endpoint:</br>
<b>"healthCheckStatus"</b></br>
Checked components :
- application operations
- database connection</br>




