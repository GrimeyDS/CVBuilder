# Libraries and components

A list of used libraries and components, with short descriptions and links to the documentation.

# ASP.NET Core 8.0
https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-8.0

Used to build our back-end API. We will be using a typical API with controllers.

---
# Entity Framework
https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli

We use Entity Framework to interact with the underlying database. Databases in the project are set-up using a code first approach and we use migrations to update our database model.

---
# Serilog
https://serilog.net/

A simple and easy to use logging library for structured events.

---
# FluentValidation
https://docs.fluentvalidation.net/en/latest/

An easy-to-use library to build fluent validation statements. Makes validation logic a lot cleaner and easier to read (if used properly).

---
# XUnit
https://xunit.net/docs/getting-started/netcore/cmdline

A unit testing framework, which we will use to test the functionality of our application.

---
# MediatR
https://github.com/jbogard/MediatR

Mediatr is a toolset for command execution and message handling within .NET Core applications. It provides the following features:
A "mediator" pipeline for executing commands
A local message bus within .NET applications
Recommended is to look at the examples provided in the codebase if you're confused.

---
# Azure B2C
https://learn.microsoft.com/en-us/azure/active-directory-b2c/overview

For authentication we will use Azure B2C, a specialized solution for business to consumer applications.

---
# Azure App Service
https://learn.microsoft.com/en-us/azure/app-service/getting-started?pivots=stack-net

Our API will be hosted on an instance of Azure App Service.

---
# Azure keyvault
https://learn.microsoft.com/en-us/azure/key-vault/general/

Used to store secrets like the connection string to the database.

---
# Azure SQL / SQL Server
https://learn.microsoft.com/en-us/azure/azure-sql/?view=azuresql

We will use Azure SQL as our database engine. To debug locally, you can download and install the developer edition of SQL Server from this link: https://go.microsoft.com/fwlink/p/?linkid=2215158&clcid=0x409&culture=en-us&country=us

When promted, simply choose the basic install option. Also remember to install SQL Server Management Studio (SSMS) or Azure Data Studio so you can interact with the data in your database.
