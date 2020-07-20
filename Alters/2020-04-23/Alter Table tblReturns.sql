Alter Table tblReturns
Add LocationID int
-----------------------------------
GO
UpDate tblReturns set LocationID=0 where LocationID is null
-----------------------------------
GO