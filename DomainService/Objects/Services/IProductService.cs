using System.Collections.Generic;
using System.ServiceModel;
using MyProject.DomainService.Objects;

namespace MyProject.DomainService
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