CREATE PROCEDURE [dbo].[UspGetClientOrders]
	@custid int
AS
	SELECT 
		orderid,
		requireddate,
		shippeddate,
		shipname,
		shipaddress,
		shipcity
	FROM Sales.Orders
	WHERE custid = @custid
