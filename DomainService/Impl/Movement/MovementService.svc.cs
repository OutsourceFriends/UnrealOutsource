using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.DomainService.Contracts;   // IMovementService
using MyProject.DomainService.Objects;     // MovementDto
using MyProject.DomainService.DbContext;    // MyProjectDBEntities

namespace MyProject.DomainService.Impl
{
    public class MovementService : IMovementService
    {
        public List<MovementDto> GetAllMovements()
        {
            using (var ctx = new MyProjectDBEntities())
            {
                return ctx.movements
                          .Select(m => new MovementDto
                          {
                              Id = m.Id,
                              ProductId = m.ProductId,
                              FromWarehouseId = m.FromWarehouseId,
                              ToWarehouseId = m.ToWarehouseId,
                              Quantity = m.Quantity,
                              Date = m.Date
                          })
                          .ToList();
            }
        }

        public MovementDto GetMovementById(int id)
        {
            using (var ctx = new MyProjectDBEntities())
            {
                var m = ctx.movements.Find(id);
                if (m == null) return null;
                return new MovementDto
                {
                    Id = m.Id,
                    ProductId = m.ProductId,
                    FromWarehouseId = m.FromWarehouseId,
                    ToWarehouseId = m.ToWarehouseId,
                    Quantity = m.Quantity,
                    Date = m.Date
                };
            }
        }

        public bool CreateMovement(MovementDto mov)
        {
            using (var ctx = new MyProjectDBEntities())
            {
                // Проверим, что товар существует и на складе FromInventory хватит
                var prodEntity = ctx.products.Find(mov.ProductId);
                if (prodEntity == null)
                    return false;

                if (mov.FromWarehouseId.HasValue && prodEntity.WarehouseId == mov.FromWarehouseId.Value)
                {
                    // Проверка наличия на складе
                    if (prodEntity.Quantity < mov.Quantity)
                        return false; // недостаточно товара

                    prodEntity.Quantity -= mov.Quantity;
                }

                if (mov.ToWarehouseId.HasValue && prodEntity.WarehouseId != mov.ToWarehouseId.Value)
                {
                    // Если перемещаем в другой склад
                    // Создадим новый объект (или изменим склад у существующего товара) —
                    // в упрощённой модели считаем, что один товар может находиться только на одном складе.
                    // Можно усложнить, разбивая по записям, но для примера:
                    prodEntity.Quantity += mov.Quantity;
                    prodEntity.WarehouseId = mov.ToWarehouseId.Value;
                }

                var entity = new movement
                {
                    ProductId = mov.ProductId,
                    FromWarehouseId = mov.FromWarehouseId,
                    ToWarehouseId = mov.ToWarehouseId,
                    Quantity = mov.Quantity,
                    Date = mov.Date == default(DateTime) ? DateTime.Now : mov.Date
                };
                ctx.movements.Add(entity);
                ctx.SaveChanges();
                return true;
            }
        }

        public bool UpdateMovement(MovementDto mov)
        {
            using (var ctx = new MyProjectDBEntities())
            {
                var entity = ctx.movements.Find(mov.Id);
                if (entity == null) return false;

                // В реальной задаче корректировка перемещения — сложная операция: 
                // нужно вернуть товар на старый склад и списать/прибавить на новый. 
                // Для простоты пропустим логику, просто обновим поля:
                entity.ProductId = mov.ProductId;
                entity.FromWarehouseId = mov.FromWarehouseId;
                entity.ToWarehouseId = mov.ToWarehouseId;
                entity.Quantity = mov.Quantity;
                entity.Date = mov.Date;

                ctx.SaveChanges();
                return true;
            }
        }

        public bool DeleteMovement(int id)
        {
            using (var ctx = new MyProjectDBEntities())
            {
                var entity = ctx.movements.Find(id);
                if (entity == null) return false;
                ctx.movements.Remove(entity);
                ctx.SaveChanges();
                return true;
            }
        }
    }
}
