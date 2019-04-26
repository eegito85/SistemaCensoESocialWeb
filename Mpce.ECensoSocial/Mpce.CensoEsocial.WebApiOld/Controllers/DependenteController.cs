using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;


namespace Mpce.CensoEsocial.WebApi.Controllers
{
    [Route("api/Dependente")]
    [ApiController]
    public class DependenteController : ControllerBase
    {
        private readonly IDependenteRepository _dependenteRepository;


        public DependenteController(IDependenteRepository DependenteRepository)
        {
            _dependenteRepository = DependenteRepository;
        }

        // GET: api/Dependente
        [HttpGet]
        public IEnumerable<Dependente> Get()
        {
            return _dependenteRepository.GetAll();
        }

        // GET: api/Dependente/5
        [HttpGet("{id}", Name = "Get")]
        public Dependente Get(int id)
        {
            var r = _dependenteRepository.GetById(id);
            return r;
        }

        // GET: api/Dependente/iCodTrabalhador/5
        [HttpGet("Trabalhador/{id}", Name = "GetDependentes")]
        public IEnumerable<Dependente> GetDependentes(int id)
        {
            return _dependenteRepository.GetDependentes(id);
        }


        // POST: api/Dependente
        [HttpPost]
        public void Post([FromBody] Dependente value)
        {
            _dependenteRepository.Add(value);
        }

        // PUT: api/Dependente/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Dependente value)
        {
            if (id == value.iCodigo)
            {
                _dependenteRepository.Update(value);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Dependente trb = this.Get(id);
            _dependenteRepository.Remove(trb);
        }
    }
}
