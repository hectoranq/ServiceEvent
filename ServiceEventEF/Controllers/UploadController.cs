using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceEventEF.Models;
using System.IO;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Drawing;
using ServiceEventEF.Services;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;


namespace ServiceEventEF.Controllers
{
    [Produces("application/json")]
    [Route("api/upload")]
    public class UploadController : ControllerBase
    {
        private readonly DB_9AE8B0_GeventDlloContext _context;
        UploadService upservice = new UploadService();

        public UploadController(DB_9AE8B0_GeventDlloContext context)
        {
            _context = context;
        }

        [HttpPost, DisableRequestSizeLimit]
        public ActionResult UploadFile(string model)
        {
            try
            {
              
                string fileName = "";
               // string imgByte = "";
                // string objBase64 = "";
                string cod_Id = "";
                var file = Request.Form.Files[0];
                string folderName = "Upload";
                // string webRootPath = _hostingEnvironment.WebRootPath;
                string webRootPath = Directory.GetCurrentDirectory();
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                   
                    string tipoDoc = "";
                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    FotoDTO fotoDesz = JsonConvert.DeserializeObject<FotoDTO>(model);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        string[] nombre_archivo = fileName.Split('.');
                        string file_ext = nombre_archivo[nombre_archivo.Length - 1];
                        if (file_ext == "jpg" || file_ext == "png" || file_ext == "jpeg" || file_ext == "gif")
                        {
                            tipoDoc = "IMG";
                        }
                        else
                        {
                            tipoDoc = "BIN";
                        }
                        
                        if (tipoDoc == "IMG")
                        {
                            var evento = _context.Evento.Find(fotoDesz.Id);
                            evento.RutaImagen = fullPath;
                            _context.Entry(evento).State = EntityState.Modified;
                            try
                            {
                                _context.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException ex)
                            {
                                
                                return NotFound(ex.ToString());
                                
                            }

                            return Ok(new {
                                ruta = fullPath,
                                id = fotoDesz.Id
                            });
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                }
                return Ok(
               new
               {
                   Id = cod_Id,
                   archivo = fileName
               });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("img/{id}")]
        public ActionResult DownloadImg(int id)
        {

            string nomnreArchivo = "";
            var evento = _context.Evento.Where(x => x.IdEvento == id).FirstOrDefault();
            if (evento == null)
            {
                return NotFound();
            }

            var stream = System.IO.File.OpenRead(evento.RutaImagen);
            // var img = Convertir_Bytes_Imagen(stream);

           // string[] nombre_archivo = evento.RutaImagen.Split('.');

            var response = File(stream, upservice.GetContentType(evento.RutaImagen), Path.GetFileName(nomnreArchivo)); // FileStreamResult
            return response;

        }

        [HttpPost("datacontact"), DisableRequestSizeLimit]
        public ActionResult UploadFileDataCapex()
        {
            int code_doc = 0;
            try
            {

                ContactoDocument document = new ContactoDocument();
                string fileName = "";

                // string objBase64 = "";

                var file = Request.Form.Files[0];
                string folderName = "Upload";
                // string webRootPath = _hostingEnvironment.WebRootPath;
                string webRootPath = Directory.GetCurrentDirectory();
                string newPath = Path.Combine(webRootPath, folderName);

                List<DTOs.ContactoDTO> capexData = new List<DTOs.ContactoDTO>();
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    Random random = new System.Random();
                    code_doc = random.Next(1000, 9999);
                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, code_doc + fileName);
                    document.NombreArchivo = code_doc + fileName;
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        string[] nombre_archivo = fileName.Split('.');
                        string file_ext = nombre_archivo[nombre_archivo.Length - 1];
                        ISheet sheet;

                        stream.Position = 0;
                        if (file_ext == ".xls")
                        {
                            HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                            sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                        }
                        else
                        {
                            XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                            sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                        }
                        IRow headerRow = sheet.GetRow(0); //Get Header Row
                        int cellCount = headerRow.LastCellNum;

                        for (int j = 0; j < cellCount; j++)
                        {
                            NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                            if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                            string cels = cell.ToString();
                        }

                        for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                        {
                            IRow row = sheet.GetRow(i);
                            if (row == null) continue;
                            if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                            /* for (int j = row.FirstCellNum; j < cellCount; j++)
                             {
                                 string celd = "";
                                 if (row.GetCell(j) != null)
                                     celd = row.GetCell(j).ToString();
                             }*/

                            DTOs.ContactoDTO capex = new DTOs.ContactoDTO();
                            int j = row.FirstCellNum;
                           
                            
                            if (row.GetCell(j) != null)
                                if (row.GetCell(j).ToString().Trim() != "")
                                    capex.Nombres = row.GetCell(j).ToString();
                                else
                                    capex.Nombres = "Error";
                            else
                                capex.Nombres = "Error";

                            j++;
                            if (row.GetCell(j) != null)
                                if (row.GetCell(j).ToString().Trim() != "")
                                    capex.Apellidos = row.GetCell(j).ToString();
                                else
                                    capex.Apellidos = "Error";
                            else
                                capex.Apellidos = "Error";

                            
                            j++;
                            if (row.GetCell(j) != null)
                                if (row.GetCell(j).ToString().Trim() != "")
                                    capex.DocumentoIdentidad = row.GetCell(j).ToString();
                                else
                                    capex.DocumentoIdentidad = "Error";
                            else
                                capex.DocumentoIdentidad = "Error";

                            j++;
                            if (row.GetCell(j) != null)
                                if (row.GetCell(j).ToString().Trim() != "")
                                    capex.CorreoElectronico = row.GetCell(j).ToString();
                                else
                                    capex.CorreoElectronico = "Error";
                            else
                                capex.CorreoElectronico = "Error";

                            j++;
                            if (row.GetCell(j) != null)
                                if (row.GetCell(j).ToString().Trim() != "")
                                    capex.TelefonoFijo = row.GetCell(j).ToString();
                                else
                                    capex.TelefonoFijo = "Error";
                            else
                                capex.TelefonoFijo = "Error";

                            j++;
                            if (row.GetCell(j) != null)
                                if (row.GetCell(j).ToString().Trim() != "")
                                    capex.TelefonoMovil = row.GetCell(j).ToString();
                                else
                                    capex.TelefonoMovil = "Error";
                            else
                                capex.TelefonoMovil = "Error";

                            j++;
                            if (row.GetCell(j) != null)
                                if (row.GetCell(j).ToString().Trim() != "")
                                    capex.Direccion = row.GetCell(j).ToString();
                                else
                                    capex.Direccion = "Error";
                            else
                                capex.Direccion = "Error";


                            j++;
                            if (row.GetCell(j) != null)
                                if (row.GetCell(j).ToString().Trim() != "")
                                    capex.Profesion = row.GetCell(j).ToString();
                                else
                                    capex.Profesion = "Error";
                            else
                                capex.Profesion = "Error";

                            //------------- Identificadores ------------
                            j++;
                            try
                            {
                                capex.Categoria = int.Parse(row.GetCell(j).ToString());
                            }
                            catch (Exception ex)
                            {
                                capex.Categoria = -1;
                            }

                            j++;
                            try
                            {
                                capex.IdEmpresa = int.Parse(row.GetCell(j).ToString());
                            }
                            catch (Exception ex)
                            {
                                capex.IdEmpresa = -1;
                            }
                            capex.Estado = 1;
                            capex.Pais = "COL";
                            capexData.Add(capex);
                        }


                    }
                    document.contactos = capexData;
                }
                return Ok(document);
            }
            catch (System.Exception ex)
            {
                return BadRequest("Upload Failed: " + ex.Message);
            }
        }


        [HttpGet("cargadata/{excel}/usuario_id/{id}")]
        public IActionResult CreateCapex(string excel, int id)
        {
            try
            {
                if (excel == null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                int id_pjr = 0;
                object result = GuardarInfoExcelCapex(excel, id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(
                    result
                );
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        private object GuardarInfoExcelCapex(string Archivo, int id_usuario)
        {
            try
            {
                int ctscapex = 0;
                int code_doc = 0;
                string errNombre = "";
                ISheet sheet;

                IWorkbook iworkbook = null;
                string folderName = "Upload";
                string webRootPath = Directory.GetCurrentDirectory();
                string newPath = Path.Combine(webRootPath, folderName);
                string filePath = newPath + "/" + Archivo;

                Random random = new System.Random();
                code_doc = random.Next(0, 100);
                XSSFWorkbook hssfwb;
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfwb = new XSSFWorkbook(file);
                }
                //This will read 2007 Excel format


                //get first sheet from workbook  
                for (int x = 0; x < hssfwb.Count; x++)
                {

                    #region Procesa el documento excel

                    DTOs.InfoExcel excel = new DTOs.InfoExcel();
                    sheet = hssfwb.GetSheetAt(x);
                    excel.name = hssfwb.GetSheetAt(x).SheetName;
                    IRow headerRow = sheet.GetRow(x); //Get Header Row
                    int cellCount = headerRow.LastCellNum;



                    for (int j = 0; j < cellCount; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        string cels = cell.ToString();
                    }

                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;

                        Contacto contacto = new Contacto();
                        int j = row.FirstCellNum;
                        contacto.Nombres = row.GetCell(j).ToString();
                        j++;
                        contacto.Apellidos = row.GetCell(j).ToString();
                        j++;
                        contacto.DocumentoIdentidad = row.GetCell(j).ToString();
                        j++;
                        contacto.CorreoElectronico = row.GetCell(j).ToString();
                        j++;
                        contacto.TelefonoFijo = row.GetCell(j).ToString();
                        j++;
                        contacto.TelefonoMovil = row.GetCell(j).ToString();
                        j++;
                        contacto.Direccion = row.GetCell(j).ToString();
                        j++;
                        contacto.Pais = row.GetCell(j).ToString();
                        j++;
                        contacto.Profesion = row.GetCell(j).ToString();
                        j++;
                        contacto.Categoria = int.Parse( row.GetCell(j).ToString());
                        j++;
                        contacto.IdEmpresa = int.Parse( row.GetCell(j).ToString());
                        j++;
                        contacto.IdUsuario = id_usuario;
                        _context.Contacto.Add(contacto);
                        _context.SaveChanges();
                        ctscapex++;

                    }



                    #endregion

                }

                return new
                {
                    id = code_doc,
                    ctsca = ctscapex,
                    descripcion = "success"
                };
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}