using System.Collections.Generic;
using UniCampus.Entities;

namespace UniCampus.Web.Views
{
    /// <summary>Contrato de la vista (MVP). Sin HTML ni SQL.</summary>
    public interface IActividadesView
    {
        // Datos del formulario (la vista los expone como texto; el Presenter los interpreta)
        string Titulo { get; }
        string Descripcion { get; }
        string FechaInicio { get; }
        string FechaFin { get; }
        string Lugar { get; }
        string Cupos { get; }
        string Estado { get; }

        void MostrarActividades(IList<ActividadDto> actividades);
        void MostrarMensaje(string mensaje, bool esError);
        void LimpiarFormulario();
    }
}
