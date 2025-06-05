using System.Collections.Generic;
using System.Linq;
using MyProject.DomainService.Contracts;   // ISupplierService
using MyProject.DomainService.Objects;     // SupplierDto
using MyProject.DomainService.DbContext;    // MyProjectDBEntities

namespace MyProject.DomainService.Impl
{
    public class SupplierService : ISupplierService
    {
        public List<SupplierDto> GetAllSuppliers()
        {
            using (var ctx = new MyProjectDBEntities())
            {
                return ctx.suppliers
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
            using (var ctx = new MyProjectDBEntities())
            {
                var s = ctx.suppliers.Find(id);
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
            using (var ctx = new MyProjectDBEntities())
            {
                if (ctx.suppliers.Any(x => x.Name == sup.Name))
                    return false;
                var entity = new supplier
                {
                    Name = sup.Name,
                    ContactInfo = sup.ContactInfo
                };
                ctx.suppliers.Add(entity);
                ctx.SaveChanges();
                return true;
            }
        }

        public bool UpdateSupplier(SupplierDto sup)
        {
            using (var ctx = new MyProjectDBEntities())
            {
                var entity = ctx.suppliers.Find(sup.Id);
                if (entity == null) return false;
                entity.Name = sup.Name;
                entity.ContactInfo = sup.ContactInfo;
                ctx.SaveChanges();
                return true;
            }
        }

        public bool DeleteSupplier(int id)
        {
            using (var ctx = new MyProjectDBEntities())
            {
                var entity = ctx.suppliers.Find(id);
                if (entity == null) return false;
                ctx.suppliers.Remove(entity);
                ctx.SaveChanges();
                return true;
            }
        }
    }
}
