create or alter procedure dbo.SalespecAdd
    @Headid int,
    @Nomenid int,
    @Quant decimal(10,3),
    @Id int output
as
begin
    set nocount on;
    set xact_abort on;

    declare @quanttosale decimal(10,3);
    declare @prid int;
    declare @store int;
    declare @status int;

    begin try
        -- Проверка количества
        if @Quant <= 0
            throw 50000, N'Количество должно быть больше нуля.', 1;

        begin transaction;

        -- Получаем склад и состояние накладной
        select
            @store = storeid,
            @status = status
        from Salehead
        where id = @Headid;

        if @store is null
            throw 50001, N'Накладная не найдена.', 1;

        -- Если накладная уже отработана, менять ее нельзя
        if @status <> 0
            throw 50005, N'В эту накладную нельзя добавлять строки.', 1;

        -- Проверка существования товара
        if not exists (
            select 1
            from Nomenlist
            where id = @Nomenid
        )
            throw 50002, N'Такой товар не найден.', 1;

        -- Проверка, что такой строки еще нет
        if exists (
            select 1
            from Salespec
            where headid = @Headid
              and nomenid = @Nomenid
        )
            throw 50006, N'Этот товар уже есть в накладной.', 1;

        -- Получаем запись остатков по нужному складу
        select
            @prid = id,
            @quanttosale = quanttosale
        from Pricelist
        where nomenid = @Nomenid
          and storeid = @store;

        if @prid is null
            throw 50004, N'Товар не найден на складе.', 1;

        -- Проверка доступного остатка
        if @quanttosale < @Quant
            throw 50003, N'Количество недостаточно для продажи.', 1;

        -- Резервируем товар
        update Pricelist
        set quanttosale = quanttosale - @Quant,
            quantreserve = quantreserve + @Quant
        where id = @prid;

        -- Добавляем строку документа
        insert into Salespec(headid, nomenid, quant)
        values(@Headid, @Nomenid, @Quant);

        set @Id = scope_identity();

        commit transaction;
    end try
    begin catch
        if @@trancount > 0
            rollback transaction;

        throw;
    end catch
end