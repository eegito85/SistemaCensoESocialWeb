using System;

namespace Mpce.ECensoSocial.Domain.Domain.Entities
{
    public class Pais
    {
        [key]
        public int iCodigo { get; set; }
        public String sPais { get; set; }
    }
}