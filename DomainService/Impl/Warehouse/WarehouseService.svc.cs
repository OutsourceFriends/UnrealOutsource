using System.Collections.Generic;
using System.Linq;
using DomainService.DbContext;
using DomainService.Objects.DTOs;
using DomainService.Objects.Services;

namespace DomainService.Impl.Warehouse
{
    public class WarehouseService : IWarehouseService
    {
        public List<WarehouseDto> GetAllWarehouses()
        {
            using (var ctx = new MyProjectDbContext())
            {
                return ctx.Warehouses
                          .Select(w => new WarehouseDto
                          {
                              Id = w.Id,
                              Name = w.Name,
                              Location = w.Location
                          })
                          .ToList();
            }
        }

        public WarehouseDto GetWarehouseById(int id)
        {
            using (var ctx = new MyProjectDbContext())
            {
                var w = ctx.Warehouses.Find(id);
                if (w == null) return null;
                return new WarehouseDto { Id = w.Id, Name = w.Name, Location = w.Location };
            }
        }

        public bool CreateWarehouse(WarehouseDto wh)
        {
            using (var ctx = new MyProjectDbContext())
            {
                if (ctx.Warehouses.Any(x => x.Name == wh.Name))
                    return false;
                var entity = new Adds.Entities.Warehouse
                {
                    Name = wh.Name,
                    Location = wh.Location
                };
                ctx.Warehouses.Add(entity);
                ctx.SaveChanges();
                return true;
            }
        }

        public bool UpdateWarehouse(WarehouseDto wh)
        {
            using (var ctx = new MyProjectDbContext())
            {
                var entity = ctx.Warehouses.Find(wh.Id);
                if (entity == null) return false;
                entity.Name = wh.Name;
                entity.Location = wh.Location;
                ctx.SaveChanges();
                return true;
            }
        }

        public bool DeleteWarehouse(int id)
        {
            using (var ctx = new MyProjectDbContext())
            {
                var entity = ctx.Warehouses.Find(id);
                if (entity == null) return false;
                ctx.Warehouses.Remove(entity);
                ctx.SaveChanges();
                return true;
            }
        }
    }
}
