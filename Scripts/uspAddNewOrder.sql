CREATE PROCEDURE [dbo].[uspAddNewOrder]
	@orderid INT OUTPUT,
	@empid int,
	@shipperid int,
	@shipname nvarchar(80),
	@shipaddress nvarchar(120),
	@shipcity nvarchar(30),
	@orderdate datetime,
	@requireddate datetime,
	@shippeddate datetime,
	@freight money,
	@shipcountry nvarchar(30),
	@OrderDetailsJSON NVARCHAR(MAX)
AS
BEGIN
	BEGIN TRY

		BEGIN TRANSACTION;		

		INSERT INTO Sales.Orders (empid, shipperid, shipname, shipaddress, shipcity, orderdate, requireddate, shippeddate, freight, shipcountry)
	    VALUES (@empid, @shipperid, @shipname, @shipaddress, @shipcity, @orderdate, @requireddate, @shippeddate, @freight, @shipcountry);

		-- Obtén el ID de la orden recién creada
		SET @Orderid = SCOPE_IDENTITY();

		INSERT INTO Sales.OrderDetails (orderid, productid, unitprice, qty, discount)
		SELECT 
			@Orderid AS orderid,			
			productid, 
			unitprice, 
			qty, 
			discount
		FROM OPENJSON(@OrderDetailsJSON)
		WITH (
			orderid INT,
			productid INT,
			unitprice money,
			qty smallint,
			discount numeric(4, 3)
		);


		COMMIT TRANSACTION;
		SELECT 'success';

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

		DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();		
	END CATCH
END
