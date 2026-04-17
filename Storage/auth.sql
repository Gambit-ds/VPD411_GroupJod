use Storag
go


create or alter view dbo.v_Users
as
select
		u.id as userid,
		u.name,
		u.sname,
		u.fname,
		u.fname + N' ' + u.name + N' ' + u.sname as longname,
		u.type,
		a.id as accid,
		a.login,
		a.password,
		r.id as roleid,
		r.code as rolecode
from Users u
join Account a on a.userid = u.id
join Roles r on r.id = a.roleid
where u.type = 0
go


create or alter view dbo.v_Pricelist
as
select
		n.id as nomenid,
		n.code as nomencode,
		n.description,
		n.weight,
		n.size,
		c.id as catid,
		c.code as cat_code,
		p.id as priceid,
		p.price,
		p.quanttosale,
		p.quantreserve,
		p.quantexpect,
		p.quantship,
		s.id as storeid,
		s.code as storecode,
		s.Longname as storeLongname
from Pricelist p
join Nomenlist n on n.id = p.nomenid
join Store s on s.id = p.storeid
join Category c on c.id = n.catid
go


create or alter view dbo.v_SalesHead
as
select
		sh.id,
		s.id as storeid,
		s.code as storecode,
		s.Longname as storeLongname,
		um.id as managerid,
		um.fname + N' ' + left(um.name, 1) + N'. ' + left(um.sname, 1) + N'.' as mgshortname,
		um.fname + N' ' + um.name + N' ' + um.sname as mgLongname,
		uc.id as clientid,
		uc.fname + N' ' + left(uc.name, 1) + N'. ' + left(uc.sname, 1) + N'.' as clishortname,
		uc.fname + N' ' + uc.name + N' ' + uc.sname as clilongname,
		sh.doctype,
		case
			when sh.doctype = 0 then N'Выдача товара'
			when sh.doctype = 1 then N'Возврат товара'
			else N'Неизвестно'
		end as doctypecode,
		sh.status,
		l.description as statuscode,
		sh.datastart,
		sh.dataend
from Salehead sh
join Store s on s.id = sh.storeid
join Users uc on uc.id = sh.client
join Users um on um.id = sh.manager
join Lookuptb l on l.status = sh.status and l.tablename = N'Salehead'
go


create or alter view dbo.v_SaleSpec
as
select
		ss.id,
		ss.headid,
		ss.nomenid,
		n.code as nomencode,
		n.description,
		n.size,
		n.weight,
		ss.quant,
		p.price
from Salespec ss
join Nomenlist n on n.id = ss.nomenid
join Salehead sh on sh.id = ss.headid
join Pricelist p on p.nomenid = ss.nomenid and p.storeid = sh.storeid
go


create or alter view dbo.v_OrderHead
as
select
		oh.id,
		s.id as storeid,
		s.code as storecode,
		s.Longname as storeLongname,
		sup.id as supplierid,
		sup.code as suppliercode,
		sup.Longname as supplierLongname,
		oh.status,
		l.description as statuscode,
		oh.datastart,
		oh.dataend
from Orderhead oh
join Store s on s.id = oh.storeid
join Supplier sup on sup.id = oh.supid
join Lookuptb l on l.status = oh.status and l.tablename = N'Orderhead'
go


create or alter view dbo.v_OrderSpec
as
select
		os.id,
		os.headid,
		os.nomenid,
		n.code as nomencode,
		n.description,
		n.size,
		n.weight,
		os.quant,
		os.price
from Orderspec os
join Nomenlist n on n.id = os.nomenid
go


create or alter view dbo.v_TransferHead
as
select
		th.id,
		sn.id as storeinid,
		sn.code as storeincode,
		sn.Longname as storeinLongname,
		sout.id as storeoutid,
		sout.code as storeoutcode,
		sout.Longname as storeoutLongname,
		th.status,
		l.description as statuscode,
		th.datastart,
		th.dataend
from Transferhead th
join Store sout on sout.id = th.storeout
join Store sn on sn.id = th.storein
join Lookuptb l on l.status = th.status and l.tablename = N'Transferhead'
go


create or alter view dbo.v_TransferSpec
as
select
		ts.id,
		ts.headid,
		ts.nomenid,
		n.code as nomencode,
		n.description,
		n.size,
		n.weight,
		ts.quant
from Transferspec ts
join Nomenlist n on n.id = ts.nomenid
go