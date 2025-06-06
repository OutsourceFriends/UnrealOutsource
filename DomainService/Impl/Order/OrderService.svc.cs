using System;
using System.Collections.Generic;
using System.Linq;
using DomainService.Adds.Entities;
using DomainService.DbContext;
using DomainService.Objects.DTOs;
using DomainService.Objects.Services;

namespace DomainService.Impl.Order
{
    public class OrderService : IOrderService
    {
        public List<OrderDto> GetAllOrders()
        {
            using (var ctx = new MyProjectDbContext())
            {
                return ctx.Orders
                          .Select(o => new OrderDto
                          {
                              Id = o.Id,
                              SupplierId = o.SupplierId,
                              Date = o.Date,
                              TotalAmount = o.TotalAmount,
                              Items = o.OrderItems.Select(oi => new OrderItemDto
                              {
                                  Id = oi.Id,
                                  OrderId = oi.OrderId,
                                  ProductId = oi.ProductId,
                                  Quantity = oi.Quantity,
                                  UnitPrice = oi.UnitPrice
                              }).ToList()
                          })
                          .ToList();
            }
        }

        public OrderDto GetOrderById(int id)
        {
            using (var ctx = new MyProjectDbContext())
            {
                var o = ctx.Orders.Find(id);
                if (o == null) return null;
                return new OrderDto
                {
                    Id = o.Id,
                    SupplierId = o.SupplierId,
                    Date = o.Date,
                    TotalAmount = o.TotalAmount,
                    Items = o.OrderItems.Select(oi => new OrderItemDto
                    {
                        Id = oi.Id,
                        OrderId = oi.OrderId,
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice
                    }).ToList()
                };
            }
        }

        public bool CreateOrder(OrderDto order)
        {
            using (var ctx = new MyProjectDbContext())
            {
                var sup = ctx.Suppliers.Find(order.SupplierId);
                if (sup == null) return false;
                
                decimal total = 0;
                foreach (var item in order.Items)
                {
                    var prod = ctx.Products.Find(item.ProductId);
                    if (prod == null) return false;
                    total += item.Quantity * item.UnitPrice;
                }

                var entity = new Adds.Entities.Order
                {
                    SupplierId = order.SupplierId,
                    Date = order.Date == default(DateTime) ? DateTime.Now : order.Date,
                    TotalAmount = total
                };
                ctx.Orders.Add(entity);
                ctx.SaveChanges();
                
                foreach (var item in order.Items)
                {
                    var orderItemEntity = new OrderItem
                    {
                        OrderId = entity.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };
                    ctx.OrderItems.Add(orderItemEntity);
                    
                    var prod = ctx.Products.Find(item.ProductId);
                    if (prod != null)
                        prod.Quantity += item.Quantity;
                }
                ctx.SaveChanges();

                return true;
            }
        }

        public bool UpdateOrder(OrderDto order)
        {
            using (var ctx = new MyProjectDbContext())
            {
                var entity = ctx.Orders.Find(order.Id);
                if (entity == null) return false;

                entity.SupplierId = order.SupplierId;
                entity.Date = order.Date;
                
                foreach (var oldItem in ctx.OrderItems.Where(oi => oi.OrderId == order.Id))
                {
                    ctx.OrderItems.Remove(oldItem);
                }
                ctx.SaveChanges();

                decimal total = 0;
                foreach (var item in order.Items)
                {
                    var orderItemEntity = new OrderItem
                    {
                        OrderId = entity.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };
                    ctx.OrderItems.Add(orderItemEntity);
                    total += item.Quantity * item.UnitPrice;
                }
                entity.TotalAmount = total;
                ctx.SaveChanges();

                return true;
            }
        }

        public bool DeleteOrder(int id)
        {
            using (var ctx = new MyProjectDbContext())
            {
                var entity = ctx.Orders.Find(id);
                if (entity == null) return false;
                
                ctx.Orders.Remove(entity);
                ctx.SaveChanges();
                return true;
            }
        }
    }
}
