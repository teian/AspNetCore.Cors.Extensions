# AspNetCore.Cors.Extensions

[![Nuget](https://img.shields.io/nuget/v/AspNetCore.Cors.Extensions)](https://www.nuget.org/packages/AspNetCore.Cors.Extensions)
[![NuGet](https://img.shields.io/nuget/dt/AspNetCore.Cors.Extensions.svg)](https://www.nuget.org/packages/AspNetCore.Cors.Extensions/)


## Usage

### Default

In appsettings.json

```json
{
  "Cors": {
    "default": {
      "Methods": [ "GET", "POST" ],
      "Origins": [ "http://www.example.com" ]
    },
    "AllowAll": {
      "Headers": ["*"],
      "Methods": ["*"],
      "Origins":  ["*"],
      "SupportsCredentials": false
    }
  }
}
```

Note: See [CorsPolicy](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.cors.infrastructure.corspolicy) documentation to see which properties to use

Note: Setting `SupportsCredentials` with a Wildcard (*) Origin is not supported by the specification and will not work!        

Startup.cs
```csharp
public void ConfigureServices(IServiceCollection services)
{    
    //Replaces the use of services.AddCors()
    services.AddCorsPolicies(Configuration);
    
    /// ...

    // Add framework services.
    services.AddMvc();

    /// ...
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{   
    if (env.IsDevelopment())
    {
        app.UseCors("AllowAll");
    }
    else
    {
        app.UseCors();
        // or 
        app.UseCors("<my named cors policy>");
    }

    /// ...

    app.UseMvc();
}
```      

