Alter Table tblGood
Add ProtypeID int
-----------------------------------
GO
Alter Table tblGood
Add GoodtypeID int
-----------------------------------
GO
UpDate tblGood set ProtypeID=0 where ProtypeID is null
-----------------------------------
GO
UpDate tblGood set GoodtypeID=0 where GoodtypeID is null
-----------------------------------
GO
