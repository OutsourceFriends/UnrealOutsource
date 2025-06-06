using System;
using System.Collections.Generic;
using System.Linq;
using DomainService.DbContext;
using DomainService.Objects.DTOs;
using DomainService.Objects.Services;

namespace DomainService.Impl.Movement
{
    public class MovementService : IMovementService
    {
        public List<MovementDto> GetAllMovements()
        {
            using (var ctx = new MyProjectDbContext())
            {
                return ctx.Movements
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
            using (var ctx = new MyProjectDbContext())
            {
                var m = ctx.Movements.Find(id);
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
            using (var ctx = new MyProjectDbContext())
            {
                var prodEntity = ctx.Products.Find(mov.ProductId);
                if (prodEntity == null)
                    return false;

                if (mov.FromWarehouseId.HasValue && prodEntity.WarehouseId == mov.FromWarehouseId.Value)
                {
                    if (prodEntity.Quantity < mov.Quantity)
                        return false; 

                    prodEntity.Quantity -= mov.Quantity;
                }

                if (mov.ToWarehouseId.HasValue && prodEntity.WarehouseId != mov.ToWarehouseId.Value)
                {
                    prodEntity.Quantity += mov.Quantity;
                    prodEntity.WarehouseId = mov.ToWarehouseId.Value;
                }

                var entity = new Adds.Entities.Movement
                {
                    ProductId = mov.ProductId,
                    FromWarehouseId = mov.FromWarehouseId,
                    ToWarehouseId = mov.ToWarehouseId,
                    Quantity = mov.Quantity,
                    Date = mov.Date == default(DateTime) ? DateTime.Now : mov.Date
                };
                ctx.Movements.Add(entity);
                ctx.SaveChanges();
                return true;
            }
        }

        public bool UpdateMovement(MovementDto mov)
        {
            using (var ctx = new MyProjectDbContext())
            {
                var entity = ctx.Movements.Find(mov.Id);
                if (entity == null) return false;
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
            using (var ctx = new MyProjectDbContext())
            {
                var entity = ctx.Movements.Find(id);
                if (entity == null) return false;
                ctx.Movements.Remove(entity);
                ctx.SaveChanges();
                return true;
            }
        }
    }
}
