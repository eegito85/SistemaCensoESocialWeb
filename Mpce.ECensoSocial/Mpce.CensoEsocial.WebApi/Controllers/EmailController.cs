using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;

namespace Mpce.CensoEsocial.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Email")]
    public class EmailController : Controller
    {
        private readonly IEmailRepository _emailRepository;
        private readonly ITrabalhadorRepository _trabalhadorRepository;


        public EmailController(IEmailRepository emailRepository, ITrabalhadorRepository trabalhadorRepository)
        {
            _emailRepository = emailRepository;
            _trabalhadorRepository = trabalhadorRepository;
        }

        [HttpGet("enviar/{codigo}")]
        //[Route("EnviarEmail")]
        public async Task<IActionResult> EnviarEmailAsync(int codigo)
        {
            //string email = "e_egito@hotmail.com";
            var trabalhador = _trabalhadorRepository.GetById(codigo);

            await _emailRepository.EnviarEmail(trabalhador.sEmail, codigo);

            //await _emailRepository.EnviarEmail(_email.sEmailDestino, _email.sAssuntoEmail, _email.sMensagem);

            return Ok();
        }

        [HttpGet("alteracao/{email}/{descricao}")]
        //[Route("EnviarEmail")]
        public async Task<IActionResult> EnviarEmailObs(string email, string descricao)
        {
            //string email = "e_egito@hotmail.com";

            await _emailRepository.EnviarEmailObservacao(email, descricao);

            return Ok();
        }

    }
}