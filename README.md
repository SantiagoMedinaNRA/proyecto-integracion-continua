# Proyecto Integracion Continua

Plataforma web interactiva basada en Harry Potter que demuestra integración continua, arquitectura desacoplada y The Standard como metodología de desarrollo.

## Equipo

- Santiago Medina Peláez

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

## 📁 Estructura del Proyecto

```
proyecto-integracion-continua/
├── Api/                    (ASP.NET Core Web API)
├── Web/                    (Blazor Server)
├── Shared/                 (DTOs, Models, Constants)
├── Tests/                  (Unit Tests)
└── .github/workflows/      (CI/CD - Entrega 1)
```

## 🚀 Cómo ejecutar

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

## 🐳 Docker - Entrega 1

### Configuración Docker

El proyecto incluye contenedores Docker completamente funcionales con orquestación mediante `docker-compose`, caché distribuido con Valkey y health checks automáticos.

#### Contenedores

| Contenedor | Imagen Base | Puerto | Función |
|-----------|------------|--------|---------|
| **api** | aspnet:10.0 | 8000 | API REST (ASP.NET Core) |
| **web** | aspnet:10.0 | 8001 | Frontend Blazor Server |
| **valkey** | valkey:7.0 | 6379 | Cache distribuido (Redis-compatible) |

#### Características

- ✅ **Multi-stage builds**: Optimización de tamaño (Api: 101 MB, Web: 105 MB)
- ✅ **Distributed caching**: Integración con Valkey mediante `IDistributedCache`
- ✅ **Health checks**: Endpoints `/health` con validación automática
- ✅ **Environment-based config**: Variables desde `.env` (`.env.example` incluido)
- ✅ **Custom network**: `potter-network` aislado
- ✅ **CI/CD automation**: GitHub Actions con validación automática

#### Cómo ejecutar con Docker

**1. Verificar requisitos**
```powershell
docker --version
docker compose version
```

**2. Construir imágenes**
```powershell
docker build -f Api/Dockerfile -t potter-api:1.0 .
docker build -f Web/Dockerfile -t potter-web:1.0 .
```

**3. Iniciar servicios**
```powershell
docker compose up -d
```

**4. Verificar estado**
```powershell
docker ps
docker compose logs -f
```

**5. Acceder a la aplicación**
- Frontend: `http://localhost:8001`
- API: `http://localhost:8000/swagger`
- Health check: `http://localhost:8000/health`

**6. Detener servicios**
```powershell
docker compose down
```

#### Variables de entorno

Ver `.env.example` para todas las variables. Principales:

```env
WEB_SERVICE_NAME=web
WEB_PORT=8001
API_SERVICE_NAME=api
API_PORT=8000
VALKEY_PORT=6379
ApiSettings__BaseUrl=http://api:8000
```

#### Verificación de caché

Logs con `[CACHE HIT]`, `[CACHE MISS]` y `[STORED IN VALKEY]` indican funcionamiento correcto:

```powershell
docker compose logs api | grep CACHE
```

#### CI/CD Pipeline

GitHub Actions (`.github/workflows/docker-build.yml`) valida automáticamente en cada push:
- Sintaxis de `docker-compose.yml`
- Compilación de imágenes Api y Web
- Tamaño de imágenes < 500 MB
- Limpieza de artefactos

## 🧩 Componentes Blazor Principales

- **BooksPage** - Listado interactivo de libros
- **CharactersPage** - Catálogo de personajes
- **SpellsPage** - Búsqueda de hechizos
- **HousesPage** - Información de casas
- **CharacterCard** - Componente reutilizable
- **SearchBar** - Búsqueda en tiempo real
