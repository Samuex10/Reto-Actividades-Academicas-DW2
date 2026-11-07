using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace UniCampus.DAL
{
    /// <summary>
    /// Deja lista la base de datos en el primer arranque: la crea si no existe y ejecuta el script
    /// database/03_BaseDatos.sql (incluido como recurso). Es idempotente: se puede ejecutar muchas veces.
    /// </summary>
    public static class BaseDatosInicializador
    {
        public static void Asegurar()
        {
            var setting = ConfigurationManager.ConnectionStrings["UniCampusDb"];
            if (setting == null)
                throw new ConfigurationErrorsException("Falta la cadena de conexión 'UniCampusDb' en Web.config.");

            var builder = new SqlConnectionStringBuilder(setting.ConnectionString);
            string nombreBd = builder.InitialCatalog;

            // 1) Crear la base si no existe (se conecta a master)
            try
            {
                builder.InitialCatalog = "master";
                using (var cn = new SqlConnection(builder.ConnectionString))
                using (var cmd = new SqlCommand(
                    "IF DB_ID(@n) IS NULL BEGIN DECLARE @sql NVARCHAR(400) = N'CREATE DATABASE ' + QUOTENAME(@n); EXEC (@sql); END", cn))
                {
                    cmd.Parameters.Add("@n", SqlDbType.NVarChar, 128).Value = nombreBd;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                // Puede que ya exista o que no haya permiso sobre master; el paso 2 confirmará si hay conexión.
            }

            // 2) Crear tablas y datos de prueba (script embebido)
            string script = LeerScript();
            using (var cn = new SqlConnection(setting.ConnectionString))
            {
                cn.Open();
                foreach (var lote in Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase))
                {
                    string sql = lote.Trim();
                    if (sql.Length == 0) continue;
                    if (sql.IndexOf("CREATE DATABASE", StringComparison.OrdinalIgnoreCase) >= 0) continue;
                    if (sql.StartsWith("USE ", StringComparison.OrdinalIgnoreCase)) continue;
                    if (sql.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase)) continue;
                    using (var cmd = new SqlCommand(sql, cn))
                        cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>Informe de texto sobre la conexión (para la página de diagnóstico local).</summary>
        public static string Diagnosticar()
        {
            var sb = new StringBuilder();
            try
            {
                var setting = ConfigurationManager.ConnectionStrings["UniCampusDb"];
                if (setting == null) return "Falta la cadena de conexión 'UniCampusDb' en Web.config.";
                var b = new SqlConnectionStringBuilder(setting.ConnectionString);
                sb.AppendLine("Servidor: " + b.DataSource);
                sb.AppendLine("Base de datos: " + b.InitialCatalog);
                using (var cn = new SqlConnection(setting.ConnectionString))
                {
                    cn.Open();
                    sb.AppendLine("Conexión: OK (SQL Server " + cn.ServerVersion + ")");
                    using (var cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.ActividadAcademica", cn))
                        sb.AppendLine("Actividades registradas: " + cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine("ERROR: " + ex.Message);
            }
            return sb.ToString();
        }

        private static string LeerScript()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("03_BaseDatos.sql"))
            {
                if (stream == null)
                    throw new FileNotFoundException("No se encontró el script 03_BaseDatos.sql incrustado en la DAL.");
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                    return reader.ReadToEnd();
            }
        }
    }
}
