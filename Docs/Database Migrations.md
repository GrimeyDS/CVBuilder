# Using EF Core Migrations

[< go back](../readme.md)

## Introduction
EF Core Migrations are used to create a database from the model for the first time, to update the database schema when the model changes, and to apply data changes. Migrations can be used to create a database from scratch, to create a new version of a database, or to apply changes to a database.

## Prerequisites
1. Install the `Microsoft.EntityFrameworkCore.Tools` package. This package provides the `dotnet ef` command-line tool, which is used to create and apply migrations.
    ```shell
    dotnet tool install --global dotnet-ef --version 8.*
    ```
2. Have a valid connection string in the `appsettings.json` or `appsettings.development.json` for the DbContext you want to work with.
   e.g. `"DefaultConnection": "Server=localhost,1400;Database=CVBuilder;User Id=SA;Password=CV!123!Build;TrustServerCertificate=True;"`  

## Migrations and git
Migrations are very handy, but they can also be a source of problems when working with a team. When a migration is created, it is added to the project and committed to the repository. However, if someone else adds a migration without you having pushed your changes, this can lead to conflicts and other issues.

That's why it is important to follow these guidelines:
1. **Always pull before creating a migration.** This will ensure that you have the latest migration files and that you are not creating a migration that will conflict with someone else's.
2. **Communicate with your team.** If you are going to create a migration, let your team know so they don't create one at the same time.
3. **Commit your migrations.** When you create a migration, commit it as soon as possible. This will allow your team to apply your migration and avoid conflicts.

## Creating and Applying Migrations
⚠️ _All the scripts below must be run from the root of the solution, in this case the `CV-Builder` folder._ ⚠️

### Creating a Migration

To create a migration, use the `dotnet ef migrations add` command. This command creates a new migration with the changes that have been made to the model since the last migration was created. The migration is created as a C# class that can be used to apply the changes to the database.

```shell
dotnet ef migrations add <migration name> --project <dbcontext project name> --startup-project <startup project name> -o <output folder>
```
Because we are using a multi-project solution, we need to specify the project that contains the `DbContext` and the project that contains the `Startup` class. We can also specify the output folder for the migration files.

**Example:** If we were to run the command to create a migration named `Initial` for the `UserDbContext` in the `CVBuilder.Users` project, we would use the following command:
```shell
dotnet ef migrations add Initial --project CVBuilder.Users --startup-project CVBuilder -o Data\Migrations
```
Notice that we specified the output folder for the migration files using the `-o` option to keep the migration files organized under the `Data` folder where all the other database-related files are located.

### Applying a Migration
To apply all migration to the database at once use `.\migration-script.ps1` or `npm run migrate` in the [root](../).

To apply a single migration to the database, use the `dotnet ef database update` command. This command applies the changes in the migration to the database model.
```shell
dotnet ef database update --project <dbcontext project name> --startup-project <startup project name>
```
**Example:** The following command would apply all migrations that haven't been applied before for the `UserDbContext` in the `CVBuilder.Users` project.
```shell
dotnet ef database update --project CVBuilder.Users --startup-project CVBuilder
