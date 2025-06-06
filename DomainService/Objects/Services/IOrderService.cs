using System.Collections.Generic;
using System.ServiceModel;
using DomainService.Objects.DTOs;

namespace DomainService.Objects.Services
{
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        List<OrderDto> GetAllOrders();

        [OperationContract]
        OrderDto GetOrderById(int id);

        [OperationContract]
        bool CreateOrder(OrderDto order);

        [OperationContract]
        bool UpdateOrder(OrderDto order);

        [OperationContract]
        bool DeleteOrder(int id);
    }
}