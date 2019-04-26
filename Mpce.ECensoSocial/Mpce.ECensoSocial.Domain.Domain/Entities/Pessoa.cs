using System;


namespace Mpce.ECensoSocial.Domain.Domain.Entities
{
    public class Pessoa
    {
        [key]
        public int iCodPessoa { get; set; }

        public string sCpf { get; set; }
        public DateTime dtNascimento { get; set; }
    }
}