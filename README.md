# Practical Project - Ukraine
.NET Core Microservices with Dapr and Tye.

## Prerequisites

- [.NET](https://dotnet.microsoft.com/download) `>=7`
- [Docker](https://www.docker.com/get-started)
- [Dapr](https://docs.dapr.io/getting-started/)
- [Tye](https://github.com/dotnet/tye/blob/main/docs/getting_started.md)

## Structure
- **Dapr**. Event Bus and Secret store.
  - **RabbitMq** pubsub.
  - Local file store.
  - **Zipkin** tracing system.
- **Tye**. Run services with one command.
  - Include **PostgreSql** database.
  - Include **Seq** — structured logs.
- **Framework**
  - **Abstractions**
  - **Core**
    - Various extensions.
    - **Mediatr** + Base Fluent Request Validator.
    - **AutoMapper** and **Serilog** packages.
  - **Dapr**
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
    - **GraphQL** Server. Annotation/Code-first mix.
    - **REST** Controller with **Swagger**
    - Event Bus subscriptions.
    - Health Checks.
    - Open Telemetry.
    - **Serilog**.
    - **Bogus**. Fake data generator.
  - **Identity Server** without UI styles.
    - Only basic controllers.
    - Seed Roles, Users, Resources and clients.
    - Migration Helper.
- **Web**. Health Checks UI.

## Coding Guidelines
- Projects use [StyleCop](https://github.com/DotNetAnalyzers/StyleCopAnalyzers) to produce style warnings. Please fix all warnings in any code you submit.

### TODO
- Identity Server Frontend
- Swagger/GraphQL Gateway
- Mail Sender
- Blazor Server/Client
- ReadOnlyDictionary.Empty (after .NET 8)

## License

This project is licensed under the [MIT License](./LICENSE).