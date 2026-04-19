use Storag
go

/* =========================================================
   СКЛАДЫ
   ========================================================= */

create or alter procedure dbo.StoresGetAll
as
begin
    set nocount on;

    select
        id as storeid,
        code as storecode,
        Longname as storelongname
    from Store
    order by Longname;
end
go

/* =========================================================
   ОСТАТКИ ДЛЯ МЕНЕДЖЕРА
   Основано на актуальном dbo.v_Pricelist
   ========================================================= */

create or alter procedure dbo.ManagerStockGet
    @StoreId int = null,
    @ProductId int = null
as
begin
    set nocount on;

    select
        priceid,
        storeid,
        storecode,
        storeLongname as storelongname,
        nomenid as productid,
        nomencode as productcode,
        description,
        price,
        quanttosale,
        quantreserve,
        quantexpect,
        quantship
    from dbo.v_Pricelist
    where (@StoreId is null or storeid = @StoreId)
      and (@ProductId is null or nomenid = @ProductId)
    order by storeLongname, nomencode;
end
go

/* =========================================================
   ШАПКИ ЗАКАЗОВ МЕНЕДЖЕРА
   Основано на актуальном dbo.v_SalesHead
   ========================================================= */

create or alter procedure dbo.ManagerSaleHeadsGetByManager
    @ManagerId int
as
begin
    set nocount on;

    select
        id as saleheadid,
        storeid,
        storecode,
        storeLongname as storelongname,
        managerid,
        mgLongname as managername,
        clientid,
        clilongname as clientname,
        doctype,
        doctypecode,
        status,
        statuscode,
        datastart,
        dataend
    from dbo.v_SalesHead
    where managerid = @ManagerId
      and doctype = 0
    order by status, datastart desc, id desc;
end
go

/* =========================================================
   СТРОКИ ЗАКАЗА МЕНЕДЖЕРА
   Основано на актуальном dbo.v_SaleSpec
   ========================================================= */

create or alter procedure dbo.ManagerSaleSpecsGetByHead
    @HeadId int
as
begin
    set nocount on;

    select
        id as salespecid,
        headid as saleheadid,
        nomenid as productid,
        nomencode as productcode,
        description,
        quant,
        price
    from dbo.v_SaleSpec
    where headid = @HeadId
    order by id;
end
go

/* =========================================================
   СОЗДАНИЕ ЗАКАЗА КЛИЕНТА
   Используем существующую таблицу Salehead
   status = 0, doctype = 0
   ========================================================= */

create or alter procedure dbo.ManagerSaleHeadAdd
    @StoreId int,
    @ManagerId int,
    @ClientId int
as
begin
    set nocount on;
    set xact_abort on;

    declare @SaleHeadId int;

    begin try
        if not exists (select 1 from Store where id = @StoreId)
            throw 54001, N'Склад не найден.', 1;

        if not exists (select 1 from Users where id = @ManagerId and type = 0)
            throw 54002, N'Менеджер не найден.', 1;

        if not exists (select 1 from Users where id = @ClientId and type = 1)
            throw 54003, N'Клиент не найден.', 1;

        insert into Salehead(storeid, manager, client, doctype, status)
        values(@StoreId, @ManagerId, @ClientId, 0, 0);

        set @SaleHeadId = scope_identity();

        select @SaleHeadId as saleheadid;
    end try
    begin catch
        throw;
    end catch
end
go

/* =========================================================
   ДОБАВЛЕНИЕ СТРОКИ В ЗАКАЗ МЕНЕДЖЕРА
   Логика взята из твоего существующего SalespecAdd
   ========================================================= */

create or alter procedure dbo.ManagerSalespecAdd
    @HeadId int,
    @NomenId int,
    @Quant decimal(10,3)
as
begin
    set nocount on;
    set xact_abort on;

    declare @quanttosale decimal(10,3);
    declare @prid int;
    declare @store int;
    declare @status int;
    declare @Id int;

    begin try
        if @Quant <= 0
            throw 54010, N'Количество должно быть больше нуля.', 1;

        begin transaction;

        select
            @store = storeid,
            @status = status
        from Salehead
        where id = @HeadId;

        if @store is null
            throw 54011, N'Накладная не найдена.', 1;

        if @status <> 0
            throw 54012, N'В эту накладную нельзя добавлять строки.', 1;

        if not exists (
            select 1
            from Nomenlist
            where id = @NomenId
        )
            throw 54013, N'Такой товар не найден.', 1;

        if exists (
            select 1
            from Salespec
            where headid = @HeadId
              and nomenid = @NomenId
        )
            throw 54014, N'Этот товар уже есть в накладной.', 1;

        select
            @prid = id,
            @quanttosale = quanttosale
        from Pricelist
        where nomenid = @NomenId
          and storeid = @store;

        if @prid is null
            throw 54015, N'Товар не найден на складе.', 1;

        if @quanttosale < @Quant
            throw 54016, N'Количество недостаточно для продажи.', 1;

        update Pricelist
        set
            quanttosale = quanttosale - @Quant,
            quantreserve = quantreserve + @Quant
        where id = @prid;

        insert into Salespec(headid, nomenid, quant)
        values(@HeadId, @NomenId, @Quant);

        set @Id = scope_identity();

        commit transaction;

        select @Id as salespecid;
    end try
    begin catch
        if @@trancount > 0
            rollback transaction;

        throw;
    end catch
end
go

/* =========================================================
   УДАЛЕНИЕ СТРОКИ ИЗ ЗАКАЗА МЕНЕДЖЕРА
   Резерв возвращается обратно в quanttosale
   ========================================================= */

create or alter procedure dbo.ManagerSalespecDelete
    @SpecId int
as
begin
    set nocount on;
    set xact_abort on;

    declare @HeadId int;
    declare @NomenId int;
    declare @Quant decimal(10,3);
    declare @StoreId int;
    declare @Status int;
    declare @PriceId int;

    begin try
        select
            @HeadId = s.headid,
            @NomenId = s.nomenid,
            @Quant = s.quant
        from Salespec s
        where s.id = @SpecId;

        if @HeadId is null
            throw 54020, N'Строка заказа не найдена.', 1;

        select
            @StoreId = storeid,
            @Status = status
        from Salehead
        where id = @HeadId;

        if @Status <> 0
            throw 54021, N'Нельзя изменять уже отработанную накладную.', 1;

        select
            @PriceId = id
        from Pricelist
        where nomenid = @NomenId
          and storeid = @StoreId;

        if @PriceId is null
            throw 54022, N'Запись остатка для товара не найдена.', 1;

        begin transaction;

        update Pricelist
        set
            quanttosale = quanttosale + @Quant,
            quantreserve = quantreserve - @Quant
        where id = @PriceId;

        delete from Salespec
        where id = @SpecId;

        commit transaction;
    end try
    begin catch
        if @@trancount > 0
            rollback transaction;

        throw;
    end catch
end
go

/* =========================================================
   ЗАКРЫТИЕ ЗАКАЗА МЕНЕДЖЕРА
   status: 0 -> 1
   reserve списывается
   ========================================================= */

create or alter procedure dbo.ManagerSaleHeadClose
    @HeadId int
as
begin
    set nocount on;
    set xact_abort on;

    declare @StoreId int;
    declare @Status int;
    declare @DocType int;

    begin try
        select
            @StoreId = storeid,
            @Status = status,
            @DocType = doctype
        from Salehead
        where id = @HeadId;

        if @StoreId is null
            throw 54030, N'Заказ не найден.', 1;

        if @DocType <> 0
            throw 54031, N'Можно закрыть только заказ на выдачу.', 1;

        if @Status <> 0
            throw 54032, N'Заказ уже закрыт.', 1;

        if not exists (
            select 1
            from Salespec
            where headid = @HeadId
        )
            throw 54033, N'Нельзя закрыть пустой заказ.', 1;

        begin transaction;

        update p
        set p.quantreserve = p.quantreserve - s.quant
        from Pricelist p
        join Salespec s
            on s.nomenid = p.nomenid
        where s.headid = @HeadId
          and p.storeid = @StoreId;

        update Salehead
        set
            status = 1,
            dataend = getdate()
        where id = @HeadId;

        commit transaction;
    end try
    begin catch
        if @@trancount > 0
            rollback transaction;

        throw;
    end catch
end
go