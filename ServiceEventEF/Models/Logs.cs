using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class Logs
    {
        public int IdLogRegistro { get; set; }
        public DateTime? Fecha { get; set; }
        public string TablaAfectada { get; set; }
        public string DescripcionActividad { get; set; }
        public string NombreMaquina { get; set; }
        public string IpMaquina { get; set; }
    }
}
