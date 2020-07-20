Select UT.CID,UT.Trans_ID,L.LocationName,UT.Car_ID,D.Dealer_Name,DR.Driver_Name,G.Good_Name,UT.First_Weigth,UT.Second_Weight,UT.Out_Date,
       case when UT.MSGID=1 then '≈”ﬂ‰œ—Ì…' when UT.MSGID=2 then '«·⁄«„—Ì…' else '' end MSGName,UT.Slip_Rate,UT.ManualEdit 
From   tblUpdateTransactions UT
join   tblLocation L on(UT.LocationID=L.LocationID)
join   tblDealer D on(UT.Dealer_ID=D.Dealer_ID)
join   tblDriver DR on (UT.Driver_ID=DR.Driver_ID)
join   tblIssueTo I on(UT.Issue_To_ID=I.Issue_To_ID)
join   tblGood G on(UT.Good_ID=G.Good_ID)
where  CAST(UT.In_Date as Date) Between '' and ''

select * from tblUpdateTransactions 