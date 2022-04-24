using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class ActividadCategoria
    {
        public int IdActividadCategoria { get; set; }
        public int IdEvento { get; set; }
        public int IdActividad { get; set; }
        public int IdCategoria { get; set; }

        public Actividad IdActividadNavigation { get; set; }
        public Parametros IdCategoriaNavigation { get; set; }
        public Evento IdEventoNavigation { get; set; }
    }
}
