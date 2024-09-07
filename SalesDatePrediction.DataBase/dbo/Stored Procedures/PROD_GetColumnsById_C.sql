-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 07-Julio-2024 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de consultar todos los registros
-- =============================================
CREATE PROCEDURE [dbo].[PROD_GetColumnsById_C]
	@ColumnId INT
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
	FROM [dbo].[Columns] WHERE ColumnId = @ColumnId
GO