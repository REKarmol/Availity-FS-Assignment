USE [Availity]
GO

-- query to show the total value of all orders each customer has placed in the past 6 months.
-- Restrict to orders > 100 and < 500 (don't need LEFT JOIN anymore)
SELECT
	c.CustID,
	c.LastName,
	c.FirstName,
	SUM(COALESCE(l.Cost,0)*COALESCE(l.Quantity,0)) AS Total
FROM Customer c
	JOIN [Order] o ON o.CustomerID=c.CustID and o.OrderDate >= DATEADD(MONTH, -6, GETDATE())
	JOIN OrderLine l ON l.OrdID=o.OrderID
GROUP BY c.CustID, c.LastName, c.FirstName
HAVING SUM(COALESCE(l.Cost,0)*COALESCE(l.Quantity,0)) BETWEEN 100.01 AND 499.99
