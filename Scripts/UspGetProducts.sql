CREATE PROCEDURE [dbo].[UspGetProducts]
AS
	SELECT 
		productid
		,productname
		,supplierid
		,categoryid
		,unitprice
		,discontinued
	FROM Production.Products
