use Storag
go

/* =========================================================
   КАТЕГОРИИ
   ========================================================= */

create or alter procedure dbo.CategoriesGetAll
as
begin
    set nocount on;

    select
        id as categoryid,
        code as categorycode
    from Category
    order by code;
end
go


create or alter procedure dbo.CategoryAdd
    @Code nvarchar(50)
as
begin
    set nocount on;
    set xact_abort on;

    declare @CategoryId int;

    begin try
        if ltrim(rtrim(isnull(@Code, N''))) = N''
            throw 51001, N'Код категории не заполнен.', 1;

        if exists (select 1 from Category where code = @Code)
            throw 51002, N'Категория с таким кодом уже существует.', 1;

        insert into Category(code)
        values(@Code);

        set @CategoryId = scope_identity();

        select @CategoryId as categoryid;
    end try
    begin catch
        throw;
    end catch
end
go


create or alter procedure dbo.CategoryUpdate
    @CategoryId int,
    @Code nvarchar(50)
as
begin
    set nocount on;
    set xact_abort on;

    begin try
        if not exists (select 1 from Category where id = @CategoryId)
            throw 51003, N'Категория не найдена.', 1;

        if ltrim(rtrim(isnull(@Code, N''))) = N''
            throw 51004, N'Код категории не заполнен.', 1;

        if exists (
            select 1
            from Category
            where code = @Code
              and id <> @CategoryId
        )
            throw 51005, N'Категория с таким кодом уже существует.', 1;

        update Category
        set code = @Code
        where id = @CategoryId;
    end try
    begin catch
        throw;
    end catch
end
go


create or alter procedure dbo.CategoryDelete
    @CategoryId int
as
begin
    set nocount on;
    set xact_abort on;

    begin try
        if not exists (select 1 from Category where id = @CategoryId)
            throw 51006, N'Категория не найдена.', 1;

        if exists (select 1 from Nomenlist where catid = @CategoryId)
            throw 51007, N'Категорию нельзя удалить: к ней привязаны товары.', 1;

        delete from Category
        where id = @CategoryId;
    end try
    begin catch
        throw;
    end catch
end
go