Public Class frmPassWord

    Public Shared CorrectPassword As Boolean

    Private Sub TextBox1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPassword.KeyDown

        If e.KeyCode = Keys.Enter Then
            Dim Password As String
            Password = gAdo.CmdExecScalar("select Password from tblCompany ")
            If Password = txtPassword.Text.Trim Then
                CorrectPassword = True
                Me.Close()
            Else
                CorrectPassword = False
                MsgBox("كلمه السر غير صحيحه")
                Me.Close()
            End If
        End If

    End Sub

    Private Sub frmPassWord_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        If CorrectPassword = True Then
        Else
            CorrectPassword = False
        End If

    End Sub

    Private Sub frmPassWord_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtPassword.Text = ""
    End Sub

End Class