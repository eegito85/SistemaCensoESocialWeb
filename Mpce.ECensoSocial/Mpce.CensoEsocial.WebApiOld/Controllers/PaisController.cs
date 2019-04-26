using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mpce.CensoEsocial.WebApi.Controllers
{
    [Route("api/Pais")]
    [ApiController]
    public class PaisController : Controller
    {
        private readonly IPaisRepository _paisRepository;

        public PaisController(IPaisRepository PaisRepository)
        {
            _paisRepository = PaisRepository;
        }

        // GET: api/Dependente
        [HttpGet]
        public IEnumerable<Pais> Get()
        {
            return _paisRepository.GetAll();
        }

        // GET: api/Dependente/5
        [HttpGet("{id}", Name = "GetIdP")]
        public Pais Get(int id)
        {
            var p = _paisRepository.GetById(id);
            return p;
        }

        // POST: api/Dependente
        [HttpPost]
        public void Post([FromBody] Pais value)
        {
            _paisRepository.Add(value);
        }

        // PUT: api/Dependente/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Pais value)
        {
            if (id == value.iCodigo)
            {
                _paisRepository.Update(value);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Pais pais = this.Get(id);
            _paisRepository.Remove(pais);
        }


    }
}
