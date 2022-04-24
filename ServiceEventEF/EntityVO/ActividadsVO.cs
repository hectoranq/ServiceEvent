using System;
namespace ServiceEventEF.EntityVO
{
    public class ActividadsVO
    {
        public int IdActividad { get; set; }
        public int IdEvento { get; set; }
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public int? Predecesor { get; set; }
        public int? CupoMaximoInscripciones { get; set; }
        public int? Conferencista { get; set; }
        public int? Moderador { get; set; }
        public string Lugar { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public int Estado { get; set; }
        public decimal Costo { get; set; }
        public ActividadsVO()
        {
        }
    }
}
