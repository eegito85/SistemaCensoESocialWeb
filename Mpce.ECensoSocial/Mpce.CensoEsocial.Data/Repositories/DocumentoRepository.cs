using Mpce.CensoEsocial.Data.Repositories.Common;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Mpce.CensoEsocial.Data.Repositories
{
    public class DocumentoRepository : RepositoryBase<Documento>, IDocumentoRepository
    {
        public int AdicionarArquivo(Documento documento)
        {
            db.Add(documento);
            db.SaveChanges();
            return documento.iCodigo;
        }

        public Documento PegarPorCpf(string cpf)
        {
            return db.Documento.Where(p => p.sCPF == cpf).FirstOrDefault();
        }

        public Documento AtualizarDocumento(string cpf, string tipo, string arquivo)
        {
            Documento documento = db.Documento.Where(p => p.sCPF == cpf).FirstOrDefault();
            documento.sArquivo = arquivo;
            documento.sCPF = cpf;
            documento.sTipo = tipo;
            db.Documento.Update(documento);
            return documento;
        }

        public IEnumerable<Documento> GetDocumentos(string cpf)
        {
            return db.Documento.Where(p => p.sCPF == cpf).ToList();
        }
    }
}
