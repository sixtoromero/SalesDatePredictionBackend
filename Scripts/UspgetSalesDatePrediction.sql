CREATE PROCEDURE [dbo].[UspgetSalesDatePrediction]
AS
	SELECT 
		C.custid as CustId,
		C.companyname AS CustomerName,
		MAX(O.orderdate) AS LastOrderDate,
		DATEADD(DAY, ROUND(AVG(DATEDIFF(DAY, O.orderdate, NextOrderDate)), 0), MAX(O.orderdate)) AS NextPredictedOrder
	FROM Sales.Customers C
	INNER JOIN Sales.Orders O ON C.custid = O.custid
	LEFT JOIN (
		SELECT 
			so.custid,
			so.orderdate,
			LEAD(so.orderdate) OVER (PARTITION BY so.custid ORDER BY so.orderdate) AS NextOrderDate
		FROM Sales.Orders so
	) AS OrderDates ON O.custid = OrderDates.custid AND O.orderdate = OrderDates.orderdate
	WHERE OrderDates.NextOrderDate IS NOT NULL
	GROUP BY C.custid, C.companyname
	ORDER BY CustomerName;
