using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mpce.CensoEsocial.Autenticacao.DAO;
using Mpce.CensoEsocial.Autenticacao.JwtModels;
using Mpce.CensoEsocial.Data.Context;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;
using static Mpce.CensoEsocial.Autenticacao.Models.LoginModel;

namespace Mpce.CensoEsocial.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ITrabalhadorRepository _trabalhadorRepository;

        public LoginController(ITrabalhadorRepository trabalhadorRepository)
        {
            _trabalhadorRepository = trabalhadorRepository;
        }

        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [AllowAnonymous]
        [HttpPost]
        public string Post(
            [FromBody]Usuario usuario
            )
        {


            AppDbContextSPG context = new AppDbContextSPG();
            Acesso acesso = new Acesso(context);

            var usuarioBase = acesso.Find(usuario.cpf);


            string sSenha = acesso.MontaSenha(usuario.cpf, usuarioBase.dtNascimento);

            if (usuarioBase == null || sSenha != usuario.Senha)
            {
                return "";
            }

            /*
            var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create("a-password-very-big-to-be-good"))
                                .AddSubject("censo")
                                .AddIssuer("mpce.mp.br")
                                .AddAudience("mpce.mp.br")
                                .AddNameId(usuario.cpf)
                                .AddClaim("employeer", "31")
                                .AddExpiry(1)
                                .Build();

            return new JwtSecurityTokenHandler().WriteToken(token);
            */
            var claims = new[]
                {
                  new Claim(JwtRegisteredClaimNames.Sub, usuario.cpf),
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a-password-very-big-to-be-good"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("qualquer coisa", "qualquer coisa",
              expires: DateTime.Now.AddMinutes(Convert.ToInt16(30)),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}