Public Class Reports
    Enum Control_Name
        btnCarsInside
        btnAllReports
        btnPreReports
        Button1
        btnReportQuota
        Button2
        Button3
    End Enum
    Public Shared x As Control_Name
    Private Sub btnCarsInside_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCarsInside.Click

        '''' Elsaman 24/4/2019 ''''
        'x = Control_Name.btnCarsInside
        'Dim frm As New frmrptCarInsideSearch
        'frm.ShowDialog()
        x = Control_Name.btnCarsInside
        Dim frm As New frmrptCarsInside
        frm.ShowDialog()
        Me.Close()
    End Sub

    Private Sub btnAllReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllReports.Click
        x = Control_Name.btnAllReports
        Me.Close()

    End Sub

    Private Sub btnPreReports_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPreReports.Click

        x = Control_Name.btnPreReports
        Me.Close()

    End Sub

  
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        x = Control_Name.Button1
        Me.Close()
    End Sub

    Private Sub Reports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnReportQuota_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReportQuota.Click
        '''' Elsaman 14/1/2020 ''''
        x = Control_Name.btnReportQuota
        Dim frm As New frmrptQuota
        frm.ShowDialog()
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        x = Control_Name.Button2
        Dim frm As New frmUpDatedTransactions
        frm.ShowDialog()
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        x = Control_Name.Button3
        Dim frm As New frmrptQuotaForSales
        frm.ShowDialog()
        Me.Close()
    End Sub
End Class