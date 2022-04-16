# BasicWebApi
Basic ASP.NET Core WebApi skeleton with the libs I use the most

**Target C#:** 6.0 with latest features (but I'm not using minimal APIs for the sake of maintenability)

This WebApi project uses the following (**Star** those on GitHub if you haven't already)

- Basic health checks using [ASP.NET Core Diagnostics](https://github.com/aspnet/Diagnostics)
- Logging as standard [Microsoft Logging](https://github.com/aspnet/Logging/tree/master/src/Microsoft.Extensions.Logging) format with [Serilog](https://github.com/serilog/serilog)
- API Documentation with [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.WebApi)
- API Versioning with [ASP.NET MVC API Versioning](https://github.com/dotnet/aspnet-api-versioning)
- [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) for dealing with JSON in the API Documentation (it works very well with Swashbuckle)
- Model validation using [FluentValidation](https://github.com/FluentValidation/FluentValidation)

As I evolve this to a more robust nano or microservice, I will put some more layers and features...

More libs planned such as:
- [AutoMapper](https://github.com/AutoMapper/AutoMapper) for mapping internal objects into Value Objects / responses
- [Flurl](https://github.com/tmenier/Flurl) or [Refit](https://github.com/reactiveui/refit) to make calls to external APIs
- [Jwt.Net](https://github.com/jwt-dotnet/jwt) for implementing Json Web Tokens OR [Auth0](https://github.com/auth0/auth0-aspnetcore-authentication) for better authentication management
- [Entity Framework Core](https://github.com/dotnet/efcore) to ease my database access
- [MassTransit](https://github.com/MassTransit) for making my API more event-driven (simpler to use than [MediatR](https://github.com/jbogard/MediatR))

To aid my testing I plan on using:
- [xUnit](https://github.com/xunit/xunit) for flexible test writing
- [Bogus](https://github.com/bchavez/Bogus) to create my fixtures
- [Moq](https://github.com/moq/moq) to create and manipulate Mocking
- [FluentAssertions](https://github.com/fluentassertions/fluentassertions) to write better assertions for my tests
