-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 09-Julio-2023 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de validar si existe el constraint o no.
-- =============================================
CREATE PROCEDURE PROD_Exists_Constraints_C
	@TableId INT,	
	@ConstraintName nvarchar(128)	
AS
BEGIN	
	BEGIN TRY						
		SELECT 
			ConstraintName 
		FROM Constraints 
		WHERE TableId = @TableId AND ConstraintName = @ConstraintName
	END TRY
	BEGIN CATCH		
		SELECT ERROR_MESSAGE();
	END CATCH
END