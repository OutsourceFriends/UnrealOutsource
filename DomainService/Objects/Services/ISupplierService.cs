using System.Collections.Generic;
using System.ServiceModel;
using DomainService.Objects.DTOs;

namespace DomainService.Objects.Services
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