-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 07-Julio-2024 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de consultar todos los registros
-- =============================================
CREATE PROCEDURE [dbo].[PROD_GetIndexsAll_C]
AS
	SELECT 	
		IndexId
		,TableId
		,IndexName
		,IndexType
		,IncludedColumns
		,UsersId
	FROM [dbo].[Indexes] ORDER BY IndexName ASC
GO