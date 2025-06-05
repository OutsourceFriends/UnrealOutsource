using System.Collections.Generic;
using System.ServiceModel;
using MyProject.DomainService.Objects;

namespace MyProject.DomainService.Contracts
{
    [ServiceContract]
    public interface IMovementService
    {
        [OperationContract]
        List<MovementDto> GetAllMovements();

        [OperationContract]
        MovementDto GetMovementById(int id);

        [OperationContract]
        bool CreateMovement(MovementDto mov);

        [OperationContract]
        bool UpdateMovement(MovementDto mov);

        [OperationContract]
        bool DeleteMovement(int id);
    }
}