# BasicWebApi
Basic ASP.NET Core WebApi skeleton with the libs I use the most

**Target C#:** 6.0 with latest features (but I'm not using minimal APIs for the sake of maintenability)

I chose to use Clean Architecture as it keeps the things simple so the projects are divided into:
- **Domain** which holds all the models and enumerations
- **Application** which contains all the business logic and validators
- **Infrastructure** which is responsible for managing conections to databases and external (like other HTTP APIs)
- **WebApi** which is the main entry point and holds all controllers and configuration

This WebApi project uses the following libraries: (:star: those on GitHub if you haven't already)

- Basic health checks using [ASP.NET Core Diagnostics](https://github.com/aspnet/Diagnostics)
- Logging as standard [Microsoft Logging](https://github.com/aspnet/Logging/tree/master/src/Microsoft.Extensions.Logging) format with [Serilog](https://github.com/serilog/serilog)
- API Documentation using [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.WebApi)
- API Versioning with [ASP.NET MVC API Versioning](https://github.com/dotnet/aspnet-api-versioning)
- [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) for dealing with JSON in the API Documentation (it works very well with Swashbuckle)
- Model validation using [FluentValidation](https://github.com/FluentValidation/FluentValidation)
- [SonarAnalizer](https://github.com/SonarSource/sonar-dotnet) for helping me write better and safer code

As I evolve this into a more robust base microservice, I will put some more layers and features...

More libs planned such as:
- [AutoMapper](https://github.com/AutoMapper/AutoMapper) for mapping internal objects into Value Objects / responses
- [Flurl](https://github.com/tmenier/Flurl) or [Refit](https://github.com/reactiveui/refit) for making calls to external APIs
- [Jwt.Net](https://github.com/jwt-dotnet/jwt) for implementing Json Web Tokens OR [Auth0](https://github.com/auth0/auth0-aspnetcore-authentication) for better authentication management
- [Entity Framework Core](https://github.com/dotnet/efcore) to ease my database access
- [MassTransit](https://github.com/MassTransit) for making my API more event-driven (in my opinion it's simpler to use than [MediatR](https://github.com/jbogard/MediatR))

To aid my testing I plan on using:
- [xUnit](https://github.com/xunit/xunit) for flexible test writing
- [Bogus](https://github.com/bchavez/Bogus) for fixtures and simulated data
- [Moq](https://github.com/moq/moq) to create and manipulate Mocking
- [FluentAssertions](https://github.com/fluentassertions/fluentassertions) for writing better assertions on my tests
