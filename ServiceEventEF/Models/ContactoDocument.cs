using System;
using System.Collections.Generic;
namespace ServiceEventEF.Models
{
    public class ContactoDocument
    {
        public string NombreArchivo { get; set; }
        public List<DTOs.ContactoDTO> contactos { get; set; }
        public ContactoDocument()
        {
        }
    }
}
