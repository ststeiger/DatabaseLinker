
IF NOT EXISTS(SELECT * FROM information_schema.columns WHERE table_name = 'T_SYS_Language_Forms' AND column_name = 'LANG_Moron')
EXECUTE('ALTER TABLE dbo.T_SYS_Language_Forms ADD LANG_Moron uniqueidentifier NULL ')
GO 

UPDATE [T_SYS_Language_Forms] SET [LANG_Moron] = NEWID(); 

SELECT 
	   LANG_UID 
	  ,LANG_Moron
      ,LANG_DE
      --,LANG_FR
      --,LANG_EN
      --,LANG_IT
	  ,LANG_Modul
      ,LANG_Object
      ,LANG_Register
      ,LANG_Position
FROM T_SYS_Language_Forms
WHERE LANG_UID IN 
(
	SELECT LANG_UID FROM T_SYS_Language_Forms
	GROUP BY LANG_UID
	HAVING COUNT(*) > 1 
) 

ORDER BY LANG_UID, LANG_DE  
