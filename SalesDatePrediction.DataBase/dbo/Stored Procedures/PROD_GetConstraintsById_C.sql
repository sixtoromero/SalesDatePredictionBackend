-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 07-Julio-2024 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de consultar todos los registros
-- =============================================
CREATE PROCEDURE PROD_GetConstraintsById_C
	@ConstraintId INT
AS	
	SELECT 	
		ConstraintId
		,TableId
		,ConstraintName
		,ConstraintType
		,Description
		,UsersId
	FROM [dbo].[Constraints] WHERE ConstraintId = @ConstraintId