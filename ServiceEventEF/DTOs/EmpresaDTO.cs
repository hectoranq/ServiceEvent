using System;
namespace ServiceEventEF.DTOs
{
    public class EmpresaDTO
    {
        public int IdEmpresa { get; set; }
        public string TipoIdentificacion { get; set; }
        public string Nombre { get; set; }
        public string Nit { get; set; }
        public int CupoMaximoAsistentes { get; set; }
        public string CorreoElectronico { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoMovil { get; set; }
        public string Direccion { get; set; }
        public string Pais { get; set; }

        public EmpresaDTO()
        {
        }
    }
}
