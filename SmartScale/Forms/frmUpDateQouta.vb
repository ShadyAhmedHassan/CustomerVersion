Public Class frmUpDateQouta

    Private Sub frmUpDateQouta_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            SelectNextControl(ActiveControl, True, True, True, True)
        End If
    End Sub

    Private Sub frmUpDateQouta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpDayDate.Text = Date.Now.Date
        gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", "TblDealer where Type in (1,3) ORDER BY Dealer_Name", cboCustID)
    End Sub

    Private Sub btnSearchQuota_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchQuota.Click
        
        
    End Sub
End Class