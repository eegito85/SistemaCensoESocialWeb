using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories
{
    public interface IEmailRepository
    {
        Task EnviarEmail(string emailDestino, int codigo);

        Task EnviarEmailObservacao(string _to, string sDescricao);
    }
}
