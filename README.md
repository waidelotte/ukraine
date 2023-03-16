# Practical Project - Ukraine
.NET Core Microservices with Dapr and Tye.

[![SWUbanner](https://raw.githubusercontent.com/vshymanskyy/StandWithUkraine/main/banner-direct-single.svg)](https://github.com/vshymanskyy/StandWithUkraine/blob/main/docs/README.md)

## Prerequisites

- [.NET](https://dotnet.microsoft.com/download) `>=7`
- [Docker](https://www.docker.com/get-started)
- [Dapr](https://docs.dapr.io/getting-started/)
- [Tye](https://github.com/dotnet/tye/blob/main/docs/getting_started.md)

## Structure
- **[Dapr](https://dapr.io/)**. Microservice Building Blocks.
  - **[RabbitMq](https://www.rabbitmq.com/)** pubsub.
  - Local file secret store.
- **[Tye](https://github.com/dotnet/tye)**. Run services with one command.
- **[Docker Support](https://docs.docker.com/)**
- **[PostgreSQL](https://www.postgresql.org/)** database.
- **[Seq](https://datalust.co/seq)** structured logs.
- **[Zipkin](https://zipkin.io/)** tracing system.
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
    - Dapr Email Service.
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
  - **Example Registrar** - Processes the registration Event.
  - **Example Mail** - Confirmation of registration by Email using the **[MailDev](https://github.com/maildev/maildev)**.
  - **[Identity Server](https://duendesoftware.com/products/identityserver)** without UI styles.
    - Only basic controllers.
    - Seed Roles, Users, Resources and clients.
    - Migration Helper.
- **Web**. Health Checks UI.
- **Gateway**
  - GraphQL Gateway (Schema Stitching)

## Coding Guidelines
- Projects use [StyleCop](https://github.com/DotNetAnalyzers/StyleCopAnalyzers) to produce style warnings. Please fix all warnings in any code you submit.

### TODO
- Identity Server Frontend
- Blazor Server/Client
- ReadOnlyDictionary.Empty (after .NET 8)

## License

This project is licensed under the [MIT License](./LICENSE).


## Give a Star! :star:

If you liked this project or if it helped you, please give a star :star: for this repository. Thank you!