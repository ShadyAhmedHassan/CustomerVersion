Imports System.Data.SqlClient
Imports System.IO

Public Class cfrmCollectedMotions

    Dim mstrStartTime, mstrEndTime As String
    Dim mstrStartDate, mstrEndDate As String
    Dim WhereCondition As String
    Dim TimeTo, TimeFrom As String
    Public Shared ScaleNo As Integer = 1 ' here write the number of scale id
    Dim Scale As String = ""

    Private Sub cfrmReports_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        gfrmChoose.Show()
    End Sub

    Private Sub cfrmReports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
       

    End Sub

  

   

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        Dim Fromdate As DateTime
        Fromdate = dtpFrom.Value.Date.ToString
        Dim Todate As DateTime
        Todate = dtpTO.Value.Date.ToString

        Dim ConnPath, ConnString As String
        ConnPath = Application.StartupPath & "\ConnString.txt"
        ConnString = GetConString(ConnPath)
        'ConnString = Server
        Dim Con As New SqlConnection(ConnString)

        Dim Statment As String = "set dateformat dmy " & _
"select dbo.SetOut_Date(tblTransactions.Out_Date) [Date],tblDealer .Dealer_Short_Name Customer, " & _
"dbo.SetOut_Date(tblTransactions.Out_Date)  + '\' + DebitNote  + '\' + Dealer_Short_Name  + '\' + tblProtype.ProtypeName  + '\'   +  tblGoodtype.GoodtypeName   ConcATENATEno , tblProtype .ProtypeName,tblGoodtype .GoodtypeName, sum(tblTransQuota.Quantity)/1000 [Quantity,MT], " & _
"tbldebitnote.DebitNote [Debit Note number],tblContracts.ContractNO [contract No.] " & _
"from tblTransactions " & _
"join tblTransQuota  on tblTransactions .Trans_ID =tblTransQuota .TransID " & _
"join tblDailyQuota on tblTransQuota .QuotaID =tblDailyQuota .QuotaID " & _
"join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID " & _
"join tblContracts on tblDebitNote .ContractID =tblContracts .ContractID " & _
"join tblDealer on tblTransactions .Dealer_ID =tblDealer .Dealer_ID " & _
"join tblGood on tblTransactions .Good_ID =tblGood .Good_ID  " & _
"join tblProtype on tblGood .ProtypeID  = tblProtype.ProtypeID  " & _
"join tblGoodtype on tblGood.GoodtypeID  = tblGoodtype.GoodtypeID " & _
"where TR_TY =2 and  convert(Datetime,tblTransactions.Out_Date,103) between '" & gMethods.GetShifName_And_SearchDateFT(True, Fromdate, Todate)(0) & "' and  '" & gMethods.GetShifName_And_SearchDateFT(True, Fromdate, Todate)(1) & "' " & _
"and  out_date is not null " & _
"group by dbo.SetOut_Date(tblTransactions.Out_Date),tblDealer .Dealer_Short_Name,tblProtype .ProtypeName,tblGoodtype .GoodtypeName,tbldebitnote.DebitNote  ,tblContracts.ContractNO "

        'Dim Statement As String = " set dateformat dmy select distinct  dbo.SetOut_Date(tblTransactions.Out_Date) [Date],Dealer_Short_Name Customer, dbo.SetOut_Date(tblTransactions.Out_Date)  + '\' + DebitNote  + '\' + Dealer_Short_Name  + '\' + tblProtype.ProtypeName  + '\'   +  tblGoodtype.GoodtypeName   ConcATENATEno, " & _
        '                          " tblProtype.ProtypeName, tblGoodtype.GoodtypeName, " & _
        '                          " TQTY/1000 [Quantity,MT]  ,DebitNote [Debit Note number]  , ContractNO [contract No.] " & _
        '                          " from tblContracts join tblDebitNote on tblContracts .ContractID = tblDebitNote .ContractID " & _
        '                          " join tblDailyQuota on tblDebitNote .DebitNoteID = tblDailyQuota .DebitNoteID  " & _
        '                          " join tblTransQuota  on tblDailyQuota .QuotaID = tblTransQuota .QuotaID " & _
        '                          " join tblTransactions on tblTransQuota .TransID =tblTransactions .Trans_ID " & _
        '                          " join tblDealer on tblTransactions .Dealer_ID = tblDealer .Dealer_ID " & _
        '                          " join tblGood on tblTransactions .Good_ID =tblGood .Good_ID " & _
        '                          " join tblProtype on tblGood .ProtypeID  = tblProtype.ProtypeID " & _
        '                          " join tblGoodtype on tblGood.GoodtypeID  = tblGoodtype.GoodtypeID " & _
        '                          " Join(Select C.ContractID,DN .DebitNoteID,G .ProtypeID,G.GoodtypeID,/*CONVERT(date,T.Out_Date,103) ODate,*/sum (Second_Weight - First_Weigth) TQTY " & _
        '                          " From tblContracts C join tblDebitNote DN on C .ContractID = DN .ContractID " & _
        '                          " join tblDailyQuota DQ on DN .DebitNoteID = DQ.DebitNoteID join tblTransQuota TQ  on DQ .QuotaID = TQ.QuotaID " & _
        '                          " join tblTransactions T on TQ.TransID =T .Trans_ID join tblGood G on T .Good_ID =G .Good_ID " & _
        '                          " where TR_TY =2 and  convert(Datetime,T.Out_Date,103) between '" & gMethods.GetShifName_And_SearchDateFT(True, Fromdate, Todate)(0) & "' and  '" & gMethods.GetShifName_And_SearchDateFT(True, Fromdate, Todate)(1) & "' " & _
        '                          " Group By C.ContractID,DN.DebitNoteID,G .ProtypeID,G.GoodtypeID/*,CONVERT(date,T.Out_Date,103)*/ " & _
        '                          " )QQ on (tblContracts .ContractID=QQ.ContractID and tblDebitNote .DebitNoteID=QQ.DebitNoteID and tblGood .ProtypeID=QQ.ProtypeID and tblGood.GoodtypeID=QQ.GoodtypeID /*and CONVERT(date,tblTransactions.Out_Date,103)=QQ.ODate*/) " & _
        '                          " where TR_TY =2 and  convert(Datetime,tblTransactions.Out_Date,103) between '" & gMethods.GetShifName_And_SearchDateFT(True, Fromdate, Todate)(0) & "' and  '" & gMethods.GetShifName_And_SearchDateFT(True, Fromdate, Todate)(1) & "' " & _
        '                          " group by dbo.SetOut_Date(tblTransactions.Out_Date), ContractNO,DebitNote,Dealer_Short_Name ,tblProtype.ProtypeName, tblGoodtype.GoodtypeName,TQTY "

        Dim Adp As New SqlDataAdapter(Statment, Con)
        Dim DT As New DataTable
        Adp.Fill(DT)
        Dim Rpt As New RptCollectedMotions

        Rpt.SetDataSource(DT)
        Rpt.SetParameterValue("DateFrom", dtpFrom.Value.Date)
        Rpt.SetParameterValue("DateTo", dtpTO.Value.Date)

        CrvTransactionRepot.ReportSource = Rpt

    End Sub

   

   

    Public Function GetConString(ByVal FullPath As String, Optional ByRef ErrInfo As String = "") As String

        Dim strContents As String
        Dim objReader As StreamReader
        Try

            objReader = New StreamReader(FullPath)
            strContents = objReader.ReadToEnd()
            objReader.Close()
            Return strContents
        Catch Ex As Exception
            ErrInfo = Ex.Message
        End Try

    End Function

    Private Sub BtnSearch_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.MouseEnter
        sender.BackColor = Color.SkyBlue
    End Sub

    Private Sub BtnSearch_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.MouseLeave
        sender.BackColor = Color.Gainsboro
    End Sub

 

   

    Private Sub timTO_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub timFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub Label12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub chkNextDay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

 
End Class