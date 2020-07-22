Imports System.IO
Imports System.Data.OleDb
Imports System.Xml
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
        cboContDetGoodID.Text = ""
        ' when he entter the program for the first time enter directly to  User tab
        Label169.Text = " الكميات المتبقيه قبل تاريخ اليوم " & Date.Now.Date.ToShortDateString()
        Dim UserCount As Integer
        UserCount = gAdo.CmdExecScalar("Select count (User_ID) from TblUser")
        If UserCount = 1 Then
            ContAddNew.SelectedTab = tbUser
        Else
            'Select Tab According To Request Flag
            ContAddNew.SelectedTab = tbCar
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

        '' when he entter the program for the first time enter directly to  User tab
        'Dim UserCount As Integer
        'UserCount = gAdo.CmdExecScalar("Select count (User_ID) from TblUser")
        'If UserCount = 1 Then
        '    tcAddNew.SelectedTab = tbUser
        'Else
        '    'Select Tab According To Request Flag
        '    tcAddNew.SelectedTab = tbCar
        'End If

        'FrmRefresh()
        ''To Allow Picture Even Drop&Drag
        'PicCompany.AllowDrop = True
        ''To Get Company Data in tabPages TblCompany
        'GetCompanyData()
        '' here chech if there are no user has been inntialized
        'Dim UserCount2 As Integer
        'UserCount2 = gAdo.CmdExecScalar("Select count (User_ID) from TblUser")
        '' If UserCount2 = 1 Then
        ''GoTo NextSub
        ''End If
        ''To See User Permission for Hide and Show TapPages
        '' Permission_TabPages()
        ''NextSub:
        ''To Clear Control Data
        ''ClearControls(sender, e)

        ' Dim ctr As Control = Me.Controls("PnlDriver")
        ' tcAddNew.Controls.Remove(TypeOf ctr Is Panel)


        cboProtype.SelectedIndex = -1
        cboGoodtype.SelectedIndex = -1


    End Sub

   

    Sub ClearControls(ByVal sender As System.Object, ByVal e As System.EventArgs)

        btnToEmailADD_Click(Nothing, Nothing)
        btnAddEmail_Click(Nothing, Nothing)
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
        '' Elsaman 15/12/2019
        '' To Clear Controls in form Daily Quota
        'btnNewDailyQuota_Click(sender, e)
        btnAddNewProtype_Click(sender, e)
        btnAddNewGoodtype_Click(sender, e)
        QbtnNew_Click(sender, e)

    End Sub

    Sub Permission_TabPages()
        For i = ContAddNew.TabPages.Count - 1 To 0 Step -1
            If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=" & (i + 1).ToString() & " and User_ID= " & gUserID) = False Then
                Try
                    ContAddNew.Controls.RemoveAt(i)
                Catch ex As Exception

                End Try

            End If
        Next
        ''بيانات العقود
        'If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=16 and User_ID= " & gUserID) = False Then
        '    'Dim count As Integer
        '    'count = ContAddNew.Controls.Count
        '    ContAddNew.Controls.RemoveAt(14)
        'End If
        ''بيانات الكوتة اليومية
        'If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=17 and User_ID= " & gUserID) = False Then
        '    'Dim count As Integer
        '    'count = ContAddNew.Controls.Count
        '    ContAddNew.Controls.RemoveAt(13)
        'End If

        ''الشرائح
        'If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=13 and User_ID= " & gUserID) = False Then
        '    ContAddNew.Controls.RemoveAt(12)
        'End If
        ''الورادى
        'If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=12 and User_ID= " & gUserID) = False Then
        '    ContAddNew.Controls.RemoveAt(11)
        'End If
        ''صلاحيات المستخدمين
        'If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=11 and User_ID= " & gUserID) = False Then
        '    ContAddNew.Controls.RemoveAt(10)
        'Else
        '    'To Set GrdUserPermission Columns
        '    gMethods.UserPermissionGrid(GrdUsePermission)
        'End If
        ''بيانات المستخدمين
        'If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=10 and User_ID= " & gUserID) = False Then
        '    ContAddNew.Controls.RemoveAt(9)
        'End If
        ''بيانات الموازين
        ''If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=9 and User_ID= " & gUserID) = False Then

        'ContAddNew.Controls.RemoveAt(8)
        ''End If
        ''المرسل إليه
        'If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=8 and User_ID= " & gUserID) = False Then
        '    ContAddNew.Controls.RemoveAt(7)
        'End If
        ''بيانات الشركه
        'If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=7 and User_ID= " & gUserID) = False Then
        '    ContAddNew.Controls.RemoveAt(6)
        'End If
        ''أنواع الحموله
        'If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=6 and User_ID= " & gUserID) = False Then
        '    ContAddNew.Controls.RemoveAt(5)
        'End If
        ''بيانات العملاء
        'If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=5 and User_ID= " & gUserID) = False Then
        '    ContAddNew.Controls.RemoveAt(4)
        'End If
        ''بيانات السائقين
        'If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=4 and User_ID= " & gUserID) = False Then
        '    ContAddNew.Controls.RemoveAt(3)
        'End If
        ''المدن
        'If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=3 and User_ID= " & gUserID) = False Then
        '    ContAddNew.Controls.RemoveAt(2)
        'End If
        ''بيانات المقطورات
        'If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=2 and User_ID= " & gUserID) = False Then
        '    ContAddNew.Controls.RemoveAt(1)
        'End If
        ''بيانات السيارات
        'If gAdo.CmdExecScalar("select Allow from tblUserPermission where Permission_ID=1 and User_ID= " & gUserID) = False Then
        '    ContAddNew.Controls.RemoveAt(0)
        'End If




    End Sub

    Private Sub tcAddNew_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ContAddNew.MouseClick
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
                Case txtProtypeName.Name
                    If txtProtypeName.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك أدخل نوع البروتين "
                        Exit For
                    End If
                Case txtGoodtypeName.Name
                    If txtGoodtypeName.Text <> "" Then
                        gMsg = ""
                    Else
                        gMsg = "من فضلك أدخل نوع الصنف "
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

    Private Sub FrmRefresh()
        'Code to Load Controls [Cars - Cities - Dealers - Trucks] From Database
        'lstCars.DataSource = Nothing
        gAdo.CtrlItemsLoad("ID", "Email", "tblFromEmail", lstEmails, "", "")
        gAdo.CtrlItemsLoad("ID", "Email", "tblEmails", lstToEmails, "", "")
        gAdo.CtrlItemsLoad("Car_ID", "CarBoard_No", "tblCar Order by CarBoard_No", lstCars)
        'gAdo.CtrlItemsLoad("RoleID", "RoleName", "tblRoles Order by RoleName", cboRoles)
        gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity  where City_ID>1  ORDER BY City_name", cboCarCity)
        gAdo.CtrlItemsLoad("Country_ID", "Country_Name", "tblCountry where Country_ID>1  ORDER BY Country_Name", LstCountry)
        gAdo.CtrlItemsLoad("Country_ID", "Country_Name", "tblCountry   ORDER BY Country_name", cboCountry)
        gAdo.CtrlItemsLoad("Msg_ID", "Message", "tblMessages WHERE Msg_ID>1 ORDER BY Msg_ID", lstMessages)
        gAdo.CtrlItemsLoad("Port_CD", "Port_Name", "tblPorts", lstPorts)
        ' In City TO Fill LstCity
        gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity where City_ID>1  ORDER BY City_Name", LstCity)

        ' In Driver To Fill LstDriver
        gAdo.CtrlItemsLoad("Driver_ID", "Driver_Name", "TblDriver WHERE Driver_ID>1 ORDER BY Driver_name", LstDrivers)

        ' In Car Info To Fill LstCarInfo
        gAdo.CtrlItemsLoad("Car_Info_ID", "TruckBoard_No", "TblCarInfo where Car_Info_ID>1 ORDER BY TruckBoard_No", LstTruck)
        gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity where City_ID>1 ORDER BY City_Name", CboTruckCity)

        'gAdo.CtrlItemsLoad("RoleID", "RoleName", "tblRoles  ORDER BY RoleName", cboRoles)

        ' In Goods Fill LstGoods
        gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood where type in (2,3) or Good_ID=1 ORDER BY Good_Name desc", LstGoods)
        '(Good_ID like '07'+'%' or  good_ID =1 ) and Good_ID in ('0705','0706','0711','0712','0713','0714')

        'gAdo.CtrlItemsLoad("RoleID", "RoleName", "tblRoles ORDER BY RoleName Asc", lstGroups)
        ' In User To Fill LstUser
        gUserO.ID = gUserID
        gUserO.Refresh()
        If gUserO.UserType = "a" Or gUserO.UserType = "m" Then
            ' Admin User
            gAdo.CtrlItemsLoad("User_ID", "User_NickName", "tblUser", LstUser, "", "User_IsDeleted='false' and User_ID<>1  ORDER BY User_Name")
            LstUser.SelectedValue = gUserID
            'BtnUserAddNew
        Else
            ' Normal User
            gAdo.CtrlItemsLoad("User_ID", "User_NickName", "tblUser", LstUser, "", "User_IsDeleted='false' and User_ID<>1 and user_ID=" & gUserID)
            chkCanUpdate.Visible = False
            RdoAdmin.Visible = False
            RdoManager.Visible = False
            RdoWZAN.Checked = True
            BtnUserAddNew.Visible = False
            BtnUserDelete.Visible = False
        End If

        'In IssueTo To Fill LstIssueTO
        gAdo.CtrlItemsLoad("Issue_To_ID", "Issue_To_Name", "tblIssueTo where Issue_To_ID>1 ORDER BY Issue_To_Name", LstIssueTo)
        'In Scale To Fill LstScale
        gAdo.CtrlItemsLoad("Scale_ID", "Scale_Name", "tblScale", LstScale, "", "scale_IsDeleted='false' ORDER BY Scale_Name")
        'In UserPermission To Fill LstUserPermission
        If gUserO.UserType = "a" Then
            gAdo.CtrlItemsLoad("User_ID", "User_NickName", "tblUser", LstUserPermission, "", "User_IsDeleted='false' and User_ID<>1 ORDER BY User_Name")
        End If

        'In Shift To Fill LstShift
        gAdo.CtrlItemsLoad("Shift_ID", "Shift_Name", "tblShift", LstShift, "", "Shift_IsDeleted='false' ORDER BY Shift_Name")
        'In Slip To Fill LstSlip
        gAdo.CtrlItemsLoad("Slip_ID", "Slip_Name", "tblSlip", LstSlip, "", "Slip_IsDeleted='false' ORDER BY Slip_Name")
        gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", "TblDealer WHERE Dealer_ID>2 ORDER BY Dealer_Name", LstDealer)

        ''' Elsaman 10/12/2019 '''
        ''' For Contracts '''
        gAdo.CtrlItemsLoad("ContractID", "ContractNO", "tblContracts ORDER BY ContractID", lstContract)
        gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", "TblDealer WHERE Dealer_ID>2 and Type = 1 ORDER BY Dealer_Name", cboCustomer)
        gAdo.CtrlItemsLoad("CategoryID", "CategoryName", "tblGoodCategories where CategoryID =ParentID   ORDER BY CategoryName", cboParentGood)
        gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood where  Good_ID=1 OR Type in (2,3) " & _
                                    "  ORDER BY Good_Name desc", cboContDetGoodID)
        gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood where  Good_ID=1 OR Type in (2,3) " & _
                                    "  ORDER BY Good_Name desc", cboDQGoodID)



        txtContDetQuantity.Clear()

        Dim intContractID As Integer
        intContractID = gAdo.CmdExecScalar("select isnull(max(ContractID),0) from tblContracts")
        lblContractCode.Text = intContractID + 1
        txtContractNo.Clear()
        dtpContractDate.Value = Date.Now
        If cboCustomer.Items.Count > 0 Then
            cboCustomer.SelectedIndex = 0
        End If
        txtNotes.Clear()
        If cboParentGood.Items.Count > 0 Then
            cboParentGood.SelectedIndex = 0
        End If
        txtAllQuantity.Clear()
        txtQuantityRatio.Clear()
        dtpDeliveryMonth.Value = Date.Now
        chkFinished.Checked = Nothing

        txtContractNo.Select()



        ''' Elsaman 15/12/2019 '''
        ''' For Daily Quota '''
        gAdo.CtrlItemsLoad(" distinct Dealer_ID", "tblDealer.Dealer_Name ", "  tblDebitNote  join tblContracts on tblDebitNote.ContractID  =tblContracts.ContractID join tblDealer on tblContracts .CustomerID =tblDealer .Dealer_ID  ", cboQCustomer)
        cboQCustomer.SelectedIndex = -1

        '' 2- cboQContractNO
        gAdo.CtrlItemsLoad("DebitNoteID", "DebitNote", "tblDebitNote " & _
                           "  join tblContracts on tblDebitNote.ContractID  =tblContracts .ContractID where finish =0 ", cboDebiteNote)
        cboDebiteNote.SelectedIndex = -1
        '' cboQCustomerSearch
        SetDate()
        gAdo.CtrlItemsLoad(" distinct Dealer_ID", "Dealer_Name", "tblDealer join tblContracts on tblContracts.CustomerID = tblDealer.Dealer_ID " & _
                           " join tblDebitNote on tblDebitNote.ContractID = tblContracts.ContractID " & _
                           " join  tblDailyQuota ON tblDebitNote.DebitNoteID=tblDailyQuota.DebitNoteID where  ((DateQuota<=Convert(date,'" & dtpQDateSearch.Value.Date & "') and tblDailyQuota.Finished=0) OR (EndDate=Convert(date,'" & dtpQDateSearch.Value.Date & "') and tblDailyQuota.Finished=1))", cboQCustomerSearch)



        Try
            cboQCustomerSearch.SelectedIndex = 0
            Dim dt As New DataTable

            Dim statment As String

            If cboQCustomerSearch.SelectedValue Is Nothing Then

                dtgGoods.DataSource = dt
            Else
                statment = "set dateformat dmy Select QuotaID, tblDebitNote.DebitNote from tblDailyQuota " & _
                           " Join tblDebitNote on tblDebitNote.DebitNoteID=tblDailyQuota.DebitNoteID " & _
                           " join tblContracts on tblDebitNote .ContractID =tblContracts .ContractID " & _
                           " where tblContracts.Finished = 0 and tblContracts.CustomerID = " & cboQCustomerSearch.SelectedValue & " and ((DateQuota<=Convert(date,'" & dtpQDateSearch.Value.Date & "') and tblDailyQuota.Finished=0) OR (EndDate=Convert(date,'" & dtpQDateSearch.Value.Date & "') and tblDailyQuota.Finished=1))  order by QuotaID"
                dt = GObjDataBaseS.FillDT(statment)
                dtgGoods.DataSource = dt
                dtgGoods.Columns(0).Visible = False
                dtgGoods.Columns(1).HeaderText = "المطالبات"
                dtgGoods.Columns(1).Width = 250
                dtgGoods.Columns(1).ReadOnly = True
            End If



            Try
                Dim ss As String

                statment = "set dateformat dmy Select Top 1 QuotaID  from tblDailyQuota " & _
                           " Join tblDebitNote on tblDebitNote.DebitNoteID=tblDailyQuota.DebitNoteID " & _
                           " join tblContracts on tblDebitNote .ContractID =tblContracts .ContractID " & _
                           " where tblContracts.Finished = 0 and tblContracts.CustomerID = " & cboQCustomerSearch.SelectedValue & " and ((DateQuota<=Convert(date,'" & dtpQDateSearch.Value.Date & "') and tblDailyQuota.Finished=0) OR (EndDate=Convert(date,'" & dtpQDateSearch.Value.Date & "') and tblDailyQuota.Finished=1)) order by QuotaID"
                ss = gAdo.CmdExecScalar(statment)

                If Not Val(dtgGoods.RowCount) = 0 Then
                    Dim intQuotaID As Integer
                    intQuotaID = ss 'dtgGoods.Rows(dtgGoods.CurrentCell.RowIndex).Cells(0).Value.ToString()
                    If (ss > 0) Then
                        gDailyQuota.ID = intQuotaID
                        gDailyQuota.Refresh()


                        gDebiteNoteO.ID = gDailyQuota.DebitNoteID.ID
                        gDebiteNoteO.Refresh()
                        gContractO.ID = gDebiteNoteO.ContractID.ID
                        gContractO.Refresh()
                        gDealerO.ID = gContractO.CustomerID
                        gDealerO.Refresh()
                        cboQCustomer.SelectedValue = gDealerO.ID
                        cboQCustomer.Enabled = False
                        cboDebiteNote.SelectedValue = gDebiteNoteO.ID
                        cboQGoodID.SelectedValue = gDebitNoteQuantityO.GoodID.ID

                        txtQuota.Text = gDailyQuota.Quota
                        txtQNotes.Text = gDailyQuota.Notes
                        dtpQDate.Value = gDailyQuota.DateQuota
                        chkDQFinished.Checked = gDailyQuota.Finished
                        txtOutGoingQty.Text = gDailyQuota.OutGoingQty.ToString()
                        GetRemainDQ(intQuotaID)
                    Else
                        gDailyQuota.ID = 0
                    End If
                    
                End If
            Catch ex As Exception
                gDailyQuota.ID = 0
            End Try

        Catch ex As Exception
            gDailyQuota.ID = 0
        End Try


        ''1- cboQCustomer
        


      
        ' '' 3- cboQGoodID
        'gAdo.CtrlItemsLoad("tblContractDet.GoodID", "Good_Name", "tblGood join tblContractDet on tblContractDet.GoodID = tblGood.Good_ID " & _
        '                   " join tblContracts on tblContracts.ContractID = tblContractDet.ContractID " & _
        '                   " (Good_ID like '07'+'%' or  good_ID =1 ) " & _
        '                   " and Finished = 0 and Good_ID in ('0705','0706','0711','0712','0713','0714') ORDER BY Good_Name desc", cboQGoodID)
        'cboQGoodID.SelectedIndex = -1

        '' Eng Ahmad Fawzy 06/02/2020
        '' for Debit Note 
        gAdo.CtrlItemsLoad("ContractID", "ContractNO", "tblContracts where finished=0", cmbContractID)
        cmbContractID.SelectedIndex = -1

        gAdo.CtrlItemsLoad("DebitNoteID", "DebitNote", "tblDebitNote where finish=0 ORDER BY DebitNoteID", lstDebitNote)


        gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood where  (type in (2,3) or  good_ID =1 ) ORDER BY Good_Name desc", cmbDebitNoteGoodID)
        cmbDebitNoteGoodID.SelectedIndex = -1

        '' Elsaman 9/3/2020
        '' for cboQCustomerSearch in ( شاشة الكوتة اليومية للعملاء )
        'gAdo.CtrlItemsLoad(" distinct Dealer_ID", "Dealer_Name", "tblDealer join tblContracts on tblContracts.CustomerID = tblDealer.Dealer_ID " & _
        '                  " join tblDebitNote on tblDebitNote.ContractID = tblContracts.ContractID " & _
        '                  " join  tblDailyQuota ON tblDebitNote.DebitNoteID=tblDailyQuota.DebitNoteID where  DateQuota=Convert(date,'" & dtpQDateSearch.Value.Date & "')", cboQCustomerSearch)
        'cboQCustomerSearch.SelectedIndex = -1

        gAdo.CtrlItemsLoad("ProtypeID", "ProtypeName", "tblProtype ORDER BY ProtypeName", LstProtype)
        gAdo.CtrlItemsLoad("GoodtypeID", "GoodtypeName", "tblGoodtype ORDER BY GoodtypeName", lstGoodtype)

        gAdo.CtrlItemsLoad("ProtypeID", "ProtypeName", "tblProtype ORDER BY ProtypeName", cboProtype)
        gAdo.CtrlItemsLoad("GoodtypeID", "GoodtypeName", "tblGoodtype ORDER BY GoodtypeName", cboGoodtype)


        gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", "TblDealer WHERE Dealer_ID>2 and Type = 1 ORDER BY Dealer_Name", CustomerID)


        gAdo.CtrlItemsLoad(" distinct Dealer_ID", "Dealer_Name", "tblDealer join tblDailyQuotaWithOutContract on tblDailyQuotaWithOutContract.CustomerID = tblDealer.Dealer_ID " & _
                           " where  ((QuotaDate<=Convert(date,'" & dtpDQDateSearch.Value.Date & "') and finish=0) OR (EndDate=Convert(date,'" & dtpDQDateSearch.Value.Date & "') and finish=1))", CboDQCustomers)
        gAdo.CtrlItemsLoad("[DailyQuotaID]", "[DailyQuotaID]", "[tblDailyQuotaWithOutContract] where CustomerID=" & CboDQCustomers.SelectedValue & " and ((QuotaDate<=Convert(date,'" & dtpDQDateSearch.Value.Date & "') and finish=0) OR (EndDate=Convert(date,'" & dtpDQDateSearch.Value.Date & "') and finish=1)) ORDER BY QuotaDate", lstDQuta)

        dtpDayDate.Text = Date.Now.Date
        gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", "TblDealer where Type in (1,3) ORDER BY Dealer_Name", cboCustID)

    End Sub

    Function SetDate()
        Dim ShiftStartHour As Integer
        ShiftStartHour = gAdo.CmdExecScalar("Set DateFormat dmy Select Datepart(Hour,convert(Datetime,S.Shift_Start_Time,103)) From tblShift S where Shift_IsFirst=1")
        Dim CurDate As Date
        CurDate = Date.Now.Date
        If (Now.Hour >= 0 And Now.Hour < ShiftStartHour) Then
            CurDate = Date.Now.AddDays(-1)
        End If

        dtpQDateSearch.Value = CurDate
        dtpDQDateSearch.Value = CurDate

    End Function
#End Region

#Region "Daily Quota"

    Private Sub cboQCustomer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboQCustomer.SelectedIndexChanged
        Try

            If cboQCustomer.SelectedIndex = -1 Then
            Else
                'gAdo.CtrlItemsLoad("tblContracts.ContractID", "ContractNO", "tblContracts " & _
                '                   " join tblDealer on tblDealer.Dealer_ID = tblContracts.CustomerID where Finished = 0 and Dealer_ID = " & cboQCustomer.SelectedValue & " ", cboDebiteNote)
                gAdo.CtrlItemsLoad("DebitNoteID", "DebitNote", "tblDebitNote " & _
                                   "join tblContracts on tblDebitNote.ContractID =tblContracts.ContractID  where Finish = 0 and CustomerID = " & cboQCustomer.SelectedValue & " ", cboDebiteNote)
                'cboQContractNO.SelectedIndex = -1
                'cboQGoodID.SelectedIndex = -1

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboQContractNO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDebiteNote.SelectedIndexChanged
        Try

            If cboDebiteNote.SelectedValue > 0 Then

                '    gAdo.CtrlItemsLoad("Dealer_ID", "tblDealer.Dealer_Name", "tblDebitNote  join tblContracts on tblDebitNote.ContractID  =tblContracts .ContractID join tblDealer on tblContracts .CustomerID =tblDealer .Dealer_ID " & _
                '                       " where DebitNoteID =" & cboDebiteNote.SelectedValue, cboQCustomer)

                gAdo.CtrlItemsLoad("CD.GoodID", "G.Good_Name", "tblContractDet CD Left Join tblGood G on(CD.GoodID=G.Good_ID) Left join tblContracts C on(CD.ContractID=C.ContractID) Left Join tblDebitNote DN on(C.ContractID=DN.ContractID) where Dn.DebitNoteID='" & cboDebiteNote.SelectedValue.ToString() & "'", lstShowGoods)

                Dim Motalba As Decimal = gAdo.CmdExecScalar("set DateFormat dmy select isnull(DN.TotalQuantity-OutgoingQuantity,0) From tblDebitNote DN where DN.DebitNoteID=" & cboDebiteNote.SelectedValue.ToString())
                Dim OQTY As String = gAdo.CmdExecScalar("set DateFormat dmy select isnull(SUM(T.Quantity),0) From tblTransQuota T join tblTransactions TA on(T.TransID=TA.Trans_ID) join tblDailyQuota DQ on(T.QuotaID=DQ.QuotaID) Join tblDebitNote DN on DQ.DebitNoteID=DN.DebitNoteID where convert(datetime,Out_Date,103)<'" & gMethods.GetShifName_And_SearchDate(True)(0) & "' and DN.DebitNoteID=" & cboDebiteNote.SelectedValue.ToString())
                Dim OUTQ As Decimal = 0
                OUTQ = Decimal.Parse(If(OQTY = "", "0", OQTY)) / 1000
                Motalba = Motalba - OUTQ
                lblRemainder.Text = Motalba.ToString()

                'gAdo.CtrlItemsLoad("GoodID", "Good_Name", "tblDebitNoteQuantity join tblGood on tblDebitNoteQuantity .GoodID =tblGood .Good_ID  where DebitNoteID = " & cboDebiteNote.SelectedValue, cboQGoodID)
                'cboQGoodID.SelectedIndex = -1


            End If



        Catch ex As Exception

        End Try
    End Sub
    ''' Elsaman 5/1/2020 '''
    Private Sub cboQGoodID_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboQGoodID.SelectedIndexChanged
        Try
            'If cboQGoodID.SelectedIndex <= 0 Then
            If cboQGoodID.SelectedValue > 0 And cboDebiteNote.SelectedValue > 0 Then
                ' Don't Do Any Thing
                Dim WhereCondition As String = ""
                WhereCondition = " and TR_TY =2 and  Out_Date is not null and  CONVERT(datetime, Out_Date) < CONVERT(date,'" & dtpQDate.Value & "',103)"
                WhereCondition = " and TR_TY=2 and  Out_Date is not null and  CONVERT(date, Out_Date) between '" & gMethods.GetShifName_And_SearchDate(True)(0) & "' and '" & gMethods.GetShifName_And_SearchDate(True)(1) & "'"
                Dim TotalWeightTrans As Decimal
                TotalWeightTrans = gAdo.CmdExecScalar(" set dateformat dmy select isnull(sum(Second_Weight-First_Weigth),0) as TotalWeightTrans from tblTransactions TA " & _
                                   " join tblTransQuota TQ on TQ.TransID = TA.Trans_ID join tblDailyQuota DQ on DQ.QuotaID = TQ.QuotaID " & _
                                   " join tblDebitNoteQuantity DebitNoteQuantity on DebitNoteQuantity.DebitNoteQuantityID = DQ.DebitNoteQuantityID " & _
                                   " join tblDebitNote DebitNote on DebitNote.DebitNoteID = DebitNoteQuantity.DebitNoteID where  DebitNoteQuantity.GoodID = " & cboQGoodID.SelectedValue & " and DebitNote.DebitNoteID = " & cboDebiteNote.SelectedValue & _
                                   WhereCondition)

                'TotalWeightTrans = gAdo.CmdExecScalar(" set dateformat dmy select isnull(sum(Second_Weight-First_Weigth),0) as TotalWeightTrans from tblTransactions TA " & _
                '                   " join tblTransQuota TQ on TQ.TransID = TA.Trans_ID join tblDailyQuota DQ on DQ.QuotaID = TQ.QuotaID " & _
                '                   " join tblContractDet ConDet on ConDet.ContractDetID = DQ.ContractDetID " & _
                '                   " join tblContracts Con on Con.ContractID = ConDet.ContractID where  ConDet.GoodID = " & cboQGoodID.SelectedValue & " and Con.ContractID = " & cboDebiteNote.SelectedValue & _
                '                   WhereCondition)
                Dim TotalDebitNoteQuantity As Decimal
                TotalDebitNoteQuantity = gAdo.CmdExecScalar("select TotalQuantity  from tblDebitNoteQuantity where  GoodID ='" & cboQGoodID.SelectedValue & "' and  DebitNoteID =" & cboDebiteNote.SelectedValue)
                'GoodContractQuantity = gAdo.CmdExecScalar(" select isnull(sum(Quantity),0) from tblContractDet " & _
                '                                          " join tblContracts on tblContracts.ContractID = tblContractDet.ContractID " & _
                '                                         " where GoodID = " & cboQGoodID.SelectedValue & "  and tblContracts.ContractID = " & cboDebiteNote.SelectedValue)
                Dim OutgoingDebitNoteQuantity As Decimal
                OutgoingDebitNoteQuantity = gAdo.CmdExecScalar("select OutGoingQuantity  from tblDebitNoteQuantity where  GoodID ='" & cboQGoodID.SelectedValue & "' and  DebitNoteID =" & cboDebiteNote.SelectedValue)
                'OutGoingQuantity = gAdo.CmdExecScalar(" select isnull(sum(OutgoingQuantity),0) from tblContractDet " & _
                '                                          " join tblContracts on tblContracts.ContractID = tblContractDet.ContractID " & _
                '                                          " where GoodID = " & cboQGoodID.SelectedValue & "  and tblContracts.ContractID = " & cboDebiteNote.SelectedValue)

                'lblRemainder.Text = CStr(GoodContractQuantity - TotalWeightTrans - OutGoingQuantity)

                'lblRemainder.Text = CStr(TotalDebitNoteQuantity - TotalWeightTrans - OutgoingDebitNoteQuantity)
            Else

                'lblRemainder.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub setQDate()
        Dim ShiftStartHour As Integer
        ShiftStartHour = gAdo.CmdExecScalar("Set DateFormat dmy Select Datepart(Hour,convert(Datetime,S.Shift_Start_Time,103)) From tblShift S where Shift_IsFirst=1")
        Dim CurDate As Date
        CurDate = Date.Now.Date
        If (Now.Hour >= 0 And Now.Hour < ShiftStartHour) Then
            CurDate = Date.Now.AddDays(-1)
        End If
        dtpQDate.Value = CurDate
        dtpQuotaDate.Value = CurDate
    End Sub

    Dim EnableCustomer As Boolean = False
    Private Sub btnNewDailyQuota_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewDailyQuota.Click
        Try
            gDailyQuota.ID = 0
            chkDQFinished.Checked = False
            txtOutGoingQty.Clear()
            cboQCustomer.SelectedIndex = -1

            cboDebiteNote.SelectedIndex = -1


            cboQGoodID.SelectedIndex = -1
            setQDate()
            txtQuota.Clear()
            txtQNotes.Clear()

            ''For Refrsh dtgGoods Grid
            'Try
            '    Dim dt As New DataTable

            '    Dim statment As String
            '    If cboQCustomerSearch.SelectedValue Is Nothing Then
            '        ' dt is empty
            '        dtgGoods.DataSource = dt
            '    Else
            '        statment = "set dateformat dmy Select QuotaID, tblDebitNote.DebitNote from tblDailyQuota " & _
            '                   " Join tblDebitNote on tblDebitNote.DebitNoteID=tblDailyQuota.DebitNoteID " & _
            '                   " join tblContracts on tblDebitNote .ContractID =tblContracts .ContractID " & _
            '                   " where tblContracts.Finished = 0 and tblContracts.CustomerID = " & cboQCustomerSearch.SelectedValue & " and DateQuota = '" & dtpQDateSearch.Value.Date & "'"
            '        dt = GObjDataBaseS.FillDT(statment)
            '        dtgGoods.DataSource = dt
            '        dtgGoods.Columns(0).Visible = False
            '        dtgGoods.Columns(1).HeaderText = "المطالبات"
            '        dtgGoods.Columns(1).Width = 250
            '        dtgGoods.Columns(1).ReadOnly = True
            '    End If


            '    'dt.Columns.Clear()



            'Catch ex As Exception
            '    MsgBox(ex.Message)
            'End Try

            btnNewDailyQuota.Enabled = True
            btnSaveDailyQuota.Enabled = True
            lblRemainder.Text = ""
            RemainDQ.Text = ""
            lstShowGoods.DataSource = Nothing
            txtOutFromQouta.Text = ""
            RemainDQ.Text = ""
            EnableCustomer = True
            cboQCustomer.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSaveDailyQuota_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveDailyQuota.Click
        Dim msgrslt As MsgBoxResult
        msgrslt = MsgBox("متأكد من الحفظ", MsgBoxStyle.OkCancel)
        If msgrslt = MsgBoxResult.Cancel Then
            Exit Sub
        End If
        Try
            'Dim SQL As String = " Select D.QuotaID From tblDailyQuota D where D.QuotaID<>" + gDailyQuota.ID.ToString() + " and D.DebitNoteID='" + cboDebiteNote.SelectedValue.ToString() + "' and D.DateQuota='" + dtpQDate.Value.ToShortDateString() + "'"
            If txtQuota.Text = "" Then
                MessageBox.Show(gMsg, " يجب ادخال الكوتة اليومية ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
                'ElseIf (gAdo.IFExists(SQL)) Then
                '    MessageBox.Show(gMsg, " لا يمكن تكرار المطالبة فى نفس اليوم ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                '    Exit Sub
            Else
                If (gDailyQuota.ID <> 0) Then
                    Dim SQl As String = "select isnull(d.Quota,0) from tblDailyQuota d where QuotaID=" & gDailyQuota.ID
                    Dim OldQuota As Decimal
                    OldQuota = gAdo.CmdExecScalar(SQl)
                    SQl = "set Dateformat dmy Select isnull(sum((TQ.Quantity)/1000),0) From tblTransQuota TQ where TQ.QuotaID=" & gDailyQuota.ID
                    Dim TransQuota As Decimal
                    TransQuota = gAdo.CmdExecScalar(SQl)
                    Dim CurQuota As Decimal
                    CurQuota = Decimal.Parse(If(txtQuota.Text.Trim() = "", "0", txtQuota.Text.Trim()))
                    If (CurQuota < TransQuota) Then
                        MsgBox(" الكوته يجب ان لا تقل عن " & TransQuota.ToString())
                        txtQuota.Text = OldQuota.ToString()
                        Exit Sub
                    End If
                End If

                'gDailyQuota.ID = txtQuota.Text
                Dim intDebitNoteQuantityID As Integer
                'intDebitNoteQuantityID = gAdo.CmdExecScalar("select ContractDetID from tblContractDet join tblContracts on tblContracts.ContractID = tblContractDet .ContractID " & _
                '                                        " join tblDealer on tblDealer .Dealer_ID = tblContracts .CustomerID join tblGood on tblGood .Good_ID = tblContractDet .GoodID " & _
                '                                        "  where Finished = 0 and tblContracts.ContractID = " & cboDebiteNote.SelectedValue & " and tblDealer .Dealer_ID = " & cboQCustomer.SelectedValue & _
                '                                        " and tblGood .Good_ID = '" & cboQGoodID.SelectedValue & "'")

                'intDebitNoteQuantityID = gAdo.CmdExecScalar("select DebitNoteQuantityID  from    " & _
                '                                       "  tblDebitNoteQuantity   " & _
                '                                        " join tblDebitNote         on tblDebitNoteQuantity .DebitNoteID =tblDebitNote .DebitNoteID  " & _
                '                                         "   join tblContracts on tblDebitNote .ContractID =tblContracts .ContractID  " & _
                '                                       "  where  tblContracts.CustomerID  = " & cboQCustomer.SelectedValue & _
                '                                       " and tblDebitNoteQuantity .GoodID = '" & cboQGoodID.SelectedValue & "'")
                gDailyQuota.DebitNoteID.ID = cboDebiteNote.SelectedValue
                gDailyQuota.Quota = txtQuota.Text
                gDailyQuota.DateQuota = dtpQDate.Value
                gDailyQuota.Notes = txtQNotes.Text
                gDailyQuota.Finished = chkDQFinished.Checked
                gDailyQuota.OutGoingQty = If(txtOutGoingQty.Text.Trim() = "", "0", txtOutGoingQty.Text.Trim())
                gDailyQuota.Save()
                btnNewDailyQuota.Enabled = True
                'btnSaveDailyQuota.Enabled = False


                MsgBox("لقد تم الحفظ بنجاح")
                ''' To Fill lstQuotaGood
                'gAdo.CtrlItemsLoad(" Good_ID ", " Good_Name ", " tblGood join tblContractDet on tblContractDet.GoodID = tblGood.Good_ID " & _
                '           " join tblDailyQuota on tblDailyQuota.ContractDetID = tblContractDet.ContractDetID " & _
                '           " join tblContracts on tblContracts.ContractID = tblContractDet.ContractID " & _
                '           " join tblDealer on tblDealer.Dealer_ID = tblContracts.CustomerID where Finished = 0 ", dtgGoods)

                gAdo.CtrlItemsLoad(" distinct Dealer_ID", "Dealer_Name", "tblDealer join tblContracts on tblContracts.CustomerID = tblDealer.Dealer_ID " & _
                        " join tblDebitNote on tblDebitNote.ContractID = tblContracts.ContractID " & _
                        " join  tblDailyQuota ON tblDebitNote.DebitNoteID=tblDailyQuota.DebitNoteID where  ((DateQuota<=Convert(date,'" & dtpQDateSearch.Value.Date & "') and tblDailyQuota.Finished=0) OR (EndDate=Convert(date,'" & dtpQDateSearch.Value.Date & "') and tblDailyQuota.Finished=1))", cboQCustomerSearch)
            End If

        Catch ex As Exception
            MsgBox(" حدث خطاء أثناء الحفظ ")
        End Try
    End Sub

    Private Sub btnDeleteDailyQuota_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteDailyQuota.Click
        Try

            Dim msgrslt As MsgBoxResult
            msgrslt = MsgBox("هل ترغب فى حذف هذا العنصر ؟", MsgBoxStyle.OkCancel)
            If msgrslt = MsgBoxResult.Ok Then

                Dim intQuotaID As Integer
                intQuotaID = dtgGoods.Rows(dtgGoods.CurrentCell.RowIndex).Cells(0).Value.ToString()
                'Dim intContractDetID As Integer
                'intContractDetID = gAdo.CmdExecScalar("select ContractDetID from tblContractDet join tblContracts on tblContracts .ContractID = tblContractDet .ContractID " & _
                '                                      " join tblDealer on tblDealer .Dealer_ID = tblContracts .CustomerID join tblGood on tblGood .Good_ID = tblContractDet .GoodID " & _
                '                                      " and Finished = 0 and tblDealer .Dealer_ID = " & cboQCustomer.SelectedValue & " and tblGood .Good_ID = '" & cboQGoodID.SelectedValue & "'")
                'Dim intQuotaID As Integer
                'intQuotaID = gAdo.CmdExecScalar("select QuotaID from tblDailyQuota where ContractDetID = " & intContractDetID)
                If (gAdo.IFExists("Select QuotaID from tblTransQuota where QuotaID=" & intQuotaID.ToString())) Then
                    MsgBox("لا يمكن مسح هذه الكوته لأنها تم التعامل عليها")
                    Exit Sub

                End If


                gDailyQuota.ID = intQuotaID
                gDailyQuota.Remove()
                MsgBox("تم الحذف بنجاح")
                ''Code to refresh lstContract

                btnNewDailyQuota_Click(sender, e)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtpQDateSearch_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpQDateSearch.ValueChanged
        cboQCustomer.SelectedIndex = -1
        cboQCustomer.Text = ""
        cboDebiteNote.SelectedIndex = -1
        cboDebiteNote.Text = ""
        lstShowGoods.DataSource = Nothing
        lblRemainder.Text = ""
        txtQuota.Text = ""
        txtOutGoingQty.Clear()
        gAdo.CtrlItemsLoad(" distinct Dealer_ID", "Dealer_Name", "tblDealer join tblContracts on tblContracts.CustomerID = tblDealer.Dealer_ID " & _
                           " join tblDebitNote on tblDebitNote.ContractID = tblContracts.ContractID " & _
                           " join  tblDailyQuota ON tblDebitNote.DebitNoteID=tblDailyQuota.DebitNoteID where ((DateQuota<=Convert(date,'" & dtpQDateSearch.Value.Date & "') and tblDailyQuota.Finished=0) OR (EndDate=Convert(date,'" & dtpQDateSearch.Value.Date & "') and tblDailyQuota.Finished=1))", cboQCustomerSearch)
        Try
            Dim dt As New DataTable

            Dim statment As String

            If cboQCustomerSearch.SelectedValue Is Nothing Then

                dtgGoods.DataSource = dt
            Else
                statment = "set dateformat dmy Select QuotaID, tblDebitNote.DebitNote from tblDailyQuota " & _
                           " Join tblDebitNote on tblDebitNote.DebitNoteID=tblDailyQuota.DebitNoteID " & _
                           " join tblContracts on tblDebitNote .ContractID =tblContracts .ContractID " & _
                           " where tblContracts.Finished = 0 and tblContracts.CustomerID = " & cboQCustomerSearch.SelectedValue & " and ((DateQuota<=Convert(date,'" & dtpQDateSearch.Value.Date & "') and tblDailyQuota.Finished=0) OR (EndDate=Convert(date,'" & dtpQDateSearch.Value.Date & "') and tblDailyQuota.Finished=1))"
                dt = GObjDataBaseS.FillDT(statment)
                dtgGoods.DataSource = dt
                dtgGoods.Columns(0).Visible = False
                dtgGoods.Columns(1).HeaderText = "المطالبات"
                dtgGoods.Columns(1).Width = 250
                dtgGoods.Columns(1).ReadOnly = True
            End If

            Try
                Dim ss As String
                ss = dtgGoods.Rows(dtgGoods.CurrentCell.RowIndex).Cells(0).Value.ToString()
                If Not Val(dtgGoods.RowCount) = 0 Then
                    Dim intQuotaID As Integer
                    intQuotaID = dtgGoods.Rows(dtgGoods.CurrentCell.RowIndex).Cells(0).Value.ToString()

                    gDailyQuota.ID = intQuotaID
                    gDailyQuota.Refresh()


                    gDebiteNoteO.ID = gDailyQuota.DebitNoteID.ID
                    gDebiteNoteO.Refresh()
                    gContractO.ID = gDebiteNoteO.ContractID.ID
                    gContractO.Refresh()
                    gDealerO.ID = gContractO.CustomerID
                    gDealerO.Refresh()
                    cboQCustomer.SelectedValue = gDealerO.ID
                    cboQCustomer.Enabled = False
                    cboDebiteNote.SelectedValue = gDebiteNoteO.ID
                    cboQGoodID.SelectedValue = gDebitNoteQuantityO.GoodID.ID

                    txtQuota.Text = gDailyQuota.Quota
                    txtQNotes.Text = gDailyQuota.Notes
                    dtpQDate.Value = gDailyQuota.DateQuota
                    chkDQFinished.Checked = gDailyQuota.Finished
                    txtOutGoingQty.Text = gDailyQuota.OutGoingQty
                    GetRemainDQ(intQuotaID)

                End If
            Catch ex As Exception

            End Try





        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub cboQCustomerSearch_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboQCustomerSearch.SelectedIndexChanged

        Try
            Dim dt As New DataTable
            'dtgGoods.Rows(i).Cells("Permission_ID").Value.ToString()

            'gAdo.CtrlItemsLoad(" Good_ID ", " Good_Name ", " tblGood join tblContractDet on tblContractDet.GoodID = tblGood.Good_ID " & _
            '                   " join tblDailyQuota on tblDailyQuota.ContractDetID = tblContractDet.ContractDetID " & _
            '                   " join tblContracts on tblContracts.ContractID = tblContractDet.ContractID " & _
            '                   " join tblDealer on tblDealer.Dealer_ID = tblContracts.CustomerID " & _
            '                   " where Dealer_ID = " & cboQCustomerSearch.SelectedValue & " and DateQuota = '" & dtpQDateSearch.Value.Date & "'" & _
            '                   "", dtgGoods)
            Dim statment As String
            'statment = "set dateformat dmy Select tblContractDet.ContractDetID, Good_ID , Good_Name from tblGood join tblContractDet on tblContractDet.GoodID = tblGood.Good_ID " & _
            '                   " join tblDailyQuota on tblDailyQuota.ContractDetID = tblContractDet.ContractDetID " & _
            '                   " join tblContracts on tblContracts.ContractID = tblContractDet.ContractID " & _
            '                   " join tblDealer on tblDealer.Dealer_ID = tblContracts.CustomerID " & _
            '                   " where Finished = 0 and Dealer_ID = " & cboQCustomerSearch.SelectedValue & " and DateQuota = '" & dtpQDateSearch.Value.Date & "'"
            If cboQCustomerSearch.SelectedValue Is Nothing Then
                ' dt is empty
                dtgGoods.DataSource = dt
            Else
                statment = "set dateformat dmy Select QuotaID, tblDebitNote.DebitNote from tblDailyQuota " & _
                           " Join tblDebitNote on tblDebitNote.DebitNoteID=tblDailyQuota.DebitNoteID " & _
                           " join tblContracts on tblDebitNote .ContractID =tblContracts .ContractID " & _
                           " where tblContracts.Finished = 0 and tblContracts.CustomerID = " & cboQCustomerSearch.SelectedValue & " and ((DateQuota<=Convert(date,'" & dtpQDateSearch.Value.Date & "') and tblDailyQuota.Finished=0) OR (EndDate=Convert(date,'" & dtpQDateSearch.Value.Date & "') and tblDailyQuota.Finished=1))"
                dt = GObjDataBaseS.FillDT(statment)
                dtgGoods.DataSource = dt
                dtgGoods.Columns(0).Visible = False
                dtgGoods.Columns(1).HeaderText = "المطالبات"
                dtgGoods.Columns(1).Width = 250
                dtgGoods.Columns(1).ReadOnly = True
            End If

            Try
                Dim ss As String
                ss = dtgGoods.Rows(dtgGoods.CurrentCell.RowIndex).Cells(0).Value.ToString()
                If Not Val(dtgGoods.RowCount) = 0 Then
                    Dim intQuotaID As Integer
                    intQuotaID = dtgGoods.Rows(dtgGoods.CurrentCell.RowIndex).Cells(0).Value.ToString()

                    gDailyQuota.ID = intQuotaID
                    gDailyQuota.Refresh()


                    gDebiteNoteO.ID = gDailyQuota.DebitNoteID.ID
                    gDebiteNoteO.Refresh()
                    gContractO.ID = gDebiteNoteO.ContractID.ID
                    gContractO.Refresh()
                    gDealerO.ID = gContractO.CustomerID
                    gDealerO.Refresh()
                    cboQCustomer.SelectedValue = gDealerO.ID
                    cboQCustomer.Enabled = False
                    cboDebiteNote.SelectedValue = gDebiteNoteO.ID
                    cboQGoodID.SelectedValue = gDebitNoteQuantityO.GoodID.ID

                    txtQuota.Text = gDailyQuota.Quota
                    txtQNotes.Text = gDailyQuota.Notes
                    dtpQDate.Value = gDailyQuota.DateQuota
                    chkDQFinished.Checked = gDailyQuota.Finished
                    txtOutGoingQty.Text = gDailyQuota.OutGoingQty
                    GetRemainDQ(intQuotaID)

                End If
            Catch ex As Exception

            End Try

            'dt.Columns.Clear()


            
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub lstQuotaGood_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'gAdo.CtrlItemsLoad(" distinct Dealer_ID", "Dealer_Name", "tblDealer join tblContracts on tblContracts.CustomerID = tblDealer.Dealer_ID " & _
        '                   " join tblContractDet on tblContractDet.ContractID = tblContracts.ContractID ", cboQCustomer)

        'gAdo.CtrlItemsLoad("tblContracts.ContractID", "ContractNO", "tblContracts " & _
        '                               " join tblDealer on tblDealer.Dealer_ID = tblContracts.CustomerID where Dealer_ID = " & cboQCustomer.SelectedValue & " ", cboQContractNO)

        'gAdo.CtrlItemsLoad("tblContractDet.GoodID", "Good_Name", "tblGood join tblContractDet on tblContractDet.GoodID = tblGood.Good_ID " & _
        '                  " join tblContracts on tblContracts.ContractID = tblContractDet.ContractID " & _
        '                  " where tblContracts.ContractID = " & cboQContractNO.SelectedValue & " and (Good_ID like '07'+'%' or  good_ID =1 ) " & _
        '                  " and Good_ID in ('0705','0706','0711','0712','0713','0714') ORDER BY Good_Name desc", cboQGoodID)

        'Dim intContractDetID As Integer
        ''intContractDetID = gAdo.CmdExecScalar("select ContractDetID from tblContractDet join tblContracts on tblContracts .ContractID = tblContractDet .ContractID " & _
        ''                                       " join tblDealer on tblDealer .Dealer_ID = tblContracts .CustomerID join tblGood on tblGood .Good_ID = tblContractDet .GoodID " & _
        ''                                       " and tblDealer .Dealer_ID = " & cboQCustomerSearch.SelectedValue & _
        ''                                       " and tblGood .Good_ID = '" & lstQuotaGood.SelectedValue & "' and tblContracts.ContractID = " & cboQContractNO.SelectedValue & " ")
        ''Dim intQuotaID As Integer
        ''intQuotaID = gAdo.CmdExecScalar(" set dateformat dmy select QuotaID from tblDailyQuota " & _
        ''                                " where ContractDetID = " & intContractDetID & " and DateQuota = '" & dtpQDateSearch.Value.Date & "'")
        'gDailyQuota.ID = intQuotaID
        'gDailyQuota.Refresh()
        'gContractDetO.ID = gDailyQuota.ContractDetID
        'gContractDetO.Refresh()
        'gContractO.ID = gContractDetO.ContractID
        'gContractO.Refresh()
        'gDealerO.ID = gContractO.CustomerID
        'gDealerO.Refresh()
        'cboQCustomer.SelectedValue = gDealerO.ID
        'cboQContractNO.SelectedValue = gContractO.ID
        'cboQGoodID.SelectedValue = gContractDetO.GoodID
        'txtQuota.Text = gDailyQuota.Quota
        'txtQNotes.Text = gDailyQuota.Notes
        'dtpQDate.Value = gDailyQuota.DateQuota



        'Try

        '    If Val(dtgGoods.RowCount) = 0 Then
        '        Exit Sub
        '    End If

        '    Dim intContractDetID As Integer
        '    intContractDetID = gAdo.CmdExecScalar(" select ContractDetID from tblContractDet join tblContracts on tblContracts.ContractID = tblContractDet .ContractID " & _
        '                                            " join tblDealer on tblDealer .Dealer_ID = tblContracts .CustomerID join tblGood on tblGood .Good_ID = tblContractDet .GoodID " & _
        '                                            " and tblContracts.ContractID = " & cboQContractNO.SelectedValue & " and tblDealer .Dealer_ID = " & cboQCustomer.SelectedValue & _
        '                                            " and tblGood.Good_ID = '" & dtgGoods. & "'")


        '    'Dim intContractDetID As Integer
        '    'intContractDetID = gAdo.CmdExecScalar("select ContractDetID from tblContractDet join tblContracts on tblContracts .ContractID = tblContractDet .ContractID " & _
        '    '                                       " join tblDealer on tblDealer .Dealer_ID = tblContracts .CustomerID join tblGood on tblGood .Good_ID = tblContractDet .GoodID " & _
        '    '                                       " and tblDealer .Dealer_ID = " & cboQCustomerSearch.SelectedValue & _
        '    '                                       " and tblGood .Good_ID = '" & lstQuotaGood.SelectedValue & "'")
        '    Dim intQuotaID As Integer
        '    intQuotaID = gAdo.CmdExecScalar(" set dateformat dmy select QuotaID from tblDailyQuota " & _
        '                                    " where ContractDetID = " & intContractDetID & " and DateQuota = '" & dtpQDateSearch.Value.Date & "'")
        '    gDailyQuota.ID = intQuotaID
        '    gDailyQuota.Refresh()
        '    gContractDetO.ID = gDailyQuota.ContractDetID
        '    gContractDetO.Refresh()
        '    gContractO.ID = gContractDetO.ContractID
        '    gContractO.Refresh()
        '    gDealerO.ID = gContractO.CustomerID
        '    gDealerO.Refresh()
        '    cboQCustomer.SelectedValue = gDealerO.ID
        '    cboQContractNO.SelectedValue = gContractO.ID
        '    cboQGoodID.SelectedValue = gContractDetO.GoodID
        '    txtQuota.Text = gDailyQuota.Quota
        '    txtQNotes.Text = gDailyQuota.Notes
        '    dtpQDate.Value = gDailyQuota.DateQuota

        'Catch ex As Exception

        'End Try


    End Sub

    '' Eng Ahmed 1/1/2020
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim ss As String
            ss = dtgGoods.Rows(dtgGoods.CurrentCell.RowIndex).Cells(0).Value.ToString()
            If Val(dtgGoods.RowCount) = 0 Then
                Exit Sub
            End If

            Dim intQuotaID As Integer
            intQuotaID = dtgGoods.Rows(dtgGoods.CurrentCell.RowIndex).Cells(0).Value.ToString()
            'intContractDetID = gAdo.CmdExecScalar(" select ContractDetID from tblContractDet join tblContracts on tblContracts.ContractID = tblContractDet .ContractID " & _
            '                                        " join tblDealer on tblDealer .Dealer_ID = tblContracts .CustomerID join tblGood on tblGood .Good_ID = tblContractDet .GoodID " & _
            '                                        " and tblContracts.ContractID = " & cboQContractNO.SelectedValue & " and tblDealer.Dealer_ID = " & cboQCustomer.SelectedValue & _
            '                                        " and tblGood.Good_ID = '" & dtgGoods.Rows(dtgGoods.CurrentCell.RowIndex).Cells(1).Value.ToString & "'")


            'Dim intContractDetID As Integer
            'intContractDetID = gAdo.CmdExecScalar("select ContractDetID from tblContractDet join tblContracts on tblContracts .ContractID = tblContractDet .ContractID " & _
            '                                       " join tblDealer on tblDealer .Dealer_ID = tblContracts .CustomerID join tblGood on tblGood .Good_ID = tblContractDet .GoodID " & _
            '                                       " and tblDealer .Dealer_ID = " & cboQCustomerSearch.SelectedValue & _
            '                                       " and tblGood .Good_ID = '" & lstQuotaGood.SelectedValue & "'")
            'Dim intQuotaID As Integer
            'intQuotaID = gAdo.CmdExecScalar(" set dateformat dmy select QuotaID from tblDailyQuota " & _
            '                                " where DebitNoteQuantityID = " & intDebitNoteQuatityID & " and DateQuota = '" & dtpQDateSearch.Value.Date & "'")
            gDailyQuota.ID = intQuotaID
            gDailyQuota.Refresh()

            'gDebitNoteQuantityO.ID = gDailyQuota.DebitNoteID.ID
            'gDebitNoteQuantityO.Refresh()
            gDebiteNoteO.ID = gDailyQuota.DebitNoteID.ID
            gDebiteNoteO.Refresh()
            gContractO.ID = gDebiteNoteO.ContractID.ID
            gContractO.Refresh()
            gDealerO.ID = gContractO.CustomerID
            gDealerO.Refresh()
            cboQCustomer.SelectedValue = gDealerO.ID
            cboQCustomer.Enabled = False
            cboDebiteNote.SelectedValue = gDebiteNoteO.ID
            cboQGoodID.SelectedValue = gDebitNoteQuantityO.GoodID.ID
            'lblRemainder.Text =
            txtQuota.Text = gDailyQuota.Quota
            txtQNotes.Text = gDailyQuota.Notes
            dtpQDate.Value = gDailyQuota.DateQuota


        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Contracts"
    ''' lstContract '''

    Private Sub lstContract_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstContract.SelectedIndexChanged

        Try
            If Val(Me.lstContract.SelectedValue) = 0 Then
                Exit Sub
            End If
            gContractO.ID = Val(lstContract.SelectedValue)
            gContractO.Refresh()
            lblContractCode.Text = gContractO.ID
            txtContractNo.Text = gContractO.ContractNO
            dtpContractDate.Value = gContractO.DateContract
            'gDealerO.ID = gContractO.CustomerID
            'gDealerO.Refresh()
            cboCustomer.SelectedValue = gContractO.CustomerID
            txtNotes.Text = gContractO.Notes
            'gGoodCategories.ID = gContractO.ParentGood
            'gGoodCategories.Refresh()
            cboParentGood.SelectedValue = gContractO.ParentGood
            txtAllQuantity.Text = gContractO.AllQuantity
            txtQuantityRatio.Text = gContractO.QuantityRatio
            dtpDeliveryMonth.Value = gContractO.DeliveryMonth
            txtOutGoingQuantity.Text = gContractO.OutGoingQuantity

            ''' For Details (tblContractDet) '''
            txtContDetQuantity.Clear()
            gAdo.CtrlItemsLoad("ContractDetID", "Good_Name", "tblContractDet join tblGood on tblGood.Good_ID = tblContractDet.GoodID " & _
                               " where ContractID = " & lstContract.SelectedValue & " ORDER BY ContractID", lstContractDet)
            If lstContractDet.Items.Count = 0 Then

                gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood where  Good_ID=1 OR Type in (2,3) " & _
                                   "  ORDER BY Good_Name desc", cboContDetGoodID)
            End If


            btnNewContract.Enabled = True
            btnSaveContract.Enabled = True
            btnNewContractDet_Click(Nothing, Nothing)

        Catch ex As Exception

        End Try

    End Sub
    ''' lstContractDet '''

    Private Sub lstContractDet_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstContractDet.SelectedValueChanged
        Try
            If Val(Me.lstContractDet.SelectedValue) = 0 Then
                Exit Sub
            End If
            gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood where  Good_ID=1 OR Type in (2,3) " & _
                                 "  ORDER BY Good_Name desc", cboContDetGoodID)
            gContractDetO.ID = Val(lstContractDet.SelectedValue)
            gContractDetO.Refresh()
            'gGoodO.ID = gContractDetO.GoodID
            'gGoodO.Refresh()

            cboContDetGoodID.SelectedValue = gContractDetO.GoodID
            'txtContDetQuantity.Text = gContractDetO.Quantity
            'txtOutGoingQuantity.Text = gContractDetO.OutGoingQuantity

            btnNewContractDet.Enabled = True
            btnSaveContractDet.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnNewContract_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewContract.Click
        gAdo.CtrlItemsLoad("ContractID", "ContractNO", "tblContracts ORDER BY ContractID", lstContract)
        lstContractDet.DataSource = Nothing
        cboContDetGoodID.Text = ""
        gContractO.ID = 0
        Dim intContractID As Integer
        'intContractID = gAdo.CmdExecScalar("select isnull(max(ContractID),0) from tblContracts")
        lblContractCode.Text = ""
        txtContractNo.Clear()
        dtpContractDate.Value = Date.Now
        'If cboCustomer.Items.Count > 0 Then
        '    cboCustomer.SelectedIndex = 0
        'End If
        cboCustomer.Text = ""
        cboParentGood.Text = ""
        txtNotes.Clear()
        'If cboParentGood.Items.Count > 0 Then
        '    cboParentGood.SelectedIndex = 0
        'End If
        txtAllQuantity.Clear()
        txtOutGoingQuantity.Clear()
        txtQuantityRatio.Clear()
        dtpDeliveryMonth.Value = Date.Now
        chkFinished.Checked = Nothing

        txtContractNo.Select()
        btnNewContractDet.Enabled = True
        btnSaveContractDet.Enabled = True

    End Sub
    Function TestVaild() As Boolean
        If txtContractNo.Text = "" Then
            MessageBox.Show(gMsg, "يجب ادخال رقم العقد", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        If cboCustomer.Text = "" Then
            MessageBox.Show(gMsg, "يجب ادخال إسم العميل", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        If cboParentGood.Text = "" Then
            MessageBox.Show(gMsg, "يجب ادخال النوع", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        If Decimal.Parse(If(txtAllQuantity.Text.Trim() = "", "0", txtAllQuantity.Text.Trim())) = 0 Then
            MessageBox.Show(gMsg, "يجب ادخال إجمالى الكمية", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        If Decimal.Parse(If(txtQuantityRatio.Text.Trim() = "", "0", txtQuantityRatio.Text.Trim())) = 0 Then
            MessageBox.Show(gMsg, "يجب ادخال النسبه المسموح بها", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function

    Private Sub btnSaveContract_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveContract.Click
        Try
            Dim msgrslt As MsgBoxResult
            msgrslt = MsgBox("متأكد من الحفظ", MsgBoxStyle.OkCancel)
            If msgrslt = MsgBoxResult.Cancel Then
                Exit Sub
            End If

            If (Not TestVaild()) Then
                Return
            End If


            gContractO.ContractNO = txtContractNo.Text
            gContractO.DateContract = dtpContractDate.Value
            gContractO.CustomerID = cboCustomer.SelectedValue
            gContractO.ParentGood = cboParentGood.SelectedValue
            gContractO.AllQuantity = txtAllQuantity.Text
            gContractO.QuantityRatio = txtQuantityRatio.Text
            gContractO.DeliveryMonth = dtpDeliveryMonth.Value
            gContractO.Notes = txtNotes.Text
            gContractO.Finished = chkFinished.Checked
            If Val(txtOutGoingQuantity.Text) = 0 Then
                gContractO.OutGoingQuantity = 0
            Else
                gContractO.OutGoingQuantity = txtOutGoingQuantity.Text
            End If
            gContractO.Save()

            ''Code to refresh the cboCustomer
            gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", "TblDealer WHERE Dealer_ID>2 and Type = 1 ORDER BY Dealer_Name", cboCustomer)
            ''Code to refresh the cboParentGood
            gAdo.CtrlItemsLoad("CategoryID", "CategoryName", "tblGoodCategories ORDER BY CategoryName", cboParentGood)
            ''Code to refresh
            gAdo.CtrlItemsLoad("ContractID", "ContractNO", "tblContracts ORDER BY ContractID ", lstContract)

            btnNewContract.Enabled = True
            btnSaveContract.Enabled = False
            MsgBox("لقد تم الحفظ بنجاح")
            '' Code To Select Last Index 
            lstContract.SelectedIndex = lstContract.Items.Count - 1



        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnDeleteContract_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteContract.Click
        Try

            Dim msgrslt As MsgBoxResult
            msgrslt = MsgBox("هل ترغب فى حذف هذا العنصر ؟", MsgBoxStyle.OkCancel)
            If msgrslt = MsgBoxResult.Ok Then
                gContractO.ID = lblContractCode.Text
                gContractO.Remove()
                MsgBox("تم الحذف بنجاح")
                ''Code to refresh lstContract
                gAdo.CtrlItemsLoad("ContractID", "ContractNO", "tblContracts ORDER BY ContractID", lstContract)
                If lstContract.Items.Count = 0 Then
                    btnNewContract_Click(sender, e)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnNewContractDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewContractDet.Click
        Try

            gContractDetO.ID = 0
            txtContDetQuantity.Clear()
            If cboContDetGoodID.Items.Count > 0 Then
                cboContDetGoodID.SelectedIndex = 0
            End If
            txtOutGoingQuantity.Clear()

            txtContDetQuantity.Select()
            btnNewContractDet.Enabled = False
            btnSaveContractDet.Enabled = True
            cboContDetGoodID.Text = ""

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSaveContractDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveContractDet.Click
        'If txtContDetQuantity.Text = "" Then
        '    MessageBox.Show(gMsg, "يجب ادخال الكمية", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'Else
        Dim msgrslt As MsgBoxResult
        msgrslt = MsgBox("متأكد من الحفظ", MsgBoxStyle.OkCancel)
        If msgrslt = MsgBoxResult.Cancel Then
            Exit Sub
        End If
        Try

      

            Dim intContractID As Integer
            intContractID = Val(lblContractCode.Text)
            gContractDetO.ContractID = intContractID
            gContractDetO.GoodID = cboContDetGoodID.SelectedValue
            'gContractDetO.Quantity = txtContDetQuantity.Text
            'If Val(txtOutGoingQuantity.Text) = 0 Then
            '    gContractDetO.OutGoingQuantity = 0
            'Else
            '    gContractDetO.OutGoingQuantity = txtOutGoingQuantity.Text
            'End If

            gContractDetO.Save()

            ''' Code to Refresh lstContractDet

            gAdo.CtrlItemsLoad("ContractDetID", "Good_Name", "tblContractDet join tblGood on tblGood.Good_ID = tblContractDet.GoodID " & _
                               " where ContractID = " & lstContract.SelectedValue & " ORDER BY ContractID ", lstContractDet)
            btnNewContractDet.Enabled = True
            btnSaveContractDet.Enabled = False
            MsgBox("لقد تم الحفظ بنجاح")
            '' Code To Select Last Index 
            lstContractDet.SelectedIndex = lstContractDet.Items.Count - 1
            cboContDetGoodID.Text = ""
            btnNewContractDet_Click(Nothing, Nothing)
        Catch ex As Exception

        End Try
        'End If
    End Sub

    Private Sub btnDeleteContractDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteContractDet.Click
        Try

            Dim msgrslt As MsgBoxResult
            msgrslt = MsgBox("هل ترغب فى حذف هذا العنصر ؟", MsgBoxStyle.OkCancel)
            If msgrslt = MsgBoxResult.Ok Then
                Dim intContractDetID As Integer
                intContractDetID = gAdo.CmdExecScalar("select ContractDetID from tblContractDet where ContractDetID = " & lstContractDet.SelectedValue & "")

                gContractDetO.Remove()
                MsgBox("تم الحذف بنجاح")
                ''Code to refresh lstContract
                gAdo.CtrlItemsLoad("ContractDetID", "Good_Name", "tblContractDet join tblGood on tblGood.Good_ID = tblContractDet.GoodID " & _
                           " where ContractID = " & lstContract.SelectedValue & " ORDER BY ContractID ", lstContractDet)
                'gAdo.CtrlItemsLoad("ContractDetID", "Good_Name", "tblContractDet join tblGood on tblGood.Good_ID = tblContractDet.GoodID ORDER BY ContractID", lstContractDet)
                If lstContractDet.Items.Count = 0 Then
                    btnNewContractDet_Click(sender, e)
                End If
            End If
            cboContDetGoodID.Text = ""

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtContractSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtContractSearch.TextChanged
        lstContract.SelectedIndex = lstContract.FindString(txtContractSearch.Text)
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
            gAdo.CtrlItemsLoad("Car_ID", "CarBoard_No", "tblCar  Order by CarBoard_No", lstCars)
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
                    gAdo.CtrlItemsLoad("Car_ID", "CarBoard_No", "tblCar  Order by CarBoard_No", lstCars)
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
                    gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity where City_ID>1 ORDER BY City_Name", LstCity)
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
            gAdo.CtrlItemsLoad("Driver_ID", "Driver_Name", "TblDriver Where Driver_ID >1 ORDER BY Driver_Name", LstDrivers)
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
                    gAdo.CtrlItemsLoad("Driver_ID", "Driver_Name", "TblDriver Where Driver_ID >1 ORDER BY Driver_Name", LstDrivers)
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
            gDealerO.ID = (LstDealer.SelectedValue)
            gDealerO.Refresh()
            txtDealerID.Text = gDealerO.ID
            txtDealerName.Text = gDealerO.Name
            txtDealerShortName.Text = gDealerO.DealerShortName
            If gDealerO.Type = 1 Then
                cboDealerType.SelectedIndex = 0
            ElseIf gDealerO.Type = 2 Then
                cboDealerType.SelectedIndex = 1
            ElseIf gDealerO.Type = 3 Then
                cboDealerType.SelectedIndex = 2
            Else
                cboDealerType.SelectedIndex = 3
            End If

            BtnDealerAddNew.Enabled = True
            BtnDealerSave.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnDealerAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDealerAddNew.Click
        gDealerO.ID = 0
        txtDealerName.Clear()
        txtDealerID.Text = ""
        txtDealerShortName.Text = ""
        cboDealerType.SelectedIndex = -1
        txtDealerID.Select()
        BtnDealerAddNew.Enabled = False
        BtnDealerSave.Enabled = True
    End Sub

    Private Sub BtnDealerSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDealerSave.Click
        If IsErrorMsg(txtDealerName) = False Then

            Dim mstrDealerName As String = txtDealerName.Text

            gDealerO.DealerID = txtDealerID.Text
            gDealerO.Name = txtDealerName.Text
            gDealerO.DealerShortName = txtDealerShortName.Text
            If cboDealerType.SelectedIndex = 0 Then
                ' عميل
                gDealerO.Type = 1

            ElseIf cboDealerType.SelectedIndex = 1 Then
                ' مورد
                gDealerO.Type = 2

            ElseIf cboDealerType.SelectedIndex = 2 Then
                ' عميل و مورد
                gDealerO.Type = 3
            Else
                ' لا عميل و لا مورد
                gDealerO.Type = 3

            End If
            gDealerO.Save()


            gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", "TblDealer Where Dealer_ID>2 ORDER BY Dealer_Name", LstDealer)
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
                    gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", "TblDealer Where Dealer_ID>2 ORDER BY Dealer_Name", LstDealer)
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
                    gAdo.CtrlItemsLoad("Car_Info_ID", "TruckBoard_No", "TblCarInfo where Car_Info_ID>1 ORDER BY TruckBoard_No", LstTruck)
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
            gGoodO.IDStr = LstGoods.SelectedValue
            gGoodO.RefreshStrID()
            txtID.Text = gGoodO.IDStr()
            TxtGoodsName.Text = gGoodO.Name
            cboGoodtype.SelectedValue = gGoodO.GoodType
            cboProtype.SelectedValue = gGoodO.ProType
            cbotype.SelectedIndex = gGoodO.Type
            BtnGoodsAddNew.Enabled = True
            BtnGoodsSave.Enabled = True
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BtnGoodsAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGoodsAddNew.Click
        gGoodO.IDStr = ""
        txtID.Text = ""
        TxtGoodsName.Clear()
        cboProtype.SelectedIndex = -1
        cboGoodtype.SelectedIndex = -1
        TxtGoodsName.Select()
        cbotype.SelectedIndex = 0
        BtnGoodsAddNew.Enabled = False
        BtnGoodsSave.Enabled = True
        txtID.Focus()
    End Sub

    Private Sub BtnGoodsSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGoodsSave.Click
        Dim mstrGoodsName As String
        Dim x As String
        If IsErrorMsg(TxtGoodsName) = False Then
            'If gGoodO.IDStr = "" Then
            x = txtID.Text
            'Else
            '    x = LstGoods.SelectedValue
            'End If


            mstrGoodsName = TxtGoodsName.Text

            gGoodO.IDStr = txtID.Text
            gGoodO.Name = TxtGoodsName.Text
            gGoodO.GoodType = cboGoodtype.SelectedValue
            gGoodO.ProType = cboProtype.SelectedValue
            gGoodO.Type = cbotype.SelectedIndex
            gGoodO.NOTIdentitySave()
            gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood Where type in (2,3) or Good_ID=1 ORDER BY Good_Name", LstGoods)

            LstGoods.SelectedValue = x
            BtnGoodsAddNew.Enabled = True
            BtnGoodsSave.Enabled = False
            MsgBox("لقد تم الحفظ بنجاح")
        Else
            MessageBox.Show(gMsg, "البيانات غير مكتمله", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        'LstGoods.SelectedValue = LstGoods.FindString(mstrGoodsName)
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
                    gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood Where type IN (2,3) or Good_ID=1 ORDER BY Good_Name", LstGoods)
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
            chkCanUpdate.Checked = gUserO.CanUpdate
            'TO Get the Picture From The SQL DataBase 
            If gMethods.BinaryPicLoad("Picture", "tblPicture", "Pic_ID =" & gUserO.User_Pic_ID.ID, PicUser) = False Then
                PicUser.Image = Nothing
            Else

            End If

            If gUserO.UserType = "a" Then
                RdoAdmin.Checked = True
            ElseIf gUserO.UserType = "m" Then
                RdoManager.Checked = True
            Else : RdoWZAN.Checked = True
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
        chkCanUpdate.Checked = False
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
                gUserO.CanUpdate = chkCanUpdate.Checked
                If RdoAdmin.Checked Then
                    gUserO.UserType = "a"
                ElseIf RdoManager.Checked Then
                    gUserO.UserType = "m"
                Else
                    gUserO.UserType = "n"
                End If

                gUserO.Save()
                Dim UserCount As Integer
                UserCount = gAdo.CmdExecScalar("Select count (User_ID) from TblUser")
                If UserCount > 0 Then
                    FrmRefresh()
                    ContAddNew.SelectedTab = tbUserPermission
                End If
                MsgBox("لقد تم الحفظ بنجاح")
            Else

                Dim mstrUserName As String = TxtUserNickName.Text
                gUserO.Name = TxtUserName.Text
                gUserO.User_Password = TxtUserPassword.Text
                gUserO.User_NickName = TxtUserNickName.Text
                gUserO.CanUpdate = chkCanUpdate.Checked
                If RdoAdmin.Checked Then
                    gUserO.UserType = "a"
                ElseIf RdoManager.Checked Then
                    gUserO.UserType = "m"
                Else
                    gUserO.UserType = "n"
                End If
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
                    ContAddNew.SelectedTab = tbUserPermission
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
            gAdo.CtrlItemsLoad("Issue_To_ID", "Issue_To_Name", "tblIssueTo where Issue_To_ID>1 ORDER BY Issue_To_Name", LstIssueTo)
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
                    gAdo.CtrlItemsLoad("Issue_To_ID", "Issue_To_Name", "tblIssueTo where Issue_To_ID>1 ORDER BY Issue_To_Name", LstIssueTo)
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
                'gAdo.CmdExec("Update tblUserPermission " _
                '             & "set Allow ='" & GrdUsePermission.Rows(i).Cells("allow").Value.ToString _
                '             & "' where User_Permission_ID= " _
                '             & GrdUsePermission.Rows(i).Cells("User_Permission_ID").Value.ToString)
                If GrdUsePermission.Rows(i).Cells("User_Permission_ID").Value.ToString > 0 Then
                    gUserPermissionO.ID = Val(GrdUsePermission.Rows(i).Cells("User_Permission_ID").Value.ToString)
                    gUserPermissionO.Refresh()
                    gUserPermissionO.Allow = CBool(GrdUsePermission.Rows(i).Cells("allow").Value.ToString)
                    gUserPermissionO.Save()
                Else
                    gUserPermissionO.ID = 0
                    'gUserO.ID = Val(GrdUsePermission.Rows(i).Cells("User_Permission_ID").Value.ToString)
                    'gUserO.Refresh()
                    gUserPermissionO.User_ID.ID = Val(LstUserPermission.SelectedValue)
                    'gPermissionO.ID = Val(GrdUsePermission.Rows(i).Cells("User_Permission_ID").Value.ToString)
                    'gUserPermissionO.Refresh()
                    gUserPermissionO.Permission_ID.ID = Val(GrdUsePermission.Rows(i).Cells("Permission_ID").Value.ToString)
                    Dim allow As String
                    allow = (GrdUsePermission.Rows(i).Cells("allow").Value.ToString)
                    If (GrdUsePermission.Rows(i).Cells("allow").Value.ToString) = "" Then
                        gUserPermissionO.Allow = False
                    Else
                        gUserPermissionO.Allow = True
                    End If

                    gUserPermissionO.Save()

                End If


            Next

            MsgBox("تم الحفظ بنجاح")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub LstUserPermission_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LstUserPermission.SelectedIndexChanged
        Try

            If Val(Me.LstUserPermission.SelectedValue) = 0 Then
                Exit Sub
            End If

            'gUserPermissionO.Refresh()
            'gUserPermissionO.User_ID.ID = Val(LstUserPermission.SelectedValue)
            Dim ds As DataSet
            'ds = gAdo.SelectData("select ISNULL( UP.User_Permission_ID,0) User_Permission_ID,Pr.Permission_ID Permission_ID,Pr.Permission_Name,Up.allow,UP.User_ID " _
            '                    & " from dbo.tblPermission as Pr left join dbo.tblUserPermission as UP " _
            '                   & "on Pr.Permission_ID = uP.Permission_ID and UP.User_ID= " & gUserPermissionO.User_ID.ID, False, "tblpermission")

            ds = gAdo.SelectData("select ISNULL( UP.User_Permission_ID,0) User_Permission_ID,Pr.Permission_ID Permission_ID,Pr.Permission_Name,Up.allow,UP.User_ID " _
                                & " from dbo.tblPermission as Pr left join dbo.tblUserPermission as UP " _
                               & "on Pr.Permission_ID = uP.Permission_ID and UP.User_ID= " & Val(LstUserPermission.SelectedValue), False, "tblpermission")

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
            ContAddNew.SelectedTab = tbCity

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
            ContAddNew.SelectedTab = tbCity

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

    'Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    gRole.ID = 0
    '    txtSearchRole.Text = ""
    '    txtRoleName.Text = ""
    'End Sub

    'Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If IsErrorMsg(txtRoleName) = False Then

    '        gRole.Name = txtRoleName.Text
    '        gRole.Save()

    '        MsgBox("لقد تم الحفظ بنجاح")
    '    Else
    '        MessageBox.Show(gMsg, "البيانات غير مكتمله", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '    End If

    'End Sub

    'Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        gAdo.CmdExec("delete tblRoles where RoleID=" & lstGroups.SelectedValue)
    '    Catch ex As Exception
    '    End Try

    'End Sub

    'Private Sub lstGroups_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If Val(Me.lstGroups.SelectedValue) = 0 Then
    '            Exit Sub
    '        End If
    '        gRole.ID = lstGroups.SelectedValue
    '        gRole.Refresh()

    '        txtRoleName.Text = gRole.Name


    '    Catch ex As Exception

    '    End Try
    'End Sub







    'Private Sub Label66_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        For i As Integer = 0 To TreeView2.Nodes.Count - 1
    '            For ii As Integer = 0 To TreeView2.Nodes(i).Nodes.Count - 1
    '                If TreeView2.Nodes(i).Nodes(ii).Checked Then
    '                    gControlRole.ID = 0
    '                    gControlRole.RoleID = cboRoles.SelectedValue
    '                    gControlRole.ControlID = TreeView2.Nodes(i).Nodes(ii).Name
    '                    gControlRole.Save()
    '                End If
    '            Next
    '        Next

    '        CreateTree1(cboRoles.SelectedValue)
    '        CreateTree2(cboRoles.SelectedValue)
    '    Catch ex As Exception
    '    End Try

    'End Sub


    'Private Sub CreateTree1(ByVal RoleID As Integer)
    '    Try
    '        TreeView1.Nodes.Clear()
    '        ' FillFormNames(GridControl1)
    '        Dim dt As New DataTable



    '        dt = gAdo.FillDTGeneral("Select Distinct C1.ControlID,C1.ControlCaption from tblControlRoles as CR Join tblControls as C on CR.ControlID = C.ControlID JOIN tblControls C1 ON C.ParentID = C1.ControlID Where CR.RoleID = " & RoleID & "")
    '        For j = 0 To dt.Rows.Count - 1
    '            Dim ID As String = dt.Rows(j).Item("ControlID").ToString
    '            Dim Name As String = dt.Rows(j).Item("ControlCaption").ToString
    '            TreeView1.Nodes.Add(ID, Name)
    '            If j = dt.Rows.Count - 1 Then
    '                Dim dtt As New DataTable
    '                '                    gobjDb.FillDS("select * from tblFormsTree where ID  IN (select Distinct ParentID FROM tblFormsTree ) AND ParentID<>0", dss)
    '                dtt = gAdo.FillDTGeneral3("Select CR.Controls_RolesID,C.ControlCaption,C.ParentID,C.ControlName from tblControlRoles as CR Join tblControls as C on CR.ControlID = C.ControlID Where RoleID = " & RoleID & " AND C.ParentID<>0")
    '                'Select CR.Controls_RolesID,ControlName from tblControlRoles as CR Join tblControls as C on CR.ControlID = C.ControlID Where RoleID = & RoleID & " and FormID = " & FormID
    '                For jj = 0 To dtt.Rows.Count - 1
    '                    Dim ID1 As String = dtt.Rows(jj).Item("Controls_RolesID").ToString
    '                    Dim Name1 As String = dtt.Rows(jj).Item("ControlCaption").ToString
    '                    Dim Parent1 As String = dtt.Rows(jj).Item("ParentID").ToString
    '                    Dim FormName1 As String = dtt.Rows(jj).Item("ControlName").ToString
    '                    Dim TreeNodes1() As TreeNode = TreeView1.Nodes.Find(Parent1, True)



    '                    If TreeNodes1.Length > 0 Then
    '                        'TreeNodes1(0).Nodes.Add(FormName1, Name1)
    '                        TreeNodes1(0).Nodes.Add(ID1, Name1)
    '                        'TreeNodes1(0).Nodes(0).Tag = FormName1
    '                    Else
    '                        MsgBox(1)
    '                    End If

    '                Next
    '            End If
    '        Next



    '    Catch ex As Exception

    '    End Try

    'End Sub



    'Private Sub CreateTree2(ByVal RoleID As Integer)
    '    Try
    '        TreeView2.Nodes.Clear()
    '        ' FillFormNames(GridControl1)
    '        Dim dt As New DataTable

    '        dt = gAdo.FillDTGeneral("Select Distinct C1.ControlID,C1.ControlCaption from tblControls as C left outer Join  tblControlRoles as CR   on CR.ControlID = C.ControlID JOIN tblControls C1 ON C.ParentID = C1.ControlID " & _
    '                      " Where C.ControlID not in (Select ControlID from tblControlRoles Where RoleID  = " & RoleID & ")  ")
    '        For j = 0 To dt.Rows.Count - 1
    '            Dim ID As String = dt.Rows(j).Item("ControlID").ToString
    '            Dim Name As String = dt.Rows(j).Item("ControlCaption").ToString
    '            TreeView2.Nodes.Add(ID, Name)
    '            If j = dt.Rows.Count - 1 Then
    '                Dim Dtt As New DataTable

    '                Dtt = gAdo.FillDTGeneral2("Select Distinct C.ControlID,C.ControlCaption,C.ParentID,C.ControlName from tblControls as C Left outer Join tblControlRoles as CR on CR.ControlID = C.ControlID " & _
    '                               " Where C.ControlID not in (Select ControlID from tblControlRoles Where RoleID = " & RoleID & " )  AND C.ParentID<>0")
    '                'Select CR.Controls_RolesID,ControlName from tblControlRoles as CR Join tblControls as C on CR.ControlID = C.ControlID Where RoleID = & RoleID & " and FormID = " & FormID
    '                For jj = 0 To Dtt.Rows.Count - 1
    '                    Dim ID1 As String = Dtt.Rows(jj).Item("ControlID").ToString
    '                    Dim Name1 As String = Dtt.Rows(jj).Item("ControlCaption").ToString
    '                    Dim Parent1 As String = Dtt.Rows(jj).Item("ParentID").ToString
    '                    Dim FormName1 As String = Dtt.Rows(jj).Item("ControlName").ToString
    '                    Dim TreeNodes1() As TreeNode = TreeView2.Nodes.Find(Parent1, True)

    '                    If TreeNodes1.Length > 0 Then
    '                        'TreeNodes1(0).Nodes.Add(FormName1, Name1)
    '                        TreeNodes1(0).Nodes.Add(ID1, Name1)
    '                        'TreeNodes1(0).Nodes(0).Tag = FormName1
    '                    Else
    '                        MsgBox(1)
    '                    End If

    '                Next
    '            End If
    '        Next



    '    Catch ex As Exception

    '    End Try

    'End Sub



    'Private Sub Label69_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    CreateTree1(cboRoles.SelectedValue)
    '    CreateTree2(cboRoles.SelectedValue)
    'End Sub

    'Private Sub Label67_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        For i As Integer = 0 To TreeView1.Nodes.Count - 1


    '            For ii As Integer = 0 To TreeView1.Nodes(i).Nodes.Count - 1


    '                If TreeView1.Nodes(i).Nodes(ii).Checked Then

    '                    gControlRole.ID = TreeView1.Nodes(i).Nodes(ii).Name
    '                    gControlRole.Remove()

    '                End If


    '            Next


    '        Next
    '        CreateTree1(cboRoles.SelectedValue)
    '        CreateTree2(cboRoles.SelectedValue)

    '    Catch ex As Exception

    '    End Try

    'End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub dtgGoods_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgGoods.CellClick
        Try
     
            If Val(dtgGoods.RowCount) = 0 Then
                Exit Sub
            End If

            'Dim intContractDetID As Integer
            'intContractDetID = dtgGoods.Rows(dtgGoods.CurrentCell.RowIndex).Cells(0).Value.ToString()
            'intContractDetID = gAdo.CmdExecScalar(" select ContractDetID from tblContractDet join tblContracts on tblContracts.ContractID = tblContractDet .ContractID " & _
            '                                        " join tblDealer on tblDealer .Dealer_ID = tblContracts .CustomerID join tblGood on tblGood .Good_ID = tblContractDet .GoodID " & _
            '                                        " and tblContracts.ContractID = " & cboQContractNO.SelectedValue & " and tblDealer.Dealer_ID = " & cboQCustomer.SelectedValue & _
            '                                        " and tblGood.Good_ID = '" & dtgGoods.Rows(dtgGoods.CurrentCell.RowIndex).Cells(1).Value.ToString & "'")


            'Dim intContractDetID As Integer
            'intContractDetID = gAdo.CmdExecScalar("select ContractDetID from tblContractDet join tblContracts on tblContracts .ContractID = tblContractDet .ContractID " & _
            '                                       " join tblDealer on tblDealer .Dealer_ID = tblContracts .CustomerID join tblGood on tblGood .Good_ID = tblContractDet .GoodID " & _
            '                                       " and tblDealer .Dealer_ID = " & cboQCustomerSearch.SelectedValue & _
            '                                       " and tblGood .Good_ID = '" & lstQuotaGood.SelectedValue & "'")
            'Dim intQuotaID As Integer
            'intQuotaID = gAdo.CmdExecScalar(" set dateformat dmy select QuotaID from tblDailyQuota " & _
            '                                " where ContractDetID = " & intContractDetID & " and DateQuota = '" & dtpQDateSearch.Value.Date & "'")

            'Dim intQuotaID As Integer
            'intQuotaID = dtgGoods.Rows(dtgGoods.CurrentCell.RowIndex).Cells(0).Value.ToString()
            'gDailyQuota.ID = intQuotaID
            'gDailyQuota.Refresh()
            ''gDebitNoteQuantityO.ID = gDailyQuota.DebitNoteQuantityID.ID
            ''gDebitNoteQuantityO.Refresh()
            'gDebiteNoteO.ID = gDebitNoteQuantityO.DebitNoteID.ID
            'gDebiteNoteO.Refresh()
            'gContractorO.ID = gDebiteNoteO.ContractID.ID
            'gContractO.Refresh()
            'gDealerO.ID = gContractO.CustomerID
            'gDealerO.Refresh()
            'cboQCustomer.SelectedValue = gDealerO.ID
            'gDebiteNoteO.ID = gDailyQuota.DebitNoteID
            'gDebiteNoteO.Refresh()
            'cboDebiteNote.SelectedValue = gDebiteNoteO.DebitNote
            'cboQGoodID.SelectedValue = gContractDetO.GoodID
            'txtQuota.Text = gDailyQuota.Quota
            'txtQNotes.Text = gDailyQuota.Notes
            'dtpQDate.Value = gDailyQuota.DateQuota

            Dim intQuotaID As Integer
            intQuotaID = dtgGoods.Rows(dtgGoods.CurrentCell.RowIndex).Cells(0).Value.ToString()
            'intContractDetID = gAdo.CmdExecScalar(" select ContractDetID from tblContractDet join tblContracts on tblContracts.ContractID = tblContractDet .ContractID " & _
            '                                        " join tblDealer on tblDealer .Dealer_ID = tblContracts .CustomerID join tblGood on tblGood .Good_ID = tblContractDet .GoodID " & _
            '                                        " and tblContracts.ContractID = " & cboQContractNO.SelectedValue & " and tblDealer.Dealer_ID = " & cboQCustomer.SelectedValue & _
            '                                        " and tblGood.Good_ID = '" & dtgGoods.Rows(dtgGoods.CurrentCell.RowIndex).Cells(1).Value.ToString & "'")


            'Dim intContractDetID As Integer
            'intContractDetID = gAdo.CmdExecScalar("select ContractDetID from tblContractDet join tblContracts on tblContracts .ContractID = tblContractDet .ContractID " & _
            '                                       " join tblDealer on tblDealer .Dealer_ID = tblContracts .CustomerID join tblGood on tblGood .Good_ID = tblContractDet .GoodID " & _
            '                                       " and tblDealer .Dealer_ID = " & cboQCustomerSearch.SelectedValue & _
            '                                       " and tblGood .Good_ID = '" & lstQuotaGood.SelectedValue & "'")
            'Dim intQuotaID As Integer
            'intQuotaID = gAdo.CmdExecScalar(" set dateformat dmy select QuotaID from tblDailyQuota " & _
            '                                " where DebitNoteQuantityID = " & intDebitNoteQuatityID & " and DateQuota = '" & dtpQDateSearch.Value.Date & "'")
            gDailyQuota.ID = intQuotaID
            gDailyQuota.Refresh()

            'gDebitNoteQuantityO.ID = gDailyQuota.DebitNoteID.ID
            'gDebitNoteQuantityO.Refresh()
            gDebiteNoteO.ID = gDailyQuota.DebitNoteID.ID
            gDebiteNoteO.Refresh()
            gContractO.ID = gDebiteNoteO.ContractID.ID
            gContractO.Refresh()
            gDealerO.ID = gContractO.CustomerID
            gDealerO.Refresh()
            cboQCustomer.SelectedValue = gDealerO.ID

            cboQCustomer.Enabled = False
            cboDebiteNote.SelectedValue = gDebiteNoteO.ID
            cboQGoodID.SelectedValue = gDebitNoteQuantityO.GoodID.ID
            'lblRemainder.Text =
            txtQuota.Text = gDailyQuota.Quota
            txtQNotes.Text = gDailyQuota.Notes
            dtpQDate.Value = gDailyQuota.DateQuota
            chkDQFinished.Checked = gDailyQuota.Finished
            txtOutGoingQty.Text = gDailyQuota.OutGoingQty

            GetRemainDQ(intQuotaID)

        Catch ex As Exception

        End Try
    End Sub

    Sub GetRemainDQ(ByVal QID As Integer)
        RemainDQ.Text = ""
        Dim SQL As String
        Dim TQTY As Decimal = 0
        Dim DQTY As Decimal = 0
        Try
            'SQL = "Select isnull(Sum(T.Quantity/1000),0) From tblTransQuota T where T.QuotaID=" & QID
            SQL = "set Dateformat dmy Select isnull(sum((TQ.Quantity)/1000.000),0) From tblTransactions T Join tblTransQuota TQ on(T.Trans_ID=TQ.TransID) where Out_Date is not Null and convert(Datetime,Out_Date,103)<'" & gMethods.GetShifName_And_SearchDate(True)(0) & "' and TQ.QuotaID=" & QID.ToString() & "  and T.Dealer_ID=" & cboQCustomer.SelectedValue.ToString()
            TQTY = gAdo.CmdExecScalar(SQL)
            TQTY = Decimal.Round(TQTY, 3)
            txtOutFromQouta.Text = TQTY.ToString()
            DQTY = Val(txtQuota.Text.Trim())
            RemainDQ.Text = (DQTY - TQTY).ToString()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnDebitNoteADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewDebitNote.Click
        gDebiteNoteO.ID = 0

        txtDebitNoteID.Text = 0
        txtDebitNote.Text = ""
        cmbContractID.SelectedIndex = -1
        chkDebitNoteFinish.Checked = False
        txtDebitNoteNotes.Text = ""
        cboDebitNoteCustomer.SelectedIndex = -1
        ' preparation Debit Note Details
        cmbDebitNoteGoodID.SelectedIndex = -1
        txtTotalQty.Clear()
        txtDoutgoingQty.Clear()
        

    End Sub

    Private Sub btnDebitNoteSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveDebitNote.Click
        Dim msgrslt As MsgBoxResult
        msgrslt = MsgBox("متأكد من الحفظ", MsgBoxStyle.OkCancel)
        If msgrslt = MsgBoxResult.Cancel Then
            Exit Sub
        End If
        If txtDebitNote.Text = "" Or cmbContractID.SelectedValue < 1 Or txtTotalQty.Text.Trim() = "" Then
            MsgBox("من فضلك إستكمل البيانات لكى يتم الحفظ")
            Exit Sub
        End If

        gDebiteNoteO.DebitNote = txtDebitNote.Text
        gDebiteNoteO.ContractID.ID = cmbContractID.SelectedValue
        gDebiteNoteO.Finished = chkDebitNoteFinish.Checked
        gDebiteNoteO.Notes = txtDebitNoteNotes.Text
        gDebiteNoteO.TotalQty = txtTotalQty.Text.Trim()
        Dim DOGQ As Decimal = If(txtDoutgoingQty.Text.Trim() = "", "0", txtDoutgoingQty.Text.Trim())
        gDebiteNoteO.OutgoingQty = DOGQ


        gDebiteNoteO.Save()

        btnNewDebitNote.Enabled = True
        'btnSaveDebitNote.Enabled = False
        MsgBox("لقد تم الحفظ بنجاح")
        gAdo.CtrlItemsLoad("DebitNoteID", "DebitNote", "tblDebitNote where finish=0 ORDER BY DebitNoteID", lstDebitNote)
        '' Code To Select Last Index 
        'lstDebitNote.SelectedIndex = lstContract.Items.Count - 1
    End Sub

    Private Sub lstDebitNote_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstDebitNote.SelectedIndexChanged
        Try
            If Val(Me.lstDebitNote.SelectedValue) = 0 Then
                Exit Sub
            End If
            gDebiteNoteO.ID = Val(lstDebitNote.SelectedValue)
            gDebiteNoteO.Refresh()
            txtDebitNoteID.Text = gDebiteNoteO.ID
            txtDebitNote.Text = gDebiteNoteO.DebitNote
            cmbContractID.SelectedValue = gDebiteNoteO.ContractID.ID
            chkDebitNoteFinish.Checked = gDebiteNoteO.Finished
            txtDebitNoteNotes.Text = gDebiteNoteO.Notes
            txtTotalQty.Text = gDebiteNoteO.TotalQty
            txtDoutgoingQty.Text = gDebiteNoteO.OutgoingQty

        
            ''Code to refresh lstDebitNoteQuantity
            ' Fill lstDebitNoteQuantity with Goods  
            gAdo.CtrlItemsLoad("DebitNoteQuantityID", "Good_Name", "tblDebitNoteQuantity join tblGood on tblDebitNoteQuantity.GoodID=tblGood.Good_ID where DebitNoteID='" & txtDebitNoteID.Text & "' ORDER BY DebitNoteID", lstDebitNoteQuantity)
            If lstDebitNoteQuantity.SelectedValue Is Nothing Then
                btnNewDebitNoteQuantity_Click(sender, e)
            End If

            'btnNewContract.Enabled = True
            'btnSaveContract.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

  
    Private Sub chkSearchDebitNote_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSearchDebitNote.CheckedChanged

        gDebiteNoteO.ID = 0
        txtDebitNoteID.Text = 0
        txtDebitNote.Text = ""
        cmbContractID.SelectedIndex = -1
        chkDebitNoteFinish.Checked = False
        txtDebitNoteNotes.Text = ""

        If chkSearchDebitNote.Checked = True Then
            gAdo.CtrlItemsLoad("DebitNoteID", "DebitNote", "tblDebitNote where finish=1 ORDER BY DebitNoteID", lstDebitNote)
        Else
            gAdo.CtrlItemsLoad("DebitNoteID", "DebitNote", "tblDebitNote where finish=0 ORDER BY DebitNoteID", lstDebitNote)
        End If
    End Sub

    Private Sub btnDeleteDebitNote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteDebitNote.Click
        Try

            Dim msgrslt As MsgBoxResult
            msgrslt = MsgBox("هل ترغب فى حذف هذا العنصر ؟", MsgBoxStyle.OkCancel)
            If msgrslt = MsgBoxResult.Ok Then
                gDebiteNoteO.ID = txtDebitNoteID.Text
                gDebiteNoteO.Remove()
                MsgBox("تم الحذف بنجاح")
                ''Code to refresh lstDebitNote
                gAdo.CtrlItemsLoad("DebitNoteID", "DebitNote", "tblDebitNote where finish=0 ORDER BY DebitNoteID", lstDebitNote)
                'If lstDebitNote.Items.Count = 0 Then
                '    btnNewContract_Click(sender, e)
                'End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchDebitNote.TextChanged
        lstDebitNote.SelectedIndex = lstDebitNote.FindString(txtSearchDebitNote.Text)
    End Sub

    Private Sub btnNewDebitNoteQuantity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewDebitNoteQuantity.Click
        txtDebitNoteQuantityID.Text = 0
        gDebitNoteQuantityO.ID = 0
        cmbDebitNoteGoodID.SelectedIndex = -1
        txtDebitNoteQuantityNotes.Text = ""

    End Sub

    Private Sub btnSaveDebitNoteQuantity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveDebitNoteQuantity.Click
        Try

        
            If cmbDebitNoteGoodID.SelectedValue < 1 Then
                MsgBox("من فضلك إستكمل البيانات لكى يتم الإدخال بصورة صحيحة")
                Exit Sub
            End If
            gDebitNoteQuantityO.DebitNoteID.ID = gDebiteNoteO.ID  'txtDebitNoteID.Text 
            gDebitNoteQuantityO.GoodID.ID = cmbDebitNoteGoodID.SelectedValue
            gDebitNoteQuantityO.Notes = txtDebitNoteQuantityNotes.Text
            gDebitNoteQuantityO.Save()
            MsgBox("لقد تم الحفظ بنجاح")

            ' Fill lstDebitNoteQuantity with Goods  
            gAdo.CtrlItemsLoad("DebitNoteQuantityID", "Good_Name", "tblDebitNoteQuantity join tblGood on tblDebitNoteQuantity.GoodID=tblGood.Good_ID where DebitNoteID='" & txtDebitNoteID.Text & "' ORDER BY DebitNoteID", lstDebitNoteQuantity)
        Catch ex As Exception
            MsgBox(" لم يتم الحفظ")
        End Try

    End Sub

    Private Sub btnDeleteDebitNoteQuantity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteDebitNoteQuantity.Click
        Try

            Dim msgrslt As MsgBoxResult
            msgrslt = MsgBox("هل ترغب فى حذف هذا العنصر ؟", MsgBoxStyle.OkCancel)
            If msgrslt = MsgBoxResult.Ok Then

                gDebitNoteQuantityO.ID = txtDebitNoteQuantityID.Text
                gDebitNoteQuantityO.Remove()
                MsgBox("تم الحذف بنجاح")
                ''Code to refresh lstDebitNoteQuantity
                gAdo.CtrlItemsLoad("DebitNoteQuantityID", "GoodID", "tblDebitNoteQuantity ORDER BY DebitNoteQuantityID", lstDebitNoteQuantity)
                'If lstDebitNote.Items.Count = 0 Then
                '    btnNewContract_Click(sender, e)
                'End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lstDebitNoteQuantity_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstDebitNoteQuantity.SelectedIndexChanged
        Try


            If Val(Me.lstDebitNoteQuantity.SelectedValue) = 0 Then
                Exit Sub
            End If
            txtDebitNoteQuantityID.Text = lstDebitNoteQuantity.SelectedValue
            gDebitNoteQuantityO.ID = Val(lstDebitNoteQuantity.SelectedValue)

            gDebitNoteQuantityO.Refresh()
            cmbDebitNoteGoodID.SelectedValue = gDebitNoteQuantityO.GoodID.ID
            'txtDebitNoteTotalQuantity.Text = gDebitNoteQuantityO.TotalQuantity

            'txtDebitNoteOutgoingQuantity.Text = gDebitNoteQuantityO.OutgoingQuantity
            txtDebitNoteQuantityNotes.Text = gDebitNoteQuantityO.Notes


            '    ''Code to refresh lstDebitNoteQuantity
            '    ' Fill lstDebitNoteQuantity with Goods  
            '    gAdo.CtrlItemsLoad("DebitNoteQuantityID", "Good_Name", "tblDebitNoteQuantity join tblGood on tblDebitNoteQuantity.GoodID=tblGood.Good_ID where DebitNoteID='" & txtDebitNoteID.Text & "' ORDER BY DebitNoteID", lstDebitNoteQuantity)

            '    'btnNewContract.Enabled = True
            '    'btnSaveContract.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbContractID_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbContractID.SelectedIndexChanged


        Try

            If cmbContractID.SelectedValue > 0 Then

                gAdo.CtrlItemsLoad("Dealer_ID", "tblDealer.Dealer_Name", "tblDealer  join tblContracts on tblDealer.Dealer_ID =tblContracts.CustomerID   " & _
                                   " where  ContractID =" & cmbContractID.SelectedValue, cboDebitNoteCustomer)

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ContAddNew_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        
    End Sub


    Private Sub btnAddNewProtype_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewProtype.Click
        gProtypeO.ID = 0
        txtProtypeName.Clear()
        txtProtypeName.Select()
        btnAddNewProtype.Enabled = False
        btnSaveProtype.Enabled = True
    End Sub

    Private Sub btnSaveProtype_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveProtype.Click
        If IsErrorMsg(txtProtypeName) = False Then

            Dim mstrPTName As String = txtProtypeName.Text
            gProtypeO.Name = txtProtypeName.Text
            gProtypeO.Save()
            gAdo.CtrlItemsLoad("ProtypeID", "ProtypeName", "tblProtype ORDER BY ProtypeName", LstProtype)
            LstProtype.SelectedIndex = LstProtype.FindString(mstrPTName)
            btnAddNewProtype.Enabled = True
            btnSaveProtype.Enabled = False
            MsgBox("لقد تم الحفظ بنجاح")
        Else
            MessageBox.Show(gMsg, "البيانات غير مكتمله", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnDeleteProtype_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteProtype.Click
        Dim msgrslt As MsgBoxResult
        msgrslt = MsgBox("هل ترغب فى حذف هذا النوع ؟", MsgBoxStyle.OkCancel)
        If msgrslt = MsgBoxResult.Ok Then
            Try
                If LstProtype.Items.Count = 0 Then
                    Exit Sub
                End If
                
                gAdo.CmdExec("delete tblProtype where ProtypeID=" & LstProtype.SelectedValue)
                gAdo.CtrlItemsLoad("ProtypeID", "ProtypeName", "tblProtype ORDER BY ProtypeName", LstProtype)
                
            Catch ex As Exception

            End Try
        Else
        End If
    End Sub

    Private Sub txtProtypeSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProtypeSearch.TextChanged
        LstProtype.SelectedIndex = LstProtype.FindString(txtProtypeSearch.Text)
    End Sub

    Private Sub LstProtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LstProtype.SelectedIndexChanged
        Try
            If Val(Me.LstProtype.SelectedValue) = 0 Then
                Exit Sub
            End If
            gProtypeO.ID = Val(LstProtype.SelectedValue)
            gProtypeO.Refresh()
            txtProtypeName.Text = gProtypeO.Name

            btnAddNewProtype.Enabled = True
            btnSaveProtype.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtProtypeName_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProtypeName.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            btnSaveProtype.Focus()
        End If
    End Sub

    Private Sub btnAddNewGoodtype_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewGoodtype.Click
        gGoodtypeO.ID = 0
        txtGoodtypeName.Clear()
        txtGoodtypeName.Select()
        btnAddNewGoodtype.Enabled = False
        btnSaveGoodtype.Enabled = True
    End Sub

    Private Sub btnSaveGoodtype_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveGoodtype.Click
        If IsErrorMsg(txtGoodtypeName) = False Then

            Dim mstrGTName As String = txtGoodtypeName.Text
            gGoodtypeO.Name = txtGoodtypeName.Text
            gGoodtypeO.Save()
            gAdo.CtrlItemsLoad("GoodtypeID", "GoodtypeName", "tblGoodtype ORDER BY GoodtypeName", lstGoodtype)
            lstGoodtype.SelectedIndex = lstGoodtype.FindString(mstrGTName)
            btnAddNewGoodtype.Enabled = True
            btnSaveGoodtype.Enabled = False
            MsgBox("لقد تم الحفظ بنجاح")
        Else
            MessageBox.Show(gMsg, "البيانات غير مكتمله", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnDeleteGoodtype_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteGoodtype.Click
        Dim msgrslt As MsgBoxResult
        msgrslt = MsgBox("هل ترغب فى حذف هذا النوع ؟", MsgBoxStyle.OkCancel)
        If msgrslt = MsgBoxResult.Ok Then
            Try
                If lstGoodtype.Items.Count = 0 Then
                    Exit Sub
                End If

                gAdo.CmdExec("delete tblGoodtype where GoodtypeID=" & lstGoodtype.SelectedValue)
                gAdo.CtrlItemsLoad("GoodtypeID", "GoodtypeName", "tblGoodtype ORDER BY GoodtypeName", lstGoodtype)

            Catch ex As Exception

            End Try
        Else
        End If
    End Sub

    Private Sub txtSearchGoodtype_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchGoodtype.TextChanged
        lstGoodtype.SelectedIndex = lstGoodtype.FindString(txtSearchGoodtype.Text.Trim())
    End Sub

    Private Sub lstGoodtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstGoodtype.SelectedIndexChanged
        Try
            If Val(Me.lstGoodtype.SelectedValue) = 0 Then
                Exit Sub
            End If
            gGoodtypeO.ID = Val(lstGoodtype.SelectedValue)
            gGoodtypeO.Refresh()
            txtGoodtypeName.Text = gGoodtypeO.Name

            btnAddNewGoodtype.Enabled = True
            btnSaveGoodtype.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtGoodtypeName_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtGoodtypeName.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            btnSaveGoodtype.Focus()
        End If
    End Sub

    Private Sub txtID_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtID.KeyDown, TxtGoodsName.KeyDown, cbotype.KeyDown, cboProtype.KeyDown, cboGoodtype.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            SelectNextControl(DirectCast(sender, Control), True, True, True, True)
        End If
    End Sub

    Function TestVaildQuota() As Boolean
        If (txtQuantity.Text.Trim() = "") Then
            MsgBox("يجب إدخال كمية")
            Return False
        End If
        If (CustomerID.Text.Trim() = "") Then
            MsgBox("يجب إدخال عميل")
            Return False
        End If
        Return True
    End Function

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QbtnSave.Click
        Dim msgrslt As MsgBoxResult
        msgrslt = MsgBox("متأكد من الحفظ", MsgBoxStyle.OkCancel)
        If msgrslt = MsgBoxResult.Cancel Then
            Exit Sub
        End If
        If Not TestVaildQuota() Then
            Exit Sub
        End If

        If (gDailyQuotaWOContractO.ID <> 0) Then
            Dim SQl As String = "select isnull(D.Quantity,0) from tblDailyQuotaWithOutContract D where D.DailyQuotaID=" & gDailyQuotaWOContractO.ID
            Dim OldQuota As Decimal
            OldQuota = gAdo.CmdExecScalar(SQl)
            SQl = "Select ISNULL(SUM(T.Quantity)/1000.00,0) From tblTransQuotaWithOutContract T where T.QuotaID=" & gDailyQuotaWOContractO.ID
            Dim TransQuota As Decimal
            TransQuota = gAdo.CmdExecScalar(SQl)
            Dim CurQuota As Decimal
            CurQuota = Decimal.Parse(If(txtQuantity.Text.Trim() = "", "0", txtQuantity.Text.Trim()))
            If (CurQuota < TransQuota) Then
                MsgBox(" الكوته يجب ان لا تقل عن " & TransQuota.ToString())
                txtQuantity.Text = OldQuota.ToString()
                Exit Sub
            End If
        End If

        gDailyQuotaWOContractO.QuotaDate = dtpQuotaDate.Value
        gDailyQuotaWOContractO.CustomerID = CustomerID.SelectedValue
        gDailyQuotaWOContractO.Quantity = txtQuantity.Text.Trim()
        gDailyQuotaWOContractO.Finished = chkfinish.Checked
        gDailyQuotaWOContractO.Save()

        QbtnNew.Enabled = True
        QbtnSave.Enabled = True
        Dim CId As Integer = CustomerID.SelectedValue

        dtpDQDateSearch.Value = dtpQuotaDate.Value
        gAdo.CtrlItemsLoad(" distinct Dealer_ID", "Dealer_Name", "tblDealer join tblDailyQuotaWithOutContract on tblDailyQuotaWithOutContract.CustomerID = tblDealer.Dealer_ID " & _
                           " where  ((QuotaDate<=Convert(date,'" & dtpDQDateSearch.Value.Date & "') and finish=0) OR (EndDate=Convert(date,'" & dtpDQDateSearch.Value.Date & "') and finish=1))", CboDQCustomers)
        CboDQCustomers.SelectedValue = CId
        gAdo.CtrlItemsLoad("[DailyQuotaID]", "[DailyQuotaID]", "[tblDailyQuotaWithOutContract] where CustomerID=" & CId & " and ((QuotaDate<=Convert(date,'" & dtpDQDateSearch.Value.Date & "') and finish=0) OR (EndDate=Convert(date,'" & dtpDQDateSearch.Value.Date & "') and finish=1)) ORDER BY QuotaDate", lstDQuta)
        gDailyQuotaWOContractGoodO.ID = 0

        MsgBox("تم الحفظ بنجاح")

    End Sub

    Private Sub QbtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QbtnNew.Click
        gDailyQuotaWOContractO.ID = 0
        'dtpQuotaDate.Value = Date.Now.Date
        setQDate()
        dtpQuotaDate.Text = Date.Now.Date.ToShortDateString()
        CustomerID.Text = ""
        CustomerID.SelectedIndex = -1

        chkfinish.Checked = False
        lstDQGoods.DataSource = Nothing
        cboDQGoodID.Text = ""
        cboDQGoodID.SelectedIndex = -1
        txtQuantity.Clear()
        QbtnNew.Enabled = True
        QbtnSave.Enabled = True
        txtOutFromQ.Text = ""
        txtRFromQ.Text = ""
        gDailyQuotaWOContractGoodO.ID = 0
        CustomerID.Enabled = True
    End Sub

    Private Sub lstDQuta_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstDQuta.SelectedIndexChanged
        If Val(Me.lstDQuta.SelectedValue) = 0 Then
            Exit Sub
        End If
        gDailyQuotaWOContractO.ID = Val(lstDQuta.SelectedValue)
        gDailyQuotaWOContractO.Refresh()
        dtpQuotaDate.Value = gDailyQuotaWOContractO.QuotaDate
        CustomerID.SelectedValue = gDailyQuotaWOContractO.CustomerID
        CustomerID.Enabled = False
        txtQuantity.Text = gDailyQuotaWOContractO.Quantity
        chkfinish.Checked = gDailyQuotaWOContractO.Finished
        gAdo.CtrlItemsLoad("DailyQuotaGoodID", "Good_Name", "tblDailyQuotaWOContractGoods join tblGood on tblGood.Good_ID = tblDailyQuotaWOContractGoods.GoodID " & _
                               " where DailyQuotaID = " & lstDQuta.SelectedValue & " ORDER BY DailyQuotaGoodID ", lstDQGoods)
        cboDQGoodID.Text = ""
        cboDQGoodID.SelectedIndex = -1
        GetRemainDQ2(Val(lstDQuta.SelectedValue))
        gDailyQuotaWOContractGoodO.ID = 0
    End Sub

    Sub GetRemainDQ2(ByVal QID As Integer)
        RemainDQ.Text = ""
        Dim SQL As String
        Dim TQTY As Decimal = 0
        Dim DQTY As Decimal = 0
        Try
            'SQL = "Select isnull(Sum(T.Quantity/1000.00),0) From tblTransQuota T where T.QuotaID=" & QID
            SQL = "set Dateformat dmy Select isnull(sum((TQ.Quantity)/1000.00),0) From tblTransactions T Join tblTransQuotaWithOutContract TQ on(T.Trans_ID=TQ.TransID) where Out_Date is not Null and convert(Datetime,Out_Date,103)<'" & gMethods.GetShifName_And_SearchDate(True)(0) & "' and TQ.QuotaID=" & QID.ToString() & "  and T.Dealer_ID=" & CustomerID.SelectedValue.ToString()
            TQTY = gAdo.CmdExecScalar(SQL)
            TQTY = Decimal.Round(TQTY, 3)
            txtOutFromQ.Text = TQTY.ToString()
            DQTY = Val(txtQuantity.Text.Trim())
            txtRFromQ.Text = (DQTY - TQTY).ToString()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub QbtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QbtnDelete.Click
        Try

            Dim msgrslt As MsgBoxResult
            msgrslt = MsgBox("هل ترغب فى حذف هذه الكوته ؟", MsgBoxStyle.OkCancel)
            If msgrslt = MsgBoxResult.Ok Then
                gDailyQuotaWOContractO.ID = lstDQuta.SelectedValue
                gDailyQuotaWOContractO.Remove()
                MsgBox("تم الحذف بنجاح")
                dtpDQDateSearch.Value = Date.Now.Date
                gAdo.CtrlItemsLoad(" distinct Dealer_ID", "Dealer_Name", "tblDealer join tblDailyQuotaWithOutContract on tblDailyQuotaWithOutContract.CustomerID = tblDealer.Dealer_ID " & _
                                   " where  QuotaDate=Convert(date,'" & dtpDQDateSearch.Value.Date & "')", CboDQCustomers)

                QbtnNew_Click(sender, e)

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveDQGood.Click
        Dim msgrslt As MsgBoxResult
        msgrslt = MsgBox("متأكد من الحفظ", MsgBoxStyle.OkCancel)
        If msgrslt = MsgBoxResult.Cancel Then
            Exit Sub
        End If
        Try
            Dim DQGID As Integer
            Try
                DQGID = lstDQGoods.SelectedValue
            Catch ex As Exception
                DQGID = 0
            End Try

            If (gAdo.IFExists("select DailyQuotaGoodID From tblDailyQuotaWOContractGoods where [DailyQuotaID]=" & lstDQuta.SelectedValue.ToString & " and [GoodID]=" & clsDBStrings.SingleQot(cboDQGoodID.SelectedValue.ToString()))) Then
                MsgBox("تم إدخال هذا الصنف لهذه الكوته قبل ذلك")
                Exit Sub
            End If


            gDailyQuotaWOContractGoodO.DailyQuotaID = lstDQuta.SelectedValue
            gDailyQuotaWOContractGoodO.GoodID = cboDQGoodID.SelectedValue
            gDailyQuotaWOContractGoodO.Save()

            gAdo.CtrlItemsLoad("DailyQuotaGoodID", "Good_Name", "tblDailyQuotaWOContractGoods join tblGood on tblGood.Good_ID = tblDailyQuotaWOContractGoods.GoodID " & _
                               " where DailyQuotaID = " & lstDQuta.SelectedValue & " ORDER BY DailyQuotaGoodID ", lstDQGoods)
            btnNewDQGood.Enabled = True
            btnSaveDQGood.Enabled = False
            MsgBox("لقد تم الحفظ بنجاح")
            '' Code To Select Last Index 
            lstDQGoods.SelectedIndex = lstDQGoods.Items.Count - 1
            cboDQGoodID.Text = ""
            cboDQGoodID.SelectedIndex = -1
            btnNewDQGood_Click(Nothing, Nothing)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnNewDQGood_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewDQGood.Click
        Try

            gDailyQuotaWOContractGoodO.ID = 0

            If cboDQGoodID.Items.Count > 0 Then
                cboDQGoodID.SelectedIndex = 0
            End If
            
            btnNewDQGood.Enabled = True
            btnSaveDQGood.Enabled = True
            cboDQGoodID.Text = ""
            cboDQGoodID.SelectedIndex = -1
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnDeletDQGood_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeletDQGood.Click
        Try

            Dim msgrslt As MsgBoxResult
            msgrslt = MsgBox("هل ترغب فى حذف هذا العنصر ؟", MsgBoxStyle.OkCancel)
            If msgrslt = MsgBoxResult.Ok Then
                Dim intDQID As Integer
                intDQID = lstDQGoods.SelectedValue

                gDailyQuotaWOContractGoodO.ID = intDQID
                gDailyQuotaWOContractGoodO.Remove()
                MsgBox("تم الحذف بنجاح")
                gAdo.CtrlItemsLoad("DailyQuotaGoodID", "Good_Name", "tblDailyQuotaWOContractGoods join tblGood on tblGood.Good_ID = tblDailyQuotaWOContractGoods.GoodID " & _
                               " where DailyQuotaID = " & lstDQuta.SelectedValue & " ORDER BY DailyQuotaGoodID ", lstDQGoods)
                If lstDQGoods.Items.Count = 0 Then
                    btnNewDQGood_Click(sender, e)
                End If
            End If
            cboDQGoodID.Text = ""
            cboDQGoodID.SelectedIndex = -1
            gDailyQuotaWOContractGoodO.ID = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lstContractDet_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstContractDet.SelectedIndexChanged

    End Sub

    Private Sub lstDQGoods_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstDQGoods.SelectedIndexChanged
        Try
            If Val(Me.lstDQGoods.SelectedValue) = 0 Then
                Exit Sub
            End If
            
            gDailyQuotaWOContractGoodO.ID = Val(lstDQGoods.SelectedValue)
            gDailyQuotaWOContractGoodO.Refresh()

            cboDQGoodID.SelectedValue = gDailyQuotaWOContractGoodO.GoodID
            

            btnNewDQGood.Enabled = True
            btnSaveDQGood.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDQDateSearch.ValueChanged
        CboDQCustomers.Text = ""
        CboDQCustomers.SelectedIndex = -1
        lstDQuta.DataSource = Nothing
        lstDQGoods.DataSource = Nothing
        gAdo.CtrlItemsLoad(" distinct Dealer_ID", "Dealer_Name", "tblDealer join tblDailyQuotaWithOutContract on tblDailyQuotaWithOutContract.CustomerID = tblDealer.Dealer_ID " & _
                           " where  ((QuotaDate<=Convert(date,'" & dtpDQDateSearch.Value.Date & "') and finish=0) OR (EndDate=Convert(date,'" & dtpDQDateSearch.Value.Date & "') and finish=1))", CboDQCustomers)
    End Sub

    Private Sub CboDQCustomers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub CboDQCustomers_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboDQCustomers.SelectedValueChanged
        gAdo.CtrlItemsLoad("[DailyQuotaID]", "[DailyQuotaID]", "[tblDailyQuotaWithOutContract] where CustomerID=" & CboDQCustomers.SelectedValue & " and ((QuotaDate<=Convert(date,'" & dtpDQDateSearch.Value.Date & "') and finish=0) OR (EndDate=Convert(date,'" & dtpDQDateSearch.Value.Date & "') and finish=1)) ORDER BY QuotaDate", lstDQuta)
    End Sub

    Private Sub CboDQCustomers_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub dtpQuotaDate_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpQuotaDate.KeyDown, txtQuantity.KeyDown, CustomerID.KeyDown, chkfinish.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            SelectNextControl(DirectCast(sender, Control), True, True, True, True)
        End If
    End Sub

    Private Sub Label114_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label114.Click

    End Sub

    Private Sub Panel15_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel15.Paint

    End Sub


    Private Sub btnSearchQuota_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchQuota.Click
        If (cboCustID.Text.Trim() = "") Then
            MsgBox("يجب أختيار عميل")
            Exit Sub
        End If
        Try
            gAdo.CtrlItemsLoad("tblDebitNote.DebitNoteID", "DebitNote", "tblDailyQuota Join tblDebitNote on tblDebitNote.DebitNoteID=tblDailyQuota.DebitNoteID " & _
                                      " join tblContracts on tblDebitNote .ContractID =tblContracts .ContractID " & _
                                      " where tblContracts.Finished = 0 and tblDailyQuota.Finished=0 and tblContracts.CustomerID = " & cboCustID.SelectedValue & " and DateQuota <= '" & dtpDayDate.Value.Date & "'", DNoteID)
            Dim dt As New DataTable
            dt = gAdo.FillDataTable("Set DateFormat dmy Select T.Trans_ID,T.CID,TQ.Quantity,D.DebitNoteID,TQ.QuotaID From tblTransactions T join tblTransQuota TQ on(T.Trans_ID=TQ.TransID) Join tblDailyQuota D on(TQ.QuotaID=D.QuotaID) where TR_TY=2 and Out_Date is not null and T.Dealer_ID=" & cboCustID.SelectedValue.ToString() & " and Convert(Datetime,T.Out_Date) between '" & gMethods.GetShifName_And_SearchDateFT(True, dtpDayDate.Value.Date, dtpDayDate.Value.Date)(0) & "' and '" & gMethods.GetShifName_And_SearchDateFT(True, dtpDayDate.Value.Date, dtpDayDate.Value.Date)(1) & "'")
            Try
                dgTransactionsQouta.Rows.Clear()
            Catch ex As Exception

            End Try
            For i = 0 To dt.Rows.Count - 1
                dgTransactionsQouta.Rows.Add()
                dgTransactionsQouta.Rows(i).Cells(TID.Name).Value = dt.Rows(i)("Trans_ID")
                dgTransactionsQouta.Rows(i).Cells(TransID.Name).Value = dt.Rows(i)("CID")
                dgTransactionsQouta.Rows(i).Cells(NetWhight.Name).Value = dt.Rows(i)("Quantity")
                dgTransactionsQouta.Rows(i).Cells(DNoteID.Name).Value = dt.Rows(i)("DebitNoteID").ToString()
                gAdo.CtrlItemsLoad("QuotaID", "Cast(DATEPART(DD,DateQuota) as Varchar(2))+'/'+Cast(MONTH(DateQuota) as Varchar(2))+'/'+Cast(YEAR(DateQuota) as Varchar(4))", "tblDailyQuota where DebitNoteID=" & dt.Rows(i)("DebitNoteID").ToString() & " and DateQuota<='" & dtpDayDate.Value.Date & "' and Finished=0", QoutaDate)
                dgTransactionsQouta.Rows(i).Cells(QoutaDate.Name).Value = dt.Rows(i)("QuotaID").ToString()
            Next
        Catch ex As Exception

        End Try
       

    End Sub

    Private Sub dgTransactionsQouta_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTransactionsQouta.CellEndEdit
        Try
            If (dgTransactionsQouta.Rows(e.RowIndex).Cells(DNoteID.Index).Value <> Nothing) Then
                gAdo.CtrlItemsLoad("QuotaID", "Cast(DATEPART(DD,DateQuota) as Varchar(2))+'/'+Cast(MONTH(DateQuota) as Varchar(2))+'/'+Cast(YEAR(DateQuota) as Varchar(4))", "tblDailyQuota where DebitNoteID=" & dgTransactionsQouta.Rows(e.RowIndex).Cells(DNoteID.Index).Value.ToString() & " and DateQuota<='" & dtpDayDate.Value.Date & "'", QoutaDate)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgTransactionsQouta_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgTransactionsQouta.DataError

    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            Dim Statment As String

            For i = 0 To dgTransactionsQouta.Rows.Count - 1
                If (dgTransactionsQouta.Rows(i).Cells(TransID.Index).Value = Nothing) Then
                    Continue For
                End If
                Statment = "UpDate tblTransQuota set QuotaID=" & dgTransactionsQouta.Rows(i).Cells(QoutaDate.Index).Value.ToString() & " where TransID=" & dgTransactionsQouta.Rows(i).Cells(TID.Index).Value.ToString()
                gAdo.CmdExec(Statment)
            Next
            MsgBox("تم تعديل الكوتات")
            Close()

        Catch ex As Exception

        End Try
    End Sub




    Private Sub btnCountryAddnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCountryAddnew.Click
        gCountry.ID = 0
        txtCountry.Clear()
        txtCountry.Select()
        btnCountryAddnew.Enabled = False
        btnCountrySave.Enabled = True
    End Sub

    Private Sub btnCountrySave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCountrySave.Click
        If IsErrorMsg(txtCountry) = False Then

            'MsgBox(gCountry.ID)
            gCountry.Name = txtCountry.Text
            gCountry.Save()

            MsgBox("لقد تم الحفظ بنجاح")
            gAdo.CtrlItemsLoad("Country_ID", "Country_Name", "tblCountry where Country_ID>1 ORDER BY Country_Name", LstCountry)
            btnCountryAddnew.Enabled = True
            btnCountrySave.Enabled = False



        Else
            MessageBox.Show(gMsg, "البيانات غير مكتمله", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnCountryDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCountryDelete.Click
        Dim msgrslt As MsgBoxResult
        msgrslt = MsgBox("هل ترغب فى حذف هذا العنصر ؟", MsgBoxStyle.OkCancel)
        If msgrslt = MsgBoxResult.Ok Then
            Try
                If LstCountry.Items.Count = 0 Then
                    Exit Sub
                End If
                Dim CountryExist As Integer


                CountryExist = gAdo.CmdExecScalar("select count(trans_id) from dbo.tblTransactions WHERE MSGID  IN (Select Distinct MSGID From tblMessages WHERE MessageCountry = " & LstCountry.SelectedValue & ")")
                If CountryExist < 1 Then
                    gAdo.CmdExec("delete tblCountry where Country_ID=" & LstCountry.SelectedValue)
                    txtCountry.Text = ""
                    gCountry.ID = 0
                    gAdo.CtrlItemsLoad("Country_ID", "Country_Name", "tblCountry where Country_ID>1 ORDER BY Country_Name", LstCountry)

                    'gCountry.ID = 0
                Else
                    MsgBox("هذا البيان مستخدم من قبل لايمكن حذفه")
                End If
            Catch ex As Exception

            End Try
        Else
        End If
    End Sub

    Private Sub btnMsgAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMsgAddNew.Click
        gMessages.ID = 0
        txtMessages.Clear()
        txtMessages.Select()
        btnMsgAddNew.Enabled = False
        btnMsgSave.Enabled = True
    End Sub

    Private Sub btnMsgSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMsgSave.Click
        If IsErrorMsg(txtMessages) = False Then

            Dim mstrMsgName As String = txtMessages.Text
            gMessages.Name = txtMessages.Text
            gMessages.MessageCountry = cboCountry.SelectedValue
            gMessages.Save()
            gAdo.CtrlItemsLoad("Msg_ID", "Message", "tblMessages WHERE Msg_ID>1 ORDER BY Msg_ID", lstMessages)

            lstMessages.SelectedIndex = lstMessages.FindString(mstrMsgName)
            btnMsgAddNew.Enabled = True
            btnMsgSave.Enabled = False
            MsgBox("لقد تم الحفظ بنجاح")
        Else
            MessageBox.Show(gMsg, "البيانات غير مكتمله", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnMsgDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMsgDelete.Click
        Dim msgrslt As MsgBoxResult
        msgrslt = MsgBox("هل ترغب فى حذف هذا العنصر ؟", MsgBoxStyle.OkCancel)
        If msgrslt = MsgBoxResult.Ok Then
            Try
                If lstMessages.Items.Count = 0 Then
                    Exit Sub
                End If
                Dim MessageExist As Integer
                MessageExist = gAdo.CmdExecScalar("select count(trans_id) from tblTransactions where MSGID =" & lstMessages.SelectedValue)
                If MessageExist < 1 Then
                    gAdo.CmdExec("delete tblMessages where Msg_ID=" & lstMessages.SelectedValue)

                    gAdo.CtrlItemsLoad("Msg_ID", "Message", "tblMessages WHERE Msg_ID>1 ORDER BY Msg_ID", lstMessages)
                Else
                    MsgBox("هذا البيان مستخدم من قبل لايمكن حذفه")
                End If
            Catch ex As Exception

            End Try
        Else
        End If
    End Sub

    Private Sub btnPorNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPorNew.Click
        txtPortCD.Text = ""
        txtPortName.Clear()

        txtPortCD.Select()
        btnPorNew.Enabled = False
        btnPortSave.Enabled = True
    End Sub

    Private Sub btnPortSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPortSave.Click
        If IsErrorMsg(txtPortName) = False Then

            gPortO.ID = txtPortCD.Text
            gPortO.Name = txtPortName.Text



            gPortO.NOTIdentitySave()

            gAdo.CtrlItemsLoad("Port_CD", "Port_Name", "TblPorts ORDER BY Port_name", lstPorts)
            lstPorts.SelectedIndex = lstPorts.FindString(txtPortSearch.Text)
            BtnDealerAddNew.Enabled = True
            BtnDealerSave.Enabled = False
            MsgBox("لقد تم الحفظ بنجاح")
            'txtDealerID.Text = ""
            'txtDealerName.Text = ""
            'cboType.Text = ""
            btnPorNew_Click(sender, e)
        Else
            MessageBox.Show(gMsg, "البيانات غير مكتمله", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnPortDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPortDelete.Click
        Dim msgrslt As MsgBoxResult
        msgrslt = MsgBox("هل ترغب فى حذف هذا العنصر ؟", MsgBoxStyle.OkCancel)
        If msgrslt = MsgBoxResult.Ok Then
            Try
                If lstPorts.Items.Count = 0 Then
                    Exit Sub
                End If
                Dim CityExist As Integer
                CityExist = gAdo.CmdExecScalar("select count(trans_id) from dbo.tblTransactions where Port_CD=" & lstPorts.SelectedValue)
                If CityExist < 1 Then
                    gAdo.CmdExec("delete tblPorts where Port_CD=" & lstPorts.SelectedValue)
                    gAdo.CtrlItemsLoad("Port_CD", "Port_Name", "TblPorts ORDER BY Port_Name", lstPorts)
                Else
                    MsgBox("هذا البيان مستخدم من قبل لايمكن حذفه")
                End If
            Catch ex As Exception

            End Try
        Else
        End If
    End Sub

    Private Sub LstCountry_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LstCountry.SelectedIndexChanged
        Try
            If Val(Me.LstCountry.SelectedValue) = 0 Then
                Exit Sub
            End If
            gCountry.ID = LstCountry.SelectedValue
            gCountry.Refresh()
            txtCountry.Text = gCountry.Name
            btnCountryAddnew.Enabled = True
            btnCountrySave.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtSearchCountry_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchCountry.TextChanged
        LstCountry.SelectedIndex = LstCountry.FindString(txtSearchCountry.Text)
    End Sub

    Private Sub lstMessages_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstMessages.SelectedIndexChanged
        Try
            If Val(Me.lstMessages.SelectedValue) = 0 Then
                Exit Sub
            End If
            gMessages.ID = Val(lstMessages.SelectedValue)
            gMessages.Refresh()
            txtMessages.Text = gMessages.Name
            cboCountry.SelectedValue = gMessages.MessageCountry
            btnMsgAddNew.Enabled = True
            btnMsgSave.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtMsgSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMsgSearch.TextChanged
        lstMessages.SelectedIndex = lstMessages.FindString(txtMsgSearch.Text)
    End Sub

    Private Sub lstPorts_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstPorts.SelectedIndexChanged
        Try
            If Val(Me.lstPorts.SelectedValue) = 0 Then
                Exit Sub
            End If

            gPortO.ID = lstPorts.SelectedValue
            gPortO.Refresh()
            txtPortCD.Text = gPortO.ID
            txtPortName.Text = gPortO.Name


            btnPorNew.Enabled = True
            btnPortSave.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtPortSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPortSearch.TextChanged
        lstPorts.SelectedIndex = lstPorts.FindString(txtPortSearch.Text)
    End Sub

    Private Sub Label161_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label161.Click

    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDisplayName.TextChanged

    End Sub

    Private Sub EmailSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmailSearch.TextChanged
        lstEmails.SelectedIndex = LstUser.FindString(EmailSearch.Text)
    End Sub

    Private Sub lstEmails_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstEmails.SelectedIndexChanged
        Try
            If Val(Me.lstEmails.SelectedValue) = 0 Then
                Exit Sub
            End If
            gEmailFromO.ID = Val(lstEmails.SelectedValue)
            gEmailFromO.Refresh()

            txtEmail.Text = gEmailFromO.Email
            txtemailPassword.Text = gEmailFromO.Password
            txtEmailPort.Text = gEmailFromO.Port
            chkEnableSsl.Checked = gEmailFromO.EnableSsl
            txtHost.Text = gEmailFromO.Host
            txtDisplayName.Text = gEmailFromO.DisplayName
            ChkUseDefaultCredentials.Checked = gEmailFromO.UseDefaultCredentials

            btnAddEmail.Enabled = True
            btnSaveEmail.Enabled = True
           
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnAddEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddEmail.Click
        gEmailFromO.ID = 0
        txtEmail.Clear()
        txtemailPassword.Clear()
        txtEmailPort.Clear()
        txtHost.Clear()
        txtDisplayName.Clear()

        btnAddEmail.Enabled = False
        btnSaveEmail.Enabled = True
        chkEnableSsl.Checked = False
        ChkUseDefaultCredentials.Checked = False
    End Sub

    Private Sub btnSaveEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveEmail.Click

        gEmailFromO.Email = txtEmail.Text.Trim()
        gEmailFromO.Password = txtemailPassword.Text.Trim()
        gEmailFromO.Port = txtEmailPort.Text.Trim()
        gEmailFromO.Host = txtHost.Text.Trim()
        gEmailFromO.DisplayName = txtDisplayName.Text.Trim()
        gEmailFromO.EnableSsl = chkEnableSsl.Checked
        gEmailFromO.UseDefaultCredentials = ChkUseDefaultCredentials.Checked
        
        gEmailFromO.Save()
        gAdo.CtrlItemsLoad("ID", "Email", "tblFromEmail", lstEmails, "", "")
        lstEmails.SelectedIndex = lstEmails.FindString(txtEmail.Text.Trim())
        btnAddEmail_Click(Nothing, Nothing)
        
        
        


        MsgBox("لقد تم الحفظ بنجاح")
        
    End Sub

    Private Sub btnDeleteEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteEmail.Click
        Dim msgrslt As MsgBoxResult
        msgrslt = MsgBox(" Delete Sure ؟", MsgBoxStyle.OkCancel)
        If msgrslt = MsgBoxResult.Ok Then
            Try
                If lstEmails.Items.Count = 0 Then
                    Exit Sub
                End If



                gAdo.CmdExec("delete tblFromEmail where ID=" & lstEmails.SelectedValue)

                gEmailFromO.ID = 0
                gAdo.CtrlItemsLoad("ID", "Email", "tblFromEmail", lstEmails)
                btnAddEmail_Click(nothing,Nothing)

            Catch ex As Exception

            End Try
        Else
        End If
    End Sub

    Private Sub txttoEmailSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttoEmailSearch.TextChanged
        lstToEmails.SelectedIndex = LstUser.FindString(txttoEmailSearch.Text)
    End Sub

    Private Sub lstToEmails_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstToEmails.SelectedIndexChanged
        Try
            If Val(Me.lstToEmails.SelectedValue) = 0 Then
                Exit Sub
            End If
            gToEmailO.ID = Val(lstToEmails.SelectedValue)
            gToEmailO.Refresh()

            txtToEmail.Text = gToEmailO.Email
            

            btnToEmailADD.Enabled = True
            btnToEmailSave.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnToEmailADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnToEmailADD.Click
        gToEmailO.ID = 0
        txtToEmail.Clear()
    End Sub

    Private Sub btnToEmailSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnToEmailSave.Click
        gToEmailO.Email = txtToEmail.Text.Trim()
        

        gToEmailO.Save()
        gAdo.CtrlItemsLoad("ID", "Email", "tblEmails", lstToEmails, "", "")
        lstToEmails.SelectedIndex = lstToEmails.FindString(txtToEmail.Text.Trim())
        btnToEmailADD_Click(Nothing, Nothing)

       



        MsgBox("لقد تم الحفظ بنجاح")
    End Sub

    Private Sub btntoemailDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntoemailDelete.Click
        Dim msgrslt As MsgBoxResult
        msgrslt = MsgBox(" Delete Sure ؟", MsgBoxStyle.OkCancel)
        If msgrslt = MsgBoxResult.Ok Then
            Try
                If lstToEmails.Items.Count = 0 Then
                    Exit Sub
                End If



                gAdo.CmdExec("delete tblEmails where ID=" & lstToEmails.SelectedValue)

                gToEmailO.ID = 0
                gAdo.CtrlItemsLoad("ID", "Email", "tblEmails", lstToEmails)
                btnToEmailADD_Click(Nothing, Nothing)

            Catch ex As Exception

            End Try
        Else
        End If
    End Sub


End Class