# Proyecto Integración Continua

Plataforma web interactiva basada en Harry Potter que demuestra integración continua, arquitectura desacoplada y donde se desarrollarán las actividades para entregar.

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
| Cache | Valkey 7.0 |

## 📁 Estructura del Proyecto

```
proyecto-integracion-continua/
├── Api/                    (ASP.NET Core Web API)
├── Web/                    (Blazor Server)
├── Shared/                 (DTOs, Models, Constants)
├── Tests/                  (Unit Tests)
├── docker-compose.yml      (Orquestación Docker)
├── .env.example            (Variables de entorno)
└── .github/workflows/      (CI/CD)
```

## 🚀 Cómo ejecutar con Docker

### Requisitos

- Docker Desktop o Docker Engine
- Docker Compose 2.x
- Git

### Pasos para ejecutar

**1. Clonar el repositorio**
```powershell
git clone https://github.com/SantiagoMedinaNRA/proyecto-integracion-continua.git
cd proyecto-integracion-continua
```

**2. Construir las imágenes Docker**
```powershell
docker build -f Api/Dockerfile -t potter-api:1.0 .
docker build -f Web/Dockerfile -t potter-web:1.0 .
```

**3. Levantar los contenedores**
```powershell
docker compose up -d
```

El comando levanta automáticamente los tres servicios: `potter-web`, `potter-api` y `potter-valkey`.

**4. Verificar que los servicios están corriendo**
```powershell
docker ps
```

Los tres contenedores (`potter-web`, `potter-api`, `potter-valkey`) deben estar en status `Up` con healthcheck `(healthy)`.

**5. Acceder a la aplicación**
- Frontend: http://localhost:5001

**6. Detener los contenedores**
```powershell
docker compose down
```

### Variables de entorno

Ver `.env.example` para la configuración. El archivo `.env` se crea automáticamente desde el ejemplo.
