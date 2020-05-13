USE [Availity]
GO

-- query to show the total value of all orders each customer has placed in the past 6 months
-- Customers without orders should show 0$
SELECT
	c.CustID,
	c.LastName,
	c.FirstName,
	SUM(COALESCE(l.Cost,0)*COALESCE(l.Quantity,0)) AS Total
FROM Customer c
	LEFT JOIN [Order] o ON o.CustomerID=c.CustID and o.OrderDate >= DATEADD(MONTH, -6, GETDATE())
	LEFT JOIN OrderLine l ON l.OrdID=o.OrderID
GROUP BY c.CustID, c.LastName, c.FirstName
