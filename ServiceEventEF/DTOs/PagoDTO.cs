using System;
namespace ServiceEventEF.DTOs
{
    public class PagoDTO
    {
        public int IdPago { get; set; }
        public int? IdFormaPago { get; set; }
        public decimal? MontoMonetario { get; set; }
        public int? Moneda { get; set; }
        public bool? Solicitud { get; set; }

        public bool? Confirmacion { get; set; }

        public string Descripcion { get; set; }
        public int? Estado { get; set; }
        public int Id_Evento { get; set; }
        public int Id_Contacto { get; set; }
        public PagoDTO()
        {
        }
    }
}
