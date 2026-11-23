# Proyecto Integración Continua

Plataforma web interactiva basada en Harry Potter que demuestra integración continua, arquitectura desacoplada y donde se desarrollarán las actividades para entregar.

## Equipo

- Santiago Medina Peláez

## 📋 Descripción

Consumidor interactivo de la API pública [PotterDB](https://docs.potterdb.com/apis/rest) con caché distribuida en Valkey, frontend Blazor Server y backend ASP.NET Core separados en contenedores Docker. La integración continua se ejecuta con Jenkins (y GitHub Actions).

## 🏗️ Tech Stack

| Componente | Tecnología |
|-----------|-----------|
| Runtime | .NET 10 |
| Frontend | Blazor Server |
| Backend | ASP.NET Core Web API |
| Caché | Valkey (compatible con Redis) |
| Pruebas | xUnit, NSubstitute, Shouldly |
| CI | Jenkins (pipeline como código) y GitHub Actions |

## 📁 Estructura del Proyecto

```
proyecto-integracion-continua/
├── Api/                    (ASP.NET Core Web API)
├── Web/                    (Blazor Server)
├── Shared/                 (DTOs, Models, Constants)
├── Tests/                  (Pruebas unitarias de Api y Web)
├── jenkins/                (Contenedor de Jenkins: Dockerfile, compose, plugins, configuración)
├── Jenkinsfile             (Pipeline de Jenkins)
├── docker-compose.yml      (Orquestación de la aplicación)
├── .env.example            (Plantilla de variables de entorno)
├── .env.ci                 (Variables usadas por los pipelines de CI)
└── .github/workflows/      (GitHub Actions)
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

**2. Crear el archivo de variables de entorno**
```powershell
Copy-Item .env.example .env
```

El archivo `.env` no se sube al repositorio (está en `.gitignore`). Ajusta los valores si lo necesitas, por ejemplo los puertos.

**3. Construir y levantar los contenedores**
```powershell
docker compose up -d --build
```

El comando construye las imágenes de Api y Web y levanta los tres servicios: `potter-web`, `potter-api` y `potter-valkey`.

**4. Verificar que los servicios están corriendo**
```powershell
docker ps
```

Los tres contenedores deben estar en estado `Up`. `potter-web` y `potter-api` tienen healthcheck y deben mostrar `(healthy)`.

**5. Acceder a la aplicación**
- Frontend: http://localhost:5001
- API: http://localhost:5000/health

**6. Detener los contenedores**
```powershell
docker compose down
```

### Variables de entorno

Ver `.env.example` para la configuración: nombres de servicios, red, puertos y URL de PotterDB.

### Pruebas unitarias

```powershell
dotnet test Tests/Api.UnitTests
dotnet test Tests/Web.UnitTests
```

## 🔧 Integración continua con Jenkins

El pipeline está definido en `Jenkinsfile` (raíz) y Jenkins corre en su propio contenedor (`jenkins/`), con .NET SDK y cliente Docker incluidos. Jenkins usa el Docker del equipo anfitrión a través de `/var/run/docker.sock`.

**1. Crear las credenciales del administrador**
```powershell
cd jenkins
Copy-Item .env.example .env
```

Edita `jenkins/.env` y define `JENKINS_ADMIN_ID` y `JENKINS_ADMIN_PASSWORD`. Este archivo no se sube al repositorio; si no existe, Jenkins no arranca.

**2. Levantar Jenkins**
```powershell
docker compose up -d --build
```

Interfaz: http://localhost:8080 (usuario y contraseña definidos en `jenkins/.env`).

**3. Crear el job**

En Jenkins: New Item → nombre `Potter-CI-Build` → tipo Pipeline.
- Definition: Pipeline script from SCM
- SCM: Git, URL `https://github.com/SantiagoMedinaNRA/proyecto-integracion-continua.git` (repositorio público, sin credenciales)
- Branch: `*/main`
- Script Path: `Jenkinsfile`
- Triggers: Poll SCM con `H/5 * * * *`

**Etapas del pipeline:** Checkout → Build (imágenes `potter-api:N` y `potter-web:N`) → Test (pruebas unitarias + prueba de integración Web → Api por nombre de servicio) → Post (limpieza y resultado).

**Disparo:** Poll SCM cada 5 minutos sobre la rama `main`.

**Importante:** la prueba de integración levanta el `docker-compose.yml` de la aplicación con los mismos nombres de contenedor. No tengas la aplicación levantada manualmente mientras corre un build.

**Detener Jenkins**
```powershell
cd jenkins
docker compose down
```
