use Storag
go

/* =========================================================
   ТОВАРЫ
   ========================================================= */

create or alter procedure dbo.ProductsGetAll
as
begin
    set nocount on;

    select
        n.id as productid,
        n.catid as categoryid,
        c.code as categorycode,
        n.code as productcode,
        n.description,
        n.weight,
        n.size
    from Nomenlist n
    join Category c on c.id = n.catid
    order by n.id;
end
go


create or alter procedure dbo.ProductsShortGetAll
as
begin
    set nocount on;

    select
        n.id as productid,
        n.code as productcode,
        n.description
    from Nomenlist n
    order by n.code;
end
go


create or alter procedure dbo.ProductAdd
    @CategoryId int,
    @Code nvarchar(50),
    @Description nvarchar(250) = null,
    @Weight int = null,
    @Size int = null
as
begin
    set nocount on;
    set xact_abort on;

    declare @ProductId int;

    begin try
        if not exists (select 1 from Category where id = @CategoryId)
            throw 52001, N'Категория не найдена.', 1;

        if ltrim(rtrim(isnull(@Code, N''))) = N''
            throw 52002, N'Код товара не заполнен.', 1;

        if exists (select 1 from Nomenlist where code = @Code)
            throw 52003, N'Товар с таким кодом уже существует.', 1;

        insert into Nomenlist(catid, code, description, weight, size)
        values(@CategoryId, @Code, @Description, @Weight, @Size);

        set @ProductId = scope_identity();

        select @ProductId as productid;
    end try
    begin catch
        throw;
    end catch
end
go


create or alter procedure dbo.ProductUpdate
    @ProductId int,
    @CategoryId int,
    @Code nvarchar(50),
    @Description nvarchar(250) = null,
    @Weight int = null,
    @Size int = null
as
begin
    set nocount on;
    set xact_abort on;

    begin try
        if not exists (select 1 from Nomenlist where id = @ProductId)
            throw 52004, N'Товар не найден.', 1;

        if not exists (select 1 from Category where id = @CategoryId)
            throw 52005, N'Категория не найдена.', 1;

        if ltrim(rtrim(isnull(@Code, N''))) = N''
            throw 52006, N'Код товара не заполнен.', 1;

        if exists (
            select 1
            from Nomenlist
            where code = @Code
              and id <> @ProductId
        )
            throw 52007, N'Товар с таким кодом уже существует.', 1;

        update Nomenlist
        set
            catid = @CategoryId,
            code = @Code,
            description = @Description,
            weight = @Weight,
            size = @Size
        where id = @ProductId;
    end try
    begin catch
        throw;
    end catch
end
go


create or alter procedure dbo.ProductDelete
    @ProductId int
as
begin
    set nocount on;
    set xact_abort on;

    begin try
        if not exists (select 1 from Nomenlist where id = @ProductId)
            throw 52008, N'Товар не найден.', 1;

        if exists (select 1 from Pricelist where nomenid = @ProductId)
            throw 52009, N'Товар нельзя удалить: он используется в остатках склада.', 1;

        if exists (select 1 from Orderspec where nomenid = @ProductId)
            throw 52010, N'Товар нельзя удалить: он используется в приходах.', 1;

        if exists (select 1 from Transferspec where nomenid = @ProductId)
            throw 52011, N'Товар нельзя удалить: он используется в перемещениях.', 1;

        if exists (select 1 from Salespec where nomenid = @ProductId)
            throw 52012, N'Товар нельзя удалить: он используется в накладных.', 1;

        delete from Nomenlist
        where id = @ProductId;
    end try
    begin catch
        throw;
    end catch
end
go