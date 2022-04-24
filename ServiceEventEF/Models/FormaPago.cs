using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class FormaPago
    {
        public FormaPago()
        {
            Pago = new HashSet<Pago>();
        }

        public int IdFormaPago { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Pago> Pago { get; set; }
    }
}
