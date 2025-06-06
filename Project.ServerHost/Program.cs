using System.ServiceModel;
using DomainService.Impl.Movement;
using DomainService.Impl.Order;
using DomainService.Impl.Product;
using DomainService.Impl.Supplier;
using DomainService.Impl.User;
using DomainService.Impl.Warehouse;
using DomainService.Objects.Services;

namespace Project.ServerHost
{
    internal static class Program
    {
        private static void WaitKey(string message, ConsoleKey key)
        {
            do
            {
                Console.WriteLine(message);
            }
            while (Console.ReadKey().Key != key);
        }

        static void Main(string[] args)
        {
            const string serviceAddress = "127.0.0.1:8080";
            var binding = new NetTcpBinding(SecurityMode.None)
            {
                MaxReceivedMessageSize = 2147483647,
                MaxBufferSize = 2147483647,
                MaxBufferPoolSize = 2147483647,
                ReaderQuotas =
                {
                    MaxDepth = 2147483647,
                    MaxStringContentLength = 2147483647,
                    MaxArrayLength = 2147483647,
                    MaxBytesPerRead = 2147483647,
                    MaxNameTableCharCount = 2147483647
                }
            };

            // 1) UserService
            var userHost = new ServiceHost(typeof(UserService), new Uri($"net.tcp://{serviceAddress}/UserService"));
            userHost.AddServiceEndpoint(typeof(IUserService), binding, "");
            Console.WriteLine($"[Host] UserService слушает на net.tcp://{serviceAddress}/UserService");

            // 2) WarehouseService
            var whHost = new ServiceHost(typeof(WarehouseService), new Uri($"net.tcp://{serviceAddress}/WarehouseService"));
            whHost.AddServiceEndpoint(typeof(IWarehouseService), binding, "");
            Console.WriteLine($"[Host] WarehouseService слушает на net.tcp://{serviceAddress}/WarehouseService");

            // 3) ProductService
            var prodHost = new ServiceHost(typeof(ProductService), new Uri($"net.tcp://{serviceAddress}/ProductService"));
            prodHost.AddServiceEndpoint(typeof(IProductService), binding, "");
            Console.WriteLine($"[Host] ProductService слушает на net.tcp://{serviceAddress}/ProductService");

            // 4) MovementService
            var movHost = new ServiceHost(typeof(MovementService), new Uri($"net.tcp://{serviceAddress}/MovementService"));
            movHost.AddServiceEndpoint(typeof(IMovementService), binding, "");
            Console.WriteLine($"[Host] MovementService слушает на net.tcp://{serviceAddress}/MovementService");

            // 5) SupplierService
            var supHost = new ServiceHost(typeof(SupplierService), new Uri($"net.tcp://{serviceAddress}/SupplierService"));
            supHost.AddServiceEndpoint(typeof(ISupplierService), binding, "");
            Console.WriteLine($"[Host] SupplierService слушает на net.tcp://{serviceAddress}/SupplierService");

            // 6) OrderService
            var orderHost = new ServiceHost(typeof(OrderService), new Uri($"net.tcp://{serviceAddress}/OrderService"));
            orderHost.AddServiceEndpoint(typeof(IOrderService), binding, "");
            Console.WriteLine($"[Host] OrderService слушает на net.tcp://{serviceAddress}/OrderService");

            // Открываем все хосты
            userHost.Open();
            whHost.Open();
            prodHost.Open();
            movHost.Open();
            supHost.Open();
            orderHost.Open();

            Console.WriteLine("[Host] Все службы запущены. Нажмите ENTER для остановки.");
            WaitKey("Нажмите ENTER для выхода...", ConsoleKey.Enter);

            // Закрываем хосты
            orderHost.Close();
            supHost.Close();
            movHost.Close();
            prodHost.Close();
            whHost.Close();
            userHost.Close();

            Console.WriteLine("[Host] Все хосты остановлены. Завершение.");
        }
    }
}
