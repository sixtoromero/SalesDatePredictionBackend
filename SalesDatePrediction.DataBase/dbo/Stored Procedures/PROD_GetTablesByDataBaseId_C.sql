-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 07-Julio-2024 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de consultar todas las tablas por DataBaseId
-- EXEC PROD_GetTablesByDataBaseId_C 1
-- =============================================
CREATE PROCEDURE [dbo].[PROD_GetTablesByDataBaseId_C]
	@DatabaseId INT
AS	
	SELECT 	
		TableId
		,DatabaseId
		,Scheme
		,TableName
		,Description
		,Status
		,UsersId
	FROM [dbo].[Tables] 
	WHERE DatabaseId = @DatabaseId
	ORDER BY TableId ASC
GO