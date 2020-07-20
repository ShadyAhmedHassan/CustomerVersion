
Public Class frmCustQnty

    Private Sub frmCustQnty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", "TblDealer where Type=1 and dealer_ID>2 ORDER BY Dealer_Name", cboCustomer)
            gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood  ORDER BY Good_Name", cboGood)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try
            If txtQuantity.Text = "" Or Val(txtQuantity.Text) < 1 Then
                MsgBox("من فضلك ادخل كميه مناسبه ")
                Exit Sub
            End If
            If cboCustomer.SelectedIndex = -1 Then
                MsgBox("من فضلك اختار عميل ")
                Exit Sub
            End If
            If cboGood.SelectedIndex = -1 Then
                MsgBox("من فضلك اختار صنف ")
                Exit Sub
            End If
            Dim Time As String
            Time = Now
            If Time.Contains("ص") Then
                Time = Time.Replace("ص", "AM")
            Else
                Time = Time.Replace("م", "PM")
            End If
            gAdo.CmdExec("set dateformat dmy insert into tblCustQnty " _
            & " values('" & cboCustomer.SelectedValue & "'," _
            & " " & Val(txtQuantity.Text) * 1000 & ",'" & Time & "'," & gUserID & "," & ShiftID & "," _
            & " " & Val(txtOverQnty.Text) & ")")
            MsgBox("تم الحفــظ بنجاح")
            Me.Close()
        Catch ex As Exception
        End Try

    End Sub

End Class