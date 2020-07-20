Imports System.Data.SqlClient

Public Class frmrptCarsInside

    Private Sub frmrptCarsInside_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            Dim ConnPath, ConnString As String
            ConnPath = Application.StartupPath & "\ConnString.txt"
            ConnString = GetConString(ConnPath)
            Dim Con As New SqlConnection(ConnString)

            Dim Where As String = "and convert (datetime,T.IN_Date) between '" & Now.Date.AddDays(-5) & " 08:00:00 AM' and '" & Now.Date.AddDays(5) & " 07:59:59 AM' "
            Dim SQLStatement As String
            SQLStatement = " set dateformat dmy SELECT T.CID,D.Dealer_Name,G.Good_Name,IT.Issue_To_Name,C.CarBoard_No,CI.TruckBoard_No,T.Dealer_ID,T.Issue_To_ID " _
                                         & ",T.First_Weigth,T.Second_Weight,Out_Date " _
                                         & "FROM tblTransactions as T join tblGood as G on T.Good_ID =G.Good_ID  " _
                                         & "join tblCarInfo as CI on CI.Car_Info_ID=T.Car_Info_ID  " _
                                         & "join tblCar as C on C.Car_ID =T.Car_ID " _
                                         & "join tblDealer as D on D.Dealer_ID=T.Dealer_ID " _
                                         & "join tblIssueTo as IT on IT.Issue_To_ID=T.Issue_To_ID where  TR_TY =2 and out_date is null " & Where

            Dim Adp As New SqlDataAdapter(SQLStatement, Con)
            Dim DT As New DataTable
            Adp.Fill(DT)
            Dim Rpt As New RptTransaction
            Rpt.SetDataSource(DT)
            Rpt.SetParameterValue("Date", Now.Date)
            Rpt.SetParameterValue("DateFrom", Now.Date)
            Rpt.SetParameterValue("DateTo", Now.Date)
            Rpt.SetParameterValue("IssueTo", "الكل")
            Rpt.SetParameterValue("Scale", "الكل")
            Rpt.SetParameterValue("Shift", "الكل")
            Rpt.SetParameterValue("User", "الكل")
            Rpt.SetParameterValue("Customers", "الكل")

            crvCarsInside.ReportSource = Rpt
        Catch ex As Exception

        End Try

    End Sub

End Class