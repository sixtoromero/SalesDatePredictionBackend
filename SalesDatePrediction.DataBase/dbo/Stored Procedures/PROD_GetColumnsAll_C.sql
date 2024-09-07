-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 07-Julio-2024 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de consultar todos los registros
-- =============================================
CREATE PROCEDURE [dbo].[PROD_GetColumnsAll_C]
AS
	
	SELECT 	
		ColumnId
		,TableId
		,ColumnName
		,DataType
		,Size
		,IsNullable
		,IsPrimaryKey
		,IsForeignKey
		,Description
		,UsersId
	FROM [dbo].[Columns] ORDER BY ColumnName ASC
