using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Sklad_uchet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class Product
    {
        public string Name { get; set; }
        public int Quantity { get; set; } = 0; // Начальное количество товара
        public decimal Price { get; set; }
        public decimal TotalCost => Quantity * Price; // Вычисляемая стоимость
        public int Reserved { get; set; } = 0; // Начальное значение в резерве
    }

    public partial class MainWindow : Window
    {
        public ObservableCollection<Product> Products { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Products = new ObservableCollection<Product>();
            MainGrid.ItemsSource = Products; // Привязка коллекции к DataGrid
            GiveTakeChoice.ItemsSource = Products;
            ReserveChoice.ItemsSource = Products;
        }
        private void NewProductButton_Click(object sender, RoutedEventArgs e)
        {
            string productName = NewProductName.Text;
            string productPriceText = NewProductPrice.Text;
            decimal productPrice;

            if (decimal.TryParse(productPriceText, out productPrice))
            {
                // Проверяем, существует ли товар с таким же наименованием
                var existingProduct = Products.FirstOrDefault(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));

                if (existingProduct != null)
                {
                    // Если товар существует, обновляем его цену
                    existingProduct.Price = productPrice;
                    MessageBox.Show("Этот товар уже есть в таблице. Вы изменили только цену.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Если товар не существует, добавляем новый
                    Product newProduct = new Product
                    {
                        Name = productName,
                        Price = productPrice
                    };

                    Products.Add(newProduct);
                }

                // Очищаем текстбоксы после добавления или изменения
                NewProductName.Clear();
                NewProductPrice.Clear();
            }
            else
            {
                MessageBox.Show("Введите корректную цену!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GiveButton_Click(object sender, RoutedEventArgs e)
        {
            if (GiveTakeChoice.SelectedItem is Product selectedProduct && int.TryParse(GiveTakeQuantity.Text, out int quantityToAdd))
            {
                selectedProduct.Quantity += quantityToAdd; // Изменение свойства Quantity
                MessageBox.Show($"Добавлено {quantityToAdd} к {selectedProduct.Name}. Новое количество: {selectedProduct.Quantity}", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите товар и введите корректное количество для добавления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            RefreshDataGrid();
        }

        private void TakeButton_Click(object sender, RoutedEventArgs e)
        {
            if (GiveTakeChoice.SelectedItem is Product selectedProduct && int.TryParse(GiveTakeQuantity.Text, out int quantityToTake))
            {
                if (selectedProduct.Quantity >= quantityToTake)
                {
                    selectedProduct.Quantity -= quantityToTake;
                    MessageBox.Show($"Убрано {quantityToTake} от {selectedProduct.Name}. Новое количество: {selectedProduct.Quantity}", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Недостаточно товара для убавления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите товар и введите корректное количество для убавления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            RefreshDataGrid();
        }
        private void RefreshDataGrid()
        {
            // Принудительное обновление DataGrid
            MainGrid.Items.Refresh();
        }

        private void ReserveGiveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReserveChoice.SelectedItem is Product selectedProduct && int.TryParse(ReserveQuantity.Text, out int quantityToReserve))
            {
                if (selectedProduct.Quantity >= quantityToReserve)
                {
                    selectedProduct.Reserved += quantityToReserve; // Увеличиваем резерв
                    selectedProduct.Quantity -= quantityToReserve; // Уменьшаем текущее количество
                    RefreshDataGrid(); // Обновляем DataGrid
                    MessageBox.Show($"Добавлено {quantityToReserve} к резерву товара {selectedProduct.Name}.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Недостаточно товара для резервирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите товар и введите корректное количество для резервирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ReserveTakeButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReserveChoice.SelectedItem is Product selectedProduct && int.TryParse(ReserveQuantity.Text, out int quantityToRelease))
            {
                if (selectedProduct.Reserved >= quantityToRelease)
                {
                    selectedProduct.Reserved -= quantityToRelease; // Уменьшаем резерв
                    RefreshDataGrid(); // Обновляем DataGrid
                    MessageBox.Show($"Снято {quantityToRelease} с резерва товара {selectedProduct.Name}.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Недостаточно товара в резерве для снятия.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите товар и введите корректное количество для снятия с резерва.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SumButton_Click(object sender, RoutedEventArgs e)
        {
            decimal totalSum = 0;

            foreach (var product in Products)
            {
                totalSum += product.TotalCost; // Суммируем стоимость каждого товара
            }

            SumTable.Text = totalSum.ToString("F2"); // Выводим сумму в TextBox с двумя знаками после запятой
        }
    }
}