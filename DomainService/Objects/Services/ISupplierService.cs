using System.Collections.Generic;
using System.ServiceModel;
using MyProject.DomainService.Objects;

namespace MyProject.DomainService.Contracts
{
    [ServiceContract]
    public interface ISupplierService
    {
        [OperationContract]
        List<SupplierDto> GetAllSuppliers();

        [OperationContract]
        SupplierDto GetSupplierById(int id);

        [OperationContract]
        bool CreateSupplier(SupplierDto sup);

        [OperationContract]
        bool UpdateSupplier(SupplierDto sup);

        [OperationContract]
        bool DeleteSupplier(int id);
    }
}