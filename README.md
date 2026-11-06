# Potter CI Academy Platform

Plataforma web interactiva basada en Harry Potter que demuestra integración continua, arquitectura desacoplada y The Standard como metodología de desarrollo.

## Equipo

- [Nombre 1]
- [Nombre 2]

## 📋 Descripción

Consumidor interactivo de la API pública [PotterDB](https://docs.potterdb.com/apis/rest) con caché en memoria, frontend Blazor Server y backend ASP.NET Core separados en contenedores Docker.

## 🏗️ Tech Stack

| Componente | Tecnología |
|-----------|-----------|
| Runtime | .NET 10 |
| Frontend | Blazor Server |
| Backend | ASP.NET Core Web API |
| Testing | xUnit 3, NSubstitute, Shouldly |
| Logging | Serilog |
| Resilience | Microsoft.Extensions.Resilience |
| Serialization | System.Text.Json |

## 📁 Estructura del Proyecto

```
proyecto-integracion-continua/
├── Api/                    (ASP.NET Core Web API)
├── Web/                    (Blazor Server)
├── Shared/                 (DTOs, Models, Constants)
├── Tests/                  (Unit Tests)
└── .github/workflows/      (CI/CD - Entrega 1)
```

## 🚀 Cómo ejecutar (Vainilla)

### Requisitos

- .NET 10 SDK
- PowerShell o Terminal compatible

### Instalar dependencias

```powershell
cd proyecto-integracion-continua
dotnet restore
```

### Compilar

```powershell
dotnet build
```

### Ejecutar tests

```powershell
dotnet test
```

### Ejecutar API

```powershell
cd Api
dotnet run --urls "https://localhost:5000;http://localhost:5000"
```

La API estará disponible en: `https://localhost:5000`

### Ejecutar Blazor (en otra terminal)

```powershell
cd Web
dotnet run --urls "https://localhost:5001;http://localhost:5001"
```

Acceder a: `https://localhost:5001`

## 🐳 Cómo ejecutar (Docker - Entrega 1)

```bash
docker compose up --build
```

Acceder a: `http://localhost:3000` (Blazor)
API en: `http://api:5000` (dentro de la red Docker)

## 📡 Endpoints API

| Endpoint | Método | Descripción |
|----------|--------|-------------|
| `/api/books` | GET | Listar todos los libros |
| `/api/books/{id}` | GET | Detalle de un libro |
| `/api/characters` | GET | Listar personajes |
| `/api/characters/{id}` | GET | Detalle de un personaje |
| `/api/spells` | GET | Listar hechizos |
| `/api/potions` | GET | Listar pociones |
| `/api/houses` | GET | Listar casas |
| `/api/cache/status` | GET | Estado del caché |

## 🧩 Componentes Blazor Principales

- **BooksPage** - Listado interactivo de libros
- **CharactersPage** - Catálogo de personajes
- **SpellsPage** - Búsqueda de hechizos
- **HousesPage** - Información de casas
- **CharacterCard** - Componente reutilizable
- **SearchBar** - Búsqueda en tiempo real

## ✅ Estado del Proyecto

- [ ] Vainilla funcional
- [ ] Docker (Entrega 1 - Semana 3)
- [ ] Jenkins (Entrega 2 - Semana 5)
- [ ] Travis CI + Codeship (Entrega 3 - Semana 7)

## 📚 Documentación Adicional

- [Spec General](.kiro/specs/spec-general.md)
- Documentación de entregas: `documentation/` (fuera del repo)

## 🔗 Referencias

- [PotterDB API](https://docs.potterdb.com/apis/rest)
- [The Standard](https://github.com/hassanhabib/The-Standard)
- [Blazor Documentation](https://learn.microsoft.com/en-us/aspnet/core/blazor)
