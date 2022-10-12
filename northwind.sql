--https://raw.githubusercontent.com/microsoft/sql-server-samples/master/samples/databases/northwind-pubs/instnwnd.sql

  --1. Get the list of not discontinued products including category name ordered by product name.

Select p.*, [CategoryName] 
from Products p
Join Categories c on c.CategoryID=p.CategoryID
where p.Discontinued !=1
Order by ProductName ;

--2. Get all Nancy Davolio’s customers.

select distinct C.* from Customers c
inner join Orders o on o.CustomerID = c.CustomerID
inner join Employees E on E.EmployeeID=o.EmployeeID
where E.FirstName = 'Nancy' and E.LastName = 'Davolio'


--3. Get the total ordered amount (money) by year of the employee Steven Buchanan.

	SELECT YEAR(o.OrderDate) AS [Year],SUM((UnitPrice*Quantity)-(UnitPrice*Discount)) AS Amount FROM [Order Details] AS od
	INNER JOIN Orders o on o.OrderID = od.OrderID
	INNER JOIN Employees e on e.EmployeeID=o.EmployeeID
 	WHERE e.FirstName = 'Steven ' AND e.LastName = 'Buchanan'
 	GROUP BY YEAR(o.OrderDate)
	

--4. Get the name of all employees that directly or indirectly report to Andrew Fuller.

		; WITH  CTE AS 
      		  (
        		SELECT  *
        		FROM    [Employees]
        		WHERE   [FirstName] = 'Andrew' AND LastName = 'Fuller'
        		UNION  ALL
        		SELECT  child.*
       		FROM    [Employees] child
        		JOIN    CTE parent
        		ON      child.[ReportsTo] = parent.[EmployeeID]
        	)
	SELECT  *
	FROM    CTE cte
	WHERE [FirstName] != 'Andrew' AND LastName != 'Fuller'