# BGwalks API Documentation

## Overview
BGwalks is an API designed to manage information about walking trails, including details about the trails themselves, their difficulty levels, and the regions they are located in. The project is built using ASP.NET Core and Entity Framework Core, and it follows a RESTful architecture.

## Project Structure
- **BGwalks.sln**: Solution file for the project.
- **BGwalks.API**: Main project directory.
  - **appsettings.json**: Configuration file for logging and database connections.
  - **BGwalks.API.csproj**: Project file with configurations and dependencies.
  - **Controllers**: Contains API controllers.
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