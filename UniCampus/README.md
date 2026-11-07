# UniCampus - Registro de actividades académicas

Proyecto del tercer corte de **Desarrollo de Aplicaciones Web II** - Corporación Universitaria Americana.
Docente: Ingeniera Cindy Fuentes.

## Integrantes y responsabilidades
| Integrante | Responsabilidad |
|---|---|
| [Nombre 1] | [p. ej. Capa de datos y script SQL] |
| [Nombre 2] | [p. ej. Negocio, Presenter y pruebas] |

## Tecnologías
C#, ASP.NET Web Forms (.NET Framework 4.8), SQL Server (LocalDB/Express) y ADO.NET con consultas parametrizadas, Git/GitHub.

## Arquitectura
Flujo: `Web Forms -> Presenter (MVP) -> Servicio de negocio -> Repositorio -> SQL Server`

| Proyecto | Capa | Contenido |
|---|---|---|
| UniCampus.Web | Presentación | Actividades.aspx, IActividadesView, ActividadesPresenter |
| UniCampus.BLL | Negocio | ActividadService, ResultadoOperacion (validaciones) |
| UniCampus.DAL | Datos | ActividadRepository (ADO.NET) |
| UniCampus.Entities | Compartida | ActividadDto |

Referencias: Web -> BLL, Entities; BLL -> DAL, Entities; DAL -> Entities. La DAL no depende de Web.
Diagrama: `docs/arquitectura.md`. Pruebas: `docs/PRUEBAS.md`.

## Requisitos
- Visual Studio 2022 o posterior con la carga de trabajo **ASP.NET y desarrollo web**
- .NET Framework 4.8 (o 4.8.1) Developer Pack
- SQL Server LocalDB (incluido con Visual Studio) o SQL Server Express

## Configuración y ejecución
1. Abrir `UniCampus.sln` en Visual Studio y compilar (Ctrl+Shift+B).
2. Establecer `UniCampus.Web` como proyecto de inicio y pulsar **F5**.
   Al arrancar, la aplicación crea la base `UniCampusDB`, las tablas y los datos de prueba si no existen
   (usa el script `database/03_BaseDatos.sql`, que también se puede ejecutar a mano en SQL Server).
3. La cadena de conexión `UniCampusDb` está en `UniCampus.Web/Web.config` (por defecto LocalDB;
   hay una opción comentada para SQL Server Express). Sin credenciales: usa autenticación integrada de Windows.
4. Si hay problemas de conexión, abrir `http://localhost:PUERTO/Diagnostico.aspx` (solo local) o revisar
   `UniCampus.Web/App_Data/errores.log`.

## Funciones implementadas (primera entrega)
- [x] Registrar actividad (flujo completo Web -> Presenter -> Negocio -> Datos -> SQL Server)
- [x] Listar actividades
- [x] Validaciones de servidor (título, fechas, cupo, estado)
- [ ] Consultar detalle
- [ ] Actualizar y eliminar/desactivar
- [ ] Búsqueda y filtros
- [ ] Inscripción con control de cupos (transacción)
- [ ] Reseñas y calificaciones
- [ ] Autenticación y roles

## Pendientes para la entrega final (23-nov-2026)
Las funciones sin marcar, más log de errores, pruebas documentadas y sustentación individual.
