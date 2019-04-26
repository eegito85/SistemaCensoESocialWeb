using Mpce.CensoEsocial.Data.Repositories.Common;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Mpce.CensoEsocial.Data.Repositories
{
    public class DependenteRepository : RepositoryBase<Dependente>, IDependenteRepository
    {
        public IEnumerable<Dependente> GetDependentes(int iCodTrabalhador)
        {
            return db.Dependente.Where(p => p.icodTrabalhador == iCodTrabalhador).ToList();
        }
    }
}
