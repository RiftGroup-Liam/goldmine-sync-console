USE [GoldmineUAT]
GO

create PROCEDURE [dbo].[CONTSUPPChangeTracking] @LastVersionNumber int
as

DECLARE @UCONTSUPREF int = COLUMNPROPERTY(
   OBJECT_ID('GoldmineUAT.dbo.CONTSUPP'),'CONTSUPREF', 'ColumnId')

   select * from (
select * from (
SELECT  
    *,
	[contsuprefChanged] = 
	CHANGE_TRACKING_IS_COLUMN_IN_MASK(@UCONTSUPREF, SYS_CHANGE_COLUMNS)	
FROM  
    CHANGETABLE(CHANGES GoldmineUAT.dbo.CONTSUPP, 1) as CT
) X
where (X.[contsuprefChanged] = 1)
and SYS_CHANGE_VERSION > @LastVersionNumber
) Y
where (select top 1 rectype from dbo.CONTSUPP where recid = Y.recid) = 'P'


GO


