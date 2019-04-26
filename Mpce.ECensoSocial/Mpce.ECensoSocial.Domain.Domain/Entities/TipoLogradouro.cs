using System;

namespace Mpce.ECensoSocial.Domain.Domain.Entities
{
    public class TipoLogradouro
    {
        [key]
        public String sCodigo { get; set; }
        public String sDescricao { get; set; }
        //public Trabalhador Trabalhador { get; set; }
    }
}