using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories.Common;
using System.Collections.Generic;

namespace Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories
{
    public interface IMunicipioRepository : IRepostitoryBase<Municipio> {
        IEnumerable<Municipio> GetByUf(string uf);
    }
}
