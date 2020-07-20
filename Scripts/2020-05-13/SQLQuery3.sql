set dateformat dmy 
select tblDebitNote.ContractID,DQty.DQ as Quota,Dealer_Name ,Good_Name,  isnull(CQTY.TQTY,0) Totalweight, 
/*(select isnull(sum(Second_Weight-First_Weigth) ,0) as TotalweightTrans from tblTransactions  
where Out_Date is not null and TR_TY=2 and  CONVERT(date, Out_Date) <= '14/05/2020 07:59:59.998 AM'   and  tblTransactions.Dealer_ID = tblDealer.Dealer_ID and  
tblTransactions.Good_ID = tblGood.Good_ID) as*/0 TotalweightTrans, 
(select count(Trans_ID) as CarInSide from  tblTransactions where Out_Date is null and tblTransactions.Dealer_ID = tblDealer.Dealer_ID and tblTransactions.Good_ID = tblGood.Good_ID)  as CarInSide  
from tblDailyQuota 
Left join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID  
left join tblContracts on tblContracts.ContractID = tblDebitNote.ContractID 
left join tblContractDet on (tblContracts.ContractID=tblContractDet.ContractID)  
left join tblGood on tblGood.Good_ID = tblContractDet.GoodID 
left join tblDealer on tblDealer.Dealer_ID = tblContracts.CustomerID  
left join
(
select tblDebitNote.ContractID,isnull(sum(tblTransQuota.Quantity),0) TQTY  
from tblDailyQuota  
left join  tblTransQuota on tblTransQuota .QuotaID =tblDailyQuota .QuotaID and  DateQuota =convert(Date,'13/05/2020') and tblDailyQuota.Finished=0 
left join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID  where DateQuota =convert(Date,'13/05/2020')
Group By tblDebitNote.ContractID 
)CQTY on (tblDebitNote.ContractID=CQTY.ContractID)  
left join
(
select tblDebitNote.ContractID,isnull(sum(tblDailyQuota.Quota)*1000,0)-isnull(sum(tblDailyQuota.OutGoingQty*1000),0)-isnull(sum(PQ.PrevQ),0) DQ  
from tblDailyQuota  
left join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID and ((DateQuota <=convert(Date,'13/05/2020') and tblDailyQuota.Finished=0) OR (EndDate=convert(Date,'13/05/2020') and tblDailyQuota.Finished=1))
left join
(
Select TQ.QuotaID,T.Second_Weight-T.First_Weigth PrevQ
From   tblTransactions T
Left join tblTransQuota TQ on(T.Trans_ID=TQ.TransID)
where  Convert(Date,T.Out_Date)<convert(Date,'13/05/2020')
)PQ on(tblDailyQuota.QuotaID=PQ.QuotaID)
where ((DateQuota <=convert(Date,'13/05/2020') and tblDailyQuota.Finished=0) OR (EndDate=convert(Date,'13/05/2020') and tblDailyQuota.Finished=1)) 
Group By tblDebitNote.ContractID,PQ.PrevQ
)DQTY
on (tblDebitNote.ContractID=DQTY.ContractID) 
where tblDailyQuota.Finished=0 and DateQuota <=  CONVERT(date,'13/05/2020')  
group by tblDealer.Dealer_ID,tblDealer.Dealer_Name,tblGood.Good_Name,tblGood.Good_ID,tblDebitNote.ContractID,CQTY.TQTY,DQty.DQ,tblContracts.ContractID  
Union All 
Select D.DailyQuotaID ContractID,isnull(DQWC.TQTY,0) Quota,DA.Dealer_Name,G.Good_Name,isnull(trans.TransQ,0) as TotalweightTrans,0, 
(select count(Trans_ID) as CarInSide from  tblTransactions where Out_Date is null  and tblTransactions.Dealer_ID = DA.Dealer_ID and tblTransactions.Good_ID = G.Good_ID)  as CarInSide 
from tblDailyQuotaWithOutContract D
Left Join
(
Select DD.DailyQuotaID,(Sum(DD.Quantity)*1000)-isnull(sum(PQ.PrevQ),0) TQTY 
From   tblDailyQuotaWithOutContract DD
left join
(
Select TQ.QuotaID,T.Second_Weight-T.First_Weigth PrevQ
From   tblTransactions T
Left join tblTransQuota TQ on(T.Trans_ID=TQ.TransID)
where  Convert(Date,T.Out_Date)<convert(Date,'13/05/2020')
)PQ on(DD.DailyQuotaID=PQ.QuotaID) 
Group BY DD.DailyQuotaID
)DQWC on(D.DailyQuotaID=DQWC.DailyQuotaID) 
left join tblDealer DA on DA.Dealer_ID = D.CustomerID 
left join tblDailyQuotaWOContractGoods DG on(D.DailyQuotaID=DG.DailyQuotaID)
left join tblGood G on(DG.GoodID=G.Good_ID)  
left join
(
Select  TT.QuotaID,sum(TT.Quantity) TransQ 
From tblTransQuotaWithOutContract TT
Left join tblTransactions T on(TT.TransID=T.Trans_ID)
where Convert(Date,Out_Date)=convert(Date,'13/05/2020')
Group By TT.QuotaID
)Trans on(D.DailyQuotaID=Trans.QuotaID)  
left join tblTransQuotaWithOutContract T on(D.DailyQuotaID=T.QuotaID) where  ((D.QuotaDate<=convert(Date,'13/05/2020') and D.finish=0) or (D.EndDate=convert(Date,'13/05/2020') and D.finish=1)) 
Group by D.DailyQuotaID,DA.Dealer_ID,DA.Dealer_Name,G.Good_ID,G.Good_Name,DQWC.TQTY,trans.TransQ 
Order By Dealer_Name 