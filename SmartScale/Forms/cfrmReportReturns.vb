Imports System.Data.SqlClient
Imports System.IO

Public Class cfrmReportReturns

    Dim mstrStartTime, mstrEndTime As String
    Dim mstrStartDate, mstrEndDate As String
    Dim WhereCondition As String
    Dim TimeTo, TimeFrom As String

    Private Sub cfrmReportReturns_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        gfrmChoose.Show()
    End Sub

    Private Sub cfrmReportReturns_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'In Shift To Fill CboShift
        gAdo.CtrlItemsLoad("Shift_ID", "Shift_Name", "tblShift", CboReportShift, "", "Shift_IsDeleted='false' ORDER BY Shift_Name", False, True, "الكــــــــل ")
        'In Scale To Fill CboScale
        gAdo.CtrlItemsLoad("Scale_ID", "Scale_Name", "tblScale", CboReportScale, "", "scale_IsDeleted='false' ORDER BY Scale_Name", False, True, "الكــــــــل")
        'In IssueTo To Fill CboIssueTo
        gAdo.CtrlItemsLoad("Issue_To_ID", "Issue_To_Name", "tblIssueTo  ORDER BY Issue_To_Name", CboReportIssueTo, "", "", False, True, " الكــــــــل")
        ' In User To Fill CboUser
        gAdo.CtrlItemsLoad("User_ID", "User_Name", "tblUser", CboReportUser, "", "User_IsDeleted='false' and user_id > 1 ORDER BY User_Name", False, True, " الكــــــــل ")

        gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", " TblDealer where Type=1 ORDER BY Dealer_Name ", cboDealer, "", "", False, True, " الكــــــــل")

        'gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood   where  (Good_ID like '07'+'%' or  good_ID =1 ) and Good_ID in ('0705','0706','0711','0712','0713','0714','0715') or good_ID =1  ORDER BY Good_Name desc", cboGoods, "", "", False, True, " الكــــــــل")
        gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood  where type in (2,3) or Good_ID=1 ORDER BY Good_Name desc", cboGoods, "", "", False, True, " الكــــــــل")

        CboRecieveType.Text = "الكل"
    End Sub

    Function ReportWhereCondition(ByVal ParamArray Crtls() As Control) As String

        If chkTime.Checked Then
            If chkNextDay.Checked = True Then
                TimeFrom = timFrom.TimeValue
                TimeFrom = TimeFrom.Replace("م", "PM")
                TimeFrom = TimeFrom.Replace("ص", "AM")
                TimeFrom = TimeFrom.Substring(11)
                TimeTo = timTO.TimeValue
                TimeTo = TimeTo.Replace("م", "PM")
                TimeTo = TimeTo.Replace("ص", "AM")
                TimeTo = TimeTo.Substring(11)
                Dim TimeNextDayTo As String
                TimeNextDayTo = Now.Date.AddDays(1) & TimeTo.Substring(10)
                WhereCondition = ""
                'WhereCondition = WhereCondition & " where TR_TY =1 and Out_Date is not null and   First_Weigth_Scale_ID =" & cfrmMain.ScaleNo & " and convert (datetime,trans.In_Shift_Date) between '" & dtpCarFrom.Value.Date & " " & TimeFrom & "' and '" & dtpCarTo.Value.Date.AddDays(1) & " " & TimeTo & "' and trans.Car_ID =" & cboCar.SelectedValue
            Else
                TimeFrom = timFrom.TimeValue
                TimeFrom = TimeFrom.Replace("م", "PM")
                TimeFrom = TimeFrom.Replace("ص", "AM")
                TimeFrom = TimeFrom.Substring(11)
                TimeTo = timTO.TimeValue
                TimeTo = TimeTo.Replace("م", "PM")
                TimeTo = TimeTo.Replace("ص", "AM")
                TimeTo = TimeTo.Substring(11)
                WhereCondition = ""
                'WhereCondition = WhereCondition & " where  TR_TY =1 and Out_Date is not null and  First_Weigth_Scale_ID =" & cfrmMain.ScaleNo & " and convert (datetime,trans.In_Shift_Date) between '" & dtpCarFrom.Value.Date & " " & TimeFrom & "' and '" & dtpCarTo.Value.Date.AddDays(1) & " " & TimeTo & "' and trans.Car_ID =" & cboCar.SelectedValue
            End If
        End If

        For i = 0 To UBound(Crtls)
            Select Case Crtls(i).Name
                Case CboReportShift.Name
                    If CboReportShift.SelectedValue <> 0 Then
                        'WhereCondition = WhereCondition & " where trans.In_Date  between '" _
                        '& GetDate_With_Time(True) & "' and '" & GetDate_With_Time(False) & "' "
                        Dim StartTime, EndTime As String
                        StartTime = GetDate_With_Time(True)
                        StartTime = StartTime.Substring(0, 12)
                        StartTime = StartTime & TimeFrom
                        EndTime = Getlast_With_Time(False)
                        EndTime = EndTime.Substring(0, 12)
                        EndTime = EndTime & TimeTo
                        If chkTime.Checked = True Then
                            WhereCondition = WhereCondition & " where TR_TY=2 and  Out_Date is not null and  convert (datetime,Out_Date)   between '" _
                                                 & StartTime & "' and '" & EndTime & "' and In_Shift_ID =" & CboReportShift.SelectedValue
                        Else
                            WhereCondition = WhereCondition & " where TR_TY=2 and  Out_Date is not null and  convert (datetime,Out_Date)   between '" _
                                                 & GetDate_With_Time(True) & "' and '" & Getlast_With_Time(False) & "' and In_Shift_ID =" & CboReportShift.SelectedValue
                        End If
                    Else
                        If chkTime.Checked = True Then
                            WhereCondition = " Where  TR_TY =2 and Out_Date is not null and  convert (datetime,Out_Date) Between '" & DTRepot.Value.Date & " " & TimeFrom & "' and '" _
                                            & dtTo.Value.Date & " " & TimeTo & "'"
                        Else
                            WhereCondition = " Where  TR_TY=2 and  Out_Date is not null and  CONVERT(datetime, Out_Date) BETWEEN  '" & gMethods.GetShifName_And_SearchDateFT(True, DTRepot.Value.Date, dtTo.Value.Date)(0) & "' and " & _
                  "'" & gMethods.GetShifName_And_SearchDateFT(True, DTRepot.Value.Date, dtTo.Value.Date)(1) & "'  "

                        End If

                    End If
                Case CboReportScale.Name
                    If CboReportScale.SelectedValue <> 0 Then
                        WhereCondition = WhereCondition & " and trans.First_Weigth_Scale_ID = " & CboReportScale.SelectedValue & " "
                    End If
                Case CboReportIssueTo.Name
                    If CboReportIssueTo.SelectedValue <> 0 Then
                        WhereCondition = WhereCondition & " and trans.Issue_To_ID=" & CboReportIssueTo.SelectedValue & " "
                    End If
                Case CboReportUser.Name
                    If CboReportUser.SelectedValue <> 0 Then
                        WhereCondition = WhereCondition & "  and trans.First_Weight_User_ID=" & CboReportUser.SelectedValue & " "
                    End If
                Case cboDealer.Name
                    If cboDealer.SelectedValue <> 0 Then
                        WhereCondition = WhereCondition & "  and trans.Dealer_ID='" & cboDealer.SelectedValue & "' "
                    End If
                Case cboGoods.Name
                    If cboGoods.SelectedValue <> 0 Then
                        WhereCondition = WhereCondition & "  and trans.Good_ID='" & cboGoods.SelectedValue & "' "
                    End If
            End Select
        Next


        If CboRecieveType.Text = "العامرية" Then
            WhereCondition = WhereCondition & " and MSGID='2' "
        ElseIf CboRecieveType.Text = "الإسكندرية" Then
            WhereCondition = WhereCondition & " and MSGID='1' "
        ElseIf CboRecieveType.Text = "" Then
            WhereCondition = WhereCondition & " and MSGID='4' "
        Else

        End If
        If WhereCondition.Contains("م") Then
            WhereCondition = WhereCondition.Replace("م", "PM")
        End If
        If WhereCondition.Contains("ص") Then
            WhereCondition = WhereCondition.Replace("ص", "AM")
        End If
        Return WhereCondition

    End Function

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click

        WhereCondition = ""
        ReportWhereCondition(CboReportShift, CboReportScale, CboReportIssueTo, CboReportUser, cboDealer, cboGoods)
        Dim ConnPath, ConnString As String
        ConnPath = Application.StartupPath & "\ConnString.txt"
        ConnString = GetConString(ConnPath)
        'ConnString = Server
        Dim Con As New SqlConnection(ConnString)
        'If CboReportScale.SelectedIndex = 0 Then
        '    WhereCondition = WhereCondition & "and  First_Weigth_Scale_ID =" & cfrmMain.ScaleNo
        'End If
        Dim str As String
   
        str = " set dateformat dmy SELECT trans_id, 'إذن مرتجع رقم : ' + convert(nvarchar(max),Trans_ID) as CID,D.Dealer_Name,G.Good_Name,IT.Issue_To_Name,C.CarBoard_No,CI.TruckBoard_No,trans.Dealer_ID,trans.Issue_To_ID " _
                                      & ",trans.First_Weigth,trans.Second_Weight,trans.Out_Date " _
                                      & "FROM tblReturns as trans join tblGood as G on trans.Good_ID =G.Good_ID  " _
                                      & "join tblCarInfo as CI on CI.Car_Info_ID=trans.Car_Info_ID  " _
                                      & "join tblCar as C on C.Car_ID =trans.Car_ID " _
                                      & "join tblDealer as D on D.Dealer_ID=trans.Dealer_ID " _
                                      & "join tblIssueTo as IT on IT.Issue_To_ID=trans.Issue_To_ID  " & WhereCondition

        Dim Adp As New SqlDataAdapter(str, Con)

        Dim DT As New DataTable
        Adp.Fill(DT)
        Dim Rpt As New RptTransactionwithRecRet
        Rpt.SetDataSource(DT)
        Rpt.SetParameterValue("Date", DTRepot.Value.Date)
        Rpt.SetParameterValue("IssueTo", CboReportIssueTo.Text)
        Rpt.SetParameterValue("Scale", CboReportScale.Text)
        Rpt.SetParameterValue("Shift", CboReportShift.Text)
        Rpt.SetParameterValue("User", CboReportUser.Text)
        Rpt.SetParameterValue("Customers", cboDealer.Text)
        Rpt.SetParameterValue("DateFrom", DTRepot.Value.Date)
        Rpt.SetParameterValue("DateTo", dtTo.Value.Date)
        Rpt.SetParameterValue("ReceiveType", CboRecieveType.Text)
        CrvTransactionRepot.ReportSource = Rpt

    End Sub

    Private Sub CboReportShift_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboReportShift.SelectedIndexChanged
        If CboReportShift.SelectedValue <> 0 Then
            gShiftO.ID = CboReportShift.SelectedValue
            gShiftO.Refresh()

            'To Get Start Time From Shift_Start_Date Column in TblShift
            mstrStartTime = Mid(gShiftO.Shift_Start_Time, 12, 11)
            'To Get End Time from Shift_End_Date Column in TblShift
            mstrEndTime = Mid(gShiftO.Shift_End_Time, 12, 11)
        Else
            'mstrStartTime = ""
            'mstrEndTime = ""
        End If

    End Sub

    Function GetDate_With_Time(ByVal Type As Boolean) As String
        Dim Shift_Start_Date As Boolean
        Dim Shift_End_Date As Boolean

        Dim DT As DataTable = gAdo.FillDT("select Shift_ID,Shift_Start_Date,Shift_End_Date from tblShift " _
                      & "where Shift_ID=" & CboReportShift.SelectedValue)
        Shift_Start_Date = DT.Rows(0).Item(1)
        Shift_End_Date = DT.Rows(0).Item(2)
        'الورديه الواقعه فى اليوم التالى 
        If Shift_Start_Date = False And Shift_End_Date = True Then
            If Type = True Then
                mstrStartDate = DTRepot.Value.Date & " " & mstrStartTime
                Return mstrStartDate
            Else
                mstrEndDate = DTRepot.Value.Date.AddDays(+1) & " " & mstrEndTime
                Return mstrEndDate
            End If
        Else
            'الورديه الواقعه فى نفس اليوم
            If Type = True Then
                mstrStartDate = DTRepot.Value.Date & " " & mstrStartTime
                Return mstrStartDate
            Else
                mstrEndDate = DTRepot.Value.Date & " " & mstrEndTime
                Return mstrEndDate
            End If
        End If

    End Function

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

    Private Sub BtnSearch_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.MouseEnter
        sender.BackColor = Color.SkyBlue
    End Sub

    Private Sub BtnSearch_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.MouseLeave
        sender.BackColor = Color.Gainsboro
    End Sub

    Function Getlast_With_Time(ByVal Type As Boolean) As String
        Dim Shift_Start_Date As Boolean
        Dim Shift_End_Date As Boolean

        Dim DT As DataTable = gAdo.FillDT("select Shift_ID,Shift_Start_Date,Shift_End_Date from tblShift " _
                      & "where Shift_ID=" & CboReportShift.SelectedValue)

        Shift_Start_Date = DT.Rows(0).Item(1)
        Shift_End_Date = DT.Rows(0).Item(2)

        'الورديه الواقعه فى اليوم التالى 
        If Shift_Start_Date = False And Shift_End_Date = True Then
            If Type = True Then
                mstrStartDate = dtTo.Value.Date & " " & mstrStartTime
                Return mstrStartDate
            Else
                mstrEndDate = dtTo.Value.Date.AddDays(+1) & " " & mstrEndTime
                Return mstrEndDate
            End If
        Else
            'الورديه الواقعه فى نفس اليوم

            If Type = True Then
                mstrStartDate = dtTo.Value.Date & " " & mstrStartTime
                Return mstrStartDate
            Else
                mstrEndDate = dtTo.Value.Date & " " & mstrEndTime
                Return mstrEndDate

            End If

        End If
    End Function

    Private Sub chkTime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTime.CheckedChanged

        If chkTime.Checked = True Then
            chkTime.ForeColor = Color.Red
            timFrom.Visible = True
            timTO.Visible = True
            chkNextDay.Visible = True
        Else
            chkTime.ForeColor = Color.White
            timFrom.Visible = False
            timTO.Visible = False
            chkNextDay.Visible = False
        End If

    End Sub

    Private Sub timTO_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timTO.ValueChanged

    End Sub
    Private Sub timFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timFrom.ValueChanged

    End Sub
    Private Sub Label12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label12.Click

    End Sub
    Private Sub chkNextDay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNextDay.CheckedChanged

    End Sub

    Private Sub cfrmReportReturns_Load_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class