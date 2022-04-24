using System;
using System.Collections.Generic;
namespace ServiceEventEF.DTOs
{
    public class InscripcionActividadDTO
    {
        public int IdEvento { get; set; }
        public int Estado { get; set; }
        public int IdContacto { get; set; }
        public int IdPago { get; set; }
        public InscripcionActividadDTO()
        {
        }
    }

    
}
