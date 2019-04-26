using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mpce.CensoEsocial.Data.Repositories;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mpce.CensoEsocial.WebApi.Controllers
{
    [Route("api/Estagiario")]
    public class EstagiarioController : ControllerBase
    {
        private readonly IEstagiarioRepository _estagiarioRepository;


        public EstagiarioController(IEstagiarioRepository EstagiarioRepository)
        {
            _estagiarioRepository = EstagiarioRepository;
        }

        // GET: api/Dependente
        [HttpGet]
        public IEnumerable<Estagiario> Get()
        {
            return _estagiarioRepository.GetAll();
        }

        // GET: api/Dependente/5
        [HttpGet("{id}", Name = "GetIdE")]
        public Estagiario Get(int id)
        {
            var r = _estagiarioRepository.GetById(id);
            return r;
        }

        // GET: api/estagiario/iCodTrabalhador/5
        [HttpGet("Trabalhador/{id}", Name = "GetEstagiario")]
        public Estagiario GetEstagiario(int id)
        {
            return _estagiarioRepository.GetEstagiario(id);
        }

        // POST: api/Dependente
        [HttpPost]
        public void Post([FromBody] Estagiario value)
        {
            _estagiarioRepository.Add(value);
        }

        // PUT: api/Dependente/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Estagiario value)
        {
            if (id == value.iCodigo)
            {
                _estagiarioRepository.Update(value);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Estagiario est = this.Get(id);
            _estagiarioRepository.Remove(est);
        }
    }
}
