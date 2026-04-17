use Storag
go

create or alter procedure dbo.AuthUser
    @Login nvarchar(250),
    @Password nvarchar(250)
as
begin
    set nocount on;

    select top (1)
        userid,
        longname,
        roleid,
        rolecode
    from dbo.v_Users
    where login = @Login
      and password = @Password;
end
go

use Storag
go

create or alter procedure dbo.RolesGetAll
as
begin
	set nocount on;

	select
		id as roleid,
		code as rolecode
	from Roles
	order by code;
end
go

create or alter procedure dbo.UsersGetAll
as
begin
	set nocount on;

	select
		userid,
		name,
		sname,
		fname,
		longname,
		login,
		roleid,
		rolecode
	from dbo.v_Users
	order by userid;
end
go


create or alter procedure dbo.UserAdd
	@Name nvarchar(250),
	@Sname nvarchar(250) = null,
	@Fname nvarchar(250) = null,
	@RoleId int,
	@Login nvarchar(250),
	@Password nvarchar(250)
as
begin
	set nocount on;
	set xact_abort on;

	declare @UserId int;

	begin try
		if ltrim(rtrim(isnull(@Name, N''))) = N''
			throw 50010, N'Имя не заполнено.', 1;

		if ltrim(rtrim(isnull(@Login, N''))) = N''
			throw 50011, N'Логин не заполнен.', 1;

		if ltrim(rtrim(isnull(@Password, N''))) = N''
			throw 50012, N'Пароль не заполнен.', 1;

		if not exists (select 1 from Roles where id = @RoleId)
			throw 50013, N'Роль не найдена.', 1;

		if exists (select 1 from Account where login = @Login)
			throw 50014, N'Такой логин уже существует.', 1;

		begin transaction;

			insert into Users(name, sname, fname, type)
			values(@Name, @Sname, @Fname, 0);

			set @UserId = scope_identity();

			insert into Account(userid, roleid, login, password)
			values(@UserId, @RoleId, @Login, @Password);

		commit transaction;

		select @UserId as userid;
	end try
	begin catch
		if @@trancount > 0
			rollback transaction;

		throw;
	end catch
end
go

create or alter procedure dbo.UserUpdate
	@UserId int,
	@Name nvarchar(250),
	@Sname nvarchar(250) = null,
	@Fname nvarchar(250) = null,
	@RoleId int,
	@Login nvarchar(250),
	@Password nvarchar(250) = null
as
begin
	set nocount on;
	set xact_abort on;

	begin try
		if not exists (select 1 from Users where id = @UserId and type = 0)
			throw 50020, N'Пользователь не найден.', 1;

		if ltrim(rtrim(isnull(@Name, N''))) = N''
			throw 50021, N'Имя не заполнено.', 1;

		if ltrim(rtrim(isnull(@Login, N''))) = N''
			throw 50022, N'Логин не заполнен.', 1;

		if not exists (select 1 from Roles where id = @RoleId)
			throw 50023, N'Роль не найдена.', 1;

		if exists (
			select 1
			from Account
			where login = @Login
			  and userid <> @UserId
		)
			throw 50024, N'Такой логин уже существует.', 1;

		begin transaction;

			update Users
			set
				name = @Name,
				sname = @Sname,
				fname = @Fname
			where id = @UserId;

			update Account
			set
				roleid = @RoleId,
				login = @Login,
				password = case
					when ltrim(rtrim(isnull(@Password, N''))) = N'' then password
					else @Password
				end
			where userid = @UserId;

		commit transaction;
	end try
	begin catch
		if @@trancount > 0
			rollback transaction;

		throw;
	end catch
end
go

create or alter procedure dbo.UserDelete
	@UserId int
as
begin
	set nocount on;
	set xact_abort on;

	begin try
		if not exists (select 1 from Users where id = @UserId and type = 0)
			throw 50030, N'Пользователь не найден.', 1;

		if exists (select 1 from Salehead where manager = @UserId)
			throw 50031, N'Нельзя удалить пользователя: он используется в документах.', 1;

		begin transaction;

			delete from Account
			where userid = @UserId;

			delete from Users
			where id = @UserId;

		commit transaction;
	end try
	begin catch
		if @@trancount > 0
			rollback transaction;

		throw;
	end catch
end
go