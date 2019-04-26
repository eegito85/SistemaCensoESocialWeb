using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;

namespace Mpce.CensoEsocial.WebApi.Controllers
{
    [Route("api/Municipio")]
    [ApiController]

    public class MunicipioController : ControllerBase
    {
        private readonly IMunicipioRepository _municipioRepository;

        public MunicipioController(IMunicipioRepository MunicipioRepository)
        {
            _municipioRepository = MunicipioRepository;
        }

        // GET: api/Municipio
        [HttpGet]
        public IEnumerable<Municipio> Get()
        {
            return _municipioRepository.GetAll();
        }

        // GET: api/Municipio/5
        [HttpGet("{id}", Name = "GetIdM")]
        public Municipio Get(int id)
        {
            var r = _municipioRepository.GetById(id);
            return r;
        }

        // POST: api/Municipio
        [HttpPost]
        public void Post([FromBody] Municipio value)
        {
            _municipioRepository.Add(value);
        }

        // PUT: api/Municipio/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Municipio value)
        {
            if (id == value.iCodigo)
            {
                _municipioRepository.Update(value);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Municipio m = this.Get(id);
            _municipioRepository.Remove(m);
        }

    }
}
