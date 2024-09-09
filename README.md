
# Sales Date Prediction
## Descripción
Este proyecto fue desarrollado utilizando .NET 8 con una arquitectura basada en el Diseño Dirigido por el Dominio (DDD).
## Arquitectura DDD
El Diseño Dirigido por el Dominio (DDD) se centra en entender y representar los conceptos clave del dominio de negocio, promoviendo una estructura de código clara y modular. Esta arquitectura facilita el desarrollo de aplicaciones complejas al alinear el modelo de software con los procesos y terminología del dominio.

## Características

- Implementación de patrones de DDD para una mejor organización del código.
- Modularidad y separación de responsabilidades a través de capas.
- Uso de .NET 8 para aprovechar las últimas características y mejoras de rendimiento.
- 
## Nota Importante sobre la Arquitectura

En la implementación de la solución, se utiliza una interfaz que hereda de una interfaz principal que define varios métodos. Sin embargo, no todos los métodos de la interfaz principal se implementan en esta solución, ya que no todos son necesarios para cumplir con los requisitos del dominio específico. Esta decisión de diseño se tomó para mantener la simplicidad y enfocarse únicamente en las funcionalidades relevantes, respetando al mismo tiempo los principios de diseño orientado a objetos.


## Configuración del Proyecto

En la capa de servicios, dentro de la carpeta `Services`, se encuentra el proyecto `SalesDatePrediction.Services.WebAPIRest` que es el proyecto que se debe ejecutar y que contiene el archivo de configuración `appsettings.json`. 

Es importante actualizar la clave de conexión `Connection` en el archivo `appsettings.json` para que apunte al servidor de base de datos correcto. A continuación, se proporciona un ejemplo de cómo debe configurarse:

```json
"ConnectionStrings": {
  "Connection": "Data Source=DESKTOP-IEOP1NV\\SQLEXPRESS; Min Pool Size=0; Max Pool Size=500; Pooling=true; Initial Catalog=StoreSample; MultipleActiveResultSets=True; Persist Security Info=True; User ID=sa; Password=1133557799;"
}
```
## Procedimientos Almacenados

A continuación se detallan los procedimientos almacenados que deben ser añadidos a la base de datos para asegurar el correcto funcionamiento de la aplicación:

> **Nota Importante:** En la solución está una carpeta llamada Scripts, allí están todos los procedimientos almacenados que se crearon.

### 1. `UspgetSalesDatePrediction`

Este procedimiento consulta las predicciones de ventas para los clientes, basándose en sus historiales de pedidos.

```sql
CREATE PROCEDURE UspgetSalesDatePrediction
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
```

### 2. `UspGetClientOrders`

Este procedimiento conslta las ordenes de los clientes

```sql
CREATE PROCEDURE UspGetClientOrders
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
```


### 3. `UspGetEmployees`

Procedimiento encargado de listar todos los empleados

```sql
CREATE PROCEDURE UspGetEmployees
AS
	SELECT empid
      ,lastname
      ,firstname
      ,title
      ,titleofcourtesy
      ,birthdate
      ,hiredate
      ,address
      ,city
      ,region
      ,postalcode
      ,country
      ,phone
      ,mgrid
  FROM HR.Employees
```
### 4. `UspGetProducts`

Procedimiento encargado de listar todos los productos

```sql
CREATE PROCEDURE UspGetProducts
AS
	SELECT 
		productid
		,productname
		,supplierid
		,categoryid
		,unitprice
		,discontinued
	FROM Production.Products
```

### 5. `UspGetSnippers`

Procedimiento encargado de listar todos los transportistas

```sql
CREATE PROCEDURE UspGetSnippers
AS
	SELECT shipperid
      ,companyname
      ,phone
  FROM Sales.Shippers
```

### 6. `uspAddNewOrder`

Procedimiento encargado de crear una nueva orden con sus detalles.

```sql
CREATE PROCEDURE uspAddNewOrder
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

```
