use GoldmineUAT
go

create procedure dbo.TESTS_CreateContact1Change @accountno nvarchar(20)
as
update dbo.CONTACT1 set phone1 = '07887 495882'
where ACCOUNTNO = @accountno
go

create procedure dbo.TESTS_CreateContact2Change @accountno nvarchar(20)
as
update dbo.CONTACT2 set uclientsta = 'LEAD - NonActive!'
where ACCOUNTNO = @accountno
go

create procedure dbo.TESTS_CreateContsuppChange @accountno nvarchar(20)
as
update dbo.CONTSUPP set CONTSUPREF = 'liamarnold93@gmail.com'
where ACCOUNTNO = @accountno
go


