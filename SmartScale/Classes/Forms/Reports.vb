Public Class Reports

    Private Sub btnCarsInside_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCarsInside.Click

        frmrptCarsInside.ShowDialog()

    End Sub

    Private Sub btnAllReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllReports.Click

        frmAllReports.ShowDialog()

    End Sub

    Private Sub btnPreReports_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPreReports.Click

        cfrmReports.ShowDialog()

    End Sub

  
End Class