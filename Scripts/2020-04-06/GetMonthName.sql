Create Function GetMonthName(@MNO int)
returns NVarchar(20)

Begin
Declare @MonthName NVarchar(20)

if(@MNO=1)
set @MonthName='Jan'
else if(@MNO=2)
set @MonthName='Feb'
else if(@MNO=3)
set @MonthName='Mar'
else if(@MNO=4)
set @MonthName='apr'
else if(@MNO=5)
set @MonthName='May'
else if(@MNO=6)
set @MonthName='Jun'
else if(@MNO=7)
set @MonthName='Jul'
else if(@MNO=8)
set @MonthName='Aug'
else if(@MNO=9)
set @MonthName='Sep'
else if(@MNO=10)
set @MonthName='Oct'
else if(@MNO=11)
set @MonthName='Nov'
else if(@MNO=12)
set @MonthName='Dec'

return @MonthName
end
