using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;

namespace Mpce.CensoEsocial.WebApi.Controllers
{
    [Route("api/DependentePessoa")]
    [ApiController]

    public class DependentePessoaController : ControllerBase
    {
        private readonly IDependentePessoaRepository _dependentePessoaRepository;

        public DependentePessoaController(IDependentePessoaRepository dependentePessoaRepository)
        {
            _dependentePessoaRepository = dependentePessoaRepository;
        }


        // GET: api/Dependente
        [HttpGet]
        public IEnumerable<DependentePessoa> Get()
        {
            return  _dependentePessoaRepository.GetAll();
        }

        // GET: api/Dependente/5
        [HttpGet("{id}", Name = "GetDp")]
        public DependentePessoa Get(int id)
        {
            var r = _dependentePessoaRepository.GetById(id);
            return r;
        }

        // GET: api/Dependente/icodTrabalhador/5
        [HttpGet("Trabalhador/{id}", Name = "GetDependentesPensao")]
        public IEnumerable<DependentePessoa> GetDependentesPensao(int id)
        {
            return _dependentePessoaRepository.GetDependentesPensao(id);
        }


        // POST: api/Dependente
        [HttpPost]
        public void Post([FromBody] DependentePessoa value)
        {
            _dependentePessoaRepository.Add(value);
        }

        // PUT: api/Dependente/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] DependentePessoa value)
        {
            if (id == value.iCodigo)
            {
                _dependentePessoaRepository.Update(value);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            DependentePessoa dp = this.Get(id);
            _dependentePessoaRepository.Remove(dp);
        }
    }
}
