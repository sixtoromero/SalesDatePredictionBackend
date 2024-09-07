-- =============================================
-- Author:      Sixto José Romero Martínez
-- Create date: 08-Julio-2024 (dd-MM-yyyy)
-- Description: Procedimiento encargado de insertar las Indexeces por Columnas
-- =============================================
CREATE PROCEDURE [dbo].[PROD_Registro_Indexes_Por_Tabla_I]
	@TableId INT,
	@UserId INT
AS
BEGIN
	BEGIN TRY
		
		DECLARE @TableName nvarchar(128)
		SELECT @TableName = TableName FROM [dbo].[Tables] WHERE TableId = @TableId					

		INSERT INTO Indexes
		(
			TableId,
			IndexName,
			IndexType,
			IncludedColumns,
			UsersId
		)
		SELECT 
			@TableId as TableId,
			i.name AS IndexName,
			CASE WHEN i.is_unique = 1 THEN 'Único' ELSE 'No Único' END AS IndexType,
			STUFF((
				SELECT ', ' + COL_NAME(ic.object_id, ic.column_id)
				FROM sys.index_columns ic
				WHERE i.object_id = ic.object_id AND i.index_id = ic.index_id
				FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS IncludedColumns,
			@UserId AS UsersId
		FROM sys.indexes i
		WHERE i.object_id = OBJECT_ID(@TableName) AND i.type_desc <> 'HEAP'
		ORDER BY i.name;

		SELECT 'success'
	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END
