-- ТАБЛИЦА ПОЛЬЗОВАТЕЛЕЙ
CREATE TABLE IF NOT EXISTS `users` (
                                       `Id` INT AUTO_INCREMENT PRIMARY KEY,
                                       `UserName` VARCHAR(50) NOT NULL UNIQUE,
    `PasswordHash` VARCHAR(255) NOT NULL,
    `FullName` VARCHAR(100) NOT NULL,
    `Role` VARCHAR(20) NOT NULL DEFAULT 'User'
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ТАБЛИЦА СКЛАДОВ
CREATE TABLE IF NOT EXISTS `warehouses` (
                                            `Id` INT AUTO_INCREMENT PRIMARY KEY,
                                            `Name` VARCHAR(100) NOT NULL UNIQUE,
    `Location` VARCHAR(100) NULL
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ТАБЛИЦА ТОВАРОВ
CREATE TABLE IF NOT EXISTS `products` (
                                          `Id` INT AUTO_INCREMENT PRIMARY KEY,
                                          `Name` VARCHAR(100) NOT NULL,
    `SKU` VARCHAR(50) NOT NULL UNIQUE,
    `WarehouseId` INT NOT NULL,
    `Quantity` INT NOT NULL DEFAULT 0,
    FOREIGN KEY (`WarehouseId`) REFERENCES `warehouses`(`Id`) ON DELETE CASCADE
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ТАБЛИЦА ПЕРЕМЕЩЕНИЙ (MOVEMENTS)
CREATE TABLE IF NOT EXISTS `movements` (
                                           `Id` INT AUTO_INCREMENT PRIMARY KEY,
                                           `ProductId` INT NOT NULL,
                                           `FromWarehouseId` INT NULL,
                                           `ToWarehouseId` INT NULL,
                                           `Quantity` INT NOT NULL,
                                           `Date` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                                           FOREIGN KEY (`ProductId`) REFERENCES `products`(`Id`) ON DELETE CASCADE,
    FOREIGN KEY (`FromWarehouseId`) REFERENCES `warehouses`(`Id`) ON DELETE SET NULL,
    FOREIGN KEY (`ToWarehouseId`) REFERENCES `warehouses`(`Id`) ON DELETE SET NULL
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ТАБЛИЦА ПОСТАВОЩИКОВ
CREATE TABLE IF NOT EXISTS `suppliers` (
                                           `Id` INT AUTO_INCREMENT PRIMARY KEY,
                                           `Name` VARCHAR(100) NOT NULL,
    `ContactInfo` VARCHAR(255) NULL
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ТАБЛИЦА ЗАКАЗОВ (ORDERS)
CREATE TABLE IF NOT EXISTS `orders` (
                                        `Id` INT AUTO_INCREMENT PRIMARY KEY,
                                        `SupplierId` INT NOT NULL,
                                        `Date` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                                        `TotalAmount` DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    FOREIGN KEY (`SupplierId`) REFERENCES `suppliers`(`Id`) ON DELETE CASCADE
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ТАБЛИЦА ЭЛЕМЕНТОВ ЗАКАЗА (ORDER_ITEMS)
CREATE TABLE IF NOT EXISTS `order_items` (
                                             `Id` INT AUTO_INCREMENT PRIMARY KEY,
                                             `OrderId` INT NOT NULL,
                                             `ProductId` INT NOT NULL,
                                             `Quantity` INT NOT NULL,
                                             `UnitPrice` DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (`OrderId`) REFERENCES `orders`(`Id`) ON DELETE CASCADE,
    FOREIGN KEY (`ProductId`) REFERENCES `products`(`Id`) ON DELETE CASCADE
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8;
