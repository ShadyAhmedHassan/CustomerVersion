Imports System.ServiceProcess

Public Class cfrmChoose

    Private Sub BtncfrmReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtncfrmReport.Click

        gfrmReports = New cfrmReports
        gfrmChoose.Hide()
        gfrmReports.ShowDialog()

    End Sub

    Private Sub BtncfrmAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtncfrmAddNew.Click
        gfrmAddNew = New cfrmAddNew
        gfrmChoose.Hide()
        gfrmAddNew.ShowDialog()

    End Sub

    Private Sub BtncfrmMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtncfrmMain.Click

       
        gTransID = 0
        gfrmMain = New cfrmMain(0)
        gfrmChoose.Hide()
        gfrmMain.ShowDialog()


    End Sub

    Private Sub cfrmChoose_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Dim ctr As Control = Me.Controls("BtncfrmAddNew")
        ' CType(ctr, Button).Enabled = False

        If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=101 and User_ID= " & gUserID) = True Then
            BtncfrmReport.Enabled = True
            Button1.Enabled = True
            btnManEditRpt.Enabled = True
            BtnCollectedMotions.Enabled = True
        Else
            BtncfrmReport.Enabled = False
            Button1.Enabled = False
            btnManEditRpt.Enabled = False
            BtnCollectedMotions.Enabled = False
        End If
        If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=102 and User_ID= " & gUserID) = True Then
            BtncfrmMain.Enabled = True
        Else
            BtncfrmMain.Enabled = False
        End If

        Try
            Dim Service As New ServiceController
            Service.MachineName = "."
            Service.ServiceName = "FaildTransService"
            If Service.Status = ServiceControllerStatus.Stopped Then
                Service.Start()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        gfrmAllReports = New frmAllReports
        gfrmChoose.Hide()
        gfrmAllReports.ShowDialog()
    End Sub

    Private Sub BtncfrmMain_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
    BtncfrmMain.MouseEnter, BtncfrmAddNew.MouseEnter, Button1.MouseEnter, BtncfrmReport.MouseEnter, _
    btnManEditRpt.MouseEnter, btnCustQnty.MouseEnter
        sender.BackColor = Color.SkyBlue
    End Sub

    Private Sub BtncfrmMain_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
    BtncfrmMain.MouseLeave, BtncfrmAddNew.MouseLeave, Button1.MouseLeave, BtncfrmReport.MouseLeave, _
    btnManEditRpt.MouseLeave, btnCustQnty.MouseLeave
        sender.BackColor = Color.Gainsboro
    End Sub

    'Private Sub Button1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
    'Button1.Enter, BtncfrmMain.Enter, BtncfrmAddNew.Enter, BtncfrmReport.Enter
    '    sender.BackColor = Color.LightSeaGreen
    'End Sub

    'Private Sub Button1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
    'Button1.Leave, BtncfrmMain.Leave, BtncfrmAddNew.Leave, BtncfrmReport.Leave
    '    sender.BackColor = Color.Gainsboro
    'End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManEditRpt.Click
        gfrmManualEditReport = New ManualEditReport
        gfrmChoose.Hide()
        gfrmManualEditReport.ShowDialog()
    End Sub


    Private Sub btnCustQnty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustQnty.Click
        gfrmrptCustQnty = New frmrptCustQnty
        gfrmChoose.Hide()
        gfrmrptCustQnty.ShowDialog()

    End Sub

    Private Sub btnReturns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturns.Click
        gfrmReturns = New frmReturns
        gfrmChoose.Hide()
        gfrmReturns.ShowDialog()
    End Sub

    Private Sub BtnCollectedMotions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCollectedMotions.Click
        gfrmCollectedMotions = New cfrmCollectedMotions
        gfrmChoose.Hide()
        gfrmCollectedMotions.ShowDialog()
    End Sub
End Class