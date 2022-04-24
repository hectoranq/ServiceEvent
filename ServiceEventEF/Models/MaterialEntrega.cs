using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class MaterialEntrega
    {
        public int Id { get; set; }
        public int? IdMaterial { get; set; }
        public int? IdInscrito { get; set; }
        public bool? Entrega { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
