Imports System.Data.SqlClient
Imports System.IO.Ports
Imports System.Transactions
Imports System.IO

Public Class frmEdit

    Dim CarNo, GoodID As String
    Dim DealerID, DriverID, IssueToID, FirstScale, SecondScale As Integer
    Dim msgResult As MsgBoxResult
    Dim SQLTrans As SqlTransaction
    Dim CarID, CarInfoID, OutDate As String
    Dim Thread As System.Threading.Thread

    Private Sub frmEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", "TblDealer where Type=1 or dealer_ID=1  ORDER BY Dealer_Name", cboDealer)
            gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood   where  (Good_ID like '07'+'%' or  good_ID =1 ) and Good_ID in ('0705','0706','0711','0712','0713','0714','0715') or good_ID =1 ORDER BY Good_Name desc", cboGoodType)
            gAdo.CtrlItemsLoad("Driver_ID", "Driver_Name", "tblDriver  ORDER BY Driver_Name", cboDriver)
            gAdo.CtrlItemsLoad("Issue_To_ID", "Issue_To_Name", "tblIssueTo ORDER BY Issue_To_Name", cboIssueTo)
            gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity   ORDER BY City_Name", cboCarCity, "", "")
            gAdo.CtrlItemsLoad("City_ID", "City_Name", "tblCity  ORDER BY City_Name", cboCarInfoCity, "", "")

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
            If gAdo.CmdExecScalar("select out_date from tblTransactions where TR_TY=2 and CID= " & Val(cfrmMain.SharedTxtTransID.Text)) Is DBNull.Value Then
                txtSecondScale.Enabled = False
            Else
                txtSecondScale.Enabled = True
            End If
            txtCarInfo.Text = gAdo.CmdExecScalar(" select TruckBoard_No from tblCarInfo where Car_Info_ID =" & CarInfoID)
            cboCarCity.SelectedValue = gAdo.CmdExecScalar("select CarBoard_City_ID from tblCar where Car_ID=" & CarNo)
            cboCarInfoCity.SelectedValue = gAdo.CmdExecScalar("select TruckBoard_City_ID from tblCarInfo where Car_Info_ID=" & CarInfoID)
            txtLicence.Text = gAdo.CmdExecScalar("select Driver_License_No from tblDriver where  Driver_ID =" & cboDriver.SelectedValue)

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
            Dim Out_Date As String = OutTime.TimeValue

            If Out_Date.Contains("م") Then
                Out_Date = Out_Date.Replace("م", "PM")
            ElseIf Out_Date.Contains("ص") Then
                Out_Date = Out_Date.Replace("ص", "AM")
            End If


            Dim IfCarExist As Integer
            Dim IfCarInfoExist As Integer

            msgResult = MsgBox("هل ترغب فى التعديل ؟", MsgBoxStyle.OkCancel)

            If msgResult = MsgBoxResult.Ok Then

                gAdo.CmdExec("update tblDriver set Driver_License_No='" & txtLicence.Text & "' where Driver_ID=" & cboDriver.SelectedValue)
                CarInfoID = gAdo.CmdExecScalar("select Car_Info_ID from tblCarInfo where TruckBoard_No ='" & txtCarInfo.Text & "'")
                IfCarInfoExist = gAdo.CmdExecScalar("select count(Car_Info_ID) from tblCarInfo where TruckBoard_No ='" & txtCarInfo.Text & "'")
                If IfCarInfoExist > 0 Then
                    gAdo.CmdExec("update tblCarInfo set TruckBoard_City_ID=" & cboCarInfoCity.SelectedValue & " where Car_Info_ID=" & CarInfoID)
                Else
                    gAdo.CmdExec("insert into tblCarInfo (TruckBoard_No,TruckBoard_City_ID,ScaleID) values(" & txtCarInfo.Text.Trim & "," & cboCarInfoCity.SelectedValue & ",1)")
                    CarInfoID = gAdo.CmdExecScalar("select max(Car_Info_ID) from tblCarInfo")
                End If

                If chkManualEdit.Checked Then

                    frmManualEditPassword.ShowDialog()
                    If frmManualEditPassword.CorrectPassword = True Then
                        CarID = gAdo.CmdExecScalar("select Car_ID from tblCar where  CarBoard_No='" & txtCarNo.Text.Trim & "'")
                        IfCarExist = gAdo.CmdExecScalar("select count (Car_ID) from tblCar where CarBoard_No ='" & txtCarNo.Text.Trim & "'")
                        If IfCarExist > 0 Then
                            gAdo.CmdExec("update tblCar set CarBoard_City_ID =" & cboCarCity.SelectedValue & " where Car_ID =" & CarID)
                        Else
                            gAdo.CmdExec("insert into tblCar (CarBoard_No,CarBoard_City_ID,ScaleID) values(" & txtCarNo.Text.Trim & "," & cboCarCity.SelectedValue & ",1)")
                            CarID = gAdo.CmdExecScalar("select max(Car_ID) from tblCar")
                        End If

                        Dim UpdateSQLStatement As String
                        UpdateSQLStatement = "update tblTransactions set Car_ID=" & CarID & " ,Dealer_ID= " & cboDealer.SelectedValue & " ,Car_Info_ID=" & CarInfoID & "" _
                                     & ",Issue_To_ID= " & cboIssueTo.SelectedValue & " ,Driver_ID= " & cboDriver.SelectedValue & " " _
                                     & ",First_Weigth=" & txtManualEdit1.Text & " ,Second_Weight=" & txtManualEdit2.Text & " ,Good_ID='" & cboGoodType.SelectedValue & "' ,ManualEdit=1  "

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

                            UpdateSQLStatement = "update tblFailedTransactions set Car_ID=" & CarID & " ,Dealer_ID= " & cboDealer.SelectedValue & " ,Car_Info_ID=" & CarInfoID & "" _
                                         & ",Issue_To_ID= " & cboIssueTo.SelectedValue & " ,Driver_ID= " & cboDriver.SelectedValue & " " _
                                         & ",First_Weigth=" & txtManualEdit1.Text & " ,Second_Weight=" & txtManualEdit2.Text & " ,Good_ID='" & cboGoodType.SelectedValue & "' ,ManualEdit=1 "

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
                    Else
                        Exit Sub
                    End If
                Else
                    frmPassWord.ShowDialog()
                    If frmPassWord.CorrectPassword = True Then
                        CarID = gAdo.CmdExecScalar("select Car_ID from tblCar where  CarBoard_No='" & txtCarNo.Text.Trim & "'")
                        IfCarExist = gAdo.CmdExecScalar("select count (CarBoard_No) from tblCar where CarBoard_No ='" & txtCarNo.Text.Trim & "'")
                        If IfCarExist > 0 Then
                            gAdo.CmdExec("update tblCar set CarBoard_City_ID =" & cboCarCity.SelectedValue & " where Car_ID =" & CarID)
                        Else
                            gAdo.CmdExec("insert into tblCar (CarBoard_No,CarBoard_City_ID,ScaleID) values(" & txtCarNo.Text.Trim & "," & cboCarInfoCity.SelectedValue & ",1)")
                            CarID = gAdo.CmdExecScalar("select max(Car_ID) from tblCar")
                        End If

                        Dim UpdateSQLStatement As String

                        UpdateSQLStatement = "update tblTransactions set Car_ID=" & CarID & " ,Dealer_ID= " & cboDealer.SelectedValue & " ,Car_Info_ID=" & CarInfoID & "" _
                                     & ",Issue_To_ID= " & cboIssueTo.SelectedValue & " ,Driver_ID= " & cboDriver.SelectedValue & " " _
                                     & ",Second_Weight=" & lblSecondScale.Text & " ,Good_ID='" & cboGoodType.SelectedValue & "' "

                        If chkEditOutTime.Checked = True Then
                            UpdateSQLStatement = UpdateSQLStatement & " ,out_date ='" & Out_Date & "'"
                        End If

                        gAdo.CmdExec(UpdateSQLStatement & " where TR_TY=2 and  CID=" & cfrmMain.SharedTxtTransID.Text & " and First_Weigth_Scale_ID=" & cfrmMain.ScaleNo)

                        'gAdo.CmdExec("update tblTransactions set Car_ID=" & CarID & " ,Dealer_ID= " & cboDealer.SelectedValue & " ,Car_Info_ID=" & CarInfoID & "" _
                        '             & ",Issue_To_ID= " & cboIssueTo.SelectedValue & " ,Driver_ID= " & cboDriver.SelectedValue & " " _
                        '             & ",Second_Weight=" & lblSecondScale.Text & " ,Good_ID='" & cboGoodType.SelectedValue & "' " _
                        '             & " where TR_TY=2 and  CID=" & cfrmMain.SharedTxtTransID.Text & " and First_Weigth_Scale_ID=" & cfrmMain.ScaleNo)

                        Dim FailedCount As Integer
                        FailedCount = gAdo.CmdExecScalar("select count(trans_id) from tblFailedTransactions where TR_TY=2 and  cid=" & cfrmMain.SharedTxtTransID.Text & " and First_Weigth_Scale_ID=" & cfrmMain.ScaleNo)
                        If FailedCount = 1 Then
                            'Edit the transactions if it was it was in the failer table of transactions before sendindg it

                            UpdateSQLStatement = "update tblFailedTransactions set Car_ID=" & CarID & " ,Dealer_ID= " & cboDealer.SelectedValue & " ,Car_Info_ID=" & CarInfoID & "" _
                                         & ",Issue_To_ID= " & cboIssueTo.SelectedValue & " ,Driver_ID= " & cboDriver.SelectedValue & " " _
                                         & ",Second_Weight=" & lblSecondScale.Text & " ,Good_ID='" & cboGoodType.SelectedValue & "' "

                            If chkEditOutTime.Checked = True Then
                                UpdateSQLStatement = UpdateSQLStatement & " ,out_date ='" & Out_Date & "'"
                            End If

                            gAdo.CmdExec(UpdateSQLStatement & " where  TR_TY=2 and CID=" & cfrmMain.SharedTxtTransID.Text & " and First_Weigth_Scale_ID=" & cfrmMain.ScaleNo)

                            'gAdo.CmdExec("update tblFailedTransactions set Car_ID=" & CarID & " ,Dealer_ID= " & cboDealer.SelectedValue & " ,Car_Info_ID=" & CarInfoID & "" _
                            '             & ",Issue_To_ID= " & cboIssueTo.SelectedValue & " ,Driver_ID= " & cboDriver.SelectedValue & " " _
                            '             & ",Second_Weight=" & lblSecondScale.Text & " ,Good_ID='" & cboGoodType.SelectedValue & "' ,ManualEdit=1 " _
                            '             & " where  TR_TY=2 and CID=" & cfrmMain.SharedTxtTransID.Text & " and First_Weigth_Scale_ID=" & cfrmMain.ScaleNo)
                        End If

                        'this is thread to update the Server DB
                        Thread = New System.Threading.Thread(AddressOf UpdateServerDB)
                        Control.CheckForIllegalCrossThreadCalls = False
                        Thread.Start()
                        ' end Thread

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

                        MsgBox("تم الحفظ بنجاح")
                        Me.Close()
                    Else
                        Exit Sub
                    End If
                End If

            Else
                Exit Sub
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click

        Me.Close()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Try
            cfrmMain.SharedRichTxt.Text = ""
            cfrmMain.SharedRichTxt.Text = cfrmMain.serl.ReadExisting
            If cfrmMain.SharedRichTxt.Text.LastIndexOf("i") - 14 > 0 Then
                lblSecondScale.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf("i") - 14, 6))
            Else
                lblSecondScale.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf("i") + 3, 6))
            End If

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

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

            If Out_Date.Contains("م") Then
                Out_Date = Out_Date.Replace("م", "PM")
            ElseIf Out_Date.Contains("ص") Then
                Out_Date = Out_Date.Replace("ص", "AM")
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

            cfrmMain.SharedRichTxt.Text = ""
            cfrmMain.SharedRichTxt.Text = cfrmMain.serl.ReadExisting
            If cfrmMain.SharedRichTxt.Text.LastIndexOf("i") - 14 > 0 Then
                lblFirstScale.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf("i") - 14, 6))
            Else
                lblFirstScale.Text = Val(cfrmMain.SharedRichTxt.Text.Substring(cfrmMain.SharedRichTxt.Text.LastIndexOf("i") + 3, 6))
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub cboDriver_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDriver.SelectedValueChanged
        Try
            txtLicence.Text = gAdo.CmdExecScalar("select Driver_License_No from tblDriver where  Driver_ID =" & cboDriver.SelectedValue)
        Catch ex As Exception

        End Try
    End Sub
End Class