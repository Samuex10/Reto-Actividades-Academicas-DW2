using System;
using System.IO;

namespace UniCampus.Web
{
    /// <summary>Registra el detalle técnico de los errores en App_Data/errores.log (no se muestra al usuario).</summary>
    public static class ErrorLog
    {
        public static void Registrar(Exception ex)
        {
            try
            {
                string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
                Directory.CreateDirectory(carpeta);
                string ruta = Path.Combine(carpeta, "errores.log");
                File.AppendAllText(ruta,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine + ex + Environment.NewLine +
                    new string('-', 60) + Environment.NewLine);
            }
            catch
            {
                // El registro de errores nunca debe tumbar la aplicación.
            }
        }
    }
}
