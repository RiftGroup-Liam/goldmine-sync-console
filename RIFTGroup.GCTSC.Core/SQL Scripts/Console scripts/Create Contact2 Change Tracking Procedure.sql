use GoldmineUAT
go

CREATE PROCEDURE dbo.CONTACT2ChangeTracking @LastVersionNumber int
as

DECLARE @UEMAILADDR int = COLUMNPROPERTY(
   OBJECT_ID('GoldmineUAT.dbo.CONTACT2'),'UEMAILADDR', 'ColumnId')

DECLARE @USTAGE1DAT int = COLUMNPROPERTY(
   OBJECT_ID('GoldmineUAT.dbo.CONTACT2'),'USTAGE1DAT', 'ColumnId')

DECLARE @UCONVDATE int = COLUMNPROPERTY(
	OBJECT_ID('GoldmineUAT.dbo.CONTACT2'),'UCONVDATE', 'ColumnId')

select * from (
SELECT  
    *, 
	[uemailaddrChanged] = 
	CHANGE_TRACKING_IS_COLUMN_IN_MASK(@UEMAILADDR, SYS_CHANGE_COLUMNS),	
	[ustage1datChanged] = 
	CHANGE_TRACKING_IS_COLUMN_IN_MASK(@USTAGE1DAT, SYS_CHANGE_COLUMNS),
	[uconvdatChanged] = 
	CHANGE_TRACKING_IS_COLUMN_IN_MASK(@USTAGE1DAT, SYS_CHANGE_COLUMNS)
FROM  
    CHANGETABLE(CHANGES GoldmineUAT.dbo.CONTACT2, 1) AS CT  
) X
where (X.uemailaddrChanged = 1 or X.ustage1datChanged = 1 or X.uconvdatChanged = 1)
and X.SYS_CHANGE_VERSION > @LastVersionNumber
