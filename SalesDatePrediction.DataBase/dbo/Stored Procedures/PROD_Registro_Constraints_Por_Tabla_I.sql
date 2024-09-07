-- =============================================
-- Author:      Sixto José Romero Martínez
-- Create date: 09-Julio-2024 (dd-MM-yyyy)
-- Description: Procedimiento encargado de insertar los constraint por tabla
-- =============================================
CREATE PROCEDURE PROD_Registro_Constraints_Por_Tabla_I
    @TableId INT,
    @UserId INT
AS
BEGIN
    BEGIN TRY
        
        DECLARE @TableName nvarchar(128)
        SELECT @TableName = TableName FROM [dbo].[Tables] WHERE TableId = @TableId

        INSERT INTO Constraints 
        (
            TableId
            ,ConstraintName
            ,ConstraintType
            ,Description
            ,UsersId
        )
        SELECT 
            @TableId AS TableId,
            rc.CONSTRAINT_NAME AS ConstraintName,
            rc.CONSTRAINT_TYPE AS ConstraintType,
            ISNULL(CONVERT(nvarchar(max), ep.value), '') AS Description,
            @UserId
        FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS rc
        LEFT JOIN sys.extended_properties ep ON ep.major_id = OBJECT_ID(@TableName)
            AND ep.minor_id = 0
            AND ep.class = 1 -- class = 1 indica propiedades extendidas para objetos de tabla
            AND ep.name = 'MS_Description' -- nombre de la propiedad extendida para descripción de tabla
        WHERE rc.TABLE_NAME = @TableName
        ORDER BY rc.CONSTRAINT_NAME;

        SELECT 'success' AS Result
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END