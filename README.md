# BasicWebAPI
 
- [x] The solution should be written in C# using .NET 8
- [x]	Connect the API with the SQL database.
- [x]	Use code first and automatic migrations to create the database structure, so the application will be ready to install.
- [x]	Create CRUD API operations for each table in the database.
- [x]	Create the web application with one of the following architectures: Clean architecture with Domain Driven Design, Onion architecture, or Vertical Slices.
- [x]	Write an API that will return each contact with the company that is working and the country name.
- [x]	FilterContacts(int countryId, int companyId) - List all contacts by countryId or companyId or both.
- [x]	Use swagger to get API UI.
- [x]	Unit tests
- [x]	Logging and Error handling
- [x]	Document the API in README.md 


Bonus

- [x]	Use lambda expressions.
- [x]	Use design patterns. (Options, Factory, Singleton)
- [x]	Use pagination (skip)

## Overview
BasicWebAPI is a .NET 8 Web API application that implements CRUD operations for managing companies, contacts, and countries. It follows Clean Architecture principles and uses Entity Framework Core for database operations.

## Features
- CRUD operations for Companies, Contacts, and Countries
- Filtering contacts by country and company
- Automatic database migrations
- Swagger UI for API documentation and testing
- Logging and error handling
- AutoMapper for object mapping

## Technologies
- .NET 8
- Entity Framework Core 8
- AutoMapper
- Swagger / OpenAPI
- SQL Server

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server

### Installation
1. Clone the repository
```git clone https://github.com/najdovskim/BasicWebAPI.git```
2. Open the project
3. Update the connection string in `src/BasicWebAPI.API/appsettings.json`
4. Run the application
## API Endpoints
![Screenshot 2025-03-17 220727](https://github.com/user-attachments/assets/c8b3f3a0-1b05-43f0-bac5-bb4985381576)
