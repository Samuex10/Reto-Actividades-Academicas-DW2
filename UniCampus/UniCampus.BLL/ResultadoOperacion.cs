using System.Collections.Generic;

namespace UniCampus.BLL
{
    /// <summary>Resultado comprensible de una operación de negocio.</summary>
    public class ResultadoOperacion
    {
        public bool Exito { get; set; }
        public int Id { get; set; }
        public string Mensaje { get; set; }
        public List<string> Errores { get; } = new List<string>();

        public static ResultadoOperacion Correcto(int id, string mensaje)
        {
            return new ResultadoOperacion { Exito = true, Id = id, Mensaje = mensaje };
        }

        public static ResultadoOperacion Fallido(IEnumerable<string> errores)
        {
            var r = new ResultadoOperacion { Exito = false, Mensaje = "No se pudo guardar. Revise los datos." };
            r.Errores.AddRange(errores);
            return r;
        }
    }
}
