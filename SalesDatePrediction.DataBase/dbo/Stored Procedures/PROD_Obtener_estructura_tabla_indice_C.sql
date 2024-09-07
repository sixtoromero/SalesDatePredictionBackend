CREATE PROCEDURE PROD_Obtener_estructura_tabla_indice_C
	@TableName NVARCHAR(128)
AS
	-- Obtener información de los índices de la tabla incluyendo las columnas asociadas
	SELECT 
		i.name AS 'Nombre del Índice',
		CASE WHEN i.is_unique = 1 THEN 'Único' ELSE 'No Único' END AS 'Tipo de Índice',
		STUFF((
			SELECT ', ' + COL_NAME(ic.object_id, ic.column_id)
			FROM sys.index_columns ic
			WHERE i.object_id = ic.object_id AND i.index_id = ic.index_id
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS 'Columnas Incluidas'
	FROM sys.indexes i
	WHERE i.object_id = OBJECT_ID(@TableName) AND i.type_desc <> 'HEAP'
	ORDER BY i.name;
