using System.Collections.Generic;
using System.Linq;
using DomainService.DbContext;
using DomainService.Objects.DTOs;
using DomainService.Objects.Services;

namespace DomainService.Impl.Supplier
{
    public class SupplierService : ISupplierService
    {
        public List<SupplierDto> GetAllSuppliers()
        {
            using (var ctx = new MyProjectDbContext())
            {
                return ctx.Suppliers
                          .Select(s => new SupplierDto
                          {
                              Id = s.Id,
                              Name = s.Name,
                              ContactInfo = s.ContactInfo
                          })
                          .ToList();
            }
        }

        public SupplierDto GetSupplierById(int id)
        {
            using (var ctx = new MyProjectDbContext())
            {
                var s = ctx.Suppliers.Find(id);
                if (s == null) return null;
                return new SupplierDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    ContactInfo = s.ContactInfo
                };
            }
        }

        public bool CreateSupplier(SupplierDto sup)
        {
            using (var ctx = new MyProjectDbContext())
            {
                if (ctx.Suppliers.Any(x => x.Name == sup.Name))
                    return false;
                var entity = new Adds.Entities.Supplier
                {
                    Name = sup.Name,
                    ContactInfo = sup.ContactInfo
                };
                ctx.Suppliers.Add(entity);
                ctx.SaveChanges();
                return true;
            }
        }

        public bool UpdateSupplier(SupplierDto sup)
        {
            using (var ctx = new MyProjectDbContext())
            {
                var entity = ctx.Suppliers.Find(sup.Id);
                if (entity == null) return false;
                entity.Name = sup.Name;
                entity.ContactInfo = sup.ContactInfo;
                ctx.SaveChanges();
                return true;
            }
        }

        public bool DeleteSupplier(int id)
        {
            using (var ctx = new MyProjectDbContext())
            {
                var entity = ctx.Suppliers.Find(id);
                if (entity == null) return false;
                ctx.Suppliers.Remove(entity);
                ctx.SaveChanges();
                return true;
            }
        }
    }
}
