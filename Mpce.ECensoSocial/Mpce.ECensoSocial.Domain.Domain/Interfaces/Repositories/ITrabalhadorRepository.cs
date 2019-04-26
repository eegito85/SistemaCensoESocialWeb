using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories.Common;

namespace Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories
{
    public interface ITrabalhadorRepository : IRepostitoryBase<Trabalhador>
    {
        int AddTrabalhador(Trabalhador trabalhador);
        Trabalhador GetByCpf(string cpf);
        

        void UpdateObsSit(int iCodigo, string sObservacao, int iVerificacao);
        Trabalhador getByNome(string nome);
    }
}
