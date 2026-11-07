using System;
using System.Collections.Generic;
using UniCampus.DAL;
using UniCampus.Entities;

namespace UniCampus.BLL
{
    /// <summary>Reglas de negocio de actividades. No conoce controles ASPX ni escribe SQL.</summary>
    public class ActividadService
    {
        public static readonly string[] EstadosPermitidos = { "Borrador", "Publicada", "Cancelada", "Finalizada" };
        private const int MaxTitulo = 120, MaxDescripcion = 1000, MaxLugar = 150;

        private readonly ActividadRepository _repository;

        public ActividadService() : this(new ActividadRepository()) { }

        public ActividadService(ActividadRepository repository)
        {
            _repository = repository;
        }

        /// <summary>Deja lista la base de datos (se llama al iniciar la aplicación).</summary>
        public static void PrepararBaseDeDatos()
        {
            BaseDatosInicializador.Asegurar();
        }

        /// <summary>Informe de conexión para la página local de diagnóstico.</summary>
        public static string Diagnosticar()
        {
            return BaseDatosInicializador.Diagnosticar();
        }

        public List<ActividadDto> ListarActividades()
        {
            return _repository.ObtenerTodas();
        }

        public ResultadoOperacion Registrar(ActividadDto actividad)
        {
            var errores = Validar(actividad);
            if (errores.Count > 0)
                return ResultadoOperacion.Fallido(errores);

            actividad.Titulo = actividad.Titulo.Trim();
            int id = _repository.Insertar(actividad);
            return ResultadoOperacion.Correcto(id, "Actividad registrada correctamente.");
        }

        /// <summary>Validaciones de servidor (no dependen del navegador).</summary>
        public List<string> Validar(ActividadDto a)
        {
            var e = new List<string>();
            if (a == null) { e.Add("No se recibieron datos de la actividad."); return e; }

            if (string.IsNullOrWhiteSpace(a.Titulo)) e.Add("El título es obligatorio.");
            else if (a.Titulo.Trim().Length > MaxTitulo) e.Add("El título no puede superar " + MaxTitulo + " caracteres.");

            if (!string.IsNullOrEmpty(a.Descripcion) && a.Descripcion.Length > MaxDescripcion)
                e.Add("La descripción no puede superar " + MaxDescripcion + " caracteres.");
            if (!string.IsNullOrEmpty(a.Lugar) && a.Lugar.Length > MaxLugar)
                e.Add("El lugar no puede superar " + MaxLugar + " caracteres.");

            if (a.FechaInicio == default(DateTime)) e.Add("La fecha de inicio es obligatoria.");
            if (a.FechaFin == default(DateTime)) e.Add("La fecha de fin es obligatoria.");
            if (a.FechaInicio != default(DateTime) && a.FechaFin != default(DateTime) && a.FechaFin < a.FechaInicio)
                e.Add("La fecha de fin no puede ser anterior a la de inicio.");

            if (a.Cupos <= 0) e.Add("El cupo debe ser un número mayor que cero.");

            if (Array.IndexOf(EstadosPermitidos, a.Estado) < 0)
                e.Add("El estado no es válido.");

            return e;
        }
    }
}
