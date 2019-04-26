using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;


namespace Mpce.CensoEsocial.WebApi.Controllers
{
    [Route("api/Cedido")]
    [ApiController]

    public class CedidoController : Controller
    {
        private readonly ICedidoRepository _cedidoRepository;

        public CedidoController(ICedidoRepository CedidoRepository)
        {
            _cedidoRepository = CedidoRepository;
        }

        // GET: api/Cedido
        [HttpGet]
        public IEnumerable<Cedido> Get()
        {
            return _cedidoRepository.GetAll();
        }

        // GET: api/Cedido/5
        [HttpGet("{id}", Name = "GetC")]
        public Cedido Get(int id)
        {
            var r = _cedidoRepository.GetById(id);
            return r;
        }

        // GET: api/cedido/iCodTrabalhador/5
        [HttpGet("Trabalhador/{id}", Name = "GetCedido")]
        public Cedido GetCedido(int id)
        {
            return _cedidoRepository.GetCedido(id);
        }


        // POST: api/Cedido
        [HttpPost]
        public void Post([FromBody] Cedido value)
        {
            _cedidoRepository.Add(value);
        }

        // PUT: api/Cedido/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Cedido value)
        {
            if (id == value.iCodigo)
            {
                _cedidoRepository.Update(value);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Cedido ced = this.Get(id);
            _cedidoRepository.Remove(ced);
        }
    }
}
