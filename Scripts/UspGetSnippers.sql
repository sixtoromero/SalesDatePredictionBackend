CREATE PROCEDURE [dbo].[UspGetSnippers]
AS
	SELECT shipperid
      ,companyname
      ,phone
  FROM Sales.Shippers
