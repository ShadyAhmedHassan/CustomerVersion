Alter Function [dbo].[SetOut_Date](@Out_Date Nvarchar(50))
returns Nvarchar(11)
begin
Declare @DDate Datetime,@EndDate Datetime,@DHour int,@OUTDate Nvarchar(50),@MDate NVarchar(50)

select @MDate=max(ff) from 
(select isnull(max(Shift_Start_Time),0) ff from tblShift where Shift_Start_Date =1 
union 
select isnull(max(Shift_End_Time),0)  ff from tblShift where Shift_End_Date =1) gg

set @DDate=Cast(@Out_Date as Datetime)
Set @OUTDate=Cast(DATEPART(DD,@DDate) as Nvarchar(2))+'-'+dbo.GetMonthName(DATEPART(MONTH,@DDate))+'-'+Cast(DATEPART(YEAR,@DDate) as Nvarchar(4))

Set @EndDate=cast(@MDate as Datetime)

set @DHour=DATEPART(HH,@DDate)

if(@DHour>=0 and @DHour<=DATEPART(HH,@EndDate))
begin
Set @DDate=DATEADD(DD,-1,@DDate)

--set @OUTDate=REPLACE(Cast(@DDate as Nvarchar(50)),'Õ','AM')
Set @OUTDate=Cast(DATEPART(DD,@DDate) as Nvarchar(2))+'-'+dbo.GetMonthName(DATEPART(MONTH,@DDate))+'-'+Cast(DATEPART(YEAR,@DDate) as Nvarchar(4))
End

Return @OUTDate 

end 