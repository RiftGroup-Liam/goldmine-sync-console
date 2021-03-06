use GoldmineUAT

declare @synchronization_version nvarchar(max) = CHANGE_TRACKING_CURRENT_VERSION(); 

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

DECLARE @KEY2 int = COLUMNPROPERTY(
   OBJECT_ID('GoldmineUAT.dbo.CONTACT1'),'KEY2', 'ColumnId')


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
	CHANGE_TRACKING_IS_COLUMN_IN_MASK(@PHONE3, SYS_CHANGE_COLUMNS),
	[key2Changed] = 
	CHANGE_TRACKING_IS_COLUMN_IN_MASK(@KEY2, SYS_CHANGE_COLUMNS)	
FROM  
    CHANGETABLE(CHANGES GoldmineUAT.dbo.CONTACT1, 1) AS CT  