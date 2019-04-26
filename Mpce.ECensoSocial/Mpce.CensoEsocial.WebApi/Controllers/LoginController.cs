using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mpce.CensoEsocial.Autenticacao.DAO;
using Mpce.CensoEsocial.Autenticacao.JwtModels;
using Mpce.CensoEsocial.Data.Context;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;
using static Mpce.CensoEsocial.Autenticacao.Models.LoginModel;

namespace Mpce.CensoEsocial.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ITrabalhadorRepository _trabalhadorRepository;
        IConfiguration _iconfiguration;

        public LoginController(ITrabalhadorRepository trabalhadorRepository, IConfiguration configuration)
        {
            _trabalhadorRepository = trabalhadorRepository;
            _iconfiguration = configuration;
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

            var trabalhador = this._trabalhadorRepository.GetByCpf(usuario.cpf);

            string sSenha = acesso.MontaSenha(usuario.cpf, usuarioBase.dtNascimento);

            Permissao permissao = new Permissao(_iconfiguration);
            int resultadoPermissao = permissao.AcessoAdmin(usuario.cpf);

            if (usuarioBase == null || sSenha != usuario.Senha)
            {
                return "Usuário ou senha inválido!";
            }

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var claimsp = new[]
                {
                  new Claim(JwtRegisteredClaimNames.Sub, usuario.cpf),
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TESTETESTETESTETESTETESTE"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
              issuer: "TESTE", 
              audience:  "TESTE",
              expires: DateTime.Now.AddMinutes(20),
              claims: claimsp,
              signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            //return Ok(new { Token = tokenString });
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}