using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class TriviaRespuesta
    {
        public int Id { get; set; }
        public int? IdContacto { get; set; }
        public int? IdOpciones { get; set; }
        public int? IdPreguntas { get; set; }
        public int? IdActividad { get; set; }
        public DateTime? FechaCrecion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? IdUsuario { get; set; }

        public Actividad IdActividadNavigation { get; set; }
        public Contacto IdContactoNavigation { get; set; }
        public TriviaOpcion IdOpcionesNavigation { get; set; }
        public TriviaPregunta IdPreguntasNavigation { get; set; }
    }
}
