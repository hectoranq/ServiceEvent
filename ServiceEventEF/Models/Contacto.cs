using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class Contacto
    {
        public Contacto()
        {
            ActividadConferencistaNavigation = new HashSet<Actividad>();
            ActividadModeradorNavigation = new HashSet<Actividad>();
            Inscripcion = new HashSet<Inscripcion>();
            TriviaRespuesta = new HashSet<TriviaRespuesta>();
        }

        public int IdContacto { get; set; }
        public int? IdEmpresa { get; set; }
        public string DocumentoIdentidad { get; set; }
        public int? Categoria { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string CorreoElectronico { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoMovil { get; set; }
        public string Direccion { get; set; }
        public string Pais { get; set; }
        public string EmpresaNombre { get; set; }
        public string Profesion { get; set; }
        public string Cargo { get; set; }
        public int? Estado { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string Observacion { get; set; }

        public Parametros CategoriaNavigation { get; set; }
        public Parametros EstadoNavigation { get; set; }
        public Empresa IdEmpresaNavigation { get; set; }
        public ICollection<Actividad> ActividadConferencistaNavigation { get; set; }
        public ICollection<Actividad> ActividadModeradorNavigation { get; set; }
        public ICollection<Inscripcion> Inscripcion { get; set; }
        public ICollection<TriviaRespuesta> TriviaRespuesta { get; set; }
    }
}
