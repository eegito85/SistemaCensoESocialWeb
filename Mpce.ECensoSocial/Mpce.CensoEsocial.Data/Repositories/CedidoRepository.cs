using Mpce.CensoEsocial.Data.Repositories.Common;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;
using System.Linq;

namespace Mpce.CensoEsocial.Data.Repositories
{
    public class CedidoRepository : RepositoryBase<Cedido>, ICedidoRepository
    {
        public Cedido GetCedido(int id)
        {
            return db.Cedido.Where(p => p.iCodTrabalhador == id).FirstOrDefault();
        }

    }
}