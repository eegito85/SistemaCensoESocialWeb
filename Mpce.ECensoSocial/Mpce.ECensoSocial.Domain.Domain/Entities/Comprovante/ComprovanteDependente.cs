using System;
using System.Collections.Generic;
using System.Text;

namespace Mpce.ECensoSocial.Domain.Domain.Entities.Comprovante
{
    public class ComprovanteDependente
    {
        public String sTipoDependente { get; set; }
        public String sNomeDependente { get; set; }
        public String dtNasc { get; set; }
        public String sCPFDependente { get; set; }
        public String sDepTrabIRRF { get; set; }
        public String sDepIncapaFisMentTrab { get; set; }
        public String sDependentePensao { get; set; }
        public String sResponsavel { get; set; }
        public String sTelefoneResp { get; set; }
    }
}
