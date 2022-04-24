using System;
namespace ServiceEventEF.DTOs
{
    public class IncripcionDTO
    {
        public int IdInscripcion { get; set; }
        public int IdContacto { get; set; }
        public bool Patrocinio { get; set; }
        public string EmpresaPatrocinadora { get; set; }
        public int IdEvento { get; set; }
        public int IdActividad { get; set; }
        public string Acompanante { get; set; }
        public string Visitante { get; set; }
        public string Motivo { get; set; }
        public int IdPago { get; set; }
        public int Estado { get; set; }
        public bool Notificaciones { get; set; }
        public bool Encuesta { get; set; }
        public IncripcionDTO()
        {
        }
    }
}
