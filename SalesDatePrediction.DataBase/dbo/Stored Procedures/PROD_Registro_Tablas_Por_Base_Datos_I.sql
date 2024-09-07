-- =============================================
-- Author:      Sixto José Romero Martínez
-- Create date: 03-Julio-2024 (dd-MM-yyyy)
-- Description: Procedimiento encargado de insertar las tablas por base de datos
-- =============================================
CREATE PROCEDURE PROD_Registro_Tablas_Por_Base_Datos_I
	@DatabaseId INT,
	@UserId INT
AS
BEGIN
	BEGIN TRY
		INSERT INTO Tables 
		(
			DatabaseId,
			Scheme,
			TableName,
			Description,
			Status,
			UsersId
		)
		SELECT 
			@DatabaseId AS DatabaseId,
			TABLE_SCHEMA AS Scheme, 
			TABLE_NAME AS TableName, 
			'' AS Description,
			1 AS Status,
			@UserId AS UsersId
		FROM 
			INFORMATION_SCHEMA.TABLES t
		WHERE NOT EXISTS (
			SELECT 1 
			FROM Tables 
			WHERE DatabaseId = @DatabaseId 
			  AND Scheme = t.TABLE_SCHEMA 
			  AND TableName = t.TABLE_NAME
		)
		ORDER BY 
			TABLE_SCHEMA, 
			TABLE_NAME;

		SELECT 'success'
	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END
