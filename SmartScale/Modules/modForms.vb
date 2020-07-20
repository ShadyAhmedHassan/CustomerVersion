Option Explicit On
Module modForms
    Public Function DoLogin() As Boolean
        Dim frmLogin = New cfrmLogin
        On Error Resume Next
        frmLogin.Login = gobjCurrentLogin
        DoLogin = (frmLogin.ShowDialog() = DialogResult.OK)
        frmLogin = Nothing

    End Function
End Module
