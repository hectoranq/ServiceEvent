using System;
using System.Collections.Generic;
namespace ServiceEventEF.DTOs
{
    public class QuizDTO
    {
        public string Titulo { get; set; }
        public List<DTOs.TriviaOpcionDTO> Opciones { get; set; }
        public QuizDTO()
        {
        }
    }
}
