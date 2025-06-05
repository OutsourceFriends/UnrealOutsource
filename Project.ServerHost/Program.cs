using System;
using System.ServiceModel;
using MyProject.DomainService.Impl;
using MyProject.DomainService;

namespace Project.ServerHost
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
            var binding = new NetTcpBinding(SecurityMode.None);

            // UserService
            var userHost = new ServiceHost(typeof(UserService), new Uri($"net.tcp://{serviceAddress}/UserService"));
            userHost.AddServiceEndpoint(typeof(IUserService), binding, "");
            Console.WriteLine($"UserService hosted at net.tcp://{serviceAddress}/UserService");

            // WarehouseService
            var whHost = new ServiceHost(typeof(WarehouseService), new Uri($"net.tcp://{serviceAddress}/WarehouseService"));
            whHost.AddServiceEndpoint(typeof(IWarehouseService), binding, "");
            Console.WriteLine($"WarehouseService hosted at net.tcp://{serviceAddress}/WarehouseService");

            // ProductService
            var prodHost = new ServiceHost(typeof(ProductService), new Uri($"net.tcp://{serviceAddress}/ProductService"));
            prodHost.AddServiceEndpoint(typeof(IProductService), binding, "");
            Console.WriteLine($"ProductService hosted at net.tcp://{serviceAddress}/ProductService");

            // Открываем хосты
            userHost.Open();
            whHost.Open();
            prodHost.Open();
            Console.WriteLine("Все службы запущены. Ожидаем нажатия ENTER для завершения.");

            WaitKey("Нажмите ENTER для остановки всех хостов...", ConsoleKey.Enter);

            // Закрываем хосты
            userHost.Close();
            whHost.Close();
            prodHost.Close();
            Console.WriteLine("Все хосты остановлены. Выход.");
        }
    }
}
