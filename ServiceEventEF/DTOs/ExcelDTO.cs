using System.Collections.Generic;
using  System;
namespace ServiceEventEF.DTOs
{
    public class ExcelDTO
    {
        public string nombreArchivo { get; set; }
        public string error { get; set; }
        public List<string> errorcampos { get; set; }
        public List<InfoExcel> contenido { get; set; }
    }
}