using Mpce.CensoEsocial.Data.Repositories.Common;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;
using System.Linq;

namespace Mpce.CensoEsocial.Data.Repositories
{
    public class EstagiarioRepository : RepositoryBase<Estagiario>, IEstagiarioRepository
    {
        public Estagiario GetEstagiario(int id)
        {
            return db.Estagiario.Where(p => p.iCodTrabalhador == id).FirstOrDefault();
        }

    }
}
