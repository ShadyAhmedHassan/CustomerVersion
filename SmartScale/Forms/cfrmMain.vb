Option Explicit On
Imports System.Math
Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.IO
Imports System.IO.Ports
Imports System.Transactions
Imports System.Xml

Public Class cfrmMain
    Public intScaleCount As Integer = 0
    Public NOPrinted As Integer
    ' meke this PC as A client
    Dim ClearControls As Boolean = True
    Dim tcpClient As New System.Net.Sockets.TcpClient()
    'Dim IP As String = gAdo.CmdExecScalar("select Scale_IP from tblscale")
    'Dim Port As Integer = gAdo.CmdExecScalar("select Scale_PortNo from tblscale")
    Public Shared SharedTxtTransID, SharedTxtCarNo, SharedtxtLicence, SharedtxtNotes As New TextBox
    Public Shared SharedlblFrstWeight, SharedlblSecondWeight As New Label
    Public Shared SharedCboDriver, SharedCboDealer, SharedCboIssueTo, SharedCboGood, _
    SharedCboCarCity, SharedCboCarInfoCity As ComboBox
    Public Shared SharedRichTxt As RichTextBox
    Dim PTransID, PFisrtScale, PSecondScale, PNetScale, PCarNo, PCarCity, PIssueto, PTruckNo, _
    PTruckCity, PDriverName, PDriverLicence, PGoodName, PDealerName, PArabic, PRecieveType As String
    Dim AutoPrint As Boolean = False
    Public Shared serl As New SerialPort
    Public Shared ScaleNo As Integer = 1 ' here write the number of scale id
    Dim ID As Integer
    Dim SQLTrans As SqlTransaction
    Dim SIsUpdate As Boolean = False
    Dim TakeAction As Boolean = False
    Dim NoChangeCheck As Boolean = False
    ' ~~~~~~~~~~~~~~ COMBOBOXS ~~~~~~~~~~~~~~~
    Public Shared CboGoods As ComboBox
    ' ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Dim intCountMizane As Integer

    Dim Scale As String = ""
#Region " MY DATA MEMBER"
    Dim TSql As String
    REM THIS IS FOR CBO 
    Dim VAL_cboTruckNo As Boolean
    Dim VAL_cboTruckCity As Boolean
    Dim VAL_cboDriver As Boolean
    Dim VAL_cboDealer As Boolean
    Dim VAL_cboIssueTo As Boolean
    Dim VAL_cboGoodType As Boolean
    REM THIS IS for textbox
    Dim val_txtTransID As Boolean
    Dim val_txtCarNo As Boolean
    Dim val_cboCarCity As Boolean
    Dim val_txtLicenceNo As Boolean


    Dim Mywhere As String
    Dim start_to_btnSearch As Boolean = True
    Dim SearchwithoutInput As Int16
    Dim Ds As New DataSet


#End Region

#Region "Enumerators"
    Private Enum enmScreenView
        NewTransaction = 0
        TodayTransaction = 1
    End Enum

    Private Enum enmValidationResult
        Valid = 1
        Empty = 0
        NewItem = -1
    End Enum
#End Region

#Region "Declarations"
    Private WithEvents Tmr As New Timer
    'Public TruckCtrls() As Control
    Private CarCtrls() As Control
    Private mobjClockFont As Font = Nothing
    Private menvCurrentView As enmScreenView
    Private RndTimer As Integer = 0
    Private mTabEvent As New System.Windows.Forms.KeyEventArgs(Keys.Tab)
#End Region

#Region "Form"

    Private Sub cfrmMain_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress

    End Sub

    'Private Sub cfrmMain_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
    '    On Error Resume Next
    '    Me.Refresh()
    'End Sub
    Dim intContractor_ID As Integer
    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            'Eng Ahmad fawzy
            
            btnSlip0.Text = "بدون"
            btnSlip1.Text = dsSlipRate.Tables(0).Rows(1).Item(0).ToString
            btnSlip2.Text = dsSlipRate.Tables(0).Rows(2).Item(0).ToString
            btnSlip3.Text = dsSlipRate.Tables(0).Rows(3).Item(0).ToString

            Label3.Text = gAdo.CmdExecScalar("select company_name from tblCompany")

            gScaleO.ID = cfrmMain.ScaleNo
            gScaleO.Refresh()
            lblScaleName.Text = gScaleO.Name
     
            If gTransID > 0 Then

            Else



                If serl.IsOpen Then
                    ' Don't do any thing
                Else

                    With serl
                        .PortName = "COM1"
                        .BaudRate = 9600
                        .DataBits = 8
                        .StopBits = StopBits.One
                        .Parity = Parity.None
                        '.Handshake = Handshake.None
                    End With
                    serl.Open()
                End If

                'pnlNewTransaction.BringToFront()
                Dim visible As Integer
                visible = gAdo.CmdExecScalar("select visible from tblvisible")
                If visible = 0 Then
                    btnSetSecondWeight.Visible = False
                    btnSetFirstWeight.Visible = False
                Else
                    btnSetSecondWeight.Visible = True
                    btnSetFirstWeight.Visible = True
                End If

                txtCarNo.Focus()
                txtCarNo.Select()
                txtCarNo.SelectAll()
                SharedTxtTransID = txtTransID
                SharedTxtCarNo = txtCarNo
                SharedCboDealer = cboDealer
                SharedCboDriver = cboDriver
                SharedCboGood = cboGoodType
                SharedCboIssueTo = cboIssueTo
                SharedlblFrstWeight = lblFrstWeight
                SharedlblSecondWeight = lblScndWeight
                SharedRichTxt = RichTextBox1
                SharedCboCarCity = cboCarCity
                SharedCboCarInfoCity = cboTruckCity
                SharedtxtLicence = txtLicenceNo
                pnlOption.Width = 50
                txtCustomWeight.Text = ""
                txtCustomWeight.Enabled = False

                'IsUpdate = gIsUpdate
                ''connect to the server using the IP and PortNO

                'gScaleO.ID = 2
                'gScaleO.Refresh()
                '' 18/2/2020
                'gTransactionO.Refresh()

                ReDim CarCtrls(1)
                CarCtrls(0) = cboCarCity
                'CarCtrls(1) = cboDealer

                ReDim gTruckCtrls(8)

                gTruckCtrls(0) = cboTruckNo
                gTruckCtrls(1) = cboTruckCity
                gTruckCtrls(2) = cboDriver
                gTruckCtrls(3) = txtLicenceNo
                gTruckCtrls(4) = cboIssueTo
                gTruckCtrls(5) = cboGoodType
                gTruckCtrls(6) = CboSlip
                gTruckCtrls(7) = cboDealer
                gTruckCtrls(8) = txtLicenceNo
                REM  Call InitCamera()
                REM  Call InitSoundEffects()
                REM  Call InitSystemTimer()
                REM  Me.CurrentView = enmScreenView.NewTransactions




                CboRecieveType.Text = ""
                Call RefreshShiftInfo()
                'txtTransID.Text = gAdo.CmdExecScalar("select isnull( max (CID),0)from tblTransactions where TR_TY=2 and  First_Weigth_Scale_ID=" & ScaleNo) + 1
                'Dim intmaxCID As Integer
                'intmaxCID = gAdo.CmdExecScalar("select isnull( max (CID),0)from tblTransactions where TR_TY=2 ")

                ''' 4/12/2019 Elsaman '''
                ''' Var for New CID Between countFrom and countTo TO Use more than One Scale
                Dim intCID As Integer
                intCID = gAdo.CmdExecScalar("select isnull(max(CID),0) as CID from tblTransactions where CID >= (select countFrom from tblScale where Scale_ID = " & ScaleNo & ") " & _
                                            " and CID <= (select countTo from tblScale where Scale_ID = " & ScaleNo & ") and TR_TY = 2")


                If intCID = 0 Then
                    txtTransID.Text = gAdo.CmdExecScalar("select countFrom  from tblScale where Scale_ID=" & cfrmMain.ScaleNo)
                Else
                    txtTransID.Text = intCID + 1
                End If
                'txtTransID.Text = gAdo.CmdExecScalar("select isnull( max (CID),0)from tblTransactions where TR_TY=2 ") + 1
                CboRecieveType.SelectedIndex = 0
                Call FrmRefresh()
                '''' Elsaman 08/01/2019 ''''
                '''' هذا الكود لاجل تثبيت المقاول لكي يثبت المستخدم المقاول طوال اليوم ولايحتاج الى ادخاله مره اخرى ''''

                intContractor_ID = gAdo.CmdExecScalar("select Contractor_ID from tblTransactions where CID " & _
                                                      " in (select isnull( max (CID),0)from tblTransactions where TR_TY=2) and TR_TY = 2")
                cboContractor.SelectedValue = intContractor_ID


                '' Code For chklstBoxScale 
                chklstBoxScale.DataSource = GObjDataBaseS.FillDT("select * from tblScale where Scale_IsDeleted = 0")

                chklstBoxScale.DisplayMember = "Scale_Name"
                chklstBoxScale.ValueMember = "Scale_ID"

                Dim dtCheckedScale As New DataTable
                If ScaleNo = 0 Then
                Else
                    Dim Locatin As Integer = gAdo.CmdExecScalar("select Location from tblScale where Scale_IsDeleted = 0 and Scale_ID = " & ScaleNo)
                    dtCheckedScale = GObjDataBaseS.FillDT("select Scale_ID from tblScale where Scale_IsDeleted = 0 and Location = " & Locatin)
                    For i = 0 To chklstBoxScale.Items.Count - 1

                        If dtCheckedScale.Select("scale_ID=" & chklstBoxScale.Items(i)(0).ToString).Length > 0 Then
                            chklstBoxScale.SetItemCheckState(i, CheckState.Checked)
                        End If

                    Next
                End If
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'btnSetSecondWeight.Enabled = False
                btnSecondScale.Enabled = False
                btnSlip0.BackColor = Color.DarkGray
                btnSlip1.BackColor = Color.DarkGray
                btnSlip2.BackColor = Color.DarkGray
                btnSlip3.BackColor = Color.DarkGray
            End If
            cboLocation.SelectedValue = gScaleO.LocatinID.ID

            gUserO.ID = gUserID
            gUserO.Refresh()
            If gUserO.UserType = "n" Then
                chklstBoxScale.Enabled = False
                cboLocation.Enabled = False
            End If

        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Sub

    Sub FrmRefresh()
        'Fill Comboboxes [Issue To - Dealer - Goods - Driver - City - TruckNo - Slip]
        gAdo.CtrlItemsLoad("Issue_To_ID", "Issue_To_Name", "tblIssueTo  ORDER BY Issue_To_Name", cboIssueTo)
        gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", "TblDealer where Type in (1,3) ORDER BY Dealer_Name", cboDealer)
        gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood  where Good_ID=1 OR Type in (2,3) ORDER BY Good_Name desc", cboGoodType)
        gAdo.CtrlItemsLoad("Driver_ID", "Driver_Name", "tblDriver ORDER BY Driver_Name", cboDriver)
        gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity ORDER BY City_Name", cboCarCity, "", "")
        gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity ORDER BY City_Name", cboTruckCity, "", "")
        gAdo.CtrlItemsLoad("Car_Info_ID", "TruckBoard_No", "tblCarInfo  ORDER BY TruckBoard_No", cboTruckNo, "", "")
        gAdo.CtrlItemsLoad("Contractor_ID", "Contractor_Name", "tblContractor ORDER BY Contractor_Name", cboContractor, "", "")
        gAdo.CtrlItemsLoad("LocationID", "LocationName", "tblLocation ORDER BY LocationID", cboLocation, "", "")
        'gAdo.CtrlItemsLoad("Slip_ID", "Slip_Name", "tblSlip", CboSlip, "", "Slip_IsDeleted='false' ORDER BY Slip_Name")

        'gAdo.CtrlItemsLoad("Issue_To_ID", "Issue_To_Name", "tblIssueTo ORDER BY Issue_To_Name", cboIssueTo)
        'gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", "TblDealer ORDER BY Dealer_Name", cboDealer)
        'gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood ORDER BY Good_Name", cboGoodType)
        'gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity ORDER BY City_Name", cboCarCity, "", "")
        'gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity ORDER BY City_Name", cboTruckCity, "", "")
        'gAdo.CtrlItemsLoad("Slip_ID", "Slip_Name", "tblSlip", CboSlip, "", "Slip_IsDeleted='false' ORDER BY Slip_Name")


        cboIssueTo.SelectedIndex = -1
        cboDealer.SelectedIndex = -1
        cboGoodType.SelectedIndex = -1
        cboDriver.SelectedIndex = -1
        cboCarCity.SelectedIndex = -1
        cboTruckCity.SelectedIndex = -1
        CboSlip.SelectedIndex = -1
        cboTruckNo.SelectedIndex = -1
        CboRecieveType.SelectedIndex = -1
        cboContractor.SelectedIndex = -1
        TakeAction = True
    End Sub

    Private Sub cfrmMain_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        On Error Resume Next
        'Call CleanupSoundEffects()
        'Call TerminateApplication()
        TmrFirstWeight.Enabled = False
        TmrSecondWeight.Enabled = False
        txtCarTimer.Enabled = False
        gfrmChoose.Show()
        serl.Close()
    End Sub

#End Region

#Region "Controls"
#Region "Good"

    ' ''Private Sub cboTruckNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTruckNo.TextChanged, cboDriver.TextChanged

    ' ''    Select Case sender.name
    ' ''        Case cboTruckNo.Name
    ' ''            'Get Truck_City
    ' ''            gMethods.IsTruckExist(cboTruckNo, cboTruckCity)
    ' ''            'To know if the truck_NO is (used with another Car or not used with another Car or New )
    ' ''            Validate_Board_NO(cboTruckNo)
    ' ''        Case Is = cboDriver.Name
    ' ''            'Get Licence_No
    ' ''            gMethods.IsDriverExist(cboDriver, txtLicenceNo)
    ' ''            'To know if the Driver is (used with another Car or not used with another Car or New )
    ' ''            Validate_Board_NO(cboDriver)
    ' ''    End Select

    ' ''End Sub 'Good

    Private Sub txtCarNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCarNo.KeyDown
        Try
            If e.KeyCode = Keys.Tab Or e.KeyCode = Keys.Enter Then
                'txtCarTimer_Tick(sender, e)
                'Dim CarID As Integer
                'CarID = gAdo.CmdExecScalar("select car_id from tblCar where carboard_no ='" & txtCarNo.Text & "'")
                'gTransID = gAdo.CmdExecScalar("Select isnull(max(Trans_ID),0) From tblTransactions Where Car_ID=" & CarID)
                'ProcessTabKey(True)
                e.SuppressKeyPress = True
                cboCarCity.Focus()
            Else
                txtCarTimer.Enabled = False
                txtCarTimer.Enabled = True
                'Dim CarID As Integer
                'CarID = gAdo.CmdExecScalar("select car_id from tblCar where carboard_no ='" & txtCarNo.Text & "'")
                'gTransID = gAdo.CmdExecScalar("Select isnull(max(Trans_ID),0) From tblTransactions Where Car_ID=" & CarID)
            End If
        Catch ex As Exception

        End Try

        'Dim TranCount As Integer
        'TranCount = gAdo.CmdExecScalar("select count (trans_id) from tbltransactions where trans_id =" & txtTransID.Text & "")

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        'Clear All Form Controls Preparing for a new transaction
        'Unlock All Controls
        'gMethods.IsLock(False, cboCarCity, cboTruckNo, cboTruckCity, cboDriver, txtLicenceNo, cboDealer)
        Dim LID As Integer
        'LID = cboLocation.SelectedValue
        'Dim LText As String = cboLocation.Text.Trim()

        gTransID = 0
        txtTransID.ReadOnly = True
        ClearCtrls()
        txtCarNo.Focus()
        lblInDate.Text = Now
        'txtTransID.Text = gAdo.CmdExecScalar("select isnull( max (CID),0)from tblTransactions where TR_TY=2 and  First_Weigth_Scale_ID=" & ScaleNo) + 1

        ''' 4/12/2019 Elsaman '''
        ''' Var for New CID Between countFrom and countTo TO Use more than One Scale
        Dim intCID As Integer
        intCID = gAdo.CmdExecScalar("select isnull(max(CID),0) as CID from tblTransactions where CID >= (select countFrom from tblScale where Scale_ID = " & ScaleNo & ") " & _
                                    " and CID <= (select countTo from tblScale where Scale_ID = " & ScaleNo & ") and TR_TY = 2")


        If intCID = 0 Then
            txtTransID.Text = gAdo.CmdExecScalar("select countFrom  from tblScale where Scale_ID=" & cfrmMain.ScaleNo)
        Else
            txtTransID.Text = intCID + 1
        End If

        'txtTransID.Text = intCID + 1

        'txtTransID.Text = gAdo.CmdExecScalar("select isnull( max (CID),0)from tblTransactions where TR_TY=2 ") + 1
        btnFirstScale.Enabled = True
        btnEditFirstScale.Enabled = True
        btnSecondScale.Enabled = True
        lblFirstUser.Text = ""
        lblSecondUser.Text = ""
        chkCustomWeight.Checked = False
        txtCustomWeight.Text = ""
        SIsUpdate = False

        btnSlip0.BackColor = Color.DarkGray
        btnSlip1.BackColor = Color.DarkGray
        btnSlip2.BackColor = Color.DarkGray
        btnSlip3.BackColor = Color.DarkGray

        CboRecieveType.Text = ""

        '''' هذا الكود لاجل تثبيت المقاول لكي يثبت المستخدم المقاول طوال اليوم ولايحتاج الى ادخاله مره اخرى ''''
        cboContractor.SelectedValue = intContractor_ID
        ''' Elsaman 9/12/2019 '''
        EnabledControls()

        'btnSetSecondWeight.Enabled = False
        btnSecondScale.Enabled = False
        cboLocation.SelectedValue = gScaleO.LocatinID.ID

    End Sub

    Private Sub txtCarTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCarTimer.Tick

        txtCarTimer.Enabled = False
        Cursor = Cursors.WaitCursor
        lblInDate.Text = Now
        If gTransID <> 0 Then
            txtTransID.Text = gTransID
        End If
        Cursor = Cursors.Default
        gTransID = 0
    End Sub

#End Region


    Private Sub AllCbo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTruckNo.SelectedIndexChanged, cboDriver.SelectedIndexChanged
        If TakeAction Then


            Select Case sender.name

                Case cboTruckNo.Name
                    'Get Truck_City
                    'MsgBox("1-" & cboTruckNo.SelectedValue)
                    'gMethods.IsTruckExist(cboTruckNo, cboTruckCity)

                    'To know if the truck_NO is (used with another Car or not used with another Car or New )
                    'Validate_Board_NO(cboTruckNo)
                Case Is = cboDriver.Name
                    'Get Licence_No
                    gMethods.IsDriverExist(cboDriver, txtLicenceNo)
                    'To know if the Driver is (used with another Car or not used with another Car or New )
                    Validate_Board_NO(cboDriver)
            End Select
        End If
        'MsgBox("2-" & cboTruckNo.SelectedValue)

        ' MsgBox()
    End Sub

    'Private Sub cboTruckNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTruckNo.Validated
    '    'CheckTruck()
    'End Sub

    Private Sub cboDriver_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDriver.Validated
        '   gMethods.IsDriverExist(cboDriver, txtLicenceNo)
        'MsgBox("s")
    End Sub

    Private Sub AllRad_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radFrstFull.CheckedChanged, radFrstEmpty.CheckedChanged

        If sender Is radFrstEmpty Then
            gMethods.RadioChecked(radFrstEmpty, radScndFull)
        ElseIf sender Is radFrstFull Then
            gMethods.RadioChecked(radFrstFull, radScndEmpty)
        End If
    End Sub

    Private Sub btnAddPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPayment.Click

        'Dim dblValue As Double
        ''On Error Resume Next
        'If radFrstEmpty.Checked = True Then
        '    dblValue = gMethods.GetSlip(Val(lblFrstWeight.Text))
        'ElseIf radScndEmpty.Checked = True Then
        '    dblValue = gMethods.GetSlip(Val(lblScndWeight.Text))
        'End If
        'If MsgBox("قيمة المعامل " & dblValue.ToString(), MsgBoxStyle.YesNo + MsgBoxStyle.Information) = MsgBoxResult.No Then
        '    Exit Sub
        'Else
        '    gWeightRate = dblValue
        'End If

        'My.Computer .Audio .Play (My.Resources ."تختار اسم الملف من اللى موجود")
        Try
            My.Computer.Audio.Play(My.Resources.Done, AudioPlayMode.Background)
        Catch ex As Exception

        End Try


    End Sub

    Private Sub btntimer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.TimerRandom.Enabled = False
        Me.TimerRandom.Enabled = True
    End Sub

    Private Sub TimerRandom_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerRandom.Tick
        If RndTimer < 500 Then
            If gIsUpdate = True Then
                gMethods.RandNumbers(lblScndWeight, lblWeight, lblNetWeight, lblFrstWeight)
            Else
                gMethods.RandNumbers(lblFrstWeight, lblWeight, lblNetWeight, lblScndWeight)
            End If
        Else
            TimerRandom.Enabled = False
            RndTimer = 0
        End If
        RndTimer += 2
    End Sub

    Private Sub lblFrstWeight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblScndWeight.TextChanged, lblFrstWeight.TextChanged


        If Val(lblScndWeight.Text) = 0 Then
            lblNetWeight.Text = 0
        Else
            lblNetWeight.Text = Abs(Val(lblFrstWeight.Text) - Val(lblScndWeight.Text))
        End If

    End Sub

#End Region

#Region "Shift"
    Private Sub btnLogOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogOff.Click
        On Error Resume Next
        If DoLogin() Then
            Call RefreshShiftInfo()
        Else

            'Call Me.Close()
        End If
    End Sub

    Private Sub RefreshShiftInfo()
        On Error Resume Next
        lblShiftEmpName.Text = gUserName
        PicBoxUser.Image = gUserPic
        lblShiftName.Text = gMethods.GetShifName_And_SearchDate(False)
        lblShiftLoginTime.Text = "تسجيل الدخول منذ " & gLoginTime.ToString("HH:mm:ss")
    End Sub

#End Region

    '#Region "Sound Effects"
    '    ''' <summary>
    '    ''' Loads list of available sound effects to the sound context menu
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub InitSoundEffects()
    '        On Error Resume Next
    '        Call MsgBox("Load Sound files to menu code goes here")

    '        'To Do: 
    '        'Loop on all files at TruckMessagesPath Property
    '        'For each file name remove the .wav extension from the right hand side -- Do not make replace
    '        'add the result to the context menu then provide a handler for the menu item click just like mnuSoundMenuItem1_Click

    '        'Example:
    '        AddHandler mnuSoundMenuItem1.Click, AddressOf OnSoundEffectItemClicked
    '        AddHandler mnuSoundMenuItem2.Click, AddressOf OnSoundEffectItemClicked
    '    End Sub
    '    Private Sub btnSoundEffect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSoundEffect.Click
    '        On Error Resume Next
    '        Call mnuSoundMenu.Show(Me, btnSoundEffect.Left + btnSoundEffect.Width, btnSoundEffect.Top + btnSoundEffect.Height)
    '        Call mnuSoundMenuItem1.Select()
    '    End Sub
    '    Private Sub OnSoundEffectItemClicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        On Error Resume Next
    '        Call My.Computer.Audio.Play(GetTruckMessageFullPath(sender.Text))
    '    End Sub
    '    ''' <summary>
    '    ''' Remove all associated handlers
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub CleanupSoundEffects()
    '        On Error Resume Next
    '        'Loop on all menu items then remove the handler associated at OnSoundEffectItemClicked
    '        RemoveHandler mnuSoundMenuItem1.Click, AddressOf OnSoundEffectItemClicked
    '    End Sub
    '#End Region

    '#Region "Camera"
    '    ''' <summary>
    '    ''' Initalize Camera procedure
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub InitCamera()
    '        On Error Resume Next
    '        mobjClockFont = New Font(Me.Font.Name, 18, FontStyle.Regular, GraphicsUnit.Pixel, 0)
    '        tmrCameraNow.Enabled = True
    '        'tmrCameraNow_Tick(Nothing, Nothing)
    '    End Sub
    '    'Private Sub tmrCameraNow_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrCameraNow.Tick
    '    '    On Error Resume Next
    '    '    pctCamerNow.Invalidate()
    '    'End Sub

    '    Private Sub pctCamerNow_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pctCamerNow.Paint
    '        On Error Resume Next
    '        Dim objGraphics As System.Drawing.Graphics = e.Graphics
    '        pctCamerNow.Image = GetCurrentCameraImage()
    '        Call objGraphics.DrawString(Now.ToString("yyyy/MM/dd"), mobjClockFont, Brushes.OrangeRed, 0, 0)
    '        Call objGraphics.DrawString(Now.ToString("HH:mm:ss"), mobjClockFont, Brushes.OrangeRed, 0, mobjClockFont.GetHeight() + 1)
    '    End Sub

    '    ''' <summary>
    '    ''' Gets last captured frame from the camera thread
    '    ''' </summary>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Private Function GetCurrentCameraImage() As Image
    '        On Error Resume Next
    '        GetCurrentCameraImage = My.Resources.Camera
    '    End Function
    '#End Region

#Region "Switch View"
    ''' <summary>
    ''' Selects current user view (New Transaction or Transactions grid view)
    ''' </summary>
    ''' <value></value>
    ''' <returns>Current Active User View</returns>
    ''' <remarks></remarks>
    ''' 

    Private Property CurrentView() As enmScreenView
        Get
            Return menvCurrentView
        End Get
        Set(ByVal value As enmScreenView)
            menvCurrentView = value
            pnlNewTransaction.Visible = False
            pnlTransactions.Visible = False
            Select Case menvCurrentView
                Case enmScreenView.NewTransaction
                    btnSwitchView.Text = "حركات اليوم"
                    pnlNewTransaction.Visible = True
                Case enmScreenView.TodayTransaction
                    btnSwitchView.Text = "جديد"
                    pnlTransactions.Visible = True
            End Select
            Me.Refresh()
        End Set
    End Property

    ''' <summary>
    ''' Toggles current view from new transaction to Transactions view
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    Private Sub SwitchView()
        On Error Resume Next
        Me.CurrentView += 1
        If Me.CurrentView > enmScreenView.TodayTransaction Then
            Me.CurrentView = enmScreenView.NewTransaction
        End If
    End Sub

    Private Sub btnGotoTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGotoTransaction.Click
        If DataGridView1.RowCount > 0 Then
            'txtTransID.Text = CStr(DataGridView1.CurrentRow.Cells(0).Value)
            SwitchMode(txtCarNo, cboCarCity, cboTruckNo, cboTruckCity, cboDriver, txtLicenceNo, _
                       cboDealer, cboIssueTo, cboGoodType)
            txtCarNo.Text = CStr(DataGridView1.CurrentRow.Cells(1).Value)
            gTransID = CStr(DataGridView1.CurrentRow.Cells(0).Value)
            txtTransID.Text = CStr(DataGridView1.CurrentRow.Cells(0).Value)
            ID = CStr(DataGridView1.CurrentRow.Cells(0).Value)
            LoadData()
            'txtCarTimer_Tick(sender, e)
            '13-11-2013
            'lblInDate.Text = CDate(DataGridView1.CurrentRow.Cells(4).Value) & vbCrLf & CDate(DataGridView1.CurrentRow.Cells(5).Value)

            'If IsDBNull((DataGridView1.CurrentRow.Cells(6).Value)) = False Or IsDBNull((DataGridView1.CurrentRow.Cells(7).Value)) = False Then
            '    lblOutDate.Text = CDate(DataGridView1.CurrentRow.Cells(6).Value) & vbCrLf & CDate(DataGridView1.CurrentRow.Cells(7).Value)
            'Else
            '    lblOutDate.Text = ""
            'End If
        End If
        Me.pnlTransactions.SendToBack()
        btnSwitchView.Text = "حركات اليوم"
        rdbCarsInside.Checked = False
    End Sub

#End Region

#Region "Validation and Save/Print"

#Region "Validation"
    Private Function Validate() As Boolean
        On Error Resume Next
        Call MsgBox("Validation code goes here")

        If ValidateEmptyControl(txtCarNo, "رقم السيارة") = False Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Check a certain control (Textbox/Combobox) for empty user data entry then returns current state.
    ''' </summary>
    ''' <param name="objControl">Control to validate</param>
    ''' <param name="strItemName">Missing item will be displayed for user validation message</param>
    ''' <returns>One of enmValidationResult results</returns>
    ''' <remarks></remarks>
    Private Function ValidateEmptyControl(ByVal objControl As Object, Optional ByVal strItemName As String = "") As enmValidationResult
        Dim intIndex As Integer = -1
        On Error Resume Next

        ValidateEmptyControl = enmValidationResult.Valid    'Assume Valid till proved otherwise
        Select Case TypeName(objControl).ToLower()
            Case "textbox"
                If objControl.Text.Trim().Length = 0 Then
                    ValidateEmptyControl = enmValidationResult.Empty
                End If
            Case "combobox"

                If objControl.Text.Trim().Length > 0 Then
                    If objControl.SelectedIndex = -1 Then
                        intIndex = objControl.FindStringExact(objControl.Text.Trim())
                        If intIndex >= 0 Then
                            objControl.SelectedIndex = intIndex
                        Else
                            'New item
                            ValidateEmptyControl = enmValidationResult.NewItem
                        End If
                    End If
                Else
                    ValidateEmptyControl = enmValidationResult.Empty
                End If
        End Select



        If ValidateEmptyControl = enmValidationResult.Empty Then
            If strItemName.Length <> 0 Then
                Call MsgBox("برجاء إدخال قيمة " & Chr(34) & strItemName & Chr(34), MsgBoxStyle.Exclamation, "بيان ناقص")
            End If
            Call objControl.SelectAll()
            Call objControl.Focus()
        End If
    End Function
#End Region

#Region "Save"

    Private Sub btnSaveAndPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveAndPrint.Click
        'On Error Resume Next
        If Val(cboDealer.SelectedValue) <= 2 Then
            MsgBox("من فضلك إختار عميل")
            Exit Sub
        End If
        If cboGoodType.SelectedValue <= 1 Then
            MsgBox("من فضلك إختار صــنـــف")
            Exit Sub
        End If

        If Val(lblFrstWeight.Text) < 100 Then
            MsgBox("من فضلك إقرأ الوزنة الأولى")
            Exit Sub
        End If

        Dim LText As String = cboLocation.Text

        Dim msgResult As MsgBoxResult
        msgResult = MsgBox("هـل ترغـب فى الحفــظ ؟", MsgBoxStyle.OkCancel, " الحفظ  ")
        If msgResult = MsgBoxResult.Ok Then
            If txtCarNo.Text = "" Then
                MsgBox("من فضلك ادخل  رقم السياره")
                txtCarNo.Focus()
                Exit Sub
            End If

            Try


                If chkCustomWeight.Checked Then

                    If txtCustomWeight.Text = "" Then
                        txtCustomWeight.Text = 0
                    End If

                    If Decimal.Parse(txtCustomWeight.Text.ToString) < Decimal.Parse(lblNetWeight.Text.ToString) Then
                        MsgBox("          لن يتم حفظ الوزنه الثانيه" & vbCrLf & "الوزن الصافى اكبر من الوزن المسموح به")
                        Exit Sub
                    End If

                End If

            Catch ex As Exception

            End Try

            'Try
            '    Dim SQl As String
            '    SQl = "Select DQ.Quota From tblContracts C left join tblDebitNote DN on(C.ContractID=DN.ContractID) left join tblDailyQuota DQ on(DN.DebitNoteID=DQ.DebitNoteID) where  C.CustomerID='" + cboDealer.SelectedValue.ToString() + "' and '" + cboGoodType.SelectedValue.ToString + "' in(Select CD.GoodID From tblContractDet CD where C.ContractID=CD.ContractID)  and DQ.DateQuota=Convert(date,GetDate())"
            '    Dim Quta As Decimal
            '    Quta = gAdo.CmdExecScalar(SQl)
            '    SQl = "Select Sum(T.Second_Weight) From tblTransactions T where T.Dealer_ID='" + cboDealer.SelectedValue.ToString() + "' and T.Good_ID in(Select CD.GoodID From tblContractDet CD Left Join tblContracts C on(CD.ContractID=C.ContractID) Left Join tblDebitNote DN on(C.ContractID=Dn.ContractID) Left Join tblDailyQuota DQ on(DN.DebitNoteID=DQ.DebitNoteID) where C.CustomerID='" + cboDealer.SelectedValue.ToString() + "' and DQ.DateQuota=Convert(date,GetDate()))" 'and Cast(T.In_Date as Date)=Convert(date,GetDate())" and CID<>" + ID
            '    Dim TW As Decimal
            '    TW = gAdo.CmdExecScalar(SQl)
            '    If (TW + Decimal.Parse(lblNetWeight.Text.ToString) > Quta) Then
            '        MsgBox("الوزن يزيد عن الكوتا اليوميه")
            '        Exit Sub
            '    End If
            'Catch ex As Exception

            'End Try

            TakeAction = False
            If Save() Then

                Call MsgBox("Print code goes here")
            End If
            If ClearControls = False Then
                ClearControls = True
            Else
                'txtTransID.Text = gAdo.CmdExecScalar("select isnull( max (CID),0)from tblTransactions where  TR_TY =2 and  First_Weigth_Scale_ID=" & ScaleNo) + 1
                'txtTransID.Text = gAdo.CmdExecScalar("select isnull( max (CID),0)from tblTransactions where  TR_TY =2 ") + 1

                ''' 4/12/2019 Elsaman '''
                ''' Var for New CID Between countFrom and countTo TO Use more than One Scale
                
                



                txtTransID.Text = gAdo.CmdExecScalar("select isnull(max(CID),0) as CID from tblTransactions where tblTransactions.CID >= (select countFrom from tblScale where Scale_ID = " & ScaleNo & ") " & _
                 " and tblTransactions.CID  <= (select countTo from tblScale where Scale_ID = " & ScaleNo & ") and TR_TY = 2") + 1
                txtCarNo.Focus()
                btnFirstScale.Enabled = True
                btnSecondScale.Enabled = False
                'btnSetSecondWeight.Enabled = False

                btnSlip0.BackColor = Color.DarkGray
                btnSlip1.BackColor = Color.DarkGray
                btnSlip2.BackColor = Color.DarkGray
                btnSlip3.BackColor = Color.DarkGray
                ''' Elsaman 24/12/2019 '''
                btnNew_Click(sender, e)
                cboLocation.Text = LText
            End If
        Else
            Exit Sub
        End If

    End Sub

    Private Function Save() As Boolean
        Try

            'CheckCar()
            CheckCtrlValues()
            'CheckCar()
            ReFill()
            cboCarCity.SelectedValue = gCarCityID
            cboTruckCity.SelectedValue = gTruckCityID
            cboDriver.SelectedValue = gDriverO.ID
            cboIssueTo.SelectedValue = gIssueToO.ID
            cboDealer.SelectedValue = gDealerO.ID
            cboGoodType.SelectedValue = gGoodO.ID
            cboTruckNo.SelectedValue = gCarInfoID
            cboContractor.SelectedValue = gContractorO.ID

            '' Added By Eng Ahmad Fawzy

            CommitTrans()
            If SaveOrCancel = 1 Then
                If ClearControls = False Then
                Else
                    ClearCtrls()
                    txtCarNo.Text = ""
                    lblFirstUser.Text = ""
                    lblSecondUser.Text = ""
                    chkCustomWeight.Checked = False
                    txtCustomWeight.Text = ""
                    txtCarNo.Select()
                    'CheckCar()
                    '''ComeHere

                    Call FrmRefresh()
                    'End If
                End If
            End If
            SaveOrCancel = 1
        Catch ex As Exception

        End Try
    End Function

#End Region

#End Region

    '#Region "System Events"
    '    ''' <summary>
    '    ''' Starts System Timer to manage background work
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub InitSystemTimer()
    '        On Error Resume Next
    '        tmrSystemTimer.Enabled = True
    '        'tmrSystemTimer_Tick(Nothing, Nothing)
    '    End Sub
    '    'Private Sub tmrSystemTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSystemTimer.Tick
    '    '    On Error Resume Next
    '    '    Call RefreshShiftInfo()
    '    '    'To Do : Refresh Current Weight Measure
    '    'End Sub
    '#End Region

#Region "Search"

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        frmSearchTrans.ShowDialog()
        If frmSearchTrans.SearchOrCancel = 0 Then

        Else
            Search(Val(frmSearchTrans.SharedTxtTrans.Text.Trim))
        End If

    End Sub

#End Region

#Region "Form Methods"

    ''' <summary>
    ''' Automatically Check each control and validate its value against the value in database 
    ''' </summary>
    ''' <remarks></remarks>

    Public Sub CheckCtrlValues()

        Try

            If gAdo.GetCount("City_Name", "tblCity", cboCarCity.Text.Trim, ClsAdo.Datatype.Characters) > 0 Then
                gCityO.ID = gAdo.GetColumnID("City_ID", "tblCity", "City_Name", cboCarCity.Text.Trim)
                gCarCityID = gCityO.ID
            Else
                gCityO.ID = 0
                gCityO.Name = cboCarCity.Text.Trim
                gCityO.Save()
                gCarCityID = gAdo.GetMax("City_Id", "tblcity")

            End If
            'If gAdo.CmdExecScalar("SELECT ISNULL ((select Count(*) from tblCar where CarBoard_No ='" & txtCarNo.Text.Trim & "' AND CarBoard_City_ID = (SELECT city_ID from tblCity WHERE City_Name = '" & cboCarCity.Text & "')),0)") > 0 Then

            'End If
            'If gAdo.CmdExecScalar("SELECT ISNULL ((select Count(*) from tblCar where CarBoard_No ='" & txtCarNo.Text.Trim & "' ),0)") > 0 Then
            If gAdo.CmdExecScalar("SELECT ISNULL ((select Count(*) from tblCar where CarBoard_No ='" & txtCarNo.Text.Trim & "' AND CarBoard_City_ID = (SELECT city_ID from tblCity WHERE City_Name = '" & cboCarCity.Text & "')),0)") > 0 Then
                ' gCarO.ID = gAdo.GetColumnID("Car_ID", "tblCar", "CarBoard_No", txtCarNo.Text.Trim)
                ' gCarO.ID = gAdo.CmdExecScalar("select Car_ID from tblCar where CarBoard_No ='" & txtCarNo.Text.Trim & "' ")
                gCarO.ID = gAdo.CmdExecScalar("select Car_ID from tblCar where CarBoard_No ='" & txtCarNo.Text.Trim & "' AND CarBoard_City_ID = (SELECT city_ID from tblCity WHERE City_Name = '" & cboCarCity.Text & "')")
            Else
                gCarO.ID = 0
                gCarO.CarBoard_City_ID.ID = gCityO.ID
                gCarO.Name = txtCarNo.Text.Trim
                gCarO.ScaleID = ScaleNo
                gCarO.Save()
                gCarO.ID = gAdo.GetMax("Car_ID", "tblCar")
            End If



            If gAdo.GetCount("Dealer_Name", "tblDealer", cboDealer.Text.Trim, ClsAdo.Datatype.Characters) > 0 Then
                gDealerO.ID = gAdo.GetColumnID("Dealer_ID", "tblDealer", "Dealer_Name", cboDealer.Text.Trim)
            Else
            End If

            If gAdo.GetCount("Driver_Name", "tblDriver", cboDriver.Text.Trim, ClsAdo.Datatype.Characters) > 0 Then
                gDriverO.ID = gAdo.GetColumnID("Driver_ID", "tblDriver", "Driver_Name", cboDriver.Text.Trim)
            Else
                gDriverO.ID = 0
                gDriverO.Name = cboDriver.Text.Trim
                gDriverO.Driver_License_No = txtLicenceNo.Text.Trim
                gDriverO.ScaleID = ScaleNo
                gDriverO.Save()
                gDriverO.ID = gAdo.GetMax("Driver_ID", "tblDriver")
            End If

            If gAdo.GetCount("Issue_To_Name", "tblIssueTo", cboIssueTo.Text.Trim, ClsAdo.Datatype.Characters) > 0 Then
                gIssueToO.ID = gAdo.GetColumnID("Issue_To_ID", "tblIssueTo", "Issue_To_Name", cboIssueTo.Text.Trim)
            Else
                gIssueToO.ID = 0
                gIssueToO.Name = cboIssueTo.Text.Trim
                gIssueToO.ScaleID = ScaleNo
                gIssueToO.IssueTo_Address = ""
                gIssueToO.IssueTo_Fax = ""
                gIssueToO.IssueTo_Field = ""
                gIssueToO.IssueTo_Mobile = ""
                gIssueToO.IssueTo_Telephone = ""
                gIssueToO.ScaleID = ScaleNo
                gIssueToO.Save()
                gIssueToO.ID = gAdo.GetMax("Issue_To_ID", "tblIssueTo")
            End If


            If gAdo.GetCount("City_Name", "tblCity", cboTruckCity.Text.Trim, ClsAdo.Datatype.Characters) > 0 Then
                gTruckCityID = gAdo.GetColumnID("City_ID", "tblCity", "City_Name", cboTruckCity.Text.Trim)
            Else
                If cboTruckCity.Text = "" And cboTruckNo.Text = "" Then
                Else
                    gCityO.ID = 0
                    gCityO.Name = cboTruckCity.Text.Trim
                    gCityO.Save()
                    gTruckCityID = gAdo.GetMax("City_Id", "tblcity")
                End If

            End If

            If gAdo.GetCount("Contractor_Name", "tblContractor", cboContractor.Text.Trim, ClsAdo.Datatype.Characters) > 0 Then
                gContractorO.ID = gAdo.GetColumnID("Contractor_ID", "tblContractor", "Contractor_Name", cboContractor.Text.Trim)
            Else
                If cboContractor.Text = "" And cboContractor.Text = "" Then
                Else
                    gContractorO.ID = 0
                    gContractorO.Name = cboContractor.Text.Trim

                    gContractorO.Save()
                    gContractorO.ID = gAdo.GetMax("Contractor_Id", "tblContractor")
                End If

            End If

            If gAdo.CmdExecScalar("SELECT ISNULL ((select COUNT(*) from tblCarInfo where TruckBoard_No ='" & cboTruckNo.Text.Trim & "' AND TruckBoard_City_ID = (SELECT city_ID from tblCity WHERE City_Name = '" & cboTruckCity.Text & "')),0)") > 0 Then
                'gCarInfoO.ID = gAdo.GetColumnID("Car_Info_ID", "tblCarInfo", "TruckBoard_No", cboTruckNo.Text.Trim)
                gCarInfoO.ID = gAdo.CmdExecScalar("select Car_Info_ID from tblCarInfo where TruckBoard_No ='" & cboTruckNo.Text.Trim & "' AND TruckBoard_City_ID = (SELECT city_ID from tblCity WHERE City_Name = '" & cboTruckCity.Text & "')")
                gCarInfoID = gCarInfoO.ID
            Else
                If cboTruckNo.Text = "" Then
                Else
                    gCarInfoO.ID = 0
                    gCarInfoO.TruckBoard_City_ID.ID = gTruckCityID
                    gCarInfoO.Name = cboTruckNo.Text.Trim
                    gCarInfoO.ScaleID = ScaleNo
                    gCarInfoO.Save()
                    gCarInfoO.ID = gAdo.GetMax("Car_Info_ID", "tblCarInfo")
                    gCarInfoID = gCarInfoO.ID
                End If
            End If

            If gAdo.GetCount("Good_name", "TblGood", cboGoodType.Text.Trim, ClsAdo.Datatype.Characters) Then
                gGoodO.ID = gAdo.GetColumnID("Good_ID", "tblGood", "Good_Name", cboGoodType.Text.Trim)
                Dim hh2 As String
                hh2 = gAdo.GetColumnID("Good_ID", "tblGood", "Good_Name", cboGoodType.Text.Trim)
                hh2 = hh2
            Else
            End If

            '''   Added by Eng Ahmad fawzy
            If gAdo.GetCount("Slip_MaxRange", "tblSlip", CboSlip.Text.Trim, ClsAdo.Datatype.Characters) > 0 Then
                gSlipo.ID = gAdo.GetColumnID("Slip_ID", "tblSlip", "Slip_MaxRange", CboSlip.Text.Trim)
                'gCarInfoID = gCarInfoO.ID
            Else
                If CboSlip.Text = "" Then
                Else
                    'CboSlip.ID = 0
                    'CboSlip.TruckBoard_City_ID.ID = gTruckCityID
                    'CboSlip.Name = cboTruckNo.Text.Trim
                    'CboSlip.ScaleID = ScaleNo
                    'CboSlip.Save()

                End If
            End If
            Dim hh As String
            ' Testing
            hh = cboGoodType.SelectedValue
            hh = cboGoodType.SelectedValue
        Catch ex As Exception

        End Try

    End Sub

    ''' <summary>
    ''' Loops through labels used to show the weight and clear it
    ''' </summary>
    ''' <remarks></remarks>

    Public Sub ClearlblWeight()
        For Each Ctrl As Control In Me.Controls
            If TypeOf Ctrl Is Label Then
                If Ctrl.Name.Contains("Weight") Then
                    Ctrl.Text = CStr(0) & " KG"
                End If
            End If
        Next
    End Sub

    Sub ClearCtrls()
        
        gMethods.ClearObj(Me, "ComboBox")


        gMethods.ClearObj(Me, "TextBox")
        cboGoodType.SelectedIndex = -1
        cboContractor.SelectedIndex = -1
        'Clear Weight Labels
        lblFrstWeight.Text = CStr(0) & " KG"
        lblScndWeight.Text = CStr(0) & " KG"
        lblNetWeight.Text = CStr(0) & " KG"
        lblWeight.Text = CStr(0) & " KG"
        lblShowWeight.Text = 0
        'لابد من ترجيعه
        ''ChangeCtrlColor(True)
        'Clear Date Labels
        lblInDate.Text = ""
        lblOutDate.Text = ""
        ' Eng Ahmad fawzy
        gTransactionO.Slip_Rate = 0

        ''' Elsaman 8/12/2019 '''
        txtNotes.Text = ""

    End Sub

    Public Sub CheckCar()
        'Unlock All Controls
        gMethods.IsLock(False, CarCtrls)
        'gMethods.IsLock(False, gTruckCtrls)

        gMethods.IsNewCar(txtCarNo, cboCarCity, cboTruckNo, cboDriver, cboGoodType, _
                                          cboIssueTo, cboTruckCity, CboSlip, cboDealer, radFrstEmpty, _
                                         radFrstFull, txtTransID, _
                                          lblInDate, lblFrstWeight, _
                                          lblOutDate, lblScndWeight)


    End Sub

    Public Sub SwitchMode(ByVal ParamArray Ctrls() As Control)
        If gIsSearch = False Then
            'Transaction Mode, Switch To Search Mode
            'Unlock All Controls
            txtTransID.ReadOnly = False
            gMethods.IsLock(False, Ctrls)
            'Removing Handlers to Controls [txtCarNo - cboTruckNo - cboDriver] 
            RemoveHandler txtCarNo.KeyDown, AddressOf txtCarNo_KeyDown
            RemoveHandler cboTruckNo.SelectedIndexChanged, AddressOf AllCbo_SelectedIndexChanged
            ' RemoveHandler cboTruckNo.Validated, AddressOf cboTruckNo_Validated
            RemoveHandler cboDriver.SelectedIndexChanged, AddressOf AllCbo_SelectedIndexChanged
            RemoveHandler cboDriver.Validated, AddressOf cboDriver_Validated
            'Load Controls for Selection [CarCity - TruckNo - Truck City - Drivers - Dealers]
            gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity", cboCarCity)
            gAdo.CtrlItemsLoad("Car_Info_ID", "TruckBoard_No", "tblCarInfo", cboTruckNo)
            gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity", cboTruckCity)
            gAdo.CtrlItemsLoad("Driver_ID", "Driver_Name", "tblDriver", cboDriver)
            gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", "tblDealer where Type=1 ", cboDealer)
            'Disable Controls [btnNew - btnSave - btnAddPayment]
            btnNew.Enabled = False
            btnSaveAndPrint.Enabled = False
            'btnAddPayment.Enabled = False
            'Change btnSearch Text
            btnSearch.Text = "بحــــــــــث"
            gIsSearch = True
        Else
            'Search Mode, Switch To Transaction Mode
            'Adding Handlers to Controls [txtCarNo - cboTruckNo - cboDriver] 
            AddHandler txtCarNo.KeyDown, AddressOf txtCarNo_KeyDown
            AddHandler cboTruckNo.SelectedIndexChanged, AddressOf AllCbo_SelectedIndexChanged
            ' AddHandler cboTruckNo.Validated, AddressOf cboTruckNo_Validated
            AddHandler cboDriver.SelectedIndexChanged, AddressOf AllCbo_SelectedIndexChanged
            AddHandler cboDriver.Validated, AddressOf cboDriver_Validated
            'Enable Controls [btnNew - btnSave - btnAddPayment]
            btnNew.Enabled = True
            btnSaveAndPrint.Enabled = True
            btnAddPayment.Enabled = True
            'Change btnSearch Text
            btnSearch.Text = "إعداد البــــــــحث"
            gIsSearch = False
        End If
    End Sub

    Function GetEndDate() As String
        Dim ShiftStartHour As Integer
        ShiftStartHour = gAdo.CmdExecScalar("Set DateFormat dmy Select Datepart(Hour,convert(Datetime,S.Shift_Start_Time,103)) From tblShift S where Shift_IsFirst=1")
        Dim CurDate As Date
        CurDate = Date.Now.Date
        If (Now.Hour >= 0 And Now.Hour < ShiftStartHour) Then
            CurDate = Date.Now.AddDays(-1)
        End If

        Dim EndDate As String = ""
        EndDate = CurDate.ToShortDateString()
        Return EndDate
    End Function

    Function CommitTrans() As String
        'gTransactionO.Car_ID.ID
        'gAdo.CmdExec("update tblCar set CarBoard_No='" & txtCarNo.Text & "' where Car_ID= '" & gCarO.ID & "'", False)
        gTransactionO.Car_ID.ID = gCarO.ID
        Dim strCar As String
        strCar = gCarO.ID
        If cboDriver.SelectedIndex = 0 Or cboDriver.SelectedValue = Nothing Then
            gTransactionO.Driver_ID.ID = 1
        Else
            gTransactionO.Driver_ID.ID = gDriverO.ID
        End If
        If cboTruckNo.SelectedIndex = 0 Or cboTruckNo.SelectedValue = Nothing Then
            gTransactionO.Car_Info_ID.ID = 1
        Else
            gTransactionO.Car_Info_ID.ID = gCarInfoID
        End If
        If cboIssueTo.SelectedIndex = 0 Or cboIssueTo.SelectedValue = Nothing Then
            gTransactionO.Issue_To_ID.ID = 1
        Else
            gTransactionO.Issue_To_ID.ID = gIssueToO.ID
        End If

        If cboContractor.SelectedIndex = 0 Or cboContractor.SelectedValue = Nothing Then
            gTransactionO.Contractor_ID.ID = 1
        Else
            gTransactionO.Contractor_ID.ID = gContractorO.ID
        End If

        gTransactionO.First_Weigth = CDec(Val(lblFrstWeight.Text.Trim))
        'gTransactionO.First_Weight_User_ID.ID = gUserID
        gTransactionO.CustomWeight = CDec(Val(txtCustomWeight.Text.Trim))

        gTransactionO.First_Weigth_Scale_ID.ID = 1
        If cboGoodType.SelectedValue = Nothing Then
            gTransactionO.Good_ID.ID = 1
        Else
            gTransactionO.Good_ID.ID = gGoodO.ID
        End If

        ''' Elsaman 8/12/2019 '''
        If txtNotes.Text = "" Then
            gTransactionO.Notes = ""
        Else
            gTransactionO.Notes = txtNotes.Text
        End If


        ''Eng Ahmad Fawzy 
        'If CboSlip.SelectedValue = Nothing Then
        '    gTransactionO.Slip_Rate = 0
        'Else
        '    gTransactionO.Slip_Rate = gSlipo.Slip_MaxRange
        'End If

        gTransactionO.First_Weight_User_ID.ID = gUserID
        Dim MsgID As Integer
        If CboRecieveType.Text = "" Then
            MsgID = 4
        ElseIf CboRecieveType.Text = "إسكندرية" Then
            MsgID = 1
        ElseIf CboRecieveType.Text = "العامرية" Then
            MsgID = 2
        End If


        gTransactionO.Slip_Charged_User_ID = 0
        gTransactionO.Second_Weight_Scale_ID.ID = 1
        gTransactionO.Second_Weight = CDec(Val(lblScndWeight.Text.Trim))
        gTransactionO.Second_Weight_User_ID.ID = gUserID
        If cboDealer.SelectedValue = Nothing Or cboDealer.SelectedIndex = 0 Then
            gTransactionO.Dealer_ID.ID = 1
        Else
            gTransactionO.Dealer_ID.ID = gDealerO.ID
        End If
        Dim IsUpdate As Boolean
        IsUpdate = SIsUpdate
        If radFrstEmpty.Checked = True Then
            gTransactionO.First_Weight_IsEmpty = True
        ElseIf radFrstFull.Checked = True Then
            gTransactionO.First_Weight_IsEmpty = False
        End If
        If radScndEmpty.Checked = True Then
            gTransactionO.Second_Weight_IsEmpty = True
        ElseIf radScndFull.Checked = True Then
            gTransactionO.Second_Weight_IsEmpty = False
        End If
        If cboLocation.SelectedValue = Nothing Or cboLocation.SelectedIndex = 0 Then
            gTransactionO.LocationID = 1
        Else
            gTransactionO.LocationID = cboLocation.SelectedValue
        End If

        Dim Str As String = ""

        Try

        Catch ex As Exception
            'MsgBox(ex)
        End Try
        'If Then

        '    MsgBox("لم يتم حفظ  الوزنة الأولى", MsgBoxStyle.O)
        'End If
        If Val(lblFrstWeight.Text) = 0 Then
            My.Computer.Audio.Play(My.Resources.beep_05, AudioPlayMode.Background)
            MsgBox("لم يتم حفظ  الوزنة الأولى")
        End If

        If SIsUpdate = False Then

            ' ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            Dim count As Integer
            'count = gAdo.CmdExecScalar("select count (CID) from tblTransactions where TR_TY=2 and  CID = " & txtTransID.Text & " and First_Weigth_Scale_ID=" & ScaleNo)
            count = gAdo.CmdExecScalar("select count (CID) from tblTransactions where TR_TY=2 and  CID = " & txtTransID.Text & " ")
            If count > 0 Then
                'If IsDBNull(gAdo.CmdExecScalar("select Out_Date from tblTransactions where TR_TY=2 and  CID =" & txtTransID.Text & " and First_Weigth_Scale_ID=" & ScaleNo)) = False Then
                If IsDBNull(gAdo.CmdExecScalar("select Out_Date from tblTransactions where TR_TY=2 and  CID =" & txtTransID.Text & " ")) = False Then
                    'GoTo LK
                    MsgBox("للتعديل من فضلك اضغط زر تعديل")
                    ClearControls = False
                    Exit Function
                End If
            Else

            End If
            ' ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            Dim Today As String
            Today = Now
            If Today.Contains("ص") Then
                Today = Today.Replace("ص", "AM")
            ElseIf Today.Contains("م") Then
                Today = Today.Replace("م", "PM")
            End If

            'Dim Lastweight As Decimal
            'Lastweight = gAdo.CmdExecScalar("Select Isnull(LastWeight,0) From tblScale where Scale_ID=" & ScaleNo)

            'If ((Lastweight - Val(lblFrstWeight.Text) >= 0 And Lastweight - Val(lblFrstWeight.Text) <= 150) Or (Val(lblFrstWeight.Text) - Lastweight >= 0 And Val(lblFrstWeight.Text) - Lastweight <= 150)) Then
            '    gAdo.CmdExec("Insert tblControlScale(UserID,MoveDate,MoveWeight) Values(" & gTransactionO.First_Weight_User_ID.ID & ",'" & Today & "'," & lblFrstWeight.Text & ")")
            'End If

            'If (Val(lblFrstWeight.Text) > 0) Then
            '    gAdo.CmdExec("UpDate tblScale set LastWeight=" & lblFrstWeight.Text & " where Scale_ID=" & ScaleNo)
            'End If

            Str = "set dateformat dmy insert into tblTransactions (In_Date,In_Shift_Date,In_Shift_ID,Car_ID,Driver_ID," _
            & "Car_Info_ID,Dealer_ID,Issue_To_ID " & _
           " ,First_Weigth,First_Weight_IsEmpty,First_Weigth_Scale_ID,First_Weight_User_ID,Second_Weight_IsEmpty,Good_ID,Slip_Rate" & _
           ",Slip_Charged_Date,Slip_Charged_User_ID,MSGID,Second_Weight,TR_TY,CustomWeight,CID,Contractor_ID,Notes,LocationID)  " & _
            "Values( "


            Dim CID As Integer
            'CID = gAdo.CmdExecScalar("select isnull( max (CID),0)from tblTransactions where TR_TY=2 ")

            CID = gAdo.CmdExecScalar("select isnull(max(CID),0) as CID from tblTransactions where CID >= (select countFrom from tblScale where Scale_ID = " & ScaleNo & ") " & _
                                        " and CID <= (select countTo from tblScale where Scale_ID = " & ScaleNo & ") and TR_TY = 2")

            If CID = 0 Then
                CID = gAdo.CmdExecScalar("select countFrom  from tblScale where Scale_ID=" & cfrmMain.ScaleNo)
                'CID = CID + 1
            Else
                CID = CID + 1
            End If
            'Dim CID As Integer = gAdo.CmdExecScalar("select isnull( max (CID)+1,0)from tblTransactions where  TR_TY =2 and First_Weigth_Scale_ID=" & ScaleNo)
            'Dim CID As Integer = gAdo.CmdExecScalar("select isnull( max (CID)+1,1)from tblTransactions where  TR_TY =2 ")

            '"13257" _
            Str &= "'" & Today & "','" & SetShift_DateTime() & "'," & gShiftO.ID & "," & gTransactionO.Car_ID.ID _
            & "," & gTransactionO.Driver_ID.ID & "," & gTransactionO.Car_Info_ID.ID & ",'" & gTransactionO.Dealer_ID.ID & "'" & _
            "," & gTransactionO.Issue_To_ID.ID & "," & gTransactionO.First_Weigth & ",'" & gTransactionO.First_Weight_IsEmpty & "'," & _
            ScaleNo & "," & gTransactionO.First_Weight_User_ID.ID & _
            ",'" & gTransactionO.Second_Weight_IsEmpty & "','" & gTransactionO.Good_ID.ID & "'," & gTransactionO.Slip_Rate & ",'" & Now.Date & "'," & _
            gTransactionO.Slip_Charged_User_ID & ",'" & MsgID & "',0 ,2," & gTransactionO.CustomWeight & "," & CID & "," & gTransactionO.Contractor_ID.ID & "," & "'" & gTransactionO.Notes & "'" & "," & gTransactionO.LocationID & ")"

            gAdo.CmdExec(Str, False)

            'Dim MaxIDofCuurentScale, MAXALL As Integer
            'MaxIDofCuurentScale = gAdo.CmdExecScalar("select isnull( max (CID)+1,0)from tblTransactions where  TR_TY =2 and First_Weigth_Scale_ID=" & ScaleNo)
            'MAXALL = gAdo.CmdExecScalar("select isnull( max (trans_id),0)from tblTransactions")
            'gAdo.CmdExec("update tblTransactions set CID =" & Val(MaxIDofCuurentScale + 1) & " where trans_id=" & MAXALL & " and First_Weigth_Scale_ID=" & ScaleNo)

            MsgBox("تم حفظ بيانات الوزنه الاولى بنجاح", , " الحفظ  ")
            SaveOrCancel = 1
            SIsUpdate = False



            ' حفظ بياتات الوزنة الثانية
        ElseIf SIsUpdate = True And Val(lblFrstWeight.Text) > 100 And Val(lblScndWeight.Text > 100) Then

            If cboGoodType.SelectedValue = 1 Or cboGoodType.SelectedValue = Nothing Then
                MsgBox("من فضلك اختار صنف ")
                SaveOrCancel = 0
                ClearControls = False
                Exit Function
            End If
            If cboDealer.SelectedValue = Nothing Or cboDealer.SelectedValue = 1 Then
                MsgBox("من فضلك اختار عميل ")
                SaveOrCancel = 0
                ClearControls = False
                Exit Function
            End If

            ' ~~~~~~~~~~~~~~~~~ TH ~~~~~~~~~~~~~~~~~~~~~~~~~~ Start ~~~
            ' هنا بشوف الكميه اللى دخلها للعميل علشان اخصم منها وانبهه
            Dim msgResult As MsgBoxResult
            Dim Qnty, CustCount, OverQnty, SumQnty As String
            Dim CQ_ID As Integer
            Dim QDR As SqlDataReader
            'CustCount = gAdo.CmdExecScalar("select isnull(count(CustomerID),0) from tblCustQnty " _
            '& "where CustomerID=" & cboDealer.SelectedValue & "")
            'If CustCount = 0 Then
            '    MsgBox("من فضلك ادخل كمية للعميل ليتم الخصم منها")
            '    SaveOrCancel = 0
            '    ClearControls = False
            '    Exit Function
            'Else
            '    SaveOrCancel = 1
            '    QDR = gAdo.CmdExecReader("select * from tblCustQnty where CQ_ID =(SELECT MAX(CQ_ID) FROM tblCustQnty  where  CustomerID='" & cboDealer.SelectedValue & "')" _
            '                           & " and CustomerID='" & cboDealer.SelectedValue & "'")
            '    While QDR.Read
            '        Qnty = QDR("Quantity")
            '        OverQnty = QDR("OverQuantity")
            '        CQ_ID = QDR("CQ_ID")
            '    End While
            '    QDR.Close()
            '    SumQnty = gAdo.CmdExecScalar("select isnull(sum(Quntity),0) from tblCustQntyDetails where CQ_ID=" & CQ_ID & "")
            'End If


            ''' Elsaman 22/12/2019 '''

            Dim Time As String
            Time = Now
            If Time.Contains("ص") Then
                Time = Time.Replace("ص", "AM")
            Else
                Time = Time.Replace("م", "PM")
            End If


            If SaveOrCancel = 1 Then

                ''' Elsaman 7/1/2020 '''
                ''' this code for shift with quota

                '' Variable From tblShift ====> " وقت نهاية الوردية "
                Dim Shift_End_Time As DateTime
                Shift_End_Time = gAdo.CmdExecScalar(CStr("Select RIGHT(CONVERT(VARCHAR, Shift_End_Time, 100),15) as Time From tblShift where Shift_End_Date = 1"))

                'CONVERT(datetime, Out_Date) <'" & gMethods.GetShifName_And_SearchDate(True)(0) & "'"

                '' Date OF Weight  ===> " تاريخ الوزن "
                Dim WeightTime As DateTime = TimeOfDay


                '' Variable For Test and Comparison
                Dim DateSearch As DateTime = Now
                If WeightTime < Shift_End_Time Then
                    DateSearch = Now.AddDays(-1)
                Else
                    DateSearch = Now
                End If
                DateSearch = DateSearch.Date.ToShortDateString
                ''' Elsaman 18/12/2019 '''
                Dim WhereCondition As String

                WhereCondition = " and TR_TY=2 and  Out_Date is not null and  CONVERT(date, Out_Date) between '" & gMethods.GetShifName_And_SearchDate(True)(0) & "' and '" & gMethods.GetShifName_And_SearchDate(True)(1) & "'"

                'Dim intDebitNoteID As Integer
                'intDebitNoteQuantityID = gAdo.CmdExecScalar("select ContractDetID from tblContractDet join tblContracts on tblContracts .ContractID = tblContractDet .ContractID " & _
                ' " join tblDealer on tblDealer .Dealer_ID = tblContracts .CustomerID join tblGood on tblGood .Good_ID = tblContractDet .GoodID " & _
                '" and tblDealer.Dealer_ID = " & cboDealer.SelectedValue & " and tblGood.Good_ID = '" & cboGoodType.SelectedValue & "'")
                'intDebitNoteID = gAdo.CmdExecScalar(" set dateformat dmy select distinct tblDailyQuota.DebitNoteID  from tblDailyQuota    " & _
                '"  join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID   " & _
                ' " join tblContracts on tblDebitNote .ContractID =tblContracts .ContractID    " & _
                ' " and customerID= " & cboDealer.SelectedValue & " and DateQuota =  CONVERT(date,'" & DateSearch & "')")

                '' TotalweightTrans + current weight
                Dim TotalweightTransWithCurrentWeight As Decimal

                Dim TotalDailyQuota As Decimal

                Dim Quota As Decimal
                Dim OutQuantityfromQuota As Decimal
                Dim RemainQuotaQuantity As Decimal

                'المتبقى من صافى الوزنه
                Dim RemainingNetWeight As Decimal = 0
                Dim Qdt As New DataTable()

                'Modifyed By Shady 08-07-2020
                Dim SQLST As String = ""
                SQLST = "Select DQ.DailyQuotaID From tblDailyQuotaWithOutContract DQ Join tblDailyQuotaWOContractGoods DG on(DQ.DailyQuotaID=DG.DailyQuotaID) " & _
                        " where  DQ.CustomerID=" & cboDealer.SelectedValue & " and DG.GoodID='" & cboGoodType.SelectedValue & "' /*and DQ.QuotaDate<=convert(Date,'" & DateSearch & "')*/ and DQ.finish=0"
                If (gAdo.IFExists(SQLST)) Then 'كوته بدون عقد

                    'إجمالى الوزنات بما فيها الوزنه الحاليه
                    TotalweightTransWithCurrentWeight = gAdo.CmdExecScalar("Set Dateformat dmy Select isnull(sum(T.Quantity),0)+" & lblNetWeight.Text & "  as TotalweightTrans " & _
                                                                           " From  tblTransQuotaWithOutContract T Join tblDailyQuotaWithOutContract DQ on(T.QuotaID=DQ.DailyQuotaID /*and DQ.QuotaDate<=convert(Date,'" & DateSearch & "')*/) " & _
                                                                           " join  tblDailyQuotaWOContractGoods DG on(DQ.DailyQuotaID=DG.DailyQuotaID) where DQ.CustomerID=" & cboDealer.SelectedValue & " and DG.GoodID='" & cboGoodType.SelectedValue & "'  /*and DQ.QuotaDate<=convert(Date,'" & DateSearch & "')*/ and DQ.finish=0")

                    'إجمالى الكوته اليوميه
                    TotalDailyQuota = gAdo.CmdExecScalar("Set Dateformat dmy Select isnull(Sum(DQ.Quantity*1000),0) From tblDailyQuotaWithOutContract DQ " & _
                                                         " join  tblDailyQuotaWOContractGoods DG on(DQ.DailyQuotaID=DG.DailyQuotaID) " & _
                                                         " where DQ.CustomerID=" & cboDealer.SelectedValue & " and DG.GoodID='" & cboGoodType.SelectedValue & "'  /*and DQ.QuotaDate<=convert(Date,'" & DateSearch & "')*/  and DQ.finish=0")

                    ' أكواد الكوتات
                    Qdt = gAdo.FillDataTable(" set dateformat dmy Select DQ.DailyQuotaID From tblDailyQuotaWithOutContract DQ " & _
                                             " join  tblDailyQuotaWOContractGoods DG on(DQ.DailyQuotaID=DG.DailyQuotaID) " & _
                                             " where DQ.CustomerID=" & cboDealer.SelectedValue & " and DG.GoodID='" & cboGoodType.SelectedValue & "' and DQ.finish=0 Order By DQ.DailyQuotaID")
                    ' الكميه المتبقيه من صافى الوزنه
                    RemainingNetWeight = Decimal.Parse(If(lblNetWeight.Text.Trim() = "", "0", lblNetWeight.Text.Trim()))

                    If TotalweightTransWithCurrentWeight > TotalDailyQuota Then
                        MsgBox(" الوزنة الحالية اكبر من الكوتة اليومية اتصل بقسم المبيعات ")
                        SaveOrCancel = 0
                        ClearControls = False
                        Exit Function
                    Else
                        For i As Integer = 0 To Qdt.Rows.Count - 1
                            ' iالكميه المسحوبه من الكوته رقم
                            OutQuantityfromQuota = gAdo.CmdExecScalar("Select isnull(sum(Quantity),0) TQ from tblTransQuotaWithOutContract  where QuotaID=" & Qdt.Rows(i)(0).ToString())
                            '  iكمية الكوته رقم 
                            Quota = gAdo.CmdExecScalar("Select Quantity*1000  From tblDailyQuotaWithOutContract  where DailyQuotaID=" & Qdt.Rows(i)(0).ToString())
                            'iالكميه المتبقيه من الكوته رقم  
                            RemainQuotaQuantity = Quota - OutQuantityfromQuota

                            If (RemainingNetWeight <= RemainQuotaQuantity) Then
                                gTransQuotaWOContractO.ID = 0
                                gTransQuotaWOContractO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
                                gTransQuotaWOContractO.QuotaID = Qdt.Rows(i)(0).ToString()
                                gTransQuotaWOContractO.Quantity = RemainingNetWeight
                                gTransQuotaWOContractO.Save()
                                If (RemainingNetWeight = RemainQuotaQuantity) Then
                                    SQLST = "Set DateFormat dmy UpDate tblDailyQuotaWithOutContract set finish=1,EndDate='" & GetEndDate() & "' where DailyQuotaID=" & Qdt.Rows(i)(0).ToString()
                                    gAdo.CmdExec(SQLST)
                                End If
                                Exit For
                            ElseIf (RemainingNetWeight > RemainQuotaQuantity) Then
                                gTransQuotaWOContractO.ID = 0
                                gTransQuotaWOContractO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
                                gTransQuotaWOContractO.QuotaID = Qdt.Rows(i)(0).ToString()
                                gTransQuotaWOContractO.Quantity = RemainQuotaQuantity
                                gTransQuotaWOContractO.Save()
                                SQLST = "Set DateFormat dmy UpDate tblDailyQuotaWithOutContract set finish=1,EndDate='" & GetEndDate() & "' where DailyQuotaID=" & Qdt.Rows(i)(0).ToString()
                                gAdo.CmdExec(SQLST)
                                RemainingNetWeight = RemainingNetWeight - RemainQuotaQuantity
                            End If

                        Next

                    End If










                    'intFirstQuotaID = gAdo.CmdExecScalar("Set Dateformat dmy Select Min(DQ.DailyQuotaID) From tblDailyQuotaWithOutContract DQ " & _
                    '                                     " join  tblDailyQuotaWOContractGoods DG on(DQ.DailyQuotaID=DG.DailyQuotaID) " & _
                    '                                     " where DQ.CustomerID=" & cboDealer.SelectedValue & " and DG.GoodID='" & cboGoodType.SelectedValue & "'  /*and DQ.QuotaDate<=convert(Date,'" & DateSearch & "')*/ and DQ.finish=0")

                    'FirstQuota = gAdo.CmdExecScalar("Select Quantity*1000 From tblDailyQuotaWithOutContract where DailyQuotaID=" & intFirstQuotaID)

                    'OutQuantityfromFirstQuota = gAdo.CmdExecScalar("Select isnull(sum(Quantity),0) From tblTransQuotaWithOutContract where QuotaID=" & intFirstQuotaID)

                    'RemainFirstQuotaQuantity = FirstQuota - OutQuantityfromFirstQuota

                    'intSecQuotaID = gAdo.CmdExecScalar("Set Dateformat dmy Select Max(DQ.DailyQuotaID) From tblDailyQuotaWithOutContract DQ " & _
                    '                                     " join  tblDailyQuotaWOContractGoods DG on(DQ.DailyQuotaID=DG.DailyQuotaID) " & _
                    '                                     " where DQ.CustomerID=" & cboDealer.SelectedValue & " and DG.GoodID='" & cboGoodType.SelectedValue & "'  /*and DQ.QuotaDate<=convert(Date,'" & DateSearch & "')*/ and DQ.finish=0")
                    'If (intFirstQuotaID <> intSecQuotaID) Then
                    '    SecQuota = gAdo.CmdExecScalar("Select Quantity*1000 From tblDailyQuotaWithOutContract where DailyQuotaID=" & intSecQuotaID)
                    '    OutFromSecQuota = gAdo.CmdExecScalar("Select isnull(sum(Quantity),0) From tblTransQuotaWithOutContract where QuotaID=" & intSecQuotaID)
                    '    RemainFromSecQuota = SecQuota - OutFromSecQuota
                    'Else
                    '    intSecQuotaID = 0
                    '    SecQuota = 0
                    '    OutFromSecQuota = 0
                    '    RemainFromSecQuota = 0
                    'End If

                    'RemainingNetWeight = Decimal.Parse(If(lblNetWeight.Text.Trim() = "", "0", lblNetWeight.Text.Trim()))


                    'If TotalweightTransWithCurrentWeight > TotalDailyQuota Then
                    '    MsgBox(" الوزنة الحالية اكبر من الكوتة اليومية اتصل بقسم المبيعات ")
                    '    SaveOrCancel = 0
                    '    ClearControls = False
                    '    Exit Function
                    'Else
                    '    If RemainingNetWeight <= RemainFirstQuotaQuantity Then
                    '        gTransQuotaWOContractO.ID = 0
                    '        gTransQuotaWOContractO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
                    '        gTransQuotaWOContractO.QuotaID = intFirstQuotaID
                    '        gTransQuotaWOContractO.Quantity = lblNetWeight.Text
                    '        gTransQuotaWOContractO.Save()
                    '        If (RemainingNetWeight - RemainFirstQuotaQuantity = 0) Then
                    '            SQLST = "Set DateFormat dmy UpDate tblDailyQuotaWithOutContract set finish=1,EndDate='" & GetEndDate() & "' where DailyQuotaID=" & intFirstQuotaID.ToString()
                    '            gAdo.CmdExec(SQLST)
                    '        End If
                    '    ElseIf RemainFirstQuotaQuantity > 0 Then
                    '        gTransQuotaWOContractO.ID = 0
                    '        gTransQuotaWOContractO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
                    '        gTransQuotaWOContractO.QuotaID = intFirstQuotaID
                    '        gTransQuotaWOContractO.Quantity = RemainFirstQuotaQuantity
                    '        gTransQuotaWOContractO.Save()
                    '        SQLST = "Set DateFormat dmy UpDate tblDailyQuotaWithOutContract set finish=1,EndDate='" & GetEndDate() & "' where DailyQuotaID=" & intFirstQuotaID.ToString()
                    '        gAdo.CmdExec(SQLST)
                    '        gTransQuotaWOContractO.ID = 0
                    '        gTransQuotaWOContractO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
                    '        gTransQuotaWOContractO.QuotaID = intSecQuotaID
                    '        gTransQuotaWOContractO.Quantity = RemainingNetWeight - RemainFirstQuotaQuantity
                    '        gTransQuotaWOContractO.Save()
                    '        If (RemainFromSecQuota - (RemainingNetWeight - RemainFirstQuotaQuantity) = 0) Then
                    '            SQLST = "Set DateFormat dmy UpDate tblDailyQuotaWithOutContract set finish=1,EndDate='" & GetEndDate() & "' where DailyQuotaID=" & intSecQuotaID.ToString()
                    '            gAdo.CmdExec(SQLST)
                    '        End If
                    '    Else
                    '        gTransQuotaWOContractO.ID = 0
                    '        gTransQuotaWOContractO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
                    '        gTransQuotaWOContractO.QuotaID = intSecQuotaID
                    '        gTransQuotaWOContractO.Quantity = RemainingNetWeight
                    '        gTransQuotaWOContractO.Save()
                    '        If (RemainFromSecQuota - RemainingNetWeight = 0) Then
                    '            SQLST = "Set DateFormat dmy UpDate tblDailyQuotaWithOutContract set finish=1,EndDate='" & GetEndDate() & "' where DailyQuotaID=" & intSecQuotaID.ToString()
                    '            gAdo.CmdExec(SQLST)
                    '        End If
                    '    End If

                    'End If


                Else 'كوته بعقد
                    ' إجمالى الوزنات للعميل بما فيهم هذه الوزنة
                    TotalweightTransWithCurrentWeight = gAdo.CmdExecScalar(" set dateformat dmy select isnull(sum(tblTransQuota.Quantity),0) + " & lblNetWeight.Text & "  as TotalweightTrans from tblTransQuota " & _
                                                                     " join tblDailyQuota on tblTransQuota .QuotaID =tblDailyQuota .QuotaID 	/*and DateQuota <=convert(Date,'" & DateSearch & "')*/ " & _
                                                                     " join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID " & _
                                                                     " where tblDebitNote .ContractID in (select tblContractDet.ContractID   from  tblContractDet " & _
                                                                     " join tblContracts on tblContractDet .ContractID =tblContracts .ContractID " & _
                                                                     " where CustomerID = " & cboDealer.SelectedValue & " and GoodID = '" & cboGoodType.SelectedValue & "') and tblDailyQuota.Finished=0")

                    'TotalweightTransWithCurrentWeight = gAdo.CmdExecScalar(" set dateformat dmy select isnull(sum(Second_Weight-First_Weigth),0)  + " & lblNetWeight.Text & "  as TotalweightTrans from tblTransactions " & _
                    '                                      " where(Dealer_ID = " & cboDealer.SelectedValue & " And Good_ID in(select GoodID  from tblDailyQuota join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID " & _
                    '                                      " join tblContractDet on tblDebitNote .ContractID =tblContractDet .ContractID join tblContracts on tblContractDet .ContractID =tblContracts .ContractID where CustomerID ='" + cboDealer.SelectedValue.ToString() + "' and DateQuota =convert(Date,'" & DateSearch & "'))" & WhereCondition & " ) ")

                    '' Total Trans Weight Without Current Weight

                    'TotalTransWeightWithoutCurrentWeight = gAdo.CmdExecScalar(" set dateformat dmy select isnull(sum(Second_Weight-First_Weigth),0)  as TotalweightTrans from tblTransactions " & _
                    '                                      " where(Dealer_ID = " & cboDealer.SelectedValue & " And Good_ID in(select GoodID  from tblDailyQuota join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID join tblContractDet on tblDebitNote .ContractID =tblContractDet .ContractID join tblContracts on tblContractDet .ContractID =tblContracts .ContractID where CustomerID ='" + cboDealer.SelectedValue.ToString() + "' and DateQuota =convert(Date,'" & DateSearch & "'))" & WhereCondition & " ) ")

                    'TotalTransWeightWithoutCurrentWeight = gAdo.CmdExecScalar(" set dateformat dmy select isnull(sum(tblTransQuota.Quantity),0) as TotalweightTrans from tblTransQuota " & _
                    '                                                 " join tblDailyQuota on tblTransQuota .QuotaID =tblDailyQuota .QuotaID 	/*and DateQuota <=convert(Date,'" & DateSearch & "')*/ " & _
                    '                                                 " join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID " & _
                    '                                                 " where tblDebitNote .ContractID in (select tblContractDet.ContractID   from  tblContractDet " & _
                    '                                                 " join tblContracts on tblContractDet .ContractID =tblContracts .ContractID " & _
                    '                                                 " where CustomerID = " & cboDealer.SelectedValue & " and GoodID = '" & cboGoodType.SelectedValue & "') and tblDailyQuota.Finished=0")




                    '' this Code TO Replace  "م" and "ًص"


                    'If TheDate.Contains("م") Then
                    '    TheDate = TheDate.Replace("م", "PM")
                    'ElseIf TheDate.Contains("ص") Then
                    '    TheDate = TheDate.Replace("ص", "AM")
                    'End If


                    '' The Total Daily Quote for this Customer on this Day    '' Deleted this Code [ * (1+tblContracts.QuantityRatio/100)]
                    '" join tblDebitNoteQuantity  on tblDebitNoteQuantity.DebitNoteQuantityID  = tblDailyQuota.DebitNoteQuantityID  " & _

                    ' إجمالى الكوتة لهذا العميل لهذا الصنف

                    TotalDailyQuota = gAdo.CmdExecScalar(" set dateformat dmy select ISNULL(d.quota,0) from (select (isnull(sum (Quota),0)*1000)-(isnull(sum(OutGoingQty),0)*1000) as quota from tblDailyQuota " & _
                                                     " join tblDebitNote on tblDebitNote.DebitNoteID =tblDailyQuota.DebitNoteID  " & _
                                                     " join tblContracts on tblContracts.ContractID = tblDebitNote.ContractID   " & _
                                                     " join tblContractDet on tblContracts .ContractID =tblContractDet .ContractID    " & _
                                                    " where  CustomerID = " & cboDealer.SelectedValue & " /*and DateQuota <=  CONVERT(date,'" & DateSearch & "')*/ and tblDailyQuota.Finished=0 and tblContractDet.GoodID ='" & cboGoodType.SelectedValue & "' ) as d")

                    'أكواد الكوتات
                    Qdt = gAdo.FillDataTable(" set dateformat dmy select ISNULL(QuotaID,0) QID from tblDailyQuota    " & _
                                                         " join tblDebitNote on tblDebitNote .DebitNoteID =tblDailyQuota .DebitNoteID  " & _
                                                         "  join tblContracts on tblDebitNote.ContractID =tblContracts.ContractID   " & _
                                                         " join tblContractDet on tblContracts .ContractID =tblContractDet .ContractID    " & _
                                                         " and CustomerID = " & cboDealer.SelectedValue & " /*and DateQuota <=  CONVERT(date,'" & DateSearch & "')*/ and tblDailyQuota.Finished=0 and tblContractDet.GoodID ='" & cboGoodType.SelectedValue & "' Order By QuotaID")
                    'المتبقى من صافى الوزنه
                    RemainingNetWeight = Decimal.Parse(If(lblNetWeight.Text.Trim() = "", "0", lblNetWeight.Text.Trim()))
                    If TotalweightTransWithCurrentWeight > TotalDailyQuota Then
                        MsgBox(" الوزنة الحالية اكبر من الكوتة اليومية اتصل بقسم المبيعات ")
                        SaveOrCancel = 0
                        ClearControls = False
                        Exit Function
                    Else
                        For i As Integer = 0 To Qdt.Rows.Count - 1
                            '  iالكمية المسحوبة من  الكوتة رقم
                            OutQuantityfromQuota = gAdo.CmdExecScalar("Select isnull(sum(Quantity),0) TQ from tblTransQuota  where QuotaID=" & Qdt.Rows(i)(0).ToString())
                            '  iإجمالى الكوتة رقم
                            Quota = gAdo.CmdExecScalar("Select (Quota-OutGoingQty)*1000  From tblDailyQuota  where QuotaID=" & Qdt.Rows(i)(0).ToString())
                            'iالكمية المتبقية من الكوتة رقم 
                            RemainQuotaQuantity = Quota - OutQuantityfromQuota

                            If (RemainingNetWeight <= RemainQuotaQuantity) Then
                                gTransQuotaO.ID = 0
                                gTransQuotaO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
                                gTransQuotaO.QuotaID = Qdt.Rows(i)(0).ToString()
                                gTransQuotaO.Quantity = RemainingNetWeight
                                gTransQuotaO.Save()
                                If (RemainingNetWeight = RemainQuotaQuantity) Then
                                    SQLST = "Set DateFormat dmy UpDate tblDailyQuota set Finished=1,EndDate='" & GetEndDate() & "' where QuotaID=" & Qdt.Rows(i)(0).ToString()
                                    gAdo.CmdExec(SQLST)
                                End If
                                Exit For
                            ElseIf (RemainingNetWeight > RemainQuotaQuantity) Then
                                gTransQuotaO.ID = 0
                                gTransQuotaO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
                                gTransQuotaO.QuotaID = Qdt.Rows(i)(0).ToString()
                                gTransQuotaO.Quantity = RemainQuotaQuantity
                                gTransQuotaO.Save()
                                SQLST = "Set DateFormat dmy UpDate tblDailyQuota set Finished=1,EndDate='" & GetEndDate() & "' where QuotaID=" & Qdt.Rows(i)(0).ToString()
                                gAdo.CmdExec(SQLST)
                                RemainingNetWeight = RemainingNetWeight - RemainQuotaQuantity
                            End If


                        Next
                    End If





                    '' MinimumQuota

                    'MinimumQuota = gAdo.CmdExecScalar(" set dateformat dmy select Quota from tblDailyQuota join tblContractDet on tblContractDet.ContractDetID = tblDailyQuota.ContractDetID " & _
                    '                                    " join tblContracts on tblContracts.ContractID = tblContractDet.ContractID where CustomerID = 17170 " & _
                    '                                    " and QuotaID in (select min(QuotaID) from tblDailyQuota where DateQuota = '" & Date.Now.Date & "') ")
                    'MinimumQuota = gAdo.CmdExecScalar("set dateformat dmy select Quota from tblDailyQuota where QuotaID in (select min(QuotaID) from tblDailyQuota where DateQuota = CONVERT(date,'" & DateSearch & "'))")
                    'FirstQuota = gAdo.CmdExecScalar(" set dateformat dmy select distinct ISNULL(d.quota,0) from ( select  (ISNULL(sum(Quota),0)*1000)-(isnull(sum(OutGoingQty),0)*1000) as quota from tblDailyQuota " & _
                    '                               " join tblDebitNote on tblDebitNote .DebitNoteID =tblDailyQuota .DebitNoteID  " & _
                    '                               " join tblContracts on tblContracts.ContractID = tblDebitNote.ContractID   " & _
                    '                                " join tblContractDet on tblContracts .ContractID =tblContractDet .ContractID    " & _
                    '                               " where CustomerID = " & cboDealer.SelectedValue & " /*and DateQuota <=  CONVERT(date,'" & DateSearch & "')*/ and tblDailyQuota.Finished=0 and tblContractDet.GoodID ='" & cboGoodType.SelectedValue & "' and QuotaID=(Select Min(D.QuotaID) From tblDailyQuota D join tblDebitNote DN on DN.DebitNoteID =D.DebitNoteID join tblContracts C on C.ContractID = DN.ContractID  join tblContractDet CD on C .ContractID =CD .ContractID where CustomerID = " & cboDealer.SelectedValue & "  /*and DateQuota <=  CONVERT(date,'" & DateSearch & "')*/ and D.Finished=0 and GoodID='" & cboGoodType.SelectedValue.ToString() & "' ) " & _
                    '                               " ) as d")



                    '' To Git ContractDetID From tblContractDet
                    'Dim intDebitNoteQuantityID As Integer
                    'intDebitNoteQuantityID = gAdo.CmdExecScalar("select ContractDetID from tblContractDet join tblContracts on tblContracts .ContractID = tblContractDet .ContractID " & _
                    ' " join tblDealer on tblDealer .Dealer_ID = tblContracts .CustomerID join tblGood on tblGood .Good_ID = tblContractDet .GoodID " & _
                    '" and tblDealer.Dealer_ID = " & cboDealer.SelectedValue & " and tblGood.Good_ID = '" & cboGoodType.SelectedValue & "'")
                    'intDebitNoteQuantityID = gAdo.CmdExecScalar("select  DebitNoteQuantityID  from tblDebitNoteQuantity  " & _
                    '                                    " join tblDebitNote on tblDebitNote .DebitNoteID =tblDebitNoteQuantity .DebitNoteID  " & _
                    '                                          " join tblContracts on tblContracts .ContractID = tblDebitNote .ContractID   " & _
                    '                                                "  join tblDealer on tblDealer .Dealer_ID = tblContracts .CustomerID  " & _
                    '                                                  "   join tblGood on tblGood .Good_ID = tblDebitNoteQuantity .GoodID   " & _
                    '                                   " and tblDealer.Dealer_ID = " & cboDealer.SelectedValue & " and tblGood.Good_ID = '" & cboGoodType.SelectedValue & "'")

                    'intQuotaID = gAdo.CmdExecScalar("select QuotaID from tblDailyQuota where ContractDetID = " & intContractDetID)
                    'intFirstQuotaID = gAdo.CmdExecScalar(" set dateformat dmy select ISNULL(Min(QuotaID),0) from tblDailyQuota    " & _
                    '                                     " join tblDebitNote on tblDebitNote .DebitNoteID =tblDailyQuota .DebitNoteID  " & _
                    '                                     "  join tblContracts on tblDebitNote.ContractID =tblContracts.ContractID   " & _
                    '                                     " join tblContractDet on tblContracts .ContractID =tblContractDet .ContractID    " & _
                    '                                     " and CustomerID = " & cboDealer.SelectedValue & " /*and DateQuota <=  CONVERT(date,'" & DateSearch & "')*/ and tblDailyQuota.Finished=0 and tblContractDet.GoodID ='" & cboGoodType.SelectedValue & "' ")


                    'OutQuantityfromFirstQuota = gAdo.CmdExecScalar(" set dateformat dmy select isnull(sum(tblTransQuota.Quantity),0) as TotalweightTrans from tblTransQuota " & _
                    '                                                 " join tblDailyQuota on tblTransQuota .QuotaID =tblDailyQuota .QuotaID 	/*and DateQuota <=convert(Date,'" & DateSearch & "')*/ " & _
                    '                                                 " join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID " & _
                    '                                                 " where tblDebitNote .ContractID in (select tblContractDet.ContractID   from  tblContractDet " & _
                    '                                                 " join tblContracts on tblContractDet .ContractID =tblContracts .ContractID " & _
                    '                                                 " where CustomerID = " & cboDealer.SelectedValue & " and GoodID = '" & cboGoodType.SelectedValue & "' ) and tblTransQuota .QuotaID=" & intFirstQuotaID.ToString())

                    'RemainFirstQuotaQuantity = FirstQuota - OutQuantityfromFirstQuota


                    'intSecQuotaID = gAdo.CmdExecScalar("select QuotaID from tblDailyQuota join tblContractDet on tblDailyQuota.ContractDetID =tblContractDet .ContractDetID " & _
                    '                                     " join tblContracts on tblContractDet.ContractID =tblContracts.ContractID " & _
                    '                                      " and CustomerID = " & cboDealer.SelectedValue & " and GoodID = '" & cboGoodType.SelectedValue & _
                    '                                      "' ORDER BY  tblContracts.ContractID DESC")

                    'intSecQuotaID = gAdo.CmdExecScalar("set dateformat dmy select ISNULL(Max(QuotaID),0) from tblDailyQuota    " & _
                    '                                     " join tblDebitNote on tblDebitNote .DebitNoteID =tblDailyQuota .DebitNoteID  " & _
                    '                                      "  join tblContracts on tblDebitNote.ContractID =tblContracts.ContractID   " & _
                    '                                      " join tblContractDet on tblContracts .ContractID =tblContractDet .ContractID    " & _
                    '                                      " and CustomerID = " & cboDealer.SelectedValue & " /*and DateQuota <=  CONVERT(date,'" & DateSearch & "')*/  and tblDailyQuota.Finished=0 and tblContractDet.GoodID ='" & cboGoodType.SelectedValue & "' ")


                    'SecQuota = gAdo.CmdExecScalar("Select (isnull(Quota,0)*1000)-(isnull(OutGoingQty,0)*1000) From tblDailyQuota where QuotaID=" & intSecQuotaID)



                    'OutFromSecQuota = gAdo.CmdExecScalar(" set dateformat dmy select isnull(sum(tblTransQuota.Quantity),0) as TotalweightTrans from tblTransQuota " & _
                    '                                                 " join tblDailyQuota on tblTransQuota .QuotaID =tblDailyQuota .QuotaID 	/*and DateQuota <=convert(Date,'" & DateSearch & "')*/ " & _
                    '                                                 " join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID " & _
                    '                                                 " where tblDebitNote .ContractID in (select tblContractDet.ContractID   from  tblContractDet " & _
                    '                                                 " join tblContracts on tblContractDet .ContractID =tblContracts .ContractID " & _
                    '                                                 " where CustomerID = " & cboDealer.SelectedValue & " and GoodID = '" & cboGoodType.SelectedValue & "' ) and tblTransQuota .QuotaID=" & intSecQuotaID.ToString())


                    'RemainFromSecQuota = SecQuota - OutFromSecQuota


                    'RemainingNetWeight = Decimal.Parse(If(lblNetWeight.Text.Trim() = "", "0", lblNetWeight.Text.Trim()))
                    ' Complete Her
                    'If TotalweightTransWithCurrentWeight > TotalDailyQuota Then    ' If TotalweightTransWithCurrentWeight <= TotalDailyQuota Then
                    '    MsgBox(" الوزنة الحالية اكبر من الكوتة اليومية اتصل بقسم المبيعات ")
                    '    SaveOrCancel = 0
                    '    ClearControls = False
                    '    Exit Function
                    'Else
                    '    If RemainingNetWeight <= RemainFirstQuotaQuantity Then
                    '        '' insert into tblTransQuota By Quantity = lblNetWeight.Text (صافي الوزنة)
                    '        gTransQuotaO.ID = 0
                    '        gTransQuotaO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
                    '        gTransQuotaO.QuotaID = intFirstQuotaID
                    '        gTransQuotaO.Quantity = lblNetWeight.Text
                    '        gTransQuotaO.Save()
                    '        If (RemainingNetWeight - RemainFirstQuotaQuantity = 0) Then
                    '            SQLST = "Set DateFormat dmy UpDate tblDailyQuota set Finished=1,EndDate='" & GetEndDate() & "' where QuotaID=" & intFirstQuotaID.ToString()
                    '            gAdo.CmdExec(SQLST)
                    '        End If

                    '    Else
                    '        If RemainFirstQuotaQuantity > 0 Then
                    '            '' insert first in tblTransQuota
                    '            gTransQuotaO.ID = 0
                    '            gTransQuotaO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where   TR_TY=2 and  CID=" & txtTransID.Text)
                    '            gTransQuotaO.QuotaID = intFirstQuotaID
                    '            gTransQuotaO.Quantity = RemainFirstQuotaQuantity
                    '            gTransQuotaO.Save()
                    '            SQLST = "Set DateFormat dmy UpDate tblDailyQuota set Finished=1,EndDate='" & GetEndDate() & "'  where QuotaID=" & intFirstQuotaID.ToString()
                    '            gAdo.CmdExec(SQLST)
                    '            '' insert Second in tblTransQuota
                    '            gTransQuotaO.ID = 0
                    '            gTransQuotaO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and  CID=" & txtTransID.Text)
                    '            gTransQuotaO.QuotaID = intSecQuotaID
                    '            'gTransQuotaO.Quantity = (lblNetWeight.Text) - MinimumQuota
                    '            gTransQuotaO.Quantity = RemainingNetWeight - RemainFirstQuotaQuantity
                    '            gTransQuotaO.Save()
                    '            If (RemainFromSecQuota - (RemainingNetWeight - RemainFirstQuotaQuantity) = 0) Then
                    '                SQLST = "Set DateFormat dmy UpDate tblDailyQuota set Finished=1,EndDate='" & GetEndDate() & "'  where QuotaID=" & intSecQuotaID.ToString()
                    '                gAdo.CmdExec(SQLST)
                    '            End If
                    '        Else
                    '            gTransQuotaO.ID = 0
                    '            gTransQuotaO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and  CID=" & txtTransID.Text)
                    '            gTransQuotaO.QuotaID = intSecQuotaID
                    '            gTransQuotaO.Quantity = lblNetWeight.Text
                    '            gTransQuotaO.Save()
                    '            If (RemainFromSecQuota - RemainingNetWeight = 0) Then
                    '                SQLST = "Set DateFormat dmy UpDate tblDailyQuota set Finished=1,EndDate='" & GetEndDate() & "'  where QuotaID=" & intSecQuotaID.ToString()
                    '                gAdo.CmdExec(SQLST)
                    '            End If

                    '        End If
                    '    End If


                    '    'If TotalweightTransWithCurrentWeight <= FirstQuota Then
                    '    '    '' insert into tblTransQuota By Quantity = lblNetWeight.Text (صافي الوزنة)
                    '    '    gTransQuotaO.ID = 0
                    '    '    gTransQuotaO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where   TR_TY=2 and  CID=" & txtTransID.Text)
                    '    '    gTransQuotaO.QuotaID = intFirstQuotaID
                    '    '    gTransQuotaO.Quantity = lblNetWeight.Text
                    '    '    gTransQuotaO.Save()

                    '    'Else
                    '    '    If TotalTransWeightWithoutCurrentWeight < FirstQuota Then
                    '    '        '' insert first in tblTransQuota
                    '    '        gTransQuotaO.ID = 0
                    '    '        gTransQuotaO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where   TR_TY=2 and  CID=" & txtTransID.Text)
                    '    '        gTransQuotaO.QuotaID = intFirstQuotaID
                    '    '        gTransQuotaO.Quantity = FirstQuota - TotalTransWeightWithoutCurrentWeight
                    '    '        gTransQuotaO.Save()
                    '    '        '' insert Second in tblTransQuota
                    '    '        gTransQuotaO.ID = 0
                    '    '        gTransQuotaO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and  CID=" & txtTransID.Text)
                    '    '        gTransQuotaO.QuotaID = intSecQuotaID
                    '    '        'gTransQuotaO.Quantity = (lblNetWeight.Text) - MinimumQuota
                    '    '        gTransQuotaO.Quantity = TotalweightTransWithCurrentWeight - FirstQuota
                    '    '        gTransQuotaO.Save()
                    '    '    Else
                    '    '        gTransQuotaO.ID = 0
                    '    '        gTransQuotaO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and  CID=" & txtTransID.Text)
                    '    '        gTransQuotaO.QuotaID = intSecQuotaID
                    '    '        gTransQuotaO.Quantity = lblNetWeight.Text
                    '    '        gTransQuotaO.Save()

                    '    '    End If
                    '    'End If


                    'End If
                End If

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                Dim NowDate As DateTime = Now
                Dim OUTDATE As String
                OUTDATE = Now
                If OUTDATE.Contains("م") Then
                    OUTDATE = OUTDATE.Replace("م", "PM")
                ElseIf OUTDATE.Contains("ص") Then
                    OUTDATE = OUTDATE.Replace("ص", "AM")
                End If

                'Dim Lastweight As Decimal
                'Lastweight = gAdo.CmdExecScalar("Select Isnull(LastWeight,0) From tblScale where Scale_ID=" & ScaleNo)

                'If ((Lastweight - Val(lblScndWeight.Text) >= 0 And Lastweight - Val(lblScndWeight.Text) <= 150) Or (Val(lblScndWeight.Text) - Lastweight >= 0 And Val(lblScndWeight.Text) - Lastweight <= 150)) Then
                '    gAdo.CmdExec("Insert tblControlScale(UserID,MoveDate,MoveWeight) Values(" & gTransactionO.First_Weight_User_ID.ID & ",'" & OUTDATE & "'," & lblScndWeight.Text & ")")
                'End If

                'If (Val(lblScndWeight.Text) > 0) Then
                '    gAdo.CmdExec("UpDate tblScale set LastWeight=" & lblScndWeight.Text & " where Scale_ID=" & ScaleNo)
                'End If


                Dim TRANSID As Integer

                'TRANSID = gAdo.CmdExecScalar("select trans_id from tblTransactions where First_Weigth_Scale_ID=" & ScaleNo & " and CID =" & ID)
                TRANSID = gAdo.CmdExecScalar("select trans_id from tblTransactions where CID =" & ID)
                'If TRANSID = 0 Then
                '    ' عشان لو مش لقى حاجه بتاعته يروح يدور عليها للميزان التانى
                '    TRANSID = gAdo.CmdExecScalar("select trans_id from tblTransactions where First_Weigth_Scale_ID=3 and CID =" & ID)
                'End If

                Dim LID As String = ""
                If (cboLocation.SelectedValue = Nothing Or cboLocation.Text = "") Then
                    LID = "0"
                Else : LID = cboLocation.SelectedValue.ToString()
                End If

                Str = "set dateformat dmy Update tblTransactions" & " Set Out_Date = '" & OUTDATE & "'" _
                    & ",Second_Weight=" & gTransactionO.Second_Weight & _
                  ",First_Weigth=" & gTransactionO.First_Weigth & _
                  ",Contractor_ID=" & gTransactionO.Contractor_ID.ID & _
                      ",CustomWeight=" & gTransactionO.CustomWeight & _
                      ",Car_ID='" & gTransactionO.Car_ID.ID & "'" & _
                      ",Driver_ID =" & gTransactionO.Driver_ID.ID & " ,Dealer_ID = " & gTransactionO.Dealer_ID.ID & " ,Issue_To_ID =" & gTransactionO.Issue_To_ID.ID & " ,Good_ID='" & gTransactionO.Good_ID.ID & "'" & _
                      ",Second_Weight_User_ID=" & gTransactionO.Second_Weight_User_ID.ID & _
                      ",Second_Weight_Scale_ID=" & ScaleNo & _
                      ",MSGID=" & MsgID & _
                      ",Car_Info_ID=" & gTransactionO.Car_Info_ID.ID & _
                      ",Slip_Rate=" & gTransactionO.Slip_Rate & ",Slip_Charged_Date='" & Now.Date & "'" & _
                      ",Slip_Charged_User_ID=" & gTransactionO.Slip_Charged_User_ID & ",LocationID=" & LID & " Where  TR_TY=2 and  CID=" & txtTransID.Text

                ' If PrintTransaction(NowDate) = True Then

                gAdo.CmdExec(Str, False)

                ' ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Start Printing Thread ~~~~~~~~~~~~ TH ~~~  
                ' i made a thread for printing to not make the progam freez while printing 
                Dim PrintMSG As MsgBoxResult
                PrintMSG = MsgBox("هـل ترغـب فى طباعة الكارته ؟", MsgBoxStyle.OkCancel, " الطباعة  ")



                PTransID = txtTransID.Text
                If PrintMSG = MsgBoxResult.Ok Then
                    NOPrinted = Val(InputBox("عدد مرات النسخ", "عدد مرات الطباعة", Val(gAdo.CmdExecScalar("select value from tblvalue where serial=1"))))
                    AutoPrint = False
                    PCarNo = txtCarNo.Text
                    PCarCity = cboCarCity.Text
                    PTruckNo = cboTruckNo.Text
                    PTruckCity = cboTruckCity.Text
                    PDriverName = cboDriver.Text
                    PDriverLicence = txtLicenceNo.Text
                    PDealerName = cboDealer.Text
                    PIssueto = cboIssueTo.Text
                    PGoodName = cboGoodType.Text

                    If CboRecieveType.Text = "إسكندرية" Or CboRecieveType.Text = "" Then
                        PRecieveType = ""
                    ElseIf CboRecieveType.Text = "العامرية" Then
                        PRecieveType = "العامرية"
                    End If

                    PFisrtScale = CDec(Val(lblFrstWeight.Text.Trim))
                    PSecondScale = CDec(Val(lblScndWeight.Text.Trim))
                    If Val(lblScndWeight.Text) < 1 Then
                        PNetScale = 0
                    Else
                        PNetScale = Abs(CDec(Val(lblFrstWeight.Text.Trim)) - CDec(Val(lblScndWeight.Text.Trim)))
                    End If
                    PArabic = HANY(Val(lblNetWeight.Text.Trim), "EGYPT")
                    Dim Thread As New System.Threading.Thread(AddressOf PrintNewReport)
                    Control.CheckForIllegalCrossThreadCalls = False
                    Thread.Start()
                End If

                ' ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ End Printing Thread ~~~~~~~~~~~~~ TH ~~~  

                ' PrintTransaction(gAdo.CmdExecScalar("select Out_Date from tblTransactions where Trans_ID=" & txtTransID.Text))
                ' here to insert into the Server DB ~~~~~~~~~~~~~~~~~~ Start ~~
                ' Dim ServerDataBaseInsertThread As New System.Threading.Thread(AddressOf InsertIntoServerDB)
                ' Control.CheckForIllegalCrossThreadCalls = False
                ' ServerDataBaseInsertThread.Start()
                ' here to insert into the Server DB ~~~~~~~~~~~~~~~~~~~~ End ~~
lk:
                ''~~~~~~~~~~ Insert The Transaction Into The Server DB ~~~~~~~~~~ Start ~~
                'Dim ThreadServerInsert As New System.Threading.Thread(AddressOf InsertIntoServerDB)
                'Control.CheckForIllegalCrossThreadCalls = False
                'Thread.Join()
                'ThreadServerInsert.Start()
                'ThreadInsertServerDB()
                'InsertIntoServerDB()
                InsertIntoFailed()
                ''~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ END ~~
                MsgBox("تم حفظ بيانات الوزنه الثانيه بنجاح", , " الحفظ  ")
                SIsUpdate = False
            End If

        ElseIf SIsUpdate = True Then

            Str = "set dateformat dmy Update tblTransactions set Driver_ID=" & gTransactionO.Driver_ID.ID & ",Slip_Rate=" & gTransactionO.Slip_Rate & ",Car_ID=" & gTransactionO.Car_ID.ID & ",Car_Info_ID=" & gTransactionO.Car_Info_ID.ID & " Where  TR_TY=2 and  CID=" & txtTransID.Text
            gAdo.CmdExec(Str, False)
            '       & " Set  First_Weigth=" & gTransactionO.First_Weigth & _
            '        ",CustomWeight='" & gTransactionO.CustomWeight & "'" & _
            '         ",Car_ID='" & gTransactionO.Car_ID.ID & "'" & _
            '         ",Driver_ID =" & gTransactionO.Driver_ID.ID & " ,Dealer_ID = " & gTransactionO.Dealer_ID.ID & " ,Issue_To_ID =" & gTransactionO.Issue_To_ID.ID & " ,Good_ID='" & gTransactionO.Good_ID.ID & "'" & _
            '         ",First_Weight_User_ID=" & gTransactionO.First_Weight_User_ID.ID & _
            '         ",Second_Weight_Scale_ID=" & ScaleNo & _
            '         ",Car_Info_ID=" & gTransactionO.Car_Info_ID.ID & _
            '         ",MSGID=" & MsgID & _
            '         ",Slip_Rate=" & gTransactionO.Slip_Rate & ",Slip_Charged_Date='" & Now.Date & "'" & _
            '         ",Contractor_ID=" & gTransactionO.Contractor_ID.ID & _
            '         ",Slip_Charged_User_ID=" & gTransactionO.Slip_Charged_User_ID & " Where  TR_TY=2 and  CID=" & txtTransID.Text
            ''gAdo.CmdExec("update tblCar set CarBoard_No='" & txtCarNo.Text & "' where Car_ID= '" & gCarO.ID & "'", False)
            'gAdo.CmdExec(Str, False)
            'SaveOrCancel = 1
            'ClearControls = True
            'SIsUpdate = False
            'MsgBox("تم الحفـــظ بنجاح ", , " الحفظ  ")
            'MsgBox(" لا يمكنك التعديل")
            MsgBox("تم التعديل بنجاح")



        End If
    End Function

    Function OLDCommitTrans() As String
        'gTransactionO.Car_ID.ID
        'gAdo.CmdExec("update tblCar set CarBoard_No='" & txtCarNo.Text & "' where Car_ID= '" & gCarO.ID & "'", False)
        gTransactionO.Car_ID.ID = gCarO.ID
        Dim strCar As String
        strCar = gCarO.ID
        If cboDriver.SelectedIndex = 0 Or cboDriver.SelectedValue = Nothing Then
            gTransactionO.Driver_ID.ID = 1
        Else
            gTransactionO.Driver_ID.ID = gDriverO.ID
        End If
        If cboTruckNo.SelectedIndex = 0 Or cboTruckNo.SelectedValue = Nothing Then
            gTransactionO.Car_Info_ID.ID = 1
        Else
            gTransactionO.Car_Info_ID.ID = gCarInfoID
        End If
        If cboIssueTo.SelectedIndex = 0 Or cboIssueTo.SelectedValue = Nothing Then
            gTransactionO.Issue_To_ID.ID = 1
        Else
            gTransactionO.Issue_To_ID.ID = gIssueToO.ID
        End If

        If cboContractor.SelectedIndex = 0 Or cboContractor.SelectedValue = Nothing Then
            gTransactionO.Contractor_ID.ID = 1
        Else
            gTransactionO.Contractor_ID.ID = gContractorO.ID
        End If

        gTransactionO.First_Weigth = CDec(Val(lblFrstWeight.Text.Trim))
        'gTransactionO.First_Weight_User_ID.ID = gUserID
        gTransactionO.CustomWeight = CDec(Val(txtCustomWeight.Text.Trim))

        gTransactionO.First_Weigth_Scale_ID.ID = 1
        If cboGoodType.SelectedValue = Nothing Then
            gTransactionO.Good_ID.ID = 1
        Else
            gTransactionO.Good_ID.ID = gGoodO.ID
        End If

        ''' Elsaman 8/12/2019 '''
        If txtNotes.Text = "" Then
            gTransactionO.Notes = ""
        Else
            gTransactionO.Notes = txtNotes.Text
        End If


        ''Eng Ahmad Fawzy 
        'If CboSlip.SelectedValue = Nothing Then
        '    gTransactionO.Slip_Rate = 0
        'Else
        '    gTransactionO.Slip_Rate = gSlipo.Slip_MaxRange
        'End If

        gTransactionO.First_Weight_User_ID.ID = gUserID
        Dim MsgID As Integer
        If CboRecieveType.Text = "" Then
            MsgID = 4
        ElseIf CboRecieveType.Text = "إسكندرية" Then
            MsgID = 1
        ElseIf CboRecieveType.Text = "العامرية" Then
            MsgID = 2
        End If


        gTransactionO.Slip_Charged_User_ID = 0
        gTransactionO.Second_Weight_Scale_ID.ID = 1
        gTransactionO.Second_Weight = CDec(Val(lblScndWeight.Text.Trim))
        gTransactionO.Second_Weight_User_ID.ID = gUserID
        If cboDealer.SelectedValue = Nothing Or cboDealer.SelectedIndex = 0 Then
            gTransactionO.Dealer_ID.ID = 1
        Else
            gTransactionO.Dealer_ID.ID = gDealerO.ID
        End If
        Dim IsUpdate As Boolean
        IsUpdate = SIsUpdate
        If radFrstEmpty.Checked = True Then
            gTransactionO.First_Weight_IsEmpty = True
        ElseIf radFrstFull.Checked = True Then
            gTransactionO.First_Weight_IsEmpty = False
        End If
        If radScndEmpty.Checked = True Then
            gTransactionO.Second_Weight_IsEmpty = True
        ElseIf radScndFull.Checked = True Then
            gTransactionO.Second_Weight_IsEmpty = False
        End If
        If cboLocation.SelectedValue = Nothing Or cboLocation.SelectedIndex = 0 Then
            gTransactionO.LocationID = 1
        Else
            gTransactionO.LocationID = cboLocation.SelectedValue
        End If

        Dim Str As String = ""

        Try

        Catch ex As Exception
            'MsgBox(ex)
        End Try
        'If Then

        '    MsgBox("لم يتم حفظ  الوزنة الأولى", MsgBoxStyle.O)
        'End If
        If Val(lblFrstWeight.Text) = 0 Then
            My.Computer.Audio.Play(My.Resources.beep_05, AudioPlayMode.Background)
            MsgBox("لم يتم حفظ  الوزنة الأولى")
        End If

        If SIsUpdate = False Then

            ' ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            Dim count As Integer
            'count = gAdo.CmdExecScalar("select count (CID) from tblTransactions where TR_TY=2 and  CID = " & txtTransID.Text & " and First_Weigth_Scale_ID=" & ScaleNo)
            count = gAdo.CmdExecScalar("select count (CID) from tblTransactions where TR_TY=2 and  CID = " & txtTransID.Text & " ")
            If count > 0 Then
                'If IsDBNull(gAdo.CmdExecScalar("select Out_Date from tblTransactions where TR_TY=2 and  CID =" & txtTransID.Text & " and First_Weigth_Scale_ID=" & ScaleNo)) = False Then
                If IsDBNull(gAdo.CmdExecScalar("select Out_Date from tblTransactions where TR_TY=2 and  CID =" & txtTransID.Text & " ")) = False Then
                    'GoTo LK
                    MsgBox("للتعديل من فضلك اضغط زر تعديل")
                    ClearControls = False
                    Exit Function
                End If
            Else

            End If
            ' ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            Dim Today As String
            Today = Now
            If Today.Contains("ص") Then
                Today = Today.Replace("ص", "AM")
            ElseIf Today.Contains("م") Then
                Today = Today.Replace("م", "PM")
            End If

            'Dim Lastweight As Decimal
            'Lastweight = gAdo.CmdExecScalar("Select Isnull(LastWeight,0) From tblScale where Scale_ID=" & ScaleNo)

            'If ((Lastweight - Val(lblFrstWeight.Text) >= 0 And Lastweight - Val(lblFrstWeight.Text) <= 150) Or (Val(lblFrstWeight.Text) - Lastweight >= 0 And Val(lblFrstWeight.Text) - Lastweight <= 150)) Then
            '    gAdo.CmdExec("Insert tblControlScale(UserID,MoveDate,MoveWeight) Values(" & gTransactionO.First_Weight_User_ID.ID & ",'" & Today & "'," & lblFrstWeight.Text & ")")
            'End If

            'If (Val(lblFrstWeight.Text) > 0) Then
            '    gAdo.CmdExec("UpDate tblScale set LastWeight=" & lblFrstWeight.Text & " where Scale_ID=" & ScaleNo)
            'End If

            Str = "set dateformat dmy insert into tblTransactions (In_Date,In_Shift_Date,In_Shift_ID,Car_ID,Driver_ID," _
            & "Car_Info_ID,Dealer_ID,Issue_To_ID " & _
           " ,First_Weigth,First_Weight_IsEmpty,First_Weigth_Scale_ID,First_Weight_User_ID,Second_Weight_IsEmpty,Good_ID,Slip_Rate" & _
           ",Slip_Charged_Date,Slip_Charged_User_ID,MSGID,Second_Weight,TR_TY,CustomWeight,CID,Contractor_ID,Notes,LocationID)  " & _
            "Values( "


            Dim CID As Integer
            'CID = gAdo.CmdExecScalar("select isnull( max (CID),0)from tblTransactions where TR_TY=2 ")

            CID = gAdo.CmdExecScalar("select isnull(max(CID),0) as CID from tblTransactions where CID >= (select countFrom from tblScale where Scale_ID = " & ScaleNo & ") " & _
                                        " and CID <= (select countTo from tblScale where Scale_ID = " & ScaleNo & ") and TR_TY = 2")

            If CID = 0 Then
                CID = gAdo.CmdExecScalar("select countFrom  from tblScale where Scale_ID=" & cfrmMain.ScaleNo)
                'CID = CID + 1
            Else
                CID = CID + 1
            End If
            'Dim CID As Integer = gAdo.CmdExecScalar("select isnull( max (CID)+1,0)from tblTransactions where  TR_TY =2 and First_Weigth_Scale_ID=" & ScaleNo)
            'Dim CID As Integer = gAdo.CmdExecScalar("select isnull( max (CID)+1,1)from tblTransactions where  TR_TY =2 ")


            Str &= "'" & Today & "','" & SetShift_DateTime() & "'," & gShiftO.ID & "," & gTransactionO.Car_ID.ID _
            & "," & gTransactionO.Driver_ID.ID & "," & gTransactionO.Car_Info_ID.ID & ",'" & gTransactionO.Dealer_ID.ID & "'" & _
            "," & gTransactionO.Issue_To_ID.ID & "," & gTransactionO.First_Weigth & ",'" & gTransactionO.First_Weight_IsEmpty & "'," & _
            ScaleNo & "," & gTransactionO.First_Weight_User_ID.ID & _
            ",'" & gTransactionO.Second_Weight_IsEmpty & "','" & gTransactionO.Good_ID.ID & "'," & gTransactionO.Slip_Rate & ",'" & Now.Date & "'," & _
            gTransactionO.Slip_Charged_User_ID & ",'" & MsgID & "',0 ,2," & gTransactionO.CustomWeight & "," & CID & "," & gTransactionO.Contractor_ID.ID & "," & "'" & gTransactionO.Notes & "'" & "," & gTransactionO.LocationID & ")"

            gAdo.CmdExec(Str, False)

            'Dim MaxIDofCuurentScale, MAXALL As Integer
            'MaxIDofCuurentScale = gAdo.CmdExecScalar("select isnull( max (CID)+1,0)from tblTransactions where  TR_TY =2 and First_Weigth_Scale_ID=" & ScaleNo)
            'MAXALL = gAdo.CmdExecScalar("select isnull( max (trans_id),0)from tblTransactions")
            'gAdo.CmdExec("update tblTransactions set CID =" & Val(MaxIDofCuurentScale + 1) & " where trans_id=" & MAXALL & " and First_Weigth_Scale_ID=" & ScaleNo)

            MsgBox("تم حفظ بيانات الوزنه الاولى بنجاح", , " الحفظ  ")
            SaveOrCancel = 1
            SIsUpdate = False



            ' حفظ بياتات الوزنة الثانية
        ElseIf SIsUpdate = True And Val(lblFrstWeight.Text) > 100 And Val(lblScndWeight.Text > 100) Then

            If cboGoodType.SelectedValue = 1 Or cboGoodType.SelectedValue = Nothing Then
                MsgBox("من فضلك اختار صنف ")
                SaveOrCancel = 0
                ClearControls = False
                Exit Function
            End If
            If cboDealer.SelectedValue = Nothing Or cboDealer.SelectedValue = 1 Then
                MsgBox("من فضلك اختار عميل ")
                SaveOrCancel = 0
                ClearControls = False
                Exit Function
            End If

            ' ~~~~~~~~~~~~~~~~~ TH ~~~~~~~~~~~~~~~~~~~~~~~~~~ Start ~~~
            ' هنا بشوف الكميه اللى دخلها للعميل علشان اخصم منها وانبهه
            Dim msgResult As MsgBoxResult
            Dim Qnty, CustCount, OverQnty, SumQnty As String
            Dim CQ_ID As Integer
            Dim QDR As SqlDataReader
            'CustCount = gAdo.CmdExecScalar("select isnull(count(CustomerID),0) from tblCustQnty " _
            '& "where CustomerID=" & cboDealer.SelectedValue & "")
            'If CustCount = 0 Then
            '    MsgBox("من فضلك ادخل كمية للعميل ليتم الخصم منها")
            '    SaveOrCancel = 0
            '    ClearControls = False
            '    Exit Function
            'Else
            '    SaveOrCancel = 1
            '    QDR = gAdo.CmdExecReader("select * from tblCustQnty where CQ_ID =(SELECT MAX(CQ_ID) FROM tblCustQnty  where  CustomerID='" & cboDealer.SelectedValue & "')" _
            '                           & " and CustomerID='" & cboDealer.SelectedValue & "'")
            '    While QDR.Read
            '        Qnty = QDR("Quantity")
            '        OverQnty = QDR("OverQuantity")
            '        CQ_ID = QDR("CQ_ID")
            '    End While
            '    QDR.Close()
            '    SumQnty = gAdo.CmdExecScalar("select isnull(sum(Quntity),0) from tblCustQntyDetails where CQ_ID=" & CQ_ID & "")
            'End If


            ''' Elsaman 22/12/2019 '''

            Dim Time As String
            Time = Now
            If Time.Contains("ص") Then
                Time = Time.Replace("ص", "AM")
            Else
                Time = Time.Replace("م", "PM")
            End If


            If SaveOrCancel = 1 Then

                ''' Elsaman 7/1/2020 '''
                ''' this code for shift with quota

                '' Variable From tblShift ====> " وقت نهاية الوردية "
                Dim Shift_End_Time As DateTime
                Shift_End_Time = gAdo.CmdExecScalar(CStr("Select RIGHT(CONVERT(VARCHAR, Shift_End_Time, 100),15) as Time From tblShift where Shift_End_Date = 1"))

                'CONVERT(datetime, Out_Date) <'" & gMethods.GetShifName_And_SearchDate(True)(0) & "'"

                '' Date OF Weight  ===> " تاريخ الوزن "
                Dim WeightTime As DateTime = TimeOfDay


                '' Variable For Test and Comparison
                Dim DateSearch As DateTime = Now
                If WeightTime < Shift_End_Time Then
                    DateSearch = Now.AddDays(-1)
                Else
                    DateSearch = Now
                End If
                DateSearch = DateSearch.Date.ToShortDateString
                ''' Elsaman 18/12/2019 '''
                Dim WhereCondition As String

                WhereCondition = " and TR_TY=2 and  Out_Date is not null and  CONVERT(date, Out_Date) between '" & gMethods.GetShifName_And_SearchDate(True)(0) & "' and '" & gMethods.GetShifName_And_SearchDate(True)(1) & "'"

                'Dim intDebitNoteID As Integer
                'intDebitNoteQuantityID = gAdo.CmdExecScalar("select ContractDetID from tblContractDet join tblContracts on tblContracts .ContractID = tblContractDet .ContractID " & _
                ' " join tblDealer on tblDealer .Dealer_ID = tblContracts .CustomerID join tblGood on tblGood .Good_ID = tblContractDet .GoodID " & _
                '" and tblDealer.Dealer_ID = " & cboDealer.SelectedValue & " and tblGood.Good_ID = '" & cboGoodType.SelectedValue & "'")
                'intDebitNoteID = gAdo.CmdExecScalar(" set dateformat dmy select distinct tblDailyQuota.DebitNoteID  from tblDailyQuota    " & _
                '"  join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID   " & _
                ' " join tblContracts on tblDebitNote .ContractID =tblContracts .ContractID    " & _
                ' " and customerID= " & cboDealer.SelectedValue & " and DateQuota =  CONVERT(date,'" & DateSearch & "')")

                '' TotalweightTrans + current weight
                Dim TotalweightTransWithCurrentWeight As Decimal

                Dim TotalDailyQuota As Decimal

                Dim Quota As Decimal
                Dim OutQuantityfromQuota As Decimal
                Dim RemainQuotaQuantity As Decimal


                Dim NetWeight As Decimal = 0
                Dim Qdt As New DataTable()


                Dim SQLST As String = ""
                SQLST = "Select DQ.DailyQuotaID From tblDailyQuotaWithOutContract DQ Join tblDailyQuotaWOContractGoods DG on(DQ.DailyQuotaID=DG.DailyQuotaID) " & _
                        " where  DQ.CustomerID=" & cboDealer.SelectedValue & " and DG.GoodID='" & cboGoodType.SelectedValue & "' /*and DQ.QuotaDate<=convert(Date,'" & DateSearch & "')*/ and DQ.finish=0"
                If (gAdo.IFExists(SQLST)) Then
                    TotalweightTransWithCurrentWeight = gAdo.CmdExecScalar("Set Dateformat dmy Select isnull(sum(T.Quantity),0)+" & lblNetWeight.Text & "  as TotalweightTrans " & _
                                                                           " From  tblTransQuotaWithOutContract T Join tblDailyQuotaWithOutContract DQ on(T.QuotaID=DQ.DailyQuotaID /*and DQ.QuotaDate<=convert(Date,'" & DateSearch & "')*/) " & _
                                                                           " join  tblDailyQuotaWOContractGoods DG on(DQ.DailyQuotaID=DG.DailyQuotaID) where DQ.CustomerID=" & cboDealer.SelectedValue & " and DG.GoodID='" & cboGoodType.SelectedValue & "'  /*and DQ.QuotaDate<=convert(Date,'" & DateSearch & "')*/ and DQ.finish=0")


                    TotalDailyQuota = gAdo.CmdExecScalar("Set Dateformat dmy Select isnull(Sum(DQ.Quantity*1000),0) From tblDailyQuotaWithOutContract DQ " & _
                                                         " join  tblDailyQuotaWOContractGoods DG on(DQ.DailyQuotaID=DG.DailyQuotaID) " & _
                                                         " where DQ.CustomerID=" & cboDealer.SelectedValue & " and DG.GoodID='" & cboGoodType.SelectedValue & "'  /*and DQ.QuotaDate<=convert(Date,'" & DateSearch & "')*/  and DQ.finish=0")



                    Qdt = gAdo.FillDataTable(" set dateformat dmy Select DQ.DailyQuotaID From tblDailyQuotaWithOutContract DQ " & _
                                             " join  tblDailyQuotaWOContractGoods DG on(DQ.DailyQuotaID=DG.DailyQuotaID) " & _
                                             " where DQ.CustomerID=" & cboDealer.SelectedValue & " and DG.GoodID='" & cboGoodType.SelectedValue & "' and DQ.finish=0 Order By DQ.DailyQuotaID")
                    NetWeight = Decimal.Parse(If(lblNetWeight.Text.Trim() = "", "0", lblNetWeight.Text.Trim()))

                    If TotalweightTransWithCurrentWeight > TotalDailyQuota Then
                        MsgBox(" الوزنة الحالية اكبر من الكوتة اليومية اتصل بقسم المبيعات ")
                        SaveOrCancel = 0
                        ClearControls = False
                        Exit Function
                    Else
                        For i As Integer = 0 To i = Qdt.Rows.Count - 1
                            OutQuantityfromQuota = gAdo.CmdExecScalar("Select isnull(sum(Quantity),0) TQ from tblTransQuotaWithOutContract  where QuotaID=" & Qdt.Rows(i)(0).ToString())
                            Quota = gAdo.CmdExecScalar("Select Quantity*1000  From tblDailyQuotaWithOutContract  where DailyQuotaID=" & Qdt.Rows(i)(0).ToString())
                            RemainQuotaQuantity = Quota - OutQuantityfromQuota

                            If (NetWeight <= RemainQuotaQuantity) Then
                                gTransQuotaWOContractO.ID = 0
                                gTransQuotaWOContractO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
                                gTransQuotaWOContractO.QuotaID = Qdt.Rows(i)(0).ToString()
                                gTransQuotaWOContractO.Quantity = NetWeight
                                gTransQuotaWOContractO.Save()
                                If (NetWeight = RemainQuotaQuantity) Then
                                    SQLST = "Set DateFormat dmy UpDate tblDailyQuotaWithOutContract set finish=1,EndDate='" & GetEndDate() & "' where DailyQuotaID=" & Qdt.Rows(i)(0).ToString()
                                    gAdo.CmdExec(SQLST)
                                End If
                                Exit For
                            ElseIf (NetWeight > RemainQuotaQuantity) Then
                                gTransQuotaWOContractO.ID = 0
                                gTransQuotaWOContractO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
                                gTransQuotaWOContractO.QuotaID = Qdt.Rows(i)(0).ToString()
                                gTransQuotaWOContractO.Quantity = RemainQuotaQuantity
                                gTransQuotaWOContractO.Save()
                                SQLST = "Set DateFormat dmy UpDate tblDailyQuotaWithOutContract set finish=1,EndDate='" & GetEndDate() & "' where DailyQuotaID=" & Qdt.Rows(i)(0).ToString()
                                gAdo.CmdExec(SQLST)
                                NetWeight = NetWeight - RemainQuotaQuantity
                            End If

                        Next

                    End If





                    'intFirstQuotaID = gAdo.CmdExecScalar("Set Dateformat dmy Select Min(DQ.DailyQuotaID) From tblDailyQuotaWithOutContract DQ " & _
                    '                                     " join  tblDailyQuotaWOContractGoods DG on(DQ.DailyQuotaID=DG.DailyQuotaID) " & _
                    '                                     " where DQ.CustomerID=" & cboDealer.SelectedValue & " and DG.GoodID='" & cboGoodType.SelectedValue & "'  /*and DQ.QuotaDate<=convert(Date,'" & DateSearch & "')*/ and DQ.finish=0")

                    'FirstQuota = gAdo.CmdExecScalar("Select Quantity*1000 From tblDailyQuotaWithOutContract where DailyQuotaID=" & intFirstQuotaID)

                    'OutQuantityfromFirstQuota = gAdo.CmdExecScalar("Select isnull(sum(Quantity),0) From tblTransQuotaWithOutContract where QuotaID=" & intFirstQuotaID)

                    'RemainFirstQuotaQuantity = FirstQuota - OutQuantityfromFirstQuota

                    'intSecQuotaID = gAdo.CmdExecScalar("Set Dateformat dmy Select Max(DQ.DailyQuotaID) From tblDailyQuotaWithOutContract DQ " & _
                    '                                     " join  tblDailyQuotaWOContractGoods DG on(DQ.DailyQuotaID=DG.DailyQuotaID) " & _
                    '                                     " where DQ.CustomerID=" & cboDealer.SelectedValue & " and DG.GoodID='" & cboGoodType.SelectedValue & "'  /*and DQ.QuotaDate<=convert(Date,'" & DateSearch & "')*/ and DQ.finish=0")
                    'If (intFirstQuotaID <> intSecQuotaID) Then
                    '    SecQuota = gAdo.CmdExecScalar("Select Quantity*1000 From tblDailyQuotaWithOutContract where DailyQuotaID=" & intSecQuotaID)
                    '    OutFromSecQuota = gAdo.CmdExecScalar("Select isnull(sum(Quantity),0) From tblTransQuotaWithOutContract where QuotaID=" & intSecQuotaID)
                    '    RemainFromSecQuota = SecQuota - OutFromSecQuota
                    'Else
                    '    intSecQuotaID = 0
                    '    SecQuota = 0
                    '    OutFromSecQuota = 0
                    '    RemainFromSecQuota = 0
                    'End If

                    'NetWeight = Decimal.Parse(If(lblNetWeight.Text.Trim() = "", "0", lblNetWeight.Text.Trim()))


                    'If TotalweightTransWithCurrentWeight > TotalDailyQuota Then
                    '    MsgBox(" الوزنة الحالية اكبر من الكوتة اليومية اتصل بقسم المبيعات ")
                    '    SaveOrCancel = 0
                    '    ClearControls = False
                    '    Exit Function
                    'Else
                    '    If NetWeight <= RemainFirstQuotaQuantity Then
                    '        gTransQuotaWOContractO.ID = 0
                    '        gTransQuotaWOContractO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
                    '        gTransQuotaWOContractO.QuotaID = intFirstQuotaID
                    '        gTransQuotaWOContractO.Quantity = lblNetWeight.Text
                    '        gTransQuotaWOContractO.Save()
                    '        If (NetWeight - RemainFirstQuotaQuantity = 0) Then
                    '            SQLST = "Set DateFormat dmy UpDate tblDailyQuotaWithOutContract set finish=1,EndDate='" & GetEndDate() & "' where DailyQuotaID=" & intFirstQuotaID.ToString()
                    '            gAdo.CmdExec(SQLST)
                    '        End If
                    '    ElseIf RemainFirstQuotaQuantity > 0 Then
                    '        gTransQuotaWOContractO.ID = 0
                    '        gTransQuotaWOContractO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
                    '        gTransQuotaWOContractO.QuotaID = intFirstQuotaID
                    '        gTransQuotaWOContractO.Quantity = RemainFirstQuotaQuantity
                    '        gTransQuotaWOContractO.Save()
                    '        SQLST = "Set DateFormat dmy UpDate tblDailyQuotaWithOutContract set finish=1,EndDate='" & GetEndDate() & "' where DailyQuotaID=" & intFirstQuotaID.ToString()
                    '        gAdo.CmdExec(SQLST)
                    '        gTransQuotaWOContractO.ID = 0
                    '        gTransQuotaWOContractO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
                    '        gTransQuotaWOContractO.QuotaID = intSecQuotaID
                    '        gTransQuotaWOContractO.Quantity = NetWeight - RemainFirstQuotaQuantity
                    '        gTransQuotaWOContractO.Save()
                    '        If (RemainFromSecQuota - (NetWeight - RemainFirstQuotaQuantity) = 0) Then
                    '            SQLST = "Set DateFormat dmy UpDate tblDailyQuotaWithOutContract set finish=1,EndDate='" & GetEndDate() & "' where DailyQuotaID=" & intSecQuotaID.ToString()
                    '            gAdo.CmdExec(SQLST)
                    '        End If
                    '    Else
                    '        gTransQuotaWOContractO.ID = 0
                    '        gTransQuotaWOContractO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
                    '        gTransQuotaWOContractO.QuotaID = intSecQuotaID
                    '        gTransQuotaWOContractO.Quantity = NetWeight
                    '        gTransQuotaWOContractO.Save()
                    '        If (RemainFromSecQuota - NetWeight = 0) Then
                    '            SQLST = "Set DateFormat dmy UpDate tblDailyQuotaWithOutContract set finish=1,EndDate='" & GetEndDate() & "' where DailyQuotaID=" & intSecQuotaID.ToString()
                    '            gAdo.CmdExec(SQLST)
                    '        End If
                    '    End If

                    'End If


                Else

                    TotalweightTransWithCurrentWeight = gAdo.CmdExecScalar(" set dateformat dmy select isnull(sum(tblTransQuota.Quantity),0) + " & lblNetWeight.Text & "  as TotalweightTrans from tblTransQuota " & _
                                                                     " join tblDailyQuota on tblTransQuota .QuotaID =tblDailyQuota .QuotaID 	/*and DateQuota <=convert(Date,'" & DateSearch & "')*/ " & _
                                                                     " join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID " & _
                                                                     " where tblDebitNote .ContractID in (select tblContractDet.ContractID   from  tblContractDet " & _
                                                                     " join tblContracts on tblContractDet .ContractID =tblContracts .ContractID " & _
                                                                     " where CustomerID = " & cboDealer.SelectedValue & " and GoodID = '" & cboGoodType.SelectedValue & "') and tblDailyQuota.Finished=0")

                    'TotalweightTransWithCurrentWeight = gAdo.CmdExecScalar(" set dateformat dmy select isnull(sum(Second_Weight-First_Weigth),0)  + " & lblNetWeight.Text & "  as TotalweightTrans from tblTransactions " & _
                    '                                      " where(Dealer_ID = " & cboDealer.SelectedValue & " And Good_ID in(select GoodID  from tblDailyQuota join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID " & _
                    '                                      " join tblContractDet on tblDebitNote .ContractID =tblContractDet .ContractID join tblContracts on tblContractDet .ContractID =tblContracts .ContractID where CustomerID ='" + cboDealer.SelectedValue.ToString() + "' and DateQuota =convert(Date,'" & DateSearch & "'))" & WhereCondition & " ) ")

                    '' Total Trans Weight Without Current Weight

                    'TotalTransWeightWithoutCurrentWeight = gAdo.CmdExecScalar(" set dateformat dmy select isnull(sum(Second_Weight-First_Weigth),0)  as TotalweightTrans from tblTransactions " & _
                    '                                      " where(Dealer_ID = " & cboDealer.SelectedValue & " And Good_ID in(select GoodID  from tblDailyQuota join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID join tblContractDet on tblDebitNote .ContractID =tblContractDet .ContractID join tblContracts on tblContractDet .ContractID =tblContracts .ContractID where CustomerID ='" + cboDealer.SelectedValue.ToString() + "' and DateQuota =convert(Date,'" & DateSearch & "'))" & WhereCondition & " ) ")

                    'TotalTransWeightWithoutCurrentWeight = gAdo.CmdExecScalar(" set dateformat dmy select isnull(sum(tblTransQuota.Quantity),0) as TotalweightTrans from tblTransQuota " & _
                    '                                                 " join tblDailyQuota on tblTransQuota .QuotaID =tblDailyQuota .QuotaID 	/*and DateQuota <=convert(Date,'" & DateSearch & "')*/ " & _
                    '                                                 " join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID " & _
                    '                                                 " where tblDebitNote .ContractID in (select tblContractDet.ContractID   from  tblContractDet " & _
                    '                                                 " join tblContracts on tblContractDet .ContractID =tblContracts .ContractID " & _
                    '                                                 " where CustomerID = " & cboDealer.SelectedValue & " and GoodID = '" & cboGoodType.SelectedValue & "') and tblDailyQuota.Finished=0")




                    '' this Code TO Replace  "م" and "ًص"


                    'If TheDate.Contains("م") Then
                    '    TheDate = TheDate.Replace("م", "PM")
                    'ElseIf TheDate.Contains("ص") Then
                    '    TheDate = TheDate.Replace("ص", "AM")
                    'End If


                    '' The Total Daily Quote for this Customer on this Day    '' Deleted this Code [ * (1+tblContracts.QuantityRatio/100)]
                    '" join tblDebitNoteQuantity  on tblDebitNoteQuantity.DebitNoteQuantityID  = tblDailyQuota.DebitNoteQuantityID  " & _



                    TotalDailyQuota = gAdo.CmdExecScalar(" set dateformat dmy select ISNULL(d.quota,0) from (select (isnull(sum (Quota),0)*1000)-(isnull(sum(OutGoingQty),0)*1000) as quota from tblDailyQuota " & _
                                                     " join tblDebitNote on tblDebitNote.DebitNoteID =tblDailyQuota.DebitNoteID  " & _
                                                     " join tblContracts on tblContracts.ContractID = tblDebitNote.ContractID   " & _
                                                     " join tblContractDet on tblContracts .ContractID =tblContractDet .ContractID    " & _
                                                    " where  CustomerID = " & cboDealer.SelectedValue & " /*and DateQuota <=  CONVERT(date,'" & DateSearch & "')*/ and tblDailyQuota.Finished=0 and tblContractDet.GoodID ='" & cboGoodType.SelectedValue & "' ) as d")

                    '' MinimumQuota

                    'MinimumQuota = gAdo.CmdExecScalar(" set dateformat dmy select Quota from tblDailyQuota join tblContractDet on tblContractDet.ContractDetID = tblDailyQuota.ContractDetID " & _
                    '                                    " join tblContracts on tblContracts.ContractID = tblContractDet.ContractID where CustomerID = 17170 " & _
                    '                                    " and QuotaID in (select min(QuotaID) from tblDailyQuota where DateQuota = '" & Date.Now.Date & "') ")
                    'MinimumQuota = gAdo.CmdExecScalar("set dateformat dmy select Quota from tblDailyQuota where QuotaID in (select min(QuotaID) from tblDailyQuota where DateQuota = CONVERT(date,'" & DateSearch & "'))")
                    'FirstQuota = gAdo.CmdExecScalar(" set dateformat dmy select distinct ISNULL(d.quota,0) from ( select  (ISNULL(sum(Quota),0)*1000)-(isnull(sum(OutGoingQty),0)*1000) as quota from tblDailyQuota " & _
                    '                               " join tblDebitNote on tblDebitNote .DebitNoteID =tblDailyQuota .DebitNoteID  " & _
                    '                               " join tblContracts on tblContracts.ContractID = tblDebitNote.ContractID   " & _
                    '                                " join tblContractDet on tblContracts .ContractID =tblContractDet .ContractID    " & _
                    '                               " where CustomerID = " & cboDealer.SelectedValue & " /*and DateQuota <=  CONVERT(date,'" & DateSearch & "')*/ and tblDailyQuota.Finished=0 and tblContractDet.GoodID ='" & cboGoodType.SelectedValue & "' and QuotaID=(Select Min(D.QuotaID) From tblDailyQuota D join tblDebitNote DN on DN.DebitNoteID =D.DebitNoteID join tblContracts C on C.ContractID = DN.ContractID  join tblContractDet CD on C .ContractID =CD .ContractID where CustomerID = " & cboDealer.SelectedValue & "  /*and DateQuota <=  CONVERT(date,'" & DateSearch & "')*/ and D.Finished=0 and GoodID='" & cboGoodType.SelectedValue.ToString() & "' ) " & _
                    '                               " ) as d")



                    '' To Git ContractDetID From tblContractDet
                    'Dim intDebitNoteQuantityID As Integer
                    'intDebitNoteQuantityID = gAdo.CmdExecScalar("select ContractDetID from tblContractDet join tblContracts on tblContracts .ContractID = tblContractDet .ContractID " & _
                    ' " join tblDealer on tblDealer .Dealer_ID = tblContracts .CustomerID join tblGood on tblGood .Good_ID = tblContractDet .GoodID " & _
                    '" and tblDealer.Dealer_ID = " & cboDealer.SelectedValue & " and tblGood.Good_ID = '" & cboGoodType.SelectedValue & "'")
                    'intDebitNoteQuantityID = gAdo.CmdExecScalar("select  DebitNoteQuantityID  from tblDebitNoteQuantity  " & _
                    '                                    " join tblDebitNote on tblDebitNote .DebitNoteID =tblDebitNoteQuantity .DebitNoteID  " & _
                    '                                          " join tblContracts on tblContracts .ContractID = tblDebitNote .ContractID   " & _
                    '                                                "  join tblDealer on tblDealer .Dealer_ID = tblContracts .CustomerID  " & _
                    '                                                  "   join tblGood on tblGood .Good_ID = tblDebitNoteQuantity .GoodID   " & _
                    '                                   " and tblDealer.Dealer_ID = " & cboDealer.SelectedValue & " and tblGood.Good_ID = '" & cboGoodType.SelectedValue & "'")

                    'intQuotaID = gAdo.CmdExecScalar("select QuotaID from tblDailyQuota where ContractDetID = " & intContractDetID)
                    Qdt = gAdo.FillDataTable(" set dateformat dmy select ISNULL(QuotaID,0) QID from tblDailyQuota    " & _
                                                         " join tblDebitNote on tblDebitNote .DebitNoteID =tblDailyQuota .DebitNoteID  " & _
                                                         "  join tblContracts on tblDebitNote.ContractID =tblContracts.ContractID   " & _
                                                         " join tblContractDet on tblContracts .ContractID =tblContractDet .ContractID    " & _
                                                         " and CustomerID = " & cboDealer.SelectedValue & " /*and DateQuota <=  CONVERT(date,'" & DateSearch & "')*/ and tblDailyQuota.Finished=0 and tblContractDet.GoodID ='" & cboGoodType.SelectedValue & "' Order By QuotaID")
                    NetWeight = Decimal.Parse(If(lblNetWeight.Text.Trim() = "", "0", lblNetWeight.Text.Trim()))
                    If TotalweightTransWithCurrentWeight > TotalDailyQuota Then
                        MsgBox(" الوزنة الحالية اكبر من الكوتة اليومية اتصل بقسم المبيعات ")
                        SaveOrCancel = 0
                        ClearControls = False
                        Exit Function
                    Else
                        For i As Integer = 0 To i = Qdt.Rows.Count - 1
                            OutQuantityfromQuota = gAdo.CmdExecScalar("Select isnull(sum(Quantity),0) TQ from tblTransQuota  where QuotaID=" & Qdt.Rows(i)(0).ToString())
                            Quota = gAdo.CmdExecScalar("Select (Quota-OutGoingQty)*1000  From tblDailyQuota  where QuotaID=" & Qdt.Rows(i)(0).ToString())
                            RemainQuotaQuantity = Quota - OutQuantityfromQuota

                            If (NetWeight <= RemainQuotaQuantity) Then
                                gTransQuotaO.ID = 0
                                gTransQuotaO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
                                gTransQuotaO.QuotaID = Qdt.Rows(i)(0).ToString()
                                gTransQuotaO.Quantity = NetWeight
                                gTransQuotaO.Save()
                                If (NetWeight = RemainQuotaQuantity) Then
                                    SQLST = "Set DateFormat dmy UpDate tblDailyQuota set Finished=1,EndDate='" & GetEndDate() & "' where QuotaID=" & Qdt.Rows(i)(0).ToString()
                                    gAdo.CmdExec(SQLST)
                                End If
                                Exit For
                            ElseIf (NetWeight > RemainQuotaQuantity) Then
                                gTransQuotaO.ID = 0
                                gTransQuotaO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
                                gTransQuotaO.QuotaID = Qdt.Rows(i)(0).ToString()
                                gTransQuotaO.Quantity = RemainQuotaQuantity
                                gTransQuotaO.Save()
                                SQLST = "Set DateFormat dmy UpDate tblDailyQuota set Finished=1,EndDate='" & GetEndDate() & "' where QuotaID=" & Qdt.Rows(i)(0).ToString()
                                gAdo.CmdExec(SQLST)
                                NetWeight = NetWeight - RemainQuotaQuantity
                            End If


                        Next
                    End If



                    'intFirstQuotaID = gAdo.CmdExecScalar(" set dateformat dmy select ISNULL(Min(QuotaID),0) from tblDailyQuota    " & _
                    '                                     " join tblDebitNote on tblDebitNote .DebitNoteID =tblDailyQuota .DebitNoteID  " & _
                    '                                     "  join tblContracts on tblDebitNote.ContractID =tblContracts.ContractID   " & _
                    '                                     " join tblContractDet on tblContracts .ContractID =tblContractDet .ContractID    " & _
                    '                                     " and CustomerID = " & cboDealer.SelectedValue & " /*and DateQuota <=  CONVERT(date,'" & DateSearch & "')*/ and tblDailyQuota.Finished=0 and tblContractDet.GoodID ='" & cboGoodType.SelectedValue & "' ")


                    'OutQuantityfromFirstQuota = gAdo.CmdExecScalar(" set dateformat dmy select isnull(sum(tblTransQuota.Quantity),0) as TotalweightTrans from tblTransQuota " & _
                    '                                                 " join tblDailyQuota on tblTransQuota .QuotaID =tblDailyQuota .QuotaID 	/*and DateQuota <=convert(Date,'" & DateSearch & "')*/ " & _
                    '                                                 " join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID " & _
                    '                                                 " where tblDebitNote .ContractID in (select tblContractDet.ContractID   from  tblContractDet " & _
                    '                                                 " join tblContracts on tblContractDet .ContractID =tblContracts .ContractID " & _
                    '                                                 " where CustomerID = " & cboDealer.SelectedValue & " and GoodID = '" & cboGoodType.SelectedValue & "' ) and tblTransQuota .QuotaID=" & intFirstQuotaID.ToString())

                    'RemainFirstQuotaQuantity = FirstQuota - OutQuantityfromFirstQuota


                    'intSecQuotaID = gAdo.CmdExecScalar("select QuotaID from tblDailyQuota join tblContractDet on tblDailyQuota.ContractDetID =tblContractDet .ContractDetID " & _
                    '                                     " join tblContracts on tblContractDet.ContractID =tblContracts.ContractID " & _
                    '                                      " and CustomerID = " & cboDealer.SelectedValue & " and GoodID = '" & cboGoodType.SelectedValue & _
                    '                                      "' ORDER BY  tblContracts.ContractID DESC")

                    'intSecQuotaID = gAdo.CmdExecScalar("set dateformat dmy select ISNULL(Max(QuotaID),0) from tblDailyQuota    " & _
                    '                                     " join tblDebitNote on tblDebitNote .DebitNoteID =tblDailyQuota .DebitNoteID  " & _
                    '                                      "  join tblContracts on tblDebitNote.ContractID =tblContracts.ContractID   " & _
                    '                                      " join tblContractDet on tblContracts .ContractID =tblContractDet .ContractID    " & _
                    '                                      " and CustomerID = " & cboDealer.SelectedValue & " /*and DateQuota <=  CONVERT(date,'" & DateSearch & "')*/  and tblDailyQuota.Finished=0 and tblContractDet.GoodID ='" & cboGoodType.SelectedValue & "' ")


                    'SecQuota = gAdo.CmdExecScalar("Select (isnull(Quota,0)*1000)-(isnull(OutGoingQty,0)*1000) From tblDailyQuota where QuotaID=" & intSecQuotaID)



                    'OutFromSecQuota = gAdo.CmdExecScalar(" set dateformat dmy select isnull(sum(tblTransQuota.Quantity),0) as TotalweightTrans from tblTransQuota " & _
                    '                                                 " join tblDailyQuota on tblTransQuota .QuotaID =tblDailyQuota .QuotaID 	/*and DateQuota <=convert(Date,'" & DateSearch & "')*/ " & _
                    '                                                 " join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID " & _
                    '                                                 " where tblDebitNote .ContractID in (select tblContractDet.ContractID   from  tblContractDet " & _
                    '                                                 " join tblContracts on tblContractDet .ContractID =tblContracts .ContractID " & _
                    '                                                 " where CustomerID = " & cboDealer.SelectedValue & " and GoodID = '" & cboGoodType.SelectedValue & "' ) and tblTransQuota .QuotaID=" & intSecQuotaID.ToString())


                    'RemainFromSecQuota = SecQuota - OutFromSecQuota



                    ' Complete Her
                    'If TotalweightTransWithCurrentWeight > TotalDailyQuota Then    ' If TotalweightTransWithCurrentWeight <= TotalDailyQuota Then
                    '    MsgBox(" الوزنة الحالية اكبر من الكوتة اليومية اتصل بقسم المبيعات ")
                    '    SaveOrCancel = 0
                    '    ClearControls = False
                    '    Exit Function
                    'Else
                    '    If NetWeight <= RemainFirstQuotaQuantity Then
                    '        '' insert into tblTransQuota By Quantity = lblNetWeight.Text (صافي الوزنة)
                    '        gTransQuotaO.ID = 0
                    '        gTransQuotaO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
                    '        gTransQuotaO.QuotaID = intFirstQuotaID
                    '        gTransQuotaO.Quantity = lblNetWeight.Text
                    '        gTransQuotaO.Save()
                    '        If (NetWeight - RemainFirstQuotaQuantity = 0) Then
                    '            SQLST = "Set DateFormat dmy UpDate tblDailyQuota set Finished=1,EndDate='" & GetEndDate() & "' where QuotaID=" & intFirstQuotaID.ToString()
                    '            gAdo.CmdExec(SQLST)
                    '        End If

                    '    Else
                    '        If RemainFirstQuotaQuantity > 0 Then
                    '            '' insert first in tblTransQuota
                    '            gTransQuotaO.ID = 0
                    '            gTransQuotaO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where   TR_TY=2 and  CID=" & txtTransID.Text)
                    '            gTransQuotaO.QuotaID = intFirstQuotaID
                    '            gTransQuotaO.Quantity = RemainFirstQuotaQuantity
                    '            gTransQuotaO.Save()
                    '            SQLST = "Set DateFormat dmy UpDate tblDailyQuota set Finished=1,EndDate='" & GetEndDate() & "'  where QuotaID=" & intFirstQuotaID.ToString()
                    '            gAdo.CmdExec(SQLST)
                    '            '' insert Second in tblTransQuota
                    '            gTransQuotaO.ID = 0
                    '            gTransQuotaO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and  CID=" & txtTransID.Text)
                    '            gTransQuotaO.QuotaID = intSecQuotaID
                    '            'gTransQuotaO.Quantity = (lblNetWeight.Text) - MinimumQuota
                    '            gTransQuotaO.Quantity = NetWeight - RemainFirstQuotaQuantity
                    '            gTransQuotaO.Save()
                    '            If (RemainFromSecQuota - (NetWeight - RemainFirstQuotaQuantity) = 0) Then
                    '                SQLST = "Set DateFormat dmy UpDate tblDailyQuota set Finished=1,EndDate='" & GetEndDate() & "'  where QuotaID=" & intSecQuotaID.ToString()
                    '                gAdo.CmdExec(SQLST)
                    '            End If
                    '        Else
                    '            gTransQuotaO.ID = 0
                    '            gTransQuotaO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and  CID=" & txtTransID.Text)
                    '            gTransQuotaO.QuotaID = intSecQuotaID
                    '            gTransQuotaO.Quantity = lblNetWeight.Text
                    '            gTransQuotaO.Save()
                    '            If (RemainFromSecQuota - NetWeight = 0) Then
                    '                SQLST = "Set DateFormat dmy UpDate tblDailyQuota set Finished=1,EndDate='" & GetEndDate() & "'  where QuotaID=" & intSecQuotaID.ToString()
                    '                gAdo.CmdExec(SQLST)
                    '            End If

                    '        End If
                    '    End If


                    'If TotalweightTransWithCurrentWeight <= FirstQuota Then
                    '    '' insert into tblTransQuota By Quantity = lblNetWeight.Text (صافي الوزنة)
                    '    gTransQuotaO.ID = 0
                    '    gTransQuotaO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where   TR_TY=2 and  CID=" & txtTransID.Text)
                    '    gTransQuotaO.QuotaID = intFirstQuotaID
                    '    gTransQuotaO.Quantity = lblNetWeight.Text
                    '    gTransQuotaO.Save()

                    'Else
                    '    If TotalTransWeightWithoutCurrentWeight < FirstQuota Then
                    '        '' insert first in tblTransQuota
                    '        gTransQuotaO.ID = 0
                    '        gTransQuotaO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where   TR_TY=2 and  CID=" & txtTransID.Text)
                    '        gTransQuotaO.QuotaID = intFirstQuotaID
                    '        gTransQuotaO.Quantity = FirstQuota - TotalTransWeightWithoutCurrentWeight
                    '        gTransQuotaO.Save()
                    '        '' insert Second in tblTransQuota
                    '        gTransQuotaO.ID = 0
                    '        gTransQuotaO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and  CID=" & txtTransID.Text)
                    '        gTransQuotaO.QuotaID = intSecQuotaID
                    '        'gTransQuotaO.Quantity = (lblNetWeight.Text) - MinimumQuota
                    '        gTransQuotaO.Quantity = TotalweightTransWithCurrentWeight - FirstQuota
                    '        gTransQuotaO.Save()
                    '    Else
                    '        gTransQuotaO.ID = 0
                    '        gTransQuotaO.TransID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and  CID=" & txtTransID.Text)
                    '        gTransQuotaO.QuotaID = intSecQuotaID
                    '        gTransQuotaO.Quantity = lblNetWeight.Text
                    '        gTransQuotaO.Save()

                    '    End If
                    'End If


                End If
            End If

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim NowDate As DateTime = Now
            Dim OUTDATE As String
            OUTDATE = Now
            If OUTDATE.Contains("م") Then
                OUTDATE = OUTDATE.Replace("م", "PM")
            ElseIf OUTDATE.Contains("ص") Then
                OUTDATE = OUTDATE.Replace("ص", "AM")
            End If

            'Dim Lastweight As Decimal
            'Lastweight = gAdo.CmdExecScalar("Select Isnull(LastWeight,0) From tblScale where Scale_ID=" & ScaleNo)

            'If ((Lastweight - Val(lblScndWeight.Text) >= 0 And Lastweight - Val(lblScndWeight.Text) <= 150) Or (Val(lblScndWeight.Text) - Lastweight >= 0 And Val(lblScndWeight.Text) - Lastweight <= 150)) Then
            '    gAdo.CmdExec("Insert tblControlScale(UserID,MoveDate,MoveWeight) Values(" & gTransactionO.First_Weight_User_ID.ID & ",'" & OUTDATE & "'," & lblScndWeight.Text & ")")
            'End If

            'If (Val(lblScndWeight.Text) > 0) Then
            '    gAdo.CmdExec("UpDate tblScale set LastWeight=" & lblScndWeight.Text & " where Scale_ID=" & ScaleNo)
            'End If


            Dim TRANSID As Integer

            'TRANSID = gAdo.CmdExecScalar("select trans_id from tblTransactions where First_Weigth_Scale_ID=" & ScaleNo & " and CID =" & ID)
            TRANSID = gAdo.CmdExecScalar("select trans_id from tblTransactions where CID =" & ID)
            'If TRANSID = 0 Then
            '    ' عشان لو مش لقى حاجه بتاعته يروح يدور عليها للميزان التانى
            '    TRANSID = gAdo.CmdExecScalar("select trans_id from tblTransactions where First_Weigth_Scale_ID=3 and CID =" & ID)
            'End If

            Dim LID As String = ""
            If (cboLocation.SelectedValue = Nothing Or cboLocation.Text = "") Then
                LID = "0"
            Else : LID = cboLocation.SelectedValue.ToString()
            End If

            Str = "set dateformat dmy Update tblTransactions" & " Set Out_Date = '" & OUTDATE & "'" _
                & ",Second_Weight=" & gTransactionO.Second_Weight & _
              ",First_Weigth=" & gTransactionO.First_Weigth & _
              ",Contractor_ID=" & gTransactionO.Contractor_ID.ID & _
                  ",CustomWeight=" & gTransactionO.CustomWeight & _
                  ",Car_ID='" & gTransactionO.Car_ID.ID & "'" & _
                  ",Driver_ID =" & gTransactionO.Driver_ID.ID & " ,Dealer_ID = " & gTransactionO.Dealer_ID.ID & " ,Issue_To_ID =" & gTransactionO.Issue_To_ID.ID & " ,Good_ID='" & gTransactionO.Good_ID.ID & "'" & _
                  ",Second_Weight_User_ID=" & gTransactionO.Second_Weight_User_ID.ID & _
                  ",Second_Weight_Scale_ID=" & ScaleNo & _
                  ",MSGID=" & MsgID & _
                  ",Car_Info_ID=" & gTransactionO.Car_Info_ID.ID & _
                  ",Slip_Rate=" & gTransactionO.Slip_Rate & ",Slip_Charged_Date='" & Now.Date & "'" & _
                  ",Slip_Charged_User_ID=" & gTransactionO.Slip_Charged_User_ID & ",LocationID=" & LID & " Where  TR_TY=2 and  CID=" & txtTransID.Text

            ' If PrintTransaction(NowDate) = True Then

            gAdo.CmdExec(Str, False)

            ' ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Start Printing Thread ~~~~~~~~~~~~ TH ~~~  
            ' i made a thread for printing to not make the progam freez while printing 
            Dim PrintMSG As MsgBoxResult
            PrintMSG = MsgBox("هـل ترغـب فى طباعة الكارته ؟", MsgBoxStyle.OkCancel, " الطباعة  ")



            PTransID = txtTransID.Text
            If PrintMSG = MsgBoxResult.Ok Then
                NOPrinted = Val(InputBox("عدد مرات النسخ", "عدد مرات الطباعة", Val(gAdo.CmdExecScalar("select value from tblvalue where serial=1"))))
                AutoPrint = False
                PCarNo = txtCarNo.Text
                PCarCity = cboCarCity.Text
                PTruckNo = cboTruckNo.Text
                PTruckCity = cboTruckCity.Text
                PDriverName = cboDriver.Text
                PDriverLicence = txtLicenceNo.Text
                PDealerName = cboDealer.Text
                PIssueto = cboIssueTo.Text
                PGoodName = cboGoodType.Text

                If CboRecieveType.Text = "إسكندرية" Or CboRecieveType.Text = "" Then
                    PRecieveType = ""
                ElseIf CboRecieveType.Text = "العامرية" Then
                    PRecieveType = "العامرية"
                End If

                PFisrtScale = CDec(Val(lblFrstWeight.Text.Trim))
                PSecondScale = CDec(Val(lblScndWeight.Text.Trim))
                If Val(lblScndWeight.Text) < 1 Then
                    PNetScale = 0
                Else
                    PNetScale = Abs(CDec(Val(lblFrstWeight.Text.Trim)) - CDec(Val(lblScndWeight.Text.Trim)))
                End If
                PArabic = HANY(Val(lblNetWeight.Text.Trim), "EGYPT")
                Dim Thread As New System.Threading.Thread(AddressOf PrintNewReport)
                Control.CheckForIllegalCrossThreadCalls = False
                Thread.Start()
            End If

            ' ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ End Printing Thread ~~~~~~~~~~~~~ TH ~~~  

            ' PrintTransaction(gAdo.CmdExecScalar("select Out_Date from tblTransactions where Trans_ID=" & txtTransID.Text))
            ' here to insert into the Server DB ~~~~~~~~~~~~~~~~~~ Start ~~
            ' Dim ServerDataBaseInsertThread As New System.Threading.Thread(AddressOf InsertIntoServerDB)
            ' Control.CheckForIllegalCrossThreadCalls = False
            ' ServerDataBaseInsertThread.Start()
            ' here to insert into the Server DB ~~~~~~~~~~~~~~~~~~~~ End ~~
lk:
            ''~~~~~~~~~~ Insert The Transaction Into The Server DB ~~~~~~~~~~ Start ~~
            'Dim ThreadServerInsert As New System.Threading.Thread(AddressOf InsertIntoServerDB)
            'Control.CheckForIllegalCrossThreadCalls = False
            'Thread.Join()
            'ThreadServerInsert.Start()
            'ThreadInsertServerDB()
            'InsertIntoServerDB()
            InsertIntoFailed()
            ''~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ END ~~
            MsgBox("تم حفظ بيانات الوزنه الثانيه بنجاح", , " الحفظ  ")
            SIsUpdate = False
            'End If

        ElseIf SIsUpdate = True Then

            Str = "set dateformat dmy Update tblTransactions set Driver_ID=" & gTransactionO.Driver_ID.ID & ",Slip_Rate=" & gTransactionO.Slip_Rate & ",Car_ID=" & gTransactionO.Car_ID.ID & ",Car_Info_ID=" & gTransactionO.Car_Info_ID.ID & " Where  TR_TY=2 and  CID=" & txtTransID.Text
            gAdo.CmdExec(Str, False)
            '       & " Set  First_Weigth=" & gTransactionO.First_Weigth & _
            '        ",CustomWeight='" & gTransactionO.CustomWeight & "'" & _
            '         ",Car_ID='" & gTransactionO.Car_ID.ID & "'" & _
            '         ",Driver_ID =" & gTransactionO.Driver_ID.ID & " ,Dealer_ID = " & gTransactionO.Dealer_ID.ID & " ,Issue_To_ID =" & gTransactionO.Issue_To_ID.ID & " ,Good_ID='" & gTransactionO.Good_ID.ID & "'" & _
            '         ",First_Weight_User_ID=" & gTransactionO.First_Weight_User_ID.ID & _
            '         ",Second_Weight_Scale_ID=" & ScaleNo & _
            '         ",Car_Info_ID=" & gTransactionO.Car_Info_ID.ID & _
            '         ",MSGID=" & MsgID & _
            '         ",Slip_Rate=" & gTransactionO.Slip_Rate & ",Slip_Charged_Date='" & Now.Date & "'" & _
            '         ",Contractor_ID=" & gTransactionO.Contractor_ID.ID & _
            '         ",Slip_Charged_User_ID=" & gTransactionO.Slip_Charged_User_ID & " Where  TR_TY=2 and  CID=" & txtTransID.Text
            ''gAdo.CmdExec("update tblCar set CarBoard_No='" & txtCarNo.Text & "' where Car_ID= '" & gCarO.ID & "'", False)
            'gAdo.CmdExec(Str, False)
            'SaveOrCancel = 1
            'ClearControls = True
            'SIsUpdate = False
            'MsgBox("تم الحفـــظ بنجاح ", , " الحفظ  ")
            'MsgBox(" لا يمكنك التعديل")
            MsgBox("تم التعديل بنجاح")



        End If
    End Function


    Public Function IsErrorMsg(ByVal ParamArray Ctrls() As Control) As Boolean
        Dim i As Short
        For i = 0 To UBound(Ctrls)
            Select Case Ctrls(i).Name
                Case txtCarNo.Name
                    If txtCarNo.Text <> "" Then
                        gCarO.Name = txtCarNo.Text.Trim
                        gMsg = ""
                    Else
                        gMsg = "من فضلك ادخل رقم السياره"
                        Exit For
                    End If

                Case cboCarCity.Name
                    If cboCarCity.Text <> "" Then
                        gCarCity = cboCarCity.Text.Trim
                        gMsg = ""
                    Else
                        'gMsg = "من فضلك ادخل اسم المدينه الخاصه بالسياره"
                        'Exit For
                    End If
                Case cboTruckNo.Name

                    If cboTruckNo.Text <> "" Then
                        gCarInfoO.Name = cboTruckNo.Text.Trim
                        gMsg = ""
                    End If
                    'Else
                    '    gMsg = "من فضلك ادخل رقم المقطوره"
                    '    Exit For
                    'End If


                    ' ''If LblTruckNoValidate.Text <> "هذه المقطوره مستخدمه مع سياره أخرى" Then

                    ' ''    gCarInfoO.Name = cboTruckNo.Text.Trim
                    ' ''    gMsg = ""
                    ' ''Else
                    ' ''    gMsg = "من فضلك أختر رقم مقطوره أخرى لأن المقطوره مازالة  مستخدمه مع سياره أخرى"
                    ' ''    Exit For
                    ' ''End If


                Case cboTruckCity.Name
                    If cboTruckCity.Text <> "" Then
                        gTruckCity = cboTruckCity.Text.Trim
                        gMsg = ""
                    End If
                    'Else
                    '    gMsg = "من فضلك ادخل اسم المدينه الخاصه بالمقطوره"
                    '    Exit For
                    'End If

                Case cboDriver.Name

                    If cboDriver.Text <> "" Then
                        gDriverO.Name = cboDriver.Text.Trim
                        gMsg = ""

                    Else

                        'gMsg = "من فضلك ادخل اسم السائق"
                        'Exit For
                    End If

                    If LblDriverValidate.Text <> "هذا السائق غير متاح" Then

                        gDriverO.Name = cboDriver.Text.Trim
                        gMsg = ""
                    Else
                        gMsg = "من فضلك أختر سائق أخر لأن السائق مازال مع سياره أخرى"
                        Exit For
                    End If


                Case txtLicenceNo.Name
                    If txtLicenceNo.Text <> "" Then
                        gDriverO.Driver_License_No = txtLicenceNo.Text.Trim
                        gMsg = ""
                    Else
                        'gMsg = "من فضلك ادخل رخصه السائق"
                        'Exit For
                    End If

                Case cboDealer.Name
                    If cboDealer.Text <> "" Then
                        gDealerO.Name = cboDealer.Text.Trim
                        gMsg = ""
                    Else
                        'gMsg = "من فضلك ادخل اسم العميل"
                        'Exit For
                    End If

                Case cboIssueTo.Name
                    If cboIssueTo.Text <> "" Then
                        gIssueToO.Name = cboIssueTo.Text.Trim
                        gMsg = ""
                    Else
                        'gMsg = "من فضلك ادخل اسم المرسل إليه"
                        'Exit For
                    End If

                Case cboGoodType.Name
                    'If cboGoodType.SelectedIndex <> -1 Then
                    '    gGoodO.ID = cboGoodType.SelectedValue
                    '    gGoodO.Name = cboGoodType.Text.Trim
                    '    gMsg = ""
                    'Else
                    '    gMsg = "من فضلك اختار نوع الحموله"
                    '    Exit For
                    'End If
                    If cboGoodType.Text <> "" Then
                        'gGoodO.ID = cboGoodType.SelectedValue
                        gGoodO.Name = cboGoodType.Text.Trim
                        gMsg = ""
                    Else
                        'gMsg = "من فضلك اختار نوع الحموله"
                        'Exit For
                    End If
                Case CboSlip.Name
                    'If IsNumeric(CboSlip.Text) = False Then
                    '    gMsg = " لا يسمح بدخول الحروف أو ترك خانة الأسعار فارعه"
                    '    Exit For
                    'Else
                    '    gMsg = ""
                    'End If

                    'If CboSlip.Text <> 0 Then
                    '    gMsg = ""

                    'Else
                    '    gMsg = "لايسمح بدخول الأصفار فى خانة الأسعار"
                    '    Exit For
                    'End If


            End Select
        Next
        If gMsg = "" Then
            Return False
        Else
            Ctrls(i).Focus()
            Return True
        End If
    End Function

#End Region

#Region "SetShift_DateTime"

    Function SetShift_DateTime() As String
        Dim NowTime As String = Mid(Now, 12, 11)
        Dim Shift_Start_Date As Boolean
        Dim Shift_End_Date As Boolean
        Dim DT As New DataTable
        ' ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ TH ~~~
        If NowTime.Contains("ص") Then
            NowTime = NowTime.Replace("ص", "AM")
        ElseIf NowTime.Contains("م") Then
            NowTime = NowTime.Replace("م", "PM")
        End If
        ' ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        Dim DR As SqlClient.SqlDataReader = gAdo.CmdExecReader("set dateformat dmy select Shift_Start_Date,Shift_End_Date,Shift_ID from tblShift " _
                      & "where CONVERT(datetime, Shift_Start_Time) < '01/01/1900 " & NowTime _
                      & "' and CONVERT(datetime, Shift_End_Time) >'01/01/1900 " & NowTime & "'and Shift_IsDeleted='false'")


        'Fetching To know if where are in the Same Day Or in The Next Day
        'Here We are in the next day So SQl Statment return Null
        If DR.HasRows = False Then
            DR.Close()
            gAdo.ClosingCon()
            'Here we See The Next Day in SQl Statment to Return Value
            DT = gAdo.FillDT("set dateformat dmy select Shift_ID,Shift_Start_Date,Shift_End_Date from tblShift " _
                          & "where convert (datetime, Shift_Start_Time) < '02/01/1900 " & NowTime _
                          & "' and convert (datetime,Shift_End_Time) >'02/01/1900 " & NowTime & "'and Shift_IsDeleted='false'")

            gShiftO.ID = DT.Rows(0).Item(0)
            Shift_Start_Date = DT.Rows(0).Item(1).ToString
            Shift_End_Date = DT.Rows(0).Item(2).ToString

        Else
            'Here We are in the Same day So SQl Statment return Value
            Do While DR.Read
                Shift_Start_Date = DR.GetValue(0)
                Shift_End_Date = DR.GetValue(1)
                gShiftO.ID = DR.GetValue(2)
            Loop
            DR.Close()
            gAdo.ClosingCon()
        End If


        If Shift_Start_Date = False And Shift_End_Date = False Then
            'the Same day(نفس اليوم)
            'MessageBox.Show(Now)
            Dim today As String
            today = Now
            If today.Contains("ص") Then
                today = today.Replace("ص", "AM")
            ElseIf today.Contains("م") Then
                today = today.Replace("م", "PM")
            End If
            Return today
        ElseIf Shift_Start_Date = True And Shift_End_Date = True Then
            'the Next day(اليوم - 1)
            ' MessageBox.Show(Now.AddDays(-1))
            Dim Lastday As String
            Lastday = Now.AddDays(-1)
            If Lastday.Contains("ص") Then
                Lastday = Lastday.Replace("ص", "AM")
            ElseIf Lastday.Contains("م") Then
                Lastday = Lastday.Replace("م", "PM")
            End If
            Return Today
        ElseIf Shift_Start_Date = False And Shift_End_Date = True Then
            Dim NightOrDay As String = Mid(Now, 21, 2)
            If NightOrDay = "AM" Then
                'and from 12:00:00 Am to Shift_End_Date  The day Before
                Dim Lastday As String
                Lastday = Now.AddDays(-1)
                If Lastday.Contains("ص") Then
                    Lastday = Lastday.Replace("ص", "AM")
                ElseIf Lastday.Contains("م") Then
                    Lastday = Lastday.Replace("م", "PM")
                End If
                Return Lastday
            Else
                'from Shift_Start_Date to 11:59:59 Pm the same day
                Dim today As String
                today = Now
                If today.Contains("ص") Then
                    today = today.Replace("ص", "AM")
                ElseIf today.Contains("م") Then
                    today = today.Replace("م", "PM")
                End If
                Return today
            End If

        End If
    End Function

#End Region

#Region "CboSlip"

    Private Sub CboSlip_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboSlip.SelectedIndexChanged

        If CboSlip.Items.Count > 0 Then
            'In Case Integer(  Slip_Rate,  Slip_Max_Rate ,Slip_Min_Rate  )
            If CboSlip.SelectedValue = 0 And CboSlip.Items.Count > 0 Then
                'If CboSlip Item is "رجوع" then we Fill CboSlip with Slip_Name
                If CboSlip.SelectedIndex = 3 Then
                    'In Slip To Fill CboSlip
                    gAdo.CtrlItemsLoad("Slip_ID", "Slip_Name", "tblSlip", CboSlip, "", "Slip_IsDeleted='false' ORDER BY Slip_Name")
                Else

                End If

                'In Case String( Slip_Name )
            ElseIf CboSlip.SelectedValue > 0 And CboSlip.Items.Count > 0 Then

                Dim Max As Decimal = gAdo.CmdExecScalar("select Slip_MaxRange from TblSlip where Slip_ID=" & CboSlip.SelectedValue)
                Dim Min As Decimal = gAdo.CmdExecScalar("select Slip_MinRange from TblSlip where Slip_ID=" & CboSlip.SelectedValue)
                Dim Rate As Decimal = gAdo.CmdExecScalar("select Slip_Rate from TblSlip where Slip_ID=" & CboSlip.SelectedValue)

                gAdo.GetSlipRates(CboSlip, "select Slip_MaxRange,Slip_Rate,Slip_MinRange from TblSlip where Slip_ID=" & CboSlip.SelectedValue, ToolTip1)

            End If
        End If
    End Sub

#End Region

#Region "Validate CboDriver_ID & CboTruck_No"
    Sub Validate_Board_NO(ByVal ParamArray Ctrls() As Control)

        For i = 0 To UBound(Ctrls)
            Select Case Ctrls(i).Name

                Case cboDriver.Name
                    If cboDriver.Text <> "" Then

                        If cboDriver.FindStringExact(cboDriver.Text) >= 0 Then
                            'Exist Car
                            'Get Driver_ID from tblDriver
                            Dim Driver_ID As Integer = gAdo.CmdExecScalar("select Driver_ID from tblDriver where Driver_Name='" & cboDriver.Text.Trim & "'")
                            'Get Out_Date From Tbltransactions
                            Dim Out_Date = "Select Out_Date From tblTransactions Where Trans_ID=(" & _
                                               "Select max(Trans_ID) From tblTransactions Where Driver_ID=" & Driver_ID & ")"

                            'Get CarBoard_No from tblCar 
                            Dim CarBoard_No = gAdo.CmdExecScalar("Select CarBoard_No From tblcar Where Car_ID=(" _
                                                             & " Select Car_ID From tblTransactions Where Trans_ID=(" _
                                                        & "Select max(Trans_ID) From tblTransactions Where Driver_ID=" & Driver_ID & "))")


                            If IsDBNull(gAdo.CmdExecScalar(Out_Date)) = True And CarBoard_No <> txtCarNo.Text Then
                                'The Driver_ID is Used With another Car and didn't have the second weight

                                LblDriverValidate.Text = "هذا السائق غير متاح"
                                LblDriverValidate.ForeColor = Color.Red
                                ErrorProvider1.SetError(cboDriver, "هذا السائق غير متاح")

                            ElseIf IsDBNull(gAdo.CmdExecScalar(Out_Date)) = True And CarBoard_No = txtCarNo.Text Then
                                'The Driver_ID is Used With this Car and didn't have the second weight

                                LblDriverValidate.Text = ""
                                LblDriverValidate.ForeColor = Color.Lime
                                ErrorProvider1.SetError(cboDriver, "")
                            Else
                                'The Driver_ID is Exit and  have the second weight

                                LblDriverValidate.Text = "هذا السائق متاح "
                                LblDriverValidate.ForeColor = Color.Lime
                                ErrorProvider1.SetError(cboDriver, "")
                            End If
                        Else
                            ' New Driver_ID
                            LblDriverValidate.Text = "هذا السائق جديد"
                            LblDriverValidate.ForeColor = Color.Lime
                            ErrorProvider1.SetError(cboDriver, "")
                        End If
                    Else
                        'CbotruckBoard_No.text is Empty
                        LblDriverValidate.Text = ""
                        ErrorProvider1.SetError(cboDriver, "")
                    End If


                Case cboTruckNo.Name

                    If cboTruckNo.Text <> "" Then

                        If cboTruckNo.FindStringExact(cboTruckNo.Text) >= 0 Then
                            'Exist Car
                            'Get Car_Info_ID from tblCarInfo
                            Dim Car_Info_ID As Integer = gAdo.CmdExecScalar("select Car_Info_ID from tblCarInfo where TruckBoard_No='" & cboTruckNo.Text.Trim & "' AND TruckBoard_City_ID = '" & cboTruckCity.SelectedValue & "' ")
                            'Get Out_Date From Tbltransactions
                            Dim Out_Date = "Select Out_Date From tblTransactions Where Trans_ID=(" & _
                                               "Select max(Trans_ID) From tblTransactions Where Car_Info_ID=" & Car_Info_ID & ")"

                            'Get CarBoard_No from tblCar 
                            Dim CarBoard_No = gAdo.CmdExecScalar("Select CarBoard_No From tblcar Where Car_ID=(" _
                                                             & " Select Car_ID From tblTransactions Where Trans_ID=(" _
                                                        & "Select max(Trans_ID) From tblTransactions Where Car_Info_ID=" & Car_Info_ID & "))")
                            '''''''''''''max

                            If IsDBNull(gAdo.CmdExecScalar(Out_Date)) = True And CarBoard_No <> txtCarNo.Text Then
                                'The truckBoard_No is Used With another Car and didn't have the second weight

                                LblTruckNoValidate.Text = "هذه المقطوره مستخدمه مع سياره أخرى"
                                LblTruckNoValidate.ForeColor = Color.Red
                                ErrorProvider1.SetError(cboTruckNo, "هذه المقطوره مستخدمه مع سياره أخرى")

                            ElseIf IsDBNull(gAdo.CmdExecScalar(Out_Date)) = True And CarBoard_No = txtCarNo.Text Then
                                'The truckBoard_No is Used With this Car and didn't have the second weight

                                LblTruckNoValidate.Text = ""
                                LblTruckNoValidate.ForeColor = Color.Lime
                                ErrorProvider1.SetError(cboTruckNo, "")
                            Else
                                'The truckBoard_No is Exit and  have the second weight

                                LblTruckNoValidate.Text = "هذه المقطوره يمكن أستخدامها "
                                LblTruckNoValidate.ForeColor = Color.Lime
                                ErrorProvider1.SetError(cboTruckNo, "")
                            End If
                        Else
                            ' New truckBoard_No
                            LblTruckNoValidate.Text = "رقم المقطوره جديد"
                            LblTruckNoValidate.ForeColor = Color.Lime
                            ErrorProvider1.SetError(cboTruckNo, "")
                        End If
                    Else
                        'CbotruckBoard_No.text is Empty
                        LblTruckNoValidate.Text = ""
                        ErrorProvider1.SetError(cboTruckNo, "")
                    End If
            End Select
        Next



    End Sub
#End Region

#Region "View data in Datagridview"

    Public Sub TodayTrans(ByVal WhereCondition As String)
        REM MYWORK
        Ds.Clear()
        Dim str As String
        Dim str2 As String
        'str2 = " set dateformat dmy SELECT CID as [رقم البطاقة], " & _
        '    " cars.CarBoard_No as [رقم السيارة],driver.Driver_Name as [إسم السائق] , " & _
        '    " Dealer.Dealer_Name as العميل,substring(convert(varchar(50),trans.[In_Date],111),1,10) as [تاريخ الدخول] ," & _
        '    " substring(convert(varchar(50),trans.[In_Date]),12,8) as [وقت الدخول], " & _
        '    " substring(convert(varchar(50),trans.[Out_Date],111),1,10) as [تاريخ الخروج ] , " & _
        '    " substring(convert(varchar(50),trans.[Out_Date]),12,8) as [وقت الخروج],abs(trans.Second_Weight - trans.First_Weigth)as net ," & _
        '    " trans.First_Weigth  as [ الوزنه الاولى ],trans.Second_Weight as [ الوزنه الثانيه],Issue.Issue_To_Name   as  [المرسل اليه  ]" & _
        '    " FROM tblTransactions  as trans INNER JOIN  tblCar as cars ON trans.Car_ID = cars.Car_ID " & _
        '    " INNER JOIN  tblDriver  as driver  ON trans.Driver_ID = driver.Driver_ID INNER JOIN tblDealer   as  Dealer  ON trans.Dealer_ID = Dealer.Dealer_ID  " & _
        '    " INNER JOIN tblCarInfo as CarInfo ON CarInfo.Car_Info_ID = trans.Car_Info_ID INNER JOIN tblIssueTo as Issue ON trans.Issue_To_ID = Issue.Issue_To_ID  " & _
        '    " INNER JOIN tblCity ON cars.CarBoard_City_ID = tblCity.City_ID  INNER JOIN dbo.tblCity AS tblCity_1" & _
        '    " ON CarInfo.TruckBoard_City_ID = tblCity_1.City_ID  " & WhereCondition
        str = " set dateformat dmy SELECT CID as [رقم البطاقة], " & _
                   " cars.CarBoard_No as [رقم السيارة],Good_Name as [الصنف],driver.Driver_Name as [إسم السائق] , " & _
                           " Dealer.Dealer_Name as العميل,Issue.Issue_To_Name   as  [المرسل اليه  ]," & _
                            " trans.First_Weigth  as [ الوزنه الاولى ] " & _
                          " FROM tblTransactions  as trans INNER JOIN  tblCar as cars ON trans.Car_ID = cars.Car_ID " & _
                           " INNER JOIN  tblDriver  as driver  ON trans.Driver_ID = driver.Driver_ID INNER JOIN tblDealer   as  Dealer  ON trans.Dealer_ID = Dealer.Dealer_ID  " & _
                           " INNER JOIN tblCarInfo as CarInfo ON CarInfo.Car_Info_ID = trans.Car_Info_ID INNER JOIN tblIssueTo as Issue ON trans.Issue_To_ID = Issue.Issue_To_ID  " & _
                           " INNER JOIN tblCity ON cars.CarBoard_City_ID = tblCity.City_ID " & _
                           " INNER JOIN tblGood as G ON G.Good_ID = trans.Good_ID  " & WhereCondition
        Ds = gAdo.SelectData(str, False)

        'DataGridView1.DataSource = Ds.Tables(0)
        DataGridView1.DataSource = Ds.Tables(0)
        Mywhere = ""
        'DataGridView1.Columns(0).Frozen = True
        'DataGridView1.Columns(8).HeaderText = "الصافى"
        'DataGridView1.Columns(1).Frozen = True
        'DataGridView1.Columns(2).Frozen = True
        'DataGridView1.Columns(3).Frozen = True
        SearchwithoutInput = 0
        'lblTransSum.Text = Ds.Tables(0).Compute(" sum (net)", "").ToString & " KG"
        lblTransCount.Text = DataGridView1.RowCount
        Mywhere = ""
        REM END MYWORK

    End Sub


    Public Sub CarINside(ByVal WhereCondition As String)
        REM MYWORK
        Dim SQLStatement1, SQLStatement2, SQLStatement As String
        SQLStatement1 = " set dateformat dmy SELECT convert(nvarchar(max),CID) as cid,D.Dealer_Name,G.Good_Name,IT.Issue_To_Name,C.CarBoard_No,CI.TruckBoard_No,T.Dealer_ID,T.Issue_To_ID " _
                                     & ",T.First_Weigth,T.Second_Weight,Out_Date " _
                                     & "FROM tblTransactions as T join tblGood as G on T.Good_ID =G.Good_ID  " _
                                     & "join tblCarInfo as CI on CI.Car_Info_ID=T.Car_Info_ID  " _
                                     & "join tblCar as C on C.Car_ID =T.Car_ID " _
                                     & "join tblDealer as D on D.Dealer_ID=T.Dealer_ID " _
                                     & "join tblIssueTo as IT on IT.Issue_To_ID=T.Issue_To_ID where  TR_TY =2 and out_date is null and  T.Issue_To_id not in(17,21)  "




        Ds = gAdo.SelectData(" set dateformat dmy SELECT CID as [رقم البطاقة], " & _
            " cars.CarBoard_No as [رقم السيارة],driver.Driver_Name as [إسم السائق] , " & _
            " Dealer.Dealer_Name as العميل,substring(convert(varchar(50),trans.[In_Date],111),1,10) as [تاريخ الدخول] ," & _
            " substring(convert(varchar(50),trans.[In_Date]),12,8) as [وقت الدخول], " & _
            " substring(convert(varchar(50),trans.[Out_Date],111),1,10) as [تاريخ الخروج ] , " & _
            " substring(convert(varchar(50),trans.[Out_Date]),12,8) as [وقت الخروج],abs(trans.Second_Weight - trans.First_Weigth)as net ," & _
            " trans.First_Weigth  as [ الوزنه الاولى ],trans.Second_Weight as [ الوزنه الثانيه],Issue.Issue_To_Name   as  [المرسل اليه  ]" & _
            " FROM tblTransactions  as trans INNER JOIN  tblCar as cars ON trans.Car_ID = cars.Car_ID " & _
            " INNER JOIN  tblDriver  as driver  ON trans.Driver_ID = driver.Driver_ID INNER JOIN tblDealer   as  Dealer  ON trans.Dealer_ID = Dealer.Dealer_ID  " & _
            " INNER JOIN tblCarInfo as CarInfo ON CarInfo.Car_Info_ID = trans.Car_Info_ID INNER JOIN tblIssueTo as Issue ON trans.Issue_To_ID = Issue.Issue_To_ID  " & _
            " INNER JOIN tblCity ON cars.CarBoard_City_ID = tblCity.City_ID  INNER JOIN dbo.tblCity AS tblCity_1" & _
            " ON CarInfo.TruckBoard_City_ID = tblCity_1.City_ID  " & WhereCondition, False)

        DataGridView1.DataSource = Ds.Tables(0)
        Mywhere = ""
        DataGridView1.Columns(0).Frozen = True
        DataGridView1.Columns(8).HeaderText = "الصافى"
        DataGridView1.Columns(1).Frozen = True
        DataGridView1.Columns(2).Frozen = True
        DataGridView1.Columns(3).Frozen = True
        SearchwithoutInput = 0
        lblTransSum.Text = Ds.Tables(0).Compute(" sum (net)", "").ToString & " KG"
        lblTransCount.Text = DataGridView1.RowCount
        Mywhere = ""
        REM END MYWORK

    End Sub

    Private Sub btnSwitchView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSwitchView.Click, btnCloseTransactions.Click
        'Call SwitchView()
        rdbCarsInside.Checked = False
        Select Case sender.Name
            Case Is = btnSwitchView.Name

                Dim SelectedScales As String = ""

                For Each ItemChecked In chklstBoxScale.CheckedItems
                    If SelectedScales = "" Then
                        SelectedScales = ItemChecked.item("Scale_ID").ToString
                    Else
                        SelectedScales = SelectedScales & " , " & ItemChecked.item("Scale_ID").ToString
                    End If
                Next

                If sender.text.trim = "حركات اليوم" Then
                    'TodayTrans("where (TR_TY=2 and First_Weigth_Scale_ID=" & ScaleNo & " and CONVERT(datetime, trans.[In_Date]) BETWEEN  '" & gMethods.GetShifName_And_SearchDate(True)(0) & "' and " & _
                    '                  "'" & gMethods.GetShifName_And_SearchDate(True)(1) & "')")
                    TodayTrans("where (TR_TY=2 and CONVERT(datetime, trans.[In_Date]) BETWEEN  '" & gMethods.GetShifName_And_SearchDate(True)(0) & "' and " & _
                                      "'" & gMethods.GetShifName_And_SearchDate(True)(1) & "') and First_Weigth_Scale_ID in(" & SelectedScales & ")")
                    sender.text = "جديـــــــد"
                    DTSearchTrans.Value = Now
                    pnlNewTransaction.SendToBack()
                    gIsSearch = True
                Else
                    sender.text = "حركات اليوم"
                    pnlNewTransaction.BringToFront()
                End If
                'Case Is = btnSwitchView.Name
                '    sender.text = "حركات اليوم"
                '    pnlNewTransaction.BringToFront()

            Case Is = btnCloseTransactions.Name
                btnSwitchView.Text = "حركات اليوم"
                SwitchMode(txtCarNo, cboCarCity, cboTruckNo, cboTruckCity, cboDriver, txtLicenceNo, _
                  cboDealer, cboIssueTo, cboGoodType)
                pnlTransactions.SendToBack()
        End Select
    End Sub

    Private Sub BtnSearchTrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchTrans.Click
        Dim SelectedScales As String = ""

        For Each ItemChecked In chklstBoxScale.CheckedItems
            If SelectedScales = "" Then
                SelectedScales = ItemChecked.item("Scale_ID").ToString
            Else
                SelectedScales = SelectedScales & " , " & ItemChecked.item("Scale_ID").ToString
            End If
        Next
        'to fill DataGridView1
        TodayTrans("where TR_TY=2 and trans.In_Shift_Date  Between '" & DTSearchTrans.Value.Date & " 00:00:00 AM' and '" _
                            & DTSearchTrans.Value.Date & " 11:59:59.998 PM' and First_Weigth_Scale_ID in (" & SelectedScales & ")")
    End Sub

    Private Sub ChkSearch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkSearch.CheckedChanged
        If ChkSearch.Checked = True Then
            GrpSearchTrans.Enabled = True
        Else
            GrpSearchTrans.Enabled = False
        End If
    End Sub
#End Region

    Function PrintTransaction() As Boolean
        Try

            Dim ConnPath, ConnString As String
            ConnPath = Application.StartupPath & "\ConnString.txt"
            ConnString = GetConString(ConnPath)
            Dim Con As New SqlConnection(ConnString)
            gAdo.OpeningCon()
            Dim Adp As New SqlDataAdapter(" select Company_Name,Company_Address,Company_Telephone," _
                                          & " Picture from tblcompany join dbo.tblPicture on Company_Pic_ID=Pic_ID", Con)

            Dim DT As New DataTable
            Adp.Fill(DT)

            Dim rp As New RptTransactionPrint
            Dim crv As New CrystalDecisions.Windows.Forms.CrystalReportViewer
            'rp.Refresh()
            'rp.SetDataSource(DT)
            'rp.PrintOptions.PrinterName = "\\BB\HP Deskjet F300 series"

            Dim NetWeight As String

            If Val(lblScndWeight.Text) < 1 Then
                NetWeight = 0
            Else
                NetWeight = Abs(CDec(Val(lblFrstWeight.Text.Trim)) - CDec(Val(lblScndWeight.Text.Trim)))
            End If

            If AutoPrint = False Then
                rp.SetParameterValue("Car_No", PCarNo)
                rp.SetParameterValue("Car_City", PCarCity)
                rp.SetParameterValue("Truck_No", PTruckNo)
                rp.SetParameterValue("Truck_City", PTruckCity)
                rp.SetParameterValue("Driver_Name", PDriverName)
                rp.SetParameterValue("Driver_Licence", PDriverLicence)
                rp.SetParameterValue("Dealer_Name", PDealerName)
                rp.SetParameterValue("IssueTO_Name", PIssueto)
                rp.SetParameterValue("First_Weight", PFisrtScale)
                rp.SetParameterValue("Good_Name", PGoodName)
                rp.SetParameterValue("Second_Weight", PSecondScale)
                rp.SetParameterValue("Net_weight", PNetScale)
                rp.SetParameterValue("Trans_ID", PTransID)
                rp.SetParameterValue("Arabic", PArabic)
            Else
                rp.SetParameterValue("Car_No", txtCarNo.Text)
                rp.SetParameterValue("Car_City", cboCarCity.Text)
                rp.SetParameterValue("Truck_No", cboTruckNo.Text)
                rp.SetParameterValue("Truck_City", cboTruckCity.Text)
                rp.SetParameterValue("Driver_Name", cboDriver.Text)
                rp.SetParameterValue("Driver_Licence", txtLicenceNo.Text)
                rp.SetParameterValue("Dealer_Name", cboDealer.Text)
                rp.SetParameterValue("IssueTO_Name", cboIssueTo.Text)
                rp.SetParameterValue("First_Weight", CDec(Val(lblFrstWeight.Text.Trim)))
                rp.SetParameterValue("Good_Name", cboGoodType.Text)
                rp.SetParameterValue("Second_Weight", CDec(Val(lblScndWeight.Text.Trim)))
                rp.SetParameterValue("Net_weight", NetWeight)
                rp.SetParameterValue("Trans_ID", txtTransID.Text)
                rp.SetParameterValue("Arabic", HANY(Val(lblNetWeight.Text.Trim), "EGYPT"))
            End If


            If radFrstEmpty.Checked = True Then
                rp.SetParameterValue("First_Weight_Type", "فارغ")
            ElseIf radFrstFull.Checked = True Then
                rp.SetParameterValue("First_Weight_Type", "معبأ")
            End If

            Dim NowDate As String
            Try
                If AutoPrint = False Then
                    NowDate = gAdo.CmdExecScalar("select Out_Date from tblTransactions where TR_TY=2 and CID=" & PTransID)
                Else
                    NowDate = gAdo.CmdExecScalar("select Out_Date from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
                End If
            Catch ex As Exception
                NowDate = ""
            End Try
            If NowDate = "" Then
                rp.SetParameterValue("End_Time", "")
                rp.SetParameterValue("Out_Date", "")
            Else
                If NowDate.EndsWith("PM") Then
                    Dim End_Time As String = NowDate.Replace("PM", "م")
                    rp.SetParameterValue("End_Time", Mid(End_Time, 12, 10))
                    Dim Out_Date As String = Mid(NowDate, 7, 4) & "/" & Mid(NowDate, 4, 2) & "/" & Mid(NowDate, 1, 2)
                    rp.SetParameterValue("Out_Date", Out_Date)
                Else
                    Dim End_Time As String = NowDate.Replace("AM", "ص")
                    rp.SetParameterValue("End_Time", Mid(End_Time, 12, 10))
                    Dim Out_Date As String = Mid(NowDate, 7, 4) & "/" & Mid(NowDate, 4, 2) & "/" & Mid(NowDate, 1, 2)
                    rp.SetParameterValue("Out_Date", Out_Date)
                End If
            End If

            If lblInDate.Text.EndsWith("PM") Then
                Dim In_Time As String = lblInDate.Text.Replace("PM", "م")
                In_Time = In_Time.Substring(11)
                rp.SetParameterValue("TimeIn", In_Time.Trim)
            Else
                Dim In_Time As String = lblInDate.Text.Replace("AM", "ص")
                In_Time = In_Time.Substring(11)
                rp.SetParameterValue("TimeIn", In_Time.Trim)
            End If

            Dim In_Date As String = Mid(lblInDate.Text, 7, 4) & "/" & Mid(lblInDate.Text, 4, 2) & "/" & Mid(lblInDate.Text, 1, 2)
            rp.SetParameterValue("In_Date", In_Date)
            If chkPrintCompayName.Checked = True Then
                rp.SetParameterValue("CompanyName", "")
                rp.SetParameterValue("CompanyAdress", "")
                rp.SetParameterValue("CompanyTel", "")
                rp.Section2.ReportObjects("Picture1").ObjectFormat.EnableSuppress = True
            Else
                rp.SetParameterValue("CompanyName", gAdo.CmdExecScalar("select Company_Name from tblcompany"))
                rp.SetParameterValue("CompanyAdress", gAdo.CmdExecScalar("select Company_Address from tblcompany"))
                rp.SetParameterValue("CompanyTel", gAdo.CmdExecScalar("select Company_Telephone from tblcompany"))
                rp.Section2.ReportObjects("Picture1").ObjectFormat.EnableSuppress = False
            End If


            'Print Report

            grptpth.ReportPath(rp, crv)
            rp.PrintToPrinter(1, False, 1, crv.ViewCount)

            Return True
        Catch ex As Exception

        End Try

    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        NOPrinted = Val(gAdo.CmdExecScalar("select value from tblvalue where serial=1"))
        Try

            Dim msgResult As MsgBoxResult
            msgResult = MsgBox("هـل ترغـب فى الطبــــاعه ؟", MsgBoxStyle.OkCancel, "الطبــــاعه")
            If msgResult = MsgBoxResult.Ok Then
                If txtTransID.Text = "" Then
                    MsgBox("لا توجد حركه لطباعتها")
                    Exit Sub
                End If
                AutoPrint = True
                Dim Thread As New System.Threading.Thread(AddressOf PrintNewReport)
                Control.CheckForIllegalCrossThreadCalls = False
                Thread.Start()
            Else

            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub TmrFirstWeight_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TmrFirstWeight.Tick
        ' here calculate the first weight


        Try

            Dim InPut As String
            InPut = serl.ReadExisting
            InPut = InPut.Substring(InPut.LastIndexOf(" ") + 1)
            InPut = InPut.Substring(0, 6)

            ' If gAdo.CmdExecScalar("select First_Weigth from tblTransactions where First_Weigth_Scale_ID=" & ScaleNo & "  and trans_id =" & txtTransID.Text) > 0 Then
            If gAdo.CmdExecScalar("select First_Weigth from tblTransactions where trans_id =" & txtTransID.Text) > 0 Then
                lblShowWeight.Text = Val(InPut)
            Else
                lblFrstWeight.Text = Val(InPut)
                lblShowWeight.Text = Val(InPut)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub TmrSecondWeight_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TmrSecondWeight.Tick
        ' here calculate the second weight

        Try

            Dim InPut As String
            InPut = serl.ReadExisting
            InPut = InPut.Substring(InPut.LastIndexOf(" ") + 1)
            InPut = InPut.Substring(0, 6)
            'If gAdo.CmdExecScalar("select Second_Weight from tblTransactions where First_Weigth_Scale_ID=" & ScaleNo & "  and trans_id =" & txtTransID.Text) > 0 Then
            If gAdo.CmdExecScalar("select Second_Weight from tblTransactions where trans_id =" & txtTransID.Text) > 0 Then

                lblShowWeight.Text = Val(InPut)
            Else
                lblScndWeight.Text = Val(InPut)
                lblShowWeight.Text = Val(InPut)
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Public Function GetConString(ByVal FullPath As String, Optional ByRef ErrInfo As String = "") As String

        Dim strContents As String
        Dim objReader As StreamReader
        Try

            objReader = New StreamReader(FullPath)
            strContents = objReader.ReadToEnd()
            objReader.Close()
            Return strContents
        Catch Ex As Exception
            ErrInfo = Ex.Message
        End Try
    End Function

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked, LinkLabel3.LinkClicked, LinkLabel2.LinkClicked
        pnlHelp.Visible = False
    End Sub

    Private Sub cfrmMain_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.F6 Then
                pnlHelp.Visible = True
            ElseIf e.KeyCode = Keys.F8 Then
                'btnFirstScale_Click(sender, e)
            ElseIf e.KeyCode = Keys.F5 Then
                'btnSecondScale_Click(sender, e)
            ElseIf e.KeyCode = Keys.F12 Then
                btnSaveAndPrint_Click(sender, e)
            ElseIf e.KeyCode = Keys.F1 Then
                btnLastTrans_Click(sender, e)
            ElseIf e.KeyCode = Keys.F2 Then
                btnNextTrans_Click(sender, e)
            ElseIf e.KeyCode = Keys.F3 Then
                btnPreviousTrans_Click(sender, e)
            ElseIf e.KeyCode = Keys.F4 Then
                btnFirstTrans_Click(sender, e)
            ElseIf e.KeyCode = Keys.F7 Then
                btnSearch_Click(sender, e)
            ElseIf e.KeyCode = Keys.F11 Then
                Dim msgResult As MsgBoxResult
                msgResult = MsgBox("هـل ترغـب فى الطبــــاعه ؟", MsgBoxStyle.OkCancel, "الطبــــاعه")
                If msgResult = MsgBoxResult.Ok Then
                    If txtTransID.Text = "" Then
                        MsgBox("لا توجد حركه لطباعتها")
                        Exit Sub
                    End If
                    AutoPrint = True
                    Dim Thread As New System.Threading.Thread(AddressOf CopyofPrintNewReport)
                    Control.CheckForIllegalCrossThreadCalls = False
                    Thread.Start()
                Else
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'gfrmAllReports = New frmAllReports
        'gfrmChoose.Hide()
        'gfrmAllReports.ShowDialog()
        'Me.Dispose()

        Reports.ShowDialog()

        Select Case Reports.x

            Case Reports.Control_Name.btnAllReports
                frmAllReports.ShowDialog()
            Case Reports.Control_Name.btnCarsInside
                '''' Elsaman ''''
                'frmrptCarInsideSearch.ShowDialog()
                'frmrptCarsInside.ShowDialog()

                '''' Elsaman 24/4/2019 ''''
                If gTransID > 0 Then
                    LoadData()
                End If


            Case Reports.Control_Name.btnPreReports
                cfrmReports.ShowDialog()
            Case Reports.Control_Name.Button1
                frmrptAccounts.ShowDialog()
            Case Else
                ' Nothing Going Here

        End Select


    End Sub

    Private Sub btnSaveAndPrint_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
      btnSwitchView.MouseEnter _
    , btnAddPayment.MouseEnter, btnGotoTransaction.MouseEnter, _
    btnCloseTransactions.MouseEnter, BtnSearchTrans.MouseEnter, btnUpGetWieght.MouseEnter, btnEnterBean.MouseEnter, btnEnterMeal.MouseEnter, Button2.MouseEnter, Button1.MouseEnter, btnShow.MouseEnter, btnSecondScale.MouseEnter, btnSearch.MouseEnter, btnSaveAndPrint.MouseEnter, btnPreviousTrans.MouseEnter, btnNextTrans.MouseEnter, btnNew.MouseEnter, btnLastTrans.MouseEnter, btnHide.MouseEnter, btnFirstTrans.MouseEnter, btnFirstScale.MouseEnter, btnEditFirstScale.MouseEnter, btnEdit.MouseEnter

        If sender.name = "btnNextTrans" Then
            sender.BackColor = Color.Indigo
            'ttInfo.Show("الحركه التاليه", btnNextTrans)
        ElseIf sender.name = "btnPreviousTrans" Then
            sender.BackColor = Color.Indigo
            'ttInfo.Show("الحركه السابقه", btnPreviousTrans)
        ElseIf sender.name = "btnFirstTrans" Then
            sender.BackColor = Color.Indigo
            'ttInfo.Show("الحركه الاولى", btnFirstTrans)
        ElseIf sender.name = "btnLastTrans" Then
            sender.BackColor = Color.Indigo
            'ttInfo.Show("الحركه الاخـيره", btnLastTrans)
        Else
            sender.BackColor = Color.SkyBlue
            'ttInfo.Hide(sender)
        End If


    End Sub

    Private Sub btnLogOff_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogOff.MouseEnter
        sender.Backcolor = Color.LightGreen
    End Sub

    Private Sub btnSaveAndPrint_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
      btnSwitchView.MouseLeave _
    , btnAddPayment.MouseLeave, btnGotoTransaction.MouseLeave, _
    btnCloseTransactions.MouseLeave, BtnSearchTrans.MouseLeave, btnUpGetWieght.MouseLeave, btnEnterBean.MouseLeave, btnEnterMeal.MouseLeave, Button2.MouseLeave, Button1.MouseLeave, btnShow.MouseLeave, btnSecondScale.MouseLeave, btnSearch.MouseLeave, btnSaveAndPrint.MouseLeave, btnPreviousTrans.MouseLeave, btnNextTrans.MouseLeave, btnNew.MouseLeave, btnLastTrans.MouseLeave, btnHide.MouseLeave, btnFirstTrans.MouseLeave, btnFirstScale.MouseLeave, btnEditFirstScale.MouseLeave, btnEdit.MouseLeave

        If sender.name = "btnNextTrans" Or sender.name = "btnPreviousTrans" Or sender.name = "btnFirstTrans" Or sender.name = "btnLastTrans" Then
            sender.BackColor = Color.LightSkyBlue
        Else
            sender.BackColor = Color.AliceBlue
        End If



    End Sub


    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click

        Dim Transaction As Integer
        Transaction = gAdo.CmdExecScalar("select count (Trans_ID)from tblTransactions  where tr_ty=2 and out_date is not null and CID=" & txtTransID.Text)
        If Transaction = 0 Then
            MsgBox("لايوجد حركه بهذا الرقم")
        Else
            gfrmEdit = New frmEdit
            gfrmChoose.Hide()
            gfrmEdit.ShowDialog()
            Try
                Search(Val(txtTransID.Text.Trim))
            Catch ex As Exception

            End Try
        End If

        If gTransactionO.Slip_Rate = 0 Then
            btnSlip0.BackColor = Color.Yellow
            btnSlip1.BackColor = Color.DarkGray
            btnSlip2.BackColor = Color.DarkGray
            btnSlip3.BackColor = Color.DarkGray
        ElseIf gTransactionO.Slip_Rate = 5 Then
            btnSlip0.BackColor = Color.DarkGray
            btnSlip1.BackColor = Color.Yellow
            btnSlip2.BackColor = Color.DarkGray
            btnSlip3.BackColor = Color.DarkGray
        ElseIf gTransactionO.Slip_Rate = 10 Then
            btnSlip0.BackColor = Color.DarkGray
            btnSlip1.BackColor = Color.DarkGray
            btnSlip2.BackColor = Color.Yellow
            btnSlip3.BackColor = Color.DarkGray
        ElseIf gTransactionO.Slip_Rate = 15 Then
            btnSlip0.BackColor = Color.DarkGray
            btnSlip1.BackColor = Color.DarkGray
            btnSlip2.BackColor = Color.DarkGray
            btnSlip3.BackColor = Color.Yellow
        Else
            btnSlip0.BackColor = Color.DarkGray
            btnSlip1.BackColor = Color.DarkGray
            btnSlip2.BackColor = Color.DarkGray
            btnSlip3.BackColor = Color.DarkGray
        End If

    End Sub



    Private Sub cboCarCity_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles _
 _
       CboSlip.KeyDown, txtLicenceNo.KeyDown, cboTruckNo.KeyDown, cboTruckCity.KeyDown, CboRecieveType.KeyDown, cboIssueTo.KeyDown, cboGoodType.KeyDown, cboDriver.KeyDown, cboDealer.KeyDown, cboCarCity.KeyDown

        'If e.KeyCode = Keys.Enter Then
        '    If sender.name = "cboGoodType" Then
        '        btnSaveAndPrint.Focus()
        '    Else
        '        ProcessTabKey(True)
        '    End If

        'End If



        If e.KeyCode = Keys.Enter Then
            If sender.name = "cboGoodType" Then
                e.SuppressKeyPress = True
                btnSaveAndPrint.Focus()
            ElseIf sender.name = "cboDriver" Then
                gMethods.IsDriverExist(cboDriver, txtLicenceNo)
                e.SuppressKeyPress = True
                txtLicenceNo.Focus()
            ElseIf sender.name = "cboTruckNo" Then
                e.SuppressKeyPress = True
                cboTruckCity.Focus()
            ElseIf sender.name = "cboIssueTo" Then
                e.SuppressKeyPress = True
                CboRecieveType.Focus()
            ElseIf sender.name = "cboDealer" Then
                e.SuppressKeyPress = True
                cboIssueTo.Focus()
            ElseIf sender.name = "cboCarCity" Then
                e.SuppressKeyPress = True
                cboTruckNo.Focus()
            ElseIf sender.name = "cboTruckCity" Then
                e.SuppressKeyPress = True
                cboDriver.Focus()
            ElseIf sender.name = "txtLicenceNo" Then
                e.SuppressKeyPress = True
                cboDealer.Focus()
            ElseIf sender.name = CboRecieveType.Name Then
                e.SuppressKeyPress = True
                cboGoodType.Focus()
            End If
        End If



        If e.KeyCode = Keys.Home Then
            If sender.name = "cboGoodType" Then
                cboGoodType.SelectedIndex = 0
            ElseIf sender.name = "cboDriver" Then
                cboDriver.SelectedIndex = 1
            ElseIf sender.name = "cboTruckNo" Then
                cboTruckNo.SelectedIndex = 1
            ElseIf sender.name = "cboIssueTo" Then
                cboIssueTo.SelectedIndex = 1
            ElseIf sender.name = "cboDealer" Then
                cboDealer.SelectedIndex = 1
            ElseIf sender.name = "cboCarCity" Then
                cboCarCity.SelectedIndex = 1
            ElseIf sender.name = "cboTruckCity" Then
                cboTruckCity.SelectedIndex = 1
            End If
        ElseIf e.KeyCode = Keys.End Then
            If sender.name = "cboGoodType" Then
                cboGoodType.SelectedIndex = cboGoodType.Items.Count - 1
            ElseIf sender.name = "cboDriver" Then
                cboDriver.SelectedIndex = cboDriver.Items.Count - 1
            ElseIf sender.name = "cboTruckNo" Then
                cboTruckNo.SelectedIndex = cboTruckNo.Items.Count - 1
            ElseIf sender.name = "cboIssueTo" Then
                cboIssueTo.SelectedIndex = cboIssueTo.Items.Count - 1
            ElseIf sender.name = "cboDealer" Then
                cboDealer.SelectedIndex = cboDealer.Items.Count - 1
            ElseIf sender.name = "cboCarCity" Then
                cboCarCity.SelectedIndex = cboCarCity.Items.Count - 1
            ElseIf sender.name = "cboTruckCity" Then
                cboTruckCity.SelectedIndex = cboTruckCity.Items.Count - 1
            End If
        End If
        ' cboTruckNo.SelectedValue = gAdo.CmdExecScalar("SELECT Car_Info_ID from tblTransactions WHERE Trans_ID =( SELECT MAX(Trans_ID) FROM tblTransactions WHERE (Car_ID) IN (SELECT Car_ID FROM tblCar WHERE CarBoard_No ='" & txtCarNo.Text.Trim & "'))")

    End Sub

    'Public Sub Print()
    '    Try
    '        PrintTransaction()
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Dim intCounteading As Integer = 0
    Public Shared STX = Chr(2) ' ascii start of text  
    Public Shared CR = Chr(13) 'ascii Carriage Return
    Private Sub btnFirstScale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirstScale.Click
        Try

            If serl.IsOpen Then
                ' Don't do any thing
            Else

                With serl
                    .PortName = "COM1"
                    .BaudRate = 9600
                    .DataBits = 8
                    .StopBits = StopBits.One
                    .Parity = Parity.None

                    '' Elsaman
                    ''بعض الخصائص الجديده 
                    '.RtsEnable = True ' تسمح للجهاز المتصل بارسال البيانات فعليا 
                    '.ReadTimeout = 500

                    '.Handshake = Handshake.None
                End With
                serl.Open()
            End If
        Catch ex As Exception

        End Try
     
        If intCounteading < 2 Then
            intCounteading += 1


            Dim dd As Integer


            Try
                If ScaleNo = 2 Then
                    RichTextBox1.Text = ""
                    RichTextBox1.Text = serl.ReadExisting
                    If RichTextBox1.Text.LastIndexOf("A") - 14 > 0 Then
                        lblFrstWeight.Text = Val(RichTextBox1.Text.Substring(RichTextBox1.Text.LastIndexOf("A") - 14, 6))
                        lblShowWeight.Text = Val(RichTextBox1.Text.Substring(RichTextBox1.Text.LastIndexOf("A") - 14, 6))
                    Else

                        dd = RichTextBox1.Text.LastIndexOf("A")
                        lblFrstWeight.Text = Val(RichTextBox1.Text.Substring(RichTextBox1.Text.LastIndexOf("A") + 3, 6))
                        lblShowWeight.Text = Val(RichTextBox1.Text.Substring(RichTextBox1.Text.LastIndexOf("A") + 3, 6))
                    End If
                Else
                   
                    Dim str As String
                    RichTextBox1.Text = ""
                    str = serl.ReadExisting
                    dd = str.LastIndexOf(")")

                    lblFrstWeight.Text = Val(str.Substring(str.LastIndexOf(")") - 16, 11))
                    lblShowWeight.Text = Val(str.Substring(str.LastIndexOf(")") - 16, 11))
                    If lblFrstWeight.Text = 0 Then
                        lblFrstWeight.Text = Val(str.Substring(str.LastIndexOf(")") - 16, 11))
                        lblShowWeight.Text = Val(str.Substring(str.LastIndexOf(")") - 16, 11))
                    End If

                
                End If




            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try

            'Try

            '    If serl.IsOpen Then
            '        serl.Close()
            '    Else
            '        ' Don't do any thing()
            '    End If

            'Catch ex As Exception

            'End Try
            If serl.IsOpen Then
                serl.Close()

            Else
                ' Don't do any thing
            End If
            btnFirstScale_Click(btnFirstScale, e)
        Else
            intCounteading = 0
        End If
    End Sub

    Private Sub btnSecondScale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSecondScale.Click
        
        Try

            If serl.IsOpen Then
                ' Don't do any thing
            Else

                With serl
                    .PortName = "COM1"
                    .BaudRate = 9600
                    .DataBits = 8
                    .StopBits = StopBits.One
                    .Parity = Parity.None
                    '.Handshake = Handshake.None
                End With
                serl.Open()
            End If
        Catch ex As Exception

        End Try


        If intCounteading < 2 Then
            intCounteading += 1


            Try

                If ScaleNo = 2 Then
                    RichTextBox1.Text = ""
                    RichTextBox1.Text = serl.ReadExisting
                    If RichTextBox1.Text.LastIndexOf("A") - 14 > 0 Then
                        lblScndWeight.Text = Val(RichTextBox1.Text.Substring(RichTextBox1.Text.LastIndexOf("A") - 14, 6))
                        lblShowWeight.Text = Val(RichTextBox1.Text.Substring(RichTextBox1.Text.LastIndexOf("A") - 14, 6))
                    Else


                        lblScndWeight.Text = Val(RichTextBox1.Text.Substring(RichTextBox1.Text.LastIndexOf("A") + 3, 6))
                        lblShowWeight.Text = Val(RichTextBox1.Text.Substring(RichTextBox1.Text.LastIndexOf("A") + 3, 6))
                    End If

                Else
                    Dim str As String
                    RichTextBox1.Text = ""
                    str = serl.ReadExisting

                    Dim dd As Integer
                    dd = str.LastIndexOf(")")
                    lblScndWeight.Text = Val(str.Substring(str.LastIndexOf(")") - 16, 11))
                    lblShowWeight.Text = Val(str.Substring(str.LastIndexOf(")") - 16, 11))
                    If lblScndWeight.Text = 0 Then
                        lblScndWeight.Text = Val(str.Substring(str.LastIndexOf(")") - 16, 11))
                        lblShowWeight.Text = Val(str.Substring(str.LastIndexOf(")") - 16, 11))
                    End If
                    'lblScndWeight.Text = Val(RichTextBox1.Text.Substring(RichTextBox1.Text.IndexOf("00)1") + 6, 7))
                    'lblShowWeight.Text = Val(RichTextBox1.Text.Substring(RichTextBox1.Text.IndexOf("00)1") + 6, 7))
                    'End If
                    'E-lse
                    '    RichTextBox1.Text = ""
                    '    RichTextBox1.Text = serl.ReadExisting
                    '    If RichTextBox1.Text.LastIndexOf("i") - 14 > 0 Then
                    '        lblScndWeight.Text = Val(RichTextBox1.Text.Substring(RichTextBox1.Text.LastIndexOf("i") - 14, 6))
                    '        lblShowWeight.Text = Val(RichTextBox1.Text.Substring(RichTextBox1.Text.LastIndexOf("i") - 14, 6))
                    '    Else
                    '        lblScndWeight.Text = Val(RichTextBox1.Text.Substring(RichTextBox1.Text.LastIndexOf("i") + 3, 6))
                    '        lblShowWeight.Text = Val(RichTextBox1.Text.Substring(RichTextBox1.Text.LastIndexOf("i") + 3, 6))
                    '    End If
                End If

            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try

            If serl.IsOpen Then
                serl.Close()

            Else
                ' Don't do any thing
            End If
            btnSecondScale_Click(btnSecondScale, e)
        Else
            intCounteading = 0
        End If



    End Sub

    Private Sub rdbSecondScale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSecondScale.CheckedChanged
        Try
            TodayTrans("where First_Weigth_Scale_ID=3 and CONVERT(datetime, trans.[In_Date]) BETWEEN  '" & gMethods.GetShifName_And_SearchDate(True)(0) & "' and " & _
                  "'" & gMethods.GetShifName_And_SearchDate(True)(1) & "' or( trans.Out_Date)is null")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub rdbAllScale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbAllScale.CheckedChanged
        Try
            TodayTrans("where CONVERT(datetime, trans.[In_Date]) BETWEEN  '" & gMethods.GetShifName_And_SearchDate(True)(0) & "' and " & _
                  "'" & gMethods.GetShifName_And_SearchDate(True)(1) & "' or( trans.Out_Date)is null")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub rdbCurrentScale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCurrentScale.CheckedChanged
        Try
            'TodayTrans("where First_Weigth_Scale_ID=" & ScaleNo & " and CONVERT(datetime, trans.[In_Date]) BETWEEN  '" & gMethods.GetShifName_And_SearchDate(True)(0) & "' and " & _
            '      "'" & gMethods.GetShifName_And_SearchDate(True)(1) & "' or( trans.Out_Date)is null")
            TodayTrans("where CONVERT(datetime, trans.[In_Date]) BETWEEN  '" & gMethods.GetShifName_And_SearchDate(True)(0) & "' and " & _
                  "'" & gMethods.GetShifName_And_SearchDate(True)(1) & "' or( trans.Out_Date)is null")
        Catch ex As Exception
        End Try
    End Sub



    Private Sub btnEnterBean_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnterBean.Click
        Try
            My.Computer.Audio.Play(My.Resources.EnterBean, AudioPlayMode.Background)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnEnterMeal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnterMeal.Click
        Try
            My.Computer.Audio.Play(My.Resources.EnterMeal, AudioPlayMode.Background)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkPrintCompayName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPrintCompayName.CheckedChanged
        If chkPrintCompayName.Checked Then
            chkPrintCompayName.ForeColor = Color.Red
            chkPrintCompayName.BackColor = Color.White
            pnlCompanyName.BackColor = Color.White
        Else
            chkPrintCompayName.ForeColor = Color.Black
            chkPrintCompayName.BackColor = Color.Gainsboro
            pnlCompanyName.BackColor = Color.Gainsboro
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            InsertIntoServerDB()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnEditFirstScale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditFirstScale.Click
        gfrmEditFirstScale = New frmEditFirstScale
        'gfrmChoose.Hide()
        gfrmEditFirstScale.ShowDialog()
    End Sub

    Private Sub btnUpGetWieght_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpGetWieght.Click
        Try
            My.Computer.Audio.Play(My.Resources.GetUp, AudioPlayMode.Background)
        Catch ex As Exception

        End Try
    End Sub

    Dim DSSrch As New DataSet
    Dim DTSrch As DataTable

    Private Sub btnFirstTrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirstTrans.Click
        Try
            SaveOrCancel = 1

            PrsentScale()

            NoChangeCheck = True
            'Eng Ahmad fawzy
            DSSrch = gAdo.SelectData(" set dateformat dmy SELECT trans.Slip_Rate,CustomWeight,isnull(trans.First_Weight_User_ID,'') as FirstUser,isnull(trans.Second_Weight_User_ID,'') as SecondUser,MSGID,trans.first_weight_user_id,trans.CID ,Good_ID, CarInfo.Car_Info_ID, " & _
                             " cars.CarBoard_No,trans.Driver_ID,driver.Driver_License_No,tblCity.City_ID, " & _
                             " trans.Dealer_ID ,substring(convert(varchar(50),trans.[In_Date],111),1,10) as [تاريخ الدخول] ," & _
                             " substring(convert(varchar(50),trans.[In_Date]),12,11) as [وقت الدخول], " & _
                             " substring(convert(varchar(50),trans.[Out_Date],111),1,10) as [تاريخ الخروج] , " & _
                             " substring(convert(varchar(50),trans.[Out_Date]),12,11) as [وقت الخروج],abs(trans.Second_Weight - trans.First_Weigth)as net ," & _
                             " trans.First_Weigth ,trans.Second_Weight,trans.Issue_To_ID,Issue.Issue_To_Name   as  [المرسل اليه  ],Contractor_ID,Notes,trans.LocationID" & _
                             " FROM tblTransactions  as trans INNER JOIN  tblCar as cars ON trans.Car_ID = cars.Car_ID  " & _
                             " INNER JOIN  tblDriver  as driver  ON trans.Driver_ID = driver.Driver_ID INNER JOIN tblDealer   as  Dealer  ON trans.Dealer_ID = Dealer.Dealer_ID  " & _
                             "INNER JOIN tblCarInfo as CarInfo ON CarInfo.Car_Info_ID = trans.Car_Info_ID INNER JOIN tblIssueTo as Issue ON trans.Issue_To_ID = Issue.Issue_To_ID  " & _
                             " INNER JOIN tblCity ON cars.CarBoard_City_ID = tblCity.City_ID  INNER JOIN dbo.tblCity AS tblCity_1  " & _
                             "  ON CarInfo.TruckBoard_City_ID = tblCity_1.City_ID  where trans.CID=(select min(CID) from tblTransactions where  TR_TY=2 " & Scale & " ) and TR_TY=2" & Scale, False)
            DTSrch = DSSrch.Tables(0)
            FrmRefresh()
            txtTransID.Text = DTSrch.Rows(0).Item("CID").ToString
            cboGoodType.SelectedValue = DTSrch.Rows(0).Item("Good_ID").ToString
            txtCarNo.Text = DTSrch.Rows(0).Item("CarBoard_No").ToString
            cboDriver.SelectedValue = DTSrch.Rows(0).Item("Driver_ID").ToString
            cboDealer.SelectedValue = DTSrch.Rows(0).Item("Dealer_ID").ToString
            cboIssueTo.SelectedValue = DTSrch.Rows(0).Item("Issue_To_ID").ToString
            txtLicenceNo.Text = DTSrch.Rows(0).Item("Driver_License_No").ToString
            cboCarCity.SelectedValue = DTSrch.Rows(0).Item("City_ID").ToString
            lblFrstWeight.Text = DTSrch.Rows(0).Item("First_Weigth").ToString
            cboTruckNo.SelectedValue = DTSrch.Rows(0).Item("Car_Info_ID").ToString
            lblInDate.Text = DTSrch.Rows(0).Item("تاريخ الدخول").ToString & " " & DTSrch.Rows(0).Item("وقت الدخول").ToString
            cboContractor.SelectedValue = DTSrch(0).Item("Contractor_ID").ToString


            If DTSrch.Rows(0).Item("Notes").ToString = "NULL" Then
                txtNotes.Text = ""
            Else
                txtNotes.Text = DTSrch.Rows(0).Item("Notes").ToString
            End If
            Dim ss As String
            'ss = DTSrch.Rows(0).Item("LocationID").ToString
            If IsDBNull(DTSrch.Rows(0).Item("LocationID")) Then
                cboLocation.SelectedIndex = -1
            Else
                cboLocation.SelectedValue = DTSrch.Rows(0).Item("LocationID").ToString()
            End If

            'txtNotes.Text = DTSrch.Rows(0).Item("Notes").ToString


            'Eng Ahmad Fawzy
            If DTSrch(0).Item("CustomWeight").ToString = "" Or DTSrch(0).Item("CustomWeight").ToString = "0.00" Then
                txtCustomWeight.Text = ""
                chkCustomWeight.Checked = False
                'txtCustomWeight.Enabled = False
            Else
                txtCustomWeight.Text = DTSrch(0).Item("CustomWeight").ToString
                chkCustomWeight.Checked = True
                txtCustomWeight.Enabled = True
            End If

            gTransactionO.Slip_Rate = DTSrch.Rows(0).Item("Slip_Rate").ToString

            Try
                gUserO.ID = DTSrch(0).Item("FirstUser").ToString
                gUserO.Refresh()
                lblFirstUser.Text = gUserO.Name
            Catch ex As Exception
                lblFirstUser.Text = ""
            End Try

            Try
                gUserO.ID = DTSrch(0).Item("SecondUser").ToString
                If gUserO.ID = 0 Then
                    lblSecondUser.Text = ""
                Else
                    gUserO.Refresh()
                    lblSecondUser.Text = gUserO.Name
                End If

            Catch ex As Exception
                lblSecondUser.Text = ""
            End Try

            Dim ReceiveType As Integer
            Try
                ReceiveType = DTSrch.Rows(0).Item("MSGID")
            Catch ex As Exception
                ReceiveType = 0
            End Try


            If ReceiveType = 1 Then
                CboRecieveType.Text = "إسكندرية"
            ElseIf ReceiveType = 2 Then
                CboRecieveType.Text = "العامرية"
            Else
                CboRecieveType.Text = ""
            End If

            ' Color Slip Rate Buttons
            ColorSlipRateButtons()

            If lblInDate.Text.Contains("AM") Then
                lblInDate.Text = lblInDate.Text.Replace("AM", "ص")
            ElseIf lblInDate.Text.Contains("PM") Then
                lblInDate.Text = lblInDate.Text.Replace("PM", "م")
            End If

            If DTSrch.Rows(0).Item("تاريخ الخروج").ToString = "" Then
                lblScndWeight.Text = "0"
                DisableControls(0)

                btnFirstScale.Enabled = False
                btnEditFirstScale.Enabled = True
                btnSecondScale.Enabled = True
                SIsUpdate = True
                lblOutDate.Text = Now
            Else
                lblScndWeight.Text = DTSrch.Rows(0).Item("Second_Weight").ToString
                DisableControls(1)

                btnFirstScale.Enabled = False
                btnEditFirstScale.Enabled = False
                btnSecondScale.Enabled = False
                SIsUpdate = False
                lblOutDate.Text = DTSrch.Rows(0).Item("تاريخ الخروج").ToString & " " & DTSrch.Rows(0).Item("وقت الخروج").ToString
                If lblOutDate.Text.Contains("AM") Then
                    lblOutDate.Text = lblOutDate.Text.Replace("AM", "ص")
                ElseIf lblOutDate.Text.Contains("PM") Then
                    lblOutDate.Text = lblOutDate.Text.Replace("PM", "م")
                End If
            End If

            'If Val(DTSrch.Rows(0).Item("First_Weigth").ToString()) < 100 Then
            '    btnSetSecondWeight.Enabled = False
            '    btnSecondScale.Enabled = False
            'Else
            '    btnSetSecondWeight.Enabled = True
            '    btnSecondScale.Enabled = True
            'End If

            DSSrch.Clear()
            NoChangeCheck = False



        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnPreviousTrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreviousTrans.Click
        Try
            SaveOrCancel = 1
              PrsentScale()

            NoChangeCheck = True
            'Eng Ahmad fawzy
            DSSrch = gAdo.SelectData(" set dateformat dmy SELECT trans.Slip_Rate,CustomWeight,MSGID,isnull(trans.First_Weight_User_ID,'') as FirstUser,isnull(trans.Second_Weight_User_ID,'') as SecondUser,trans.first_weight_user_id,trans.CID ,Good_ID, CarInfo.Car_Info_ID," & _
                               " cars.CarBoard_No,trans.Driver_ID,driver.Driver_License_No,tblCity.City_ID, " & _
                               " trans.Dealer_ID ,substring(convert(varchar(50),trans.[In_Date],111),1,10) as [تاريخ الدخول] ," & _
                               " substring(convert(varchar(50),trans.[In_Date]),12,11) as [وقت الدخول], " & _
                               " substring(convert(varchar(50),trans.[Out_Date],111),1,10) as [تاريخ الخروج] , " & _
                               " substring(convert(varchar(50),trans.[Out_Date]),12,11) as [وقت الخروج],abs(trans.Second_Weight - trans.First_Weigth)as net ," & _
                               " trans.First_Weigth ,trans.Second_Weight ,trans.Issue_To_ID,Issue.Issue_To_Name   as  [المرسل اليه  ],Contractor_ID,Notes,trans.LocationID" & _
                               " FROM tblTransactions  as trans INNER JOIN  tblCar as cars ON trans.Car_ID = cars.Car_ID  " & _
                               " INNER JOIN  tblDriver  as driver  ON trans.Driver_ID = driver.Driver_ID INNER JOIN tblDealer   as  Dealer  ON trans.Dealer_ID = Dealer.Dealer_ID  " & _
                               " INNER JOIN tblCarInfo as CarInfo ON CarInfo.Car_Info_ID = trans.Car_Info_ID INNER JOIN tblIssueTo as Issue ON trans.Issue_To_ID = Issue.Issue_To_ID  " & _
                               " INNER JOIN tblCity ON cars.CarBoard_City_ID = tblCity.City_ID  INNER JOIN dbo.tblCity AS tblCity_1  " & _
                               " ON CarInfo.TruckBoard_City_ID = tblCity_1.City_ID  where  TR_TY=2 " & Scale & "  and trans.CID =(select max(cid) from tblTransactions where tr_ty=2 " & Scale & " and CID < " & txtTransID.Text & ") order by cid desc", False)

            'Dim d As String = " set dateformat dmy SELECT trans.Slip_Rate,CustomWeight,MSGID,isnull(trans.First_Weight_User_ID,'') as FirstUser,isnull(trans.Second_Weight_User_ID,'') as SecondUser,trans.first_weight_user_id,trans.CID ,Good_ID, CarInfo.Car_Info_ID," & _
            '                   " cars.CarBoard_No,trans.Driver_ID,driver.Driver_License_No,tblCity.City_ID, " & _
            '                   " trans.Dealer_ID ,substring(convert(varchar(50),trans.[In_Date],111),1,10) as [تاريخ الدخول] ," & _
            '                   " substring(convert(varchar(50),trans.[In_Date]),12,11) as [وقت الدخول], " & _
            '                   " substring(convert(varchar(50),trans.[Out_Date],111),1,10) as [تاريخ الخروج] , " & _
            '                   " substring(convert(varchar(50),trans.[Out_Date]),12,11) as [وقت الخروج],abs(trans.Second_Weight - trans.First_Weigth)as net ," & _
            '                   " trans.First_Weigth ,trans.Second_Weight ,trans.Issue_To_ID,Issue.Issue_To_Name   as  [المرسل اليه  ]" & _
            '                   " FROM tblTransactions  as trans INNER JOIN  tblCar as cars ON trans.Car_ID = cars.Car_ID  " & _
            '                   " INNER JOIN  tblDriver  as driver  ON trans.Driver_ID = driver.Driver_ID INNER JOIN tblDealer   as  Dealer  ON trans.Dealer_ID = Dealer.Dealer_ID  " & _
            '                   "INNER JOIN tblCarInfo as CarInfo ON CarInfo.Car_Info_ID = trans.Car_Info_ID INNER JOIN tblIssueTo as Issue ON trans.Issue_To_ID = Issue.Issue_To_ID  " & _
            '                   " INNER JOIN tblCity ON cars.CarBoard_City_ID = tblCity.City_ID  INNER JOIN dbo.tblCity AS tblCity_1  " & _
            '                   "  ON CarInfo.TruckBoard_City_ID = tblCity_1.City_ID  where  TR_TY=2 and trans.CID = " & txtTransID.Text - 1 & " order by cid desc"
            If DSSrch.Tables(0).Rows.Count < 1 Then
                'MsgBox("لا توجد حركات اقل من هذه الحركه")
                Exit Sub
            End If
            FrmRefresh()
            Dim DTR() As DataRow
            DTSrch = DSSrch.Tables(0)
            DTR = DTSrch.Select("CID=max (CID)")
            cboGoodType.SelectedValue = DTSrch.Rows(0).Item("Good_ID").ToString
            txtTransID.Text = DTR(0).Item("CID").ToString
            txtCarNo.Text = DTR(0).Item("CarBoard_No").ToString
            cboDriver.SelectedValue = DTR(0).Item("Driver_ID").ToString
            cboDealer.SelectedValue = DTR(0).Item("Dealer_ID").ToString
            cboIssueTo.SelectedValue = DTR(0).Item("Issue_To_ID").ToString
            txtLicenceNo.Text = DTR(0).Item("Driver_License_No").ToString
            cboCarCity.SelectedValue = DTR(0).Item("City_ID").ToString
            lblFrstWeight.Text = DTR(0).Item("First_Weigth").ToString
            cboTruckNo.SelectedValue = DTR(0).Item("Car_Info_ID").ToString
            cboContractor.SelectedValue = DTR(0).Item("Contractor_ID").ToString

            If IsDBNull(DTR(0).Item("LocationID")) Then
                cboLocation.SelectedIndex = -1
            Else
                cboLocation.SelectedValue = DTR(0).Item("LocationID").ToString()
            End If

            If DTSrch.Rows(0).Item("LocationID").ToString = "NULL" Or DTSrch.Rows(0).Item("LocationID").ToString = "" Then
                cboLocation.SelectedIndex = -1
            Else
                cboLocation.SelectedValue = DTSrch.Rows(0).Item("LocationID").ToString()
            End If


            If DTSrch.Rows(0).Item("Notes").ToString = "NULL" Then
                txtNotes.Text = ""
            Else
                txtNotes.Text = DTSrch.Rows(0).Item("Notes").ToString
            End If

            If DTR(0).Item("CustomWeight").ToString = "" Or DTR(0).Item("CustomWeight").ToString = "0.00" Then
                txtCustomWeight.Text = ""
                chkCustomWeight.Checked = False
                'txtCustomWeight.Enabled = False

            Else
                txtCustomWeight.Text = DTR(0).Item("CustomWeight").ToString
                chkCustomWeight.Checked = True
                txtCustomWeight.Enabled = True
            End If


            lblInDate.Text = DTR(0).Item("تاريخ الدخول").ToString & " " & DTR(0).Item("وقت الدخول").ToString
            'Eng Ahmad Fawzy
            gTransactionO.Slip_Rate = DTR(0).Item("Slip_Rate").ToString
            Try
                gUserO.ID = DTSrch(0).Item("FirstUser").ToString
                gUserO.Refresh()
                lblFirstUser.Text = gUserO.Name
            Catch ex As Exception
                lblFirstUser.Text = ""
            End Try

            Try
                gUserO.ID = DTSrch(0).Item("SecondUser").ToString
                If gUserO.ID = 0 Then
                    lblSecondUser.Text = ""
                Else
                    gUserO.Refresh()
                    lblSecondUser.Text = gUserO.Name
                End If

            Catch ex As Exception
                lblSecondUser.Text = ""
            End Try
            Dim ReceiveType As Integer
            Try
                ReceiveType = DTSrch.Rows(0).Item("MSGID")
            Catch ex As Exception
                ReceiveType = 0
            End Try
            If ReceiveType = 1 Then
                CboRecieveType.Text = "إسكندرية"
            ElseIf ReceiveType = 2 Then
                CboRecieveType.Text = "العامرية"
            Else
                CboRecieveType.Text = ""
            End If

            ' Color Slip Rate Buttons
            ColorSlipRateButtons()

            If lblInDate.Text.Contains("AM") Then
                lblInDate.Text = lblInDate.Text.Replace("AM", "ص")
            ElseIf lblInDate.Text.Contains("PM") Then
                lblInDate.Text = lblInDate.Text.Replace("PM", "م")
            End If

            If DTR(0).Item("تاريخ الخروج").ToString = "" Then
                lblScndWeight.Text = "0"
                DisableControls(0)

                btnFirstScale.Enabled = False
                btnEditFirstScale.Enabled = True
                btnSecondScale.Enabled = True
                SIsUpdate = True
                lblOutDate.Text = Now
            Else
                lblScndWeight.Text = DTR(0).Item("Second_Weight").ToString
                DisableControls(1)

                btnFirstScale.Enabled = False
                btnEditFirstScale.Enabled = False
                btnSecondScale.Enabled = False
                SIsUpdate = False
                lblOutDate.Text = DTR(0).Item("تاريخ الخروج").ToString & " " & DTR(0).Item("وقت الخروج").ToString
                If lblOutDate.Text.Contains("AM") Then
                    lblOutDate.Text = lblOutDate.Text.Replace("AM", "ص")
                ElseIf lblOutDate.Text.Contains("PM") Then
                    lblOutDate.Text = lblOutDate.Text.Replace("PM", "م")
                End If
            End If


            'If Val(DTSrch.Rows(0).Item("First_Weigth").ToString()) < 100 Then
            '    btnSetSecondWeight.Enabled = False
            '    btnSecondScale.Enabled = False
            'Else
            '    btnSetSecondWeight.Enabled = True
            '    btnSecondScale.Enabled = True
            'End If

            DSSrch.Clear()
            NoChangeCheck = False

        

        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnNextTrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextTrans.Click
        Try
            SaveOrCancel = 1
              PrsentScale()

            NoChangeCheck = True
            'Eng Ahmad fawzy
            DSSrch = gAdo.SelectData(" set dateformat dmy SELECT trans.Slip_Rate,CustomWeight,isnull(trans.First_Weight_User_ID,'') as FirstUser,isnull(trans.Second_Weight_User_ID,'') as SecondUser,MSGID,trans.first_weight_user_id,trans.CID ,isnull(trans.First_Weight_User_ID,'') as FirstUser,isnull(trans.Second_Weight_User_ID,'') as SecondUser,Good_ID, CarInfo.Car_Info_ID, " & _
                             " cars.CarBoard_No,trans.Driver_ID,driver.Driver_License_No,tblCity.City_ID, " & _
                             " trans.Dealer_ID ,substring(convert(varchar(50),trans.[In_Date],111),1,10) as [تاريخ الدخول] ," & _
                             " substring(convert(varchar(50),trans.[In_Date]),12,11) as [وقت الدخول], " & _
                             " substring(convert(varchar(50),trans.[Out_Date],111),1,10) as [تاريخ الخروج] , " & _
                             " substring(convert(varchar(50),trans.[Out_Date]),12,11) as [وقت الخروج],abs(trans.Second_Weight - trans.First_Weigth)as net ," & _
                             " trans.First_Weigth,trans.Second_Weight ,trans.Issue_To_ID,Issue.Issue_To_Name   as  [المرسل اليه  ],Contractor_ID,Notes,trans.LocationID" & _
                             " FROM tblTransactions  as trans INNER JOIN  tblCar as cars ON trans.Car_ID = cars.Car_ID  " & _
                             " INNER JOIN  tblDriver  as driver  ON trans.Driver_ID = driver.Driver_ID INNER JOIN tblDealer   as  Dealer  ON trans.Dealer_ID = Dealer.Dealer_ID  " & _
                             "INNER JOIN tblCarInfo as CarInfo ON CarInfo.Car_Info_ID = trans.Car_Info_ID INNER JOIN tblIssueTo as Issue ON trans.Issue_To_ID = Issue.Issue_To_ID  " & _
                             " INNER JOIN tblCity ON cars.CarBoard_City_ID = tblCity.City_ID  INNER JOIN dbo.tblCity AS tblCity_1  " & _
                             "  ON CarInfo.TruckBoard_City_ID = tblCity_1.City_ID  where  TR_TY=2 " & Scale & " and trans.CID = (select min(cid) from tblTransactions where tr_ty=2 " & Scale & " and CID > " & txtTransID.Text & ")", False)
            If DSSrch.Tables(0).Rows.Count < 1 Then
                'MsgBox("لا توجد حركات اكبر من هذه الحركه")
                Exit Sub
            End If
            Dim hh As String
            'hh = DTSrch.Rows(0).Item("Good_ID").ToString
            FrmRefresh()
            'hh = DTSrch.Rows(0).Item("Good_ID").ToString
            DTSrch = DSSrch.Tables(0)
            Dim DTR() As DataRow
            DTSrch = DSSrch.Tables(0)
            DTR = DTSrch.Select("CID=max (CID)")
            cboGoodType.SelectedValue = DTSrch.Rows(0).Item("Good_ID").ToString
            txtTransID.Text = DTR(0).Item("CID").ToString
            txtCarNo.Text = DTR(0).Item("CarBoard_No").ToString
            cboDriver.SelectedValue = DTR(0).Item("Driver_ID").ToString
            cboDealer.SelectedValue = DTR(0).Item("Dealer_ID").ToString
            cboIssueTo.SelectedValue = DTR(0).Item("Issue_To_ID").ToString
            txtLicenceNo.Text = DTR(0).Item("Driver_License_No").ToString
            cboCarCity.SelectedValue = DTR(0).Item("City_ID").ToString
            lblFrstWeight.Text = DTR(0).Item("First_Weigth").ToString
            cboTruckNo.SelectedValue = DTR(0).Item("Car_Info_ID").ToString
            lblInDate.Text = DTR(0).Item("تاريخ الدخول").ToString & " " & DTR(0).Item("وقت الدخول").ToString
            cboContractor.SelectedValue = DTR(0).Item("Contractor_ID").ToString

            If DTSrch.Rows(0).Item("Notes").ToString = "NULL" Then
                txtNotes.Text = ""
            Else
                txtNotes.Text = DTSrch.Rows(0).Item("Notes").ToString
            End If


            If IsDBNull(DTR(0).Item("LocationID")) Then
                cboLocation.SelectedIndex = -1
            Else
                cboLocation.SelectedValue = DTR(0).Item("LocationID").ToString()
            End If

            If DTR(0).Item("CustomWeight").ToString = "" Or DTR(0).Item("CustomWeight").ToString = "0.00" Then
                txtCustomWeight.Text = ""
                chkCustomWeight.Checked = False
                'txtCustomWeight.Enabled = False
            Else
                txtCustomWeight.Text = DTR(0).Item("CustomWeight").ToString
                chkCustomWeight.Checked = True
                txtCustomWeight.Enabled = True
            End If

            Try
                gUserO.ID = DTSrch(0).Item("FirstUser").ToString
                gUserO.Refresh()
                lblFirstUser.Text = gUserO.Name
            Catch ex As Exception
                lblFirstUser.Text = ""
            End Try

            Try
                gUserO.ID = DTSrch(0).Item("SecondUser").ToString
                If gUserO.ID = 0 Then
                    lblSecondUser.Text = ""
                Else
                    gUserO.Refresh()
                    lblSecondUser.Text = gUserO.Name
                End If

            Catch ex As Exception
                lblSecondUser.Text = ""
            End Try
            'Eng Ahmad Fawzy
            gTransactionO.Slip_Rate = DTSrch.Rows(0).Item("Slip_Rate").ToString
            Dim ReceiveType As Integer
            Try
                ReceiveType = DTSrch.Rows(0).Item("MSGID")
            Catch ex As Exception
                ReceiveType = 0
            End Try
            If ReceiveType = 1 Then
                CboRecieveType.Text = "إسكندرية"
            ElseIf ReceiveType = 2 Then
                CboRecieveType.Text = "العامرية"
            Else
                CboRecieveType.Text = ""
            End If

            ' Color Slip Rate Buttons
            ColorSlipRateButtons()



            If lblInDate.Text.Contains("AM") Then
                lblInDate.Text = lblInDate.Text.Replace("AM", "ص")
            ElseIf lblInDate.Text.Contains("PM") Then
                lblInDate.Text = lblInDate.Text.Replace("PM", "م")
            End If

            If DTR(0).Item("تاريخ الخروج").ToString = "" Then
                lblScndWeight.Text = "0"
                DisableControls(0)

                btnFirstScale.Enabled = False
                btnEditFirstScale.Enabled = True
                btnSecondScale.Enabled = True
                SIsUpdate = True
                lblOutDate.Text = Now
            Else
                lblScndWeight.Text = DTR(0).Item("Second_Weight").ToString
                DisableControls(1)

                btnFirstScale.Enabled = False
                btnEditFirstScale.Enabled = False
                btnSecondScale.Enabled = False
                SIsUpdate = False
                lblOutDate.Text = DTR(0).Item("تاريخ الخروج").ToString & " " & DTR(0).Item("وقت الخروج").ToString
                If lblOutDate.Text.Contains("AM") Then
                    lblOutDate.Text = lblOutDate.Text.Replace("AM", "ص")
                ElseIf lblOutDate.Text.Contains("PM") Then
                    lblOutDate.Text = lblOutDate.Text.Replace("PM", "م")
                End If
            End If


            'If Val(DTSrch.Rows(0).Item("First_Weigth").ToString()) < 100 Then
            '    btnSetSecondWeight.Enabled = False
            '    btnSecondScale.Enabled = False
            'Else
            '    btnSetSecondWeight.Enabled = True
            '    btnSecondScale.Enabled = True
            'End If

            DSSrch.Clear()
            NoChangeCheck = False

         

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnLastTrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLastTrans.Click

        Try
            SaveOrCancel = 1

            PrsentScale()

            ''' 4/12/2019 Elsaman '''
            ''' Var for New CID Between countFrom and countTo TO Use more than One Scale

            'Dim intCID As Integer
            'intCID = gAdo.CmdExecScalar("select isnull(max(CID),0) as CID from tblTransactions where CID > (select countFrom from tblScale where Scale_ID = " & ScaleNo & ") and CID < (select countTo from tblScale where Scale_ID = " & ScaleNo & ")")

           

            NoChangeCheck = True
            ' Eng Ahmad fawzy
            DSSrch = gAdo.SelectData(" set dateformat dmy SELECT trans.Slip_Rate,CustomWeight,MSGID,isnull(trans.First_Weight_User_ID,'') as FirstUser,isnull(trans.Second_Weight_User_ID,'') as SecondUser,trans.first_weight_user_id,trans.CID,trans.Good_ID , CarInfo.Car_Info_ID, " & _
                             " cars.CarBoard_No,trans.Driver_ID,driver.Driver_License_No,tblCity.City_ID, " & _
                             " trans.Dealer_ID ,substring(convert(varchar(50),trans.[In_Date],111),1,10) as [تاريخ الدخول] ," & _
                             " substring(convert(varchar(50),trans.[In_Date]),12,11) as [وقت الدخول], " & _
                             " substring(convert(varchar(50),trans.[Out_Date],111),1,10) as [تاريخ الخروج] , " & _
                             " substring(convert(varchar(50),trans.[Out_Date]),12,11) as [وقت الخروج],abs(trans.Second_Weight - trans.First_Weigth)as net ," & _
                             " trans.First_Weigth ,trans.Second_Weight ,trans.Issue_To_ID,Issue.Issue_To_Name   as  [المرسل اليه  ],Contractor_ID,Notes,trans.LocationID" & _
                             " FROM tblTransactions  as trans INNER JOIN  tblCar as cars ON trans.Car_ID = cars.Car_ID  " & _
                             " INNER JOIN  tblDriver  as driver  ON trans.Driver_ID = driver.Driver_ID INNER JOIN tblDealer   as  Dealer  ON trans.Dealer_ID = Dealer.Dealer_ID  " & _
                             "INNER JOIN tblCarInfo as CarInfo ON CarInfo.Car_Info_ID = trans.Car_Info_ID INNER JOIN tblIssueTo as Issue ON trans.Issue_To_ID = Issue.Issue_To_ID  " & _
                             " INNER JOIN tblCity ON cars.CarBoard_City_ID = tblCity.City_ID  INNER JOIN dbo.tblCity AS tblCity_1  " & _
                             "  ON CarInfo.TruckBoard_City_ID = tblCity_1.City_ID  where  TR_TY=2 " & Scale & " and trans.CID= (select max(CID) from tblTransactions where  TR_TY=2 " & Scale & ")", False)

            '' Old Code (select max(CID) from tblTransactions where  TR_TY=2)" Replaced intCID



            If DSSrch.Tables(0).Rows.Count < 1 Then
                'MsgBox("لا توجد حركات بعد هذه الحركه")
                Exit Sub
            End If
            FrmRefresh()
            DTSrch = DSSrch.Tables(0)
            txtTransID.Text = DTSrch.Rows(0).Item("CID").ToString
            cboGoodType.SelectedValue = DTSrch.Rows(0).Item("Good_ID").ToString
            txtCarNo.Text = DTSrch.Rows(0).Item("CarBoard_No").ToString
            cboDriver.SelectedValue = DTSrch.Rows(0).Item("Driver_ID").ToString
            cboDealer.SelectedValue = DTSrch.Rows(0).Item("Dealer_ID").ToString
            cboIssueTo.SelectedValue = DTSrch.Rows(0).Item("Issue_To_ID").ToString
            txtLicenceNo.Text = DTSrch.Rows(0).Item("Driver_License_No").ToString
            cboCarCity.SelectedValue = DTSrch.Rows(0).Item("City_ID").ToString
            lblFrstWeight.Text = DTSrch.Rows(0).Item("First_Weigth").ToString
            cboTruckNo.SelectedValue = DTSrch.Rows(0).Item("Car_Info_ID").ToString
            lblInDate.Text = DTSrch.Rows(0).Item("تاريخ الدخول").ToString & " " & DTSrch.Rows(0).Item("وقت الدخول").ToString
            cboContractor.SelectedValue = DTSrch(0).Item("Contractor_ID").ToString

            If IsDBNull(DTSrch.Rows(0).Item("LocationID")) Then
                cboLocation.SelectedIndex = -1
            Else
                cboLocation.SelectedValue = DTSrch.Rows(0).Item("LocationID").ToString()
            End If

            If IsDBNull(DTSrch.Rows(0).Item("Notes")) Then
                txtNotes.Text = ""
            Else
                txtNotes.Text = DTSrch.Rows(0).Item("Notes").ToString
            End If

            'Eng Ahmad Fawzy
            If DTSrch(0).Item("CustomWeight").ToString = "" Or DTSrch(0).Item("CustomWeight").ToString = "0.00" Then
                txtCustomWeight.Text = ""
                chkCustomWeight.Checked = False
                'txtCustomWeight.Enabled = False
            Else
                txtCustomWeight.Text = DTSrch(0).Item("CustomWeight").ToString
                chkCustomWeight.Checked = True
                txtCustomWeight.Enabled = True
            End If

            gTransactionO.Slip_Rate = DTSrch.Rows(0).Item("Slip_Rate").ToString
            Try
                gUserO.ID = DTSrch(0).Item("FirstUser").ToString
                gUserO.Refresh()
                lblFirstUser.Text = gUserO.Name
            Catch ex As Exception
                lblFirstUser.Text = ""
            End Try

            Try
                gUserO.ID = DTSrch(0).Item("SecondUser").ToString
                If gUserO.ID = 0 Then
                    lblSecondUser.Text = ""
                Else
                    gUserO.Refresh()
                    lblSecondUser.Text = gUserO.Name
                End If

            Catch ex As Exception
                lblSecondUser.Text = ""
            End Try

            Dim ReceiveType As Integer
            Try
                ReceiveType = DTSrch.Rows(0).Item("MSGID")
            Catch ex As Exception
                ReceiveType = 0
            End Try

            If ReceiveType = 1 Then
                CboRecieveType.Text = "إسكندرية"
            ElseIf ReceiveType = 2 Then
                CboRecieveType.Text = "العامرية"
            Else
                CboRecieveType.Text = ""
            End If


            ' Color Slip Rate Buttons
            ColorSlipRateButtons()

            If lblInDate.Text.Contains("AM") Then
                lblInDate.Text = lblInDate.Text.Replace("AM", "ص")
            ElseIf lblInDate.Text.Contains("PM") Then
                lblInDate.Text = lblInDate.Text.Replace("PM", "م")
            End If

            If DTSrch.Rows(0).Item("تاريخ الخروج").ToString = "" Then
                lblScndWeight.Text = "0"
                DisableControls(0)

                btnFirstScale.Enabled = False
                btnEditFirstScale.Enabled = True
                btnSecondScale.Enabled = True
                SIsUpdate = True
                lblOutDate.Text = Now
            Else
                lblScndWeight.Text = DTSrch.Rows(0).Item("Second_Weight").ToString
                DisableControls(1)

                btnFirstScale.Enabled = False
                btnEditFirstScale.Enabled = False
                btnSecondScale.Enabled = False
                SIsUpdate = False
                lblOutDate.Text = DTSrch.Rows(0).Item("تاريخ الخروج").ToString & " " & DTSrch.Rows(0).Item("وقت الخروج").ToString
                If lblOutDate.Text.Contains("AM") Then
                    lblOutDate.Text = lblOutDate.Text.Replace("AM", "ص")
                ElseIf lblOutDate.Text.Contains("PM") Then
                    lblOutDate.Text = lblOutDate.Text.Replace("PM", "م")
                End If
            End If


            'If Val(DTSrch.Rows(0).Item("First_Weigth").ToString()) < 100 Then
            '    btnSetSecondWeight.Enabled = False
            '    btnSecondScale.Enabled = False
            'Else
            '    btnSetSecondWeight.Enabled = True
            '    btnSecondScale.Enabled = True
            'End If

            DSSrch.Clear()
            NoChangeCheck = False

       

        Catch ex As Exception

        End Try

    End Sub

    Sub ColorSlipRateButtons()

        If gTransactionO.Slip_Rate = dsSlipRate.Tables(0).Rows(0).Item(0).ToString Then
            btnSlip0.BackColor = Color.Yellow
            btnSlip1.BackColor = Color.DarkGray
            btnSlip2.BackColor = Color.DarkGray
            btnSlip3.BackColor = Color.DarkGray
        ElseIf gTransactionO.Slip_Rate = dsSlipRate.Tables(0).Rows(1).Item(0).ToString Then
            btnSlip0.BackColor = Color.DarkGray
            btnSlip1.BackColor = Color.Yellow
            btnSlip2.BackColor = Color.DarkGray
            btnSlip3.BackColor = Color.DarkGray
        ElseIf gTransactionO.Slip_Rate = dsSlipRate.Tables(0).Rows(2).Item(0).ToString Then
            btnSlip0.BackColor = Color.DarkGray
            btnSlip1.BackColor = Color.DarkGray
            btnSlip2.BackColor = Color.Yellow
            btnSlip3.BackColor = Color.DarkGray
        ElseIf gTransactionO.Slip_Rate = dsSlipRate.Tables(0).Rows(3).Item(0).ToString Then
            btnSlip0.BackColor = Color.DarkGray
            btnSlip1.BackColor = Color.DarkGray
            btnSlip2.BackColor = Color.DarkGray
            btnSlip3.BackColor = Color.Yellow
        Else
            btnSlip0.BackColor = Color.DarkGray
            btnSlip1.BackColor = Color.DarkGray
            btnSlip2.BackColor = Color.DarkGray
            btnSlip3.BackColor = Color.DarkGray
        End If
    End Sub


    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        Try
            gfrmCustQnty = New frmCustQnty
            gfrmCustQnty.ShowDialog()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub cboDealer_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDealer.SelectedValueChanged
        'MsgBox("srfgsdf")
    End Sub

#Region "Insert Into Server DB "

    Public Sub InsertIntoServerDB()

        Try

            Dim DR As SqlDataReader

            Dim Trans_ID, In_Shift_ID, First_Weigth_Scale_ID, _
            First_Weight_User_ID, Second_Weight_User_ID, _
            Second_Weight_Scale_ID, Slip_Charged_User_ID, Driver_ID, _
            Car_ID, CID, Car_Info_ID, ManualEdit, TR_TY As Integer
            Dim In_Date, Out_Date, In_Shift_Date, Dealer_ID, _
            Issue_To_ID, Good_ID, Slip_Charged_Date, _
            TruckBoard_No, CarBoard_No, Driver_Name, Issue_To_Name, Driver_License_No As String
            Dim Slip_Rate, Second_Weight, First_Weigth As Decimal
            Dim First_Weight_IsEmpty, Second_Weight_IsEmpty As Boolean

            If Val(PTransID) = 0 Or PTransID = Nothing Then
                DR = gAdo.CmdExecReader("select Trans_ID,In_Date,Out_Date,In_Shift_ID,In_Shift_Date," _
                            & "T.Car_Info_ID,T.Dealer_ID,T.Issue_To_ID,First_Weigth," _
                            & " First_Weigth_Scale_ID, First_Weight_User_ID, Second_Weight, " _
                            & " Second_Weight_User_ID, Second_Weight_Scale_ID, Good_ID, " _
                            & " Slip_Rate, Slip_Charged_Date, Slip_Charged_User_ID, T.Driver_ID, " _
                            & " T.Car_ID, CID, ManualEdit, First_Weight_IsEmpty, Second_Weight_IsEmpty" _
                            & " ,CarBoard_No,TruckBoard_No,Driver_Name,Issue_To_Name,Driver_License_No,TR_TY" _
                            & " from tblTransactions as T join tblCar as C on T.Car_ID=C.Car_ID" _
                            & " join tblCarInfo as CI on T.Car_Info_ID=CI.Car_Info_ID" _
                            & " join tblDriver as D on T.Driver_ID=D.Driver_ID" _
                            & " join tblIssueTo as I on T.Issue_To_ID=I.Issue_To_ID" _
                            & " where TR_TY =2 and  CID = " & txtTransID.Text)
            Else
                DR = gAdo.CmdExecReader("select Trans_ID,In_Date,Out_Date,In_Shift_ID,In_Shift_Date," _
                            & "T.Car_Info_ID,T.Dealer_ID,T.Issue_To_ID,First_Weigth," _
                            & " First_Weigth_Scale_ID, First_Weight_User_ID, Second_Weight, " _
                            & " Second_Weight_User_ID, Second_Weight_Scale_ID, Good_ID, " _
                            & " Slip_Rate, Slip_Charged_Date, Slip_Charged_User_ID, T.Driver_ID, " _
                            & " T.Car_ID, CID, ManualEdit, First_Weight_IsEmpty, Second_Weight_IsEmpty" _
                            & " ,CarBoard_No,TruckBoard_No,Driver_Name,Issue_To_Name,Driver_License_No,TR_TY" _
                            & " from tblTransactions as T join tblCar as C on T.Car_ID=C.Car_ID" _
                            & " join tblCarInfo as CI on T.Car_Info_ID=CI.Car_Info_ID" _
                            & " join tblDriver as D on T.Driver_ID=D.Driver_ID" _
                            & " join tblIssueTo as I on T.Issue_To_ID=I.Issue_To_ID" _
                            & " where TR_TY =2 and  CID = " & PTransID)
            End If

            While DR.Read
                Trans_ID = DR("Trans_ID")
                In_Shift_ID = DR("In_Shift_ID")
                First_Weigth_Scale_ID = DR("First_Weigth_Scale_ID")
                First_Weight_User_ID = DR("First_Weight_User_ID")
                Second_Weight_User_ID = DR("Second_Weight_User_ID")
                Second_Weight = DR("Second_Weight")
                Second_Weight_Scale_ID = DR("Second_Weight_Scale_ID")
                Slip_Charged_User_ID = DR("Slip_Charged_User_ID")
                Driver_ID = DR("Driver_ID")
                Car_ID = DR("Car_ID")
                CID = DR("CID")
                Car_Info_ID = DR("Car_Info_ID")
                First_Weigth = DR("First_Weigth")
                In_Date = DR("In_Date")
                Out_Date = DR("Out_Date")
                In_Shift_Date = DR("In_Shift_Date")
                Dealer_ID = DR("Dealer_ID")
                Issue_To_ID = DR("Issue_To_ID")
                Good_ID = DR("Good_ID")
                Slip_Charged_Date = DR("Slip_Charged_Date")
                Slip_Rate = DR("Slip_Rate")
                ManualEdit = DR("ManualEdit")
                First_Weight_IsEmpty = DR("First_Weight_IsEmpty")
                Second_Weight_IsEmpty = DR("Second_Weight_IsEmpty")
                CarBoard_No = DR("CarBoard_No")
                TruckBoard_No = DR("TruckBoard_No")
                Driver_Name = DR("Driver_Name")
                Issue_To_Name = DR("Issue_To_Name")
                Driver_License_No = DR("Driver_License_No")
                TR_TY = DR("TR_TY")
            End While
            DR.Close()

            If Out_Date.Contains("م") Then
                Out_Date = Out_Date.Replace("م", "PM")
            ElseIf Out_Date.Contains("ص") Then
                Out_Date = Out_Date.Replace("ص", "AM")
            End If
            'here the code of inserting the transaction in the server database
            Dim SqlCon As New SqlConnection
            Dim SqlCmnd As New SqlCommand

            Try
                Using Scope As New TransactionScope()
                    gconConnection.Close()
                    SqlCon.ConnectionString = GetServerNameForServerDB()
                    SqlCon.Open()
                    SqlCmnd.Connection = SqlCon
                    SQLTrans = SqlCon.BeginTransaction
                    SqlCmnd.CommandType = CommandType.Text

                    SqlCmnd.CommandText = "insert into tblTransactions (Trans_ID,In_Date,Out_Date,In_Shift_ID,In_Shift_Date," _
                                        & "Car_Info_ID,Dealer_ID,Issue_To_ID,First_Weigth," _
                                        & "First_Weigth_Scale_ID,First_Weight_User_ID,Second_Weight," _
                                        & "Second_Weight_User_ID,Second_Weight_Scale_ID,Good_ID,Slip_Rate,Slip_Charged_Date," _
                                        & "Slip_Charged_User_ID, Driver_ID, Car_ID, CID,ManualEdit,First_Weight_IsEmpty,Second_Weight_IsEmpty,TR_TY)" _
                                        & "values (" & Trans_ID & ",'" & In_Date & "','" & Out_Date & "'," & In_Shift_ID & ",'" & In_Shift_Date & "', " _
                                        & "" & Car_Info_ID & ",'" & Dealer_ID & "','" & Issue_To_ID & "'," & First_Weigth & "," _
                                        & "" & First_Weigth_Scale_ID & "," & First_Weight_User_ID & "," & Second_Weight & "," _
                                        & "" & Second_Weight_User_ID & "," & Second_Weight_Scale_ID & "," _
                                        & "'" & Good_ID & "'," & Slip_Rate & ",'" & Slip_Charged_Date & "'," & Slip_Charged_User_ID & "," _
                                        & "" & Driver_ID & "," & Car_ID & "," & CID & "," & ManualEdit & " ,'" & First_Weight_IsEmpty & "','" & Second_Weight_IsEmpty & "'," & TR_TY & ")"
                    'SqlCmnd.CommandText = "INSERT INTO IVDALY_SLS" _
                    '    & "(TR_DT,SLS_NO,IN_TR_DT,IN_TR_TIME,CAR_NO,CAR_NO1,DRV_NAME,DRV_NO,ACC_NO," _
                    '    & "TO_ACC,OUT_TR_DT,OUT_TR_TIME,ITM_CD,WEIGHT_EMPTY,WEIGH_FULL,WEIGHT_NET)" _
                    '    & " VALUES( convert (datetime,'" & In_Date & "')," & Trans_ID & ",convert (datetime,'" & In_Date & "')," _
                    '    & "convert (datetime,'" & In_Date & "'),'" & CarBoard_No & "','" & TruckBoard_No & "', " _
                    '    & "'" & Driver_Name & "','" & Driver_License_No & "'," _
                    '    & "'" & Dealer_ID & "','" & Issue_To_Name & "',convert (datetime,'" & Out_Date & "'), " _
                    '    & "convert (datetime,'" & Out_Date & "'),'" & Good_ID & "'," & WEIGHT_EMPTY & "," _
                    '    & "" & WEIGH_FULL & "," & WEIGHT_NET & ")"

                    SqlCmnd.ExecuteNonQuery()

                    SQLTrans.Commit()
                    Scope.Complete()
                    SqlCon.Close()
                End Using
            Catch ex As Exception

                'if it fail to write it to the server here to insert it to the failed table
                SqlCon.Close()
                gconConnection.Close()
                gconConnection = New DBLib.clsDBConnection(GetConString(Application.StartupPath & "\ConnString.txt"))
                gAdo = New ClsAdo(GetServerName(), GetDataBaseName(), GetUserID(), GetPassword())
                gconConnection.Open()
                gAdo.CmdExec("insert into tblFailedTransactions (Trans_ID,In_Date,Out_Date,In_Shift_ID,In_Shift_Date," _
                           & "Car_Info_ID,Dealer_ID,Issue_To_ID,First_Weigth," _
                           & "First_Weigth_Scale_ID,First_Weight_User_ID,Second_Weight," _
                           & "Second_Weight_User_ID,Second_Weight_Scale_ID,Good_ID,Slip_Rate,Slip_Charged_Date," _
                           & "Slip_Charged_User_ID, Driver_ID, Car_ID, CID,ManualEdit,First_Weight_IsEmpty,Second_Weight_IsEmpty,TR_TY)" _
                           & "values (" & Trans_ID & ",'" & In_Date & "','" & Out_Date & "'," & In_Shift_ID & ",'" & In_Shift_Date & "', " _
                           & "" & Car_Info_ID & ",'" & Dealer_ID & "','" & Issue_To_ID & "'," & First_Weigth & "," _
                           & "" & First_Weigth_Scale_ID & "," & First_Weight_User_ID & "," & Second_Weight & "," _
                           & "" & Second_Weight_User_ID & "," & Second_Weight_Scale_ID & "," _
                           & "'" & Good_ID & "'," & Slip_Rate & ",'" & Slip_Charged_Date & "'," & Slip_Charged_User_ID & "," _
                           & "" & Driver_ID & "," & Car_ID & "," & CID & "," & ManualEdit & " ,'" & First_Weight_IsEmpty & "','" & Second_Weight_IsEmpty & "'," & TR_TY & ")")

            End Try

            ' here the Local conection will be established
            gconConnection.Close()
            gconConnection = New DBLib.clsDBConnection(GetConString(Application.StartupPath & "\ConnString.txt"))
            gAdo = New ClsAdo(GetServerName(), GetDataBaseName(), GetUserID(), GetPassword())
            gconConnection.Open()

        Catch ex As Exception
            gconConnection.Close()
            gconConnection = New DBLib.clsDBConnection(GetConString(Application.StartupPath & "\ConnString.txt"))
            gAdo = New ClsAdo(GetServerName(), GetDataBaseName(), GetUserID(), GetPassword())
            gconConnection.Open()
        End Try

    End Sub

    Public Function WriteFailedTrans(ByVal FailedTrans_ID As Integer) As String

        Dim strContents As String
        Dim objWriter As StreamWriter
        Try
            Dim OldData As String
            OldData = ReadFaildTrans()
            objWriter = New StreamWriter(Application.StartupPath & "\FaildTransActions.txt")
            objWriter.Write(OldData & "-" & FailedTrans_ID)
            objWriter.Close()
            'objReader.Close()
            Return strContents
        Catch Ex As Exception
        End Try

    End Function

    Public Function ReadFaildTrans() As String

        Dim OldData As String
        Dim objReader As StreamReader
        Try
            objReader = New StreamReader(Application.StartupPath & "\FaildTransActions.txt")
            OldData = objReader.ReadToEnd()
            objReader.Close()
            Return OldData
        Catch Ex As Exception
            'ErrInfo = Ex.Message
        End Try

    End Function

    Public Function GetServerNameForServerDB() As String

        Dim ServerName As String
        Dim objReader As StreamReader
        Try
            objReader = New StreamReader(Application.StartupPath & "\ServerConnString.txt")
            ServerName = objReader.ReadToEnd()

            'ServerName = ServerName.Substring(0, ServerName.LastIndexOf("\SQLEXPRESS"))
            objReader.Close()
            Return ServerName
        Catch Ex As Exception
            'ErrInfo = Ex.Message
        End Try
    End Function

    Public Function GetDataBaseNameForServerDB() As String

        Dim DataBaseName As String
        Dim objReader As StreamReader
        Try
            objReader = New StreamReader(Application.StartupPath & "\ServerConnString.txt")
            DataBaseName = objReader.ReadToEnd()
            DataBaseName = DataBaseName.Substring(0, DataBaseName.LastIndexOf(";"))
            DataBaseName = DataBaseName.Substring(0, DataBaseName.LastIndexOf(";"))
            DataBaseName = DataBaseName.Substring(DataBaseName.LastIndexOf("=") + 1)
            objReader.Close()
            Return DataBaseName
        Catch Ex As Exception
            'ErrInfo = Ex.Message
        End Try
    End Function

    Public Function GetUserIDForServerDB() As String

        Dim UserID As String
        Dim objReader As StreamReader
        Try
            objReader = New StreamReader(Application.StartupPath & "\ServerConnString.txt")
            UserID = objReader.ReadToEnd()
            UserID = UserID.Substring(0, UserID.LastIndexOf(";"))
            UserID = UserID.Substring(UserID.LastIndexOf("=") + 1)
            objReader.Close()
            Return UserID
        Catch Ex As Exception
            'ErrInfo = Ex.Message
        End Try
    End Function

    Public Function GetPasswordForServerDB() As String

        Dim Password As String
        Dim objReader As StreamReader
        Try
            objReader = New StreamReader(Application.StartupPath & "\ServerConnString.txt")
            Password = objReader.ReadToEnd()
            Password = Password.Substring(Password.LastIndexOf("=") + 1)
            objReader.Close()
            Return Password
        Catch Ex As Exception
            'ErrInfo = Ex.Message
        End Try
    End Function

#End Region

    Private Sub btnHide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHide.Click
        tmrPnlOptions.Enabled = True
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        'If pnlOption.Width <> 580 Then
        '    tmrPnlOptoion2.Enabled = True
        'End If
        'btnShow.Image = My.Resources.arrow_left_blue

        If pnlOption.Width = 50 Then
            btnShow.Image = My.Resources.arrow_left_blue
            tmrPnlOptoion2.Enabled = True
        ElseIf pnlOption.Width = 580 Then
            btnShow.Image = My.Resources.arrow_right_blue
            tmrPnlOptions.Enabled = True
        End If

    End Sub

    Private Sub tmrPnlOptions_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrPnlOptions.Tick
        pnlOption.Width -= 50
        If pnlOption.Width = 50 Or pnlOption.Width < 50 Then
            pnlOption.Width = 50
            tmrPnlOptions.Enabled = False
        End If
    End Sub

    Private Sub tmrPnlOptoion2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrPnlOptoion2.Tick
        pnlOption.Width += 50
        If pnlOption.Width = 580 Or pnlOption.Width > 580 Then
            pnlOption.Width = 580
            tmrPnlOptoion2.Enabled = False
        End If
    End Sub

    Private Sub btnLogOff_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogOff.MouseLeave
        sender.Backcolor = Color.Moccasin
    End Sub

    Private Sub ASTIME_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ASTIME.Tick
        Dim STime As String
        STime = Now
        STime = STime.Substring(11, 5) & " " & STime.Substring(20)
        lblTime.Text = STime
    End Sub

    Public Sub ReFill()
        Try
            gAdo.CtrlItemsLoad("Issue_To_ID", "Issue_To_Name", "tblIssueTo  ORDER BY Issue_To_Name", cboIssueTo)
            gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", "TblDealer where Type=1 ORDER BY Dealer_Name", cboDealer)
            gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood  where type in (2,3) or Good_ID=1 ORDER BY Good_Name desc", cboGoodType)
            gAdo.CtrlItemsLoad("Driver_ID", "Driver_Name", "tblDriver ORDER BY Driver_Name", cboDriver)
            gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity ORDER BY City_Name", cboCarCity, "", "")
            gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity ORDER BY City_Name", cboTruckCity, "", "")
            gAdo.CtrlItemsLoad("Car_Info_ID", "TruckBoard_No", "tblCarInfo ORDER BY TruckBoard_No", cboTruckNo, "", "")
            gAdo.CtrlItemsLoad("Slip_ID", "Slip_Rate", "tblSlip", CboSlip, "", "Slip_IsDeleted='false' ORDER BY Slip_Name")
            gAdo.CtrlItemsLoad("Contractor_ID", "Contractor_Name", "tblContractor ORDER BY Contractor_Name", cboContractor, "", "")
            Dim hh As String
            hh = cboGoodType.SelectedValue
            hh = cboGoodType.SelectedValue
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtCarNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCarNo.TextChanged
        Try
            Dim C As Integer
            C = gAdo.CmdExecScalar("select count(Car_ID)from tblCar where CarBoard_No ='" & txtCarNo.Text.Trim & "'")
            If C > 0 Then
                cboCarCity.SelectedValue = gAdo.CmdExecScalar("SELECT CarBoard_City_ID from tblCar where Car_ID = (SELECT Car_ID from tblTransactions WHERE Trans_ID =( SELECT MAX(Trans_ID) FROM tblTransactions WHERE (Car_ID) IN (SELECT Car_ID FROM tblCar WHERE CarBoard_No ='" & txtCarNo.Text.Trim & "')))")
                cboTruckNo.SelectedValue = gAdo.CmdExecScalar("SELECT Car_Info_ID from tblTransactions WHERE Trans_ID =( SELECT MAX(Trans_ID) FROM tblTransactions WHERE (Car_ID) IN (SELECT Car_ID FROM tblCar WHERE CarBoard_No ='" & txtCarNo.Text.Trim & "'))")

            Else
                cboCarCity.SelectedIndex = -1
                cboTruckNo.SelectedValue = -1
            End If
        Catch ex As Exception
            If txtCarNo.Text.Trim = "" Then
            Else
                If gAdo.CmdExecScalar("SELECT CarBoard_City_ID from tblCar where Car_ID ='" & txtCarNo.Text.Trim & "'") Is Nothing Then
                    'MsgBox("Nothing")
                Else
                    cboCarCity.SelectedValue = gAdo.CmdExecScalar("SELECT CarBoard_City_ID from tblCar where Car_ID ='" & txtCarNo.Text.Trim & "'")
                End If
                cboTruckNo.SelectedValue = -1
            End If

        End Try
    End Sub

    'Public Sub ThreadInsertServerDB()
    '    '~~~~~~~~~~ Insert The Transaction Into The Server DB ~~~~~~~~~~ Start ~~
    '    Dim ThreadServerInsert As New System.Threading.Thread(AddressOf InsertIntoServerDB)
    '    Control.CheckForIllegalCrossThreadCalls = False
    '    'Thread.Join()
    '    ThreadServerInsert.Start()

    '    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ END ~~
    'End Sub

    Public Sub InsertIntoFailed()
        Try


            Dim DR As SqlDataReader

            Dim Trans_ID, In_Shift_ID, First_Weigth_Scale_ID, _
            First_Weight_User_ID, Second_Weight_User_ID, _
            Second_Weight_Scale_ID, Slip_Charged_User_ID, Driver_ID, _
            Car_ID, CID, Car_Info_ID, ManualEdit, TR_TY As Integer
            Dim In_Date, Out_Date, In_Shift_Date, Dealer_ID, _
            Issue_To_ID, Good_ID, Slip_Charged_Date, _
            TruckBoard_No, CarBoard_No, Driver_Name, Issue_To_Name, Driver_License_No As String
            Dim Slip_Rate, Second_Weight, First_Weigth As Decimal
            Dim First_Weight_IsEmpty, Second_Weight_IsEmpty As Boolean

            If Val(PTransID) = 0 Or PTransID = Nothing Then
                DR = gAdo.CmdExecReader("select Trans_ID,In_Date,Out_Date,In_Shift_ID,In_Shift_Date," _
                            & "T.Car_Info_ID,T.Dealer_ID,T.Issue_To_ID,First_Weigth," _
                            & " First_Weigth_Scale_ID, First_Weight_User_ID, Second_Weight, " _
                            & " Second_Weight_User_ID, Second_Weight_Scale_ID, Good_ID, " _
                            & " Slip_Rate, Slip_Charged_Date, Slip_Charged_User_ID, T.Driver_ID, " _
                            & " T.Car_ID, CID, ManualEdit, First_Weight_IsEmpty, Second_Weight_IsEmpty" _
                            & " ,CarBoard_No,TruckBoard_No,Driver_Name,Issue_To_Name,Driver_License_No,TR_TY" _
                            & " from tblTransactions as T join tblCar as C on T.Car_ID=C.Car_ID" _
                            & " join tblCarInfo as CI on T.Car_Info_ID=CI.Car_Info_ID" _
                            & " join tblDriver as D on T.Driver_ID=D.Driver_ID" _
                            & " join tblIssueTo as I on T.Issue_To_ID=I.Issue_To_ID" _
                            & " where TR_TY =2 and  CID = " & txtTransID.Text)
            Else
                DR = gAdo.CmdExecReader("select Trans_ID,In_Date,Out_Date,In_Shift_ID,In_Shift_Date," _
                            & "T.Car_Info_ID,T.Dealer_ID,T.Issue_To_ID,First_Weigth," _
                            & " First_Weigth_Scale_ID, First_Weight_User_ID, Second_Weight, " _
                            & " Second_Weight_User_ID, Second_Weight_Scale_ID, Good_ID, " _
                            & " Slip_Rate, Slip_Charged_Date, Slip_Charged_User_ID, T.Driver_ID, " _
                            & " T.Car_ID, CID, ManualEdit, First_Weight_IsEmpty, Second_Weight_IsEmpty" _
                            & " ,CarBoard_No,TruckBoard_No,Driver_Name,Issue_To_Name,Driver_License_No,TR_TY" _
                            & " from tblTransactions as T join tblCar as C on T.Car_ID=C.Car_ID" _
                            & " join tblCarInfo as CI on T.Car_Info_ID=CI.Car_Info_ID" _
                            & " join tblDriver as D on T.Driver_ID=D.Driver_ID" _
                            & " join tblIssueTo as I on T.Issue_To_ID=I.Issue_To_ID" _
                            & " where TR_TY =2 and  CID = " & PTransID)
            End If

            While DR.Read
                Trans_ID = DR("Trans_ID")
                In_Shift_ID = DR("In_Shift_ID")
                First_Weigth_Scale_ID = DR("First_Weigth_Scale_ID")
                First_Weight_User_ID = DR("First_Weight_User_ID")
                Second_Weight_User_ID = DR("Second_Weight_User_ID")
                Second_Weight = DR("Second_Weight")
                Second_Weight_Scale_ID = DR("Second_Weight_Scale_ID")
                Slip_Charged_User_ID = DR("Slip_Charged_User_ID")
                Driver_ID = DR("Driver_ID")
                Car_ID = DR("Car_ID")
                CID = DR("CID")
                Car_Info_ID = DR("Car_Info_ID")
                First_Weigth = DR("First_Weigth")
                In_Date = DR("In_Date")
                Out_Date = DR("Out_Date")
                In_Shift_Date = DR("In_Shift_Date")
                Dealer_ID = DR("Dealer_ID")
                Issue_To_ID = DR("Issue_To_ID")
                Good_ID = DR("Good_ID")
                Slip_Charged_Date = DR("Slip_Charged_Date")
                Slip_Rate = DR("Slip_Rate")
                ManualEdit = DR("ManualEdit")
                First_Weight_IsEmpty = DR("First_Weight_IsEmpty")
                Second_Weight_IsEmpty = DR("Second_Weight_IsEmpty")
                CarBoard_No = DR("CarBoard_No")
                TruckBoard_No = DR("TruckBoard_No")
                Driver_Name = DR("Driver_Name")
                Issue_To_Name = DR("Issue_To_Name")
                Driver_License_No = DR("Driver_License_No")
                TR_TY = DR("TR_TY")
            End While
            DR.Close()

            If Out_Date.Contains("م") Then
                Out_Date = Out_Date.Replace("م", "PM")
            ElseIf Out_Date.Contains("ص") Then
                Out_Date = Out_Date.Replace("ص", "AM")
            End If

            gAdo.CmdExec("insert into tblFailedTransactions (Trans_ID,In_Date,Out_Date,In_Shift_ID,In_Shift_Date," _
                           & "Car_Info_ID,Dealer_ID,Issue_To_ID,First_Weigth," _
                           & "First_Weigth_Scale_ID,First_Weight_User_ID,Second_Weight," _
                           & "Second_Weight_User_ID,Second_Weight_Scale_ID,Good_ID,Slip_Rate,Slip_Charged_Date," _
                           & "Slip_Charged_User_ID, Driver_ID, Car_ID, CID,ManualEdit,First_Weight_IsEmpty,Second_Weight_IsEmpty,TR_TY)" _
                           & "values (" & Trans_ID & ",'" & In_Date & "','" & Out_Date & "'," & In_Shift_ID & ",'" & In_Shift_Date & "', " _
                           & "" & Car_Info_ID & ",'" & Dealer_ID & "','" & Issue_To_ID & "'," & First_Weigth & "," _
                           & "" & First_Weigth_Scale_ID & "," & First_Weight_User_ID & "," & Second_Weight & "," _
                           & "" & Second_Weight_User_ID & "," & Second_Weight_Scale_ID & "," _
                           & "'" & Good_ID & "'," & Slip_Rate & ",'" & Slip_Charged_Date & "'," & Slip_Charged_User_ID & "," _
                           & "" & Driver_ID & "," & Car_ID & "," & CID & "," & ManualEdit & " ,'" & First_Weight_IsEmpty & "','" & Second_Weight_IsEmpty & "'," & TR_TY & ")")


        Catch ex As Exception

            Dim ErrorFile As String = Application.StartupPath & "\ErrorFailed.txt"
            If System.IO.File.Exists(ErrorFile) = True Then
                Dim objWriter As StreamWriter
                objWriter = File.AppendText(ErrorFile)
                objWriter.WriteLine(vbCrLf & ex.Message)
                objWriter.Close()
            Else
            End If

        End Try
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSecondWeight.Click
        lblScndWeight.Text = 35000
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetFirstWeight.Click
        lblFrstWeight.Text = 10000
    End Sub

    Public Sub Search(ByVal CID As Integer)
        Try
            NoChangeCheck = True
            PrsentScale()
            DSSrch = gAdo.SelectData(" set dateformat dmy SELECT trans.Slip_Rate,CustomWeight,isnull(trans.First_Weight_User_ID,'') as FirstUser,isnull(trans.Second_Weight_User_ID,'') as SecondUser,MSGID,trans.CID ,Good_ID, CarInfo.Car_Info_ID,first_weight_user_id," & _
                               " cars.CarBoard_No,trans.Driver_ID,driver.Driver_License_No,tblCity.City_ID, " & _
                               " trans.Dealer_ID ,substring(convert(varchar(50),trans.[In_Date],111),1,10) as [تاريخ الدخول] ," & _
                               " substring(convert(varchar(50),trans.[In_Date]),12,11) as [وقت الدخول], " & _
                               " substring(convert(varchar(50),trans.[Out_Date],111),1,10) as [تاريخ الخروج] , " & _
                               " substring(convert(varchar(50),trans.[Out_Date]),12,11) as [وقت الخروج],abs(trans.Second_Weight - trans.First_Weigth)as net ," & _
                               " trans.First_Weigth ,trans.Second_Weight ,trans.Issue_To_ID,Issue.Issue_To_Name   as  [المرسل اليه  ],Notes,LocationID" & _
                               " FROM tblTransactions  as trans INNER JOIN  tblCar as cars ON trans.Car_ID = cars.Car_ID  " & _
                               " INNER JOIN  tblDriver  as driver  ON trans.Driver_ID = driver.Driver_ID INNER JOIN tblDealer   as  Dealer  ON trans.Dealer_ID = Dealer.Dealer_ID  " & _
                               "INNER JOIN tblCarInfo as CarInfo ON CarInfo.Car_Info_ID = trans.Car_Info_ID INNER JOIN tblIssueTo as Issue ON trans.Issue_To_ID = Issue.Issue_To_ID  " & _
                               " INNER JOIN tblCity ON cars.CarBoard_City_ID = tblCity.City_ID  INNER JOIN dbo.tblCity AS tblCity_1  " & _
                               "  ON CarInfo.TruckBoard_City_ID = tblCity_1.City_ID  where  TR_TY=2 and trans.CID = " & CID & Scale & " order by cid desc", False)
            If DSSrch.Tables(0).Rows.Count < 1 Then
                'MsgBox("لا توجد حركات اقل من هذه الحركه")
                Exit Sub
            End If
            FrmRefresh()
            Dim DTR() As DataRow
            DTSrch = DSSrch.Tables(0)
            DTR = DTSrch.Select("CID=max (CID)")
            cboGoodType.SelectedValue = DTSrch.Rows(0).Item("Good_ID").ToString
            txtTransID.Text = DTR(0).Item("CID").ToString
            txtCarNo.Text = DTR(0).Item("CarBoard_No").ToString
            cboDriver.SelectedValue = DTR(0).Item("Driver_ID").ToString
            cboDealer.SelectedValue = DTR(0).Item("Dealer_ID").ToString
            cboIssueTo.SelectedValue = DTR(0).Item("Issue_To_ID").ToString
            txtLicenceNo.Text = DTR(0).Item("Driver_License_No").ToString
            cboCarCity.SelectedValue = DTR(0).Item("City_ID").ToString
            lblFrstWeight.Text = DTR(0).Item("First_Weigth").ToString
            cboTruckNo.SelectedValue = DTR(0).Item("Car_Info_ID").ToString
            lblInDate.Text = DTR(0).Item("تاريخ الدخول").ToString & " " & DTR(0).Item("وقت الدخول").ToString
            txtNotes.Text = DTR(0).Item("Notes").ToString
            cboLocation.SelectedValue = DTR(0).Item("LocationID").ToString
            If DTR(0).Item("CustomWeight").ToString = "" Or DTR(0).Item("CustomWeight").ToString = "0.00" Then
                txtCustomWeight.Text = ""
                chkCustomWeight.Checked = False
                'txtCustomWeight.Enabled = False
            Else
                txtCustomWeight.Text = DTR(0).Item("CustomWeight").ToString
                chkCustomWeight.Checked = True
                txtCustomWeight.Enabled = True
            End If

            Try
                gUserO.ID = DTSrch(0).Item("FirstUser").ToString
                gUserO.Refresh()
                lblFirstUser.Text = gUserO.Name
            Catch ex As Exception
                lblFirstUser.Text = ""
            End Try

            Try
                gUserO.ID = DTSrch(0).Item("SecondUser").ToString
                If gUserO.ID = 0 Then
                    lblSecondUser.Text = ""
                Else
                    gUserO.Refresh()
                    lblSecondUser.Text = gUserO.Name
                End If

            Catch ex As Exception
                lblSecondUser.Text = ""
            End Try
            If lblInDate.Text.Contains("AM") Then
                lblInDate.Text = lblInDate.Text.Replace("AM", "ص")
            ElseIf lblInDate.Text.Contains("PM") Then
                lblInDate.Text = lblInDate.Text.Replace("PM", "م")
            End If
            Dim ReceiveType As Integer
            ReceiveType = DTSrch.Rows(0).Item("MSGID")

            If ReceiveType = 1 Then
                CboRecieveType.Text = "إسكندرية"
            ElseIf ReceiveType = 2 Then
                CboRecieveType.Text = "العامرية"
            Else
                CboRecieveType.Text = ""
            End If


            If lblInDate.Text.Contains("AM") Then
                lblInDate.Text = lblInDate.Text.Replace("AM", "ص")
            ElseIf lblInDate.Text.Contains("PM") Then
                lblInDate.Text = lblInDate.Text.Replace("PM", "م")
            End If
            gTransactionO.Slip_Rate = DTSrch.Rows(0).Item("Slip_Rate").ToString

            ColorSlipRateButtons()





            If DTR(0).Item("تاريخ الخروج").ToString = "" Then
                lblScndWeight.Text = "0"
                btnFirstScale.Enabled = True
                btnEditFirstScale.Enabled = True
                btnSecondScale.Enabled = True
                SIsUpdate = True
                lblOutDate.Text = Now
            Else
                lblScndWeight.Text = DTR(0).Item("Second_Weight").ToString
                btnFirstScale.Enabled = False
                btnEditFirstScale.Enabled = False
                btnSecondScale.Enabled = False
                SIsUpdate = False
                lblOutDate.Text = DTR(0).Item("تاريخ الخروج").ToString & " " & DTR(0).Item("وقت الخروج").ToString
                If lblOutDate.Text.Contains("AM") Then
                    lblOutDate.Text = lblOutDate.Text.Replace("AM", "ص")
                ElseIf lblOutDate.Text.Contains("PM") Then
                    lblOutDate.Text = lblOutDate.Text.Replace("PM", "م")
                End If
            End If


            If Val(DTSrch.Rows(0).Item("First_Weigth").ToString()) < 100 Then
                'btnSetSecondWeight.Enabled = False
                btnSecondScale.Enabled = False
            Else
                btnSetSecondWeight.Enabled = True
                btnSecondScale.Enabled = True
            End If

            If (IsDBNull(DTR(0).Item("LocationID"))) Then
                cboLocation.SelectedIndex = -1
            Else : cboLocation.SelectedValue = DTR(0).Item("LocationID").ToString()
            End If



            DSSrch.Clear()
            NoChangeCheck = False
        Catch ex As Exception
        End Try

    End Sub

    Private Sub LinkLabel4_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        pnlHelp.Visible = False
    End Sub

    'Public Sub PrintNewReport()
    '    Try

    '        Dim ConnPath, ConnString As String
    '        ConnPath = Application.StartupPath & "\ConnString.txt"
    '        ConnString = GetConString(ConnPath)
    '        Dim Con As New SqlConnection(ConnString)
    '        gAdo.OpeningCon()
    '        Dim Adp As New SqlDataAdapter(" select Company_Name,Company_Address,Company_Telephone," _
    '                                      & " Picture from tblcompany join dbo.tblPicture on Company_Pic_ID=Pic_ID", Con)

    '        Dim DT As New DataTable
    '        Adp.Fill(DT)

    '        Dim rp As New CopyofrptPolicy
    '        Dim crv As New CrystalDecisions.Windows.Forms.CrystalReportViewer
    '        'rp.Refresh()
    '        'rp.SetDataSource(DT)
    '        'rp.PrintOptions.PrinterName = "\\BB\HP Deskjet F300 series"

    '        Dim NetWeight As String

    '        If Val(lblScndWeight.Text) < 1 Then
    '            NetWeight = 0
    '        Else
    '            NetWeight = Abs(CDec(Val(lblFrstWeight.Text.Trim)) - CDec(Val(lblScndWeight.Text.Trim)))
    '        End If


    '        'Dim s As String
    '        PTransID = txtTransID.Text

    '        If AutoPrint = False Then
    '            rp.SetParameterValue("TransNo", PTransID)
    '            rp.SetParameterValue("CarNo", PCarNo)
    '            rp.SetParameterValue("CarCity", PCarCity)
    '            rp.SetParameterValue("TruckNo", PTruckNo)
    '            rp.SetParameterValue("TruckCity", PTruckCity)
    '            rp.SetParameterValue("DriverName", PDriverName)
    '            rp.SetParameterValue("DriverLicenceNo", PDriverLicence)
    '            rp.SetParameterValue("Customer", PDealerName)
    '            rp.SetParameterValue("IssueTo", PIssueto)
    '            rp.SetParameterValue("EmptyWieght", PFisrtScale)
    '            rp.SetParameterValue("GoodName", PGoodName)
    '            rp.SetParameterValue("FullWieght", PSecondScale)
    '            rp.SetParameterValue("NetWieght", PNetScale)
    '            rp.SetParameterValue("RecieveType", PRecieveType)
    '            If Val(PNetScale.Length) > 3 Then
    '                Dim kilo, ton As String
    '                kilo = PNetScale.Substring(PNetScale.Length - 3)
    '                ton = PNetScale.Substring(0, PNetScale.Length - 3)
    '                rp.SetParameterValue("Kilo", kilo)
    '                rp.SetParameterValue("Ton", ton)
    '            Else
    '                rp.SetParameterValue("Kilo", PNetScale)
    '                rp.SetParameterValue("Ton", "")
    '            End If
    '            rp.SetParameterValue("Arabic", PArabic)
    '        Else
    '            rp.SetParameterValue("TransNo", txtTransID.Text)
    '            rp.SetParameterValue("CarNo", txtCarNo.Text)
    '            rp.SetParameterValue("CarCity", cboCarCity.Text)
    '            rp.SetParameterValue("TruckNo", cboTruckNo.Text)
    '            rp.SetParameterValue("TruckCity", cboTruckCity.Text)
    '            rp.SetParameterValue("DriverName", cboDriver.Text)
    '            rp.SetParameterValue("DriverLicenceNo", txtLicenceNo.Text)
    '            rp.SetParameterValue("Customer", cboDealer.Text)
    '            rp.SetParameterValue("IssueTo", cboIssueTo.Text)
    '            rp.SetParameterValue("GoodName", cboGoodType.Text)
    '            rp.SetParameterValue("EmptyWieght", CDec(Val(lblFrstWeight.Text.Trim)))
    '            rp.SetParameterValue("FullWieght", CDec(Val(lblScndWeight.Text.Trim)))
    '            rp.SetParameterValue("NetWieght", NetWeight)
    '            rp.SetParameterValue("RecieveType", CboRecieveType.Text)
    '            If Val(NetWeight.Length) > 3 Then
    '                Dim kilo, ton As String
    '                kilo = NetWeight.Substring(NetWeight.Length - 3)
    '                ton = NetWeight.Substring(0, NetWeight.Length - 3)
    '                rp.SetParameterValue("Kilo", kilo)
    '                rp.SetParameterValue("Ton", ton)
    '            Else
    '                rp.SetParameterValue("Kilo", NetWeight)
    '                rp.SetParameterValue("Ton", "")
    '            End If
    '            rp.SetParameterValue("Arabic", HANY(Val(lblNetWeight.Text.Trim), "EGYPT"))
    '        End If

    '        Dim NowDate As String
    '        Try
    '            If AutoPrint = False Then
    '                NowDate = gAdo.CmdExecScalar("select Out_Date from tblTransactions where TR_TY=2 and CID=" & PTransID)
    '            Else
    '                NowDate = gAdo.CmdExecScalar("select Out_Date from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
    '            End If
    '        Catch ex As Exception
    '            NowDate = ""
    '        End Try

    '        If NowDate = "" Then
    '            rp.SetParameterValue("Out_Date", "")
    '        Else
    '            rp.SetParameterValue("Out_Date", NowDate)
    '        End If

    '        If chkPrintCompayName.Checked = True Then
    '            rp.SetParameterValue("CompanyName", "")
    '            rp.SetParameterValue("CompanyAddress", "")
    '            rp.SetParameterValue("CompanyTel", "")
    '            rp.Section2.ReportObjects("Picture1").ObjectFormat.EnableSuppress = True
    '        Else
    '            rp.SetParameterValue("CompanyName", DT.Rows(0).Item("Company_Name").ToString())
    '            rp.SetParameterValue("CompanyAddress", DT.Rows(0).Item("Company_Address").ToString())
    '            rp.SetParameterValue("CompanyTel", DT.Rows(0).Item("Company_Telephone").ToString())
    '            rp.Section2.ReportObjects("Picture1").ObjectFormat.EnableSuppress = False
    '        End If
    '        'Print Report

    '        grptpth.ReportPath(rp, crv)
    '        For i As Integer = 0 To 2
    '            If i = 0 Then
    '                rp.Section2.ReportObjects("Picture8").ObjectFormat.EnableSuppress = True
    '            Else
    '                rp.Section2.ReportObjects("Picture8").ObjectFormat.EnableSuppress = False
    '            End If
    '            rp.PrintToPrinter(1, False, 0, 0)
    '        Next




    '    Catch ex As Exception

    '    End Try
    'End Sub
    Public Sub PrintNewReport()
        Try

            Dim ConnPath, ConnString As String
            ConnPath = Application.StartupPath & "\ConnString.txt"
            ConnString = GetConString(ConnPath)
            Dim Con As New SqlConnection(ConnString)
            gAdo.OpeningCon()
            Dim Adp As New SqlDataAdapter(" select Company_Name,Company_Address,Company_Telephone," _
                                          & " Picture from tblcompany join dbo.tblPicture on Company_Pic_ID=Pic_ID", Con)

            Dim DT As New DataTable
            Adp.Fill(DT)
            Dim txtTrans As Integer
            txtTrans = Val(txtTransID.Text)
            Dim Adp1 As New SqlDataAdapter("select u1.[user_name], u2.[user_name] from tblTransactions as trans" _
                                         & " left join tblUser as U1 on trans.First_Weight_User_ID=U1.[User_ID] " _
                                         & " left join tblUser as U2 on trans.Second_Weight_User_ID=U2.[User_ID]" _
                                        & " where tr_ty =2 and CID='" & txtTrans & "'", Con)

            Dim DT1 As New DataTable
            Adp1.Fill(DT1)

            Dim rp As New CopyofrptPolicy
            Dim crv As New CrystalDecisions.Windows.Forms.CrystalReportViewer
            'rp.Refresh()
            'rp.SetDataSource(DT)
            'rp.PrintOptions.PrinterName = "\\BB\HP Deskjet F300 series"

            Dim NetWeight As String

            If Val(lblScndWeight.Text) < 1 Then
                NetWeight = 0
            Else
                NetWeight = Abs(CDec(Val(lblFrstWeight.Text.Trim)) - CDec(Val(lblScndWeight.Text.Trim)))
            End If
            Dim FirstUser, SecondUser As String
            If DT1.Rows.Count > 0 Then
                FirstUser = DT1.Rows(0).Item(0).ToString()
            Else
                FirstUser = ""

            End If
            If DT1.Rows.Count > 0 Then
                SecondUser = DT1.Rows(0).Item(1).ToString()
            Else
                SecondUser = ""

            End If
            rp.SetParameterValue("FirstUser", FirstUser)
            rp.SetParameterValue("SecondUser", SecondUser)



            Dim dadap As New SqlDataAdapter("Select U.User_Name from tblUser U JOIN tblTransactions T ON U.User_ID = T.Slip_Charged_User_ID WHERE CID='" & txtTrans & "' AND TR_TY=2", Con)

            Dim DataT As New DataTable
            dadap.Fill(DataT)

            If DataT.Rows.Count > 0 Then
                rp.SetParameterValue("EditUser", "تعديل الوزان: " & DataT.Rows(0).Item(0))
                'rp.Section2.ReportObjects("EditUserLabel").ObjectFormat.EnableSuppress = False
            Else

                rp.SetParameterValue("EditUser", "")
            End If


            If CboRecieveType.Text = "إسكندرية" Or CboRecieveType.Text = "" Then
                PRecieveType = ""
            ElseIf CboRecieveType.Text = "العامرية" Then
                PRecieveType = "العامرية"
            End If


            If AutoPrint = False Then
                rp.SetParameterValue("TransNo", PTransID)
                rp.SetParameterValue("CarNo", PCarNo)
                rp.SetParameterValue("CarCity", PCarCity)
                rp.SetParameterValue("TruckNo", PTruckNo)
                rp.SetParameterValue("TruckCity", PTruckCity)
                rp.SetParameterValue("DriverName", PDriverName)
                rp.SetParameterValue("DriverLicenceNo", PDriverLicence)
                rp.SetParameterValue("Customer", PDealerName)
                rp.SetParameterValue("IssueTo", PIssueto)
                rp.SetParameterValue("EmptyWieght", PFisrtScale)
                rp.SetParameterValue("GoodName", PGoodName)
                rp.SetParameterValue("FullWieght", PSecondScale)
                rp.SetParameterValue("NetWieght", PNetScale)
                rp.SetParameterValue("RecieveType", PRecieveType)
                If Val(PNetScale.Length) > 3 Then
                    Dim kilo, ton As String
                    kilo = PNetScale.Substring(PNetScale.Length - 3)
                    ton = PNetScale.Substring(0, PNetScale.Length - 3)
                    rp.SetParameterValue("Kilo", kilo)
                    rp.SetParameterValue("Ton", ton)
                Else
                    rp.SetParameterValue("Kilo", PNetScale)
                    rp.SetParameterValue("Ton", "")
                End If
                rp.SetParameterValue("Arabic", PArabic)
            Else
                rp.SetParameterValue("TransNo", txtTransID.Text)
                rp.SetParameterValue("CarNo", txtCarNo.Text)
                rp.SetParameterValue("CarCity", cboCarCity.Text)
                rp.SetParameterValue("TruckNo", cboTruckNo.Text)
                rp.SetParameterValue("TruckCity", cboTruckCity.Text)
                rp.SetParameterValue("DriverName", cboDriver.Text)
                rp.SetParameterValue("DriverLicenceNo", txtLicenceNo.Text)
                rp.SetParameterValue("Customer", cboDealer.Text)
                rp.SetParameterValue("IssueTo", cboIssueTo.Text)
                rp.SetParameterValue("GoodName", cboGoodType.Text)
                rp.SetParameterValue("EmptyWieght", CDec(Val(lblFrstWeight.Text.Trim)))
                rp.SetParameterValue("FullWieght", CDec(Val(lblScndWeight.Text.Trim)))
                rp.SetParameterValue("NetWieght", NetWeight)
                rp.SetParameterValue("RecieveType", PRecieveType)
                If Val(NetWeight.Length) > 3 Then
                    Dim kilo, ton As String
                    kilo = NetWeight.Substring(NetWeight.Length - 3)
                    ton = NetWeight.Substring(0, NetWeight.Length - 3)
                    rp.SetParameterValue("Kilo", kilo)
                    rp.SetParameterValue("Ton", ton)
                Else
                    rp.SetParameterValue("Kilo", NetWeight)
                    rp.SetParameterValue("Ton", "")
                End If
                rp.SetParameterValue("Arabic", HANY(Val(lblNetWeight.Text.Trim), "EGYPT"))
            End If

            Dim InDate, OutDate As String
            Try
                If AutoPrint = False Then
                    InDate = gAdo.CmdExecScalar("select IN_Date from tblTransactions where TR_TY=2 and CID=" & PTransID)
                Else
                    InDate = gAdo.CmdExecScalar("select In_Date from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
                End If
            Catch ex As Exception
                InDate = ""
            End Try
            If InDate = "" Then
                rp.SetParameterValue("In_Date", "")
            Else
                rp.SetParameterValue("In_Date", InDate)
            End If


            'Dim s() As Byte
            ''s = gAdo.CmdExecScalar("select Picture from tblPicture  as p join tblcompany as comp on Company_Pic_ID=pic_id")
            's = gAdo.CmdExecScalar("select Picture from tblPicture  where pic_id=40 ")
            ''rp.SetParameterValue("Picture2", s)
            'rp.Section2.ReportObjects("Picture2").ObjectFormat.CssClass. = s.ToString
            Try
                If AutoPrint = False Then
                    OutDate = gAdo.CmdExecScalar("select Out_Date from tblTransactions where TR_TY=2 and CID=" & PTransID)

                Else
                    OutDate = gAdo.CmdExecScalar("select Out_Date from tblTransactions where TR_TY=2 and CID=" & txtTransID.Text)
                End If
            Catch ex As Exception
                OutDate = ""
            End Try



            If OutDate = "" Then
                rp.SetParameterValue("Out_Date", "")
            Else
                rp.SetParameterValue("Out_Date", OutDate)
            End If

            If chkPrintCompayName.Checked = True Then
                rp.SetParameterValue("CompanyName", "")
                rp.SetParameterValue("CompanyAddress", "")
                rp.SetParameterValue("CompanyTel", "")
                rp.Section2.ReportObjects("Picture1").ObjectFormat.EnableSuppress = True
            Else
                rp.SetParameterValue("CompanyName", DT.Rows(0).Item("Company_Name").ToString())
                rp.SetParameterValue("CompanyAddress", DT.Rows(0).Item("Company_Address").ToString())
                rp.SetParameterValue("CompanyTel", DT.Rows(0).Item("Company_Telephone").ToString())
                rp.Section2.ReportObjects("Picture1").ObjectFormat.EnableSuppress = False
            End If


            rp.SetParameterValue("UserID", gUserFullName)
            'Print Report

            grptpth.ReportPath(rp, crv)
            For i As Integer = 0 To NOPrinted - 1
                If i = 0 Then
                    rp.Section2.ReportObjects("PicCopy").ObjectFormat.EnableSuppress = True
                Else
                    rp.Section2.ReportObjects("PicCopy").ObjectFormat.EnableSuppress = False
                    rp.SetParameterValue("EditUser", "")
                End If
                rp.PrintToPrinter(1, False, 0, 0)
            Next




        Catch ex As Exception

        End Try
    End Sub


    Public Sub PrintEmptyReport()
        Try
            Dim Nfrom As Integer = CInt(txtFrom.Text)
            Dim NTo As Integer = CInt(txtTo.Text)
            Dim ConnPath, ConnString As String
            ConnPath = Application.StartupPath & "\ConnString.txt"
            ConnString = GetConString(ConnPath)
            Dim Con As New SqlConnection(ConnString)
            gAdo.OpeningCon()
            Dim Adp As New SqlDataAdapter(" select Company_Name,Company_Address,Company_Telephone," _
                                          & " Picture from tblcompany join dbo.tblPicture on Company_Pic_ID=Pic_ID", Con)

            Dim DT As New DataTable
            Adp.Fill(DT)

            Dim rp As New CopyofrptPolicy
            Dim crv As New CrystalDecisions.Windows.Forms.CrystalReportViewer

            rp.SetParameterValue("FirstUser", "")
            rp.SetParameterValue("SecondUser", "")

            rp.SetParameterValue("CarNo", "")
            rp.SetParameterValue("CarCity", "")
            rp.SetParameterValue("TruckNo", "")
            rp.SetParameterValue("TruckCity", "")
            rp.SetParameterValue("DriverName", "")
            rp.SetParameterValue("DriverLicenceNo", "")
            rp.SetParameterValue("Customer", cboDealer.Text)
            rp.SetParameterValue("IssueTo", cboIssueTo.Text)
            rp.SetParameterValue("EmptyWieght", "")
            rp.SetParameterValue("GoodName", cboGoodType.Text)
            rp.SetParameterValue("FullWieght", "")
            rp.SetParameterValue("NetWieght", "")
            rp.SetParameterValue("RecieveType", "")
            rp.SetParameterValue("Kilo", "")
            rp.SetParameterValue("Ton", "")
            rp.SetParameterValue("Arabic", "")
            rp.SetParameterValue("In_Date", "")
            rp.SetParameterValue("Out_Date", "")

            rp.SetParameterValue("CompanyName", DT.Rows(0).Item("Company_Name").ToString())
            rp.SetParameterValue("CompanyAddress", DT.Rows(0).Item("Company_Address").ToString())
            rp.SetParameterValue("CompanyTel", DT.Rows(0).Item("Company_Telephone").ToString())
            rp.Section2.ReportObjects("Picture1").ObjectFormat.EnableSuppress = False

            rp.SetParameterValue("UserID", gUserO.Name)

            'Print Report
            rp.SetParameterValue("EditUser", "")

            For index As Integer = Nfrom To NTo



                rp.SetParameterValue("TransNo", index)

                grptpth.ReportPath(rp, crv)
                For i As Integer = 0 To NOPrinted - 1
                    If i = 0 Then
                        rp.Section2.ReportObjects("PicCopy").ObjectFormat.EnableSuppress = True
                    Else
                        rp.Section2.ReportObjects("PicCopy").ObjectFormat.EnableSuppress = False
                    End If
                    rp.PrintToPrinter(1, False, 0, 0)
                Next


            Next
        Catch ex As Exception

        End Try

    End Sub
    Public Sub CopyofPrintNewReport()
        'NOPrinted = 1
        Try

            Dim ConnPath, ConnString As String
            ConnPath = Application.StartupPath & "\ConnString.txt"
            ConnString = GetConString(ConnPath)
            Dim Con As New SqlConnection(ConnString)
            gAdo.OpeningCon()
            Dim Adp As New SqlDataAdapter(" select Company_Name,Company_Address,Company_Telephone," _
                                          & " Picture from tblcompany join dbo.tblPicture on Company_Pic_ID=Pic_ID", Con)

            Dim DT As New DataTable
            Adp.Fill(DT)

            Dim rp As New CopyofrptPolicy
            Dim crv As New CrystalDecisions.Windows.Forms.CrystalReportViewer
            'rp.Refresh()
            'rp.SetDataSource(DT)
            'rp.PrintOptions.PrinterName = "\\BB\HP Deskjet F300 series"

            Dim NetWeight As String

            If Val(lblScndWeight.Text) < 1 Then
                NetWeight = 0
            Else
                NetWeight = Abs(CDec(Val(lblFrstWeight.Text.Trim)) - CDec(Val(lblScndWeight.Text.Trim)))
            End If
            Dim txtTrans As Integer
            txtTrans = Val(txtTransID.Text)
            Dim Adp1 As New SqlDataAdapter("select u1.[user_name], u2.[user_name] from tblTransactions as trans" _
                                         & " left join tblUser as U1 on trans.First_Weight_User_ID=U1.[User_ID] " _
                                         & " left join tblUser as U2 on trans.Second_Weight_User_ID=U2.[User_ID]" _
                                        & " where tr_ty =2 and CID='" & txtTrans & "'", Con)

            Dim DT1 As New DataTable
            Adp1.Fill(DT1)
            Dim FirstUser, SecondUser As String
            If DT1.Rows.Count > 0 Then
                FirstUser = DT1.Rows(0).Item(0).ToString()
            Else
                FirstUser = ""

            End If
            If DT1.Rows.Count > 0 Then
                SecondUser = DT1.Rows(0).Item(1).ToString()
            Else
                SecondUser = ""

            End If
            rp.SetParameterValue("FirstUser", FirstUser)
            rp.SetParameterValue("SecondUser", SecondUser)

            ' ''Dim dadap As New SqlDataAdapter("Select U.User_Name from tblUser U JOIN tblTransactions T ON U.User_ID = T.Slip_Charged_User_ID WHERE CID='" & txtTrans & "' AND TR_TY=2", Con)

            ' ''Dim DataT As New DataTable
            ' ''dadap.Fill(DataT)

            ' ''If DataT.Rows.Count > 0 Then
            ' ''    rp.SetParameterValue("EditUser", "تعديل الوزان: " & DataT.Rows(0).Item(0))
            ' ''    'rp.Section2.ReportObjects("EditUserLabel").ObjectFormat.EnableSuppress = False
            ' ''Else

            rp.SetParameterValue("EditUser", "")
            ' ''End If

            If CboRecieveType.Text = "إسكندرية" Or CboRecieveType.Text = "" Then
                PRecieveType = ""
            ElseIf CboRecieveType.Text = "العامرية" Then
                PRecieveType = "العامرية"
            End If


            If AutoPrint = False Then
                rp.SetParameterValue("TransNo", PTransID)
                rp.SetParameterValue("CarNo", PCarNo)
                rp.SetParameterValue("CarCity", PCarCity)
                rp.SetParameterValue("TruckNo", PTruckNo)
                rp.SetParameterValue("TruckCity", PTruckCity)
                rp.SetParameterValue("DriverName", PDriverName)
                rp.SetParameterValue("DriverLicenceNo", PDriverLicence)
                rp.SetParameterValue("Customer", PDealerName)
                rp.SetParameterValue("IssueTo", PIssueto)
                rp.SetParameterValue("EmptyWieght", PFisrtScale)
                rp.SetParameterValue("GoodName", PGoodName)
                rp.SetParameterValue("FullWieght", PSecondScale)
                rp.SetParameterValue("NetWieght", PNetScale)
                rp.SetParameterValue("RecieveType", PRecieveType)
                If Val(PNetScale.Length) > 3 Then
                    Dim kilo, ton As String
                    kilo = PNetScale.Substring(PNetScale.Length - 3)
                    ton = PNetScale.Substring(0, PNetScale.Length - 3)
                    rp.SetParameterValue("Kilo", kilo)
                    rp.SetParameterValue("Ton", ton)
                Else
                    rp.SetParameterValue("Kilo", PNetScale)
                    rp.SetParameterValue("Ton", "")
                End If
                rp.SetParameterValue("Arabic", PArabic)
            Else
                Dim strtransID, strCarNo, strCarCity, strTruckNo, strTruckCity, strDriver, strLicenceNo, strDealer, strIssueTo, strGoodType As String
                strtransID = txtTransID.Text
                strCarNo = txtCarNo.Text
                strCarCity = cboCarCity.Text
                strTruckNo = cboTruckNo.Text
                strTruckCity = cboTruckCity.Text
                strDriver = cboDriver.Text
                strLicenceNo = txtLicenceNo.Text

                strDealer = cboDealer.Text
                strIssueTo = cboIssueTo.Text
                strGoodType = cboGoodType.Text
                rp.SetParameterValue("TransNo", strtransID)
                rp.SetParameterValue("CarNo", strCarNo)
                rp.SetParameterValue("CarCity", strCarCity)
                rp.SetParameterValue("TruckNo", strTruckNo)
                rp.SetParameterValue("TruckCity", strTruckCity)
                rp.SetParameterValue("DriverName", strDriver)
                rp.SetParameterValue("DriverLicenceNo", strLicenceNo)
                rp.SetParameterValue("Customer", strDealer)
                rp.SetParameterValue("IssueTo", strIssueTo)
                rp.SetParameterValue("GoodName", strGoodType)
                rp.SetParameterValue("EmptyWieght", CDec(Val(lblFrstWeight.Text.Trim)))
                rp.SetParameterValue("FullWieght", CDec(Val(lblScndWeight.Text.Trim)))
                rp.SetParameterValue("NetWieght", NetWeight)
                rp.SetParameterValue("RecieveType", PRecieveType)
                If Val(NetWeight.Length) > 3 Then
                    Dim kilo, ton As String
                    kilo = NetWeight.Substring(NetWeight.Length - 3)
                    ton = NetWeight.Substring(0, NetWeight.Length - 3)
                    rp.SetParameterValue("Kilo", kilo)
                    rp.SetParameterValue("Ton", ton)
                Else
                    rp.SetParameterValue("Kilo", NetWeight)
                    rp.SetParameterValue("Ton", "")
                End If
                rp.SetParameterValue("Arabic", HANY(Val(lblNetWeight.Text.Trim), "EGYPT"))
            End If

            Dim InDate, OutDate As String
            Try
                If AutoPrint = False Then
                    InDate = gAdo.CmdExecScalar("select IN_Date from tblTransactions where TR_TY=2 and CID=" & PTransID)
                Else
                    InDate = gAdo.CmdExecScalar("select In_Date from tblTransactions where TR_TY=2 and CID=" & txtTrans)
                End If
            Catch ex As Exception
                InDate = ""
            End Try
            If InDate = "" Then
                rp.SetParameterValue("In_Date", "")
            Else
                rp.SetParameterValue("In_Date", InDate)
            End If


            'Dim s() As Byte
            ''s = gAdo.CmdExecScalar("select Picture from tblPicture  as p join tblcompany as comp on Company_Pic_ID=pic_id")
            's = gAdo.CmdExecScalar("select Picture from tblPicture  where pic_id=40 ")
            ''rp.SetParameterValue("Picture2", s)
            'rp.Section2.ReportObjects("Picture2").ObjectFormat.CssClass. = s.ToString
            Try
                If AutoPrint = False Then
                    OutDate = gAdo.CmdExecScalar("select Out_Date from tblTransactions where TR_TY=2 and CID=" & PTransID)

                Else
                    OutDate = gAdo.CmdExecScalar("select Out_Date from tblTransactions where TR_TY=2 and CID=" & txtTrans)
                End If
            Catch ex As Exception
                OutDate = ""
            End Try



            If OutDate = "" Then
                rp.SetParameterValue("Out_Date", "")
            Else
                rp.SetParameterValue("Out_Date", OutDate)
            End If

            If chkPrintCompayName.Checked = True Then
                rp.SetParameterValue("CompanyName", "")
                rp.SetParameterValue("CompanyAddress", "")
                rp.SetParameterValue("CompanyTel", "")
                rp.Section2.ReportObjects("Picture1").ObjectFormat.EnableSuppress = True
            Else
                rp.SetParameterValue("CompanyName", DT.Rows(0).Item("Company_Name").ToString())
                rp.SetParameterValue("CompanyAddress", DT.Rows(0).Item("Company_Address").ToString())
                rp.SetParameterValue("CompanyTel", DT.Rows(0).Item("Company_Telephone").ToString())
                rp.Section2.ReportObjects("Picture1").ObjectFormat.EnableSuppress = False
            End If
            'Print Report

            grptpth.ReportPath(rp, crv)
            'For i As Integer = 0 To NOPrinted
            '    If i = 0 Then
            'rp.Section2.ReportObjects("PicCopy").ObjectFormat.EnableSuppress = True
            'Else
            rp.Section2.ReportObjects("PicCopy").ObjectFormat.EnableSuppress = False
            'End If
            'Next

            rp.SetParameterValue("UserID", gUserFullName)

            rp.PrintToPrinter(1, False, 0, 0)
            'Next





        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Try
            AutoPrint = True
            PrintNewReport()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSlip0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSlip0.Click
        'Eng Ahmad fawzy
        gTransactionO.Slip_Rate = 0
        btnSlip0.BackColor = Color.Yellow
        btnSlip1.BackColor = Color.DarkGray
        btnSlip2.BackColor = Color.DarkGray
        btnSlip3.BackColor = Color.DarkGray

    End Sub

    Private Sub btnSlip1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSlip1.Click
        'Eng Ahmad fawzy
        gTransactionO.Slip_Rate = btnSlip1.Text
        btnSlip0.BackColor = Color.DarkGray
        btnSlip1.BackColor = Color.Yellow
        btnSlip2.BackColor = Color.DarkGray
        btnSlip3.BackColor = Color.DarkGray

    End Sub

    Private Sub btnSlip2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSlip2.Click
        'Eng Ahmad fawzy
        gTransactionO.Slip_Rate = btnSlip2.Text
        btnSlip0.BackColor = Color.DarkGray
        btnSlip1.BackColor = Color.DarkGray
        btnSlip2.BackColor = Color.Yellow
        btnSlip3.BackColor = Color.DarkGray
    End Sub

    Private Sub btnSlip3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSlip3.Click
        'Eng Ahmad fawzy
        gTransactionO.Slip_Rate = btnSlip3.Text
        btnSlip0.BackColor = Color.DarkGray
        btnSlip1.BackColor = Color.DarkGray
        btnSlip2.BackColor = Color.DarkGray
        btnSlip3.BackColor = Color.Yellow
    End Sub

    Private Sub tmrCameraNow_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrCameraNow.Tick

    End Sub

    Private Sub rdbCarsInside_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCarsInside.CheckedChanged
        Try

            Dim SelectedScales As String = ""

            For Each ItemChecked In chklstBoxScale.CheckedItems
                If SelectedScales = "" Then
                    SelectedScales = ItemChecked.item("Scale_ID").ToString
                Else
                    SelectedScales = SelectedScales & " , " & ItemChecked.item("Scale_ID").ToString
                End If
            Next
            TodayTrans(" where  TR_TY =2 and out_date is null and  Trans.Issue_To_id not in(17,21) and First_Weigth_Scale_ID in (" & SelectedScales & ")")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub lblTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTime.Click

    End Sub

    Private Sub chkMaxWeight_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCustomWeight.CheckedChanged

        If NoChangeCheck Then

        Else

            If chkCustomWeight.Checked = False Then
                txtCustomWeight.Text = ""
                txtCustomWeight.Enabled = False
            Else
                txtCustomWeight.Text = ""
                txtCustomWeight.Enabled = True
            End If

        End If

    End Sub

    Private Sub txtMaxWeight_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomWeight.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If

    End Sub

    ''''''
    'Private Sub pnlNewTransaction_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlNewTransaction.Paint

    'End Sub

    Private Sub cboTruckNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTruckNo.TextChanged
        Select Case sender.name
            Case cboTruckNo.Name

                If TakeAction Then
                    Try
                        Dim C As Integer
                        Dim result As Integer
                        C = gAdo.CmdExecScalar("select count(Car_Info_ID)from tblCarInfo where TruckBoard_No ='" & cboTruckNo.Text & "'")
                        If C > 0 Then
                            result = gAdo.CmdExecScalar("SELECT ISNULL ((SELECT TruckBoard_City_ID from tblCarInfo where Car_Info_ID = (SELECT Car_Info_ID from tblTransactions WHERE Trans_ID =( SELECT MAX(Trans_ID) FROM tblTransactions WHERE (Car_Info_ID) IN (SELECT Car_Info_ID FROM tblCarInfo WHERE TruckBoard_No ='" & cboTruckNo.Text & "')))),0)")

                            If result = 0 Then
                                cboTruckCity.SelectedValue = gAdo.CmdExecScalar("SELECT TruckBoard_City_ID From tblCarInfo WHERE TruckBoard_No  ='" & cboTruckNo.Text & "'")
                            Else
                                cboTruckCity.SelectedValue = result
                            End If


                        Else
                            cboTruckCity.SelectedIndex = 0
                        End If
                    Catch ex As Exception
                    End Try
                End If



                'Get Truck_City
                ''''gMethods.IsTruckExist(cboTruckNo, cboTruckCity)
                'To know if the truck_NO is (used with another Car or not used with another Car or New )
                ''''Validate_Board_NO(cboTruckNo)
            Case Is = cboDriver.Name
                'Get Licence_No
                gMethods.IsDriverExist(cboDriver, txtLicenceNo)
                'To know if the Driver is (used with another Car or not used with another Car or New )
                Validate_Board_NO(cboDriver)
        End Select

    End Sub

    'Private Sub cboGoodType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboGoodType.SelectedIndexChanged

    '    If TakeAction Then
    '        Me.cboGoodType.Width = 350
    '        Me.cboGoodType.Location = New System.Drawing.Point(433, 56)
    '    End If



    'End Sub

    Private Sub cboGoodType_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboGoodType.MouseHover
        Me.ToolTip1.SetToolTip(Me.cboGoodType, cboGoodType.Text)
    End Sub



    Private Sub cboDriver_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDriver.TextChanged
        gMethods.IsDriverExist(cboDriver, txtLicenceNo)
    End Sub





    Private Sub cboDriver_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboDriver.KeyUp
        gMethods.IsDriverExist(cboDriver, txtLicenceNo)
    End Sub


    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        NOPrinted = 5
        Try

            Dim msgResult As MsgBoxResult
            msgResult = MsgBox("هـل ترغـب فى الطبــــاعه ؟", MsgBoxStyle.OkCancel, "الطبــــاعه")
            If msgResult = MsgBoxResult.Ok Then
                If txtTransID.Text = "" Then
                    MsgBox("لا توجد حركه لطباعتها")
                    Exit Sub
                End If
                AutoPrint = True
                Dim Thread As New System.Threading.Thread(AddressOf PrintEmptyReport)
                Control.CheckForIllegalCrossThreadCalls = False
                Thread.Start()
            Else

            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub txtFrom_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFrom.KeyPress, txtTo.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub pnlNewTransaction_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlNewTransaction.Paint

    End Sub
    Dim dsSlipRate As New DataSet
    Public Sub New(ByVal CID As Integer)

        dsSlipRate = gAdo.SelectData("select slip_rate from tblSlip order by slip_rate", False)
        ' This call is required by the Windows Form Designer.

        If CID = 0 Then
            InitializeComponent()
        Else
            gTransID = CID
            LoadData()
        End If
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub serl_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) 'Handles serl.DataReceived
        ReceivedText(serl.ReadExisting())    'Automatically called every time a data is received at the serialPort

    End Sub
    Private Sub ReceivedText(ByVal [text] As String)
        'compares the ID of the creating Thread to the ID of the calling Thread
        If Me.lblFrstWeight.InvokeRequired Then
            Dim x As New SetTextCallback(AddressOf ReceivedText)
            Me.Invoke(x, New Object() {(text)})
        Else
            Me.lblFrstWeight.Text &= [text]
        End If
    End Sub

    Private Delegate Sub SetTextCallback(ByVal [text] As String)



    ''' Elsaman 8/12/2019 '''
    Private Sub chkPrintCompayName_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPrintCompayName.MouseHover
        Me.chkPrintCompayName.Text = " ألغاء اسم الشركة من الطباعة "
    End Sub
    ''' Elsaman 8/12/2019 '''
    Private Sub chkPrintCompayName_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPrintCompayName.MouseLeave
        Me.chkPrintCompayName.Text = ""
    End Sub

    ''' Elsaman 9/12/2019 '''
    Public Sub DisableControls(ByVal x As Integer)
        If x = 0 Then
            ' فى حالة الوزنة الأولى
            txtCarNo.Enabled = True
            cboCarCity.Enabled = True
            cboDriver.Enabled = True
            txtLicenceNo.Enabled = True
            cboTruckNo.Enabled = True
            cboTruckCity.Enabled = True
        Else
            ' فى حالة أنه وزت وزتة ثانية

            txtCarNo.Enabled = False
            cboCarCity.Enabled = False
            cboDriver.Enabled = False
            txtLicenceNo.Enabled = False



            cboTruckNo.Enabled = False
            cboTruckCity.Enabled = False
        End If


        txtTransID.Enabled = False
        'btnSlip0.Enabled = False
        'btnSlip1.Enabled = False
        'btnSlip2.Enabled = False
        'btnSlip3.Enabled = False



        chkPrintCompayName.Enabled = False
        'txtNotes.Enabled = False
        'If (Val(lblScndWeight.Text.Trim()) > 0) Then
        '    cboDriver.Enabled = False
        '    txtLicenceNo.Enabled = False
        '    btnSlip0.Enabled = False
        '    btnSlip1.Enabled = False
        '    btnSlip2.Enabled = False
        '    btnSlip3.Enabled = False
        'Else
        '    cboDriver.Enabled = True
        '    txtLicenceNo.Enabled = True
        '    btnSlip0.Enabled = True
        '    btnSlip1.Enabled = True
        '    btnSlip2.Enabled = True
        '    btnSlip3.Enabled = True
        'End If

        cboDealer.Enabled = False
        cboContractor.Enabled = False
        cboIssueTo.Enabled = False
        CboRecieveType.Enabled = False
        'chkCustomWeight.Enabled = False
        txtCustomWeight.Enabled = False
        cboGoodType.Enabled = False
        btnEditFirstScale.Enabled = False
        btnFirstScale.Enabled = False
        'btnSecondScale.Enabled = False
        Button8.Enabled = False
        cboLocation.Enabled = False

    End Sub

    ''' Elsaman 9/12/2019 '''
    Public Sub EnabledControls()
        txtTransID.Enabled = True
        btnSlip0.Enabled = True
        btnSlip1.Enabled = True
        btnSlip2.Enabled = True
        btnSlip3.Enabled = True
        txtCarNo.Enabled = True
        cboCarCity.Enabled = True
        cboTruckNo.Enabled = True
        cboTruckCity.Enabled = True
        chkPrintCompayName.Enabled = True
        txtNotes.Enabled = True
        cboDriver.Enabled = True
        txtLicenceNo.Enabled = True
        cboDealer.Enabled = True
        cboContractor.Enabled = True
        cboIssueTo.Enabled = True
        CboRecieveType.Enabled = True
        chkCustomWeight.Enabled = True
        txtCustomWeight.Enabled = True
        cboGoodType.Enabled = True
        btnEditFirstScale.Enabled = True
        btnFirstScale.Enabled = True
        btnSecondScale.Enabled = False
        Button8.Enabled = True
        btnSaveAndPrint.Enabled = True

    End Sub

    Sub PrsentScale()
        ''' 2/1/2019 ''''
        '''' Elsaman ''''
        '' To Fill By Choose Scale

        Scale = ""
        If chklstBoxScale.CheckedItems.Count = 0 Then
            MsgBox(" الرجاء اختيار ميزان  ")
            Exit Sub
        Else
            Dim ItemChecked As Object
            For Each ItemChecked In chklstBoxScale.CheckedItems
                If Scale = "" Then
                    Scale = ItemChecked.item("Scale_ID").ToString
                Else
                    Scale = Scale & " , " & ItemChecked.item("Scale_ID").ToString
                End If
            Next
            'a = a & " , " & ItemChecked.item("Good_ID").ToString
            Scale = " and First_Weigth_Scale_ID in ( " & Scale & " )"

        End If
    End Sub

    Private Sub cboGoodType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboGoodType.SelectedIndexChanged

        CalculateDailyQuaota()

    End Sub

    Sub CalculateDailyQuaota()
        ''' Elsaman 5/1/2020 '''
        Try
            If cboGoodType.SelectedValue Is Nothing Or cboDealer.SelectedValue Is Nothing Then
                lblTotalDQ.Text = 0
                lblTotalWeightTrans.Text = 0
                lblRemainderFromQuota.Text = 0
                Exit Sub
            End If


            '' Variable From tblShift ====> " وقت نهاية الوردية "
            Dim Shift_End_Time As DateTime
            Shift_End_Time = gAdo.CmdExecScalar(CStr("Select RIGHT(CONVERT(VARCHAR, Shift_End_Time, 100),15) as Time From tblShift where Shift_End_Date = 1"))

            '' Date OF Weight  ===> " تاريخ الوزن "
            Dim WeightTime As DateTime = TimeOfDay

            Dim DateSearch As DateTime = Now
            If WeightTime < Shift_End_Time Then
                DateSearch = Now.AddDays(-1)
            Else
                DateSearch = Now
            End If
            DateSearch = DateSearch.Date.ToShortDateString

            Dim SQLST As String = ""
            SQLST = "Select DQ.DailyQuotaID From tblDailyQuotaWithOutContract DQ Join tblDailyQuotaWOContractGoods DG on(DQ.DailyQuotaID=DG.DailyQuotaID) " & _
                    " where  DQ.CustomerID=" & cboDealer.SelectedValue & " and DG.GoodID='" & cboGoodType.SelectedValue & "' /*and DQ.QuotaDate<=convert(Date,'" & DateSearch & "')*/ and DQ.finish=0"
            If (gAdo.IFExists(SQLST)) Then

                lblTotalDQ.Text = gAdo.CmdExecScalar("Set Dateformat dmy Select sum(DQ.Quantity*1000) From tblDailyQuotaWithOutContract DQ " & _
                                                         " join  tblDailyQuotaWOContractGoods DG on(DQ.DailyQuotaID=DG.DailyQuotaID) " & _
                                                         " where DQ.CustomerID=" & cboDealer.SelectedValue & " and DG.GoodID='" & cboGoodType.SelectedValue & "'  /*and DQ.QuotaDate<=convert(Date,'" & DateSearch & "')*/  and DQ.finish=0")

                lblTotalWeightTrans.Text = gAdo.CmdExecScalar("Set Dateformat dmy Select isnull(sum(T.Quantity),0) as TotalweightTrans " & _
                                                              " From  tblTransQuotaWithOutContract T Join tblDailyQuotaWithOutContract DQ on(T.QuotaID=DQ.DailyQuotaID /*and DQ.QuotaDate<=convert(Date,'" & DateSearch & "')*/) " & _
                                                              " join  tblDailyQuotaWOContractGoods DG on(DQ.DailyQuotaID=DG.DailyQuotaID) where DQ.CustomerID=" & cboDealer.SelectedValue & " and DG.GoodID='" & cboGoodType.SelectedValue & "'  /*and DQ.QuotaDate<=convert(Date,'" & DateSearch & "')*/ and DQ.finish=0")

            Else

                Dim WhereCondition As String = ""
                WhereCondition = " and TR_TY =2 and  Out_Date is not null and  CONVERT(datetime, Out_Date) <= '" & gMethods.GetShifName_And_SearchDate(True)(1) & "'"
                '" '" & gMethods.GetShifName_And_SearchDate(True)(0) & "' and " & _
                'WhereCondition = " Where  TR_TY=2 and  Out_Date is not null and  CONVERT(datetime, Out_Date) BETWEEN  '" & gMethods.GetShifName_And_SearchDateFT(True, DTRepot.Value.Date, dtTo.Value.Date)(0) & "' and " & _
                '      "'" & gMethods.GetShifName_And_SearchDateFT(True, DTRepot.Value.Date, dtTo.Value.Date)(1) & "'  "

                '' For Total Daily Qouta

                lblTotalDQ.Text = gAdo.CmdExecScalar(" set dateformat dmy select isnull(sum(Quota)*1000,0)-isnull(sum(OutGoingQty)*1000,0) as TotalQuota from tblDailyQuota " & _
                                                     " join tblDebitNote  on tblDailyQuota.DebitNoteID  = tblDebitNote .DebitNoteID " & _
                                                     " join tblContracts on tblContracts.ContractID = tblDebitNote.ContractID " & _
                                                     " join tblContractDet on tblContracts.ContractID=tblContractDet.ContractID " & _
                                                     " join tblGood on tblGood.Good_ID = tblContractDet.GoodID " & _
                                                     " join tblDealer on tblDealer.Dealer_ID = tblContracts.CustomerID " & _
                                                     " where tblGood.Good_ID = " & cboGoodType.SelectedValue & " and tblDealer.Dealer_ID = " & cboDealer.SelectedValue & " /*and tblDailyQuota.DateQuota <= '" & Date.Now.Date & "'*/ and tblDailyQuota.Finished=0")





                '' Total Weight OF Transaction
                'lblTotalWeightTrans.Text = gAdo.CmdExecScalar(" set dateformat dmy select isnull(sum(Second_Weight - First_Weigth),0) as TotalWeightOFTrans from tblTransactions where Good_ID = " & cboGoodType.SelectedValue & _
                '                                              " and Dealer_ID = " & cboDealer.SelectedValue & WhereCondition)
                lblTotalWeightTrans.Text = gAdo.CmdExecScalar("  set dateformat dmy select isnull(sum(tblTransQuota.Quantity),0) as TotalWeightOFTrans from tblTransQuota " & _
                                                              " join tblTransactions on tblTransQuota.TransID=tblTransactions.Trans_ID " & _
                                                              " join tblDailyQuota on tblTransQuota .QuotaID =tblDailyQuota .QuotaID 	/*and DateQuota <=convert(Date,'" & DateSearch.ToShortDateString() & "')*/ " & _
                                                              " join tblDebitNote on tblDailyQuota .DebitNoteID =tblDebitNote .DebitNoteID " & _
                                                              " where tblTransactions.Dealer_ID='" & cboDealer.SelectedValue.ToString() & "' and tblDailyQuota.Finished=0 and tblDebitNote.ContractID in(select tblContractDet.ContractID   from  tblContractDet  join tblContracts on tblContractDet .ContractID =tblContracts .ContractID  where CustomerID ='" & cboDealer.SelectedValue.ToString() & "' and GoodID = '" + cboGoodType.SelectedValue.ToString() + "') " & WhereCondition)

                '' Remainder OF Daily Quota -  Total Weight OF Transaction
            End If
            lblRemainderFromQuota.Text = lblTotalDQ.Text - lblTotalWeightTrans.Text

            Dim DIndex As Integer
            DIndex = lblTotalDQ.Text.LastIndexOf(".")
            Dim TQ As String
            TQ = lblTotalDQ.Text.Substring(0, DIndex)

            lblTotalDQ.Text = TQ.Substring(0, TQ.Length - 3) & "," & TQ.Substring(TQ.Length - 3, 3)


        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboDealer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDealer.SelectedIndexChanged
        CalculateDailyQuaota()
    End Sub

    Private Sub TimerControlScale_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerControlScale.Tick
        'Try
        '    Dim Lastweight As Decimal
        '    Lastweight = gAdo.CmdExecScalar("Select Isnull(LastWeight,0) From tblScale where Scale_ID=" & ScaleNo)
        '    If (Lastweight <> 0) Then
        '        Dim CurWeight = ReadScale()
        '        If (Val(CurWeight) >= 0 And Val(CurWeight) <= 150) Then
        '            CurWeight = "0"
        '        End If
        '        gAdo.CmdExec("UpDate tblScale set LastWeight=" & CurWeight & " where Scale_ID=" & ScaleNo)
        '    End If


        'Catch ex As Exception

        'End Try
    End Sub

    Function ReadScale() As String
        Dim CurWeight As String = "0"
        Dim RecievedData As String = ""
        Try

            If serl.IsOpen Then
                ' Don't do any thing
            Else

                With serl
                    .PortName = "COM1"
                    .BaudRate = 9600
                    .DataBits = 8
                    .StopBits = StopBits.One
                    .Parity = Parity.None
                    '.Handshake = Handshake.None
                End With
                serl.Open()
            End If
        Catch ex As Exception

        End Try

        If intCounteading < 2 Then
            intCounteading += 1


            Dim dd As Integer


            Try
                If ScaleNo = 2 Then

                    RecievedData = serl.ReadExisting
                    If RecievedData.LastIndexOf("A") - 14 > 0 Then
                        CurWeight = Val(RecievedData.Substring(RecievedData.LastIndexOf("A") - 14, 6))

                    Else

                        dd = RecievedData.LastIndexOf("A")
                        CurWeight = Val(RecievedData.Substring(RecievedData.LastIndexOf("A") + 3, 6))

                    End If
                Else

                    Dim str As String

                    str = serl.ReadExisting
                    dd = str.LastIndexOf(")")

                    CurWeight = Val(str.Substring(str.LastIndexOf(")") - 16, 11))

                End If

            Catch ex As Exception

            End Try


            If serl.IsOpen Then
                serl.DiscardOutBuffer()

                serl.Close()

            Else
                ' Don't do any thing
            End If

        Else
            intCounteading = 0
        End If

        Return CurWeight

    End Function

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If DataGridView1.RowCount > 0 Then
            'txtTransID.Text = CStr(DataGridView1.CurrentRow.Cells(0).Value)
            SwitchMode(txtCarNo, cboCarCity, cboTruckNo, cboTruckCity, cboDriver, txtLicenceNo, _
                       cboDealer, cboIssueTo, cboGoodType)
            txtCarNo.Text = CStr(DataGridView1.CurrentRow.Cells(1).Value)
            gTransID = CStr(DataGridView1.CurrentRow.Cells(0).Value)
            txtTransID.Text = CStr(DataGridView1.CurrentRow.Cells(0).Value)
            ID = CStr(DataGridView1.CurrentRow.Cells(0).Value)
            LoadData()
            'txtCarTimer_Tick(sender, e)
            '13-11-2013
            'lblInDate.Text = CDate(DataGridView1.CurrentRow.Cells(4).Value) & vbCrLf & CDate(DataGridView1.CurrentRow.Cells(5).Value)

            'If IsDBNull((DataGridView1.CurrentRow.Cells(6).Value)) = False Or IsDBNull((DataGridView1.CurrentRow.Cells(7).Value)) = False Then
            '    lblOutDate.Text = CDate(DataGridView1.CurrentRow.Cells(6).Value) & vbCrLf & CDate(DataGridView1.CurrentRow.Cells(7).Value)
            'Else
            '    lblOutDate.Text = ""
            'End If
        End If
        Me.pnlTransactions.SendToBack()
        btnSwitchView.Text = "حركات اليوم"
        rdbCarsInside.Checked = False
    End Sub
    Public Sub LoadData()

        Try
            'To Cancel Checking Event
            NoChangeCheck = True

            Dim DT As DataTable
            Dim DS As New DataSet


            DS = gAdo.SelectData("select Trans_ID,In_Date,Out_Date,CustomWeight,isnull(First_Weight_User_ID,'') as FirstUser,isnull(Second_Weight_User_ID,'') as SecondUser,In_Shift_ID,In_Shift_Date," _
                                        & "T.Car_Info_ID,T.Dealer_ID,T.Issue_To_ID,First_Weigth," _
                                        & " First_Weigth_Scale_ID, First_Weight_User_ID, Second_Weight, " _
                                        & " Second_Weight_User_ID, Second_Weight_Scale_ID, Good_ID, " _
                                        & " Slip_Rate, Slip_Charged_Date, Slip_Charged_User_ID, T.Driver_ID, " _
                                        & " T.Car_ID, CID, ManualEdit, First_Weight_IsEmpty, Second_Weight_IsEmpty" _
                                        & " ,CarBoard_No,TruckBoard_No,Driver_Name,Issue_To_Name,CustomWeight,Bill," _
                                        & " Driver_License_No,TR_TY,Port_CD,CarBoard_City_ID,TruckBoard_City_ID" _
                                        & " from tblTransactions as T join tblCar as C on T.Car_ID=C.Car_ID" _
                                        & " join tblCarInfo as CI on T.Car_Info_ID=CI.Car_Info_ID" _
                                        & " join tblDriver as D on T.Driver_ID=D.Driver_ID" _
                                        & " join tblIssueTo as I on T.Issue_To_ID=I.Issue_To_ID" _
                                        & " where TR_TY =2 and  CID = " & gTransID, False)
            DT = DS.Tables(0)
            FrmRefresh()
            txtTransID.Text = DT.Rows(0).Item("cid").ToString()
            txtCarNo.Text = DT.Rows(0).Item("CarBoard_No").ToString()
            cboDriver.SelectedValue = DT.Rows(0).Item("Driver_ID").ToString()
            cboDealer.SelectedValue = DT.Rows(0).Item("Dealer_ID").ToString()
            cboIssueTo.SelectedValue = DT.Rows(0).Item("Issue_To_ID").ToString()
            cboCarCity.SelectedValue = DT.Rows(0).Item("CarBoard_City_ID").ToString()
            cboGoodType.SelectedValue = DT.Rows(0).Item("Good_ID").ToString()
            lblInDate.Text = DT.Rows(0).Item("In_Date")
            lblFrstWeight.Text = DT.Rows(0).Item("First_Weigth")
            lblScndWeight.Text = DT.Rows(0).Item("Second_Weight")
            cboTruckNo.SelectedValue = DT.Rows(0).Item("Car_Info_ID")
            cboTruckCity.SelectedValue = DT.Rows(0).Item("TruckBoard_City_ID")
            lblInDate.Text = DT.Rows(0).Item("In_Date")
            '''' Elsaman 08/01/2019 ''''
            '''' هذا الكود لاجل تثبيت المقاول لكي يثبت المستخدم المقاول طوال اليوم ولايحتاج الى ادخاله مره اخرى ''''
            Dim intContractor_ID As Integer
            intContractor_ID = gAdo.CmdExecScalar("select Contractor_ID from tblTransactions where CID " & _
                                                  " in (select isnull( max (CID),0)from tblTransactions where TR_TY=2) and TR_TY = 2")
            cboContractor.SelectedValue = intContractor_ID
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If DT(0).Item("CustomWeight").ToString = "" Or DT(0).Item("CustomWeight").ToString = "0.00" Then
                txtCustomWeight.Text = ""
                chkCustomWeight.Checked = False
                'txtCustomWeight.Enabled = False
            Else
                txtCustomWeight.Text = DT(0).Item("CustomWeight").ToString
                chkCustomWeight.Checked = True
                txtCustomWeight.Enabled = True
            End If

            Try
                gUserO.ID = DT(0).Item("FirstUser").ToString
                gUserO.Refresh()
                lblFirstUser.Text = gUserO.Name
            Catch ex As Exception
                lblFirstUser.Text = ""
            End Try

            Try
                gUserO.ID = DT(0).Item("SecondUser").ToString
                If gUserO.ID = 0 Then
                    lblSecondUser.Text = ""
                Else
                    gUserO.Refresh()
                    lblSecondUser.Text = gUserO.Name
                End If

            Catch ex As Exception
                lblSecondUser.Text = ""
            End Try
            gTransactionO.Slip_Rate = DT.Rows(0).Item("Slip_Rate").ToString()
            If DT.Rows(0).Item("First_Weight_IsEmpty").ToString = True Then
                radFrstEmpty.Checked = True
            Else
                radFrstFull.Checked = True
            End If



            Dim ReceiveType As Integer
            ReceiveType = DT.Rows(0).Item("first_weight_user_id").ToString

            If ReceiveType = 1 Then
                CboRecieveType.Text = "إسكندرية"
            ElseIf ReceiveType = 2 Then
                CboRecieveType.Text = "العامرية"
            Else
                CboRecieveType.Text = ""
            End If

            ColorSlipRateButtons()




            If IsDBNull(DT.Rows(0).Item("Out_Date")) Then
                lblOutDate.Text = Now
                btnFirstScale.Enabled = False
                btnEditFirstScale.Enabled = True
                btnSecondScale.Enabled = True
                SIsUpdate = True
                'By Shady 16-07-2020
                DisableControls(0)
            Else
                lblOutDate.Text = DT.Rows(0).Item("Out_Date")
                btnSecondScale.Enabled = False
                btnFirstScale.Enabled = False
                btnEditFirstScale.Enabled = False
                SIsUpdate = False
                'By Shady 16-07-2020
                DisableControls(1)
            End If


            If Val(DT.Rows(0).Item("First_Weigth").ToString()) < 100 Then
                'btnSetSecondWeight.Enabled = False
                btnSecondScale.Enabled = False
            Else
                btnSetSecondWeight.Enabled = True
                btnSecondScale.Enabled = True
            End If

            NoChangeCheck = False
        Catch ex As Exception
            MsgBox("Error Loading")
            btnNew.PerformClick()
        End Try

    End Sub

    Private Sub btnGotoTransaction_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub
End Class