-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 09-Julio-2023 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de actualizar los constraints
-- =============================================
CREATE PROCEDURE PROD_Update_Constraints_I
	@ConstraintId int
	,@TableId int
	,@ConstraintName nvarchar(128)
	,@ConstraintType nvarchar(50)
	,@Description nvarchar(max)
	,@UsersId int
AS
BEGIN	
	BEGIN TRY						
		UPDATE 
			Constraints 
		SET ConstraintName = @ConstraintName, ConstraintType = @ConstraintType, Description = @Description
		WHERE ConstraintId = @ConstraintId
		SELECT 'success'
	END TRY
	BEGIN CATCH		
		SELECT ERROR_MESSAGE();
	END CATCH
END