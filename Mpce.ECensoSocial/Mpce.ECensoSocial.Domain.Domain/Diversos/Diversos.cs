using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace Mpce.ECensoSocial.Domain.Domain.Diversos
{
    public class Diversos
    {
        public static async Task SalvarArquivo(IFormFile arquivo, string sArquivo)
        {
            string path = PathArquivo(sArquivo);

            if (arquivo == null || arquivo.Length == 0)
            {

            }
            else
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }
            }
        }

        public static string PathArquivo(string sArquivo)
        {
            string path = "";
            path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\arquivos", sArquivo);
            //path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\doc\\instituicao", sArquivo);
            return path;
        }


        public static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".odt", "application/vnd.oasis.opendocument.text"},
                {".ods", "application/vnd.oasis.opendocument.spreadsheet"},
                {".odp", "application/vnd.oasis.opendocument.presentation"}
            };
        }
    }
}
