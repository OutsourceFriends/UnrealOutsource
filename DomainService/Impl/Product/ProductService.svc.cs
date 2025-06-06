using System.Collections.Generic;
using System.Linq;
using DomainService.DbContext;
using DomainService.Objects.DTOs;
using DomainService.Objects.Services;

namespace DomainService.Impl.Product
{
    public class ProductService : IProductService
    {
        public List<ProductDto> GetAllProducts()
        {
            using (var ctx = new MyProjectDbContext())
            {
                return ctx.Products
                          .Select(p => new ProductDto
                          {
                              Id = p.Id,
                              Name = p.Name,
                              SKU = p.SKU,
                              WarehouseId = p.WarehouseId,
                              Quantity = p.Quantity
                          })
                          .ToList();
            }
        }

        public ProductDto GetProductById(int id)
        {
            using (var ctx = new MyProjectDbContext())
            {
                var p = ctx.Products.Find(id);
                if (p == null) return null;
                return new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    SKU = p.SKU,
                    WarehouseId = p.WarehouseId,
                    Quantity = p.Quantity
                };
            }
        }

        public bool CreateProduct(ProductDto prod)
        {
            using (var ctx = new MyProjectDbContext())
            {
                if (ctx.Products.Any(x => x.SKU == prod.SKU))
                    return false;
                var entity = new Adds.Entities.Product
                {
                    Name = prod.Name,
                    SKU = prod.SKU,
                    WarehouseId = prod.WarehouseId,
                    Quantity = prod.Quantity
                };
                ctx.Products.Add(entity);
                ctx.SaveChanges();
                return true;
            }
        }

        public bool UpdateProduct(ProductDto prod)
        {
            using (var ctx = new MyProjectDbContext())
            {
                var entity = ctx.Products.Find(prod.Id);
                if (entity == null) return false;
                entity.Name = prod.Name;
                entity.SKU = prod.SKU;
                entity.WarehouseId = prod.WarehouseId;
                entity.Quantity = prod.Quantity;
                ctx.SaveChanges();
                return true;
            }
        }

        public bool DeleteProduct(int id)
        {
            using (var ctx = new MyProjectDbContext())
            {
                var entity = ctx.Products.Find(id);
                if (entity == null) return false;
                ctx.Products.Remove(entity);
                ctx.SaveChanges();
                return true;
            }
        }

        public List<ProductDto> GetProductsByWarehouse(int warehouseId)
        {
            using (var ctx = new MyProjectDbContext())
            {
                return ctx.Products
                          .Where(p => p.WarehouseId == warehouseId)
                          .Select(p => new ProductDto
                          {
                              Id = p.Id,
                              Name = p.Name,
                              SKU = p.SKU,
                              WarehouseId = p.WarehouseId,
                              Quantity = p.Quantity
                          })
                          .ToList();
            }
        }
    }
}
