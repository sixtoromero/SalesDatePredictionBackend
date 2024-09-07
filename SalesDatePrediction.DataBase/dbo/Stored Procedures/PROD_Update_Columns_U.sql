-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 02-Julio-2023 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de actualizar las columnas
-- =============================================
CREATE PROCEDURE [dbo].[PROD_Update_Columns_U]
	@ColumnId int
	,@TableId int
	,@ColumnName nvarchar(128)
	,@DataType nvarchar(128)
	,@Size int
	,@IsNullable bit
	,@IsPrimaryKey bit
	,@IsForeignKey bit
	,@Description nvarchar(MAX)
	,@UsersId int
AS
BEGIN	
	BEGIN TRY						
		UPDATE 
			Columns 
		SET TableId = @TableId, ColumnName = @ColumnName, DataType = @DataType, Size = @Size, IsNullable = @IsNullable, IsPrimaryKey = @IsPrimaryKey, IsForeignKey = @IsForeignKey, Description = @Description, UsersId = @UsersId
		WHERE ColumnId = @ColumnId
		SELECT 'success'
	END TRY
	BEGIN CATCH		
		SELECT ERROR_MESSAGE();
	END CATCH
END