using System.Collections.Generic;
using System.Linq;
using MyProject.DomainService.Objects;

namespace MyProject.DomainService.Impl
{
    public class WarehouseService : IWarehouseService
    {
        public List<WarehouseDto> GetAllWarehouses()
        {
            using (var ctx = new Myproject_dbEntities())
            {
                return ctx.warehouses
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
            using (var ctx = new Myproject_dbEntities())
            {
                var w = ctx.warehouses.Find(id);
                if (w == null) return null;
                return new WarehouseDto { Id = w.Id, Name = w.Name, Location = w.Location };
            }
        }

        public bool CreateWarehouse(WarehouseDto wh)
        {
            using (var ctx = new Myproject_dbEntities())
            {
                if (ctx.warehouses.Any(x => x.Name == wh.Name))
                    return false;
                var entity = new warehouse
                {
                    Name = wh.Name,
                    Location = wh.Location
                };
                ctx.warehouses.Add(entity);
                ctx.SaveChanges();
                return true;
            }
        }

        public bool UpdateWarehouse(WarehouseDto wh)
        {
            using (var ctx = new Myproject_dbEntities())
            {
                var entity = ctx.warehouses.Find(wh.Id);
                if (entity == null) return false;
                entity.Name = wh.Name;
                entity.Location = wh.Location;
                ctx.SaveChanges();
                return true;
            }
        }

        public bool DeleteWarehouse(int id)
        {
            using (var ctx = new Myproject_dbEntities())
            {
                var entity = ctx.warehouses.Find(id);
                if (entity == null) return false;
                ctx.warehouses.Remove(entity);
                ctx.SaveChanges();
                return true;
            }
        }
    }
}
