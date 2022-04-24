using System;
namespace ServiceEventEF.DTOs
{
    public class TriviaOpcionDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int IdPregunta { get; set; }
        public bool EsCorrecto { get; set; }
        public TriviaOpcionDTO()
        {
        }
    }
}
