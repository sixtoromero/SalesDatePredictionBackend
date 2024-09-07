-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 07-Julio-2024 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de eliminar el registro
-- =============================================
CREATE PROCEDURE [dbo].[PROD_Delete_IndexesById_D]
	@IndexId int	
AS
BEGIN	
	BEGIN TRY						
		DELETE FROM Indexes WHERE IndexId = @IndexId
		SELECT 'success'
	END TRY
	BEGIN CATCH		
		SELECT ERROR_MESSAGE();
	END CATCH
END