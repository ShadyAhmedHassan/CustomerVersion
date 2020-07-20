  set dateformat dmy
  select tblDebitNote.ContractID,sum ((Quota)) as Quota,Dealer_Name ,Good_Name,
 (select isnull(sum(Second_Weight-First_Weigth) ,0) as TotalweightTrans from tblTransactions  where Out_Date is not null and TR_TY=2 and  CONVERT(date, Out_Date)
  between '19/03/2020 08:00:00 AM' and '23/03/2020 07:59:59.998 AM'  and  tblTransactions.Dealer_ID = tblDealer.Dealer_ID and  tblTransactions.Good_ID = tblGood.Good_ID) as TotalweightTrans,
 (select count(Trans_ID) as CarInSide from  tblTransactions where Out_Date is null  and tblTransactions.LocationID=1 and tblTransactions.Dealer_ID = tblDealer.Dealer_ID and tblTransactions.Good_ID = tblGood.Good_ID)  as CarInSide,DateQuota,
  CQTY.TQTY 
  from tblDailyQuota
  join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID   
  join tblContracts on tblContracts.ContractID = tblDebitNote.ContractID    
  join tblContractDet on (tblContracts.ContractID=tblContractDet.ContractID)
  join tblGood on tblGood.Good_ID = tblContractDet.GoodID   join tblDealer on tblDealer.Dealer_ID = tblContracts.CustomerID
  join 
  (
  select tblDebitNote.ContractID,isnull(sum(tblTransQuota.Quantity),0) TQTY 
  from tblTransQuota 
  join tblDailyQuota on tblTransQuota .QuotaID =tblDailyQuota .QuotaID 	and DateQuota =convert(Date,'22/03/2020')
  join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID
  where DateQuota =convert(Date,'22/03/2020')
  Group By tblDebitNote.ContractID  
  )CQTY on (tblDebitNote.ContractID=CQTY.ContractID)   
  where  DateQuota =  CONVERT(date,'22/03/2020') 
  group by tblDealer.Dealer_ID,tblDealer.Dealer_Name,tblGood.Good_Name,tblGood.Good_ID,tblDailyQuota.DateQuota,tblDebitNote.ContractID,CQTY.TQTY
  Order By ContractID,Dealer_Name

  Select *
  From    tblTransQuota D
  join    tblDailyQuota DQ on D .QuotaID =DQ .QuotaID 	and DateQuota =convert(Date,'19/03/2020')
  join     tblDebitNote on DQ .DebitNoteID =tblDebitNote .DebitNoteID







/*,  (select isnull(sum(tblTransQuota.Quantity),0) from tblTransQuota join tblDailyQuota on tblTransQuota .QuotaID =tblDailyQuota .QuotaID 	and DateQuota =convert(Date,'19/03/2020')
   join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID where tblDebitNote .ContractID in (select tblContractDet.ContractID   
   from  tblContractDet 
   join tblContracts on tblContractDet .ContractID =tblContracts .ContractID where CustomerID = tblDealer.Dealer_ID and GoodID =tblGood.Good_ID)) as Totalweight*/