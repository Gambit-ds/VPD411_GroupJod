use Storag
go

/* =========================================================
   ОТЧЕТЫ
   ========================================================= */

create or alter procedure dbo.ReportStock
as
begin
    set nocount on;

    select
        s.id as storeid,
        s.code as storecode,
        s.Longname as storelongname,
        c.id as categoryid,
        c.code as categorycode,
        n.id as productid,
        n.code as productcode,
        n.description,
        p.price,
        p.quanttosale,
        p.quantreserve,
        p.quantexpect,
        p.quantship
    from Pricelist p
    join Nomenlist n on n.id = p.nomenid
    join Category c on c.id = n.catid
    join Store s on s.id = p.storeid
    order by s.code, c.code, n.code;
end
go


create or alter procedure dbo.ReportMovement
    @DateFrom datetime2 = null,
    @DateTo datetime2 = null
as
begin
    set nocount on;

    declare @DateToNext datetime2 = case
        when @DateTo is null then null
        else dateadd(day, 1, cast(@DateTo as date))
    end;

    select
        coalesce(oh.dataend, oh.datastart) as operationdate,
        N'Приход от поставщика' as operationtype,
        oh.id as documentid,
        s.code as storefromcode,
        s.Longname as storefromname,
        cast(null as nvarchar(50)) as storetocode,
        cast(null as nvarchar(250)) as storetoname,
        sup.code as partnercode,
        sup.Longname as partnername,
        n.id as productid,
        n.code as productcode,
        n.description,
        os.quant,
        os.price
    from Orderhead oh
    join Orderspec os on os.headid = oh.id
    join Store s on s.id = oh.storeid
    join Supplier sup on sup.id = oh.supid
    join Nomenlist n on n.id = os.nomenid
    where (@DateFrom is null or coalesce(oh.dataend, oh.datastart) >= @DateFrom)
      and (@DateToNext is null or coalesce(oh.dataend, oh.datastart) < @DateToNext)

    union all

    select
        coalesce(th.dataend, th.datastart) as operationdate,
        N'Перемещение расход' as operationtype,
        th.id as documentid,
        sout.code as storefromcode,
        sout.Longname as storefromname,
        sin.code as storetocode,
        sin.Longname as storetoname,
        cast(null as nvarchar(50)) as partnercode,
        cast(null as nvarchar(250)) as partnername,
        n.id as productid,
        n.code as productcode,
        n.description,
        -ts.quant as quant,
        cast(null as decimal(10,2)) as price
    from Transferhead th
    join Transferspec ts on ts.headid = th.id
    join Store sout on sout.id = th.storeout
    join Store sin on sin.id = th.storein
    join Nomenlist n on n.id = ts.nomenid
    where (@DateFrom is null or coalesce(th.dataend, th.datastart) >= @DateFrom)
      and (@DateToNext is null or coalesce(th.dataend, th.datastart) < @DateToNext)

    union all

    select
        coalesce(th.dataend, th.datastart) as operationdate,
        N'Перемещение приход' as operationtype,
        th.id as documentid,
        sout.code as storefromcode,
        sout.Longname as storefromname,
        sin.code as storetocode,
        sin.Longname as storetoname,
        cast(null as nvarchar(50)) as partnercode,
        cast(null as nvarchar(250)) as partnername,
        n.id as productid,
        n.code as productcode,
        n.description,
        ts.quant as quant,
        cast(null as decimal(10,2)) as price
    from Transferhead th
    join Transferspec ts on ts.headid = th.id
    join Store sout on sout.id = th.storeout
    join Store sin on sin.id = th.storein
    join Nomenlist n on n.id = ts.nomenid
    where (@DateFrom is null or coalesce(th.dataend, th.datastart) >= @DateFrom)
      and (@DateToNext is null or coalesce(th.dataend, th.datastart) < @DateToNext)

    union all

    select
        coalesce(sh.dataend, sh.datastart) as operationdate,
        N'Выдача товара' as operationtype,
        sh.id as documentid,
        s.code as storefromcode,
        s.Longname as storefromname,
        cast(null as nvarchar(50)) as storetocode,
        cast(null as nvarchar(250)) as storetoname,
        cast(uc.id as nvarchar(50)) as partnercode,
        uc.fname + N' ' + uc.name + N' ' + uc.sname as partnername,
        n.id as productid,
        n.code as productcode,
        n.description,
        -ss.quant as quant,
        cast(null as decimal(10,2)) as price
    from Salehead sh
    join Salespec ss on ss.headid = sh.id
    join Store s on s.id = sh.storeid
    join Users uc on uc.id = sh.client
    join Nomenlist n on n.id = ss.nomenid
    where sh.doctype = 0
      and (@DateFrom is null or coalesce(sh.dataend, sh.datastart) >= @DateFrom)
      and (@DateToNext is null or coalesce(sh.dataend, sh.datastart) < @DateToNext)

    union all

    select
        coalesce(sh.dataend, sh.datastart) as operationdate,
        N'Возврат товара' as operationtype,
        sh.id as documentid,
        s.code as storefromcode,
        s.Longname as storefromname,
        cast(null as nvarchar(50)) as storetocode,
        cast(null as nvarchar(250)) as storetoname,
        cast(uc.id as nvarchar(50)) as partnercode,
        uc.fname + N' ' + uc.name + N' ' + uc.sname as partnername,
        n.id as productid,
        n.code as productcode,
        n.description,
        ss.quant as quant,
        cast(null as decimal(10,2)) as price
    from Salehead sh
    join Salespec ss on ss.headid = sh.id
    join Store s on s.id = sh.storeid
    join Users uc on uc.id = sh.client
    join Nomenlist n on n.id = ss.nomenid
    where sh.doctype = 1
      and (@DateFrom is null or coalesce(sh.dataend, sh.datastart) >= @DateFrom)
      and (@DateToNext is null or coalesce(sh.dataend, sh.datastart) < @DateToNext)

    order by operationdate, operationtype, documentid, productcode;
end
go


create or alter procedure dbo.ReportOrdersByDate
    @DateFrom datetime2 = null,
    @DateTo datetime2 = null
as
begin
    set nocount on;

    declare @DateToNext datetime2 = case
        when @DateTo is null then null
        else dateadd(day, 1, cast(@DateTo as date))
    end;

    select
        sh.id as saleid,
        coalesce(sh.dataend, sh.datastart) as operationdate,
        s.code as storecode,
        s.Longname as storelongname,
        um.fname + N' ' + um.name + N' ' + um.sname as managername,
        uc.id as clientid,
        uc.fname + N' ' + uc.name + N' ' + uc.sname as clientname,
        case
            when sh.doctype = 0 then N'Выдача'
            when sh.doctype = 1 then N'Возврат'
            else N'Неизвестно'
        end as doctype,
        sh.status,
        n.id as productid,
        n.code as productcode,
        n.description,
        ss.quant
    from Salehead sh
    join Salespec ss on ss.headid = sh.id
    join Store s on s.id = sh.storeid
    join Users um on um.id = sh.manager
    join Users uc on uc.id = sh.client
    join Nomenlist n on n.id = ss.nomenid
    where (@DateFrom is null or coalesce(sh.dataend, sh.datastart) >= @DateFrom)
      and (@DateToNext is null or coalesce(sh.dataend, sh.datastart) < @DateToNext)
    order by operationdate desc, sh.id desc, n.code;
end
go


create or alter procedure dbo.ReportOrdersByClient
    @ClientId int
as
begin
    set nocount on;

    if not exists (select 1 from Users where id = @ClientId and type = 1)
        throw 53001, N'Клиент не найден.', 1;

    select
        sh.id as saleid,
        coalesce(sh.dataend, sh.datastart) as operationdate,
        s.code as storecode,
        s.Longname as storelongname,
        um.fname + N' ' + um.name + N' ' + um.sname as managername,
        uc.id as clientid,
        uc.fname + N' ' + uc.name + N' ' + uc.sname as clientname,
        case
            when sh.doctype = 0 then N'Выдача'
            when sh.doctype = 1 then N'Возврат'
            else N'Неизвестно'
        end as doctype,
        sh.status,
        n.id as productid,
        n.code as productcode,
        n.description,
        ss.quant
    from Salehead sh
    join Salespec ss on ss.headid = sh.id
    join Store s on s.id = sh.storeid
    join Users um on um.id = sh.manager
    join Users uc on uc.id = sh.client
    join Nomenlist n on n.id = ss.nomenid
    where sh.client = @ClientId
    order by operationdate desc, sh.id desc, n.code;
end
go


create or alter procedure dbo.ReportOrdersByProduct
    @ProductId int
as
begin
    set nocount on;

    if not exists (select 1 from Nomenlist where id = @ProductId)
        throw 53002, N'Товар не найден.', 1;

    select
        sh.id as saleid,
        coalesce(sh.dataend, sh.datastart) as operationdate,
        s.code as storecode,
        s.Longname as storelongname,
        um.fname + N' ' + um.name + N' ' + um.sname as managername,
        uc.id as clientid,
        uc.fname + N' ' + uc.name + N' ' + uc.sname as clientname,
        case
            when sh.doctype = 0 then N'Выдача'
            when sh.doctype = 1 then N'Возврат'
            else N'Неизвестно'
        end as doctype,
        sh.status,
        n.id as productid,
        n.code as productcode,
        n.description,
        ss.quant
    from Salehead sh
    join Salespec ss on ss.headid = sh.id
    join Store s on s.id = sh.storeid
    join Users um on um.id = sh.manager
    join Users uc on uc.id = sh.client
    join Nomenlist n on n.id = ss.nomenid
    where ss.nomenid = @ProductId
    order by operationdate desc, sh.id desc;
end
go