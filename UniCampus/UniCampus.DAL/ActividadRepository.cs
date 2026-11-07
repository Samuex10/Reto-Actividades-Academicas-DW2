using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using UniCampus.Entities;

namespace UniCampus.DAL
{
    /// <summary>Acceso a datos de actividades. Solo ADO.NET con consultas parametrizadas.</summary>
    public class ActividadRepository
    {
        private readonly string _connectionString;

        public ActividadRepository()
        {
            var settings = ConfigurationManager.ConnectionStrings["UniCampusDb"];
            if (settings == null)
                throw new ConfigurationErrorsException("Falta la cadena de conexión 'UniCampusDb' en Web.config.");
            _connectionString = settings.ConnectionString;
        }

        /// <summary>Inserta una actividad y devuelve el Id generado.</summary>
        public int Insertar(ActividadDto a)
        {
            const string sql = @"
                INSERT INTO dbo.ActividadAcademica
                    (Titulo, Descripcion, FechaInicio, FechaFin, Lugar, Cupos, Estado)
                OUTPUT INSERTED.ActividadId
                VALUES
                    (@Titulo, @Descripcion, @FechaInicio, @FechaFin, @Lugar, @Cupos, @Estado);";

            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@Titulo", SqlDbType.NVarChar, 120).Value = a.Titulo;
                cmd.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 1000).Value =
                    string.IsNullOrWhiteSpace(a.Descripcion) ? (object)DBNull.Value : a.Descripcion;
                cmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime2).Value = a.FechaInicio;
                cmd.Parameters.Add("@FechaFin", SqlDbType.DateTime2).Value = a.FechaFin;
                cmd.Parameters.Add("@Lugar", SqlDbType.NVarChar, 150).Value =
                    string.IsNullOrWhiteSpace(a.Lugar) ? (object)DBNull.Value : a.Lugar;
                cmd.Parameters.Add("@Cupos", SqlDbType.Int).Value = a.Cupos;
                cmd.Parameters.Add("@Estado", SqlDbType.NVarChar, 20).Value = a.Estado;

                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        /// <summary>Lista todas las actividades (más recientes primero).</summary>
        public List<ActividadDto> ObtenerTodas()
        {
            const string sql = @"
                SELECT ActividadId, Titulo, Descripcion, FechaInicio, FechaFin, Lugar, Cupos, Estado
                FROM dbo.ActividadAcademica
                ORDER BY FechaInicio DESC, ActividadId DESC;";
            return Consultar(sql, null);
        }

        /// <summary>Lista solo las actividades en un estado dado (ej. 'Publicada').</summary>
        public List<ActividadDto> ObtenerPorEstado(string estado)
        {
            const string sql = @"
                SELECT ActividadId, Titulo, Descripcion, FechaInicio, FechaFin, Lugar, Cupos, Estado
                FROM dbo.ActividadAcademica
                WHERE Estado = @Estado
                ORDER BY FechaInicio;";
            return Consultar(sql, cmd =>
                cmd.Parameters.Add("@Estado", SqlDbType.NVarChar, 20).Value = estado);
        }

        private List<ActividadDto> Consultar(string sql, Action<SqlCommand> parametros)
        {
            var resultado = new List<ActividadDto>();
            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                if (parametros != null) parametros(cmd);
                cn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        resultado.Add(new ActividadDto
                        {
                            ActividadId = r.GetInt32(0),
                            Titulo = r.GetString(1),
                            Descripcion = r.IsDBNull(2) ? "" : r.GetString(2),
                            FechaInicio = r.GetDateTime(3),
                            FechaFin = r.GetDateTime(4),
                            Lugar = r.IsDBNull(5) ? "" : r.GetString(5),
                            Cupos = r.GetInt32(6),
                            Estado = r.GetString(7)
                        });
                    }
                }
            }
            return resultado;
        }
    }
}
