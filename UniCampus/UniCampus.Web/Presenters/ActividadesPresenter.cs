using System;
using System.Collections.Generic;
using System.Globalization;
using UniCampus.BLL;
using UniCampus.Entities;
using UniCampus.Web.Views;

namespace UniCampus.Web.Presenters
{
    /// <summary>Coordina la vista y el servicio de negocio.</summary>
    public class ActividadesPresenter
    {
        private readonly IActividadesView _view;
        private readonly ActividadService _service;

        public ActividadesPresenter(IActividadesView view) : this(view, new ActividadService()) { }

        public ActividadesPresenter(IActividadesView view, ActividadService service)
        {
            _view = view;
            _service = service;
        }

        public void Cargar()
        {
            try
            {
                _view.MostrarActividades(_service.ListarActividades());
            }
            catch (Exception ex)
            {
                ErrorLog.Registrar(ex);
                _view.MostrarMensaje("No fue posible cargar las actividades. Intente nuevamente.", true);
            }
        }

        public void Guardar()
        {
            try
            {
                var dto = new ActividadDto
                {
                    Titulo = _view.Titulo,
                    Descripcion = _view.Descripcion,
                    Lugar = _view.Lugar,
                    Estado = _view.Estado
                };

                var errores = new List<string>();

                DateTime inicio, fin;
                int cupos;
                if (TryFecha(_view.FechaInicio, out inicio)) dto.FechaInicio = inicio;
                else errores.Add("La fecha de inicio no tiene un formato válido.");
                if (TryFecha(_view.FechaFin, out fin)) dto.FechaFin = fin;
                else errores.Add("La fecha de fin no tiene un formato válido.");
                if (int.TryParse(_view.Cupos, NumberStyles.Integer, CultureInfo.InvariantCulture, out cupos)) dto.Cupos = cupos;
                else errores.Add("El cupo debe ser un número entero.");

                if (errores.Count > 0)
                {
                    _view.MostrarMensaje(string.Join(Environment.NewLine, errores), true);
                    return;
                }

                var resultado = _service.Registrar(dto);
                if (resultado.Exito)
                {
                    _view.LimpiarFormulario();
                    Cargar();                                   // refresca el listado
                    _view.MostrarMensaje(resultado.Mensaje, false); // el mensaje de éxito se muestra al final
                }
                else
                {
                    var texto = resultado.Mensaje + Environment.NewLine + string.Join(Environment.NewLine, resultado.Errores);
                    _view.MostrarMensaje(texto, true);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Registrar(ex);
                _view.MostrarMensaje("Ocurrió un error al guardar la actividad. Intente nuevamente.", true);
            }
        }

        private static bool TryFecha(string texto, out DateTime valor)
        {
            return DateTime.TryParse(texto, CultureInfo.InvariantCulture, DateTimeStyles.None, out valor);
        }
    }
}
