using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class Empresa
    {
        public Empresa()
        {
            Contacto = new HashSet<Contacto>();
        }

        public int IdEmpresa { get; set; }
        public string TipoIdentificacion { get; set; }
        public string Nombre { get; set; }
        public string Nit { get; set; }
        public int? CupoMaximoAsistentes { get; set; }
        public string CorreoElectronico { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoMovil { get; set; }
        public string Direccion { get; set; }
        public string Pais { get; set; }
        public int Estado { get; set; }

        public Parametros EstadoNavigation { get; set; }
        public ICollection<Contacto> Contacto { get; set; }
    }
}
