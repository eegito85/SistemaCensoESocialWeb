using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mpce.ECensoSocial.Domain.Domain.Entities
{
    public class Email
    {
        [Key]
        public int iCodigo { get; set; }

        public String sEmailDestino { get; set; }

        public String sAssuntoEmail { get; set; }

        public String sMensagem { get; set; }
    }
}
