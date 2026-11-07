USE UniCampusDB;
-- Ver las últimas actividades registradas (use esto para las capturas)
SELECT TOP (10) ActividadId, Titulo, FechaInicio, FechaFin, Lugar, Cupos, Estado
FROM dbo.ActividadAcademica ORDER BY ActividadId DESC;
-- Verificar que las tablas siguen existiendo después de la prueba de inyección SQL
SELECT name FROM sys.tables ORDER BY name;
-- Verificar que no hay cupos inválidos
SELECT COUNT(*) AS CuposInvalidos FROM dbo.ActividadAcademica WHERE Cupos < 0;
