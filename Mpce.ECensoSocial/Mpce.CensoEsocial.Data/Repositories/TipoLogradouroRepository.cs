using Mpce.CensoEsocial.Data.Repositories.Common;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mpce.CensoEsocial.Data.Repositories
{
    public class TipoLogradouroRepository : RepositoryBase<TipoLogradouro>, ITipoLogradouroRepository
    {
        public   TipoLogradouro GetBySid(String sCodigo) {
            return db.Set<TipoLogradouro>().Find(sCodigo); }
    }
}
