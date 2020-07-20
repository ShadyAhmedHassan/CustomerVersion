Imports System.IO

Public Class cfrmAddNew

#Region "Declarations"
    Private mReturnTab As New TabPage
#End Region

#Region "Enumerators"
    Enum FrmRequest
        Car
        Truck
    End Enum
#End Region

#Region "Methods"

    Private Sub cfrmAddNew_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        gfrmChoose.Show()
    End Sub



    Private Sub cfrmAddNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' when he entter the program for the first time enter directly to  User tab
        Dim UserCount As Integer
        UserCount = gAdo.CmdExecScalar("Select count (User_ID) from TblUser")
        If UserCount = 1 Then
            tcAddNew.SelectedTab = tbUser
        Else
            'Select Tab According To Request Flag
            tcAddNew.SelectedTab = tbCar
        End If

        FrmRefresh()
        'To Allow Picture Even Drop&Drag
        PicCompany.AllowDrop = True
        'To Get Company Data in tabPages TblCompany
        GetCompanyData()
        ' here chech if there are no user has been inntialized
        Dim UserCount2 As Integer
        UserCount2 = gAdo.CmdExecScalar("Select count (User_ID) from TblUser")
        If UserCount2 = 1 Then
            GoTo NextSub
        End If
        'To See User Permission for Hide and Show TapPages
        Permission_TabPages()
NextSub:
        'To Clear Control Data
        ClearControls(sender, e)

    End Sub

    Sub ClearControls(ByVal sender As System.Object, ByVal e As System.EventArgs)

        btnCarAddNew_Click(sender, e)
        BtnTruckAddNew_Click(sender, e)
        BtnCityAddNew_Click(sender, e)
        BtnDriverAddNew_Click(sender, e)
        BtnDealerAddNew_Click(sender, e)
        BtnGoodsAddNew_Click(sender, e)
        BtnIssueToAddNew_Click(sender, e)
        BtnScaleAddNew_Click(sender, e)
        BtnUserAddNew_Click(sender, e)
        BtnShiftAddNew_Click(sender, e)
        BtnSlipAddNew_Click(sender, e)

    End Sub

    Sub Permission_TabPages()

        'الشرائح
        'If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=13 and User_ID= " & gUserID) = False Then
        tcAddNew.Controls.RemoveAt(12)
        'End If
        'الورادى
        If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=12 and User_ID= " & gUserID) = False Then
            tcAddNew.Controls.RemoveAt(11)
        End If
        'صلاحيات المستخدمين
        If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=11 and User_ID= " & gUserID) = False Then
            tcAddNew.Controls.RemoveAt(10)
        Else
            'To Set GrdUserPermission Columns
            gMethods.UserPermissionGrid(GrdUsePermission)
        End If
        'بيانات المستخدمين
        If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=10 and User_ID= " & gUserID) = False Then
            tcAddNew.Controls.RemoveAt(9)
        End If
        'بيانات الموازين
        'If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=9 and User_ID= " & gUserID) = False Then

        tcAddNew.Controls.RemoveAt(8)
        'End If
        'المرسل إليه
        If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=8 and User_ID= " & gUserID) = False Then
            tcAddNew.Controls.RemoveAt(7)
        End If
        'بيانات الشركه
        If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=7 and User_ID= " & gUserID) = False Then
            tcAddNew.Controls.RemoveAt(6)
        End If
        'أنواع الحموله
        'If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=6 and User_ID= " & gUserID) = False Then
        tcAddNew.Controls.RemoveAt(5)
        'End If
        'بيانات العملاء
        'If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=5 and User_ID= " & gUserID) = False Then
        tcAddNew.Controls.RemoveAt(4)
        'End If
        'بيانات السائقين
        If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=4 and User_ID= " & gUserID) = False Then
            tcAddNew.Controls.RemoveAt(3)
        End If
        'المدن
        If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=3 and User_ID= " & gUserID) = False Then
            tcAddNew.Controls.RemoveAt(2)
        End If
        'بيانات المقطورات
        If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=2 and User_ID= " & gUserID) = False Then
            tcAddNew.Controls.RemoveAt(1)
        End If
        'بيانات السيارات
        If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=1 and User_ID= " & gUserID) = False Then
            tcAddNew.Controls.RemoveAt(0)
        End If

    End Sub

    Private Sub tcAddNew_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tcAddNew.MouseClick
        FrmRefresh()
        'To Clear Control Data
        ClearControls(sender, e)
    End Sub

    Private Sub AllStrip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
                               Handles StripUpdate.Click, StripRemove.Click
        'Car Class Id holding the current CarID preparing for Update
        gCarO.ID = lstCars.SelectedValue
        Select Case sender.Name
            Case StripUpdate.Name
                gIsRemove = False
            Case StripRemove.Name
                gIsRemove = True
        End Select
    End Sub

    Public Sub CheckCtrlValues(ByVal ParamArray Ctrls() As Control)
        Dim I As Integer
        For I = 0 To UBound(Ctrls)
            Select Case Ctrls(I).Name
                '#################### (( Car )) #######################

                Case cboCarCity.Name
                    If cboCarCity.FindStringExact(cboCarCity.Text) < 0 Then
                        gCityO.ID = 0
                        gCityO.Name = cboCarCity.Text
                        gCityO.Save()
                        gCityO.ID = gAdo.GetMax("City_Id", "tblcity")
                    Else
                        cboCarCity.SelectedIndex = cboCarCity.FindStringExact(cboCarCity.Text)
                        gCityO.ID = cboCarCity.SelectedValue
                    End If


               
                    '#################### (( Truck )) #######################

                Case CboTruckCity.Name
                    If CboTruckCity.FindStringExact(CboTruckCity.Text) < 0 Then
                        gCityO.ID = 0
                        gCityO.Name = CboTruckCity.Text
                        gCityO.Save()
                        gCityO.ID = gAdo.GetMax("City_Id", "tblcity")
                    Else
                        CboTruckCity.SelectedIndex = CboTruckCity.FindStringExact(CboTruckCity.Text)
                        gCityO.ID = CboTruckCity.SelectedValue
                    End If

          

                    '#################### (( Driver )) #######################


            End Select
        Next


    End Sub

    Public Function IsErrorMsg(ByVal ParamArray Ctrls() As Control) As Boolean
        Dim i As Short
        For i = 0 To UBound(Ctrls)
            Select Case Ctrls(i).Name
                '#################### ((( Car ))) #####################
                Case txtCarNo.Name
                    If txtCarNo.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك ادخل رقم السياره"
                        Exit For
                    End If
                Case cboCarCity.Name
                    If cboCarCity.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك إختار اسم المدينه الخاصه بالسياره"
                        Exit For
                    End If
             

                    '#################### ((( Truck ))) #####################
                Case TxtTruckNO.Name
                    If TxtTruckNO.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك أدخل رقم المقطوره"
                        Exit For
                    End If
                Case CboTruckCity.Name
                    If CboTruckCity.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك إختار اسم المدينه الخاصه بالمقطوره"
                        Exit For
                    End If

                    '##################((( City )))#############
                Case TxtCityName.Name
                    If TxtCityName.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك أدخل أسم المدينه"
                        Exit For
                    End If

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

                    '##################((( Dealer )))#############
                Case txtDealerName.Name
                    If txtDealerName.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك أدخل أسم العميل"
                        Exit For
                    End If
                    '##################((( IssueTo )))#############
                Case TxtIssueToName.Name
                    If TxtIssueToName.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك أدخل أسم المرسل إليه"
                        Exit For
                    End If
                    '##################((( Goods )))#############
                Case TxtGoodsName.Name
                    If TxtGoodsName.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك أدخل أسم الصنف"
                        Exit For
                    End If
                    '##################((( Company )))#############
                Case TxtCompanyName.Name
                    If TxtCompanyName.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك أدخل أسم الشركه"
                        Exit For
                    End If
                Case TxtCompanyAddress.Name
                    If TxtCompanyAddress.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك أدخل عنوان الشركه"
                        Exit For
                    End If
                Case TxtCompanyTelephone.Name
                    If TxtCompanyTelephone.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك أدخل رقم تليفون الشركه"
                        Exit For
                    End If
                    '##################((( Scale )))#############
                Case TxtScaleName.Name
                    If TxtScaleName.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك أدخل أسم الميزان"
                        Exit For
                    End If
                Case TxtScalePort.Name
                    If TxtScalePort.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك أدخل مخرج الميزان"
                        Exit For
                    End If
                    '##################((( User )))#############
                Case TxtUserName.Name
                    If TxtUserName.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك أدخل أسم الموظف"
                        Exit For
                    End If
                Case TxtUserPassword.Name
                    If TxtUserPassword.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك أدخل كلمة السر"
                        Exit For
                    End If
                Case TxtUserPasswordConfirm.Name
                    If TxtUserPasswordConfirm.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك أدخل تأكيد كلمة السر"
                        Exit For
                    End If
                Case TxtUserNickName.Name
                    If TxtUserNickName.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك أدخل الأسم الحركى"
                        Exit For
                    End If

                    '##################((( Shift )))#############
                Case TxtShiftName.Name
                    If TxtShiftName.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك أدخل أسم الورديه"
                        Exit For
                    End If
                Case ChkShiftStartDate.Name
                    If ChkShiftStartDate.Checked = True And ChkShiftEndDate.Checked = False Then

                        gMsg = "غير منطقى أن يكون ميعاد بداية الورديه فى اليوم التالى و ميعاد نهاية الورديه فى اليوم السابق له"
                        Exit For
                    Else
                        gMsg = ""
                    End If

                    '##################((( Slip )))#############
                Case TxtSlipName.Name
                    If TxtSlipName.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك أدخل أسم الشريحه "
                        Exit For
                    End If
                    'Case TxtSlipMaxRange.Name

                    '    If Val(TxtSlipMaxRange.Text) <> 0 Then
                    '        If Val(TxtSlipMinRange.Text) > Val(TxtSlipMaxRange.Text) Then
                    '            gMsg = "لا يمكن ان يكون ادنى سعر أعلى من أعلى سعر "
                    '            Exit For
                    '        Else
                    '            gMsg = ""
                    '        End If
                    '    End If


                    'Case TxtSlipMinRange.Name

                    '    If Val(TxtSlipMinRange.Text) = 0 And Val(TxtSlipRate.Text) = 0 Then
                    '        gMsg = "لا يسمح بترك أدنى سعر و السعر العادى فارغين"
                    '        Exit For
                    '    End If

                Case TxtSlipRate.Name
                    '''''''''''''''''''''''''
                    If Val(TxtSlipMaxRange.Text) <> 0 Then
                        If Val(TxtSlipMinRange.Text) > Val(TxtSlipMaxRange.Text) Then
                            gMsg = "لا يمكن ان يكون ادنى سعر أعلى من أعلى سعر "
                            Exit For
                        Else
                            gMsg = ""
                        End If
                    End If

                    If Val(TxtSlipMinRange.Text) = 0 And Val(TxtSlipRate.Text) = 0 Then
                        gMsg = "لا يسمح بترك أدنى سعر و السعر العادى فارغين"
                        Exit For
                    End If
                    '''''''''''''''''''''''''

                    If Val(TxtSlipMinRange.Text) > 0 And Val(TxtSlipRate.Text) > 0 Then
                        If Val(TxtSlipMinRange.Text) > Val(TxtSlipRate.Text) Then
                            gMsg = "السعر العادى أقل  من أدنى سعر "
                            Exit For
                        Else
                            gMsg = ""
                        End If
                    End If

                    If Val(TxtSlipMaxRange.Text) > 0 And Val(TxtSlipRate.Text) > 0 Then
                        If Val(TxtSlipMaxRange.Text) < Val(TxtSlipRate.Text) Then
                            gMsg = "السعر العادى أعلى من أعلى سعر "
                            Exit For
                        Else
                            gMsg = ""
                        End If
                    End If

                    If Val(TxtSlipMinRange.Text) = 0 Then
                        TxtSlipMinRange.Text = TxtSlipRate.Text
                    ElseIf Val(TxtSlipRate.Text) = 0 Then
                        TxtSlipRate.Text = TxtSlipMinRange.Text

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

    Private Sub FrmRefresh()
        'Code to Load Controls [Cars - Cities - Dealers - Trucks] From Database
        'lstCars.DataSource = Nothing 
        gAdo.CtrlItemsLoad("Car_ID", "CarBoard_No", "tblCar Order by CarBoard_No", lstCars)
        gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity  where City_ID>1  ORDER BY City_name", cboCarCity)

        ' In City TO Fill LstCity
        gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity where City_ID>1  ORDER BY City_Name", LstCity)
        ' In Driver To Fill LstDriver
        gAdo.CtrlItemsLoad("Driver_ID", "Driver_Name", "TblDriver ORDER BY Driver_name", LstDrivers)
    
        ' In Car Info To Fill LstCarInfo
        gAdo.CtrlItemsLoad("Car_Info_ID", "TruckBoard_No", "TblCarInfo where Car_Info_ID>1 ORDER BY TruckBoard_No", LstTruck)
        gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity where City_ID>1 ORDER BY City_Name", CboTruckCity)

        ' In Goods Fill LstGoods
        gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood where  (Good_ID like '07'+'%' or  good_ID =1 ) and Good_ID in ('0705','0706','0711','0712','0713','0714') ORDER BY Good_Name desc", LstGoods)
        ' In User To Fill LstUser
        gAdo.CtrlItemsLoad("User_ID", "User_NickName", "tblUser", LstUser, "", "User_IsDeleted='false' and User_ID<>1 ORDER BY User_Name")
        'In IssueTo To Fill LstIssueTO
        gAdo.CtrlItemsLoad("Issue_To_ID", "Issue_To_Name", "tblIssueTo where Issue_To_ID<>1  ORDER BY Issue_To_Name", LstIssueTo)
        'In Scale To Fill LstScale
        gAdo.CtrlItemsLoad("Scale_ID", "Scale_Name", "tblScale", LstScale, "", "scale_IsDeleted='false' ORDER BY Scale_Name")
        'In UserPermission To Fill LstUserPermission
        gAdo.CtrlItemsLoad("User_ID", "User_NickName", "tblUser", LstUserPermission, "", "User_IsDeleted='false' and User_ID<>1 ORDER BY User_Name")
        'In Shift To Fill LstShift
        gAdo.CtrlItemsLoad("Shift_ID", "Shift_Name", "tblShift", LstShift, "", "Shift_IsDeleted='false' ORDER BY Shift_Name")
        'In Slip To Fill LstSlip
        gAdo.CtrlItemsLoad("Slip_ID", "Slip_Name", "tblSlip", LstSlip, "", "Slip_IsDeleted='false' ORDER BY Slip_Name")
        gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", "TblDealer ORDER BY Dealer_Name", LstDealer)

    End Sub

#End Region

#Region "Car"
    Private Sub lstCars_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstCars.SelectedIndexChanged
        Try
            If Val(Me.lstCars.SelectedValue) = 0 Then
                Exit Sub
            End If
            gCarO.ID = Val(lstCars.SelectedValue)
            gCarO.Refresh()
            txtCarNo.Text = gCarO.Name
            cboCarCity.SelectedValue = gCarO.CarBoard_City_ID.ID

            btnCarAddNew.Enabled = True
            btnCarSave.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCarAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCarAddNew.Click
        gCarO.ID = 0
        txtCarNo.Clear()
        If cboCarCity.Items.Count > 0 Then
            cboCarCity.SelectedIndex = 0
        End If

        txtCarNo.Select()
        btnCarAddNew.Enabled = False
        btnCarSave.Enabled = True


    End Sub

    Private Sub btnCarSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCarSave.Click
        If IsErrorMsg(txtCarNo, cboCarCity) = False Then

            CheckCtrlValues(cboCarCity)
            Dim mstrCarBoard_No As String = txtCarNo.Text
            gCarO.Name = txtCarNo.Text
            gCarO.CarBoard_City_ID.ID = gCityO.ID
            gCarO.ScaleID = cfrmMain.ScaleNo

            gCarO.Save()
            ''Code to refresh the CboCarCity
            gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity where City_ID>1 ORDER BY City_Name", cboCarCity)
            ''Code to refresh the Lstcars
            gAdo.CtrlItemsLoad("Car_ID", "CarBoard_No", "tblCar Order by CarBoard_No", lstCars)
            lstCars.SelectedIndex = lstCars.FindString(mstrCarBoard_No)
            btnCarAddNew.Enabled = True
            btnCarSave.Enabled = False
            MsgBox("لقد تم الحفظ بنجاح")
        Else
            MessageBox.Show(gMsg, "البيانات غير مكتمله", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub BtnCarDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCarDelete.Click

        Dim msgrslt As MsgBoxResult
        msgrslt = MsgBox("هل ترغب فى حذف هذا العنصر ؟", MsgBoxStyle.OkCancel)
        If msgrslt = MsgBoxResult.Ok Then
            Try

                If lstCars.Items.Count = 0 Then
                    Exit Sub
                End If

                Dim CarExist As Integer
                CarExist = gAdo.CmdExecScalar("select count(trans_id) from dbo.tblTransactions where car_id=" & lstCars.SelectedValue)
                If CarExist < 1 Then
                    gAdo.CmdExec("delete dbo.tblCar where Car_ID=" & lstCars.SelectedValue)
                    gAdo.CtrlItemsLoad("Car_ID", "CarBoard_No", "tblCar Order by CarBoard_No", lstCars)
                Else
                    MsgBox("هذا البيان مستخدم من قبل لايمكن حذفه")
                End If

            Catch ex As Exception

            End Try
        Else
        End If

        'If gCarO.Remove = True Then
        '    MessageBox.Show("هذا البيان مستخدم من قبل لايمكن حذفه", "عـمليه خـــاطئـــــه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        'Else
        '    ''Code to refresh the Lstcars

        'End If

    End Sub

    Private Sub TxtCarSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCarSearch.TextChanged
        lstCars.SelectedIndex = lstCars.FindString(TxtCarSearch.Text)
    End Sub
#End Region

#Region "City"

    Private Sub LstCity_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LstCity.SelectedIndexChanged

        Try
            If Val(Me.LstCity.SelectedValue) = 0 Then
                Exit Sub
            End If
            gCityO.ID = Val(LstCity.SelectedValue)
            gCityO.Refresh()
            TxtCityName.Text = gCityO.Name

            BtnCityAddNew.Enabled = True
            BtnCitySave.Enabled = True

        Catch ex As Exception

        End Try

    End Sub

    Private Sub BtnCityAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCityAddNew.Click

        gCityO.ID = 0
        TxtCityName.Clear()
        TxtCityName.Select()
        BtnCityAddNew.Enabled = False
        BtnCitySave.Enabled = True
    End Sub

    Private Sub BtnCitySave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCitySave.Click
        If IsErrorMsg(TxtCityName) = False Then

            Dim mstrCityName As String = TxtCityName.Text
            gCityO.Name = TxtCityName.Text
            gCityO.Save()
            gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity where City_ID>1 ORDER BY City_Name", LstCity)
            LstCity.SelectedIndex = LstCity.FindString(mstrCityName)
            BtnCityAddNew.Enabled = True
            BtnCitySave.Enabled = False
            MsgBox("لقد تم الحفظ بنجاح")
        Else
            MessageBox.Show(gMsg, "البيانات غير مكتمله", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub BtnCityDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCityDelete.Click
        Dim msgrslt As MsgBoxResult
        msgrslt = MsgBox("هل ترغب فى حذف هذا العنصر ؟", MsgBoxStyle.OkCancel)
        If msgrslt = MsgBoxResult.Ok Then
            Try
                If LstCity.Items.Count = 0 Then
                    Exit Sub
                End If
                Dim CityExist As Integer
                CityExist = gAdo.CmdExecScalar("select count(Car_ID) from tblCar where CarBoard_City_ID =" & LstCity.SelectedValue)
                If CityExist < 1 Then
                    gAdo.CmdExec("delete tblCity where City_ID=" & LstCity.SelectedValue)
                    gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity ORDER BY City_Name", LstCity)
                Else
                    MsgBox("هذا البيان مستخدم من قبل لايمكن حذفه")
                End If
            Catch ex As Exception

            End Try
        Else
        End If

        'If gCityO.Remove = False Then
        '    MessageBox.Show("هذا البيان مستخدم من قبل لايمكن حذفه", "عـمليه خـــاطئـــــه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        'Else

        'End If


    End Sub

    Private Sub txtCitySearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCitySearch.TextChanged
        LstCity.SelectedIndex = LstCity.FindString(txtCitySearch.Text)
    End Sub
#End Region

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
            gDriverO.ScaleID = cfrmMain.ScaleNo
            gDriverO.Save()
            gAdo.CtrlItemsLoad("Driver_ID", "Driver_Name", "TblDriver ORDER BY Driver_Name", LstDrivers)
            LstDrivers.SelectedIndex = LstDrivers.FindString(mstrDriverName)
            BtnDriverAddNew.Enabled = True
            BtnDriverSave.Enabled = False
            MsgBox("لقد تم الحفظ بنجاح")
        Else
            MessageBox.Show(gMsg, "البيانات غير مكتمله", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If


    End Sub

    Private Sub BtnDriverDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDriverDelete.Click

        Dim msgrslt As MsgBoxResult
        msgrslt = MsgBox("هل ترغب فى حذف هذا العنصر ؟", MsgBoxStyle.OkCancel)
        If msgrslt = MsgBoxResult.Ok Then
            Try
                If LstDrivers.Items.Count = 0 Then
                    Exit Sub
                End If
                Dim CityExist As Integer
                CityExist = gAdo.CmdExecScalar("select count(trans_id) from dbo.tblTransactions where Driver_ID=" & LstDrivers.SelectedValue)
                If CityExist < 1 Then
                    gAdo.CmdExec("delete tblDriver where Driver_ID=" & LstDrivers.SelectedValue)
                    gAdo.CtrlItemsLoad("Driver_ID", "Driver_Name", "TblDriver ORDER BY Driver_Name", LstDrivers)
                Else
                    MsgBox("هذا البيان مستخدم من قبل لايمكن حذفه")
                End If
            Catch ex As Exception

            End Try
        Else
        End If

        'If LstDrivers.Items.Count = 0 Then
        '    Exit Sub
        'End If
        'If gDriverO.Remove = False Then
        '    MessageBox.Show("هذا البيان مستخدم من قبل لايمكن حذفه", "عـمليه خـــاطئـــــه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        'Else
        'End If

    End Sub

    Private Sub TxtDriverSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtDriverSearch.TextChanged
        LstDrivers.SelectedIndex = LstDrivers.FindString(TxtDriverSearch.Text)
    End Sub
#End Region

#Region "Dealer"

    Private Sub LstDealer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LstDealer.SelectedIndexChanged
        Try
            If Val(Me.LstDealer.SelectedValue) = 0 Then
                Exit Sub
            End If
            gDealerO.ID = Val(LstDealer.SelectedValue)
            gDealerO.Refresh()
            txtDealerName.Text = gDealerO.Name
            BtnDealerAddNew.Enabled = True
            BtnDealerSave.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnDealerAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDealerAddNew.Click
        gDealerO.ID = 0
        txtDealerName.Clear()
        txtDealerName.Select()
        BtnDealerAddNew.Enabled = False
        BtnDealerSave.Enabled = True
    End Sub

    Private Sub BtnDealerSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDealerSave.Click
        If IsErrorMsg(txtDealerName) = False Then

            Dim mstrDealerName As String = txtDealerName.Text
            gDealerO.Name = txtDealerName.Text
            gDealerO.Save()
            gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", "TblDealer ORDER BY Dealer_Name", LstDealer)
            LstDealer.SelectedIndex = LstDealer.FindString(mstrDealerName)
            BtnDealerAddNew.Enabled = True
            BtnDealerSave.Enabled = False
            MsgBox("لقد تم الحفظ بنجاح")
        Else
            MessageBox.Show(gMsg, "البيانات غير مكتمله", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub

    Private Sub BtnDealerDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDealerDelete.Click
        Dim msgrslt As MsgBoxResult
        msgrslt = MsgBox("هل ترغب فى حذف هذا العنصر ؟", MsgBoxStyle.OkCancel)
        If msgrslt = MsgBoxResult.Ok Then
            Try
                If LstDealer.Items.Count = 0 Then
                    Exit Sub
                End If
                Dim CityExist As Integer
                CityExist = gAdo.CmdExecScalar("select count(trans_id) from dbo.tblTransactions where Dealer_ID=" & LstDealer.SelectedValue)
                If CityExist < 1 Then
                    gAdo.CmdExec("delete tblDealer where Dealer_ID=" & LstDealer.SelectedValue)
                    gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", "TblDealer ORDER BY Dealer_Name", LstDealer)
                Else
                    MsgBox("هذا البيان مستخدم من قبل لايمكن حذفه")
                End If
            Catch ex As Exception

            End Try
        Else
        End If

        'If gDealerO.Remove = False Then
        '    MessageBox.Show("هذا البيان مستخدم من قبل لايمكن حذفه", "عـمليه خـــاطئـــــه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        'Else
        'End If

    End Sub

    Private Sub TxtDealerSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtDealerSearch.TextChanged
        LstDealer.SelectedIndex = LstDealer.FindString(TxtDealerSearch.Text)
    End Sub
#End Region

#Region "CarInfo"
    Private Sub LstTruck_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LstTruck.SelectedIndexChanged
        Try
            If Val(Me.LstTruck.SelectedValue) = 0 Then
                Exit Sub
            End If
            gCarInfoO.ID = Val(LstTruck.SelectedValue)
            gCarInfoO.Refresh()
            TxtTruckNO.Text = gCarInfoO.Name
            CboTruckCity.SelectedValue = gCarInfoO.TruckBoard_City_ID.ID

            BtnTruckAddNew.Enabled = True
            BtnTruckSave.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnTruckAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTruckAddNew.Click
        gCarInfoO.ID = 0
        TxtTruckNO.Clear()
        If CboTruckCity.Items.Count > 0 Then
            CboTruckCity.SelectedIndex = 0
        End If
       
        TxtTruckNO.Select()
        BtnTruckAddNew.Enabled = False
        BtnTruckSave.Enabled = True
    End Sub

    Private Sub BtnTruckSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTruckSave.Click

        If IsErrorMsg(TxtTruckNO, CboTruckCity) = False Then

            CheckCtrlValues(CboTruckCity)

            Dim mstrTruckBoardNo As String = TxtTruckNO.Text
            gCarInfoO.Name = TxtTruckNO.Text
            gCarInfoO.TruckBoard_City_ID.ID = gCityO.ID
            gCarInfoO.ScaleID = cfrmMain.ScaleNo
            gCarInfoO.Save()
            ''Code to refresh the CboTruckCity
            gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity where City_ID>1 ORDER BY City_Name", CboTruckCity)

            ''Code to refresh the LstTruck
            gAdo.CtrlItemsLoad("Car_Info_ID", "TruckBoard_No", "TblCarInfo where Car_Info_ID>1 ORDER BY TruckBoard_No", LstTruck)

            LstTruck.SelectedIndex = LstTruck.FindString(mstrTruckBoardNo)
            BtnTruckAddNew.Enabled = True
            BtnTruckSave.Enabled = False
            MsgBox("لقد تم الحفظ بنجاح")
        Else
            MessageBox.Show(gMsg, "البيانات غير مكتمله", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub BtnTruckDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTruckDelete.Click

        Dim msgrslt As MsgBoxResult
        msgrslt = MsgBox("هل ترغب فى حذف هذا العنصر ؟", MsgBoxStyle.OkCancel)
        If msgrslt = MsgBoxResult.Ok Then
            Try
                If LstTruck.Items.Count = 0 Then
                    Exit Sub
                End If

                Dim CarExist As Integer
                CarExist = gAdo.CmdExecScalar("select count(trans_id) from dbo.tblTransactions where Car_Info_ID=" & LstTruck.SelectedValue)
                If CarExist < 1 Then
                    gAdo.CmdExec("delete tblCarInfo where Car_Info_ID=" & LstTruck.SelectedValue)
                    gAdo.CtrlItemsLoad("Car_Info_ID", "TruckBoard_No", "TblCarInfo ORDER BY TruckBoard_No", LstTruck)
                Else
                    MsgBox("هذا البيان مستخدم من قبل لايمكن حذفه")
                End If
            Catch ex As Exception

            End Try
        Else
        End If

        'If gCarInfoO.Remove = False Then
        '    MessageBox.Show("هذا البيان مستخدم من قبل لايمكن حذفه", "عـمليه خـــاطئـــــه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        'Else
        'End If

    End Sub

    Private Sub TxtTruckSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtTruckSearch.TextChanged
        LstTruck.SelectedIndex = LstTruck.FindString(TxtTruckSearch.Text)
    End Sub
#End Region

#Region "Goods"
    Private Sub LstGoods_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LstGoods.SelectedIndexChanged
        Try
            If Val(Me.LstGoods.SelectedValue) = 0 Then
                Exit Sub
            End If
            gGoodO.ID = LstGoods.SelectedValue
            gGoodO.Refresh()
            TxtGoodsName.Text = gGoodO.Name
            BtnGoodsAddNew.Enabled = True
            BtnGoodsSave.Enabled = True
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BtnGoodsAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGoodsAddNew.Click
        gGoodO.ID = 0
        TxtGoodsName.Clear()
        TxtGoodsName.Select()
        BtnGoodsAddNew.Enabled = False
        BtnGoodsSave.Enabled = True
    End Sub

    Private Sub BtnGoodsSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGoodsSave.Click
        If IsErrorMsg(TxtGoodsName) = False Then

            Dim mstrGoodsName As String = TxtGoodsName.Text
            gGoodO.Name = TxtGoodsName.Text
            gGoodO.Save()
            gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood ORDER BY Good_Name", LstGoods)
            LstGoods.SelectedValue = LstGoods.FindString(mstrGoodsName)
            BtnGoodsAddNew.Enabled = True
            BtnGoodsSave.Enabled = False
            MsgBox("لقد تم الحفظ بنجاح")
        Else
            MessageBox.Show(gMsg, "البيانات غير مكتمله", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If


    End Sub

    Private Sub BtnGoodsDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGoodsDelete.Click

        Dim msgrslt As MsgBoxResult
        msgrslt = MsgBox("هل ترغب فى حذف هذا العنصر ؟", MsgBoxStyle.OkCancel)
        If msgrslt = MsgBoxResult.Ok Then
            Try
                If LstGoods.Items.Count = 0 Then
                    Exit Sub
                End If
                Dim CityExist As Integer
                CityExist = gAdo.CmdExecScalar("select count(trans_id) from dbo.tblTransactions where Good_ID=" & LstGoods.SelectedValue)
                If CityExist < 1 Then
                    gAdo.CmdExec("delete dbo.tblGood where Good_ID=" & LstGoods.SelectedValue)
                    gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood ORDER BY Good_Name", LstGoods)
                Else
                    MsgBox("هذا البيان مستخدم من قبل لايمكن حذفه")
                End If
            Catch ex As Exception

            End Try
        Else
        End If

        'If gGoodO.Remove = False Then
        '    MessageBox.Show("هذا البيان مستخدم من قبل لايمكن حذفه", "عـمليه خـــاطئـــــه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        'Else
        'End If

    End Sub

    Private Sub TxtGoodsSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtGoodsSearch.TextChanged
        LstGoods.SelectedIndex = LstGoods.FindString(TxtGoodsSearch.Text)
    End Sub
#End Region

#Region "Company"
    'Global Picture Path
    Dim PicPath As String

    Private Sub BtnCompanySave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCompanySave.Click
        If IsErrorMsg(TxtCompanyName, TxtCompanyAddress, TxtCompanyTelephone) = False Then

            gCompanyO.Name = TxtCompanyName.Text
            gCompanyO.Company_Telephone = TxtCompanyTelephone.Text
            gCompanyO.Company_Address = TxtCompanyAddress.Text
            gCompanyO.Password = txtPassWord.Text
            gCompanyO.ManPass = txtManualPassword.Text
            gCompanyO.Save()
            MsgBox("لقد تم الحفظ بنجاح")
        Else
            MessageBox.Show(gMsg, "البيانات غير مكتمله", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If



    End Sub

    Private Sub BtnCompanyLoadPic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCompanyLoadPic.Click
        With OpenFileDialog1
            .CheckFileExists = True
            .ShowReadOnly = False
            .Filter = "All Files|*.*|Bitmap Files (*)|*.bmp;*.gif;*.jpg"
            .FilterIndex = 2
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                PicPath = .FileName
                gCompanyO.Company_Pic_ID.ID = gMethods.BinaryPicStore(PicPath, PicCompany)
            End If
        End With
    End Sub

    Private Sub BtnCompanyDeletePic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCompanyDeletePic.Click
        Try
            PicPath = ""
            PicCompany.Image = Nothing
            gCompanyO.Company_Pic_ID.ID = 0

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub GetCompanyData()
        If gAdo.CmdExecScalar(" select count(*) from TblCompany") > 0 Then
            gCompanyO.ID = gAdo.GetMax("company_ID", "tblCompany")
            gCompanyO.Refresh()
            TxtCompanyName.Text = gCompanyO.Name
            TxtCompanyTelephone.Text = gCompanyO.Company_Telephone
            TxtCompanyAddress.Text = gCompanyO.Company_Address
            txtPassWord.Text = gCompanyO.Password
            txtManualPassword.Text = gCompanyO.ManPass
            gMethods.BinaryPicLoad("Picture", "tblPicture", "Pic_ID =" & gCompanyO.Company_Pic_ID.ID, PicCompany)
            PicCompany.SizeMode = PictureBoxSizeMode.StretchImage
        Else
            gCompanyO.ID = 0

        End If
    End Sub
#Region "PicCompany Drag And Drop"
    Private Sub PicCompany_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PicCompany.DragDrop
        PicPath = ((CType(e.Data.GetData(DataFormats.FileDrop), Array).GetValue(0).ToString))
        PicCompany.Load(PicPath)
        PicCompany.SizeMode = PictureBoxSizeMode.StretchImage
        gCompanyO.Company_Pic_ID.ID = gMethods.BinaryPicStore(PicPath, PicCompany)
    End Sub

    Private Sub PicCompany_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PicCompany.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

#End Region

#End Region

#Region "User"

    Private Sub LstUser_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LstUser.SelectedIndexChanged
        Try
            If Val(Me.LstUser.SelectedValue) = 0 Then
                Exit Sub
            End If
            gUserO.ID = Val(LstUser.SelectedValue)
            gUserO.Refresh()
            TxtUserName.Text = gUserO.Name
            TxtUserPassword.Text = gUserO.User_Password
            TxtUserPasswordConfirm.Text = gUserO.User_Password
            TxtUserNickName.Text = gUserO.User_NickName
            'TO Get the Picture From The SQL DataBase 
            If gMethods.BinaryPicLoad("Picture", "tblPicture", "Pic_ID =" & gUserO.User_Pic_ID.ID, PicUser) = False Then
                PicUser.Image = Nothing
            Else

            End If

            BtnUserAddNew.Enabled = True
            BtnUserSave.Enabled = True
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BtnUserAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUserAddNew.Click
        gUserO.ID = 0
        TxtUserName.Clear()
        TxtUserNickName.Clear()
        TxtUserPassword.Clear()
        TxtUserPasswordConfirm.Clear()
        PicPath = ""
        PicUser.Image = Nothing
        gUserO.User_Pic_ID.ID = 0
        TxtUserName.Select()
        BtnUserAddNew.Enabled = False
        BtnUserSave.Enabled = True
    End Sub

    Private Sub BtnUserSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUserSave.Click
        If IsErrorMsg(TxtUserName, TxtUserNickName, TxtUserPassword, TxtUserPasswordConfirm) = False Then
            If TxtUserPassword.Text <> TxtUserPasswordConfirm.Text Then
                gMsg = "كلمتى المرور غير منطابقتان"
                MessageBox.Show(gMsg, "البيانات غير مكتمله", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            'Notes that the Admin is hidden(doesn't show the name "Admin"  in the lstUser
            'We check the Admin by TxtUserName & TxtUserNickName 
            If TxtUserName.Text = "Admin" And TxtUserNickName.Text = "Admin" Then
                gUserO.ID = 1
                gUserO.Name = TxtUserName.Text
                gUserO.User_Password = TxtUserPassword.Text
                gUserO.User_NickName = TxtUserNickName.Text
                gUserO.Save()
                Dim UserCount As Integer
                UserCount = gAdo.CmdExecScalar("Select count (User_ID) from TblUser")
                If UserCount > 0 Then
                    FrmRefresh()
                    tcAddNew.SelectedTab = tbUserPermission
                End If
                MsgBox("لقد تم الحفظ بنجاح")
            Else

                Dim mstrUserName As String = TxtUserNickName.Text
                gUserO.Name = TxtUserName.Text
                gUserO.User_Password = TxtUserPassword.Text
                gUserO.User_NickName = TxtUserNickName.Text
                gUserO.Save()
                gAdo.CtrlItemsLoad("User_ID", "User_NickName", "tblUser", LstUser, "", "User_IsDeleted='false' and User_ID<>1 ORDER BY User_Name")
                LstUser.SelectedIndex = LstUser.FindString(mstrUserName)
                'To Set User Permission In TblUserPermission (Junction Table Between tblUser & tblPermission)
                gMethods.setUserPermission(gUserO.ID)
                BtnUserAddNew.Enabled = True
                BtnUserSave.Enabled = False
                Dim UserCount As Integer
                UserCount = gAdo.CmdExecScalar("Select count (User_ID) from TblUser")
                If UserCount > 0 Then
                    FrmRefresh()
                    ClearControls(sender, e)
                    tcAddNew.SelectedTab = tbUserPermission
                End If
            End If
            MsgBox("لقد تم الحفظ بنجاح")
        Else
            MessageBox.Show(gMsg, "البيانات غير مكتمله", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub

    Private Sub BtnUserDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUserDelete.Click
        If LstUser.Items.Count = 0 Then
            Exit Sub
        End If
        gAdo.CmdExec("Update TblUser set User_IsDeleted='true' where user_ID=" & gUserO.ID)
        gAdo.CtrlItemsLoad("User_ID", "User_NickName", "tblUser", LstUser, "", "User_IsDeleted='false' and User_ID<>1 ORDER BY User_Name")
    End Sub

    Private Sub BtnUserLoadPic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUserLoadPic.Click
        With OpenFileDialog1
            .CheckFileExists = True
            .ShowReadOnly = False
            .Filter = "All Files|*.*|Bitmap Files (*)|*.bmp;*.gif;*.jpg"
            .FilterIndex = 2
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                PicPath = .FileName
                gUserO.User_Pic_ID.ID = gMethods.BinaryPicStore(PicPath, PicUser)
            End If
        End With
    End Sub

    Private Sub BtnUserDeletePic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUserDeletePic.Click
        Try
            PicPath = ""
            PicUser.Image = Nothing
            gUserO.User_Pic_ID.ID = 0

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub TxtUserSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtUserSearch.TextChanged
        LstUser.SelectedIndex = LstUser.FindString(TxtUserSearch.Text)
    End Sub

#Region "PicUser Drag And Drop User"
    Private Sub PicUser_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PicUser.DragDrop
        PicPath = ((CType(e.Data.GetData(DataFormats.FileDrop), Array).GetValue(0).ToString))
        PicUser.Load(PicPath)
        PicUser.SizeMode = PictureBoxSizeMode.StretchImage
        gUserO.User_Pic_ID.ID = gMethods.BinaryPicStore(PicPath, PicUser)
    End Sub

    Private Sub PicUser_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PicUser.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

#End Region

#End Region

#Region "Issue To"

    Private Sub LstIssueTo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LstIssueTo.SelectedIndexChanged
        Try
            If Val(Me.LstIssueTo.SelectedValue) = 0 Then
                Exit Sub
            End If
            gIssueToO.ID = Val(LstIssueTo.SelectedValue)
            gIssueToO.Refresh()
            TxtIssueToName.Text = gIssueToO.Name
            TxtIssueToTelephone.Text = gIssueToO.IssueTo_Telephone
            TxtIssueToMobile.Text = gIssueToO.IssueTo_Mobile
            TxtIssueToFax.Text = gIssueToO.IssueTo_Fax
            TxtIssueToAddress.Text = gIssueToO.IssueTo_Address
            TxtIssueToField.Text = gIssueToO.IssueTo_Field
            BtnIssueToAddNew.Enabled = True
            BtnIssueToSave.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnIssueToAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnIssueToAddNew.Click
        gIssueToO.ID = 0
        TxtIssueToName.Clear()
        TxtIssueToTelephone.Clear()
        TxtIssueToMobile.Clear()
        TxtIssueToFax.Clear()
        TxtIssueToAddress.Clear()
        TxtIssueToField.Clear()
        TxtIssueToName.Select()
        BtnIssueToAddNew.Enabled = False
        BtnIssueToSave.Enabled = True
    End Sub

    Private Sub BtnIssueToSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnIssueToSave.Click
        If IsErrorMsg(TxtIssueToName) = False Then

            Dim mstrIssueToName As String = TxtIssueToName.Text
            gIssueToO.Name = TxtIssueToName.Text
            gIssueToO.IssueTo_Telephone = TxtIssueToTelephone.Text
            gIssueToO.IssueTo_Mobile = TxtIssueToMobile.Text
            gIssueToO.IssueTo_Fax = TxtIssueToFax.Text
            gIssueToO.IssueTo_Address = TxtIssueToAddress.Text
            gIssueToO.IssueTo_Field = TxtIssueToField.Text
            gIssueToO.ScaleID = cfrmMain.ScaleNo
            gIssueToO.Save()
            gAdo.CtrlItemsLoad("Issue_To_ID", "Issue_To_Name", "tblIssueTo ORDER BY Issue_To_Name", LstIssueTo)
            LstIssueTo.SelectedIndex = LstIssueTo.FindString(mstrIssueToName)
            BtnIssueToAddNew.Enabled = True
            BtnIssueToSave.Enabled = False
            MsgBox("لقد تم الحفظ بنجاح")
        Else
            MessageBox.Show(gMsg, "البيانات غير مكتمله", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If



    End Sub

    Private Sub BtnIssueToDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnIssueToDelete.Click

        Dim msgrslt As MsgBoxResult
        msgrslt = MsgBox("هل ترغب فى حذف هذا العنصر ؟", MsgBoxStyle.OkCancel)
        If msgrslt = MsgBoxResult.Ok Then
            Try
                If LstIssueTo.Items.Count = 0 Then
                    Exit Sub
                End If
                Dim CityExist As Integer
                CityExist = gAdo.CmdExecScalar("select count(trans_id) from dbo.tblTransactions where Issue_To_ID=" & LstIssueTo.SelectedValue)
                If CityExist < 1 Then
                    gAdo.CmdExec("delete  tblIssueTo where Issue_To_ID=" & LstIssueTo.SelectedValue)
                    gAdo.CtrlItemsLoad("Issue_To_ID", "Issue_To_Name", "tblIssueTo ORDER BY Issue_To_Name", LstIssueTo)
                Else
                    MsgBox("هذا البيان مستخدم من قبل لايمكن حذفه")
                End If
            Catch ex As Exception
            End Try
        Else
        End If

        'If gIssueToO.Remove = False Then
        '    MessageBox.Show("هذا البيان مستخدم من قبل لايمكن حذفه", "عـمليه خـــاطئـــــه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        'Else
        'End If

    End Sub

    Private Sub TxtIssueToSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtIssueToSearch.TextChanged
        LstIssueTo.SelectedIndex = LstIssueTo.FindString(TxtIssueToSearch.Text)
    End Sub
#End Region

#Region "Scale"

    Private Sub LstScale_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LstScale.SelectedIndexChanged
        Try
            If Val(Me.LstScale.SelectedValue) = 0 Then
                Exit Sub
            End If
            gScaleO.ID = LstScale.SelectedValue
            gScaleO.Refresh()
            TxtScaleName.Text = gScaleO.Name
            If gScaleO.Scale_Port = "0" Then
                CboPortType.SelectedIndex = 1
            Else
                CboPortType.SelectedIndex = 0
            End If
            TxtScalePort.Text = gScaleO.Scale_Port
            TxtScaleIp.Text = gScaleO.IP
            Dim Ip As String
            Ip = gScaleO.IP
            txtIP4.Text = Ip.Substring(Ip.LastIndexOf(".") + 1)
            Ip = Ip.Substring(0, Ip.LastIndexOf("."))
            txtIP3.Text = Ip.Substring(Ip.LastIndexOf(".") + 1)
            Ip = Ip.Substring(0, Ip.LastIndexOf("."))
            txtIP2.Text = Ip.Substring(Ip.LastIndexOf(".") + 1)
            Ip = Ip.Substring(0, Ip.LastIndexOf("."))
            txtIP1.Text = Ip

            TxtScalePortNo.Text = gScaleO.PortNo
            BtnScaleAddNew.Enabled = True
            BtnScaleSave.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnScaleAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnScaleAddNew.Click
        gScaleO.ID = 0
        TxtScaleName.Clear()
        TxtScalePort.Clear()
        TxtScaleName.Select()
        BtnScaleAddNew.Enabled = False
        BtnScaleSave.Enabled = True
    End Sub

    Private Sub BtnScaleSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnScaleSave.Click

        'If IsErrorMsg(TxtScaleName, TxtScalePort) = False Then
        If CboPortType.SelectedIndex <> 0 Then
            If Val(txtIP1.Text) = 0 Or Val(txtIP4.Text) = 0 Or Val(txtIP1.Text) > 255 _
                 Or Val(txtIP2.Text) > 255 Or Val(txtIP4.Text) >= 255 Or Val(txtIP4.Text) >= 255 _
                 Or txtIP4.Text = "" Or txtIP4.Text = "" Or txtIP4.Text = "" Or txtIP4.Text = "" Then
                MsgBox("من فضلك تاكد من ادخال الارقام بطريقه صحيحه")
                Exit Sub
            End If
        End If

        Dim mstrScaleName As String = TxtScaleName.Text
        gScaleO.Name = TxtScaleName.Text
        'gScaleO.Scale_Port = TxtScalePort.Text
        'gScaleO.Scale_Port = 0
        ' check the format of the IP
        If CboPortType.SelectedIndex = 0 Then
            gScaleO.IP = " "
            gScaleO.PortNo = 0
            gScaleO.Scale_Port = CboPortType.SelectedItem
        Else
            gScaleO.IP = txtIP1.Text & "." & txtIP2.Text & "." & txtIP3.Text & "." & txtIP4.Text  'TxtScaleIp.Text
            gScaleO.PortNo = TxtScalePortNo.Text
            gScaleO.Scale_Port = 0
        End If
        gScaleO.Save()
        gAdo.CtrlItemsLoad("Scale_ID", "Scale_Name", "tblScale", LstScale, "", "scale_IsDeleted='false' ORDER BY Scale_Name")
        LstScale.SelectedIndex = LstScale.FindString(mstrScaleName)
        BtnScaleAddNew.Enabled = True
        BtnScaleSave.Enabled = False
        Clear()
        MsgBox("لقد تم الحفظ بنجاح")
        ' Else
        'MessageBox.Show(gMsg, "البيانات غير مكتمله", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        'End If

    End Sub

    Private Sub BtnScaleDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnScaleDelete.Click
        If LstScale.Items.Count = 0 Then
            Exit Sub
        End If
        gAdo.CmdExec("Update TblScale set Scale_IsDeleted='true' where Scale_ID=" & LstScale.SelectedValue)
        gAdo.CtrlItemsLoad("Scale_ID", "Scale_Name", "tblScale", LstScale, "", "scale_IsDeleted='false' ORDER BY Scale_Name")

    End Sub

    Private Sub TxtScaleSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtScaleSearch.TextChanged
        LstScale.SelectedIndex = LstScale.FindString(TxtScaleSearch.Text)
    End Sub
#End Region

#Region "UserPermission"

    Private Sub BtnUserPermissionSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUserPermissionSave.Click
        Try
            Dim i As Integer
            For i = 0 To GrdUsePermission.RowCount - 1
                gAdo.CmdExec("Update tblUserPermission " _
                             & "set Allow ='" & GrdUsePermission.Rows(i).Cells("allow").Value.ToString _
                             & "' where User_Permission_ID= " _
                             & GrdUsePermission.Rows(i).Cells("User_Permission_ID").Value.ToString)

            Next

            MsgBox("تم الحفظ بنجاح")

        Catch ex As Exception

        End Try

    End Sub

    Private Sub LstUserPermission_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LstUserPermission.SelectedIndexChanged
        Try
       
            If Val(Me.LstUserPermission.SelectedValue) = 0 Then
                Exit Sub
            End If
            gUserPermissionO.Refresh()
            gUserPermissionO.User_ID.ID = Val(LstUserPermission.SelectedValue)
            Dim ds As DataSet
            ds = gAdo.SelectData("select UP.User_Permission_ID,Pr.Permission_Name,Up.allow " _
                                & " from dbo.tblPermission as Pr join dbo.tblUserPermission as UP " _
                               & "on Pr.Permission_ID = uP.Permission_ID where UP.User_ID= " & gUserPermissionO.User_ID.ID, False, "tblpermission")

            GrdUsePermission.DataSource = ds.Tables(0)
            gMethods.UserPermissionGrid(GrdUsePermission)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TxtUserPermissionSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtUserPermissionSearch.TextChanged
        LstUserPermission.SelectedIndex = LstUserPermission.FindString(TxtUserPermissionSearch.Text)
    End Sub

#End Region

#Region "Shift"
    Private Sub LstShift_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LstShift.SelectedIndexChanged
        Try
            If Val(Me.LstShift.SelectedValue) = 0 Then
                Exit Sub
            End If
            gShiftO.ID = LstShift.SelectedValue
            gShiftO.Refresh()
            TxtShiftName.Text = gShiftO.Name
            DTShiftStart.DateTimeValue = gShiftO.Shift_Start_Time
            DTShiftEnd.DateTimeValue = gShiftO.Shift_End_Time


            'التاريخ الأول عند الساعه الأولى
            If gShiftO.Shift_Start_Date = True Then
                ChkShiftStartDate.Checked = True
            Else
                ChkShiftStartDate.Checked = False
            End If

            'التاريخ الثانى عند الساعه الثانيه
            If gShiftO.Shift_End_Date = True Then

                ChkShiftEndDate.Checked = True
            Else
                ChkShiftEndDate.Checked = False
            End If
            'فى حالة ان الورديه مش أولى و له الأخيره
            If gShiftO.Shift_IsFirst = 5 Then
                ChkShiftFirstShift.Checked = False
                ChkShiftLastShift.Checked = False
                'فى حالة أن الورديه الأولى
            ElseIf gShiftO.Shift_IsFirst = 1 Then
                ChkShiftFirstShift.Checked = True
                ChkShiftLastShift.Checked = False
                'فى حالة أن الورديه الأخيره
            ElseIf gShiftO.Shift_IsFirst = 10 Then
                ChkShiftLastShift.Checked = True
                ChkShiftFirstShift.Checked = False
            End If




            BtnShiftAddNew.Enabled = True
            BtnShiftSave.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnShiftAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnShiftAddNew.Click
        gShiftO.ID = 0
        TxtShiftName.Clear()
        DTShiftStart.DateTimeValue = Now
        DTShiftEnd.DateTimeValue = Now
        ChkShiftEndDate.Checked = False
        ChkShiftFirstShift.Checked = False
        ChkShiftLastShift.Checked = False
        ChkShiftStartDate.Checked = False
        TxtShiftName.Select()
        BtnShiftAddNew.Enabled = False
        BtnShiftSave.Enabled = True
    End Sub

    Private Sub BtnShiftSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnShiftSave.Click

        If IsErrorMsg(TxtShiftName, ChkShiftStartDate) = False Then

            Dim mstrShift_Start_Time As String = (CType(DTShiftStart.TimeValue.ToString, String))
            Dim mstrShift_End_Time As String = CType(DTShiftEnd.TimeValue.ToString, String)
            ' ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ TH ~~~~
            If mstrShift_Start_Time.Contains("ص") Then
                mstrShift_Start_Time = mstrShift_Start_Time.Replace("ص", "AM")
            ElseIf mstrShift_Start_Time.Contains("م") Then
                mstrShift_Start_Time = mstrShift_Start_Time.Replace("م", "PM")
            End If
            If mstrShift_End_Time.Contains("ص") Then
                mstrShift_End_Time = mstrShift_End_Time.Replace("ص", "AM")
            ElseIf mstrShift_End_Time.Contains("م") Then
                mstrShift_End_Time = mstrShift_End_Time.Replace("م", "PM")
            End If
            ' ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            'التاريخ الأول عند الساعه الأولى
            If ChkShiftStartDate.Checked = True Then
                'In Case the Shift Start Time is in the next day
                gShiftO.Shift_Start_Date = True
                gShiftO.Shift_Start_Time = "01/02/1900 " & Mid(mstrShift_Start_Time, 12, 11)
            Else
                'In Case the Shift Start Time is in the same day
                gShiftO.Shift_Start_Date = False
                gShiftO.Shift_Start_Time = "01/01/1900 " & Mid(mstrShift_Start_Time, 12, 11)

            End If

            'التاريخ الثانى عند الساعه الثانيه
            If ChkShiftEndDate.Checked = True Then
                'In Case the Shift end Time is in the next day
                gShiftO.Shift_End_Date = True
                'Notes that Month(01) & Day(02) & Year(1900)
                gShiftO.Shift_End_Time = "01/02/1900 " & Mid(mstrShift_End_Time, 12, 6) & "59.998" & Mid(mstrShift_End_Time, 20, 3)
            Else
                'In Case the Shift end Time is in the same day
                gShiftO.Shift_End_Date = False
                gShiftO.Shift_End_Time = "01/01/1900 " & Mid(mstrShift_End_Time, 12, 6) & "59.998" & Mid(mstrShift_End_Time, 20, 3)
            End If
            'فى حالة ان الورديه مش أولى و له الأخيره
            If ChkShiftFirstShift.Checked = False And ChkShiftLastShift.Checked = False Then
                gShiftO.Shift_IsFirst = 5
                'فى حالة أن الورديه الأولى
            ElseIf ChkShiftFirstShift.Checked = True Then
                gShiftO.Shift_IsFirst = 1
                'فى حالة أن الورديه الأخيره
            ElseIf ChkShiftLastShift.Checked = True Then
                gShiftO.Shift_IsFirst = 10
            End If


            Dim mstrShiftName As String = TxtShiftName.Text
            gShiftO.Name = TxtShiftName.Text
            gShiftO.Save()

            gAdo.CtrlItemsLoad("Shift_ID", "Shift_Name", "tblShift", LstShift, "", "Shift_IsDeleted='false' ORDER BY Shift_Name")
            LstShift.SelectedIndex = LstShift.FindString(mstrShiftName)
            BtnShiftAddNew.Enabled = True
            BtnShiftSave.Enabled = False
            MsgBox("لقد تم الحفظ بنجاح")
        Else
            MessageBox.Show(gMsg, "البيانات غير مكتمله", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub

    Private Sub BtnShiftDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnShiftDelete.Click
        If LstShift.Items.Count = 0 Then
            Exit Sub
        End If
        gAdo.CmdExec("Update [tblShift] Set Shift_IsDeleted='True' Where Shift_ID= " & gShiftO.ID)
        gAdo.CtrlItemsLoad("Shift_ID", "Shift_Name", "tblShift", LstShift, "", "Shift_IsDeleted='false' ORDER BY Shift_Name")

    End Sub

    Private Sub TxtShiftSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtShiftSearch.TextChanged
        LstShift.SelectedIndex = LstShift.FindString(TxtShiftSearch.Text)
    End Sub

    Private Sub ChkShiftFirstShift_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkShiftFirstShift.CheckedChanged
        If ChkShiftFirstShift.Checked = True Then
            ChkShiftLastShift.Checked = False
        Else

        End If
    End Sub

    Private Sub ChkShiftLastShift_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkShiftLastShift.CheckedChanged
        If ChkShiftLastShift.Checked = True Then
            ChkShiftFirstShift.Checked = False
        Else

        End If
    End Sub
#End Region

#Region "Slip"

    Private Sub LstSlip_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LstSlip.SelectedIndexChanged
        Try
            If Val(Me.LstSlip.SelectedValue) = 0 Then
                Exit Sub
            End If
            gSlipo.ID = LstSlip.SelectedValue
            gSlipo.Refresh()
            TxtSlipName.Text = gSlipo.Name
            TxtSlipMinRange.Text = gSlipo.Slip_MinRange
            TxtSlipMaxRange.Text = gSlipo.Slip_MaxRange
            TxtSlipRate.Text = gSlipo.Slip_Rate

            BtnSlipAddNew.Enabled = True
            BtnSlipSave.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnSlipAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSlipAddNew.Click
        gSlipo.ID = 0
        TxtSlipName.Clear()
        TxtSlipMinRange.Clear()
        TxtSlipMaxRange.Clear()
        TxtSlipRate.Clear()
        TxtSlipName.Select()
        BtnSlipAddNew.Enabled = False
        BtnSlipSave.Enabled = True
    End Sub

    Private Sub BtnSlipSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSlipSave.Click
        'If TxtSlipMinRange.Text = "" Or Val(TxtSlipMinRange.Text) = 0 Then
        '    TxtSlipMinRange.Text = TxtSlipRate.Text
        'ElseIf TxtSlipRate.Text = "" Or Val(TxtSlipRate.Text) = 0 Then
        '    TxtSlipRate.Text = TxtSlipMinRange.Text

        'End If

        If IsErrorMsg(TxtSlipName, TxtSlipRate) = False Then

            Dim mstrSlipName As String = TxtSlipName.Text
            gSlipo.Name = TxtSlipName.Text
            gSlipo.Slip_MinRange = Val(TxtSlipMinRange.Text)
            gSlipo.Slip_MaxRange = Val(TxtSlipMaxRange.Text)
            gSlipo.Slip_Rate = Val(TxtSlipRate.Text)
            gSlipo.Save()
            gAdo.CtrlItemsLoad("Slip_ID", "Slip_Name", "tblSlip", LstSlip, "", "Slip_IsDeleted='false' ORDER BY Slip_Name")
            LstSlip.SelectedIndex = LstSlip.FindString(mstrSlipName)
            BtnSlipAddNew.Enabled = True
            'BtnSlipSave.Enabled = False
            MsgBox("لقد تم الحفظ بنجاح")
        Else
            MessageBox.Show(gMsg, "البيانات غير مكتمله", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub

    Private Sub BtnSlipDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSlipDelete.Click

        Try
            gAdo.CmdExec("update tblSlip set Slip_IsDeleted='True' where Slip_ID=" & gSlipo.ID)
            gAdo.CtrlItemsLoad("Slip_ID", "Slip_Name", "tblSlip", LstSlip, "", "Slip_IsDeleted='false' ORDER BY Slip_Name")
        Catch ex As Exception
        End Try

    End Sub

    Private Sub TxtSlipSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSlipSearch.TextChanged
        LstSlip.SelectedIndex = LstSlip.FindString(TxtSlipSearch.Text)
    End Sub

#End Region

#Region "Validate"
    Private Sub TxtCompanyTelephone_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtCompanyTelephone.KeyPress _
    , TxtIssueToTelephone.KeyPress, TxtIssueToMobile.KeyPress, TxtIssueToFax.KeyPress
        Dim M As Boolean = Char.IsNumber(e.KeyChar) Or Char.IsControl(e.KeyChar)
        If Not M = True Then
            e.Handled = True
        End If
    End Sub

    Private Sub TxtSlipMinRange_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtSlipMinRange.KeyPress _
  , TxtSlipMaxRange.KeyPress, TxtSlipRate.KeyPress
        Dim M As Boolean = Char.IsNumber(e.KeyChar) Or Char.IsControl(e.KeyChar) Or Char.IsPunctuation(e.KeyChar)

        If Not M = True Then
            e.Handled = True
        End If
    End Sub
#End Region

    Private Sub Label42_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label42.Click
        Try
            tcAddNew.SelectedTab = tbCity

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label42_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label42.MouseHover
        Try
            Label42.Cursor = Cursors.Hand
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label44_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label44.MouseHover
        Label44.Cursor = Cursors.Hand
    End Sub

    Private Sub Label44_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label44.Click
        Try
            tcAddNew.SelectedTab = tbCity

        Catch ex As Exception

        End Try
    End Sub

    Private Sub CboPortType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboPortType.SelectedIndexChanged
        Try
            If CboPortType.SelectedIndex = 0 Then
                lblScal_Ip.Visible = False
                lblScalePortNo.Visible = False
                TxtScaleIp.Visible = False
                TxtScalePortNo.Visible = False
                pnlIP.Visible = False
            Else
                lblScal_Ip.Visible = True
                lblScalePortNo.Visible = True
                TxtScaleIp.Visible = True
                TxtScalePortNo.Visible = True
                pnlIP.Visible = True
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub txtIP4_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
    txtIP4.KeyPress, txtIP1.KeyPress, txtIP3.KeyPress, txtIP2.KeyPress
        Dim M As Boolean = Char.IsNumber(e.KeyChar) Or Char.IsControl(e.KeyChar)
        If Not M = True Then
            e.Handled = True
        End If
    End Sub
    Public Sub Clear()

        txtIP1.Text = ""
        txtIP1.Text = ""
        txtIP1.Text = ""
        txtIP1.Text = ""
        TxtScalePortNo.Text = ""
        CboPortType.SelectedIndex = 0

    End Sub

    Private Sub btnCarAddNew_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
    btnCarAddNew.MouseEnter, BtnCarDelete.MouseEnter, btnCarSave.MouseEnter, BtnCityAddNew.MouseEnter _
    , BtnCityDelete.MouseEnter, BtnCitySave.MouseEnter, BtnCompanyDeletePic.MouseEnter, BtnCompanyLoadPic.MouseEnter _
    , BtnCompanySave.MouseEnter, BtnDealerAddNew.MouseEnter, BtnDealerDelete.MouseEnter, BtnDealerSave.MouseEnter _
    , BtnDriverAddNew.MouseEnter, BtnDriverDelete.MouseEnter, BtnDriverSave.MouseEnter, BtnGoodsAddNew.MouseEnter, BtnGoodsDelete.MouseEnter _
    , BtnGoodsSave.MouseEnter, BtnIssueToAddNew.MouseEnter, BtnIssueToDelete.MouseEnter, BtnIssueToSave.MouseEnter, BtnScaleAddNew.MouseEnter, BtnScaleDelete.MouseEnter _
    , BtnScaleSave.MouseEnter, BtnShiftAddNew.MouseEnter, BtnShiftDelete.MouseEnter, BtnShiftSave.MouseEnter, BtnSlipAddNew.MouseEnter _
    , BtnSlipDelete.MouseEnter, BtnSlipSave.MouseEnter, BtnTruckAddNew.MouseEnter, BtnTruckDelete.MouseEnter, BtnTruckSave.MouseEnter _
    , BtnUserAddNew.MouseEnter, BtnUserDelete.MouseEnter, BtnUserDeletePic.MouseEnter, BtnUserLoadPic.MouseEnter, BtnUserPermissionSave.MouseEnter _
    , BtnUserSave.MouseEnter

        sender.BackColor = Color.SkyBlue

    End Sub

    Private Sub btnCarAddNew_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
    btnCarAddNew.MouseLeave, BtnCarDelete.MouseLeave, btnCarSave.MouseLeave, BtnCityAddNew.MouseLeave _
    , BtnCityDelete.MouseLeave, BtnCitySave.MouseLeave, BtnCompanyDeletePic.MouseLeave, BtnCompanyLoadPic.MouseLeave _
    , BtnCompanySave.MouseLeave, BtnDealerAddNew.MouseLeave, BtnDealerDelete.MouseLeave, BtnDealerSave.MouseLeave _
    , BtnDriverAddNew.MouseLeave, BtnDriverDelete.MouseLeave, BtnDriverSave.MouseLeave, BtnGoodsAddNew.MouseLeave, BtnGoodsDelete.MouseLeave _
    , BtnGoodsSave.MouseLeave, BtnIssueToAddNew.MouseLeave, BtnIssueToDelete.MouseLeave, BtnIssueToSave.MouseLeave, BtnScaleAddNew.MouseLeave, BtnScaleDelete.MouseLeave _
    , BtnScaleSave.MouseLeave, BtnShiftAddNew.MouseLeave, BtnShiftDelete.MouseLeave, BtnShiftSave.MouseLeave, BtnSlipAddNew.MouseLeave _
    , BtnSlipDelete.MouseLeave, BtnSlipSave.MouseLeave, BtnTruckAddNew.MouseLeave, BtnTruckDelete.MouseLeave, BtnTruckSave.MouseLeave _
    , BtnUserAddNew.MouseLeave, BtnUserDelete.MouseLeave, BtnUserDeletePic.MouseLeave, BtnUserLoadPic.MouseLeave, BtnUserPermissionSave.MouseLeave _
    , BtnUserSave.MouseLeave

        sender.BackColor = Color.Gainsboro

    End Sub

End Class