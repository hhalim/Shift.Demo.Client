# Shift.Demo.Client
The client demo works best with the [Shift.Demo.Server](https://github.com/hhalim/Shift.Demo.Server) console app.

Before running this app, ensure that at the database storage is set up correctly and change the App.config file connection string.
```
<add name="ShiftDBConnection" connectionString="Data Source=localhost\SQL2014;Initial Catalog=ShiftJobsDB;Integrated Security=SSPI;" providerName="System.Data.SqlClient" />
```

If you like to use Redis cache, change the cache App.config to the Redis configuration.
```
<add key="UseCache" value="true" />
<add key="CacheConfigurationString" value="localhost:6379"/>
```

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

The 1. add job command insert a simple Hello World job in to Shift queue. Multiple jobs can be added pressing the 1. key multiple times. The 2. key show simple progress for existing jobs added by the client app. Use 6. key to exit the demo and automatically clean up and delete all jobs.

To run jobs added in Shift queue, run the [Shift.Demo.Server](https://github.com/hhalim/Shift.Demo.Server) console app.
