select * from UserAllInfo;

create view v_Users as
select  
		u.id userid, 
		u.name, 
		u.sname, 
		u.fname,
		u.fname+' '+u.name+' '+u.sname as longname,
		u.type, 
		a.id accid,
		a.login,
		a.password,
		r.id roleid,
		r.code
from 
		Users u join Account a on u.id = a.userid
		join Roles r on r.id = a.roleid
where 
		u.type = 0

create view v_Pricelist as
select  
		n.id,
		n.code,
		n.description,
		n.weight,
		n.size,
		c.id catid,
		c.code cat_code,
		p.id priceid,
		p.price,
		p.quanttosale,
		p.quantreserve,
		p.quantexpect,
		p.quantship,
		s.id storeid,
		s.code store,
		s.Longname 
from 
		Pricelist p join Nomenlist n on p.nomenid = n.id
		join Store s on s.id = p.storeid
		join Category c on c.id = n.catid

create view v_SalesHead as
select 
		sh.id,
		s.id storeid,
		s.code storecode,
		s.Longname storeLongname,
		um.id mangerid,
		um.fname + ' ' +LEFT(um.name, 1) + '. '+ LEFT(um.sname, 1) + '.' as mgshortname,
		um.fname+' '+um.name+' '+um.sname as mnLongname,
		uc.id clientid,
		uc.fname + ' ' +LEFT(uc.name, 1) + '. '+ LEFT(uc.sname, 1) + '.' as clishortname,
		uc.fname+' '+uc.name+' '+uc.sname as clilongname,
		sh.doctype,
		CASE sh.doctype WHEN 0 THEN 'Выдача товара' WHEN 1 THEN 'Возврат товара' ELSE 'Неизвестно' END doctypecode,
		sh.status,
		l.description statuscode,
		sh.datastart,
		sh.dataend
from
		Salehead sh join Store s on s.id = sh.storeid
		join Users uc on uc.id = sh.client
		join Users um on um.id = sh.manager
		join Lookuptb l on l.status = sh.status and tablename = 'Salehead'

create view v_SaleSpec as
select 
		s.id,
		s.headid,
		s.nomenid,
		n.code,
		n.description,
		n.size,
		n.weight,
		s.quant,
		p.price
from 
		Salespec s join Nomenlist n on s.nomenid = n.id
		join Pricelist p on n.id = p.nomenid

create view v_Orderhead as
select 
		oh.id,
		s.id storeid,
		s.code storecode,
		s.Longname storeLongname,
		sup.id supplierid,
		sup.code suppliercode,
		sup.Longname supplierLong,
		oh.status,
		l.description statuscode,
		oh.datastart,
		oh.dataend
from
		Orderhead oh join Store s on s.id = oh.storeid
		join Supplier sup on sup.id = oh.supid
		join Lookuptb l on l.status = oh.status and tablename = 'Orderhead'

create view v_Orderspec as
select 
		o.id,
		o.headid,
		o.nomenid,
		n.code,
		n.description,
		n.size,
		n.weight,
		o.quant,
		o.price
from 
		Orderspec o join Nomenlist n on o.nomenid = n.id


create view v_Transferhead as
select 
		th.id,
		sn.id storeinid,
		sn.code storeincode,
		sn.Longname storeinLong,
		sout.id storeoutid,
		sout.code storeoutcode,
		sout.Longname storeoutLong,
		th.status,
		l.description statuscode,
		th.datastart,
		th.dataend
from
		Transferhead th join Store sout on sout.id = th.storeout
		join Store sn on sn.id = th.storein
		join Lookuptb l on l.status = th.status and tablename = 'Transferhead'

create view v_Transferspec as
select  
		t.id,
		t.headid,
		t.nomenid,
		n.code,
		n.description,
		n.size,
		n.weight,
		t.quant
from 
		Transferspec t join Nomenlist n on t.nomenid = n.id