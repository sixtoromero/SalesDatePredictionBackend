CREATE PROCEDURE [dbo].[PROD_Registro_Indexes_Por_JsonTable_I]
	@jsonTables NVARCHAR(MAX),
	@DatabaseId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Tabla temporal para almacenar los datos del JSON
    IF OBJECT_ID('tempdb..#TablesTempTemp') IS NOT NULL
        DROP TABLE #TablesTemp;

    CREATE TABLE #TablesTemp (
        TableId INT,
        DatabaseId INT,
        Scheme NVARCHAR(50),
        TableName NVARCHAR(255),
        Description NVARCHAR(MAX),
        Status BIT,
        UsersId INT
    );

    -- Insertar los datos del JSON en la tabla temporal
    INSERT INTO #TablesTemp (TableId, DatabaseId, Scheme, TableName, Description, Status, UsersId)
    SELECT TableId, DatabaseId, Scheme, TableName, Description, Status, UsersId
    FROM OPENJSON(@jsonTables)
    WITH (
        TableId INT,
        DatabaseId INT,
        Scheme NVARCHAR(50),
        TableName NVARCHAR(255),
        Description NVARCHAR(MAX),
        Status BIT,
        UsersId INT
    );

    DECLARE @TableId INT, @TableName NVARCHAR(255);
    DECLARE @SQL NVARCHAR(MAX);

    -- Cursor para recorrer todas las tablas en la tabla temporal
    DECLARE cur CURSOR FOR
    SELECT TableId, DatabaseId, TableName
    FROM #TablesTemp;

    OPEN cur;
    FETCH NEXT FROM cur INTO @TableId, @DatabaseId, @TableName;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @SQL = '
        INSERT INTO dbo.Indexes (TableId, IndexName, IndexType, IncludedColumns)
        SELECT 
            ' + CAST(@TableId AS NVARCHAR) + ',
            i.name AS IndexName,
            CASE WHEN i.is_unique = 1 THEN ''Único'' ELSE ''No Único'' END AS IndexType,
            STUFF((
                SELECT '', '' + COL_NAME(ic.object_id, ic.column_id)
                FROM sys.index_columns ic
                WHERE i.object_id = ic.object_id AND i.index_id = ic.index_id
                FOR XML PATH(''''), TYPE).value(''.'', ''NVARCHAR(MAX)''), 1, 2, '''') AS IncludedColumns
        FROM sys.indexes i
        WHERE i.object_id = OBJECT_ID(''' + @TableName + ''') AND i.type_desc <> ''HEAP''
        ORDER BY i.name;
        ';

        EXEC sp_executesql @SQL;

        FETCH NEXT FROM cur INTO @TableId, @DatabaseId, @TableName;
    END;

    CLOSE cur;
    DEALLOCATE cur;
END;
