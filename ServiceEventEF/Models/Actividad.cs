using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class Actividad
    {
        public Actividad()
        {
            ActividadCategoria = new HashSet<ActividadCategoria>();
            Asistencia = new HashSet<Asistencia>();
            Inscripcion = new HashSet<Inscripcion>();
            Registro = new HashSet<Registro>();
            Tarifa = new HashSet<Tarifa>();
            TriviaRespuesta = new HashSet<TriviaRespuesta>();
        }

        public int IdActividad { get; set; }
        public int IdEvento { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? Predecesor { get; set; }
        public int? CupoMaximoInscripciones { get; set; }
        public int? Conferencista { get; set; }
        public int? Moderador { get; set; }
        public string Lugar { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public int Estado { get; set; }
        public decimal? CostoActividad { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }

        public Contacto ConferencistaNavigation { get; set; }
        public Parametros EstadoNavigation { get; set; }
        public Evento IdEventoNavigation { get; set; }
        public Contacto ModeradorNavigation { get; set; }
        public ICollection<ActividadCategoria> ActividadCategoria { get; set; }
        public ICollection<Asistencia> Asistencia { get; set; }
        public ICollection<Inscripcion> Inscripcion { get; set; }
        public ICollection<Registro> Registro { get; set; }
        public ICollection<Tarifa> Tarifa { get; set; }
        public ICollection<TriviaRespuesta> TriviaRespuesta { get; set; }
    }
}
