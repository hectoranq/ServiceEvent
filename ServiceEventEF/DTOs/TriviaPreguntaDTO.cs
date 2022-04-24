using System;
namespace ServiceEventEF.DTOs
{
    public class TriviaPreguntaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Indicio { get; set; }
        public int IdCuestionario { get; set; }
        public string RutaImagen { get; set; }
        public int Tipo { get; set; }
        public TriviaPreguntaDTO()
        {
        }
    }
}
