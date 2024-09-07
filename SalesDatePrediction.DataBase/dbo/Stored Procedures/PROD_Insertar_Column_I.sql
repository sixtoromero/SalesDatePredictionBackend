-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 07-Julio-2023 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de insertar las columnas de las tablas a documentar
-- =============================================
CREATE PROCEDURE [dbo].[PROD_Insertar_Column_I]
	@TableId int
	,@ColumnName nvarchar(128)
	,@DataType nvarchar(128)
	,@Size int
	,@IsNullable bit
	,@IsPrimaryKey bit
	,@IsForeignKey bit
	,@Description nvarchar(MAX)
	,@UsersId int
AS
BEGIN
	BEGIN TRY						
		INSERT INTO Columns (
		TableId
		,ColumnName
		,DataType
		,Size
		,IsNullable
		,IsPrimaryKey
		,IsForeignKey
		,Description
		,UsersId
		)
		VALUES (
			@TableId
			,@ColumnName
			,@DataType
			,@Size
			,@IsNullable
			,@IsPrimaryKey
			,@IsForeignKey
			,@Description
			,@UsersId
		)
		SELECT 'success'
	END TRY
	BEGIN CATCH		
		SELECT ERROR_MESSAGE();
	END CATCH
END