using System;
namespace ServiceEventEF.EntityVO
{
    public class RegistroAsistencia
    {
        public string NroDocumento { get; set; }
        public string NombreCompleto { get; set; }
        public string Estado { get; set; }
        public int IdInscripcion { get; set; }
        public RegistroAsistencia()
        {
        }
    }
}
