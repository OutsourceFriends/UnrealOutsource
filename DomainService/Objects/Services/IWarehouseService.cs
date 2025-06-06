using System.Collections.Generic;
using System.ServiceModel;
using DomainService.Objects.DTOs;

namespace DomainService.Objects.Services
{
    [ServiceContract]
    public interface IWarehouseService
    {
        [OperationContract]
        List<WarehouseDto> GetAllWarehouses();

        [OperationContract]
        WarehouseDto GetWarehouseById(int id);

        [OperationContract]
        bool CreateWarehouse(WarehouseDto wh);

        [OperationContract]
        bool UpdateWarehouse(WarehouseDto wh);

        [OperationContract]
        bool DeleteWarehouse(int id);
    }
}