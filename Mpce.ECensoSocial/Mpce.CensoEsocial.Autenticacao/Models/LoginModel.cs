using System;
using System.Collections.Generic;
using System.Text;

namespace Mpce.CensoEsocial.Autenticacao.Models
{
    public class LoginModel
    {
        public class Pessoa
        {
            public string sCpf { get; set; }

            public DateTime DataNascimento { get; set; }
        }

        public class Usuario
        {
            public string UsuarioID { get; set; }

            public string cpf { get; set; }

            public string Senha { get; set; }
        }
    }
}
