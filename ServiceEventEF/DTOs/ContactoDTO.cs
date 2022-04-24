using System;
namespace ServiceEventEF.DTOs
{
    public class ContactoDTO
    {
        public int IdContacto { get; set; }
        public int IdEmpresa { get; set; }
        public string DocumentoIdentidad { get; set; }
        public int Categoria { get; set; }
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
        public int Estado { get; set; }
        public int IdUsuario { get; set; }
        public ContactoDTO()
        {
        }
    }
}
