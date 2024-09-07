-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 09-Julio-2023 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de insertar los constrains de las tablas a documentar
-- =============================================
CREATE PROCEDURE PROD_Insertar_Constraints_I
	@TableId int
	,@ConstraintName nvarchar(128)
	,@ConstraintType nvarchar(50)
	,@Description nvarchar(max)
	,@UsersId int
AS
BEGIN
	BEGIN TRY						
		INSERT INTO Constraints (
			TableId
			,ConstraintName
			,ConstraintType
			,Description
			,UsersId
		)
		VALUES (
			@TableId
			,@ConstraintName
			,@ConstraintType
			,@Description
			,@UsersId
		)
		SELECT 'success'
	END TRY
	BEGIN CATCH		
		SELECT ERROR_MESSAGE();
	END CATCH
END