using System;
namespace ServiceEventEF.DTOs
{
    public class CuestionarioActividadDTO
    {
        public int Id { get; set; }
        public int IdActividad { get; set; }
        public int IdInscritos { get; set; }
        public string Titulo { get; set; }
        public CuestionarioActividadDTO()
        {

        }
    }
}
