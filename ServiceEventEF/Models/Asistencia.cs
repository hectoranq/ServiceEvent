using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class Asistencia
    {
        public int IdAsistencia { get; set; }
        public int IdRegistro { get; set; }
        public int IdEvento { get; set; }
        public int IdActividad { get; set; }
        public DateTime? FechaHoraIngreso { get; set; }
        public DateTime? FechaHoraSalida { get; set; }
        public TimeSpan? TiempoParticipacion { get; set; }
        public string Descripcion { get; set; }

        public Actividad IdActividadNavigation { get; set; }
        public Evento IdEventoNavigation { get; set; }
        public Registro IdRegistroNavigation { get; set; }
    }
}
