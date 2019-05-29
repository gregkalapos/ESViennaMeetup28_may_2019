# Observability with Elastic - Slides and Demo

Talk given at 28. May 2019 at the Elasticsearch user group in Vienna.

 
Slides: 
- [Slides.key](Slides.key) and [Slides.pptx](Slides.pptx).

## Demo setup

The [docker-compose.yml](docker-compose.yml) file starts the sample application and Elasticsearch + Kibana + APM server.

The sample application consists of 2 ASP.NET Core services and a MySql database. All it does is that it reads a static value (10mio) from the database and retrieves the EUR/USD currency rate from fixer.io and shows the USD value of 10mio EUR.

The app randomly throws exceptions and generates CPU load.

The elastic APM agent is included in the application and everything is captured, so you can observe the application.
This uses very early bits of the .NET Elastic APM Agent, which is a custom build; for simplicity all agent dlls are copied to the application. Please use [our NuGet packages](https://www.nuget.org/packages?q=Elastic.apm) for real setups.

Starting the sample:

```
docker-compose up 
```

Generating load on the sample app:

```
http://localhost:5000
```

Access Kibana with all the captured data:

```
http://localhost:5601
```
