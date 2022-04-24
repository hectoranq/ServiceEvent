using System;
namespace ServiceEventEF.DTOs
{
    public class CuestionarioReportDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string TipoEncuesta { get; set; }
        public int ContactosInscritos { get; set; }
        public int Completados { get; set; }

        public CuestionarioReportDTO()
        {
        }
    }
}
