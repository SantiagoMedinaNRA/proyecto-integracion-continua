# Docker - Entrega 1: Documentación Técnica

**Fecha**: Noviembre 2026  
**Entrega**: Entrega 1 - Dockerización  
**Límite**: Martes, 10 de noviembre, 23:55  
**Status**: ✅ Completada

## 📋 Índice

1. [Resumen Ejecutivo](#resumen-ejecutivo)
2. [Arquitectura](#arquitectura)
3. [Contenedores](#contenedores)
4. [Instrucciones de Uso](#instrucciones-de-uso)
5. [Verificación y Testing](#verificación-y-testing)
6. [Troubleshooting](#troubleshooting)
7. [Propiedades Verificadas](#propiedades-verificadas)

## 📝 Resumen Ejecutivo

Este documento técnico describe la implementación completa de Docker para el proyecto Potter Academy. La solución incluye:

- **3 contenedores**: Api (ASP.NET Core), Web (Blazor Server), Valkey (Cache distribuido)
- **Multi-stage builds**: Optimizados para minimizar tamaño de imágenes
- **Health checks**: Validación automática de servicios
- **Distributed caching**: Integración con Valkey 7.0 (Redis-compatible)
- **CI/CD automation**: GitHub Actions con validación en cada push
- **Environment-based config**: Configuración centralizada en `.env`

## 🏗️ Arquitectura

### Diagrama de servicios

```
┌────────────────────────────────────────────────────────────┐
│              potter-network (Docker network)               │
│                                                            │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐    │
│  │     web      │  │     api      │  │    valkey    │    │
│  │ 8001:3000    │  │ 8000:8000    │  │ 6379:6379   │    │
│  │ (Blazor)     │  │ (ASP.NET)    │  │ (Cache)     │    │
│  └──────────────┘  └──────────────┘  └──────────────┘    │
│       ↓ (http)          ↓ (http)           ↑ (Redis)      │
│       └──────────────────┴─────────────────┘              │
│                                                            │
└────────────────────────────────────────────────────────────┘
```

### Flujo de datos

1. **Usuario** accede a `http://localhost:8001` (Web)
2. **Web** realiza peticiones HTTP a `http://api:8000/api/books`
3. **Api** verifica cache en Valkey (`http://valkey:6379`)
4. **Cache HIT**: Retorna datos almacenados
5. **Cache MISS**: Consulta PotterDB, almacena en Valkey, retorna
6. **Web** renderiza resultado en navegador

## 🐳 Contenedores

### 1. Api (potter-api:1.0)

**Dockerfile**: `Api/Dockerfile` (multi-stage)

```dockerfile
# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source
COPY Api/Api.csproj .
RUN dotnet restore
COPY Api/ .
RUN dotnet publish -c Release -o /app

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:8000
EXPOSE 8000
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD dotnet /app/Api.dll GET http://localhost:8000/health
ENTRYPOINT ["dotnet", "Api.dll"]
```

**Características**:
- Build separado del runtime (multi-stage)
- Puerto interno: 8000
- Health check: GET `/health` cada 30s
- Tamaño final: ~101 MB

**Dependencias principales**:
- `StackExchange.Redis` 2.8.12 (cliente Valkey)
- `AspNetCore.HealthChecks.Redis` 9.0.0 (health check)

**Variables de entorno esperadas**:
- `ASPNETCORE_URLS`: URL del servidor (http://+:8000)
- `ApiSettings__BaseUrl`: URL base (ej: http://api:8000)

### 2. Web (potter-web:1.0)

**Dockerfile**: `Web/Dockerfile` (multi-stage)

```dockerfile
# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source
COPY Web/Web.csproj .
RUN dotnet restore
COPY Web/ .
RUN dotnet publish -c Release -o /app

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:8001
EXPOSE 8001
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD dotnet /app/Web.dll GET http://localhost:8001/health
ENTRYPOINT ["dotnet", "Web.dll"]
```

**Características**:
- Frontend Blazor Server
- Puerto interno: 8001
- Health check: GET `/health` cada 30s
- Tamaño final: ~105 MB

**Variables de entorno esperadas**:
- `ASPNETCORE_URLS`: URL del servidor (http://+:8001)
- `ApiSettings__BaseUrl`: URL del API en red Docker (http://api:8000)

### 3. Valkey (valkey:7.0)

**Imagen base**: `valkey:7.0` (imagen oficial)

**Características**:
- Redis-compatible, más eficiente en memoria
- Puerto: 6379
- Almacenamiento en memoria (ephemeral en Docker)
- Health check: `redis-cli ping`

## 📋 Instrucciones de Uso

### Prerequisitos

- Docker Desktop (Windows/Mac) o Docker Engine (Linux)
- Docker Compose 2.x
- Git

### Verificar instalación

```powershell
# Windows PowerShell
docker --version
docker compose version
```

Salida esperada:
```
Docker version 27.0+
Docker Compose version 2.x+
```

### Clonar repositorio

```powershell
git clone https://github.com/SantiagoMedinaNRA/proyecto-integracion-continua.git
cd proyecto-integracion-continua
```

### Construir imágenes

```powershell
# Construir Api
docker build -f Api/Dockerfile -t potter-api:1.0 --no-cache .

# Construir Web
docker build -f Web/Dockerfile -t potter-web:1.0 --no-cache .

# Verificar imágenes
docker images | grep potter
```

Salida esperada:
```
REPOSITORY   TAG    IMAGE ID     CREATED      SIZE
potter-api   1.0    abc123...    2 min ago    101MB
potter-web   1.0    def456...    2 min ago    105MB
```

### Configurar variables de entorno

```powershell
# Copiar template
Copy-Item .env.example .env

# Editar .env según necesidad (generalmente no requerido)
# El archivo .env.example contiene valores por defecto funcionales
```

Contenido de `.env.example`:
```env
WEB_SERVICE_NAME=web
WEB_PORT=8001
API_SERVICE_NAME=api
API_PORT=8000
VALKEY_PORT=6379
ApiSettings__BaseUrl=http://api:8000
```

### Iniciar servicios

```powershell
# Iniciar en background
docker compose up -d

# Ver logs en vivo
docker compose logs -f

# Para detener:
# docker compose down
```

Salida esperada:
```
[+] Running 4/4
 ✔ Network potter-network Created
 ✔ Container potter-valkey Started
 ✔ Container potter-api Started
 ✔ Container potter-web Started
```

### Verificar estado

```powershell
docker ps
```

Salida esperada:
```
CONTAINER ID  IMAGE          STATUS         PORTS
abc123...     potter-web:1.0 Up 30s (healthy) 8001->8001/tcp
def456...     potter-api:1.0 Up 30s (healthy) 8000->8000/tcp
ghi789...     valkey:7.0     Up 30s (healthy) 6379->6379/tcp
```

### Acceder a la aplicación

- **Frontend**: http://localhost:8001
- **API Swagger**: http://localhost:8000/swagger/index.html
- **API Health**: http://localhost:8000/health

### Ver logs

```powershell
# Todos los servicios
docker compose logs

# Solo Api con filtro CACHE
docker compose logs api | grep CACHE

# Solo Web
docker compose logs web

# Valkey
docker compose logs valkey

# Seguir logs en vivo
docker compose logs -f
```

### Detener servicios

```powershell
# Parar sin eliminar volúmenes
docker compose stop

# Parar y eliminar contenedores/red
docker compose down

# Eliminar también volúmenes
docker compose down -v
```

## ✅ Verificación y Testing

### Test 1: Verificación de contenedores (docker ps)

```powershell
docker ps
```

**Criterios de éxito**:
- 3 contenedores listados
- Todos con status "Up"
- Health status en "healthy" (después de 5s)

### Test 2: Verificación de health checks

```powershell
# Api health
curl http://localhost:8000/health

# Web health
curl http://localhost:8001/health
```

**Salida esperada**:
```json
{
  "status": "Healthy",
  "checks": {
    "redis": "Healthy"
  }
}
```

### Test 3: Verificación de conectividad Api-Valkey

```powershell
docker compose logs api | grep -E "(CACHE HIT|CACHE MISS|STORED IN VALKEY)"
```

**Salida esperada**:
```
[STORED IN VALKEY] Book 1: Harry Potter and the Philosopher's Stone
[CACHE HIT] Book 1: Harry Potter and the Philosopher's Stone
```

### Test 4: Verificación de conectividad Web-Api

1. Abrir navegador: http://localhost:8001
2. Navegar a "Books"
3. Verificar que se carga listado de libros
4. Revisar logs: `docker compose logs web` para errores de conectividad

### Test 5: Verificación de tamaño de imágenes

```powershell
docker images potter-api:1.0 --format "table {{.Size}}"
docker images potter-web:1.0 --format "table {{.Size}}"
```

**Criterios de éxito**:
- Ambas imágenes < 500 MB ✓

### Test 6: Verificación de .dockerignore

```powershell
# Reconstruir Api sin --no-cache para verificar capas
docker build -f Api/Dockerfile -t potter-api:1.0 .

# Verificar tamaño de layers
docker inspect potter-api:1.0
```

**Criterios de éxito**:
- `bin/`, `obj/` no incluidos en imagen final
- Tamaño <= 101 MB

## 🔧 Troubleshooting

### Problema: "docker-compose: command not found"

**Causa**: En sistemas Linux/CI/CD, el comando es `docker compose` (sin guion)

**Solución**:
```powershell
# ❌ Incorrecto
docker-compose up

# ✅ Correcto
docker compose up
```

### Problema: Web no muestra libros

**Causa**: Api no es accesible desde Web

**Diagnóstico**:
```powershell
# Ver logs del Web
docker compose logs web

# Entrar al contenedor Web
docker exec -it potter-web /bin/bash
curl http://api:8000/api/books
```

**Solución**: Verificar `ApiSettings__BaseUrl=http://api:8000` en `.env` y docker-compose.yml

### Problema: Health check retorna error

**Causa**: Middleware de salud no configurado en Program.cs

**Diagnóstico**:
```powershell
docker compose logs api
curl http://localhost:8000/health
```

**Solución**: Verificar que `Program.cs` incluye:
```csharp
app.MapHealthChecks("/health");
```

### Problema: Imágenes reconstruidas pero cambios no aplican

**Causa**: Docker cachea layers

**Solución**:
```powershell
# Forzar rebuild sin cache
docker build -f Api/Dockerfile -t potter-api:1.0 --no-cache .
docker build -f Web/Dockerfile -t potter-web:1.0 --no-cache .
```

### Problema: Puerto ya en uso

**Causa**: Otro servicio usa puerto 8000, 8001 o 6379

**Diagnóstico**:
```powershell
# Windows
Get-NetTCPConnection -LocalPort 8000

# Linux/Mac
lsof -i :8000
```

**Solución**: Cambiar puertos en `.env`:
```env
WEB_PORT=9001
API_PORT=9000
VALKEY_PORT=7379
```

### Problema: Cache no funciona (siempre MISS)

**Causa**: Valkey no accesible desde Api

**Diagnóstico**:
```powershell
docker exec -it potter-api dotnet

# Dentro del contenedor:
# Test conexión a Valkey
redis-cli -h valkey -p 6379 ping
```

**Solución**: Verificar que `StackExchange.Redis` está configurado con endpoint `valkey:6379`

## 📊 Propiedades Verificadas

| Propiedad | Valor | Estado |
|-----------|-------|--------|
| Contenedores | 3 (Web, Api, Valkey) | ✅ |
| Multi-stage builds | Dockerfile api + web | ✅ |
| Distributed cache | Valkey 7.0 | ✅ |
| Health checks | `/health` endpoints | ✅ |
| Tamaño Api | 101 MB | ✅ < 500MB |
| Tamaño Web | 105 MB | ✅ < 500MB |
| Network | potter-network | ✅ |
| Env config | .env.example + .env | ✅ |
| CI/CD | GitHub Actions | ✅ |
| Logs CACHE | [CACHE HIT/MISS] | ✅ |
| Conectividad Web-Api | http://api:8000 | ✅ |
| Conectividad Api-Valkey | valkey:6379 | ✅ |

## 📚 Archivos relacionados

- `Api/Dockerfile` - Build Api
- `Web/Dockerfile` - Build Web
- `Api/.dockerignore` - Excluir archivos Api
- `Web/.dockerignore` - Excluir archivos Web
- `.dockerignore` - Excluir archivos globales
- `docker-compose.yml` - Orquestación de servicios
- `.env.example` - Template de variables
- `.env` - Variables locales (no versionado)
- `.github/workflows/docker-build.yml` - CI/CD pipeline
- `Api/Program.cs` - Health check endpoint
- `Web/Program.cs` - Health check endpoint

## 📞 Contacto y Soporte

Para reportar issues o preguntas sobre Docker:
1. Revisar [Troubleshooting](#troubleshooting)
2. Revisar logs: `docker compose logs -f`
3. Contactar: Santiago Medina (@SantiagoMedinaNRA)
