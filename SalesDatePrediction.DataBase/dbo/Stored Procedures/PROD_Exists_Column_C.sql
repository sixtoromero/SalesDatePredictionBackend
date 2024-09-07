-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 07-Julio-2023 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de validar si existe una columna o no.
-- =============================================
CREATE PROCEDURE [dbo].[PROD_Exists_Column_C]
	@TableId INT,	
	@ColumnName nvarchar(128)	
AS
BEGIN	
	BEGIN TRY						
		SELECT 
			ColumnName 
		FROM Columns 
		WHERE TableId = @TableId AND ColumnName = @ColumnName
	END TRY
	BEGIN CATCH		
		SELECT ERROR_MESSAGE();
	END CATCH
END