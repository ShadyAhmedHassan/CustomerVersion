Imports System.IO
Imports System.Data.SqlClient

Public Class ManualEditReport

    Private Sub ManualEditReport_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        gfrmChoose.Show()
    End Sub

    Private Sub ManualEditReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
        gAdo.CtrlItemsLoad("LocationID", "LocationName", "tblLocation ORDER BY LocationID", cboLocation, "", "")
        gUserO.ID = gUserID
        gUserO.Refresh()
        If gUserO.UserType = "n" Then
            chklstBoxScale.Enabled = False
        End If
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        Try
            Dim WhereCondition As String
            WhereCondition = "where TR_TY=2 and convert (datetime,T.In_Shift_Date) between '" & dtpAllFrom.Value.Date & " 08:00:00 AM' and '" & dtpAllTo.Value.Date.AddDays(1) & " 07:59:59 AM' and ManualEdit=1"
            Dim SelectedScales As String = ""
            Dim ItemChecked As Object
            For Each ItemChecked In chklstBoxScale.CheckedItems
                If SelectedScales = "" Then
                    SelectedScales = ItemChecked.item("Scale_ID").ToString
                Else
                    SelectedScales = SelectedScales & " , " & ItemChecked.item("Scale_ID").ToString
                End If
            Next
            WhereCondition = WhereCondition & " and Second_Weight_Scale_ID in(" & SelectedScales & ") " & (If(cboLocation.SelectedValue <> Nothing And ChkLocation.Checked, " and T.LocationID=" & cboLocation.SelectedValue, ""))

            Dim ConnPath, ConnString As String
            ConnPath = Application.StartupPath & "\ConnString.txt"
            ConnString = GetConString(ConnPath)
            Dim Con As New SqlConnection(ConnString)

            Dim Adp As New SqlDataAdapter(" set dateformat dmy SELECT T.CID,D.Dealer_Name,G.Good_Name,IT.Issue_To_Name,C.CarBoard_No,CI.TruckBoard_No,T.Dealer_ID,T.Issue_To_ID " _
                                        & ",T.First_Weigth,T.Second_Weight,Out_Date " _
                                        & "FROM tblTransactions as T join tblGood as G on T.Good_ID =G.Good_ID  " _
                                        & "join tblCarInfo as CI on CI.Car_Info_ID=T.Car_Info_ID  " _
                                        & "join tblCar as C on C.Car_ID =T.Car_ID " _
                                        & "join tblDealer as D on D.Dealer_ID=T.Dealer_ID " _
                                        & "join tblIssueTo as IT on IT.Issue_To_ID=T.Issue_To_ID  " & WhereCondition, Con)

            Dim strScaleName As String = ""
            Dim count = 0
            For i = 0 To chklstBoxScale.Items.Count - 1
                If (chklstBoxScale.GetItemChecked(i)) Then
                    If count = 0 Then
                        strScaleName = chklstBoxScale.Items(i)(1).ToString
                    Else
                        strScaleName = strScaleName & " , " & chklstBoxScale.Items(i)(1).ToString

                    End If
                    
                    count += 1
                End If
            Next

            Dim DT As New DataTable
            Adp.Fill(DT)
            Dim Rpt As New RptTransaction
            Rpt.SetDataSource(DT)
            Rpt.SetParameterValue("Date", Now.Date)
            Rpt.SetParameterValue("DateFrom", dtpAllFrom.Value.Date)
            Rpt.SetParameterValue("DateTo", dtpAllTo.Value.Date)

            Rpt.SetParameterValue("IssueTo", "الكل")
            Rpt.SetParameterValue("Scale", strScaleName)
            Rpt.SetParameterValue("Shift", "الكل")
            Rpt.SetParameterValue("User", "الكل")
            Rpt.SetParameterValue("Customers", "الكل")
            Rpt.SetParameterValue("LocationName", If(cboLocation.SelectedValue <> Nothing And ChkLocation.Checked, cboLocation.Text, "الكل"))
            crvManEditReports.ReportSource = Rpt
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