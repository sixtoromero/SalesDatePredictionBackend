-- =============================================
-- Author:		Sixto José Romero Martínez
-- Create date: 07-Julio-2023 (dd-MM-yyyy)
-- Description:	Procedimiento encargado de insertar los indixes de las columnas de las tablas a documentar
-- =============================================
CREATE PROCEDURE [dbo].[PROD_Insertar_Indexes_I]
	@TableId int
	,@IndexName nvarchar(128)
	,@IndexType nvarchar(50)
	,@IncludedColumns nvarchar(MAX)
	,@UsersId int
AS
BEGIN
	BEGIN TRY						
		INSERT INTO Indexes (
			TableId
			,IndexName
			,IndexType
			,IncludedColumns
			,UsersId
		)
		VALUES (
			@TableId
			,@IndexName
			,@IndexType
			,@IncludedColumns
			,@UsersId
		)
		SELECT 'success'
	END TRY
	BEGIN CATCH		
		SELECT ERROR_MESSAGE();
	END CATCH
END