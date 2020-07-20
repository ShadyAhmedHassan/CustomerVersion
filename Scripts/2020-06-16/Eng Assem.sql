set dateformat dmy
select dbo.SetOut_Date(tblTransactions.Out_Date) [Date],tblDealer .Dealer_Short_Name Customer, 
dbo.SetOut_Date(tblTransactions.Out_Date)  + '\' + DebitNote  + '\' + Dealer_Short_Name  + '\' + tblProtype.ProtypeName  + '\'   +  tblGoodtype.GoodtypeName   ConcATENATEno , tblProtype .ProtypeName,tblGoodtype .GoodtypeName, sum(tblTransQuota.Quantity)/1000 [Quantity,MT],
tbldebitnote.DebitNote [Debit Note number],tblContracts.ContractNO [contract No.]       
from tblTransactions 
join tblTransQuota  on tblTransactions .Trans_ID =tblTransQuota .TransID 
join tblDailyQuota on tblTransQuota .QuotaID =tblDailyQuota .QuotaID 
join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID 
join tblContracts on tblDebitNote .ContractID =tblContracts .ContractID 

join tblDealer on tblTransactions .Dealer_ID =tblDealer .Dealer_ID 
join tblGood on tblTransactions .Good_ID =tblGood .Good_ID 
join tblProtype on tblGood .ProtypeID  = tblProtype.ProtypeID  
join tblGoodtype on tblGood.GoodtypeID  = tblGoodtype.GoodtypeID
where TR_TY =2 and  convert(Datetime,tblTransactions.Out_Date,103) between '14/06/2020 08:00:00 AM' and  '15/06/2020 07:59:59.998 AM' 
and  out_date is not null
group by dbo.SetOut_Date(tblTransactions.Out_Date),tblDealer .Dealer_Short_Name,tblProtype .ProtypeName,tblGoodtype .GoodtypeName,tbldebitnote.DebitNote  ,tblContracts.ContractNO  
