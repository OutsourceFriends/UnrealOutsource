using System;
using System.ServiceModel;
using MyProject.DomainService.Impl;     // реализации ваших сервисов
using MyProject.DomainService.Contracts; // контракты (для справки)

namespace MyProject.ServerHost
{
    internal class Program
    {
        public static void WaitKey(string message, ConsoleKey key)
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
                // Здесь можно настроить BufferSize, Timeouts и т. д.
            };

            // 1) UserService
            var userHost = new ServiceHost(typeof(UserService), new Uri($"net.tcp://{serviceAddress}/UserService"));
            userHost.AddServiceEndpoint(typeof(IUserService), binding, "");
            Console.WriteLine($"[Host] UserService запущен по адресу net.tcp://{serviceAddress}/UserService");

            // 2) WarehouseService
            var whHost = new ServiceHost(typeof(WarehouseService), new Uri($"net.tcp://{serviceAddress}/WarehouseService"));
            whHost.AddServiceEndpoint(typeof(IWarehouseService), binding, "");
            Console.WriteLine($"[Host] WarehouseService запущен по адресу net.tcp://{serviceAddress}/WarehouseService");

            // 3) ProductService
            var prodHost = new ServiceHost(typeof(ProductService), new Uri($"net.tcp://{serviceAddress}/ProductService"));
            prodHost.AddServiceEndpoint(typeof(IProductService), binding, "");
            Console.WriteLine($"[Host] ProductService запущен по адресу net.tcp://{serviceAddress}/ProductService");

            // 4) MovementService
            var movHost = new ServiceHost(typeof(MovementService), new Uri($"net.tcp://{serviceAddress}/MovementService"));
            movHost.AddServiceEndpoint(typeof(IMovementService), binding, "");
            Console.WriteLine($"[Host] MovementService запущен по адресу net.tcp://{serviceAddress}/MovementService");

            // 5) SupplierService
            var supHost = new ServiceHost(typeof(SupplierService), new Uri($"net.tcp://{serviceAddress}/SupplierService"));
            supHost.AddServiceEndpoint(typeof(ISupplierService), binding, "");
            Console.WriteLine($"[Host] SupplierService запущен по адресу net.tcp://{serviceAddress}/SupplierService");

            // 6) OrderService
            var orderHost = new ServiceHost(typeof(OrderService), new Uri($"net.tcp://{serviceAddress}/OrderService"));
            orderHost.AddServiceEndpoint(typeof(IOrderService), binding, "");
            Console.WriteLine($"[Host] OrderService запущен по адресу net.tcp://{serviceAddress}/OrderService");

            // Открываем все хосты
            userHost.Open();
            whHost.Open();
            prodHost.Open();
            movHost.Open();
            supHost.Open();
            orderHost.Open();

            Console.WriteLine("[Host] Все службы успешно запущены. Нажмите ENTER, чтобы завершить работу.");

            WaitKey("Нажмите ENTER для остановки хостов...", ConsoleKey.Enter);

            // Закрываем хосты
            userHost.Close();
            whHost.Close();
            prodHost.Close();
            movHost.Close();
            supHost.Close();
            orderHost.Close();

            Console.WriteLine("[Host] Все хосты остановлены. Завершение.");
        }
    }
}
