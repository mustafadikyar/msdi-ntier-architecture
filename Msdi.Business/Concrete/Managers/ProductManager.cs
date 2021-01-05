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
using Msdi.Business.ValidationRules.FluentValidation;
using Msdi.Core.Aspects.Autofac.Validation;
using Msdi.Core.Aspects.Autofac.Transaction;
using Msdi.Core.Aspects.Autofac.Caching;
using Microsoft.AspNetCore.Http;
using Msdi.Authentication.Extensions;
using Msdi.Business.BusinessAspects.Autofac;
using Msdi.Core.Aspects.Autofac.Performance;
using System.Threading;
using Msdi.Core.Aspects.Autofac.Logging;
using Msdi.Core.CrossCuttingConcerns.Logging.Log4net.Loggers;

namespace Msdi.Business.Concrete.Managers
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly IMapper _mapper;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductManager(IProductDal productDal, IMapper mapper)
        {
            _productDal = productDal;
            _mapper = mapper;
        }

        //[CacheRemoveAspect(pattern: "IProductService.Get", Priority = 1)]
        //[CacheRemoveAspect(pattern: "ICategoryService.Get", Priority = 2)]
        [CacheAspect(600)]
        public IResult AddProduct(List<ProductDTO> productDTO)
        {
            try
            {
                //Step 1
                //ProductValidator productValidator = new ProductValidator();
                //var result = productValidator.Validate(productDTO);
                //if (!result.IsValid)
                //{
                //    throw new ValidationException(result.Errors);
                //}

                //Step2
                //ValidationTool.Validate(new ProductValidator(), productDTO);


                Product added = _mapper.Map<ProductDTO, Product>(productDTO.FirstOrDefault());
                added.IsActive = true;
                added.RegisterDate = DateTime.Now;
                _productDal.Add(added);
                return new SuccessResult(BaseMessages.Added);
            }
            catch (Exception ex)
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

        [SecuredOperation(roles: "Admin")]
        public IDataResult<ProductDTO> GetProduct(int id)
        {
            try
            {
                //var claimRoles = _httpContextAccessor.HttpContext.User.ClaimRoles();
                var product = _mapper.Map<Product, ProductDTO>(_productDal.Get(c => c.ProductId.Equals(id)));
                return new SuccessDataResult<ProductDTO>(product);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<ProductDTO>(ex.ToString());
            }
        }

        //[CacheAspect(duration: 60)]
        [PerformanceAspect(5)]
        //[LogAspect(typeof(FileLogger))]
        //[LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(600)]
        public IDataResult<List<Product>> GetProducts()
        {
            try
            {
                //Thread.Sleep(5000);
                return new SuccessDataResult<List<Product>>(_productDal.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<Product>>(ex.ToString());
            }
        }

        [ValidationAspect(typeof(ProductValidator))]
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

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Product product)
        {
            _productDal.Update(product);
            _productDal.Add(product);
            return new SuccessResult(BaseMessages.Updated);
        }
    }
}
