using System;
using System.Collections.Generic;
using System.Text;

namespace Mpce.ECensoSocial.Domain.Domain.Entities.Comprovante
{
    public class ComprovanteCedido
    {
        public String sCNPJEmpCed { get; set; }
        public String sMatriculaTrab { get; set; }
        public String dtAdmissao { get; set; }
        public String sTipoRegTrab { get; set; }
        public String sTipoRegPrev { get; set; }
        public String sOnusCessReq { get; set; }
        public String sCategoria { get; set; }
    }
}
