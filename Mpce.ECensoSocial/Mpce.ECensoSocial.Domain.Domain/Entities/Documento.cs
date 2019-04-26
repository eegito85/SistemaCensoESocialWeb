using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mpce.ECensoSocial.Domain.Domain.Entities
{
    public class Documento
    {
        [Key]
        public int iCodigo { get; set; }

        public String sCPF { get; set; }

        public String sTipo { get; set; }

        public String sArquivo { get; set; }
        public String sNomeArquivo { get; set; }
        
    }
}
