using System;
namespace ServiceEventEF.DTOs
{
    public class TriviaRespuestaDTO
    {
        public int Id { get; set; }
        public int IdContacto { get; set; }
        public int IdOpciones { get; set; }
        public int IdPreguntas { get; set; }
        public int IdActividad { get; set; }
        public int IdUsuario { get; set; }
        public TriviaRespuestaDTO()
        {
        }
    }
}
