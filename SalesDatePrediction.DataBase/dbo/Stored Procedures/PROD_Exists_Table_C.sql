-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 03-Julio-2023 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de validar si existe una tabla o no.
-- =============================================
CREATE PROCEDURE [dbo].[PROD_Exists_Table_C]
	@DatabaseId INT,
	@TableName nvarchar(128)	
AS
BEGIN	
	BEGIN TRY						
		SELECT TableName FROM Tables WHERE DatabaseId = @DatabaseId AND TableName = @TableName
	END TRY
	BEGIN CATCH		
		SELECT ERROR_MESSAGE();
	END CATCH
END