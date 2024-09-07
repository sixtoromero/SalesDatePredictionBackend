-- =============================================
-- Author:      Sixto José Romero Martínez
-- Create date: 07-Julio-2024 (dd-MM-yyyy)
-- Description: Procedimiento encargado de insertar las Columnas por tablas
-- =============================================
CREATE PROCEDURE [dbo].[PROD_Registro_Columnas_Por_Tabla_I]
	@TableId INT,
	@UserId INT
AS
BEGIN
	BEGIN TRY
		
		DECLARE @TableName nvarchar(128)
		SELECT @TableName = TableName FROM [dbo].[Tables] WHERE TableId = @TableId

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
			@TableId,
			c.COLUMN_NAME AS ColumnName,
			c.DATA_TYPE AS DataType,
			COALESCE(CAST(c.CHARACTER_MAXIMUM_LENGTH AS NVARCHAR(10)), '') AS Size,
			CASE WHEN c.IS_NULLABLE = 'YES' THEN 1 ELSE 0 END AS IsNullable,
			CASE WHEN pk.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END AS IsPrimaryKey,
			CASE WHEN fk.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END AS IsForeignKey,
			ISNULL(CAST(ep.value AS NVARCHAR(MAX)), '') AS Description, -- Obtener la descripción de la tabla desde las propiedades extendidas
			@UserId AS UserId
		FROM INFORMATION_SCHEMA.COLUMNS c
		LEFT JOIN (
			SELECT ku.TABLE_CATALOG, ku.TABLE_SCHEMA, ku.TABLE_NAME, ku.COLUMN_NAME
			FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tc
			INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ku 
				ON tc.CONSTRAINT_TYPE = 'PRIMARY KEY' 
				AND tc.CONSTRAINT_NAME = ku.CONSTRAINT_NAME
		) pk ON c.TABLE_CATALOG = pk.TABLE_CATALOG 
			AND c.TABLE_SCHEMA = pk.TABLE_SCHEMA 
			AND c.TABLE_NAME = pk.TABLE_NAME 
			AND c.COLUMN_NAME = pk.COLUMN_NAME
		LEFT JOIN (
			SELECT ku.TABLE_CATALOG, ku.TABLE_SCHEMA, ku.TABLE_NAME, ku.COLUMN_NAME
			FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tc
			INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ku 
				ON tc.CONSTRAINT_TYPE = 'FOREIGN KEY' 
				AND tc.CONSTRAINT_NAME = ku.CONSTRAINT_NAME
		) fk ON c.TABLE_CATALOG = fk.TABLE_CATALOG 
			AND c.TABLE_SCHEMA = fk.TABLE_SCHEMA 
			AND c.TABLE_NAME = fk.TABLE_NAME 
			AND c.COLUMN_NAME = fk.COLUMN_NAME
		LEFT JOIN sys.extended_properties ep ON ep.major_id = OBJECT_ID(@TableName)
			AND ep.minor_id = 0
			AND ep.class = 1 -- class = 1 indica propiedades extendidas para objetos de tabla
			AND ep.name = 'MS_Description' -- nombre de la propiedad extendida para descripción de tabla
		WHERE c.TABLE_NAME = @TableName
		ORDER BY c.ORDINAL_POSITION;

		SELECT 'success'
	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END
