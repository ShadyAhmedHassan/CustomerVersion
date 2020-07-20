Imports System.IO.Ports

Public Class frmEditFirstScale

    Private Sub frmEditFirstScale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRead.Click


        Try
            cfrmMain.SharedRichTxt.Text = ""
            '  ' 20190217
            'cfrmMain.SharedRichTxt.Text = cfrmMain.serl.ReadExisting
            If cfrmMain.SharedRichTxt.Text.LastIndexOf("i") - 14 > 0 Then
                lblScaleWeight.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf("i") - 14, 6))
            Else
                lblScaleWeight.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf("i") + 3, 6))
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try
            Dim Count As Integer
            Count = gAdo.CmdExecScalar("select count (trans_id) from tblTransactions where  TR_TY=2 and CID=" & cfrmMain.SharedTxtTransID.Text)
            If Count < 1 Then
                MsgBox("لم يتم حفظ هذه الحركه")
            Else
                gAdo.CmdExec("update tblTransactions set First_Weigth=" & Val(lblScaleWeight.Text) & " " _
                           & " where TR_TY=2 and CID=" & cfrmMain.SharedTxtTransID.Text & " and First_Weigth_Scale_ID=" & cfrmMain.ScaleNo)
                cfrmMain.SharedlblFrstWeight.Text = lblScaleWeight.Text
                MsgBox(" تم التعديل")
                Me.Close()
            End If
        Catch ex As Exception
        End Try

    End Sub

End Class