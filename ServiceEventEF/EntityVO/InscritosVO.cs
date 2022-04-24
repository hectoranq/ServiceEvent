using System;
namespace ServiceEventEF.EntityVO
{
    public class InscritosVO
    {
        public int Id { get; set; }
        public int IdInscripcion { get; set; }
        public string Fecha { get; set; }
        public string Motivo { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string NroIdentidad { get; set; }
        public bool isRegistro { get; set; }
        public InscritosVO()
        {
        }
    }
}
