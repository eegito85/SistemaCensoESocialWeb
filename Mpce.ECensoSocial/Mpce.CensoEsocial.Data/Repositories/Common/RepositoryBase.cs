using Microsoft.EntityFrameworkCore;
using Mpce.CensoEsocial.Data.Context;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mpce.CensoEsocial.Data.Repositories.Common
{
    public class RepositoryBase<TEntity> : IDisposable, IRepostitoryBase<TEntity> where TEntity : class
    {
        protected readonly AppDbContext db;
        public RepositoryBase() { db = new AppDbContext(); }
        public virtual void Add(TEntity obj) { db.Add(obj); db.SaveChanges(); }
        public virtual IEnumerable<TEntity> GetAll()
        {
            return db.Set<TEntity>().ToList();
        }
        public virtual TEntity GetById(int? id) { return db.Set<TEntity>().Find(id); }

        public virtual void Remove(TEntity obj) { db.Set<TEntity>().Remove(obj); db.SaveChanges(); }
        public virtual void Update(TEntity obj) { db.Entry(obj).State = EntityState.Modified; db.SaveChanges(); }
        public void Dispose() { db.Dispose(); GC.SuppressFinalize(this); }
    }
}