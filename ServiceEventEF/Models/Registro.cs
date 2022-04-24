using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class Registro
    {
        public Registro()
        {
            Asistencia = new HashSet<Asistencia>();
        }

        public int IdRegistro { get; set; }
        public int IdInscripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public int IdEvento { get; set; }
        public int IdActividad { get; set; }
        public string Descripcion { get; set; }

        public Actividad IdActividadNavigation { get; set; }
        public Evento IdEventoNavigation { get; set; }
        public Inscripcion IdInscripcionNavigation { get; set; }
        public ICollection<Asistencia> Asistencia { get; set; }
    }
}
