using System;
using UniCampus.BLL;

namespace UniCampus.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                // Crea la base de datos y las tablas si aún no existen (idempotente).
                ActividadService.PrepararBaseDeDatos();
            }
            catch (Exception ex)
            {
                ErrorLog.Registrar(ex);
            }
        }
    }
}
