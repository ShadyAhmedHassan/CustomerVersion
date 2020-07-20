Imports System.Data.SqlClient
Public Class frmrptQuotaForSales
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnSearchQuota_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchQuota.Click

        Try
            Dim ConnPath, ConnString As String
            ConnPath = Application.StartupPath & "\ConnString.txt"
            ConnString = GetConString(ConnPath)
            'ConnString = Server
            Dim Con As New SqlConnection(ConnString)

            Dim WhereCondition As String
            Dim FromToDate() As String = gMethods.GetShifName_And_SearchDateFT(True, dtpReportQuota.Value.Date, dtpReportQuota.Value.Date)
            'WhereCondition = " where Out_Date is not null and TR_TY=2 and  CONVERT(date, Out_Date) between '" & FromToDate(0) & "' and '" & FromToDate(1) & "' " & (If(CboLocation.SelectedValue <> Nothing And ChkLocation.Checked, " and tblTransactions.LocationID=" & CboLocation.SelectedValue, ""))
            WhereCondition = " where Out_Date is not null and TR_TY=2 and  CONVERT(date, Out_Date) <= '" & FromToDate(1) & "' " & (If(CboLocation.SelectedValue <> Nothing And ChkLocation.Checked, " and tblTransactions.LocationID=" & CboLocation.SelectedValue, ""))

            Dim SQLStatement As String

            '" ,(select isnull(sum(Second_Weight-First_Weigth) ,0) as TotalweightTrans from tblTransactions " & WhereCondition & _
            '" and  tblTransactions.Dealer_ID = tblDealer.Dealer_ID and  tblTransactions.Good_ID = tblGood.Good_ID) as TotalweightTrans " & _

            SQLStatement = " set dateformat dmy " & _
"select tblDebitNote.ContractID,DQty.DQ as Quota,Dealer_Name ,Good_Name,  isnull(CQTY.TQTY,0) Totalweight, " & _
" /*(select isnull(sum(Second_Weight-First_Weigth) ,0) as TotalweightTrans from tblTransactions " & _
" where Out_Date is not null and TR_TY=2 and  CONVERT(date, Out_Date) <= '14/05/2020 07:59:59.998 AM'   and  tblTransactions.Dealer_ID = tblDealer.Dealer_ID and" & _
" tblTransactions.Good_ID = tblGood.Good_ID) as*/0 TotalweightTrans," & _
"(select count(Trans_ID) as CarInSide from  tblTransactions where Out_Date is null and tblTransactions.Dealer_ID = tblDealer.Dealer_ID and tblTransactions.Good_ID = tblGood.Good_ID)  as CarInSide" & _
" from tblDailyQuota " & _
"Left join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID " & _
"left join tblContracts on tblContracts.ContractID = tblDebitNote.ContractID " & _
"left join tblContractDet on (tblContracts.ContractID=tblContractDet.ContractID)" & _
"left join tblGood on tblGood.Good_ID = tblContractDet.GoodID " & _
"left join tblDealer on tblDealer.Dealer_ID = tblContracts.CustomerID " & _
"Left Join " & _
"( " & _
"select tblDebitNote.ContractID,/*isnull(sum(tblTransQuota.Quantity),0)*/0 TQTY " & _
"from tblDailyQuota " & _
"left join  tblTransQuota on tblTransQuota .QuotaID =tblDailyQuota .QuotaID /*and  DateQuota =convert(Date,'" & dtpReportQuota.Value.Date & "') and tblDailyQuota.Finished=0*/ " & _
"left join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID  left join  tblTransactions T on (tblTransQuota.TransID=T.Trans_ID) " & _
"where  CONVERT(Datetime,T.Out_Date,103)<'" & gMethods.GetShifName_And_SearchDateFT(True, dtpReportQuota.Value.Date, dtpReportQuota.Value.Date)(0) & "'" & _
"Group By tblDebitNote.ContractID " & _
")CQTY on (tblDebitNote.ContractID=CQTY.ContractID) " & _
"Left Join " & _
"( " & _
"select tblDebitNote.ContractID,isnull(sum(tblDailyQuota.Quota)*1000,0)-isnull(sum(tblDailyQuota.OutGoingQty*1000),0)-isnull(PQ.PrevQ,0) DQ " & _
" from tblDailyQuota " & _
"left join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID and ((DateQuota <=convert(Date,'" & dtpReportQuota.Value.Date & "') and tblDailyQuota.Finished=0) OR (EndDate=convert(Date,'" & dtpReportQuota.Value.Date & "') and tblDailyQuota.Finished=1)) " & _
"Left Join " & _
"( " & _
"Select NN.ContractID,sum(TQ.Quantity) PrevQ " & _
"From   tblTransactions T " & _
"Left join tblTransQuota TQ on(T.Trans_ID=TQ.TransID) " & _
"Left Join tblDailyQuota DD on(TQ.QuotaID=DD.QuotaID) " & _
"Left Join tblDebitNote NN on(DD.DebitNoteID=NN.DebitNoteID) " & _
"where  Convert(Datetime,T.Out_Date,103)< '" & gMethods.GetShifName_And_SearchDateFT(True, dtpReportQuota.Value.Date, dtpReportQuota.Value.Date)(0) & "' " & _
"and ((DateQuota <=convert(Date,'" & dtpReportQuota.Value.Date & "') and DD.Finished=0) OR (EndDate=convert(Date,'" & dtpReportQuota.Value.Date & "') and DD.Finished=1)) " & _
"Group By NN.ContractID " & _
")PQ on(tblDebitNote.ContractID=PQ.ContractID) " & _
"where ((DateQuota <=convert(Date,'" & dtpReportQuota.Value.Date & "') and tblDailyQuota.Finished=0) OR (EndDate=convert(Date,'" & dtpReportQuota.Value.Date & "') and tblDailyQuota.Finished=1)) " & _
"Group By tblDebitNote.ContractID,PQ.PrevQ " & _
")DQTY " & _
"on (tblDebitNote.ContractID=DQTY.ContractID) " & _
"where ((DateQuota <=convert(Date,'" & dtpReportQuota.Value.Date & "') and tblDailyQuota.Finished=0) OR (EndDate=convert(Date,'" & dtpReportQuota.Value.Date & "') and tblDailyQuota.Finished=1)) " & _
"group by tblDealer.Dealer_ID,tblDealer.Dealer_Name,tblGood.Good_Name,tblGood.Good_ID,tblDebitNote.ContractID,CQTY.TQTY,DQty.DQ,tblContracts.ContractID " & _
"Union All " & _
"Select D.DailyQuotaID ContractID,isnull(DQWC.TQTY,0) Quota,DA.Dealer_Name,G.Good_Name,isnull(trans.TransQ,0) as TotalweightTrans,0, " & _
"(select count(Trans_ID) as CarInSide from  tblTransactions where Out_Date is null  and tblTransactions.Dealer_ID = DA.Dealer_ID and tblTransactions.Good_ID = G.Good_ID)  as CarInSide " & _
"from tblDailyQuotaWithOutContract D " & _
"Left Join " & _
"( " & _
"Select DD.DailyQuotaID,(Sum(DD.Quantity)*1000)-isnull(PQ.PrevQ,0) TQTY " & _
"From   tblDailyQuotaWithOutContract DD " & _
"Left Join " & _
"( " & _
"Select TQ.QuotaID,sum(TQ.Quantity) PrevQ " & _
"From   tblTransactions T " & _
"Left join tblTransQuotaWithOutContract TQ on(T.Trans_ID=TQ.TransID) " & _
"where  Convert(Date,T.Out_Date)< '" & gMethods.GetShifName_And_SearchDateFT(True, dtpReportQuota.Value.Date, dtpReportQuota.Value.Date)(0) & "'" & _
"Group By TQ.QuotaID " & _
")PQ on(DD.DailyQuotaID=PQ.QuotaID) " & _
"Group BY DD.DailyQuotaID,PQ.PrevQ " & _
")DQWC on(D.DailyQuotaID=DQWC.DailyQuotaID) " & _
"left join tblDealer DA on DA.Dealer_ID = D.CustomerID " & _
"left join tblDailyQuotaWOContractGoods DG on(D.DailyQuotaID=DG.DailyQuotaID) " & _
"left join tblGood G on(DG.GoodID=G.Good_ID) " & _
"Left Join " & _
"( " & _
"Select  TT.QuotaID,/*sum(TT.Quantity)*/0 TransQ " & _
"From tblTransQuotaWithOutContract TT " & _
"Left join tblTransactions T on(TT.TransID=T.Trans_ID) " & _
"where Convert(Datetime,Out_Date,103)<'" & gMethods.GetShifName_And_SearchDateFT(True, dtpReportQuota.Value.Date, dtpReportQuota.Value.Date)(0) & "' /*Convert(Date,Out_Date)=convert(Date,'" & dtpReportQuota.Value.Date & "')*/" & _
"Group By TT.QuotaID " & _
")Trans on(D.DailyQuotaID=Trans.QuotaID) " & _
"/*left join tblTransQuotaWithOutContract T on(D.DailyQuotaID=T.QuotaID)*/ where  ((D.QuotaDate<=convert(Date,'" & dtpReportQuota.Value.Date & "') and D.finish=0) or (D.EndDate=convert(Date,'" & dtpReportQuota.Value.Date & "') and D.finish=1)) " & _
"Group by D.DailyQuotaID,DA.Dealer_ID,DA.Dealer_Name,G.Good_ID,G.Good_Name,DQWC.TQTY,trans.TransQ " & _
"Order By Dealer_Name "


            'SQLStatement = " set dateformat dmy select tblDebitNote.ContractID,DQty.DQ as Quota,Dealer_Name ,Good_Name,  isnull(CQTY.TQTY,0) Totalweight," & _
            '               " (select isnull(sum(Second_Weight-First_Weigth) ,0) as TotalweightTrans from tblTransactions " & WhereCondition & _
            '               "  and  tblTransactions.Dealer_ID = tblDealer.Dealer_ID and  tblTransactions.Good_ID = tblGood.Good_ID) as TotalweightTrans, " & _
            '               " (select count(Trans_ID) as CarInSide from  tblTransactions where Out_Date is null and tblTransactions.Dealer_ID = tblDealer.Dealer_ID and tblTransactions.Good_ID = tblGood.Good_ID)  as CarInSide " & _
            '               " from tblDailyQuota Left join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID " & _
            '               " left join tblContracts on tblContracts.ContractID = tblDebitNote.ContractID left join tblContractDet on (tblContracts.ContractID=tblContractDet.ContractID) " & _
            '               " left join tblGood on tblGood.Good_ID = tblContractDet.GoodID left join tblDealer on tblDealer.Dealer_ID = tblContracts.CustomerID " & _
            '               " left join(select tblDebitNote.ContractID,isnull(sum(tblTransQuota.Quantity),0)+isnull(sum(tblDailyQuota.OutGoingQty),0) TQTY  from tblDailyQuota " & _
            '               " left join  tblTransQuota on tblTransQuota .QuotaID =tblDailyQuota .QuotaID and  DateQuota <=convert(Date,'" & dtpReportQuota.Value.Date & "') and tblDailyQuota.Finished=" & If(ChkFinishedQouta.Checked, "1", "0") & " left join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID " & _
            '               " where DateQuota <=convert(Date,'" & dtpReportQuota.Value.Date & "') Group By tblDebitNote.ContractID )CQTY on (tblDebitNote.ContractID=CQTY.ContractID) " & _
            '               " left join(select tblDebitNote.ContractID,isnull(sum(tblDailyQuota.Quota)*1000,0) DQ  from tblDailyQuota " & _
            '               " left join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID and DateQuota <=convert(Date,'" & dtpReportQuota.Value.Date & "') and tblDailyQuota.Finished=" & If(ChkFinishedQouta.Checked, "1", "0") & _
            '               " where DateQuota <=convert(Date,'" & dtpReportQuota.Value.Date & "') Group By tblDebitNote.ContractID )DQTY on (tblDebitNote.ContractID=DQTY.ContractID) " & _
            '               " where tblDailyQuota.Finished=" & If(ChkFinishedQouta.Checked, "1", "0") & " and DateQuota <=  CONVERT(date,'" & dtpReportQuota.Value.Date & "') " & _
            '               " group by tblDealer.Dealer_ID,tblDealer.Dealer_Name,tblGood.Good_Name,tblGood.Good_ID,tblDebitNote.ContractID,CQTY.TQTY,DQty.DQ,tblContracts.ContractID  " & _
            '               " Union All  Select D.DailyQuotaID ContractID,isnull(DQWC.TQTY,0) Quota,DA.Dealer_Name,G.Good_Name,isnull(trans.TransQ,0) as TotalweightTrans,0, " & _
            '               " (select count(Trans_ID) as CarInSide from  tblTransactions where Out_Date is null  and tblTransactions.Dealer_ID = DA.Dealer_ID and tblTransactions.Good_ID = G.Good_ID)  as CarInSide " & _
            '               " from tblDailyQuotaWithOutContract D Left Join ( Select DD.DailyQuotaID,Sum(DD.Quantity)*1000 TQTY From   tblDailyQuotaWithOutContract DD Group BY DD.DailyQuotaID)DQWC on(D.DailyQuotaID=DQWC.DailyQuotaID) " & _
            '               " left join tblDealer DA on DA.Dealer_ID = D.CustomerID left join tblDailyQuotaWOContractGoods DG on(D.DailyQuotaID=DG.DailyQuotaID) left join tblGood G on(DG.GoodID=G.Good_ID) " & _
            '               " left join(Select  TT.QuotaID,sum(TT.Quantity) TransQ From tblTransQuotaWithOutContract TT Group By TT.QuotaID)Trans on(D.DailyQuotaID=Trans.QuotaID) " & _
            '               " left join tblTransQuotaWithOutContract T on(D.DailyQuotaID=T.QuotaID) where  D.QuotaDate<=convert(Date,'" & dtpReportQuota.Value.Date & "') and D.finish=" & If(ChkFinishedQouta.Checked, "1", "0") & _
            '               " Group by D.DailyQuotaID,DA.Dealer_ID,DA.Dealer_Name,G.Good_ID,G.Good_Name,DQWC.TQTY,trans.TransQ Order By Dealer_Name "

            'SQLStatement = " set dateformat dmy select tblDebitNote.ContractID,sum ((Quota))*1000 as Quota,Dealer_Name ,Good_Name, " & _
            '               " isnull(CQTY.TQTY,0) Totalweight," & _
            '               " (select isnull(sum(Second_Weight-First_Weigth) ,0) as TotalweightTrans from tblTransactions " & WhereCondition & _
            '               "  and  tblTransactions.Dealer_ID = tblDealer.Dealer_ID and  tblTransactions.Good_ID = tblGood.Good_ID) as TotalweightTrans, " & _
            '               " (select count(Trans_ID) as CarInSide from  tblTransactions where Out_Date is null " & (If(CboLocation.SelectedValue <> Nothing And ChkLocation.Checked, " and tblTransactions.LocationID=" & CboLocation.SelectedValue, "")) & " and tblTransactions.Dealer_ID = tblDealer.Dealer_ID and tblTransactions.Good_ID = tblGood.Good_ID) " & _
            '               " as CarInSide from tblDailyQuota " & _
            '               " Left join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID  " & _
            '               " left join tblContracts on tblContracts.ContractID = tblDebitNote.ContractID   " & _
            '               " left join tblContractDet on (tblContracts.ContractID=tblContractDet.ContractID) " & _
            '               " left join tblGood on tblGood.Good_ID = tblContractDet.GoodID  " & _
            '               " left join tblDealer on tblDealer.Dealer_ID = tblContracts.CustomerID  " & _
            '               " left join(select tblDebitNote.ContractID,isnull(sum(tblTransQuota.Quantity),0) TQTY from tblTransQuota join tblDailyQuota on tblTransQuota .QuotaID =tblDailyQuota .QuotaID and  DateQuota <=convert(Date,'" & dtpReportQuota.Value.Date & "') and tblDailyQuota.Finished=" & If(ChkFinishedQouta.Checked, "1", "0") & " join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID where DateQuota <=convert(Date,'" & dtpReportQuota.Value.Date & "') Group By tblDebitNote.ContractID )CQTY on (tblDebitNote.ContractID=CQTY.ContractID) " & _
            '               " where tblDailyQuota.Finished=" & If(ChkFinishedQouta.Checked, "1", "0") & " and DateQuota <=  CONVERT(date,'" & dtpReportQuota.Value.Date & "') " & _
            '               " group by tblDealer.Dealer_ID,tblDealer.Dealer_Name,tblGood.Good_Name,tblGood.Good_ID,tblDebitNote.ContractID,CQTY.TQTY,tblContracts.ContractID " & _
            '               " Union All Select 0 ContractID,isnull(Sum(D.Quantity),0)*1000 Quota,DA.Dealer_Name,G.Good_Name,isnull(sum(T.Quantity),0) as TotalweightTrans,0, " & _
            '               " (select count(Trans_ID) as CarInSide from  tblTransactions where Out_Date is null  and tblTransactions.Dealer_ID = DA.Dealer_ID and tblTransactions.Good_ID = G.Good_ID)  as CarInSide " & _
            '               " from   tblDailyQuotaWithOutContract D left join   tblDealer DA on DA.Dealer_ID = D.CustomerID " & _
            '               " left join tblDailyQuotaWOContractGoods DG on(D.DailyQuotaID=DG.DailyQuotaID) left join tblGood G on(DG.GoodID=G.Good_ID) left join tblTransQuotaWithOutContract T on(D.DailyQuotaID=T.QuotaID) " & _
            '               " where  D.QuotaDate<=convert(Date,'" & dtpReportQuota.Value.Date & "') and D.finish=" & If(ChkFinishedQouta.Checked, "1", "0") & " Group by DA.Dealer_ID,DA.Dealer_Name,G.Good_ID,G.Good_Name Order By Dealer_Name "


            Dim Adp As New SqlDataAdapter(SQLStatement, Con)
            Dim DT As New DataTable
            Adp.Fill(DT)

            Dim rpt As New rptTransWithQuota
            rpt.SetDataSource(DT)
            rpt.SetParameterValue("DateQuota", dtpReportQuota.Value.Date)
            rpt.SetParameterValue("LocationName", If(CboLocation.SelectedValue <> Nothing And ChkLocation.Checked, CboLocation.Text, "الكل"))
            grptpth.ReportPath(rpt, crvQuota)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmrptQuota_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        gAdo.CtrlItemsLoad("LocationID", "LocationName", "tblLocation ORDER BY LocationID", CboLocation, "", "")
        setDate()
    End Sub

    Sub setDate()
        Dim ShiftStartHour As Integer
        ShiftStartHour = gAdo.CmdExecScalar("Set DateFormat dmy Select Datepart(Hour,convert(Datetime,S.Shift_Start_Time,103)) From tblShift S where Shift_IsFirst=1")
        Dim CurDate As Date
        CurDate = Date.Now.Date
        If (Now.Hour >= 0 And Now.Hour < ShiftStartHour) Then
            CurDate = Date.Now.AddDays(-1)
        End If
        dtpReportQuota.Value = CurDate

    End Sub

    Private Sub ChkLocation_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkLocation.CheckedChanged
        If (ChkLocation.Checked) Then
            CboLocation.Visible = True
        Else : CboLocation.Visible = False
        End If
    End Sub
End Class