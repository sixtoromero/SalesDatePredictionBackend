-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 07-Julio-2024 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de eliminar el registro
-- =============================================
CREATE PROCEDURE [dbo].[PROD_Delete_database_D]
	@DatabaseId int	
AS
BEGIN	
	BEGIN TRY						
		DELETE FROM [dbo].[Databases] WHERE DatabaseId = @DatabaseId
		SELECT 'success'
	END TRY
	BEGIN CATCH		
		SELECT ERROR_MESSAGE();
	END CATCH
END