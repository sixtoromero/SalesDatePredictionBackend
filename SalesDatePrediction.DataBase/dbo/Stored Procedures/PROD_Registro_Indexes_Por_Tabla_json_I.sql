-- =============================================
-- Author:      Sixto José Romero Martínez
-- Create date: 03-Julio-2024 (dd-MM-yyyy)
-- Description: Procedimiento encargado de insertar los indices por tabla
-- =============================================
CREATE PROCEDURE [dbo].[PROD_Registro_Indexes_Por_Tabla_json_I]
	@json NVARCHAR(MAX)	
AS
BEGIN
	BEGIN TRY

		CREATE TABLE #TempIndexes (
			TableId INT,			
			IndexName NVARCHAR(128),
			IndexType NVARCHAR(50),
			IncludedColumns NVARCHAR(MAX),			
			UsersId INT
		);

		-- Insertar los datos del JSON en la tabla temporal
		INSERT INTO #TempIndexes (TableId, IndexName, IndexType, IncludedColumns, UsersId)
		SELECT 
			TableId, 
			IndexName, 
			IndexType, 
			IncludedColumns, 
			UsersId
		FROM OPENJSON(@json)
		WITH (
			TableId INT,
			IndexName NVARCHAR(128),
			IndexType NVARCHAR(50),
			IncludedColumns NVARCHAR(MAX),			
			UsersId INT
		);

		INSERT INTO [Indexes]
		(
			TableId, 
			IndexName, 
			IndexType, 
			IncludedColumns, 
			UsersId
		)
		SELECT 
			TableId, 
			IndexName, 
			IndexType, 
			IncludedColumns, 
			UsersId
		FROM 
			#TempIndexes t
		WHERE NOT EXISTS (
			SELECT 1 
			FROM [Indexes] 
			WHERE TableId = t.TableId
			  AND IndexName = t.IndexName
			  AND IndexType = t.IndexType
		)
		ORDER BY 
			IndexName, 
			IndexType;

		SELECT 'success'
	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END