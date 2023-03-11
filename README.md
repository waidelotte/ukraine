# Practical Project - Ukraine
.NET Core Microservices with Dapr and Tye.

## Prerequisites

- [.NET](https://dotnet.microsoft.com/download) `>=7`
- [Docker](https://www.docker.com/get-started)
- [Dapr](https://docs.dapr.io/getting-started/)
- [Tye](https://github.com/dotnet/tye/blob/main/docs/getting_started.md)

## Structure
- **[Dapr](https://dapr.io/)**. Event Bus and Secret store.
  - **[RabbitMq](https://www.rabbitmq.com/)** pubsub.
  - Local file store.
  - **[Zipkin](https://zipkin.io/)** tracing system.
- **[Tye](https://github.com/dotnet/tye)**. Run services with one command.
  - Include **[PostgreSQL](https://www.postgresql.org/)** database.
  - Include **[Seq](https://datalust.co/seq)** — structured logs.
- **Framework**
  - **Abstractions**
  - **Core**
    - Various extensions.
    - **[Mediatr](https://github.com/jbogard/MediatR)** + Base **[Fluent](https://github.com/FluentValidation)** Request Validator.
    - **[AutoMapper](https://automapper.org/)** and **[Serilog](https://serilog.net/)** packages.
    - **[Guard Clauses](https://github.com/ardalis/GuardClauses)** + Validation Guard
  - **[Dapr](https://dapr.io/)**
    - Dapr Event Bus.
    - Dapr Health Checks.
    - Dapr Secret store.
  - **EF Core**
    - Base Repository with projections.
    - Specification Repository.
    - Unit of Work.
- **Services**
  - **Example** (DDD/CQRS/ES)
    - Authorization.
    - JWT Authentication.
    - **[GraphQL](https://graphql.org/)** Server. Annotation/Code-first mix.
    - **REST** Controller with **[Swagger](https://swagger.io/)**
    - Event Bus subscriptions.
    - Health Checks.
    - **[Open Telemetry](https://opentelemetry.io/)**.
    - **[Serilog](https://serilog.net/)**.
    - **[Bogus](https://github.com/bchavez/Bogus)**. Fake data generator.
  - **[Identity Server](https://duendesoftware.com/products/identityserver)** without UI styles.
    - Only basic controllers.
    - Seed Roles, Users, Resources and clients.
    - Migration Helper.
- **Web**. Health Checks UI.

## Coding Guidelines
- Projects use [StyleCop](https://github.com/DotNetAnalyzers/StyleCopAnalyzers) to produce style warnings. Please fix all warnings in any code you submit.

### TODO
- Identity Server Frontend
- Mail Sender
- Blazor Server/Client
- ReadOnlyDictionary.Empty (after .NET 8)
- Docker support

## License

This project is licensed under the [MIT License](./LICENSE).