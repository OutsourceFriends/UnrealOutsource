-- Пример данных для складов:
INSERT INTO `warehouses` (`Name`, `Location`) VALUES
                                                  ('Основной склад', 'Москва, ул. Пушкина, д.1'),
                                                  ('Резервный склад', 'Санкт-Петербург, ул. Ленина, д.10');

-- Пример товаров:
INSERT INTO `products` (`Name`, `SKU`, `WarehouseId`, `Quantity`) VALUES
                                                                      ('Ноутбук Lenovo', 'LENOVO-001', 1, 10),
                                                                      ('Клавиатура Logitech', 'LOGI-123', 1, 25),
                                                                      ('Монитор Samsung', 'SAM-456', 2, 5);

-- Пример поставщиков:
INSERT INTO `suppliers` (`Name`, `ContactInfo`) VALUES
                                                    ('ООО “Компьютерные технологии”', 'tech@example.com'),
                                                    ('ООО “Периферия”', 'sales@periphery.ru');
