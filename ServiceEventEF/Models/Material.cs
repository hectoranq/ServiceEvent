using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class Material
    {
        public int Id { get; set; }
        public int? IdMaterial { get; set; }
        public int? IdActividad { get; set; }
        public int? IdEvento { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public Parametros IdMaterialNavigation { get; set; }
    }
}
