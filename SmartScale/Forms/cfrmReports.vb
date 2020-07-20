Imports System.Data.SqlClient
Imports System.IO

Public Class cfrmReports

    Dim mstrStartTime, mstrEndTime As String
    Dim mstrStartDate, mstrEndDate As String
    Dim WhereCondition As String
    Dim TimeTo, TimeFrom As String
    Public Shared ScaleNo As Integer = 1 ' here write the number of scale id
    Dim Scale As String = ""

    Private Sub cfrmReports_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        gfrmChoose.Show()
    End Sub

    Private Sub cfrmReports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
        gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood  where type in(2,3) or Good_ID=1 ORDER BY Good_Name desc", cboGoods, "", "", False, True, " الكــــــــل")
        gAdo.CtrlItemsLoad("LocationID", "LocationName", "tblLocation ORDER BY LocationID", cboLocation, "", "")

        CboRecieveType.Text = "الكل"

        '' Elsaman 15/1/2020 ''
        '' Code For chklstBoxScale 
        gScaleO.ID = cfrmReports.ScaleNo
        gScaleO.Refresh()

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
        'gUserO.ID = gUserID
        'gUserO.Refresh()
        'If gUserO.UserType = "n" Then
        '    chklstBoxScale.Enabled = False
        'End If

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
            'BY Shady
            'Scale = " and First_Weigth_Scale_ID in ( " & Scale & " )"
            Scale = " and Second_Weight_Scale_ID in ( " & Scale & " )"

        End If
    End Sub

    Function ReportWhereCondition(ByVal ParamArray Crtls() As Control) As String
        Dim SelectedScales As String = ""
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
                                                 & StartTime & "' and '" & EndTime & "' "
                        Else
                            WhereCondition = WhereCondition & " where TR_TY=2 and  Out_Date is not null and  convert (datetime,Out_Date)   between '" _
                                                 & GetDate_With_Time(True) & "' and '" & Getlast_With_Time(False) & "' "
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
                Case chklstBoxScale.Name
                    Dim ItemChecked As Object
                    For Each ItemChecked In chklstBoxScale.CheckedItems
                        If SelectedScales = "" Then
                            SelectedScales = ItemChecked.item("Scale_ID").ToString
                        Else
                            SelectedScales = SelectedScales & " , " & ItemChecked.item("Scale_ID").ToString
                        End If
                    Next
                    WhereCondition = WhereCondition & " and Second_Weight_Scale_ID in(" & SelectedScales & ")"
                    'If CboReportScale.SelectedValue <> 0 Then
                    '    WhereCondition = WhereCondition & " and trans.Second_Weight_Scale_ID = " & CboReportScale.SelectedValue & " "
                    'End If
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
                Case cboLocation.Name
                    If cboLocation.SelectedValue <> Nothing And ChkLocation.Checked Then
                        WhereCondition = WhereCondition & "  and trans.LocationID='" & cboLocation.SelectedValue & "' "
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
        
        PrsentScale()
        WhereCondition = ""
        ReportWhereCondition(CboReportShift, chklstBoxScale, CboReportIssueTo, CboReportUser, cboDealer, cboGoods, cboLocation)
        Dim ConnPath, ConnString As String
        ConnPath = Application.StartupPath & "\ConnString.txt"
        ConnString = GetConString(ConnPath)
        'ConnString = Server
        Dim Con As New SqlConnection(ConnString)
        'If CboReportScale.SelectedIndex = 0 Then
        '    WhereCondition = WhereCondition & "and  First_Weigth_Scale_ID =" & cfrmMain.ScaleNo
        'End If
        Dim str As String
        str = " set dateformat dmy select trans_id, convert(nvarchar(max),CID) as cid,D.Dealer_Name,G.Good_Name,IT.Issue_To_Name,C.CarBoard_No,CI.TruckBoard_No,trans.Dealer_ID,trans.Issue_To_ID " _
                                      & ",trans.First_Weigth,trans.Second_Weight,Out_Date,tblScale.Scale_Name,In_Date " _
                                      & "FROM tblTransactions as trans join tblGood as G on trans.Good_ID =G.Good_ID  " _
                                      & "join tblCarInfo as CI on CI.Car_Info_ID=trans.Car_Info_ID  " _
                                      & "join tblCar as C on C.Car_ID =trans.Car_ID " _
                                      & "join tblDealer as D on D.Dealer_ID=trans.Dealer_ID " _
                                      & "join tblIssueTo as IT on IT.Issue_To_ID=trans.Issue_To_ID " _
                                      & "join tblScale on tblScale.Scale_ID = trans.Second_Weight_Scale_ID " & Scale & WhereCondition
        'str &= " set date format dmy SELECT trans_id, 'إذن مرتجع رقم : ' + convert(nvarchar(max),Trans_ID) as CID,D.Dealer_Name,G.Good_Name,IT.Issue_To_Name,C.CarBoard_No,CI.TruckBoard_No,trans.Dealer_ID,trans.Issue_To_ID " _
        '                              & ",trans.First_Weigth,trans.Second_Weight,trans.Out_Date " _
        '                              & "FROM tblReturns as trans join tblGood as G on trans.Good_ID =G.Good_ID  " _
        '                              & "join tblCarInfo as CI on CI.Car_Info_ID=trans.Car_Info_ID  " _
        '                              & "join tblCar as C on C.Car_ID =trans.Car_ID " _
        '                              & "join tblDealer as D on D.Dealer_ID=trans.Dealer_ID " _
        '                              & "join tblIssueTo as IT on IT.Issue_To_ID=trans.Issue_To_ID  " & WhereCondition

        Dim Adp As New SqlDataAdapter(str, Con)
        Dim strScaleName As String = ""
        'For i = 0 To chklstBoxScale.Items.Count - 1

        '    If chklstBoxScale.CheckedItems.Item(i).Then Then
        '        strScaleName = strScaleName + " " & chklstBoxScale.Items(i).ToString
        '    End If

        'Next
        Dim count = 0
        For i = 0 To chklstBoxScale.Items.Count - 1
            If (chklstBoxScale.GetItemChecked(i)) Then
                If count = 0 Then
                    strScaleName = chklstBoxScale.Items(i)(1).ToString
                Else
                    strScaleName = strScaleName & " , " & chklstBoxScale.Items(i)(1).ToString

                End If
                'For Each itemChecked In chklstBoxScale.CheckedItems
                '    strScaleName &= chklstBoxScale.Items(0).ToString
                'Next

                count += 1
            End If
        Next
        Dim DT As New DataTable
        Adp.Fill(DT)
        Dim Rpt As New RptTransactionwithRec
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


        Rpt.SetParameterValue("Scale_Name", strScaleName)
        Rpt.SetParameterValue("LocationName", If(ChkLocation.Checked, cboLocation.Text, "الكل"))

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

    Private Sub ChkLocation_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkLocation.CheckedChanged
        If (ChkLocation.Checked) Then
            cboLocation.Visible = True
        Else : cboLocation.Visible = False
        End If
    End Sub
End Class