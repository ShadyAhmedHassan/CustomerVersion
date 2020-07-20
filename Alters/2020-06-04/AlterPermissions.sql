Set identity_insert dbo.tblPermission on
GO 
Insert tblPermission([Permission_ID],[Permission_Name]) Values(21,'بلد المنشأ')
Insert tblPermission([Permission_ID],[Permission_Name]) Values(22,'بيانات الرسائل')
Insert tblPermission([Permission_ID],[Permission_Name]) Values(23,'الميناء')
Insert tblPermission([Permission_ID],[Permission_Name]) Values(24,'From Email')
Insert tblPermission([Permission_ID],[Permission_Name]) Values(25,'To Email')
Set identity_insert dbo.tblPermission off
GO