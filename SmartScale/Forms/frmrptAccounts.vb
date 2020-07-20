Imports System.Data.SqlClient
Public Class frmrptAccounts
    Dim mstrStartTime, mstrEndTime As String
    Dim mstrStartDate, mstrEndDate As String
    Dim WhereCondition As String
    Dim TimeTo, TimeFrom As String

    Private Sub frmrptAccounts_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            'In Shift To Fill CboShift
            gAdo.CtrlItemsLoad("Shift_ID", "Shift_Name", "tblShift", CboReportShift, "", "Shift_IsDeleted='false' ORDER BY Shift_Name", False, True, "الكــــــــل ")
            'In Scale To Fill CboScale
            gAdo.CtrlItemsLoad("Scale_ID", "Scale_Name", "tblScale", CboReportScale, "", "scale_IsDeleted='false' ORDER BY Scale_Name", False, True, "الكــــــــل")
            'In IssueTo To Fill CboIssueTo
            gAdo.CtrlItemsLoad("Issue_To_ID", "Issue_To_Name", "tblIssueTo  ORDER BY Issue_To_Name", CboReportIssueTo, "", "", False, True, " الكــــــــل")
            ' In User To Fill CboUser
            gAdo.CtrlItemsLoad("User_ID", "User_Name", "tblUser", CboReportUser, "", "User_IsDeleted='false' and user_id > 1 ORDER BY User_Name", False, True, " الكــــــــل ")

            gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", " TblDealer where Type=1 ORDER BY Dealer_Name ", cboDealer, "", "", False, True, " الكــــــــل")

            gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood   where (type in(2,3) or Good_ID =1) ORDER BY Good_Name desc", cboGoods, "", "", False, True, " الكــــــــل")
            'gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood   where  (Good_ID like '07'+'%' or  good_ID =1 ) and Good_ID in ('0705','0706','0711','0712','0713','0714','0715') or good_ID =1  ORDER BY Good_Name desc", cboGoods, "", "", False, True, " الكــــــــل")
            crv.ReportSource = Nothing
            'gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood   where  Type = 2 ORDER BY Good_Name desc", chklstBoxGood, "", "", False, True)

            chklstBoxGood.DataSource = GObjDataBaseS.FillDT("select * from tblGood where Type in(2,3) or Good_ID=1")
            'chklstBoxGood.DataSource = GObjDataBaseS.FillDT("select * from tblGood where Type = 2")

            chklstBoxScale.DataSource = GObjDataBaseS.FillDT("select * from tblScale where Scale_IsDeleted = 0")
            chklstBoxScale.DisplayMember = "Scale_Name"
            chklstBoxScale.ValueMember = "Scale_ID"

            Dim dtCheckedScale As New DataTable

            If cfrmReports.ScaleNo = 0 Then
            Else
                Dim Locatin As Integer = gAdo.CmdExecScalar("select Location from tblScale where Scale_IsDeleted = 0 and Scale_ID = " & cfrmReports.ScaleNo)
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

            gAdo.CtrlItemsLoad("LocationID", "LocationName", "tblLocation ORDER BY LocationID", cboLocation, "", "")

            '''' 28/01/2019 ''''
            '''' Elsaman ''''
            'Dim connection As New SqlConnection("Data Source=(local);Initial Catalog=MultiSeeds2017;User ID=it;Password=123")
            'Dim command As New SqlCommand("select * from tblGood where Type = 2")
            'command.Connection = connection

            'Dim adapter As New SqlDataAdapter(command)
            'Dim dt2 As New DataTable()

            'dt2 = gAdo.FillDT("select * from tblGood where Type = 2")

            'chklstBoxGood.DataSource = dt2

            chklstBoxGood.DisplayMember = "Good_Name"
            chklstBoxGood.ValueMember = "Good_ID"

            If CheckBoxGood.Checked = True Then
                chklstBoxGood.Visible = True
            Else
                chklstBoxGood.Visible = False

            End If

        Catch ex As Exception

        End Try
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
                                                 & StartTime & "' and '" & EndTime & "' "
                        Else
10:                         WhereCondition = WhereCondition & " where TR_TY=2 and  Out_Date is not null and  convert (datetime,Out_Date)   between '" _
                                                 & GetDate_With_Time(True) & "' and '" & Getlast_With_Time(False) & "' "
                        End If
                    Else
                        If chkTime.Checked = True Then
                            WhereCondition = " Where  TR_TY =2 and Out_Date is not null and  convert (datetime,Out_Date) Between '" & DTRepot.Value.Date & " " & TimeFrom & "' and '" _
                                            & dtTo.Value.Date & " " & TimeTo & "'"
                        Else
                            WhereCondition = " Where  TR_TY=2 and  Out_Date is not null and  CONVERT(datetime, Out_Date) BETWEEN  '" & gMethods.GetShifName_And_SearchDate(True)(0) & "' and " & _
                                      "'" & gMethods.GetShifName_And_SearchDate(True)(1) & "'"
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
        If WhereCondition.Contains("م") Then
            WhereCondition = WhereCondition.Replace("م", "PM")
        End If
        If WhereCondition.Contains("ص") Then
            WhereCondition = WhereCondition.Replace("ص", "AM")
        End If
        Return WhereCondition

    End Function

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        WhereCondition = " and tr_ty=2  "
        'ReportWhereCondition(CboReportShift, CboReportScale, CboReportIssueTo, CboReportUser, cboDealer, cboGoods)
        Dim ConnPath, ConnString As String
        ConnPath = Application.StartupPath & "\ConnString.txt"
        ConnString = GetConString(ConnPath)
        'ConnString = Server
        Dim Con As New SqlConnection(ConnString)
        'If CboReportScale.SelectedIndex = 0 Then
        '    WhereCondition = WhereCondition & "where tr_ty=2 and  First_Weigth_Scale_ID =" & cfrmMain.ScaleNo
        'End If
        Dim str As String = CboReportShift.Text
        Dim dd As String
        dd = CboReportShift.SelectedIndex
        If CboReportShift.SelectedIndex = 0 Then
            WhereCondition = WhereCondition & "  and CONVERT(datetime, trans.Out_Date) BETWEEN  '" & gMethods.GetShifName_And_SearchDateFT(True, DTRepot.Value.Date, dtTo.Value.Date)(0) & "' and " & _
             "'" & gMethods.GetShifName_And_SearchDateFT(True, DTRepot.Value.Date, dtTo.Value.Date)(1) & "' "
            'WhereCondition = " and CONVERT(datetime, Out_Date) BETWEEN  '" & gMethods.GetShifName_And_SearchDate(True)(0) & "' and " & _
            '"'" & gMethods.GetShifName_And_SearchDate(True)(1) & "'"
        ElseIf CboReportShift.SelectedIndex = 1 Then
            WhereCondition = WhereCondition & "  and CONVERT(datetime, trans.Out_Date) BETWEEN  '" & gMethods.MorniningShift(True, DTRepot.Value.Date, dtTo.Value.Date)(0) & "' and " & _
                      "'" & gMethods.MorniningShift(True, DTRepot.Value.Date, dtTo.Value.Date)(1) & "'  "
        ElseIf CboReportShift.SelectedIndex = 2 Then
            WhereCondition = WhereCondition & "  and CONVERT(datetime, trans.Out_Date) BETWEEN  '" & gMethods.EveninigShift(True, DTRepot.Value.Date, dtTo.Value.Date)(0) & "' and " & _
                  "'" & gMethods.EveninigShift(True, DTRepot.Value.Date, dtTo.Value.Date)(1) & "' "
        End If

        'If CboReportScale.Text = "الكــــــــل" Then

        'Else
        '    WhereCondition = WhereCondition & "and  First_Weigth_Scale_ID =" & CboReportScale.SelectedValue
        'End If

        'Dim Adp As New SqlDataAdapter(" set dateformat dmy SELECT trans.CID,D.Dealer_Name,G.Good_Name,Itrans.Issue_To_Name,C.CarBoard_No,CI.TruckBoard_No,trans.Dealer_ID,trans.Issue_To_ID " _
        '                             & ",trans.First_Weigth,trans.Second_Weight,Out_Date " _
        '                             & "FROM tblTransactions as T join tblGood as G on trans.Good_ID =G.Good_ID  " _
        '                             & "join tblCarInfo as CI on CI.Car_Info_ID=trans.Car_Info_ID  " _
        '                             & "join tblCar as C on C.Car_ID =trans.Car_ID " _
        '                             & "join tblDealer as D on D.Dealer_ID=trans.Dealer_ID " _
        '                             & "join tblIssueTo as IT on Itrans.Issue_To_ID=trans.Issue_To_ID  " & WhereCondition, Con)

        'Dim DT As New DataTable
        'Adp.Fill(DT)
        'Dim Rpt As New rptAccounts
        'rpt.SetDataSource(DT)


        'Dim Where As String = ""
        'Where = "and convert (datetime,trans.IN_Date) between '" & Now.Date.AddDays(-5) & " 08:00:00 AM' and '" & Now.Date.AddDays(5) & " 07:59:59 AM' "



        ''''29/01/2019 ''''
        '''' Elsaman ''''

        Dim a As String = ""

        If CheckBoxGood.Checked = True Then
            If chklstBoxGood.CheckedItems.Count = 0 Then
                MsgBox(" الرجاء اختيار صنف  ")
                Exit Sub
            Else
                Dim ItemChecked As Object
                For Each ItemChecked In chklstBoxGood.CheckedItems
                    'MsgBox(ItemChecked.item("Good_ID").ToString)
                    If a = "" Then
                        a = ItemChecked.item("Good_ID").ToString
                    Else
                        a = a & " , " & ItemChecked.item("Good_ID").ToString
                    End If
                Next
                'a = a & " , " & ItemChecked.item("Good_ID").ToString
                a = " and trans.Good_ID in ( " & a & " )"

            End If
        Else

        End If

        WhereCondition &= a

        WhereCondition += (If(cboLocation.SelectedValue <> Nothing And ChkLocation.Checked, " and trans.LocationID=" & cboLocation.SelectedValue, ""))
        Dim SelectedScales As String = ""

        For Each ItemChecked In chklstBoxScale.CheckedItems
            If SelectedScales = "" Then
                SelectedScales = ItemChecked.item("Scale_ID").ToString
            Else
                SelectedScales = SelectedScales & " , " & ItemChecked.item("Scale_ID").ToString
            End If
        Next
        WhereCondition = WhereCondition & " and Second_Weight_Scale_ID in(" & SelectedScales & ")"




        Dim SQLStatement1, SQLStatement2, SQLStatement As String
        SQLStatement1 = " set dateformat dmy select trans_id, convert(nvarchar(max),CID) as cid,D.Dealer_Name,G.Good_Name,IT.Issue_To_Name,C.CarBoard_No,CI.TruckBoard_No,trans.Dealer_ID,trans.Issue_To_ID,trans.Slip_Rate " _
                                     & ",trans.First_Weigth,trans.Second_Weight,Out_Date " _
                                     & "FROM tblTransactions as trans join tblGood as G on trans.Good_ID =G.Good_ID  " _
                                     & "join tblCarInfo as CI on CI.Car_Info_ID=trans.Car_Info_ID  " _
                                     & "join tblCar as C on C.Car_ID =trans.Car_ID " _
                                     & "join tblDealer as D on D.Dealer_ID=trans.Dealer_ID " _
                                     & "join tblIssueTo as IT on IT.Issue_To_ID=trans.Issue_To_ID where  TR_TY =2 " & WhereCondition & " and out_date is not  null and  trans.Issue_To_id not in(17,21) "

        SQLStatement2 = "SELECT trans_id, 'إذن مرتجع رقم : ' + convert(nvarchar(max),Trans_ID) as CID,D.Dealer_Name,G.Good_Name,IT.Issue_To_Name,C.CarBoard_No,CI.TruckBoard_No,trans.Dealer_ID,trans.Issue_To_ID,trans.Slip_Rate " _
                                     & ",trans.First_Weigth,trans.Second_Weight,Out_Date " _
                                     & "FROM tblReturns as trans join tblGood as G on trans.Good_ID =G.Good_ID  " _
                                     & "join tblCarInfo as CI on CI.Car_Info_ID=trans.Car_Info_ID  " _
                                     & "join tblCar as C on C.Car_ID =trans.Car_ID " _
                                     & "join tblDealer as D on D.Dealer_ID=trans.Dealer_ID " _
                                     & "join tblIssueTo as IT on IT.Issue_To_ID=trans.Issue_To_ID where  TR_TY =2 " & WhereCondition & " and out_date is not  null and  trans.Issue_To_id not in(17,21) order by trans_id   "


        If cboDealer.SelectedIndex = 0 Then
        Else
            SQLStatement1 = SQLStatement1 & " and trans.Dealer_ID='" & cboDealer.SelectedValue & "'"
            SQLStatement2 = SQLStatement2 & " and trans.Dealer_ID='" & cboDealer.SelectedValue & "'"
        End If



        SQLStatement = SQLStatement1 & " Union " & SQLStatement2
        Dim Adp As New SqlDataAdapter(SQLStatement, Con)
        Dim DT As New DataTable
        Adp.Fill(DT)

        '''' 25/12/2018 ''''
        '''' Elsaman ''''

        'Dim dtSlip As New DataTable

        'GObjDataBaseS.FillDT("select Slip_Rate  from tblSlip where Slip_Rate > 0 order by Slip_Rate", dtSlip)

        Dim SRateF As Decimal = GObjDataBaseS.ExecuteScaler("select Slip_Rate  from tblSlip where Slip_ID=3")
        Dim SRateGR As Decimal = GObjDataBaseS.ExecuteScaler("select Slip_Rate  from tblSlip where Slip_ID=1")
        Dim SRateJA As Decimal = GObjDataBaseS.ExecuteScaler("select Slip_Rate  from tblSlip where Slip_ID=2")

        Dim intFaradany As Integer = GObjDataBaseS.ExecuteScaler(" set dateformat dmy select count(trans.Trans_ID) as Trans_ID " & _
                       " FROM tblTransactions as trans join tblGood as G on trans.Good_ID =G.Good_ID " & _
                       " join tblCarInfo as CI on CI.Car_Info_ID=trans.Car_Info_ID  join tblCar as C on C.Car_ID =trans.Car_ID " & _
                       " join tblDealer as D on D.Dealer_ID=trans.Dealer_ID join tblIssueTo as IT on IT.Issue_To_ID=trans.Issue_To_ID " & _
                       " where  TR_TY =2  " & WhereCondition & " and out_date is not  null and Slip_Rate = " & SRateF.ToString())

        intFaradany += GObjDataBaseS.ExecuteScaler(" set dateformat dmy SELECT count(trans.Trans_ID)FROM tblReturns as trans join tblGood as G on trans.Good_ID =G.Good_ID " & _
                       " join tblCarInfo as CI on CI.Car_Info_ID=trans.Car_Info_ID " & _
                       " join tblCar as C on C.Car_ID =trans.Car_ID join tblDealer as D on D.Dealer_ID=trans.Dealer_ID " & _
                       " join tblIssueTo as IT on IT.Issue_To_ID=trans.Issue_To_ID where  TR_TY =2  " & WhereCondition & _
                       " and out_date is not  null and Slip_Rate = " & SRateF)


        Dim intGararat As Integer = GObjDataBaseS.ExecuteScaler(" set dateformat dmy select count(trans.Trans_ID) as Trans_ID " & _
                       " FROM tblTransactions as trans join tblGood as G on trans.Good_ID =G.Good_ID " & _
                       " join tblCarInfo as CI on CI.Car_Info_ID=trans.Car_Info_ID  join tblCar as C on C.Car_ID =trans.Car_ID " & _
                       " join tblDealer as D on D.Dealer_ID=trans.Dealer_ID join tblIssueTo as IT on IT.Issue_To_ID=trans.Issue_To_ID " & _
                       " where  TR_TY =2 " & WhereCondition & " and out_date is not  null and Slip_Rate = " & SRateGR)

        intGararat += GObjDataBaseS.ExecuteScaler("set dateformat dmy SELECT count(trans.Trans_ID)FROM tblReturns as trans join tblGood as G on trans.Good_ID =G.Good_ID " & _
                       " join tblCarInfo as CI on CI.Car_Info_ID=trans.Car_Info_ID " & _
                       " join tblCar as C on C.Car_ID =trans.Car_ID join tblDealer as D on D.Dealer_ID=trans.Dealer_ID " & _
                       " join tblIssueTo as IT on IT.Issue_To_ID=trans.Issue_To_ID where  TR_TY =2 " & WhereCondition & _
                       " and out_date is not  null  and Slip_Rate = " & SRateGR)


        Dim intJumbo As Integer = GObjDataBaseS.ExecuteScaler(" set dateformat dmy select count(trans.Trans_ID) as Trans_ID " & _
                       " FROM tblTransactions as trans join tblGood as G on trans.Good_ID =G.Good_ID " & _
                       " join tblCarInfo as CI on CI.Car_Info_ID=trans.Car_Info_ID  join tblCar as C on C.Car_ID =trans.Car_ID " & _
                       " join tblDealer as D on D.Dealer_ID=trans.Dealer_ID join tblIssueTo as IT on IT.Issue_To_ID=trans.Issue_To_ID " & _
                       " where  TR_TY =2 " & WhereCondition & " and out_date is not  null and Slip_Rate = " & SRateJA)

        intJumbo += GObjDataBaseS.ExecuteScaler("set dateformat dmy SELECT count(trans.Trans_ID)FROM tblReturns as trans join tblGood as G on trans.Good_ID =G.Good_ID " & _
                       " join tblCarInfo as CI on CI.Car_Info_ID=trans.Car_Info_ID " & _
                       " join tblCar as C on C.Car_ID =trans.Car_ID join tblDealer as D on D.Dealer_ID=trans.Dealer_ID " & _
                       " join tblIssueTo as IT on IT.Issue_To_ID=trans.Issue_To_ID where  TR_TY =2 " & WhereCondition & _
                       " and out_date is not  null   and Slip_Rate = " & SRateJA)

        Dim Without As Integer = GObjDataBaseS.ExecuteScaler(" set dateformat dmy select count(trans.Trans_ID) as Trans_ID " & _
                       " FROM tblTransactions as trans join tblGood as G on trans.Good_ID =G.Good_ID " & _
                       " join tblCarInfo as CI on CI.Car_Info_ID=trans.Car_Info_ID  join tblCar as C on C.Car_ID =trans.Car_ID " & _
                       " join tblDealer as D on D.Dealer_ID=trans.Dealer_ID join tblIssueTo as IT on IT.Issue_To_ID=trans.Issue_To_ID " & _
                       " where  TR_TY =2 " & WhereCondition & " and out_date is not  null and Slip_Rate = 0 ")

        Without += GObjDataBaseS.ExecuteScaler("set dateformat dmy SELECT count(trans.Trans_ID) FROM tblReturns as trans join tblGood as G on trans.Good_ID =G.Good_ID " & _
                       " join tblCarInfo as CI on CI.Car_Info_ID=trans.Car_Info_ID " & _
                       " join tblCar as C on C.Car_ID =trans.Car_ID join tblDealer as D on D.Dealer_ID=trans.Dealer_ID " & _
                       " join tblIssueTo as IT on IT.Issue_To_ID=trans.Issue_To_ID where  TR_TY =2 " & WhereCondition & _
                       " and out_date is not  null   and Slip_Rate = 0 ")

        Dim strScaleName As String = ""
        Dim count = 0
        For i = 0 To chklstBoxScale.Items.Count - 1
            If (chklstBoxScale.GetItemChecked(i)) Then
                If count = 0 Then
                    strScaleName = chklstBoxScale.Items(i)(1).ToString
                Else
                    strScaleName = strScaleName & " , " & chklstBoxScale.Items(i)(1).ToString

                End If

                count += 1
            End If
        Next


        Dim rpt As New rptAccounts
        rpt.SetDataSource(DT)
        rpt.SetParameterValue("Date", DTRepot.Value.Date)
        'rpt.SetParameterValue("IssueTo", CboReportIssueTo.Text)
        'rpt.SetParameterValue("Scale", CboReportScale.Text)
        rpt.SetParameterValue("Shift", CboReportShift.Text)
        rpt.SetParameterValue("Scale", CboReportScale.Text)
        'rpt.SetParameterValue("User", CboReportUser.Text)
        'rpt.SetParameterValue("Customers", cboDealer.Text)
        'rpt.SetParameterValue("DateFrom", DTRepot.Value.Date)
        'rpt.SetParameterValue("DateTo", dtTo.Value.Date)
        rpt.SetParameterValue("@where", WhereCondition)
        rpt.SetParameterValue("@where2", WhereCondition & " order by trans_id ")
        rpt.SetParameterValue("Slip_Rate1", intFaradany)
        rpt.SetParameterValue("Slip_Rate2", intGararat)
        rpt.SetParameterValue("Slip_Rate3", intJumbo)
        rpt.SetParameterValue("Without", Without)
        rpt.SetParameterValue("Scale_Name", strScaleName)
        rpt.SetParameterValue("LocationName", If(cboLocation.SelectedValue <> Nothing And ChkLocation.Checked, cboLocation.Text, "الكل"))
        rpt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4


        grptpth.ReportPath(rpt, crv)


        'crv.ReportSource = Rpt

    End Sub

    Private Sub crv_AutoSizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles crv.AutoSizeChanged

    End Sub

    Private Sub crv_ChangeUICues(ByVal sender As Object, ByVal e As System.Windows.Forms.UICuesEventArgs) Handles crv.ChangeUICues

    End Sub

    Private Sub crv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles crv.Click
        Try

            Dim y = DirectCast(sender, CrystalDecisions.Windows.Forms.CrystalReportViewer).ActiveControl.Text
            Dim x = 1
            Dim ob = sender
            Dim e_ = e

        Catch ex As Exception

        End Try
    End Sub

    Private Sub crv_CursorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles crv.CursorChanged

    End Sub

    Private Sub crv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles crv.DoubleClick
        Try

            Dim y = DirectCast(sender, CrystalDecisions.Windows.Forms.CrystalReportViewer).ActiveControl.Text
            Dim x = 1
            Dim ob = sender
            Dim e_ = e

        Catch ex As Exception

        End Try
    End Sub

    Private Sub crv_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles crv.KeyDown

    End Sub

    Private Sub crv_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles crv.KeyPress

    End Sub

    Private Sub crv_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles crv.KeyUp

    End Sub

    'Private Sub CRV_ClickPage(ByVal sender As System.Object, _
    '                ByVal e As CrystalDecisions.Windows.Forms.NavigateEventArgs) Handles crv.ClickPage
    '    If e.ObjectInfo.Name = "vouCode" Then
    '        'Dim frmToLoad As New Services
    '        'frmToLoad.LoadOrders(e.ObjectInfo.Text)
    '        'frmToLoad.Show()
    '    End If
    'End Sub

    Private Sub crv_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles crv.MouseClick
        Try

            Dim x = 1
            Dim ob = sender
            Dim e_ = e

        Catch ex As Exception

        End Try
    End Sub

    Private Sub crv_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles crv.MouseDoubleClick

    End Sub

    Private Sub crv_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles crv.MouseDown

    End Sub

    Private Sub crv_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles crv.MouseEnter

    End Sub

    Private Sub crv_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles crv.MouseUp

    End Sub

    Private Sub crv_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles crv.PreviewKeyDown

    End Sub

    Private Sub crv_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles crv.Validated

    End Sub

    Private Sub crv_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles crv.Validating

    End Sub

    Private Sub crv_Drill(ByVal source As System.Object, ByVal e As CrystalDecisions.Windows.Forms.DrillEventArgs) Handles crv.Drill

    End Sub

    Private Sub crv_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles crv.Enter
        Try

            Dim y = DirectCast(sender, CrystalDecisions.Windows.Forms.CrystalReportViewer).ActiveControl.Text
            Dim x = 1
            Dim ob = sender
            Dim e_ = e

        Catch ex As Exception

        End Try
    End Sub

    Private Sub crv_LocationChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles crv.LocationChanged

    End Sub

    Private Sub crv_GiveFeedback(ByVal sender As Object, ByVal e As System.Windows.Forms.GiveFeedbackEventArgs) Handles crv.GiveFeedback

    End Sub

    Private Sub crv_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles crv.Move
        Try

            Dim y = DirectCast(sender, CrystalDecisions.Windows.Forms.CrystalReportViewer).ActiveControl.Text
            Dim x = 1
            Dim ob = sender
            Dim e_ = e

        Catch ex As Exception

        End Try
    End Sub

    Private Sub crv_RegionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles crv.RegionChanged

    End Sub

    Private Sub crv_Search(ByVal source As Object, ByVal e As CrystalDecisions.Windows.Forms.SearchEventArgs) Handles crv.Search

    End Sub

    Private Sub crv_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles crv.VisibleChanged
        Try

            Dim y = DirectCast(sender, CrystalDecisions.Windows.Forms.CrystalReportViewer).ActiveControl.Text
            Dim x = 1
            Dim ob = sender
            Dim e_ = e

        Catch ex As Exception

        End Try
    End Sub


    Private Sub CheckBoxGood_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxGood.CheckedChanged
        If CheckBoxGood.Checked = True Then
            chklstBoxGood.Visible = True
        Else
            chklstBoxGood.Visible = False
        End If
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cboDealer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDealer.SelectedIndexChanged

    End Sub

    Private Sub chklstBoxGood_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chklstBoxGood.SelectedIndexChanged

    End Sub

    Private Sub timTO_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timTO.ValueChanged

    End Sub

    Private Sub timFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timFrom.ValueChanged

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