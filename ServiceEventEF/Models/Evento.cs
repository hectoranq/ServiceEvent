using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class Evento
    {
        public Evento()
        {
            Actividad = new HashSet<Actividad>();
            ActividadCategoria = new HashSet<ActividadCategoria>();
            Asistencia = new HashSet<Asistencia>();
            Inscripcion = new HashSet<Inscripcion>();
            Registro = new HashSet<Registro>();
            Tarifa = new HashSet<Tarifa>();
        }

        public int IdEvento { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? CupoMaximoInscripciones { get; set; }
        public string Lugar { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public int? Estado { get; set; }
        public string RutaImagen { get; set; }
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public int? IdUsuario { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public bool? Destacado { get; set; }
        public decimal? CostoEvento { get; set; }

        public Parametros EstadoNavigation { get; set; }
        public ICollection<Actividad> Actividad { get; set; }
        public ICollection<ActividadCategoria> ActividadCategoria { get; set; }
        public ICollection<Asistencia> Asistencia { get; set; }
        public ICollection<Inscripcion> Inscripcion { get; set; }
        public ICollection<Registro> Registro { get; set; }
        public ICollection<Tarifa> Tarifa { get; set; }
    }
}
