using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class TriviaOpcion
    {
        public TriviaOpcion()
        {
            TriviaRespuesta = new HashSet<TriviaRespuesta>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public int? IdPregunta { get; set; }
        public bool? EsCorrecto { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public TriviaPregunta IdPreguntaNavigation { get; set; }
        public ICollection<TriviaRespuesta> TriviaRespuesta { get; set; }
    }
}
