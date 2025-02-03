# Getting Started

### Set up the database

1. To run the application locally, make sure you have the docker installed.
https://www.docker.com/

2. Once docker is installed, download the latest MSSQL image.

    ```powershell
    docker pull mcr.microsoft.com/mssql/server
    ```

3. Next, spin up a docker container with the following command.

    ```powershell
    docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=CV!123!Build" -p 1400:1433 -d mcr.microsoft.com/mssql/server:2022-latest
    ```

    This will create an MSSQLserver instance you can connect to on **localhost:1433** with username **sa** and password **CV!123!Build**.

    The correct connectionstring is already present in **appsettings.Development.json**.

4. Connect to your MSSQL instance and create a new Database named **CVBuilder**.

5. Finally, apply the migrations. See **[Docs > DatabaseMigrations.md](./Docs/Database%20Migrations.md)** on how to do this.

    Once migrations have been installed you can run the application locally.

### Miro Flowchart
https://miro.com/app/board/uXjVLEPrjJs=/?share_link_id=475833490818
