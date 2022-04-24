using System;
namespace ServiceEventEF.EntityVO
{
    public class EventoVO
    {
        public int IdEvento { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public int? CupoMaximoInscripciones { get; set; }
        public string Lugar { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public int? Estado { get; set; }
        public string Ruta { get; set; }
        public int NroActividades { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public int IdUsuario { get; set; }
        public EventoVO()
        {
        }
    }
}
