use GoldmineUAT
GO


CREATE PROCEDURE dbo.CONTACT1ChangeTracking @LastVersionNumber int
as

DECLARE @KEY5 int = COLUMNPROPERTY(
   OBJECT_ID('GoldmineUAT.dbo.CONTACT1'),'KEY5', 'ColumnId')

DECLARE @CONTACT int = COLUMNPROPERTY(
   OBJECT_ID('GoldmineUAT.dbo.CONTACT1'),'CONTACT', 'ColumnId')

DECLARE @SECR int = COLUMNPROPERTY(
   OBJECT_ID('GoldmineUAT.dbo.CONTACT1'),'SECR', 'ColumnId')

DECLARE @LASTNAME int = COLUMNPROPERTY(
   OBJECT_ID('GoldmineUAT.dbo.CONTACT1'),'LASTNAME', 'ColumnId')

DECLARE @PHONE1 int = COLUMNPROPERTY(
   OBJECT_ID('GoldmineUAT.dbo.CONTACT1'),'PHONE1', 'ColumnId')

DECLARE @PHONE2 int = COLUMNPROPERTY(
   OBJECT_ID('GoldmineUAT.dbo.CONTACT1'),'PHONE2', 'ColumnId')

DECLARE @PHONE3 int = COLUMNPROPERTY(
   OBJECT_ID('GoldmineUAT.dbo.CONTACT1'),'PHONE3', 'ColumnId')

select * from (
SELECT  
    *, 
	[key5Changed] = 
	CHANGE_TRACKING_IS_COLUMN_IN_MASK(@KEY5, SYS_CHANGE_COLUMNS),
	[contactChanged] = 
	CHANGE_TRACKING_IS_COLUMN_IN_MASK(@CONTACT, SYS_CHANGE_COLUMNS),
	[secrChanged] = 
	CHANGE_TRACKING_IS_COLUMN_IN_MASK(@SECR, SYS_CHANGE_COLUMNS),
	[LastnameChanged] = 
	CHANGE_TRACKING_IS_COLUMN_IN_MASK(@LASTNAME, SYS_CHANGE_COLUMNS),	
	[phone1Changed] = 
	CHANGE_TRACKING_IS_COLUMN_IN_MASK(@PHONE1, SYS_CHANGE_COLUMNS),
	[phone2Changed] = 
	CHANGE_TRACKING_IS_COLUMN_IN_MASK(@PHONE2, SYS_CHANGE_COLUMNS),
	[phone3Changed] = 
	CHANGE_TRACKING_IS_COLUMN_IN_MASK(@PHONE3, SYS_CHANGE_COLUMNS)	
FROM  
    CHANGETABLE(CHANGES GoldmineUAT.dbo.CONTACT1, 1) AS CT)
X
where (X.key5Changed = 1 or X.contactChanged = 1 or X.secrChanged = 1 or X.LastnameChanged = 1 or X.phone1Changed = 1 
		or X.phone2Changed = 1 or X.phone3Changed = 1)
and X.SYS_CHANGE_VERSION > @LastVersionNumber
go