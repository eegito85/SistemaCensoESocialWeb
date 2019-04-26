using System;
using System.ComponentModel.DataAnnotations;

namespace Mpce.ECensoSocial.Domain.Domain.Entities
{
    public class Municipio
    {
        [key]
        public int iCodigo { get; set; }
        public String sCidade { get; set; }
    }
}
