 set dateformat dmy
 
 Declare @Temp Table
 (
 DayDate Nvarchar(100),
 Customer Varchar(300),
 ConcATENATEno Varchar(500),
 ProTypeID     int,
 ProtypeName   Varchar(300),
 GoodTypeID    int,
 GoodtypeName  Varchar(300),
 DebitNoteID    int,
 DebitNote     Varchar(100),
 ContractID    int,
 ContractNO    Varchar(100)
 )

  Declare @Temp2 Table
 (
 DayDate Nvarchar(100),
 Customer Varchar(300),
 ConcATENATEno Varchar(500),
 ProTypeID     int,
 ProtypeName   Varchar(300),
 GoodTypeID    int,
 GoodtypeName  Varchar(300),
 DebitNoteID    int,
 DebitNote     Varchar(100),
 ContractID    int,
 ContractNO    Varchar(100),
 TransID       int
 )

 insert into @Temp
  select distinct  dbo.SetOut_Date(tblTransactions.Out_Date) [Date],Dealer_Short_Name Customer, dbo.SetOut_Date(tblTransactions.Out_Date)  + '\' + DebitNote  + '\' + Dealer_Short_Name  + '\' + tblProtype.ProtypeName  + '\'   +  tblGoodtype.GoodtypeName   ConcATENATEno,  
 tblProtype.ProtypeID,tblProtype.ProtypeName,tblGoodtype.GoodtypeID,tblGoodtype.GoodtypeName,tblDebitNote .DebitNoteID,DebitNote [Debit Note number]  ,tblDebitNote .ContractID, ContractNO [contract No.]  
 from tblContracts join tblDebitNote on tblContracts .ContractID = tblDebitNote .ContractID  
 join tblDailyQuota on tblDebitNote .DebitNoteID = tblDailyQuota .DebitNoteID   
 join tblTransQuota  on tblDailyQuota .QuotaID = tblTransQuota .QuotaID  
 join tblTransactions on tblTransQuota .TransID =tblTransactions .Trans_ID  
 join tblDealer on tblTransactions .Dealer_ID = tblDealer .Dealer_ID  
 join tblGood on tblTransactions .Good_ID =tblGood .Good_ID  
 join tblProtype on tblGood .ProtypeID  = tblProtype.ProtypeID  
 join tblGoodtype on tblGood.GoodtypeID  = tblGoodtype.GoodtypeID
 where TR_TY =2 and  convert(Datetime,tblTransactions.Out_Date,103) between '14/06/2020 08:00:00 AM' and  '15/06/2020 07:59:59.998 AM' and tblTransactions.Out_Date is not null
 
 --Select * from @Temp

 insert into @Temp2
  select distinct  dbo.SetOut_Date(tblTransactions.Out_Date) [Date],Dealer_Short_Name Customer, dbo.SetOut_Date(tblTransactions.Out_Date)  + '\' + DebitNote  + '\' + Dealer_Short_Name  + '\' + tblProtype.ProtypeName  + '\'   +  tblGoodtype.GoodtypeName   ConcATENATEno,  
 tblProtype.ProtypeID,tblProtype.ProtypeName,tblGoodtype.GoodtypeID,tblGoodtype.GoodtypeName,tblDebitNote .DebitNoteID,DebitNote [Debit Note number]  ,tblDebitNote .ContractID, ContractNO [contract No.],tblTransactions.Trans_ID  
 from tblContracts join tblDebitNote on tblContracts .ContractID = tblDebitNote .ContractID  
 join tblDailyQuota on tblDebitNote .DebitNoteID = tblDailyQuota .DebitNoteID   
 join tblTransQuota  on tblDailyQuota .QuotaID = tblTransQuota .QuotaID  
 join tblTransactions on tblTransQuota .TransID =tblTransactions .Trans_ID  
 join tblDealer on tblTransactions .Dealer_ID = tblDealer .Dealer_ID  
 join tblGood on tblTransactions .Good_ID =tblGood .Good_ID  
 join tblProtype on tblGood .ProtypeID  = tblProtype.ProtypeID  
 join tblGoodtype on tblGood.GoodtypeID  = tblGoodtype.GoodtypeID
 where TR_TY =2 and  convert(Datetime,tblTransactions.Out_Date,103) between '14/06/2020 08:00:00 AM' and  '15/06/2020 07:59:59.998 AM' and tblTransactions.Out_Date is not null
 


  
 select T.DayDate [Date],T.Customer, dbo.SetOut_Date(T.DayDate)  + '\' + DebitNote  + '\' + T.Customer  + '\' + T.ProtypeName  + '\'   +  T.GoodtypeName   ConcATENATEno,  
 T.ProtypeName, T.GoodtypeName,  TQTY/1000 [Quantity,MT]  ,T.DebitNote [Debit Note number]  ,T.ContractNO [contract No.]  
 from @Temp T  
 Join(
 Select Q.ContractID,Q.DebitNoteID,Q.ProtypeID,Q.GoodtypeID,sum (TR.Second_Weight - TR.First_Weigth) TQTY 
 From @Temp2 Q 
 join tblTransactions TR on(Q.TransID=TR.Trans_ID)
  where TR_TY =2 and  convert(Datetime,TR.Out_Date,103) between '14/06/2020 08:00:00 AM' and  '15/06/2020 07:59:59.998 AM' and TR.Out_Date is not null  
  Group By Q.ContractID,Q.DebitNoteID,Q.ProtypeID,Q.GoodtypeID)QQ 
  on (T.ContractID=QQ.ContractID and T.DebitNoteID=QQ.DebitNoteID and T .ProtypeID=QQ.ProtypeID and T.GoodtypeID=QQ.GoodtypeID) 