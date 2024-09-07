--EXEC PROD_GetByUserAndPassword_C 'admin', '123'
CREATE PROCEDURE PROD_GetByUserAndPassword_C
	@UserName NVARCHAR(50),
	@PasswordHash NVARCHAR(256)
AS
SELECT Id
,FirstName
,LastName
,Username
,Email FROM Users WHERE UserName = @UserName AND PasswordHash = @PasswordHash