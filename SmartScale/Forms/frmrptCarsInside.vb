Imports System.Data.SqlClient

Public Class frmrptCarsInside

    Private Sub frmrptCarsInside_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", " TblDealer where Type=1 ORDER BY Dealer_Name ", cboDealer, "", "", False, True, " الكــــــــل")
        gAdo.CtrlItemsLoad("LocationID", "LocationName", "tblLocation ORDER BY LocationID", cboLocation, "", "")
        crvCarsInside.ReportSource = Nothing
        chklstBoxScale.DataSource = GObjDataBaseS.FillDT("select * from tblScale where Scale_IsDeleted = 0")
        chklstBoxScale.DisplayMember = "Scale_Name"
        chklstBoxScale.ValueMember = "Scale_ID"
        Dim dtCheckedScale As New DataTable

        If cfrmReports.ScaleNo = 0 Then
        Else
            Dim Locatin As Integer = gAdo.CmdExecScalar("select Location from tblScale where Scale_IsDeleted = 0 and Scale_ID = " & cfrmReports.ScaleNo)
            dtCheckedScale = GObjDataBaseS.FillDT("select Scale_ID from tblScale where Scale_IsDeleted = 0 and Location = " & Locatin)
            For i = 0 To chklstBoxScale.Items.Count - 1

                If dtCheckedScale.Select("scale_ID=" & chklstBoxScale.Items(i)(0).ToString).Length > 0 Then
                    chklstBoxScale.SetItemCheckState(i, CheckState.Checked)
                End If

            Next
        End If
        'gUserO.ID = gUserID
        'gUserO.Refresh()
        'If gUserO.UserType = "n" Then
        '    chklstBoxScale.Enabled = False
        'End If

    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        Try

            Dim ConnPath, ConnString As String
            ConnPath = Application.StartupPath & "\ConnString.txt"
            ConnString = GetConString(ConnPath)
            Dim Con As New SqlConnection(ConnString)

            'Dim Where As String = ""
            'Where = "and convert (datetime,T.IN_Date) between '" & Now.Date.AddDays(-5) & " 08:00:00 AM' and '" & Now.Date.AddDays(5) & " 07:59:59 AM' "

            Dim SelectedScales As String = ""
            'Dim ItemChecked As Object
            For Each ItemChecked In chklstBoxScale.CheckedItems
                If SelectedScales = "" Then
                    SelectedScales = ItemChecked.item("Scale_ID").ToString
                Else
                    SelectedScales = SelectedScales & " , " & ItemChecked.item("Scale_ID").ToString
                End If
            Next

            Dim SQLStatement1, SQLStatement2, SQLStatement As String
            SQLStatement1 = " set dateformat dmy SELECT convert(nvarchar(max),CID) as cid,Trans_ID,D.Dealer_Name,G.Good_Name,IT.Issue_To_Name,C.CarBoard_No,CI.TruckBoard_No,T.Dealer_ID,T.Issue_To_ID " _
                                         & ",T.First_Weigth,T.Second_Weight,Out_Date " _
                                         & "FROM tblTransactions as T join tblGood as G on T.Good_ID =G.Good_ID  " _
                                         & "join tblCarInfo as CI on CI.Car_Info_ID=T.Car_Info_ID  " _
                                         & "join tblCar as C on C.Car_ID =T.Car_ID " _
                                         & "join tblDealer as D on D.Dealer_ID=T.Dealer_ID " _
                                         & "join tblIssueTo as IT on IT.Issue_To_ID=T.Issue_To_ID where  TR_TY =2 and out_date is null /*and  T.Issue_To_id not in(17,21)*/ " & (If(cboLocation.SelectedValue <> Nothing And ChkLocation.Checked, " and T.LocationID=" & cboLocation.SelectedValue, "")) & " and First_Weigth_Scale_ID in(" & SelectedScales & ")"

            SQLStatement2 = " SELECT 'إذن مرتجع رقم : ' + convert(nvarchar(max),Trans_ID) as CID,Trans_ID,D.Dealer_Name,G.Good_Name,IT.Issue_To_Name,C.CarBoard_No,CI.TruckBoard_No,T.Dealer_ID,T.Issue_To_ID " _
                                         & ",T.First_Weigth,T.Second_Weight,Out_Date " _
                                         & "FROM tblReturns as T join tblGood as G on T.Good_ID =G.Good_ID  " _
                                         & "join tblCarInfo as CI on CI.Car_Info_ID=T.Car_Info_ID  " _
                                         & "join tblCar as C on C.Car_ID =T.Car_ID " _
                                         & "join tblDealer as D on D.Dealer_ID=T.Dealer_ID " _
                                         & "join tblIssueTo as IT on IT.Issue_To_ID=T.Issue_To_ID where TR_TY =2 and out_date is null /*and  T.Issue_To_id not in(17,21)*/ " & (If(cboLocation.SelectedValue <> Nothing And ChkLocation.Checked, " and T.LocationID=" & cboLocation.SelectedValue, "")) & " and First_Weigth_Scale_ID in(" & SelectedScales & ")"


            If cboDealer.SelectedIndex = 0 Then

                SQLStatement2 = SQLStatement2 & "ORDER BY Trans_ID"
            Else
                SQLStatement1 = SQLStatement1 & " and T.Dealer_ID='" & cboDealer.SelectedValue & "'"
                SQLStatement2 = SQLStatement2 & " and T.Dealer_ID='" & cboDealer.SelectedValue & "' ORDER BY Trans_ID"
            End If



            SQLStatement = SQLStatement1 & " Union " & SQLStatement2
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
            Rpt.SetParameterValue("Customers", cboDealer.Text)
            Rpt.SetParameterValue("LocationName", If(cboLocation.SelectedValue <> Nothing And ChkLocation.Checked, cboLocation.Text, "الكل"))

            crvCarsInside.ReportSource = Rpt
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ChkLocation_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkLocation.CheckedChanged
        If (ChkLocation.Checked) Then
            cboLocation.Visible = True
        Else : cboLocation.Visible = False
        End If
    End Sub
End Class