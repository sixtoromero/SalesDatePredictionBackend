-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 02-Julio-2023 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de actualizar las bases de datos
-- =============================================
CREATE PROCEDURE [dbo].[PROD_Update_database_U]
	@DatabaseId int	
	,@DatabaseName nvarchar(128)
	,@Description nvarchar(MAX)
	,@ConnectionString nvarchar(MAX)
AS
BEGIN	
	BEGIN TRY						
		UPDATE [dbo].[Databases] SET DatabaseName = @DatabaseName, Description = @Description, ConnectionString = @ConnectionString WHERE DatabaseId = @DatabaseId
		SELECT 'success'
	END TRY
	BEGIN CATCH		
		SELECT ERROR_MESSAGE();
	END CATCH
END