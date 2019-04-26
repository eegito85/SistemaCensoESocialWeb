using Mpce.CensoEsocial.Data.Repositories.Common;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mpce.CensoEsocial.Data.Repositories
{
    public class MunicipioRepository : RepositoryBase<Municipio>, IMunicipioRepository
    {
        public IEnumerable<Municipio>GetByUf(string uf)
        {
            return db.Municipio.Where(p => p.sCidade.Contains("(" + uf + ")")).ToList();
        }

    }
}
