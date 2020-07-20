Public Class cfrmDrivers

    Public Function IsErrorMsg(ByVal ParamArray Ctrls() As Control) As Boolean

        Dim i As Short
        For i = 0 To UBound(Ctrls)
            Select Case Ctrls(i).Name
                '##################((( Driver )))#############
                Case TxtDriverName.Name
                    If TxtDriverName.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك أدخل أسم السائق"
                        Exit For
                    End If
                Case TxtDriverLicence.Name
                    If TxtDriverLicence.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك أدخل رقم رخصة السائق"
                        Exit For
                    End If
            End Select
        Next
        If gMsg = "" Then
            Return False
        Else
            Ctrls(i).Focus()
            Return True
        End If

    End Function

#Region "Drivers"

    Private Sub LstDrivers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LstDrivers.SelectedIndexChanged
        Try
            If Val(Me.LstDrivers.SelectedValue) = 0 Then
                Exit Sub
            End If
            gDriverO.ID = Val(LstDrivers.SelectedValue)
            gDriverO.Refresh()
            TxtDriverName.Text = gDriverO.Name
            TxtDriverLicence.Text = gDriverO.Driver_License_No
            BtnDriverAddNew.Enabled = True
            BtnDriverSave.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnDriverAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDriverAddNew.Click
        gDriverO.ID = 0
        TxtDriverName.Clear()
        TxtDriverLicence.Clear()

        TxtDriverName.Select()
        BtnDriverAddNew.Enabled = False
        BtnDriverSave.Enabled = True
    End Sub

    Private Sub BtnDriverSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDriverSave.Click
        If IsErrorMsg(TxtDriverName, TxtDriverLicence) = False Then

            Dim mstrDriverName As String = TxtDriverName.Text
            gDriverO.Name = TxtDriverName.Text
            gDriverO.Driver_License_No = TxtDriverLicence.Text
            gDriverO.Save()
            gAdo.CtrlItemsLoad("Driver_ID", "Driver_Name", "TblDriver ORDER BY Driver_Name", LstDrivers)
            LstDrivers.SelectedIndex = LstDrivers.FindString(mstrDriverName)
            BtnDriverAddNew.Enabled = True
            BtnDriverSave.Enabled = False

        Else
            MessageBox.Show(gMsg, "البيانات غير مكتمله", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub

    Private Sub BtnDriverDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDriverDelete.Click
        If gDriverO.Remove = False Then
            MessageBox.Show("هذا البيان مستخدم من قبل لايمكن حذفه", "عـمليه خـــاطئـــــه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            gAdo.CtrlItemsLoad("Driver_ID", "Driver_Name", "TblDriver ORDER BY Driver_Name", LstDrivers)

        End If
    End Sub

    Private Sub TxtDriverSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtDriverSearch.TextChanged

        LstDrivers.SelectedIndex = LstDrivers.FindString(TxtDriverSearch.Text)

    End Sub

#End Region

End Class