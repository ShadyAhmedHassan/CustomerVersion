
Select * from tblDailyQuotaWithOutContract 
join tblDailyQuotaWOContractGoods on tblDailyQuotaWithOutContract.DailyQuotaID=tblDailyQuotaWOContractGoods.DailyQuotaID
where CustomerID =17116

select sum( tblTransQuotaWithOutContract.Quantity)  from tblTransQuotaWithOutContract 
join  tblDailyQuotaWithOutContract on tblTransQuotaWithOutContract.QuotaID=tblDailyQuotaWithOutContract.DailyQuotaID
join tblDailyQuotaWOContractGoods on tblDailyQuotaWithOutContract.DailyQuotaID=tblDailyQuotaWOContractGoods.DailyQuotaID
where CustomerID =17106


set dateformat dmy
select sum(Second_Weight -First_Weigth  ) from tblTransactions 
where Good_ID ='0701' and Dealer_ID ='17106' 
and Out_Date > CONVERT(datetime,'15-07-2020 08:00:00',103)