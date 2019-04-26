using System;
using System.ComponentModel.DataAnnotations;

namespace Mpce.ECensoSocial.Domain.Domain.Entities
{
    public class Cedido
    {
        [key]
        public int iCodigo { get; set; }
        public int iCodTrabalhador { get; set; }
        public String sCNPJEmpCed { get; set; }
        public String sMatriculaTrab { get; set; }
        public DateTime? dtAdmissao { get; set; }
        public int iTipoRegTrab { get; set; }
        public int iTipoRegPrev { get; set; }
        public int iOnusCessReq { get; set; }
        public int? iCategoria { get; set; }
        public Trabalhador Trabalhador { get; set; }
    }
}