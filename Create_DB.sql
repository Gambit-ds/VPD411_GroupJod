create database Storag

use Storag

create table Roles --роли для пользователей
(
	id INT NOT NULL PRIMARY KEY IDENTITY,
	code NVARCHAR(50) NOT NULL CHECK(code != '') unique
)

create table Users --Список людей. 
(
	id INT NOT NULL PRIMARY KEY IDENTITY,
	name NVARCHAR(250) NOT NULL CHECK(name != ''),
	sname NVARCHAR(250),
	fname NVARCHAR(250),
	type int not null CHECK(type in (0,1)) -- 0 - пользователи, 1 - покупатели
)

create table Account --Хранит логин пароль для входа пользователей
(
	id INT NOT NULL PRIMARY KEY IDENTITY,
	userid int not null references Users(id) UNIQUE, 
	roleid int not null references Roles(id),
	login NVARCHAR(250) NOT NULL CHECK(login != '') UNIQUE,
	password NVARCHAR(250) NOT NULL CHECK(password != '')
)

create table Category --Категории товаро
(
	id INT NOT NULL PRIMARY KEY IDENTITY,
	code NVARCHAR(50) NOT NULL CHECK(code != '') unique	
)

create table Nomenlist --Описание товара
(
	id INT NOT NULL PRIMARY KEY IDENTITY,
	catid int not null references Category(id),
	code NVARCHAR(50) NOT NULL CHECK(code != '') unique,
	description NVARCHAR(250),
	weight int,
	size int
)

create table Store --Склады
(
	id INT NOT NULL PRIMARY KEY IDENTITY,
	code NVARCHAR(50) NOT NULL CHECK(code != '') unique,	
	Longname NVARCHAR(250) NOT NULL CHECK(Longname != '')
)

create table Pricelist
(
	id INT NOT NULL PRIMARY KEY IDENTITY,
	nomenid int not null references Nomenlist(id),
	storeid int not null references Store(id),
	price DECIMAL(10,2)  NOT NULL CHECK(price > 0),
	quanttosale DECIMAL(10,3) NOT NULL CHECK (quanttosale > 0), --к продаже
	quantship DECIMAL(10,3) NOT NULL CHECK (quantship > 0), --к перемещению
	quantexpect DECIMAL(10,3) NOT NULL CHECK (quantexpect > 0), --ожидается
	quantreserve DECIMAL(10,3) NOT NULL CHECK (quantreserve > 0)--зарезирвировано для выдачи
)

create table Supplier --Поставщики
(
	id INT NOT NULL PRIMARY KEY IDENTITY,
	code NVARCHAR(50) NOT NULL CHECK(code != '') unique,	
	Longname NVARCHAR(250) NOT NULL CHECK(Longname != '')
)

create table Orderhead --Прием товара от поставщика (Шапка)
(
	id INT NOT NULL PRIMARY KEY IDENTITY,
	storeid int not null references Store(id),
	supid int not null references Supplier(id),
	status int not null CHECK(status in (0,1,2)), --0 - ожидает приемки, 1 - приемка, 2 - принят
	datastart DATETIME2 NOT NULL DEFAULT GETDATE(), --заполнить при создании
	dataend DATETIME2 --заполнить при status = 2
)

create table Orderspec --Прием товара от поставщика
(
	id INT NOT NULL PRIMARY KEY IDENTITY,
	headid int not null references Orderhead(id),
	nomenid int not null references Nomenlist(id),
	quant DECIMAL(10,3) NOT NULL CHECK (quant > 0),
	price DECIMAL(10,2)  NOT NULL CHECK(price > 0), --Цена на момент прихода
	CONSTRAINT UQ_Orderspec UNIQUE (headid, nomenid)
)

create table Transferhead --Перемещение товара между складами(Шапка)
(
	id INT NOT NULL PRIMARY KEY IDENTITY,
	storeout int not null references Store(id),
	storein int not null references Store(id),
	status int not null CHECK(status in (0,1,2)), --0 - отправлен, 1 - приемка, 2 - принят
	datastart DATETIME2 NOT NULL DEFAULT GETDATE(), --заполнить при создании
	dataend DATETIME2 --заполнить при status = 2
)

create table Transferspec --Перемещение товара между складами
(
	id INT NOT NULL PRIMARY KEY IDENTITY,
	headid int not null references Transferhead(id),
	nomenid int not null references Nomenlist(id),
	quant DECIMAL(10,3) NOT NULL CHECK (quant > 0),
	CONSTRAINT UQ_Transferspec UNIQUE (headid, nomenid)
)

create table Salehead --Накладная на выдачу товара(Шапка)
(
	id INT NOT NULL PRIMARY KEY IDENTITY,
	storeid int not null references Store(id),
	manager int not null references Users(id),
	client int not null references Users(id),
	doctype int not null CHECK(doctype in (0,1)), --0 - выдача товара, 1 - возврат товара
	status int not null CHECK(status in (0,1)), --0 - создана, 1 - отработана
	datastart DATETIME2 NOT NULL DEFAULT GETDATE(), --заполнить при создании
	dataend DATETIME2 --заполнить при status = 1
)

create table Salespec --Накладная на выдачу товара
(
	id INT NOT NULL PRIMARY KEY IDENTITY,
	headid int not null references Salehead(id),
	nomenid int not null references Nomenlist(id),
	quant DECIMAL(10,3) NOT NULL CHECK (quant > 0),
	CONSTRAINT UQ_Salespec UNIQUE (headid, nomenid)
)

create table Lookuptb --Справочная таблица, для описания статусов по таблицам
(
	id INT NOT NULL PRIMARY KEY IDENTITY,
	tablename NVARCHAR(50) NOT NULL,	
	status int not null,
	description NVARCHAR(100) NOT NULL
)


select * from Lookuptb;
select * from Salespec;
select * from Salehead;
select * from Transferspec;
select * from Transferhead;
select * from Orderspec;
select * from Orderhead;
select * from Supplier;
select * from Pricelist;
select * from Store;
select * from Nomenlist;
select * from Category;
select * from Account;
select * from Users;
select * from Roles;

select 
		p.id, 
		n.code,
		n.description,
		s.code,
		s.Longname,
		p.price,
		p.quanttosale as 'К продаже',
		p.quantexpect as 'Ожидается',
		p.quantreserve as 'Зарезирвировано',
		p.quantship as 'К перемещению'
from Pricelist p join Nomenlist n on n.id = p.nomenid join Store s on p.storeid = s.id