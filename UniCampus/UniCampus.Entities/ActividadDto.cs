using System;

namespace UniCampus.Entities
{
    /// <summary>Transporta los datos de una actividad entre capas. Sin controles ni SQL.</summary>
    public class ActividadDto
    {
        public int ActividadId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Lugar { get; set; }
        public int Cupos { get; set; }
        public string Estado { get; set; }
    }
}
