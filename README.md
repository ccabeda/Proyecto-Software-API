# Sistema de Aprobación de Proyectos (Parte 3)

Este proyecto corresponde a la **Parte 3** del Trabajo Práctico de la materia **Proyecto de Software**.  
Consiste en el desarrollo completo de una aplicación con backend API REST y un frontend web que permite gestionar propuestas de proyectos y su flujo dinámico de aprobación.

## 🧱 Tecnologías utilizadas

### Backend
- **Lenguaje:** C# (.NET 6)
- **Framework:** ASP.NET Core Web API
- **ORM:** Entity Framework Core (Code-First)
- **Base de datos:** SQLite / SQL Server (configurable)
- **Documentación:** Swagger (OpenAPI)

### Frontend
- **Lenguaje:** HTML, CSS, JavaScript
- **Framework CSS:** Tailwind + Bootstrap (opcional)
- **Estilo:** Dark Mode habilitado
- **Interacción:** Consumo de la API REST desarrollada en la Parte 2

---

## 🚀 Funcionalidades Principales

### Backend
- Crear proyectos con validación de datos
- Generar automáticamente pasos de aprobación según reglas
- API RESTful con endpoints para:
  - Crear, consultar, editar y aprobar proyectos
  - Filtrar proyectos por estado, área, tipo, usuario creador, etc.
  - Aprobaciones por usuario asignado a cada paso
- Control de acceso básico con identificación de usuario (localStorage)

### Frontend
- Inicio de sesión simulado con selección de usuario
- Vista general de todos los proyectos
- Detalle completo de cada proyecto
- Creación de nuevos proyectos
- Edición de proyectos observados
- Aprobación, rechazo u observación de pasos
- Búsqueda y filtrado por título, área, tipo y estado
- Diseño responsivo e intuitivo

---

## ⚙️ Instalación y ejecución

### Backend

```bash
cd Proyecto-Software-Individual
dotnet restore
dotnet ef database update
dotnet run
