using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mpce.CensoEsocial.Autenticacao.DAO;
using Mpce.CensoEsocial.Data.Context;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;

namespace Mpce.CensoEsocial.WebApi.Controllers
{
    //[Authorize]
    [Route("api/Trabalhador")]
    public class TrabalhadorController : ControllerBase
    {
        private readonly ITrabalhadorRepository _trabalhadorRepository;
        IConfiguration _iconfiguration;

        public TrabalhadorController(ITrabalhadorRepository trabalhadorRepository, IConfiguration configuration)
        {
            _trabalhadorRepository = trabalhadorRepository;
            _iconfiguration = configuration;
        }

        // GET: api/Trabalhador
        [HttpGet]
        public IEnumerable<Trabalhador> Get()
        {
            IEnumerable<Trabalhador> trabalhadores = _trabalhadorRepository.GetAll();
            
            return trabalhadores;
        }

        // GET: api/Trabalhador
        [HttpGet("inicio/{cpf}", Name = "GetPermissao")]
        public int GetPermissao(string cpf)
        {
            Permissao permissao = new Permissao(_iconfiguration);
            return permissao.AcessoAdmin(cpf);
        }

        // GET: api/Trabalhador/5
        [HttpGet("{id}", Name = "GetById")]
        public Trabalhador Get(int id)
        {
            var r = _trabalhadorRepository.GetById(id);
            return r;
        }

        // POST: api/Trabalhador
        [HttpPost]
        public int Post([FromBody] Trabalhador value)
        {
            return _trabalhadorRepository.AddTrabalhador(value);
        }

        [Authorize]
        // GET: api/Trabalhador/5
        [HttpGet("Trabalhador/{cpf}", Name = "GetByCpf")]
        public Trabalhador GetTrabalhador(string cpf)
        {
            Trabalhador trabalhador = _trabalhadorRepository.GetByCpf(cpf);
            return trabalhador;
        }


        // PUT: api/Trabalhador/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Trabalhador value)

        {
            if(id == value.iCodigo)
            {
                _trabalhadorRepository.Update(value);
            }
        }

        // PUT: api/Trabalhador/5
        [HttpPut("atualizar/{iCodigo}/{sDescricao}/{iVerificado}")]
        public void PutTrabalhador(int iCodigo, string sDescricao, int iVerificado)
        {
            _trabalhadorRepository.UpdateObsSit(iCodigo, sDescricao, iVerificado);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Trabalhador trb = this.Get(id);
            _trabalhadorRepository.Remove(trb);
        }

        [HttpGet("iTipo/{cpf}")]
        public int GetiTipo(string cpf)
        {
            AppDbContextSPG context = new AppDbContextSPG();
            Acesso acesso = new Acesso(context);
            int iTipo = acesso.RetornaVinculo(cpf);

            return iTipo;
        }

    }
}
