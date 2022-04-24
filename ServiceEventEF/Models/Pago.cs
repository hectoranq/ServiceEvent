using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class Pago
    {
        public Pago()
        {
            Inscripcion = new HashSet<Inscripcion>();
        }

        public int IdPago { get; set; }
        public int? IdFormaPago { get; set; }
        public decimal? MontoMonetario { get; set; }
        public int? Moneda { get; set; }
        public bool? Solicitud { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public bool? Confirmacion { get; set; }
        public DateTime? FechaConformacion { get; set; }
        public string Descripcion { get; set; }
        public int? Estado { get; set; }
        public int? IdEvento { get; set; }
        public int? IdContacto { get; set; }

        public Parametros EstadoNavigation { get; set; }
        public FormaPago IdFormaPagoNavigation { get; set; }
        public Parametros MonedaNavigation { get; set; }
        public ICollection<Inscripcion> Inscripcion { get; set; }
    }
}
