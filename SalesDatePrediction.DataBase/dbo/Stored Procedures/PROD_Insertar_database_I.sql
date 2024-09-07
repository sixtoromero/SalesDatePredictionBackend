-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 02-Julio-2023 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de insertar las bases de datos a documentar
-- =============================================
CREATE PROCEDURE PROD_Insertar_database_I
	@DatabaseName NVARCHAR(128)
	,@Description NVARCHAR(MAX)
	,@ConnectionString NVARCHAR(MAX)
	,@UsersId INT
AS
BEGIN
	BEGIN TRY						
		INSERT INTO Databases (DatabaseName, Description, ConnectionString,  UsersId)
		VALUES (@DatabaseName, @Description, @ConnectionString, @UsersId)		
		SELECT 'success'
	END TRY
	BEGIN CATCH		
		SELECT ERROR_MESSAGE();
	END CATCH
END