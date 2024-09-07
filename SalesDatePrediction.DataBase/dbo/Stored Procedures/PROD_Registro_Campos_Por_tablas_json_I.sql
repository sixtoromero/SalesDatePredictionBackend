-- =============================================
-- Author:      Sixto José Romero Martínez
-- Create date: 03-Julio-2024 (dd-MM-yyyy)
-- Description: Procedimiento encargado de insertar las tablas por base de datos
-- =============================================
CREATE PROCEDURE dbo.PROD_Registro_Campos_Por_tablas_json_I
	@json NVARCHAR(MAX),
	@DatabaseId INT,
	@UserId INT
AS
BEGIN
	BEGIN TRY

		CREATE TABLE #TempColumns (
			TableId int NOT NULL,
			ColumnName nvarchar(128) NOT NULL,
			DataType nvarchar(128) NOT NULL,
			Size int NOT NULL,
			IsNullable bit NOT NULL,
			IsPrimaryKey bit NOT NULL,
			IsForeignKey bit NOT NULL,
			Description nvarchar(max) NULL,
			UsersId int NOT NULL,
		);

		-- Insertar los datos del JSON en la tabla temporal
		INSERT INTO #TempColumns 
		(
			TableId
			,ColumnName
			,DataType
			,Size
			,IsNullable
			,IsPrimaryKey
			,IsForeignKey
			,Description
			,UsersId)
		SELECT 
			TableId
			,ColumnName
			,DataType
			,Size
			,IsNullable
			,IsPrimaryKey
			,IsForeignKey
			,Description
			,UsersId
		FROM OPENJSON(@json)
		WITH (
			TableId int,
			ColumnName nvarchar(128),
			DataType nvarchar(128),
			Size int,
			IsNullable bit,
			IsPrimaryKey bit,
			IsForeignKey bit,
			Description nvarchar(max),
			UsersId int
		);

		INSERT INTO Columns 
		(
			TableId,
			ColumnName,
			DataType,
			Size,
			IsNullable,
			IsPrimaryKey,
			IsForeignKey,
			Description,
			UsersId
		)
		SELECT 
            tc.TableId,
            tc.ColumnName,
            tc.DataType,
            tc.Size,
            tc.IsNullable,
            tc.IsPrimaryKey,
            tc.IsForeignKey,
            tc.Description,
            tc.UsersId
        FROM #TempColumns tc
        WHERE NOT EXISTS (
            SELECT 1
            FROM Columns c
            WHERE c.TableId = tc.TableId
              AND c.ColumnName = tc.ColumnName
        );
		
		SELECT 'success'

	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END
