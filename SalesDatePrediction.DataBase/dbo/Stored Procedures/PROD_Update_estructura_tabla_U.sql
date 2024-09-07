-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 02-Julio-2023 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de actualizar las tablas
-- =============================================
CREATE PROCEDURE PROD_Update_estructura_tabla_U
	@TableId int
	,@DatabaseId int
	,@TableName nvarchar(128)
	,@Description nvarchar(MAX)
	,@Status bit
AS
BEGIN	
	BEGIN TRY						
		UPDATE Tables SET DatabaseId = @DatabaseId, TableName = @TableName, Description = @Description, Status = @Status
		SELECT 'success'
	END TRY
	BEGIN CATCH		
		SELECT ERROR_MESSAGE();
	END CATCH
END