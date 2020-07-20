Alter Table tblGood
Add ProtypeID int
-----------------------------------
GO
Alter Table tblGood
Add GoodtypeID int
-----------------------------------
GO
UpDate tblGood set ProtypeID=1 where ProtypeID is null
-----------------------------------
GO
UpDate tblGood set GoodtypeID=1 where GoodtypeID is null
-----------------------------------
GO
