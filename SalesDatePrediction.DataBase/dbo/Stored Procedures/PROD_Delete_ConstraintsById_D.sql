-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 07-Julio-2024 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de eliminar el registro
-- =============================================
CREATE PROCEDURE PROD_Delete_ConstraintsById_D
	@ConstraintId int	
AS
BEGIN	
	BEGIN TRY						
		DELETE FROM Constraints WHERE ConstraintId = @ConstraintId
		SELECT 'success'
	END TRY
	BEGIN CATCH		
		SELECT ERROR_MESSAGE();
	END CATCH
END