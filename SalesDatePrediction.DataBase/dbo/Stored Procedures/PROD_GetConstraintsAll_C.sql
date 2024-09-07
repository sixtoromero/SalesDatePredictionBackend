-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 08-Julio-2024 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de consultar todos los registros
-- =============================================
CREATE PROCEDURE PROD_GetConstraintsAll_C
AS
	
	SELECT 	
		ConstraintId
		,TableId
		,ConstraintName
		,ConstraintType
		,Description
		,UsersId
	FROM [dbo].[Constraints] ORDER BY ConstraintName ASC

GO