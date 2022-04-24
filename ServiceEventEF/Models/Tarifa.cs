using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class Tarifa
    {
        public int IdTarifa { get; set; }
        public int IdEvento { get; set; }
        public int IdActividad { get; set; }
        public decimal? MontoMonetario { get; set; }
        public int? Moneda { get; set; }

        public Actividad IdActividadNavigation { get; set; }
        public Evento IdEventoNavigation { get; set; }
        public Parametros MonedaNavigation { get; set; }
    }
}
