using Msdi.Entities.Concrete;
using Msdi.Exceptions.Abstract;
using Msdi.ViewModels.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Msdi.Business.Abstract
{
    public interface IProductService
    {
        IDataResult<ProductDTO> GetProduct(int id);
        IDataResult<List<Product>> GetProducts();
        IResult AddProduct(ProductDTO productDTO);
        IResult UpdateProduct(Product product);
        IResult DeleteProduct(Product product);

        IResult TransactionalOperation(Product product);
    }
}
