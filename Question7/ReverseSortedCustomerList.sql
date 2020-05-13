USE [Availity]
GO

-- Produce a reverse-sorted list (alphabetically by name) of Customers (First and Last names)
--   whose last name begins with the letter 'S'
SELECT LastName, FirstName
FROM Customer
WHERE SUBSTRING(LastName,1,1) = 'S'
ORDER BY LastName DESC, FirstName DESC;
GO
