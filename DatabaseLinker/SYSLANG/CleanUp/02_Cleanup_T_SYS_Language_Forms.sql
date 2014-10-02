
UPDATE T_SYS_Language_Forms SET LANG_UID = NEWID()
WHERE LANG_Moron IN 
(
	 'BB0AA512-B583-41EB-9BA3-532985094D2F'
	,'D29BC896-A72F-45EF-90F1-58002E1CD5A6'
	,'FFDCC1AD-87A2-43C9-B25F-10B6E49DEE8C'
)

DELETE FROM T_SYS_Language_Forms WHERE LANG_Moron IN ('AA3768E0-340D-465E-B849-9C2CB08F3021' 
,'EC3F70C7-72DE-4DE3-B6B7-4D7A035B0548'
)
;

GO 

IF EXISTS(SELECT * FROM information_schema.columns WHERE table_name = 'T_SYS_Language_Forms' AND column_name = 'LANG_Moron')
EXECUTE('ALTER TABLE [dbo].[T_SYS_Language_Forms] DROP COLUMN [LANG_Moron]')
GO 
