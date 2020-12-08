using Msdi.Core.DataAccess.EntityFramework;
using Msdi.DataAccess.Abstract;
using Msdi.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Msdi.DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EFEntityRepositoryBase<Product, Msdi_2020_DbContext>, IProductDal
    {
    }
}
