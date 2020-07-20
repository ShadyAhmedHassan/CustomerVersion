Alter Table tblDailyQuota
Add Finished bit
------------------------------
GO
UpDate tblDailyQuota set Finished=0 where Finished is null
------------------------------
GO
Alter Table tblDailyQuota
Add OutGoingQty decimal(12,3)
------------------------------
GO
UpDate tblDailyQuota set OutGoingQty=0 where OutGoingQty is null
------------------------------
GO