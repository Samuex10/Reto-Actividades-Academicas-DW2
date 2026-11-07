/* =====================================================================
   UniCampus - Script de base de datos (PRIMERA ENTREGA)
   IMPORTANTE: es un modelo de referencia. Si su equipo ya tiene un
   diagrama ER aprobado, ADAPTE nombres/columnas y no duplique tablas.
   Reproducible: puede ejecutarse varias veces sin duplicar datos.
   ===================================================================== */
IF DB_ID(N'UniCampusDB') IS NULL CREATE DATABASE UniCampusDB;
GO
USE UniCampusDB;
GO

IF OBJECT_ID(N'dbo.ActividadAcademica', N'U') IS NULL
CREATE TABLE dbo.ActividadAcademica (
    ActividadId  INT IDENTITY(1,1) PRIMARY KEY,
    Titulo       NVARCHAR(120)  NOT NULL,
    Descripcion  NVARCHAR(1000) NULL,
    FechaInicio  DATETIME2      NOT NULL,
    FechaFin     DATETIME2      NOT NULL,
    Lugar        NVARCHAR(150)  NULL,
    Cupos        INT            NOT NULL CONSTRAINT DF_Actividad_Cupos DEFAULT (0),
    Estado       NVARCHAR(20)   NOT NULL CONSTRAINT DF_Actividad_Estado DEFAULT ('Publicada'),
    CONSTRAINT CK_Actividad_Cupos  CHECK (Cupos >= 0),
    CONSTRAINT CK_Actividad_Fechas CHECK (FechaFin >= FechaInicio),
    CONSTRAINT CK_Actividad_Estado CHECK (Estado IN (N'Borrador', N'Publicada', N'Cancelada', N'Finalizada'))
);
GO
IF OBJECT_ID(N'dbo.Usuario', N'U') IS NULL
CREATE TABLE dbo.Usuario (
    UsuarioId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre    NVARCHAR(100) NOT NULL,
    Correo    NVARCHAR(150) NOT NULL UNIQUE,
    Rol       NVARCHAR(30)  NOT NULL
);
GO
IF OBJECT_ID(N'dbo.Inscripcion', N'U') IS NULL
CREATE TABLE dbo.Inscripcion (
    InscripcionId    INT IDENTITY(1,1) PRIMARY KEY,
    ActividadId      INT NOT NULL,
    UsuarioId        INT NOT NULL,
    FechaInscripcion DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    Estado           NVARCHAR(20) NOT NULL DEFAULT ('Activa'),
    CONSTRAINT FK_Inscripcion_Actividad FOREIGN KEY (ActividadId) REFERENCES dbo.ActividadAcademica(ActividadId),
    CONSTRAINT FK_Inscripcion_Usuario   FOREIGN KEY (UsuarioId)   REFERENCES dbo.Usuario(UsuarioId),
    CONSTRAINT UQ_Inscripcion_Actividad_Usuario UNIQUE (ActividadId, UsuarioId)
);
GO
IF OBJECT_ID(N'dbo.Resena', N'U') IS NULL
CREATE TABLE dbo.Resena (
    ResenaId      INT IDENTITY(1,1) PRIMARY KEY,
    ActividadId   INT NOT NULL,
    UsuarioId     INT NOT NULL,
    Calificacion  TINYINT NOT NULL,
    Comentario    NVARCHAR(1000) NULL,
    FechaCreacion DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT CK_Resena_Calificacion CHECK (Calificacion BETWEEN 1 AND 5),
    CONSTRAINT FK_Resena_Actividad FOREIGN KEY (ActividadId) REFERENCES dbo.ActividadAcademica(ActividadId),
    CONSTRAINT FK_Resena_Usuario   FOREIGN KEY (UsuarioId)   REFERENCES dbo.Usuario(UsuarioId)
);
GO

/* ---------- Datos de prueba ficticios (no se repiten) ---------- */
IF NOT EXISTS (SELECT 1 FROM dbo.ActividadAcademica WHERE Titulo = N'Feria de proyectos tecnológicos')
    INSERT INTO dbo.ActividadAcademica (Titulo, Descripcion, FechaInicio, FechaFin, Lugar, Cupos, Estado)
    VALUES (N'Feria de proyectos tecnológicos', N'Presentación de proyectos desarrollados por estudiantes.',
            '2026-11-18T09:00:00', '2026-11-18T13:00:00', N'Auditorio principal', 40, N'Publicada');
IF NOT EXISTS (SELECT 1 FROM dbo.ActividadAcademica WHERE Titulo = N'Taller de seguridad digital')
    INSERT INTO dbo.ActividadAcademica (Titulo, Descripcion, FechaInicio, FechaFin, Lugar, Cupos, Estado)
    VALUES (N'Taller de seguridad digital', N'Buenas prácticas para proteger información y cuentas.',
            '2026-11-20T14:00:00', '2026-11-20T17:00:00', N'Laboratorio 2', 25, N'Publicada');
IF NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE Correo = N'estudiante.demo@ejemplo.edu')
    INSERT INTO dbo.Usuario (Nombre, Correo, Rol)
    VALUES (N'Estudiante de prueba', N'estudiante.demo@ejemplo.edu', N'Estudiante');
GO

SELECT TOP (10) * FROM dbo.ActividadAcademica ORDER BY ActividadId DESC;
GO
