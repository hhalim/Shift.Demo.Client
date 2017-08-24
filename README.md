# Shift.Demo.Client
The client demo works best with the [Shift.Demo.Server](https://github.com/hhalim/Shift.Demo.Server) console app.

## Quick Start
Install Redis for windows [Redis-x64-<version>.msi](https://github.com/MSOpenTech/redis/releases) package.

Or to use the SQL Server, first run the sql script to create Shift database in [/setup/create_db.sql](https://github.com/hhalim/Shift.Demo.Client/blob/master/setup/create_db.sql). 

Open this project solution in Visual Studio 2015, update the App.config connection string and cache.

```
  <connectionStrings>
    <!-- LOCAL SQL 
    <add name="ShiftDBConnection" connectionString="Data Source=localhost\SQL2014;Initial Catalog=ShiftJobsDB;Integrated Security=SSPI;" providerName="System.Data.SqlClient" />
    <add name="ShiftDBConnection" connectionString="mongodb://localhost" providerName="MongoDB" />
    <add name="ShiftDBConnection" connectionString="https://localhost:8081/" providerName="DocumentDB" />
    -->
    <add name="ShiftDBConnection" connectionString="localhost:6379" providerName="Redis" />
  </connectionStrings>

  <appSettings>
    <!-- 
    <add key="StorageMode" value="mssql" />
    <add key="StorageMode" value="mongo" />
    <add key="StorageMode" value="documentdb" />
    <add key="DocumentDBAuthKey" value="C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==" />
    -->
    <add key="StorageMode" value="redis" />

  </appSettings>
```

- Build and run the console demo app.
- To actually run jobs added by this demo client app, build and run the [Shift.Demo.Server](https://github.com/hhalim/Shift.Demo.Server) console app too.


## Menu
```
Shift Client Demo
1. Add a 'Hello World!' job.
2. Show progress for jobs.
3. Send 'STOP' command.
4. Send 'PAUSE' command.
5. Send 'CONTINUE' command.
6. Reset all jobs.
7. Delete jobs.
8. Exit.
Press (8) key to exit.
```

The #1 key insert a simple job to Shift queue. Multiple jobs can be added pressing the #1 key multiple times. The #2 key show simple progress for existing jobs added by the client app. Use #8 key to exit the demo and automatically clean up and delete jobs added by the client app.

