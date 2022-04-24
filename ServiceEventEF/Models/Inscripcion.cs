using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class Inscripcion
    {
        public Inscripcion()
        {
            Registro = new HashSet<Registro>();
        }

        public int IdInscripcion { get; set; }
        public int IdContacto { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? Patrocinio { get; set; }
        public string EmpresaPatrocinadora { get; set; }
        public int? IdEvento { get; set; }
        public int? IdActividad { get; set; }
        public string Acompanante { get; set; }
        public string Visitante { get; set; }
        public string Motivo { get; set; }
        public int? IdPago { get; set; }
        public int? Estado { get; set; }
        public bool? Notificaciones { get; set; }
        public bool? Encuesta { get; set; }

        public Parametros EstadoNavigation { get; set; }
        public Actividad IdActividadNavigation { get; set; }
        public Contacto IdContactoNavigation { get; set; }
        public Evento IdEventoNavigation { get; set; }
        public Pago IdPagoNavigation { get; set; }
        public ICollection<Registro> Registro { get; set; }
    }
}
