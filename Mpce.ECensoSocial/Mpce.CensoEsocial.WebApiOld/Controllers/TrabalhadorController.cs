using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;

namespace Mpce.CensoEsocial.WebApi.Controllers
{
    //[Authorize("Bearer")]
    [Route("api/Trabalhador")]
    [ApiController]
    public class TrabalhadorController : ControllerBase
    {
        private readonly ITrabalhadorRepository _trabalhadorRepository;


        public TrabalhadorController(ITrabalhadorRepository trabalhadorRepository) {
            _trabalhadorRepository = trabalhadorRepository;
        }

        // GET: api/Trabalhador
        [HttpGet]
        public IEnumerable<Trabalhador> Get()
        {
            return _trabalhadorRepository.GetAll();
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

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Trabalhador trb = this.Get(id);
            _trabalhadorRepository.Remove(trb);
        }
    }
}
