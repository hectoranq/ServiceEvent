using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class Parametros
    {
        public Parametros()
        {
            Actividad = new HashSet<Actividad>();
            ActividadCategoria = new HashSet<ActividadCategoria>();
            ContactoCategoriaNavigation = new HashSet<Contacto>();
            ContactoEstadoNavigation = new HashSet<Contacto>();
            Empresa = new HashSet<Empresa>();
            Evento = new HashSet<Evento>();
            Inscripcion = new HashSet<Inscripcion>();
            Material = new HashSet<Material>();
            PagoEstadoNavigation = new HashSet<Pago>();
            PagoMonedaNavigation = new HashSet<Pago>();
            Tarifa = new HashSet<Tarifa>();
        }

        public int IdParametro { get; set; }
        public int IdTipo { get; set; }
        public string NombreTipo { get; set; }
        public string NombreParametro { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Actividad> Actividad { get; set; }
        public ICollection<ActividadCategoria> ActividadCategoria { get; set; }
        public ICollection<Contacto> ContactoCategoriaNavigation { get; set; }
        public ICollection<Contacto> ContactoEstadoNavigation { get; set; }
        public ICollection<Empresa> Empresa { get; set; }
        public ICollection<Evento> Evento { get; set; }
        public ICollection<Inscripcion> Inscripcion { get; set; }
        public ICollection<Material> Material { get; set; }
        public ICollection<Pago> PagoEstadoNavigation { get; set; }
        public ICollection<Pago> PagoMonedaNavigation { get; set; }
        public ICollection<Tarifa> Tarifa { get; set; }
    }
}
