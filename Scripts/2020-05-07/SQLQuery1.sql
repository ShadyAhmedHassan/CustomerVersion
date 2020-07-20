 set dateformat dmy 
 select tblDebitNote.ContractID,CQTY.DQuota as Quota,Dealer_Name ,Good_Name,  isnull(CQTY.TQTY,0) Totalweight, 
 (select isnull(sum(Second_Weight-First_Weigth) ,0) as TotalweightTrans from tblTransactions  where Out_Date is not null and TR_TY=2
  and  CONVERT(date, Out_Date) <= '08/05/2020 07:59:59.998 AM'   and  tblTransactions.Dealer_ID = tblDealer.Dealer_ID 
  and  tblTransactions.Good_ID = tblGood.Good_ID) as TotalweightTrans,  (select count(Trans_ID) as CarInSide from  tblTransactions where Out_Date is null  
  and tblTransactions.Dealer_ID = tblDealer.Dealer_ID and tblTransactions.Good_ID = tblGood.Good_ID)  as CarInSide 
  from tblDailyQuota  
  Left join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID   
  left join tblContracts on tblContracts.ContractID = tblDebitNote.ContractID    
  left join tblContractDet on (tblContracts.ContractID=tblContractDet.ContractID)  
  left join tblGood on tblGood.Good_ID = tblContractDet.GoodID   
  left join tblDealer on tblDealer.Dealer_ID = tblContracts.CustomerID   
  left join(select tblDebitNote.ContractID,isnull(sum(tblTransQuota.Quantity),0)+isnull(sum(tblDailyQuota.OutGoingQty),0) TQTY,isnull(sum(tblDailyQuota.Quota)*1000,0) DQuota  from tblDailyQuota 
  left join  tblTransQuota on tblTransQuota .QuotaID =tblDailyQuota .QuotaID and  DateQuota <=convert(Date,'07/05/2020') and tblDailyQuota.Finished=0 
  left join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID 
 where DateQuota <=convert(Date,'07/05/2020') Group By tblDebitNote.ContractID )CQTY on (tblDebitNote.ContractID=CQTY.ContractID) 
 where tblDailyQuota.Finished=0 and DateQuota <=  CONVERT(date,'07/05/2020') 
 group by tblDealer.Dealer_ID,tblDealer.Dealer_Name,tblGood.Good_Name,tblGood.Good_ID,tblDebitNote.ContractID,CQTY.TQTY,CQTY.DQuota,tblContracts.ContractID  
 Union All 
 Select D.DailyQuotaID ContractID,isnull(DQWC.TQTY,0) Quota,DA.Dealer_Name,G.Good_Name,isnull(sum(T.Quantity),0) as TotalweightTrans,0,  
 (select count(Trans_ID) as CarInSide from  tblTransactions where Out_Date is null  and tblTransactions.Dealer_ID = DA.Dealer_ID and tblTransactions.Good_ID = G.Good_ID)  as CarInSide  
 from   tblDailyQuotaWithOutContract D 
 Left join
 (
 Select DD.DailyQuotaID,Sum(DD.Quantity)*1000 TQTY
 From   tblDailyQuotaWithOutContract DD
 Group BY DD.DailyQuotaID
 )DQWC on(D.DailyQuotaID=DQWC.DailyQuotaID)
 left join   tblDealer DA on DA.Dealer_ID = D.CustomerID  
 left join tblDailyQuotaWOContractGoods DG on(D.DailyQuotaID=DG.DailyQuotaID) 
 left join tblGood G on(DG.GoodID=G.Good_ID) 
 left join tblTransQuotaWithOutContract T on(D.DailyQuotaID=T.QuotaID)  
 where  D.QuotaDate<=convert(Date,'07/05/2020') and D.finish=0 
 Group by D.DailyQuotaID,DA.Dealer_ID,DA.Dealer_Name,G.Good_ID,G.Good_Name,DQWC.TQTY Order By Dealer_Name 