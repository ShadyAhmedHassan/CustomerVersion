Imports System.Windows.Forms

Public Class clsCONTROLS

    Public Sub Clear(ByVal Frm As Form, ByVal CtrlType As String)
        For Each Ctrl As Control In Frm.Controls
            If Ctrl.GetType.ToString = "System.Windows.Forms" & CtrlType Then
                Ctrl.Text = ""
            End If
        Next
    End Sub
End Class
