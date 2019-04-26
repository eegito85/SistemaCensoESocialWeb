using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;
using System.IO;
using Mpce.ECensoSocial.Domain.Domain.Diversos;
using System.Text;

namespace Mpce.CensoEsocial.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Documento")]
    public class DocumentoController : Controller
    {
        private readonly IDocumentoRepository _documentoRepository;

        public DocumentoController(IDocumentoRepository documentoRepository)
        {
            _documentoRepository = documentoRepository;
        }

        [HttpPost]
        public int Upload([FromBody] Documento _documento)
        {
            string caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\arquivos", _documento.sNomeArquivo);
            string dataAtual = DateTime.Now.ToString();
            dataAtual = dataAtual.Replace("/","");
            dataAtual = dataAtual.Replace(":","");
            string[] nome = _documento.sArquivo.Split(',');
            try
            {
                _documento.sArquivo = "nome";
                //Documento _doc = new Documento
                //{
                //    sCPF = _documento.sCPF,
                //    sArquivo = "nome",
                //    sTipo = _documento.sTipo,
                //    sNomeArquivo = dataAtual +'_'+ _documento.sNomeArquivo 
                //};

                caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\arquivos", _documento.sNomeArquivo);
                System.IO.File.WriteAllBytes(caminho, Convert.FromBase64String(nome[1]));
                if(this.VerificaArquivoDiretorio(_documento.sNomeArquivo))
                {
                    _documentoRepository.Add(_documento);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                e.ToString();
                return 0;
            }

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{iCodigo}")]
        //[Authorize]
        public int Delete(int iCodigo)
        {
            try
            {
                var _arquivoProjeto = _documentoRepository.GetById(iCodigo);
                _documentoRepository.Remove(_arquivoProjeto);
                string path = Diversos.PathArquivo(_arquivoProjeto.sNomeArquivo);
                System.IO.File.Delete(path);
                return 1;
            }
            catch (System.Exception)
            {
                return 0;
            }
        }

        // GET: api/Documento/5
        [HttpGet("Documento/{cpf}", Name = "GetDocumentos")]
        public IEnumerable<Documento> GetDocumentos(string cpf)
        {
            return  _documentoRepository.GetDocumentos(cpf);
        }


        [HttpGet("codigo/{id}", Name = "Download")]
        public async Task<IActionResult> Download(int id)
        {
            try
            {
                var _arquivoProjeto = _documentoRepository.GetById(id);
                //string sTipo;

                if (_arquivoProjeto.sNomeArquivo == null)
                    return Content("Arquivo não encontrado");

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\arquivos", _arquivoProjeto.sNomeArquivo);
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;

                return File(memory, Diversos.GetContentType(path), Path.GetFileName(path));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("verifica/{sNomeArquivo}", Name = "VerificaArquivoDiretorio")]
        public bool VerificaArquivoDiretorio(string sNomeArquivo)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\arquivos", sNomeArquivo);
            //string path = @"\\pgjsrv163\e$\WEBSITE\SITECORE\CensoEsocialApi\wwwroot\arquivos\" + sNomeArquivo;
            bool resposta = System.IO.File.Exists(path);
            return resposta;

        }
    }
}