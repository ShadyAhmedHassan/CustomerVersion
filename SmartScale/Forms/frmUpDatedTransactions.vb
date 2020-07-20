Imports System.Data.SqlClient
Public Class frmUpDatedTransactions
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

            WhereCondition = " where Out_Date is not null and TR_TY=2 and CAST(UT.In_Date as Date) Between '" & dtpFromDate.Value.ToShortDateString & "' and '" & dtpToDate.Value.ToShortDateString & "' and UT.Flag=0"

            Dim SQLStatement As String

            '" ,(select isnull(sum(Second_Weight-First_Weigth) ,0) as TotalweightTrans from tblTransactions " & WhereCondition & _
            '" and  tblTransactions.Dealer_ID = tblDealer.Dealer_ID and  tblTransactions.Good_ID = tblGood.Good_ID) as TotalweightTrans " & _

            SQLStatement = " set dateformat dmy Select UTM.UpDateTransID,UTM.UpDateDate,U.[User_Name],UT.CID,UT.Trans_ID,L.LocationName,C.CarBoard_No Car_ID,D.Dealer_Name,DR.Driver_Name,G.Good_Name,UT.First_Weigth,UT.Second_Weight,UT.Out_Date, " & _
                           " case when UT.MSGID=1 then 'إسكندرية' when UT.MSGID=2 then 'العامرية' else '' end MSGName,UT.Slip_Rate,UT.ManualEdit,CT.City_Name,CI.TruckBoard_No,CT2.City_Name TruckCityName,DR.Driver_License_No,I.Issue_To_Name,UT.Notes " & _
                           " From tblUpDateTransactionsMst UTM join tblUpdateTransactions UT on(UTM.UpDateTransID=UT.UpDateTransID) join tblUser U on(UTM.UserID=U.[User_ID]) join tblLocation L on(UT.LocationID=L.LocationID) join tblDealer D on(UT.Dealer_ID=D.Dealer_ID) " & _
                           " join tblDriver DR on (UT.Driver_ID=DR.Driver_ID) join tblIssueTo I on(UT.Issue_To_ID=I.Issue_To_ID) join tblGood G on(UT.Good_ID=G.Good_ID) join tblCar C on(C.Car_ID=UT.Car_ID) Join tblCity CT on(C.CarBoard_City_ID=CT.City_ID) " & _
                           " join tblCarInfo CI on(UT.Car_Info_ID=CI.Car_Info_ID) Join tblCity CT2 on(CI.TruckBoard_City_ID=CT2.City_ID)" & WhereCondition
                           

            Dim Adp As New SqlDataAdapter(SQLStatement, Con)
            Dim DT As New DataTable
            Adp.Fill(DT)

            Dim rpt As New UpDatedTransactions
            rpt.SetDataSource(DT)
            rpt.SetParameterValue("FromDate", dtpFromDate.Value.Date)
            rpt.SetParameterValue("ToDate", dtpToDate.Value.Date)
            grptpth.ReportPath(rpt, crvQuota)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmrptQuota_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        gAdo.CtrlItemsLoad("LocationID", "LocationName", "tblLocation ORDER BY LocationID", CboLocation, "", "")
    End Sub

    Private Sub ChkLocation_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkLocation.CheckedChanged
        If (ChkLocation.Checked) Then
            CboLocation.Visible = True
        Else : CboLocation.Visible = False
        End If
    End Sub
End Class