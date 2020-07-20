Alter Table tblUpdateTransactions
Add Flag Int
--------------------------------------
GO
UpDate tblUpdateTransactions set Flag=0 where Flag is null
--------------------------------------
GO 