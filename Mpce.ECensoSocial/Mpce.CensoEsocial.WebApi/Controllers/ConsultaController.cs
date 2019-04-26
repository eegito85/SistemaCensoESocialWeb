using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mpce.ECensoSocial.Domain.Domain.Entities.Comprovante;

namespace Mpce.CensoEsocial.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Consulta")]
    public class ConsultaController : Controller
    {
        IConfiguration _iconfiguration;

        public ConsultaController(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        // GET: api/Consulta/5
        [HttpGet("trabalhador/{id}", Name = "GetTrabalhador")]
        public ComprovanteTrabalhador GetTrabalhador(int id)
        {
            string conexao = _iconfiguration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            DAO.ConsultaDados dados = new DAO.ConsultaDados();
            ComprovanteTrabalhador trabalhador = dados.PegarDadosTrabalhador(id, conexao);

            return trabalhador;
        }

        // GET: api/Consulta/5
        [HttpGet("cedido/{id}", Name = "PegaCedido")]
        public ComprovanteCedido PegaCedido(int id)
        {
            string conexao = _iconfiguration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            DAO.ConsultaDados dados = new DAO.ConsultaDados();
            ComprovanteCedido cedido = dados.PegarDadosCedido(id, conexao);

            return cedido;
        }

        // GET: api/Consulta/5
        [HttpGet("estagiario/{id}", Name = "PegaEstagiario")]
        public ComprovanteEstagiario PegaEstagiario(int id)
        {
            string conexao = _iconfiguration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            DAO.ConsultaDados dados = new DAO.ConsultaDados();
            ComprovanteEstagiario estagiario = dados.PegarDadosEstagiario(id, conexao);

            return estagiario;
        }

        // GET: api/Consulta/5
        [HttpGet("dependentes/{id}", Name = "PegaDependentes")]
        public List<ComprovanteDependente> PegaDependentes(int id)
        {
            string conexao = _iconfiguration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            DAO.ConsultaDados dados = new DAO.ConsultaDados();
            List<ComprovanteDependente> dependentes = dados.PegarDadosDependentes(id, conexao);

            return dependentes;
        }
    }
}