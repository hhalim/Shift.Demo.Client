# Shift.Demo.Client
The client demo works best with the [Shift.Demo.Server](https://github.com/hhalim/Shift.Demo.Server) console app.

## Quick Start
Install Redis for windows [Redis-x64-<version>.msi](https://github.com/MSOpenTech/redis/releases) package.

Or to use the SQL Server:
- Run the sql script to create Shift database in [/setup/create_db.sql](https://github.com/hhalim/Shift.Demo.Client/blob/master/setup/create_db.sql). 
- If you want to use Redis cache, setup and create a Redis instance. 

Open this project solution in Visual Studio, update the App.config connection string and cache.

```
<connectionStrings>
    <!-- LOCAL SQL 
    <add name="ShiftDBConnection" connectionString="Data Source=localhost\SQL2014;Initial Catalog=ShiftJobsDB;Integrated Security=SSPI;" providerName="System.Data.SqlClient" />
    -->
    <add name="ShiftDBConnection" connectionString="localhost:6379" providerName="System.Data.Redis" />
</connectionStrings>

<appSettings>
    <!-- 
    <add key="StorageMode" value="mssql" />
    -->
    <add key="StorageMode" value="redis" />
    
    <!-- Azure Cache 
    <add key="UseCache" value="true" />
    <add key="CacheConfigurationString" value="localhost:6379" />
    -->
</appSettings>
```
- Build and run the console demo app.
- To actually run jobs added by this demo client app, build and run the [Shift.Demo.Server](https://github.com/hhalim/Shift.Demo.Server) console app too.


## Menu
```
Shift Client Demo
1. Add a Hello World job.
2. Show progress for Hello World job(s).
3. Send 'STOP' command to Hello World job(s).
4. Reset Hello World job(s).
5. Delete Hello World job(s).
6. Exit.
Press escape (6) key to exit.
```

The 1 key insert a simple Hello World job in to Shift queue. Multiple jobs can be added pressing the 1 key multiple times. The 2 key show simple progress for existing jobs added by the client app. Use 6 key to exit the demo and automatically clean up and delete jobs added by the client app.

