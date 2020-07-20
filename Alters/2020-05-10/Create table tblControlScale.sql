Create table tblControlScale
(
ControlScaleID int identity(1,1),
UserID         int,
MoveDate       Nvarchar(50),
MoveWeight     int
)
---------------------------------------------
GO
Alter Table tblScale
Add LastWeight int
---------------------------------------------
GO
