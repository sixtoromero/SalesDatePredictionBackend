-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 07-Julio-2024 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de consultar por id 
-- =============================================
CREATE PROCEDURE [dbo].[PROD_GetDataBaseById_C]
	@DatabaseId INT
AS		
SELECT 
	DatabaseId
	,DatabaseName
	,Description
	,ConnectionString
	,UsersId 
FROM [dbo].[Databases] WHERE DatabaseId = @DatabaseId