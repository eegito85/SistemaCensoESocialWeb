using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories.Common;

namespace Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories
{
    public interface ICedidoRepository : IRepostitoryBase<Cedido> {
        Cedido GetCedido(int iCodTrabalhador);
    }
}
