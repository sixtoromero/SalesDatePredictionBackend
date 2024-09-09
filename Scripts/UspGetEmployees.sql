CREATE PROCEDURE [dbo].[UspGetEmployees]
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
