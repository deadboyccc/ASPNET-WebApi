# BGwalks API Documentation

## Overview
BGwalks is an API designed to manage information about walking trails, including details about the trails themselves, their difficulty levels, and the regions they are located in. The project is built using ASP.NET Core and Entity Framework Core, and it follows a RESTful architecture.

## Technologies and Design Patterns
- **ASP.NET Core**: For building the web API.
- **Entity Framework Core**: For database interactions.
- **AutoMapper**: For object-object mapping.
- **Serilog**: For logging.
- **JWT (JSON Web Tokens)**: For authentication.
- **Repository Pattern**: For data access abstraction.
- **Dependency Injection**: For managing dependencies.
- **Swagger**: For API documentation.

## Technical Skills Required
- Proficiency in C# and .NET Core.
- Understanding of RESTful API principles.
- Experience with Entity Framework Core.
- Knowledge of JWT for authentication.
- Familiarity with AutoMapper for mapping objects.
- Experience with logging frameworks like Serilog.
- Understanding of design patterns like Repository Pattern and Dependency Injection.
- Basic knowledge of Swagger for API documentation.

## Project Structure
- **BGwalks.sln**: Solution file for the project.
- **BGwalks.API**: Main project directory.
  - **appsettings.json**: Configuration file for logging and database connections.
  - **BGwalks.API.csproj**: Project file with configurations and dependencies.
  - **Controllers**: Contains API controllers.
    - **RegionsController.cs**: Manages API endpoints for regions.
    - **WalksController.cs**: Manages API endpoints for walks.
    - **WeatherForecastController.cs**: Sample controller for weather forecasts.
  - **Data**: Contains database context and migrations.
    - **BGWalksDbContext.cs**: Database context for Entity Framework Core.
  - **Models**: Domain models representing data structures.
    - **Difficulty.cs**: Represents difficulty levels.
    - **Region.cs**: Represents geographical regions.
    - **Walk.cs**: Represents walking trails.
  - **Program.cs**: Entry point for web host configuration.
  - **Properties**: Contains project settings.
    - **launchSettings.json**: Configuration for launching the app.
  - **Repositories**: Contains repository interfaces and implementations.
    - **IRegionRepository.cs**: Interface for region repository.
    - **IWalkRepository.cs**: Interface for walk repository.
    - **SQLRegionRepository.cs**: SQL implementation of region repository.
    - **SQLWalkRepository.cs**: SQL implementation of walk repository.
  - **Mappings**: Contains AutoMapper profiles.
    - **AutoMapperProfiles.cs**: AutoMapper configuration for mapping domain models to DTOs.
  - **Middlewares**: Contains custom middleware.
    - **ExceptionHandlerMiddleware.cs**: Middleware for handling exceptions.
  - **Filters**: Contains custom filters.
    - **ValidateModelAttributes.cs**: Filter for model validation.

## API Routes
- **Regions**
  - `GET /api/regions`: Retrieve all regions.
  - `GET /api/regions/{id:guid}`: Retrieve a region by ID.
  - `POST /api/regions`: Create a new region.
  - `PATCH /api/regions/{id:guid}`: Update a region by ID.
  - `DELETE /api/regions/{id:guid}`: Delete a region by ID.
- **Walks**
  - `GET /api/walks`: Retrieve all walks.
  - `GET /api/walks/{id:guid}`: Retrieve a walk by ID.
  - `POST /api/walks`: Create a new walk.
  - `PATCH /api/walks/{id:guid}`: Update a walk by ID.
  - `DELETE /api/walks/{id:guid}`: Delete a walk by ID.

## Configuration
- **appsettings.json**: Contains configuration settings such as logging levels and database connection strings.
- **launchSettings.json**: Defines the launch settings for the application, including URLs and environment variables.

## Database
The project uses Entity Framework Core to interact with a SQL Server database. The database context (`BGWalksDbContext`) defines three main entities:
- **Difficulties**: Stores information about the difficulty levels of walks.
- **Regions**: Stores information about geographical regions.
- **Walks**: Stores information about walking trails, including references to their difficulty levels and regions.

## Work in Progress
Please note that this project is a work in progress and is not yet finished. Additional features and improvements are planned for future development.
