﻿using System.Collections.Generic;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories.Common;

namespace Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories
{
    public interface IDependenteRepository : IRepostitoryBase<Dependente>  {
        IEnumerable<Dependente> GetDependentes(int iCodTrabalhador);
    }
}