use Storag
go

/* =========================================================
   бяонлнцюрекэмше яохяйх дкъ тнпл х тхкэрпнб, бшахпюер ХГ USERs рнкэйн реу йрн ъбкъеряъ йкхемрнл
   ========================================================= */

create or alter procedure dbo.ClientsGetAll
as
begin
    set nocount on;

    select
        id as clientid,
        fname + N' ' + name + N' ' + sname as clientname
    from Users
    where type = 1
    order by fname, name, sname;
end
go