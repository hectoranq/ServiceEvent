using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class CuestionarioActividad
    {
        public int Id { get; set; }
        public int? IdActividad { get; set; }
        public int? IdInscritos { get; set; }
        public string Titulo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
