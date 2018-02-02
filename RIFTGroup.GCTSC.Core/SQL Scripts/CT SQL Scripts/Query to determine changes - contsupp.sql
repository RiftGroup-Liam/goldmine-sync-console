use GoldmineUAT

declare @synchronization_version nvarchar(max) = CHANGE_TRACKING_CURRENT_VERSION(); 

DECLARE @UCONTSUPREF int = COLUMNPROPERTY(
   OBJECT_ID('GoldmineUAT.dbo.CONTSUPP'),'UCONTSUPREF', 'ColumnId')

SELECT  
    *,
	[uclientstaChanged] = 
	CHANGE_TRACKING_IS_COLUMN_IN_MASK(@UCONTSUPREF, SYS_CHANGE_COLUMNS)
FROM  
    CHANGETABLE(CHANGES GoldmineUAT.dbo.CONTSUPP, 1) AS CT  