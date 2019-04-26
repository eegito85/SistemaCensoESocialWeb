using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories
{
    public interface IDocumentoRepository : IRepostitoryBase<Documento>
    {
        int AdicionarArquivo(Documento documento);
        Documento PegarPorCpf(string cpf);
        IEnumerable<Documento> GetDocumentos(string cpf);
        Documento AtualizarDocumento(string cpf, string tipo, string arquivo);
    }
}
