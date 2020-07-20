Imports System.Data.SqlClient
Imports System.IO.Ports
Imports System.Transactions
Imports System.IO
Imports System.Net.Mail

Public Class frmEdit

    Dim CarNo, GoodID, Notes As String
    Dim DealerID, DriverID, IssueToID, FirstScale, SecondScale, ReceiveType, UpDateTransID As Integer
    Dim msgResult As MsgBoxResult
    Dim SQLTrans As SqlTransaction
    Dim CarID, CarInfoID, OutDate, Slip_Rate As String
    Dim Thread As System.Threading.Thread

    Private Sub frmEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", "TblDealer where Type=1 or dealer_ID=1  ORDER BY Dealer_Name", cboDealer)
            gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood  where type in(2,3) or Good_ID=1 ORDER BY Good_Name desc", cboGoodType)
            gAdo.CtrlItemsLoad("Driver_ID", "Driver_Name", "tblDriver  ORDER BY Driver_Name", cboDriver)
            gAdo.CtrlItemsLoad("Issue_To_ID", "Issue_To_Name", "tblIssueTo ORDER BY Issue_To_Name", cboIssueTo)
            gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity   ORDER BY City_Name", cboCarCity, "", "")
            gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity  ORDER BY City_Name", cboCarInfoCity, "", "")
            gAdo.CtrlItemsLoad("LocationID", "LocationName", "tblLocation ORDER BY LocationID", cboLocation, "", "")


            Dim Query As String
            Dim DR As SqlDataReader
            ' Read data from trancactions
            Query = "select * from tblTransactions where TR_TY=2 and  CID=" & Val(cfrmMain.SharedTxtTransID.Text & " and First_Weigth_Scale_ID=" & cfrmMain.ScaleNo)
            DR = gAdo.CmdExecReader(Query)

            While DR.Read
                CarNo = DR("Car_ID")
                DealerID = DR("Dealer_ID")
                DriverID = DR("Driver_ID")
                GoodID = DR("Good_ID")
                IssueToID = DR("Issue_To_ID")
                FirstScale = DR("First_Weigth")
                SecondScale = DR("Second_Weight")
                CarInfoID = DR("Car_Info_ID")
                OutDate = DR("out_date")
                If (IsDBNull(DR("Notes")) = True) Then
                    Notes = ""
                Else : Notes = DR("Notes")
                End If

                If IsDBNull(DR("MSGID").ToString) = True Then
                    ReceiveType = 4
                Else
                    'ComeHERE
                    ReceiveType = DR("MSGID") ' DR("MSGID")
                End If

                ' Eng Ahmad fawzy
                Slip_Rate = DR("Slip_Rate")
                If (IsDBNull(DR("LocationID")) = True) Then
                    cboLocation.SelectedIndex = -1
                Else : cboLocation.SelectedValue = DR("LocationID")
                End If


            End While
            DR.Close()

            ' bind data to controls
            txtCarNo.Text = gAdo.CmdExecScalar("select CarBoard_No from tblCar where Car_ID =" & CarNo)
            cboDealer.SelectedValue = DealerID
            cboDriver.SelectedValue = DriverID
            cboIssueTo.SelectedValue = IssueToID
            cboGoodType.SelectedValue = GoodID
            lblFirstScale.Text = FirstScale
            lblSecondScale.Text = SecondScale
            txtManualEdit1.Text = FirstScale
            txtManualEdit2.Text = SecondScale
            OutTime.DateTimeValue = OutDate
            lblTransNumber.Text = cfrmMain.SharedTxtTransID.Text
            ' Eng Ahmad fawzy
            txtSlipRate.Text = Slip_Rate
            If gAdo.CmdExecScalar("select out_date from tblTransactions where TR_TY=2 and CID= " & Val(cfrmMain.SharedTxtTransID.Text)) Is DBNull.Value Then
                txtSecondScale.Enabled = False
            Else
                txtSecondScale.Enabled = True
            End If
            txtCarInfo.Text = gAdo.CmdExecScalar(" select TruckBoard_No from tblCarInfo where Car_Info_ID =" & CarInfoID)
            cboCarCity.SelectedValue = gAdo.CmdExecScalar("select CarBoard_City_ID from tblCar where Car_ID=" & CarNo)
            cboCarInfoCity.SelectedValue = gAdo.CmdExecScalar("select TruckBoard_City_ID from tblCarInfo where Car_Info_ID=" & CarInfoID)
            txtLicence.Text = gAdo.CmdExecScalar("select Driver_License_No from tblDriver where  Driver_ID =" & cboDriver.SelectedValue)
            txtNotes.Text = Notes
          
            If ReceiveType = 1 Then
                CboRecieveType.Text = "إسكندرية"
            ElseIf ReceiveType = 2 Then
                CboRecieveType.Text = "العامرية"
            Else
                CboRecieveType.Text = ""
            End If

            gUserO.ID = gUserID
            gUserO.Refresh()
            If gUserO.UserType = "n" Then
                cboLocation.Enabled = False
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnEdit_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
    btnEdit.MouseEnter, BtnCancel.MouseEnter

        sender.BackColor = Color.SkyBlue

    End Sub

    Private Sub btnEdit_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
    btnEdit.MouseLeave, BtnCancel.MouseLeave

        sender.BackColor = Color.AliceBlue

    End Sub

    Private Sub txtFirstScale_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
    txtFirstScale.KeyPress, txtSecondScale.KeyPress

        Dim M As Boolean = Char.IsNumber(e.KeyChar) Or Char.IsControl(e.KeyChar)
        If Not M = True Then
            e.Handled = True
        End If

    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click

        Try

            Dim UserCanUpdate As Integer
            UserCanUpdate = gAdo.CmdExecScalar("select canupdate from tblUser where user_ID = " & gUserID)
            If UserCanUpdate = 0 Then
                MsgBox("ليس لك صلاحية التعديل")
                Exit Sub
            Else
                GoTo 10
            End If
10:         Dim Out_Date As String = OutTime.TimeValue

            If Out_Date.Contains("م") Then
                Out_Date = Out_Date.Replace("م", "PM")
            ElseIf Out_Date.Contains("ص") Then
                Out_Date = Out_Date.Replace("ص", "AM")
            End If


            Dim IfCarExist As Integer
            Dim IfCarInfoExist As Integer

            msgResult = MsgBox("هل ترغب فى التعديل ؟", MsgBoxStyle.OkCancel)

            SaveBeforeUpdate()

            If msgResult = MsgBoxResult.Ok Then
                gAdo.CmdExec("UPDATE tblTransactions SET Slip_Charged_User_ID = " & gUserID & "  where CID=" & cfrmMain.SharedTxtTransID.Text)
                gAdo.CmdExec("update tblDriver set Driver_License_No='" & txtLicence.Text & "' where Driver_ID=" & cboDriver.SelectedValue)
                CarInfoID = gAdo.CmdExecScalar("select Car_Info_ID from tblCarInfo where TruckBoard_No ='" & txtCarInfo.Text & "'")
                IfCarInfoExist = gAdo.CmdExecScalar("select count(Car_Info_ID) from tblCarInfo where TruckBoard_No ='" & txtCarInfo.Text & "'")
                If IfCarInfoExist > 0 Then
                    gAdo.CmdExec("update tblCarInfo set TruckBoard_City_ID=" & cboCarInfoCity.SelectedValue & " where Car_Info_ID=" & CarInfoID)
                Else
                    gAdo.CmdExec("insert into tblCarInfo (TruckBoard_No,TruckBoard_City_ID,ScaleID) values(" & txtCarInfo.Text.Trim & "," & cboCarInfoCity.SelectedValue & ",1)")
                    CarInfoID = gAdo.CmdExecScalar("select max(Car_Info_ID) from tblCarInfo")
                End If
                Dim LID As String = ""
                If (cboLocation.SelectedValue = Nothing) Then
                    LID = 0
                Else : LID = cboLocation.SelectedValue.ToString()
                End If
                If chkManualEdit.Checked Then

                    'frmManualEditPassword.ShowDialog()
                    'If frmManualEditPassword.CorrectPassword = True Then
                    CarID = gAdo.CmdExecScalar("select Car_ID from tblCar where  CarBoard_No='" & txtCarNo.Text.Trim & "'")
                    IfCarExist = gAdo.CmdExecScalar("select count (Car_ID) from tblCar where CarBoard_No ='" & txtCarNo.Text.Trim & "'")
                    If IfCarExist > 0 Then
                        gAdo.CmdExec("update tblCar set CarBoard_City_ID =" & cboCarCity.SelectedValue & " where Car_ID =" & CarID)
                    Else
                        gAdo.CmdExec("insert into tblCar (CarBoard_No,CarBoard_City_ID,ScaleID) values(" & txtCarNo.Text.Trim & "," & cboCarCity.SelectedValue & ",1)")
                        CarID = gAdo.CmdExecScalar("select max(Car_ID) from tblCar")
                    End If

                    ' Eng Ahmad fawzy for updating Receive type value
                    If CboRecieveType.Text = "" Then
                        ReceiveType = 4
                    ElseIf CboRecieveType.Text = "إسكندرية" Then
                        ReceiveType = 1
                    ElseIf CboRecieveType.Text = "العامرية" Then
                        ReceiveType = 2
                    End If

                    Dim UpdateSQLStatement As String
                    

                    'Eng Ahmad fawzy
                    UpdateSQLStatement = "update tblTransactions set Car_ID=" & CarID & " ,Dealer_ID= " & cboDealer.SelectedValue & " ,Car_Info_ID=" & CarInfoID & "" _
                                 & ",Issue_To_ID= " & cboIssueTo.SelectedValue & " ,Driver_ID= " & cboDriver.SelectedValue & " " _
                                 & ",Second_Weight=" & lblSecondScale.Text & " ,Good_ID='" & cboGoodType.SelectedValue & "' " _
                                            & ",Slip_Rate=" & txtSlipRate.Text & " ,MSGID='" & ReceiveType & "' ,LocationID=" & LID

                    If chkEditOutTime.Checked = True Then

                        UpdateSQLStatement = UpdateSQLStatement & " ,out_date ='" & Out_Date & "'"
                    End If

                    gAdo.CmdExec(UpdateSQLStatement & " where TR_TY=2 and  CID=" & cfrmMain.SharedTxtTransID.Text & " and First_Weigth_Scale_ID=" & cfrmMain.ScaleNo)

                    'gAdo.CmdExec("update tblTransactions set Car_ID=" & CarID & " ,Dealer_ID= " & cboDealer.SelectedValue & " ,Car_Info_ID=" & CarInfoID & "" _
                    '             & ",Issue_To_ID= " & cboIssueTo.SelectedValue & " ,Driver_ID= " & cboDriver.SelectedValue & " " _
                    '             & ",First_Weigth=" & txtManualEdit1.Text & " ,Second_Weight=" & txtManualEdit2.Text & " ,Good_ID='" & cboGoodType.SelectedValue & "' ,ManualEdit=1  " _
                    '             & " where TR_TY=2 and  CID=" & cfrmMain.SharedTxtTransID.Text & " and First_Weigth_Scale_ID=" & cfrmMain.ScaleNo)

                    Dim FailedCount As Integer
                    FailedCount = gAdo.CmdExecScalar("select count(trans_id) from tblFailedTransactions where  TR_TY=2 and cid=" & cfrmMain.SharedTxtTransID.Text & " and First_Weigth_Scale_ID=" & cfrmMain.ScaleNo)
                    If FailedCount = 1 Then
                        'Edit the transactions if it was it was in the failer table of transactions before sendindg it
                        'Eng Ahmad fawzy
                        UpdateSQLStatement = "update tblTransactions set Car_ID=" & CarID & " ,Dealer_ID= " & cboDealer.SelectedValue & " ,Car_Info_ID=" & CarInfoID & "" _
                                     & ",Issue_To_ID= " & cboIssueTo.SelectedValue & " ,Driver_ID= " & cboDriver.SelectedValue & " " _
                                     & ",Second_Weight=" & lblSecondScale.Text & " ,Good_ID='" & cboGoodType.SelectedValue & "' " _
                                                & ",Slip_Rate=" & txtSlipRate.Text & " ,LocationID=" & cboLocation.SelectedValue & " "
                        If chkEditOutTime.Checked = True Then
                            UpdateSQLStatement = UpdateSQLStatement & " ,out_date ='" & Out_Date & "'"
                        End If

                        gAdo.CmdExec(UpdateSQLStatement & " where TR_TY=2 and  CID=" & cfrmMain.SharedTxtTransID.Text & " and First_Weigth_Scale_ID=" & cfrmMain.ScaleNo)


                        'gAdo.CmdExec("update tblFailedTransactions set Car_ID=" & CarID & " ,Dealer_ID= " & cboDealer.SelectedValue & " ,Car_Info_ID=" & CarInfoID & "" _
                        '             & ",Issue_To_ID= " & cboIssueTo.SelectedValue & " ,Driver_ID= " & cboDriver.SelectedValue & " " _
                        '             & ",First_Weigth=" & txtManualEdit1.Text & " ,Second_Weight=" & txtManualEdit2.Text & " ,Good_ID='" & cboGoodType.SelectedValue & "' ,ManualEdit=1 " _
                        '             & " where TR_TY=2 and  CID=" & cfrmMain.SharedTxtTransID.Text & " and First_Weigth_Scale_ID=" & cfrmMain.ScaleNo)
                    End If

                    'this is thread to update the Server DB
                    Thread = New System.Threading.Thread(AddressOf UpdateServerDB)
                    Control.CheckForIllegalCrossThreadCalls = False
                    Thread.Start()
                    ' end Thread

                    cfrmMain.SharedlblFrstWeight.Text = txtManualEdit1.Text
                    cfrmMain.SharedlblSecondWeight.Text = txtManualEdit2.Text
                    cfrmMain.SharedTxtCarNo.Text = txtCarNo.Text
                    cfrmMain.SharedCboDealer.SelectedValue = cboDealer.SelectedValue
                    cfrmMain.SharedCboDriver.SelectedValue = cboDriver.SelectedValue
                    cfrmMain.SharedCboGood.SelectedValue = cboGoodType.SelectedValue
                    cfrmMain.SharedCboIssueTo.SelectedValue = cboIssueTo.SelectedValue
                    cfrmMain.SharedCboCarCity.SelectedValue = cboCarCity.SelectedValue
                    cfrmMain.SharedCboCarInfoCity.SelectedValue = cboCarInfoCity.SelectedValue
                    cfrmMain.SharedtxtLicence.Text = txtLicence.Text
                    MsgBox("تم الحفظ بنجاح")
                    Me.Close()
                    'Else
                    '    Exit Sub
                    'End If
                Else
                    ' عندما كان التعديل بكلمة سر فقط
                    'frmPassWord.ShowDialog()
                    'If frmPassWord.CorrectPassword = True Then

                    CarID = gAdo.CmdExecScalar("select Car_ID from tblCar where  CarBoard_No='" & txtCarNo.Text.Trim & "'")
                    IfCarExist = gAdo.CmdExecScalar("select count (CarBoard_No) from tblCar where CarBoard_No ='" & txtCarNo.Text.Trim & "'")
                    If IfCarExist > 0 Then
                        gAdo.CmdExec("update tblCar set CarBoard_City_ID =" & cboCarCity.SelectedValue & " where Car_ID =" & CarID)
                    Else
                        gAdo.CmdExec("insert into tblCar (CarBoard_No,CarBoard_City_ID,ScaleID) values(" & txtCarNo.Text.Trim & "," & cboCarInfoCity.SelectedValue & ",1)")
                        CarID = gAdo.CmdExecScalar("select max(Car_ID) from tblCar")
                    End If

                    ' Eng Ahmad fawzy for updating Receive type value
                    If CboRecieveType.Text = "" Then
                        ReceiveType = 4
                    ElseIf CboRecieveType.Text = "إسكندرية" Then
                        ReceiveType = 1
                    ElseIf CboRecieveType.Text = "العامرية" Then
                        ReceiveType = 2
                    End If

                    Dim UpdateSQLStatement As String
                    'Eng Ahmad fawzy
                    UpdateSQLStatement = "update tblTransactions set Car_ID=" & CarID & " ,Dealer_ID= " & cboDealer.SelectedValue & " ,Car_Info_ID=" & CarInfoID & "" _
                                 & ",Issue_To_ID= " & cboIssueTo.SelectedValue & " ,Driver_ID= " & cboDriver.SelectedValue & " " _
                                 & ",Second_Weight=" & lblSecondScale.Text & " ,Good_ID='" & cboGoodType.SelectedValue & "' " _
                                            & ",Slip_Rate=" & txtSlipRate.Text & " ,MSGID='" & ReceiveType & "',Notes='" & txtNotes.Text & "',LocationID=" & LID & " "



                    gTransactionO.Slip_Rate = txtSlipRate.Text
                    If chkEditOutTime.Checked = True Then
                        UpdateSQLStatement = UpdateSQLStatement & " ,out_date ='" & Out_Date & "'"
                    End If

                    gAdo.CmdExec(UpdateSQLStatement & " where TR_TY=2 and  CID=" & cfrmMain.SharedTxtTransID.Text)

                    'gAdo.CmdExec("update tblTransactions set Car_ID=" & CarID & " ,Dealer_ID= " & cboDealer.SelectedValue & " ,Car_Info_ID=" & CarInfoID & "" _
                    '             & ",Issue_To_ID= " & cboIssueTo.SelectedValue & " ,Driver_ID= " & cboDriver.SelectedValue & " " _
                    '             & ",Second_Weight=" & lblSecondScale.Text & " ,Good_ID='" & cboGoodType.SelectedValue & "' " _
                    '             & " where TR_TY=2 and  CID=" & cfrmMain.SharedTxtTransID.Text & " and First_Weigth_Scale_ID=" & cfrmMain.ScaleNo)

                    'Dim FailedCount As Integer
                    'FailedCount = gAdo.CmdExecScalar("select count(trans_id) from tblFailedTransactions where TR_TY=2 and  cid=" & cfrmMain.SharedTxtTransID.Text & " and First_Weigth_Scale_ID=" & cfrmMain.ScaleNo)
                    'If FailedCount = 1 Then
                    '    'Edit the transactions if it  was in the failer table of transactions before sendindg it

                    '    'Eng Ahmad fawzy
                    '    UpdateSQLStatement = "update tblTransactions set Car_ID=" & CarID & " ,Dealer_ID= " & cboDealer.SelectedValue & " ,Car_Info_ID=" & CarInfoID & "" _
                    '                 & ",Issue_To_ID= " & cboIssueTo.SelectedValue & " ,Driver_ID= " & cboDriver.SelectedValue & " " _
                    '                 & ",Second_Weight=" & lblSecondScale.Text & " ,Good_ID='" & cboGoodType.SelectedValue & "' " _
                    '                            & ",Slip_Rate=" & txtSlipRate.Text & " "
                    '    gTransactionO.Slip_Rate = txtSlipRate.Text
                    '    If chkEditOutTime.Checked = True Then
                    '        UpdateSQLStatement = UpdateSQLStatement & " ,out_date ='" & Out_Date & "'"
                    '    End If

                    '    gAdo.CmdExec(UpdateSQLStatement & " where  TR_TY=2 and CID=" & cfrmMain.SharedTxtTransID.Text & " and First_Weigth_Scale_ID=" & cfrmMain.ScaleNo)

                    '    'gAdo.CmdExec("update tblFailedTransactions set Car_ID=" & CarID & " ,Dealer_ID= " & cboDealer.SelectedValue & " ,Car_Info_ID=" & CarInfoID & "" _
                    '    '             & ",Issue_To_ID= " & cboIssueTo.SelectedValue & " ,Driver_ID= " & cboDriver.SelectedValue & " " _
                    '    '             & ",Second_Weight=" & lblSecondScale.Text & " ,Good_ID='" & cboGoodType.SelectedValue & "' ,ManualEdit=1 " _
                    '    '             & " where  TR_TY=2 and CID=" & cfrmMain.SharedTxtTransID.Text & " and First_Weigth_Scale_ID=" & cfrmMain.ScaleNo)
                    'End If

                    'this is thread to update the Server DB
                    'Thread = New System.Threading.Thread(AddressOf UpdateServerDB)
                    'Control.CheckForIllegalCrossThreadCalls = False
                    'Thread.Start()
                    ' end Thread
                    SaveAfterUpdate()

                    cfrmMain.SharedlblFrstWeight.Text = lblFirstScale.Text
                    cfrmMain.SharedlblSecondWeight.Text = lblSecondScale.Text
                    cfrmMain.SharedTxtCarNo.Text = txtCarNo.Text
                    cfrmMain.SharedCboDealer.SelectedValue = cboDealer.SelectedValue
                    cfrmMain.SharedCboDriver.SelectedValue = cboDriver.SelectedValue
                    cfrmMain.SharedCboGood.SelectedValue = cboGoodType.SelectedValue
                    cfrmMain.SharedCboIssueTo.SelectedValue = cboIssueTo.SelectedValue
                    cfrmMain.SharedCboCarCity.SelectedValue = cboCarCity.SelectedValue
                    cfrmMain.SharedCboCarInfoCity.SelectedValue = cboCarInfoCity.SelectedValue
                    cfrmMain.SharedtxtLicence.Text = txtLicence.Text
                    cfrmMain.SharedtxtNotes.Text = txtNotes.Text
                    MsgBox("تم الحفظ بنجاح")
                    Me.Close()
                    'Else
                    '    Exit Sub
                End If
            End If
            SendWOMail()
            'Else
            '    Exit Sub
            'End If

        Catch ex As Exception
        End Try

    End Sub
    Private Sub SendWOMail()
        Try

            Dim Statment As String = "select Email from tblEmails"

            Dim dtE As New DataTable
            dtE = gAdo.FillDataTable(Statment)
            For ii = 0 To dtE.Rows.Count - 1
                Dim dt_ As New DataTable
                dt_ = gAdo.FillDataTable("Select * from tblFromEmail Order by ID")

                Dim Smtp_Server As New SmtpClient
                Dim e_mail As New MailMessage()
                gUserO.ID = gUserID
                gUserO.Refresh()

                With dt_.Rows(0)
                    Smtp_Server.UseDefaultCredentials = .Item("UseDefaultCredentials")
                    Smtp_Server.Credentials = New Net.NetworkCredential(.Item("Email"), .Item("Password"))
                    Smtp_Server.Port = .Item("Port")
                    Smtp_Server.EnableSsl = .Item("EnableSsl")
                    Smtp_Server.Host = .Item("Host")

                    e_mail = New MailMessage()
                    e_mail.From = New MailAddress(.Item("Email"), gUserO.Name)
                End With

                e_mail.Bcc.Add(dtE.Rows(ii).Item("Email"))
                'Next
                Try
                    e_mail.Subject = "تعديل كارته"
                    e_mail.IsBodyHtml = False

                    e_mail.Body = " تم تعديل الكارته رقم " & lblTransNumber.Text
                    Smtp_Server.Send(e_mail)
                    e_mail.Dispose()

                Catch ex As Exception
                    e_mail.Dispose()

                End Try



            Next

        Catch error_t As Exception

            MsgBox(error_t.ToString)
            Throw error_t
        End Try
        'Try

        '    Dim dt_ As New DataTable
        '    dt_ = gAdo.FillDT("Select * from tblFromEmail Order by ID")

        '    Dim Smtp_Server As New SmtpClient
        '    Dim e_mail As New MailMessage()

        '    With dt_.Rows(0)
        '        Smtp_Server.UseDefaultCredentials = .Item("UseDefaultCredentials")
        '        Smtp_Server.Credentials = New Net.NetworkCredential(.Item("Email"), .Item("Password"))
        '        Smtp_Server.Port = .Item("Port")
        '        Smtp_Server.EnableSsl = .Item("EnableSsl")
        '        Smtp_Server.Host = .Item("Host")

        '        e_mail = New MailMessage()
        '        e_mail.From = New MailAddress(.Item("Email"), gUser.Name)

        '    End With





        '    e_mail.Subject = ""
        '    e_mail.IsBodyHtml = False


        '    Smtp_Server.Send(e_mail)
        '    e_mail.Dispose()
        'Catch error_t As Exception
        '    '  MsgBox(error_t.ToString)
        'End Try
    End Sub


    Dim intType As Integer

    Sub SaveBeforeUpdate()



        gTransactionO.ID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and CID =" & lblTransNumber.Text)
        gTransactionO.Refresh()

        intType = gAdo.CmdExecScalar("select isnull(max(Type),0)  from tblUpdateTransactions")

        Dim Today As String
        Today = Now
        If Today.Contains("ص") Then
            Today = Today.Replace("ص", "AM")
        ElseIf Today.Contains("م") Then
            Today = Today.Replace("م", "PM")
        End If

        gUpdateTransactionMstO.ID = 0
        gUpdateTransactionMstO.UpDateDate = Today
        gUpdateTransactionMstO.User_ID = gUserID
        gUpdateTransactionMstO.Save()
        UpDateTransID = gAdo.CmdExecScalar("SELECT IDENT_CURRENT('tblUpDateTransactionsMst') AS MyTableID")

        gUpdateTransactionO.ID = 0
        gUpdateTransactionO.In_Date = gTransactionO.In_Date
        gUpdateTransactionO.Out_Date = gTransactionO.Out_Date
        gUpdateTransactionO.In_Shift_ID.ID = gTransactionO.In_Shift_ID.ID
        gUpdateTransactionO.In_Shift_Date = gTransactionO.In_Shift_Date
        gUpdateTransactionO.Car_Info_ID.ID = gTransactionO.Car_Info_ID.ID
        gUpdateTransactionO.Dealer_ID.ID = gTransactionO.Dealer_ID.ID
        gUpdateTransactionO.Issue_To_ID.ID = gTransactionO.Issue_To_ID.ID
        gUpdateTransactionO.First_Weigth = gTransactionO.First_Weigth
        gUpdateTransactionO.First_Weight_IsEmpty = gTransactionO.First_Weight_IsEmpty
        gUpdateTransactionO.First_Weight_User_ID.ID = gTransactionO.First_Weight_User_ID.ID
        gUpdateTransactionO.First_Weigth_Scale_ID.ID = gTransactionO.First_Weigth_Scale_ID.ID

        gUpdateTransactionO.Second_Weight = gTransactionO.Second_Weight
        gUpdateTransactionO.Second_Weight_IsEmpty = gTransactionO.Second_Weight_IsEmpty
        gUpdateTransactionO.Second_Weight_User_ID.ID = gTransactionO.Second_Weight_User_ID.ID
        gUpdateTransactionO.Second_Weight_Scale_ID.ID = gTransactionO.Second_Weight_Scale_ID.ID

        gUpdateTransactionO.Good_ID.ID = gTransactionO.Good_ID.ID
        gUpdateTransactionO.Slip_Rate = gTransactionO.Slip_Rate
        gUpdateTransactionO.Slip_Charged_Date = gTransactionO.Slip_Charged_Date
        gUpdateTransactionO.Slip_Charged_User_ID = gTransactionO.Slip_Charged_User_ID

        gUpdateTransactionO.Driver_ID.ID = gTransactionO.Driver_ID.ID
        gUpdateTransactionO.Car_ID.ID = gTransactionO.Car_ID.ID


        gUpdateTransactionO.CID = lblTransNumber.Text
        gUpdateTransactionO.ManualEdit = 0
        gUpdateTransactionO.Type = intType + 1
        gUpdateTransactionO.TR_TY = 2
        gUpdateTransactionO.PortCD = "0"
        gUpdateTransactionO.IsGab = 0
        gUpdateTransactionO.Gab = 0
        gUpdateTransactionO.Bill = 0
        gUpdateTransactionO.CustomWeight = gTransactionO.Second_Weight - gTransactionO.First_Weigth
        gUpdateTransactionO.ConfirmedWeight = 0
        gUpdateTransactionO.MSGID = ReceiveType
        gUpdateTransactionO.Contractor_ID.ID = gTransactionO.Contractor_ID.ID
        gUpdateTransactionO.Notes = Notes
        If (gTransactionO.LocationID = Nothing) Then
            gUpdateTransactionO.Location = 0
        Else : gUpdateTransactionO.Location = gTransactionO.LocationID
        End If

        gUpdateTransactionO.UpDateTransID = UpDateTransID
        gUpdateTransactionO.Flag = 0
        gUpdateTransactionO.Save()
    End Sub

    Sub SaveAfterUpdate()

        gTransactionO.ID = gAdo.CmdExecScalar("select Trans_ID from tblTransactions where TR_TY=2 and CID =" & lblTransNumber.Text)
        gTransactionO.Refresh()
       
        gUpdateTransactionO.ID = 0
        gUpdateTransactionO.In_Date = gTransactionO.In_Date
        gUpdateTransactionO.Out_Date = gTransactionO.Out_Date
        gUpdateTransactionO.In_Shift_ID.ID = gTransactionO.In_Shift_ID.ID
        gUpdateTransactionO.In_Shift_Date = gTransactionO.In_Shift_Date
        gUpdateTransactionO.Car_Info_ID.ID = gTransactionO.Car_Info_ID.ID
        gUpdateTransactionO.Dealer_ID.ID = gTransactionO.Dealer_ID.ID
        gUpdateTransactionO.Issue_To_ID.ID = gTransactionO.Issue_To_ID.ID
        gUpdateTransactionO.First_Weigth = gTransactionO.First_Weigth
        gUpdateTransactionO.First_Weight_IsEmpty = gTransactionO.First_Weight_IsEmpty
        gUpdateTransactionO.First_Weight_User_ID.ID = gTransactionO.First_Weight_User_ID.ID
        gUpdateTransactionO.First_Weigth_Scale_ID.ID = gTransactionO.First_Weigth_Scale_ID.ID

        gUpdateTransactionO.Second_Weight = gTransactionO.Second_Weight
        gUpdateTransactionO.Second_Weight_IsEmpty = gTransactionO.Second_Weight_IsEmpty
        gUpdateTransactionO.Second_Weight_User_ID.ID = gTransactionO.Second_Weight_User_ID.ID
        gUpdateTransactionO.Second_Weight_Scale_ID.ID = gTransactionO.Second_Weight_Scale_ID.ID

        gUpdateTransactionO.Good_ID.ID = gTransactionO.Good_ID.ID
        gUpdateTransactionO.Slip_Rate = gTransactionO.Slip_Rate
        gUpdateTransactionO.Slip_Charged_Date = gTransactionO.Slip_Charged_Date
        gUpdateTransactionO.Slip_Charged_User_ID = gTransactionO.Slip_Charged_User_ID

        gUpdateTransactionO.Driver_ID.ID = gTransactionO.Driver_ID.ID
        gUpdateTransactionO.Car_ID.ID = gTransactionO.Car_ID.ID


        gUpdateTransactionO.CID = lblTransNumber.Text
        gUpdateTransactionO.ManualEdit = 0
        gUpdateTransactionO.Type = intType + 1
        gUpdateTransactionO.TR_TY = 2
        gUpdateTransactionO.PortCD = "0"
        gUpdateTransactionO.IsGab = 0
        gUpdateTransactionO.Gab = 0
        gUpdateTransactionO.Bill = 0
        gUpdateTransactionO.CustomWeight = gTransactionO.Second_Weight - gTransactionO.First_Weigth
        gUpdateTransactionO.ConfirmedWeight = 0
        gUpdateTransactionO.MSGID = ReceiveType
        gUpdateTransactionO.Contractor_ID = gTransactionO.Contractor_ID
        gUpdateTransactionO.Notes = txtNotes.Text
        If (gTransactionO.LocationID = Nothing) Then
            gUpdateTransactionO.Location = 0
        Else : gUpdateTransactionO.Location = gTransactionO.LocationID
        End If
        gUpdateTransactionO.UpDateTransID = UpDateTransID
        gUpdateTransactionO.Flag = 0
        gUpdateTransactionO.Save()
    End Sub
    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click

        Me.Close()

    End Sub
    Dim intCounteading As Integer = 0
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Try

            If cfrmMain.serl.IsOpen Then
                ' Don't do any thing
            Else

                With cfrmMain.serl
                    .PortName = "COM1"
                    .BaudRate = 9600
                    .DataBits = 8
                    .StopBits = StopBits.One
                    .Parity = Parity.None
                    '.Handshake = Handshake.None
                End With
                cfrmMain.serl.Open()
            End If
        Catch ex As Exception

        End Try


        OutTimeEdit.DateTimeValue = DateTime.Now
        Dim Out_DateEdit As String = OutTimeEdit.TimeValue

        If Out_DateEdit.Contains("م") Then
            Out_DateEdit = Out_DateEdit.Replace("م", "PM")
        ElseIf Out_DateEdit.Contains("ص") Then
            Out_DateEdit = Out_DateEdit.Replace("ص", "AM")
        End If

        If intCounteading < 2 Then
            intCounteading += 1
            Try

                ' gAdo.CmdExec("update tblTransactions set out_date ='" & Out_DateEdit & "' where TR_TY=2 and CID=" & cfrmMain.SharedTxtTransID.Text)
                cfrmMain.SharedRichTxt.Text = ""
                cfrmMain.SharedRichTxt.Text = cfrmMain.serl.ReadExisting
                '' 20190217
                'cfrmMain.SharedRichTxt.Text = cfrmMain.serl.ReadExisting
                'If cfrmMain. cfrmMain.SharedRichTxt.Text.LastIndexOf("i") - 14 > 0 Then
                '     cfrmMain.SharedRichTxt.Text = Val(cfrmMain. cfrmMain.SharedRichTxt.Text.Substring(cfrmMain. cfrmMain.SharedRichTxt.Text.LastIndexOf("i") - 14, 6))
                'Else
                '     cfrmMain.SharedRichTxt.Text = Val(cfrmMain. cfrmMain.SharedRichTxt.Text.Substring(cfrmMain. cfrmMain.SharedRichTxt.Text.LastIndexOf("i") + 3, 6))
                'End If




                'cfrmMain.SharedRichTxt.Text = ""
                'cfrmMain.SharedRichTxt.Text = cfrmMain.serl.ReadExisting
                If cfrmMain.ScaleNo = 2 Then   ' الميزان الرئيسى
                    If cfrmMain.SharedRichTxt.Text.LastIndexOf("A") - 14 > 0 Then
                        lblSecondScale.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf("A") - 14, 6))
                        'lblShowWeight.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf("A") - 14, 6))
                    Else


                        lblSecondScale.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf("A") + 3, 6))
                        'lblShowWeight.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf("A") + 3, 6))
                    End If

                Else
                    Dim str As String
                    'cfrmMain.SharedRichTxt.Text = ""

                    'Str = cfrmMain.SharedRichTxt.Text

                    lblSecondScale.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf(")") - 16, 11))

                    If lblSecondScale.Text = 0 Then
                        lblSecondScale.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf(")") - 16, 11))

                    End If
                    'If cfrmMain.SharedRichTxt.Text.LastIndexOf(")1") - 17 > 0 Then
                    '    lblSecondScale.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf(")1") - 17, 6))

                    'Else
                    '    lblSecondScale.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf(")1") + 6, 6))

                    'End If

                End If



                'If cfrmMain.SharedRichTxt.Text.LastIndexOf("i") - 14 > 0 Then
                '    lblFirstScale.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf("i") - 14, 6))
                'Else
                '    lblFirstScale.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf("i") + 3, 6))
                'End If




                'If cfrmMain.ScaleNo = 3 Then

                '    Dim s As String
                '    s = cfrmMain.SharedRichTxt.Text
                '    s = s.Remove(0, 10)
                '    s = s.Replace("kg", " ")
                '    cfrmMain.SharedRichTxt.Text = Val(s)
                '    lblSecondScale.Text = cfrmMain.SharedRichTxt.Text
                'Else

                '    If cfrmMain.SharedRichTxt.Text.LastIndexOf("i") - 14 > 0 Then
                '        lblSecondScale.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf("i") - 14, 6))

                '    Else
                '        lblSecondScale.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf("i") + 3, 6))

                '    End If
                'End If
            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try





            If cfrmMain.serl.IsOpen Then
                cfrmMain.serl.Close()

            Else
                ' Don't do any thing
            End If

            Button1_Click(Button1, e)
        Else
            intCounteading = 0
        End If


        

    End Sub

    Private Sub chkManualEdit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkManualEdit.CheckedChanged
        If chkManualEdit.Checked Then
            txtManualEdit1.Enabled = True
            txtManualEdit2.Enabled = True
        Else
            txtManualEdit1.Enabled = False
            txtManualEdit2.Enabled = False
        End If
    End Sub

    Private Sub txtManualEdit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtManualEdit1.KeyPress
        Try
            Dim M As Boolean = Char.IsNumber(e.KeyChar) Or Char.IsControl(e.KeyChar)
            If Not M = True Then
                e.Handled = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub UpdateServerDB()

        Dim SqlCon As New SqlConnection
        Dim SqlCmnd As New SqlCommand
        Try
            If gAdo.CmdExecScalar(" select Out_Date from tblTransactions where  TR_TY=2 and CID = " & cfrmMain.SharedTxtTransID.Text) Is DBNull.Value Then
                Exit Sub
            End If

            Using Scope As New TransactionScope()
                gconConnection.Close() 'here close the main connection 
                SqlCon.ConnectionString = GetServerNameForServerDB() 'here initialize theserver connection to update in server DB
                SqlCon.Open()
                SqlCmnd.Connection = SqlCon
                SQLTrans = SqlCon.BeginTransaction
                SqlCmnd.Transaction = SQLTrans
                SqlCmnd.CommandType = CommandType.Text
                If chkManualEdit.Checked Then
                    SqlCmnd.CommandText = "update tblTransactions set Car_ID=" & CarID & " ,Dealer_ID= " & cboDealer.SelectedValue & " " _
                                        & ",Issue_To_ID= " & cboIssueTo.SelectedValue & " ,Driver_ID= " & cboDriver.SelectedValue & " " _
                                        & ",Second_Weight=" & txtManualEdit1.Text & " ,Good_ID='" & cboGoodType.SelectedValue & "' ,ManualEdit=1  " _
                                        & " where TR_TY=2 and CID=" & cfrmMain.SharedTxtTransID.Text & " and First_Weigth_Scale_ID=" & cfrmMain.ScaleNo
                    SqlCmnd.ExecuteNonQuery()
                Else
                    SqlCmnd.CommandText = "update tblTransactions set Car_ID=" & CarID & " ,Dealer_ID= " & cboDealer.SelectedValue & " " _
                                         & ",Issue_To_ID= " & cboIssueTo.SelectedValue & " ,Driver_ID= " & cboDriver.SelectedValue & " " _
                                         & ",Second_Weight=" & lblSecondScale.Text & " ,Good_ID='" & cboGoodType.SelectedValue & "' " _
                                         & " where TR_TY=2 and  CID=" & cfrmMain.SharedTxtTransID.Text & " and First_Weigth_Scale_ID=" & cfrmMain.ScaleNo
                    SqlCmnd.ExecuteNonQuery()
                End If


                SQLTrans.Commit()
                Scope.Complete()
                SqlCon.Close()
            End Using
        Catch ex As Exception
            ' if it failto insert into server database
            gconConnection.Close()
            gconConnection = New DBLib.clsDBConnection(GetConString(Application.StartupPath & "\ConnString.txt"))
            gAdo = New ClsAdo(GetServerName(), GetDataBaseName(), GetUserID(), GetPassword())
            gconConnection.Open()

            Dim DR As SqlDataReader

            Dim Trans_ID, In_Shift_ID, First_Weigth_Scale_ID, _
            First_Weight_User_ID, Second_Weight_User_ID, _
            Second_Weight_Scale_ID, Slip_Charged_User_ID, Driver_ID, _
            Car_ID, CID, Car_Info_ID, ManualEdit, TR_TY As Integer
            Dim In_Date, Out_Date, In_Shift_Date, Dealer_ID, _
            Issue_To_ID, Good_ID, Slip_Charged_Date, _
            TruckBoard_No, CarBoard_No, Driver_Name, Issue_To_Name, Driver_License_No, Port_CD, bill As String
            Dim Slip_Rate, Second_Weight, First_Weigth As Decimal
            Dim First_Weight_IsEmpty, Second_Weight_IsEmpty As Boolean

            DR = gAdo.CmdExecReader("select Trans_ID,In_Date,Out_Date,In_Shift_ID,In_Shift_Date," _
                        & "T.Car_Info_ID,T.Dealer_ID,T.Issue_To_ID,First_Weigth," _
                        & " First_Weigth_Scale_ID, First_Weight_User_ID, Second_Weight, " _
                        & " Second_Weight_User_ID, Second_Weight_Scale_ID, Good_ID, " _
                        & " Slip_Rate, Slip_Charged_Date, Slip_Charged_User_ID, T.Driver_ID, " _
                        & " T.Car_ID, CID, ManualEdit, First_Weight_IsEmpty, Second_Weight_IsEmpty" _
                        & " ,CarBoard_No,TruckBoard_No,Driver_Name,Issue_To_Name,Driver_License_No,TR_TY,Port_CD,bill " _
                        & " from tblTransactions as T join tblCar as C on T.Car_ID=C.Car_ID" _
                        & " join tblCarInfo as CI on T.Car_Info_ID=CI.Car_Info_ID" _
                        & " join tblDriver as D on T.Driver_ID=D.Driver_ID" _
                        & " join tblIssueTo as I on T.Issue_To_ID=I.Issue_To_ID" _
                        & " where TR_TY=2 and  CID = " & cfrmMain.SharedTxtTransID.Text)

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
                'Port_CD = DR("Port_CD")
                'bill = DR("bill")
            End While
            DR.Close()
            If Out_Date Is Nothing Then

            Else
                If Out_Date.Contains("م") Then
                    Out_Date = Out_Date.Replace("م", "PM")
                ElseIf Out_Date.Contains("ص") Then
                    Out_Date = Out_Date.Replace("ص", "AM")
                End If
            End If

            If chkManualEdit.Checked Then
                ManualEdit = 1
            End If
            gAdo.CmdExec("insert into tblFailedUpdateTransactions (Trans_ID,In_Date,Out_Date,In_Shift_ID,In_Shift_Date," _
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

        gconConnection.Close()
        gconConnection = New DBLib.clsDBConnection(GetConString(Application.StartupPath & "\ConnString.txt"))
        gAdo = New ClsAdo(GetServerName(), GetDataBaseName(), GetUserID(), GetPassword())
        gconConnection.Open()

    End Sub

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

    Private Sub btnRead1stScale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRead1stScale.Click

        Try

            If cfrmMain.serl.IsOpen Then
                ' Don't do any thing
            Else

                With cfrmMain.serl
                    .PortName = "COM1"
                    .BaudRate = 9600
                    .DataBits = 8
                    .StopBits = StopBits.One
                    .Parity = Parity.None
                    '.Handshake = Handshake.None
                End With
                cfrmMain.serl.Open()
            End If
        Catch ex As Exception

        End Try


        Try
           
            cfrmMain.SharedRichTxt.Text = ""
            ' 20190217
            'cfrmMain.SharedRichTxt.Text = cfrmMain.serl.ReadExisting
            If cfrmMain.ScaleNo = 2 Then
                If cfrmMain.SharedRichTxt.Text.LastIndexOf("A") - 14 > 0 Then
                    lblFirstScale.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf("A") - 14, 6))
                    'lblShowWeight.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf("A") - 14, 6))
                Else


                    lblFirstScale.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf("A") + 3, 6))
                    'lblShowWeight.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf("A") + 3, 6))
                End If
            Else
                'If cfrmMain.SharedRichTxt.Text.LastIndexOf(")1") - 17 > 0 Then

                lblFirstScale.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf(")") - 16, 11))

                '    Else
                '    lblFirstScale.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf(")") - 16, 11))

                'End If

            End If



            'If cfrmMain.SharedRichTxt.Text.LastIndexOf("i") - 14 > 0 Then
            '    lblFirstScale.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf("i") - 14, 6))
            'Else
            '    lblFirstScale.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf("i") + 3, 6))
            'End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub cboDriver_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDriver.SelectedValueChanged
        Try
            txtLicence.Text = gAdo.CmdExecScalar("select Driver_License_No from tblDriver where  Driver_ID =" & cboDriver.SelectedValue)
        Catch ex As Exception

        End Try
    End Sub

   
    Private Sub chkEditOutTime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEditOutTime.CheckedChanged

    End Sub
End Class