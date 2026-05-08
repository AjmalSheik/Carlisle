CARLISLE - DLM API 

Overview

* .NET Web API built using Clean Architecture
* Focus on separation of concerns and maintainability

Architecture

* API Layer: Controllers
* Application Layer: MediatR, business logic
* Domain Layer: Entities
* Infrastructure Layer: Database and external services

Technologies Used

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server (Azure SQL)
* MediatR (CQRS)
* FluentValidation
* Serilog

Features

* Clean Architecture implementation
* CQRS using MediatR
* Request validation using FluentValidation
* Global exception handling middleware
* Logging using Serilog
* Entity Framework Core for database operations
* Azure SQL Database integration
* Database project added for schema tracking

Database

* Azure SQL Database is already configured
* No need to change connection string

How to Run

* Open solution in Visual Studio
* Run

Solution Structure

* API
* Application
* Domain
* Infrastructure
* Database Project

Hosted API


 Swagger URL: **[http://carlisle-ajmal-env.eba-ypthxjp7.eu-north-1.elasticbeanstalk.com/swagger/index.html](http://carlisle-ajmal-env.eba-ypthxjp7.eu-north-1.elasticbeanstalk.com/swagger/index.html)**

 Frontend

Built using Next.js
Components structured into reusable components
Hosted in AWS

* **http://carlisle-ajmal.s3-website.eu-north-1.amazonaws.com/**

Notes
* i used my personal aws account and azure sql for hosting
