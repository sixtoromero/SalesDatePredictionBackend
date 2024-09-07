-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 02-Julio-2023 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de eliminar el registro
-- =============================================
CREATE PROCEDURE PROD_Delete_estructura_tabla_D
	@TableId int	
AS
BEGIN	
	BEGIN TRY						
		DELETE FROM Tables WHERE TableId = @TableId
		SELECT 'success'
	END TRY
	BEGIN CATCH		
		SELECT ERROR_MESSAGE();
	END CATCH
END