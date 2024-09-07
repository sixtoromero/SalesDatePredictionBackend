CREATE PROCEDURE PROD_Obtener_estructura_tabla_restriccion_C
	@TableName NVARCHAR(128)
AS
	-- Obtener información de las restricciones de la tabla y sus descripciones
	SELECT 
		rc.CONSTRAINT_NAME AS 'Nombre de la Restricción',
		rc.CONSTRAINT_TYPE AS 'Tipo de Restricción',
		ISNULL(ep.value, '') AS 'Descripción'
	FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS rc
	LEFT JOIN sys.extended_properties ep ON ep.major_id = OBJECT_ID(@TableName)
		AND ep.minor_id = 0
		AND ep.class = 1 -- class = 1 indica propiedades extendidas para objetos de tabla
		AND ep.name = 'MS_Description' -- nombre de la propiedad extendida para descripción de tabla
	WHERE rc.TABLE_NAME = @TableName
	ORDER BY rc.CONSTRAINT_NAME;
