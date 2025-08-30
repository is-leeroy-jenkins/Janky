###### Janky

![](https://github.com/is-leeroy-jenkins/Janky/blob/master/Resources/Images/GIthub/JankyProject.png)

#### A C# Razor Pages Starter Kit 🚀
> *Spin up a production-ready ASP.NET Core app in minutes, not days.*


**Janky** is a bare-bones starter template for building **server-rendered web apps** with the
[Razor Pages](https://learn.microsoft.com/aspnet/core/razor-pages) paradigm on **.NET 8**.  
It rolls together the pieces you reach for on *every* green-field project—authentication,
database access, CSS tooling, testing, CI/CD—and wires them up with sensible defaults so you
can focus on *your* business logic instead of yak-shaving boilerplate.

| 🔍 Problem | 💡 Janky’s Approach |
|-----------|--------------------|
| *“I just want to scaffold a site without drowning in MVC plumbing.”* | Razor Pages by default—simple page/view-model pattern, zero controllers unless **you** add them. |
| *“Setting up Tailwind, bundling, minification, and hot-reload is tedious.”* | **Tailwind v3**, ESBuild, and `dotnet watch` are pre-wired with live-reload out of the box. |
| *“Local auth vs. JWT, EF migrations, Serilog sinks… every project starts the same!”* | One-click Identity scaffold, EF Core 8, **SQLite** dev DB, opinionated Serilog + OpenTelemetry pipeline. |
| *“CI/CD takes half a day to script.”* | GitHub Actions workflow builds, tests, lints, and pushes a distroless Docker image on every commit. |
| *“I need to demo fast, but not pay for that speed later.”* | Clean vertical slices (`Web / Core / Infrastructure`), DDD-friendly structure—prototype now, scale later. |

### When should you be Janky?
* **Internal tools** – dashboards, CRUD admin portals, staff portals  
* **Micro-SaaS MVPs** – subscription paywall ready (just hook your payment provider)  
* **Marketing sites** – static-ish pages with islands of interactivity (htmx or Fetch)  
* **Educational demos** – teach ASP.NET 8 concepts without hand-waving the plumbing  

### Philosophy
1. **Convention beats configuration** – follow the happy path, override when you must.  
2. **Plain C# > “magic”** – no hidden code-gen or black-box base classes.  
3. **Composable** – swap SQLite → PostgreSQL, Tailwind → Bootstrap, Serilog → NLog without lock-in.  
4. **Small-sharp-tools** – each NuGet package earns its keep; no megapackages.  
5. **Prod-minded** – health checks, metrics, dockerisation, and security headers baked in.

<br/>


## Table of Contents

1. [Features](https://github.com/is-leeroy-jenkins/Janky/tree/master?tab=readme-ov-file#-features)
2. [Getting Started](https://github.com/is-leeroy-jenkins/Janky/tree/master?tab=readme-ov-file#-getting-started)
3. [Folder Structure](https://github.com/is-leeroy-jenkins/Janky/tree/master?tab=readme-ov-file#-folder-structure)
4. [Configuration](https://github.com/is-leeroy-jenkins/Janky/tree/master?tab=readme-ov-file#configuration)
5. [Scripts & Tooling](https://github.com/is-leeroy-jenkins/Janky/tree/master?tab=readme-ov-file#-scripts--tooling)
6. [Roadmap](https://github.com/is-leeroy-jenkins/Janky/tree/master?tab=readme-ov-file#-roadmap)
7. [Contributing](https://github.com/is-leeroy-jenkins/Janky/tree/master?tab=readme-ov-file#-contributing)
8. [License](https://github.com/is-leeroy-jenkins/Janky/blob/master/LICENSE.txt)



## ✨ Features

| 🗂 Category               | 🛠 Goodies                                                                     |
|--------------------------|--------------------------------------------------------------------------------|
| 🏗 **Framework**         | ASP.NET Core 8 Razor Pages (no MVC controllers by default)                     |
| 🎨 **Styling**           | Tailwind CSS v3 with JIT & hot-reload                                          |
| ⚙️ **Build**             | `dotnet watch` hooked to Tailwind, ESBuild, & Live Reload (no Gulp!)          |
| 🔐 **Auth**              | Cookie & JWT via ASP.NET Core Identity *(optional)*                           |
| 💾 **Data**              | EF Core 8 + SQLite provider (swap-able) & sample repository pattern            |
| 🧪 **Testing**           | xUnit + FluentAssertions + Playwright component tests                          |
| ✅ **Validation**        | FluentValidation integration                                                   |
| 🔌 **API**               | Minimal APIs co-located with pages for JSON endpoints                          |
| 🐳 **CI/CD**             | GitHub Actions → build, test, & push distroless Docker image                  |
| 📦 **Docker**            | Multi-stage `Dockerfile` & `docker-compose` for dev                            |
| 📊 **Observability**     | Serilog + OpenTelemetry (OTLP exporter)                                        |
| 🌍 **I18n**              | Resource-based localization boilerplate                                        |
| 🔑 **Secrets**           | First-class `dotnet user-secrets` support                                      |




## 🚀 Getting Started

```bash
# 1. Create your project folder
dotnet new install Janky.Template      # 1st time only
dotnet new janky -n MyApp
cd MyApp

# 2. Restore & run
dotnet watch                           # compiles + hot-reload
```

## 🗂 Folder Structure

```src/
├─ MyApp.Web/              # Razor Pages project
│  ├─ Pages/               # .cshtml + PageModels
│  ├─ Components/          # ViewComponents & partials
│  ├─ Api/                 # Minimal API endpoints
│  └─ Assets/              # Tailwind input CSS, TS, images
├─ MyApp.Core/             # Domain & service layer
├─ MyApp.Infrastructure/   # EF Core DbContext, repositories
tests/                     # Unit + integration tests
.github/                   # CI/CD workflows
```



## Configuration

🔧 Setting | 📄 Location | 📝 Purpose
-----------|-------------|----------------
ConnectionString | appsettings*.json | DB connection (SQLite default)
Serilog | appsettings*.json | Console + OTLP exporter
FeatureFlags | appsettings.json | Toggle optional modules
Secrets | dotnet user-secrets | Safe local secrets storage


## 🏃 Scripts & Tooling

🔨 Command | 💬 Description
-----------|----------------
dotnet watch | Rebuild, Tailwind hot-reload
npm run tailwind:build | One-off CSS build (CI)
dotnet test | Run all tests
docker compose up dev | Start dev stack (DB + app)
./scripts/migrate.sh | Apply EF Core migrations in container


## 🗺 Roadmap

- Blazor Hybrid starter variant
- Azure Bicep infra as code sample
- GraphQL endpoint scaffold
- VS Code Dev Container


## 📜 [License](https://github.com/is-leeroy-jenkins/Janky/blob/master/LICENSE.txt)

- MIT © 2020 Terry D. Eppler
