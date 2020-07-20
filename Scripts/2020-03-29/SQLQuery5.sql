set dateformat dmy select  distinct dbo.SetOut_Date(tblTransactions.Out_Date)  [Date]  ,Dealer_Short_Name Customer,
tblProtype.ProtypeName, tblGoodtype.GoodtypeName,
TQTY [Quantity,MT]  ,DebitNote [Debit Note number]  , ContractNO [contract No.] 
from tblContracts 
 join tblDebitNote on tblContracts .ContractID = tblDebitNote .ContractID  
 join tblDailyQuota on tblDebitNote .DebitNoteID = tblDailyQuota .DebitNoteID 
  join tblTransQuota  on tblDailyQuota .QuotaID = tblTransQuota .QuotaID 
  join tblTransactions on tblTransQuota .TransID =tblTransactions .Trans_ID 
   join tblDealer on tblTransactions .Dealer_ID = tblDealer .Dealer_ID 
   join tblGood on tblTransactions .Good_ID =tblGood .Good_ID  
  join tblProtype on tblGood .ProtypeID  = tblProtype.ProtypeID   
  join tblGoodtype on tblGood.GoodtypeID  = tblGoodtype.GoodtypeID
  Join
  (
  Select C.ContractID,DN .DebitNoteID,G .ProtypeID,G.GoodtypeID,T.Out_Date,sum (Second_Weight - First_Weigth) TQTY
  From tblContracts C 
  join tblDebitNote DN on C .ContractID = DN .ContractID  
  join tblDailyQuota DQ on DN .DebitNoteID = DQ.DebitNoteID 
  join tblTransQuota TQ  on DQ .QuotaID = TQ.QuotaID 
  join tblTransactions T on TQ.TransID =T .Trans_ID  
  join tblGood G on T .Good_ID =G .Good_ID
  where TR_TY =2 
  and  CONVERT(date,T.Out_Date,103) between CONVERT(date,'01/03/2020',103) and  CONVERT(date,'30/03/2020',103)  
  Group By C.ContractID,DN.DebitNoteID,G .ProtypeID,G.GoodtypeID,T.Out_Date 
  )QQ on (tblContracts .ContractID=QQ.ContractID and tblDebitNote .DebitNoteID=QQ.DebitNoteID and tblGood .ProtypeID=QQ.ProtypeID and tblGood.GoodtypeID=QQ.GoodtypeID and tblTransactions.Out_Date=QQ.Out_Date)  
  
  where TR_TY =2  
  and  CONVERT(date,tblTransactions.Out_Date,103) between CONVERT(date,'01/03/2020',103) and  CONVERT(date,'30/03/2020',103) 
  group by tblTransactions.Out_Date, ContractNO,DebitNote,Dealer_Short_Name ,tblProtype.ProtypeName   , tblGoodtype.GoodtypeName,TQTY  

--select * from tblGood 

--select * from tblProtype 

--select * from tblGoodtype 


set dateformat dmy Select * from tblTransactions where cast(Out_Date as date)='31/03/2020'