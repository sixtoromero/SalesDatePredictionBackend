CREATE PROCEDURE PROD_Obtener_estructura_tabla_C
	@TableName NVARCHAR(128)
AS
	-- Obtener información de la tabla y sus columnas, incluyendo claves primarias y foráneas, y descripción de la tabla
	SELECT 
		c.COLUMN_NAME AS 'Nombre del Campo',
		c.DATA_TYPE AS 'Tipo de Dato',
		CASE WHEN c.CHARACTER_MAXIMUM_LENGTH IS NOT NULL THEN '(' + CAST(c.CHARACTER_MAXIMUM_LENGTH AS NVARCHAR(10)) + ')' ELSE '' END AS 'Tamaño',
		c.IS_NULLABLE AS 'Es Nulo',
		CASE WHEN pk.COLUMN_NAME IS NOT NULL THEN 'Primary Key' ELSE '' END AS 'Primary Key',
		CASE WHEN fk.COLUMN_NAME IS NOT NULL THEN 'Foreign Key' ELSE '' END AS 'Foreign Key',
		ISNULL(CAST(ep.value AS NVARCHAR(MAX)), '') AS 'Descripción' -- Obtener la descripción de la tabla desde las propiedades extendidas
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
