using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class TriviaPregunta
    {
        public TriviaPregunta()
        {
            TriviaOpcion = new HashSet<TriviaOpcion>();
            TriviaRespuesta = new HashSet<TriviaRespuesta>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Indicio { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? IdCuestionario { get; set; }
        public string RutaImagen { get; set; }
        public int? NumPregunta { get; set; }
        public int? Tipo { get; set; }

        public ICollection<TriviaOpcion> TriviaOpcion { get; set; }
        public ICollection<TriviaRespuesta> TriviaRespuesta { get; set; }
    }
}
