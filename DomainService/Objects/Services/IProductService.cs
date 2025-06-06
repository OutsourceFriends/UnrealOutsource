using System.Collections.Generic;
using System.ServiceModel;
using DomainService.Objects.DTOs;

namespace DomainService.Objects.Services
{
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        List<ProductDto> GetAllProducts();

        [OperationContract]
        ProductDto GetProductById(int id);

        [OperationContract]
        bool CreateProduct(ProductDto prod);

        [OperationContract]
        bool UpdateProduct(ProductDto prod);

        [OperationContract]
        bool DeleteProduct(int id);

        [OperationContract]
        List<ProductDto> GetProductsByWarehouse(int warehouseId);
    }
}