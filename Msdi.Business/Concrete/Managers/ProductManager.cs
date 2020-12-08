using AutoMapper;
using Msdi.Business.Abstract;
using Msdi.DataAccess.Abstract;
using Msdi.Entities.Concrete;
using Msdi.Exceptions.Abstract;
using Msdi.Exceptions.Results;
using Msdi.ViewModels.DTOs;
using Msdi.ViewModels.Constants.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Msdi.Business.Concrete.Managers
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly IMapper _mapper;

        public ProductManager(IProductDal productDal, IMapper mapper)
        {
            _productDal = productDal;
            _mapper = mapper;
        }
        public IResult AddProduct(ProductDTO productDTO)
        {
            try
            {
                Product added = _mapper.Map<ProductDTO, Product>(productDTO);
                _productDal.Add(added);
                return new SuccessResult(BaseMessages.Added);
            }
            catch (Exception)
            {
                return new ErrorResult(BaseMessages.Error);
            }
        }

        public IResult DeleteProduct(Product product)
        {
            try
            {
                _productDal.Delete(product);
                return new SuccessResult(BaseMessages.Deleted);
            }
            catch (Exception)
            {
                return new ErrorResult(BaseMessages.Error);
            }
        }

        public IDataResult<ProductDTO> GetProduct(int id)
        {
            try
            {
                var product = _mapper.Map<Product, ProductDTO>(_productDal.Get(c => c.ProductId.Equals(id)));
                return new SuccessDataResult<ProductDTO>(product);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<ProductDTO>(ex.ToString());
            }
        }

        public IDataResult<List<Product>> GetProducts()
        {
            try
            {
                return new SuccessDataResult<List<Product>>(_productDal.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<Product>>(ex.ToString());
            }
        }

        public IResult UpdateProduct(Product product)
        {
            try
            {
                _productDal.Update(product);
                return new SuccessResult(BaseMessages.Updated);
            }
            catch (Exception)
            {
                return new ErrorResult(BaseMessages.Error);
            }
        }
    }
}
