using System;
using System.ComponentModel.DataAnnotations;

namespace Mpce.ECensoSocial.Domain.Domain.Entities
{
    public class Estagiario
    {
        [Key]
        public int iCodigo { get; set; }
        public int iCodTrabalhador { get; set; }
        public int iNaturezaEstagio { get; set; }
        public int? iCodCidadeInst { get; set; }
        public int? iAreaAtuacao { get; set; }
        public string sRazaoSocialInst { get; set; }
        public string sCNPJInst { get; set; }
        public string sLogradouroInst { get; set; }
        public string sNomeSupervisor { get; set; }
        public string sNumInst { get; set; }
        public string sBairroInst { get; set; }
        public string sUfInst { get; set; }
        public string sCepInst { get; set;
        }
        public Trabalhador Trabalhador { get; set; }
    }
}