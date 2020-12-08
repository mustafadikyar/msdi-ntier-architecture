using Msdi.Core.DataAccess;
using Msdi.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Msdi.DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
    }
}
