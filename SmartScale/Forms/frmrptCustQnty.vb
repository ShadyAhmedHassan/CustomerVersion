Public Class frmrptCustQnty

    Dim rpt As New rptCustQnty

    Private Sub frmrptCustQnty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood", cboGoods, "", " good_id <> 1 ORDER BY Good_Name")
            cboGoods.SelectedIndex = 1
            gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", "tblDealer", cboDealers, "", " Dealer_ID <> 1 and Type =1 ORDER BY Dealer_Name")
            cboDealers.SelectedIndex = 1
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click

        Try

            Dim DateFrom, DateTO As String
            DateFrom = dtFrom.Value
            DateTO = dtTo.Value

            If DateFrom.Contains("ص") Then
                DateFrom = DateFrom.Replace("ص", "AM")
            Else
                DateFrom = DateFrom.Replace("م", "PM")
            End If
            If DateTO.Contains("ص") Then
                DateTO = DateTO.Replace("ص", "AM")
            Else
                DateTO = DateTO.Replace("م", "PM")
            End If
            'If chkDealer.Checked And chkGood.Checked Then
            '    rpt.SetParameterValue("@From", DateFrom)
            '    rpt.SetParameterValue("@TO", DateTO)
            '    rpt.SetParameterValue("@CustID", cboDealers.SelectedValue)
            '    rpt.SetParameterValue("@GoodID", cboGoods.SelectedValue)
            '    'ElseIf chkGood.Checked Then
            '    '    rpt.SetParameterValue("@From", DateFrom)
            '    '    rpt.SetParameterValue("@TO", DateTO)
            '    '    rpt.SetParameterValue("@CustID", 0)
            '    '    rpt.SetParameterValue("@GoodID", cboGoods.SelectedValue)
            'Else
            If chkDealer.Checked Then
                rpt.SetParameterValue("@From", DateFrom)
                rpt.SetParameterValue("@TO", DateTO)
                rpt.SetParameterValue("@CustID", cboDealers.SelectedValue)
                'rpt.SetParameterValue("@GoodID", 0)
            Else
                rpt.SetParameterValue("@From", DateFrom)
                rpt.SetParameterValue("@TO", DateTO)
                rpt.SetParameterValue("@CustID", 0)
                'rpt.SetParameterValue("@GoodID", 0)
            End If
            grptpth.ReportPath(rpt, crvRptCustQnty)
            'crvRptCustQnty.ReportSource = rpt
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkGood_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGood.CheckedChanged
        If chkGood.Checked Then
            cboGoods.Enabled = True
        Else
            cboGoods.Enabled = False
        End If
    End Sub

    Private Sub chkDealer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDealer.CheckedChanged
        If chkDealer.Checked Then
            cboDealers.Enabled = True
        Else
            cboDealers.Enabled = False
        End If
    End Sub

    Private Sub frmrptCustQnty_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        gfrmChoose.Show()
    End Sub
End Class