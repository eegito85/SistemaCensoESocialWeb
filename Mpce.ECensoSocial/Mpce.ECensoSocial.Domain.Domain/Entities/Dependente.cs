using System;
using System.ComponentModel.DataAnnotations;

namespace Mpce.ECensoSocial.Domain.Domain.Entities
{
    public class Dependente
    {   
        [Key]
        public int iCodigo { get; set; }
        public int icodTrabalhador { get; set; }
        public int iTipoDependente { get; set; }
        public String sNomeDependente { get; set; }
        public DateTime? dtNasc { get; set; }
        public String sCPFDependente { get; set; }
        public String sDepTrabIRRF { get; set; }
        public String sDepIncapaFisMentTrab { get; set; }
        public int? iDependentePensao { get; set; }
        public String sResponsavel { get; set; }
        public String sTelefoneResp { get; set; }

        public Trabalhador Trabalhador { get; set; }
    }
}