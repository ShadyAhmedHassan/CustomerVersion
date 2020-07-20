Public Class frmSearchTrans

    Public Shared SharedTxtTrans As New TextBox
    Public Shared SearchOrCancel As Integer = 0


    Private Sub frmSearchTrans_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SharedTxtTrans = txtTransNo
        txtTransNo.Text = ""
        txtTransNo.Focus()
        txtTransNo.Select()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try

            If txtTransNo.Text.Trim = "" Or txtTransNo.Text.Trim = 0 Then
                MsgBox("من فضلك ادخل رقم للبحث ")
            Else
                SearchOrCancel = 1       ' Active search
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        SearchOrCancel = 0       ' Deactive search
        Me.Close()

    End Sub
End Class