using Microsoft.EntityFrameworkCore;
using Mpce.CensoEsocial.Data.Repositories.Common;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mpce.CensoEsocial.Data.Repositories
{
    public class TrabalhadorRepository : RepositoryBase<Trabalhador>, ITrabalhadorRepository
    {
        public override IEnumerable<Trabalhador> GetAll()
        {
            return db.Trabalhador.ToList().OrderBy(t => t.sNome);
        }

        public override Trabalhador GetById(int? id)
        {
            return db.Trabalhador.Where(p=>p.iCodigo == id).FirstOrDefault();
        }

        public int AddTrabalhador(Trabalhador trabalhador)
        {
            trabalhador.dtCadastro = DateTime.Now;
            db.Add(trabalhador);
            db.SaveChanges();
            return trabalhador.iCodigo;
        }

        public Trabalhador GetByCpf(string cpf)
        {
            return db.Trabalhador.Where(p => p.sCPF == cpf).FirstOrDefault();
        }

        public Trabalhador GetByNome(string nome)
        {
            return db.Trabalhador.Where(p => p.sNome.ToUpper() == nome.ToUpper()).FirstOrDefault();
        }

        public void UpdateObsSit(int iCodigo, string sObservacao, int iVerificacao)
        {
            if(sObservacao == null)
            {
                sObservacao = "";
            }
            Trabalhador trabalhador = db.Trabalhador.Where(t => t.iCodigo == iCodigo).FirstOrDefault();
            trabalhador.sDescricao = sObservacao;
            trabalhador.iVerificado = iVerificacao;
            db.SaveChanges();
        }

        public override void Update(Trabalhador trabalhador)
        {
            trabalhador.dtUltimaAtualizacao = DateTime.Now;
            db.Entry(trabalhador).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Trabalhador getByNome(string nome)
        {
            throw new NotImplementedException();
        }
    }
}