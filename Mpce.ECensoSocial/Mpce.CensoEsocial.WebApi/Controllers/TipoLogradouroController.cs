using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;

namespace Mpce.CensoEsocial.WebApi.Controllers
{
    [Route("api/TipoLogradouro")]
    public class TipoLogradouroController : ControllerBase
    {
        private readonly ITipoLogradouroRepository _tipoLogradouroRepository;


        public TipoLogradouroController(ITipoLogradouroRepository TipoLogradouroRepository)
        {
            _tipoLogradouroRepository = TipoLogradouroRepository;
        }

        // GET: api/TipoLogradouro
        [HttpGet]
        public IEnumerable<TipoLogradouro> Get()
        {

            return _tipoLogradouroRepository.GetAll();
        }

        // GET: api/TipoLogradouro/5
        [HttpGet("{id}", Name = "GetIdTl")]
        public TipoLogradouro Get(string id)
        {
            var r = _tipoLogradouroRepository.GetBySid(id);
            return r;
        }

        // POST: api/TipoLogradouro
        [HttpPost]
        public void Post([FromBody] TipoLogradouro value)
        {
            _tipoLogradouroRepository.Add(value);
        }

        // PUT: api/TipoLogradouro/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] TipoLogradouro value)
        {
            if (id == value.sCodigo)
            {
                _tipoLogradouroRepository.Update(value);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            TipoLogradouro tpl = this.Get(id);
            _tipoLogradouroRepository.Remove(tpl);
        }
    }
    
}