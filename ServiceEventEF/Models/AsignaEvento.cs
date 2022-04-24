using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class AsignaEvento
    {
        public int Id { get; set; }
        public int? IdEvento { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdUsuarioComp { get; set; }
        public DateTime? FechaRelacion { get; set; }
    }
}
