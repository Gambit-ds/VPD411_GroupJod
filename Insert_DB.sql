use Storag

-- Роли
INSERT INTO Roles (code) VALUES
('admin'),
('manager'),
('user'),
('accountant'),
('guest');

-- Пользователи
INSERT INTO Users (name, sname, fname, type) VALUES
('Иван', 'Иванов', 'Иванович', 0),
('Петр', 'Петров', 'Петрович', 0),
('Сергей', 'Сергеев', 'Сергеевич', 0),
('Мария', 'Морозова', 'Михайловна', 1),
('Елена', 'Еленова', 'Егоровна', 1);

-- Аккаунты (только для type=0)
INSERT INTO Account (userid, roleid, login, password) VALUES
(1, 1, 'ivan_admin', 'pass123'),
(2, 2, 'petr_manager', 'pass123'),
(3, 3, 'sergey_user', 'pass123');

-- Категории товаров
INSERT INTO Category (code) VALUES
('cat_food'),
('cat_electronics'),
('cat_clothes'),
('cat_tools'),
('cat_toys');

-- Товары (Nomenlist)
INSERT INTO Nomenlist (catid, code, description, weight, size) VALUES
(1, 'apple', 'Яблоки красные', 1, 3),
(1, 'banana', 'Бананы', 1, 3),
(2, 'laptop', 'Ноутбук 15"', 2000, 40),
(3, 'jeans', 'Джинсы мужские', 500, 30),
(4, 'hammer', 'Молоток', 800, 25);

-- Склады
INSERT INTO Store (code, Longname) VALUES
('store_msk', 'Склад Москва'),
('store_spb', 'Склад Санкт-Петербург'),
('store_kzn', 'Склад Казань'),
('store_nsk', 'Склад Новосибирск'),
('store_sochi', 'Склад Сочи');

-- Pricelist
INSERT INTO Pricelist (nomenid, storeid, price, quanttosale, quantship, quantexpect, quantreserve) VALUES
(1,1,50,100,50,30,10),
(2,1,40,120,60,20,5),
(3,2,50000,10,5,2,1),
(4,3,2000,25,10,5,2),
(5,4,500,15,8,3,1);

-- Поставщики
INSERT INTO Supplier (code, Longname) VALUES
('sup_apple', 'Поставщик фруктов'),
('sup_electro', 'Поставщик электроники'),
('sup_clothes', 'Поставщик одежды'),
('sup_tools', 'Поставщик инструментов'),
('sup_toys', 'Поставщик игрушек');

-- Orderhead
INSERT INTO Orderhead (storeid, supid, status) VALUES
(1,1,2),
(2,2,1),
(3,3,0),
(4,4,2),
(5,5,1);

-- Orderspec
INSERT INTO Orderspec (headid, nomenid, quant, price) VALUES
(1,1,50,45),
(1,2,30,35),
(2,3,5,48000),
(3,4,10,1900),
(4,5,7,450);

-- Transferhead
INSERT INTO Transferhead (storeout, storein, status) VALUES
(1,2,2),
(2,3,1),
(3,4,0),
(4,5,2),
(5,1,1);

-- Transferspec
INSERT INTO Transferspec (headid, nomenid, quant) VALUES
(1,1,20),
(1,2,10),
(2,3,2),
(3,4,5),
(4,5,3);

-- Salehead
INSERT INTO Salehead (storeid, manager, client, doctype, status) VALUES
(1,1,4,0,1),
(2,2,5,0,0),
(3,3,4,1,1),
(4,1,5,0,1),
(5,2,4,1,0);

-- Salespec
INSERT INTO Salespec (headid, nomenid, quant) VALUES
(1,1,10),
(1,2,5),
(2,3,1),
(3,4,2),
(4,5,3);

-- Lookuptb
INSERT INTO Lookuptb (tablename, status, description) VALUES
('Orderhead',0,'Ожидает приемки'),
('Orderhead',1,'Приемка'),
('Orderhead',2,'Принят'),
('Transferhead',0,'Отправлен'),
('Transferhead',1,'Приемка'),
('Transferhead',2,'Принят'),
('Salehead',0,'Создана'),
('Salehead',1,'Отработана');
