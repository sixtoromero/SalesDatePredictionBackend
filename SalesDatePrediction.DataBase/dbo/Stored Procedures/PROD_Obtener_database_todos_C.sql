-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 02-Julio-2023 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de consultar todos los registros de la tabla databases
-- =============================================
CREATE PROCEDURE PROD_Obtener_database_todos_C	
AS
SELECT  
DatabaseId
,DatabaseName
,Description
,ConnectionString
,UsersId
FROM Databases
