select *
from Suppliers_Tbl

select *
from Products_Tbl

select *
from SupplierProduct_Tbl

select *
from Orders_Tbl


select *
from OrdersProduct_Tbl


select *
from Orders_Tbl


SELECT * 
FROM SupplierProduct_Tbl sp
JOIN Suppliers_Tbl s ON sp.SupplierId = s.Id
WHERE s.CompanyName = 'итоп';