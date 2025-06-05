using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.DomainService.Contracts;   // IOrderService
using MyProject.DomainService.Objects;     // OrderDto, OrderItemDto
using MyProject.DomainService.DbContext;    // MyProjectDBEntities

namespace MyProject.DomainService.Impl
{
    public class OrderService : IOrderService
    {
        public List<OrderDto> GetAllOrders()
        {
            using (var ctx = new MyProjectDBEntities())
            {
                return ctx.orders
                          .Select(o => new OrderDto
                          {
                              Id = o.Id,
                              SupplierId = o.SupplierId,
                              Date = o.Date,
                              TotalAmount = o.TotalAmount,
                              Items = o.order_items.Select(oi => new OrderItemDto
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
            using (var ctx = new MyProjectDBEntities())
            {
                var o = ctx.orders.Find(id);
                if (o == null) return null;
                return new OrderDto
                {
                    Id = o.Id,
                    SupplierId = o.SupplierId,
                    Date = o.Date,
                    TotalAmount = o.TotalAmount,
                    Items = o.order_items.Select(oi => new OrderItemDto
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
            using (var ctx = new MyProjectDBEntities())
            {
                // Проверяем поставщика
                var sup = ctx.suppliers.Find(order.SupplierId);
                if (sup == null) return false;

                // Рассчитываем общую сумму
                decimal total = 0;
                foreach (var item in order.Items)
                {
                    var prod = ctx.products.Find(item.ProductId);
                    if (prod == null) return false;
                    total += item.Quantity * item.UnitPrice;
                }

                var entity = new order
                {
                    SupplierId = order.SupplierId,
                    Date = order.Date == default(DateTime) ? DateTime.Now : order.Date,
                    TotalAmount = total
                };
                ctx.orders.Add(entity);
                ctx.SaveChanges();

                // После сохранения у entity.Id появится значение
                foreach (var item in order.Items)
                {
                    var orderItemEntity = new order_items
                    {
                        OrderId = entity.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };
                    ctx.order_items.Add(orderItemEntity);

                    // (Опционально) Списываем товар со склада поставщика:
                    var prod = ctx.products.Find(item.ProductId);
                    if (prod != null)
                        prod.Quantity += item.Quantity; // Предположим, что при заказе товар приходит на склад
                }
                ctx.SaveChanges();

                return true;
            }
        }

        public bool UpdateOrder(OrderDto order)
        {
            using (var ctx = new MyProjectDBEntities())
            {
                var entity = ctx.orders.Find(order.Id);
                if (entity == null) return false;

                // Возврат товара: 
                // 1) Читаем старые позиции: списываем «входящие» обратно?
                // Для упрощения — не обрабатываем откат, просто обновим поля:
                entity.SupplierId = order.SupplierId;
                entity.Date = order.Date;
                // Item‐ов можно удалить старые и заново записать новые:
                foreach (var oldItem in ctx.order_items.Where(oi => oi.OrderId == order.Id))
                {
                    ctx.order_items.Remove(oldItem);
                }
                ctx.SaveChanges();

                decimal total = 0;
                foreach (var item in order.Items)
                {
                    var orderItemEntity = new order_items
                    {
                        OrderId = entity.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };
                    ctx.order_items.Add(orderItemEntity);
                    total += item.Quantity * item.UnitPrice;
                }
                entity.TotalAmount = total;
                ctx.SaveChanges();

                return true;
            }
        }

        public bool DeleteOrder(int id)
        {
            using (var ctx = new MyProjectDBEntities())
            {
                var entity = ctx.orders.Find(id);
                if (entity == null) return false;

                // Удалятся и связанные order_items благодаря каскаду
                ctx.orders.Remove(entity);
                ctx.SaveChanges();
                return true;
            }
        }
    }
}
