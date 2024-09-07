-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 02-Julio-2023 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de insertar las tablas
-- =============================================
CREATE PROCEDURE [dbo].[PROD_Insertar_estructura_tabla_I]
	@DatabaseId int
	,@Scheme nvarchar(20)
	,@TableName nvarchar(128)
	,@Description nvarchar(MAX)
	,@Status bit
AS
BEGIN	
	BEGIN TRY						
		INSERT INTO Tables (DatabaseId, Scheme, TableName, Description, Status)
		VALUES (@DatabaseId, @Scheme, @TableName, @Description, @Status)
		
		SELECT 'success'
	END TRY
	BEGIN CATCH		
		SELECT ERROR_MESSAGE();
	END CATCH
END