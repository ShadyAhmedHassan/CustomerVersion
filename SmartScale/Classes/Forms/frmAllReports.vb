Imports System.IO
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Text

Public Class frmAllReports

    Private Sub frmAllReports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' fill Cars
            gAdo.CtrlItemsLoad("Car_ID", "CarBoard_No", "tblCar", cboCar, "", " Car_ID>0 ORDER BY CarBoard_No")
            ' fill Goods
            gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood", cboGoods, "", " (Good_ID like '07'+'%' or  good_ID =1 ) and Good_ID in ('0705','0706','0711','0712','0713','0714','0715') or good_ID =1 ORDER BY Good_Name desc")
            gAdo.CtrlItemsLoad("Issue_To_ID", "Issue_To_Name", "tblIssueTo   ORDER BY Issue_To_Name", CboIssueTO)
            gAdo.CtrlItemsLoad("Dealer_ID", "Dealer_Name", "TblDealer where Type=1 ORDER BY Dealer_Name", cboCustomers)

        Catch ex As Exception
        End Try

    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click


        Try



            Dim WhereCondition, TimeTo, TimeFrom As String
            WhereCondition = ""
            Dim d As String = Now.Date.AddDays(1)
            WhereCondition = " where TR_TY =2 and  Out_Date is not null and  First_Weigth_Scale_ID =" & cfrmMain.ScaleNo & " and convert (datetime,T.Out_Date) between '" & Now.Date & " 08:00:00 AM' and '" & Now.Date.AddDays(1) & " 07:59:59 AM' "
            If rdbCar.Checked = True Then  ' search by car No
                If cboCar.Items.Count < 1 Or cboCar.SelectedIndex = -1 Then
                    MsgBox("من فضلك تاكد من انك قمت باختيار سياره")
                    Exit Sub
                End If
                WhereCondition = ""
                'WhereCondition = WhereCondition & " where TR_TY =2 and  Out_Date is not null and First_Weigth_Scale_ID =" & cfrmMain.ScaleNo & " and convert (datetime,T.Out_Date) between '" & dtpCarFrom.Value.Date & " 08:00:00 AM' and '" & dtpCarTo.Value.Date.AddDays(1) & " 07:59:59 AM' and T.Car_ID =" & cboCar.SelectedValue
                If chkDateForCars.Checked = True Then
                    WhereCondition = WhereCondition & " where TR_TY =2 and Out_Date is not null and   First_Weigth_Scale_ID =" & cfrmMain.ScaleNo & " and convert (datetime,T.In_Shift_Date) between '" & dtpCarFrom.Value.Date & " 08:00:00 AM' and '" & dtpCarTo.Value.Date.AddDays(1) & " 07:59:59 AM' and T.Car_ID =" & cboCar.SelectedValue
                Else
                    WhereCondition = WhereCondition & " where TR_TY =2 and Out_Date is not null and   First_Weigth_Scale_ID =" & cfrmMain.ScaleNo & " and T.Car_ID =" & cboCar.SelectedValue
                End If
                If chkTime.Checked = True Then
                    If chkNextDay.Checked = True Then
                        TimeFrom = timFrom.TimeValue
                        TimeFrom = TimeFrom.Replace("م", "PM")
                        TimeFrom = TimeFrom.Replace("ص", "AM")
                        TimeFrom = TimeFrom.Substring(12)
                        TimeTo = timTO.TimeValue
                        TimeTo = TimeTo.Replace("م", "PM")
                        TimeTo = TimeTo.Replace("ص", "AM")
                        TimeTo = TimeTo.Substring(12)
                        Dim TimeNextDayTo As String
                        TimeNextDayTo = Now.Date.AddDays(1) & TimeTo.Substring(10)
                        WhereCondition = ""
                        WhereCondition = WhereCondition & " where TR_TY =2 and Out_Date is not null and First_Weigth_Scale_ID =" & cfrmMain.ScaleNo & " and convert (datetime,T.Out_Date) between '" & dtpCarFrom.Value.Date & " " & TimeFrom & "' and '" & dtpCarTo.Value.Date.AddDays(1) & " " & TimeTo & "' and T.Car_ID =" & cboCar.SelectedValue
                    Else
                        TimeFrom = timFrom.TimeValue
                        TimeFrom = TimeFrom.Replace("م", "PM")
                        TimeFrom = TimeFrom.Replace("ص", "AM")
                        TimeFrom = TimeFrom.Substring(12)
                        TimeTo = timTO.TimeValue
                        TimeTo = TimeTo.Replace("م", "PM")
                        TimeTo = TimeTo.Replace("ص", "AM")
                        TimeTo = TimeTo.Substring(12)
                        WhereCondition = ""
                        WhereCondition = WhereCondition & " where TR_TY =2 and Out_Date is not null and  First_Weigth_Scale_ID =" & cfrmMain.ScaleNo & " and convert (datetime,T.Out_Date) between '" & dtpCarFrom.Value.Date & " " & TimeFrom & "' and '" & dtpCarTo.Value.Date.AddDays(1) & " " & TimeTo & "' and T.Car_ID =" & cboCar.SelectedValue
                    End If
                End If
                GoTo Report
            ElseIf rdbGoods.Checked = True Then   ' search by Good
                If cboGoods.Items.Count < 1 Or cboGoods.SelectedIndex = -1 Then
                    MsgBox("من فضلك تاكد من انك قمت باختيار صنف")
                    Exit Sub
                End If
                WhereCondition = ""
                WhereCondition = WhereCondition & " where TR_TY =2 and Out_Date is not null and First_Weigth_Scale_ID =" & cfrmMain.ScaleNo & " and convert (datetime,T.Out_Date) between '" & dtpProdFrom.Value.Date & " 08:00:00 AM' and '" & dtpProdTo.Value.Date.AddDays(1) & " 07:59:59 AM' and T.good_ID =" & cboGoods.SelectedValue
                If chkTime.Checked = True Then
                    If chkNextDay.Checked = True Then
                        TimeFrom = timFrom.TimeValue
                        TimeFrom = TimeFrom.Replace("م", "PM")
                        TimeFrom = TimeFrom.Replace("ص", "AM")
                        TimeFrom = TimeFrom.Substring(12)
                        TimeTo = timTO.TimeValue
                        TimeTo = TimeTo.Replace("م", "PM")
                        TimeTo = TimeTo.Replace("ص", "AM")
                        TimeTo = TimeTo.Substring(12)
                        Dim TimeNextDayTo As String
                        TimeNextDayTo = Now.Date.AddDays(1) & TimeTo.Substring(10)
                        WhereCondition = ""
                        WhereCondition = WhereCondition & " where TR_TY =2 and Out_Date is not null and First_Weigth_Scale_ID =" & cfrmMain.ScaleNo & " and convert (datetime,T.Out_Date) between '" & dtpProdFrom.Value.Date & " " & TimeFrom & "' and '" & dtpProdTo.Value.Date.AddDays(1) & " " & TimeTo & "' and T.good_ID =" & cboGoods.SelectedValue

                    Else
                        TimeFrom = timFrom.TimeValue
                        TimeFrom = TimeFrom.Replace("م", "PM")
                        TimeFrom = TimeFrom.Replace("ص", "AM")
                        TimeFrom = TimeFrom.Substring(12)
                        TimeTo = timTO.TimeValue
                        TimeTo = TimeTo.Replace("م", "PM")
                        TimeTo = TimeTo.Replace("ص", "AM")
                        TimeTo = TimeTo.Substring(12)
                        WhereCondition = ""
                        WhereCondition = WhereCondition & " where TR_TY =2 and Out_Date is not null and First_Weigth_Scale_ID =" & cfrmMain.ScaleNo & " and convert (datetime,T.Out_Date) between '" & dtpProdFrom.Value.Date & " " & TimeFrom & "' and '" & dtpProdTo.Value.Date.AddDays(1) & " " & TimeTo & "' and T.good_ID =" & cboGoods.SelectedValue

                    End If
                End If
                GoTo Report
            ElseIf rdbCustomer.Checked = True Then  ' Search By Customers
                If cboCustomers.Items.Count < 1 Or cboCustomers.SelectedIndex = -1 Then
                    MsgBox("من فضلك تاكد من انك قمت باختيار عميل")
                    Exit Sub
                End If
                WhereCondition = ""
                WhereCondition = WhereCondition & " where TR_TY =2 and Out_Date is not null and First_Weigth_Scale_ID =" & cfrmMain.ScaleNo & " and convert (datetime,T.Out_Date) between '" & dtpCustomerFrom.Value.Date & " 08:00:00 AM' and '" & dtpCustomerTo.Value.Date.AddDays(1) & " 07:59:59 AM' and T.Dealer_ID =" & cboCustomers.SelectedValue
                If chkTime.Checked = True Then
                    If chkNextDay.Checked = True Then
                        TimeFrom = timFrom.TimeValue
                        TimeFrom = TimeFrom.Replace("م", "PM")
                        TimeFrom = TimeFrom.Replace("ص", "AM")
                        TimeFrom = TimeFrom.Substring(12)
                        TimeTo = timTO.TimeValue
                        TimeTo = TimeTo.Replace("م", "PM")
                        TimeTo = TimeTo.Replace("ص", "AM")
                        TimeTo = TimeTo.Substring(12)
                        Dim TimeNextDayTo As String
                        TimeNextDayTo = Now.Date.AddDays(1) & TimeTo.Substring(10)
                        WhereCondition = ""
                        WhereCondition = WhereCondition & " where TR_TY =2 and Out_Date is not null and First_Weigth_Scale_ID =" & cfrmMain.ScaleNo & " and convert (datetime,T.Out_Date) between '" & dtpCustomerFrom.Value.Date & " " & TimeFrom & "' and '" & dtpCustomerTo.Value.Date.AddDays(1) & " " & TimeTo & "' and T.Dealer_ID =" & cboCustomers.SelectedValue

                    Else
                        TimeFrom = timFrom.TimeValue
                        TimeFrom = TimeFrom.Replace("م", "PM")
                        TimeFrom = TimeFrom.Replace("ص", "AM")
                        TimeFrom = TimeFrom.Substring(12)
                        TimeTo = timTO.TimeValue
                        TimeTo = TimeTo.Replace("م", "PM")
                        TimeTo = TimeTo.Replace("ص", "AM")
                        TimeTo = TimeTo.Substring(12)
                        WhereCondition = ""
                        WhereCondition = WhereCondition & " where TR_TY =2 and Out_Date is not null and First_Weigth_Scale_ID =" & cfrmMain.ScaleNo & " and convert (datetime,T.Out_Date) between '" & dtpCustomerFrom.Value.Date & " " & TimeFrom & "' and '" & dtpCustomerTo.Value.Date.AddDays(1) & " " & TimeTo & "' and T.Dealer_ID =" & cboCustomers.SelectedValue

                    End If
                End If
                GoTo Report

            ElseIf rdbIssueTo.Checked = True Then  ' Search By Issue to
                If CboIssueTO.Items.Count < 1 Or CboIssueTO.SelectedIndex = -1 Then
                    MsgBox("من فضلك تاكد من انك قمت باختيار مرسل اليه")
                    Exit Sub
                End If
                WhereCondition = ""
                WhereCondition = WhereCondition & " where TR_TY =2 and Out_Date is not null and First_Weigth_Scale_ID =" & cfrmMain.ScaleNo & " and convert (datetime,T.Out_Date) between '" & dtpIssueToFrom.Value.Date & " 08:00:00 AM' and '" & dtpIssueToTo.Value.Date.AddDays(1) & " 07:59:59 AM' and T.Issue_To_ID =" & CboIssueTO.SelectedValue
                If chkTime.Checked = True Then
                    If chkNextDay.Checked = True Then
                        TimeFrom = timFrom.TimeValue
                        TimeFrom = TimeFrom.Replace("م", "PM")
                        TimeFrom = TimeFrom.Replace("ص", "AM")
                        TimeFrom = TimeFrom.Substring(12)
                        TimeTo = timTO.TimeValue
                        TimeTo = TimeTo.Replace("م", "PM")
                        TimeTo = TimeTo.Replace("ص", "AM")
                        TimeTo = TimeTo.Substring(12)
                        Dim TimeNextDayTo As String
                        TimeNextDayTo = Now.Date.AddDays(1) & TimeTo.Substring(10)
                        WhereCondition = ""
                        WhereCondition = WhereCondition & " where TR_TY =2 and Out_Date is not null and First_Weigth_Scale_ID =" & cfrmMain.ScaleNo & " and convert (datetime,T.Out_Date) between '" & dtpIssueToFrom.Value.Date & " " & TimeFrom & "' and '" & dtpIssueToTo.Value.Date.AddDays(1) & " " & TimeTo & "' and T.Issue_To_ID =" & CboIssueTO.SelectedValue

                    Else
                        TimeFrom = timFrom.TimeValue
                        TimeFrom = TimeFrom.Replace("م", "PM")
                        TimeFrom = TimeFrom.Replace("ص", "AM")
                        TimeFrom = TimeFrom.Substring(12)
                        TimeTo = timTO.TimeValue
                        TimeTo = TimeTo.Replace("م", "PM")
                        TimeTo = TimeTo.Replace("ص", "AM")
                        TimeTo = TimeTo.Substring(12)
                        WhereCondition = ""
                        WhereCondition = WhereCondition & " where TR_TY =2 and Out_Date is not null and First_Weigth_Scale_ID =" & cfrmMain.ScaleNo & " and convert (datetime,T.Out_Date) between '" & dtpIssueToFrom.Value.Date & " " & TimeFrom & "' and '" & dtpIssueToTo.Value.Date.AddDays(1) & " " & TimeTo & "' and T.Issue_To_ID =" & CboIssueTO.SelectedValue

                    End If
                End If
                GoTo Report
            ElseIf rdbTrans.Checked = True Then  ' search by trans
                If txtTransFrom.Text.Trim = "" Or txtTransTo.Text.Trim = "" Or Val(txtTransFrom.Text) < 1 Or Val(txtTransTo.Text) < 1 Then
                    MsgBox("من فضلك تاكد من ارقام البطاقات")
                    Exit Sub
                End If
                WhereCondition = ""
                WhereCondition = " where TR_TY =2 and Out_Date is not null and First_Weigth_Scale_ID =" & cfrmMain.ScaleNo & " and T.CID between " & txtTransFrom.Text & " and " & txtTransTo.Text
                GoTo Report
            ElseIf rdbPeriod.Checked = True Then
                WhereCondition = ""
                WhereCondition = " where TR_TY =2 and Out_Date is not null and First_Weigth_Scale_ID =" & cfrmMain.ScaleNo & " and convert (datetime,T.Out_Date) between '" & dtpAllFrom.Value.Date & " 08:00:00 AM' and '" & dtpAllTo.Value.Date.AddDays(1) & " 07:59:59 AM' "
            End If

            If chkTime.Checked = True Then
                If chkNextDay.Checked = True Then
                    TimeFrom = timFrom.TimeValue
                    TimeFrom = TimeFrom.Replace("م", "PM")
                    TimeFrom = TimeFrom.Replace("ص", "AM")
                    TimeTo = timTO.TimeValue
                    TimeTo = TimeTo.Replace("م", "PM")
                    TimeTo = TimeTo.Replace("ص", "AM")
                    Dim TimeNextDayTo As String
                    TimeNextDayTo = Now.Date.AddDays(1) & TimeTo.Substring(10)
                    WhereCondition = ""
                    WhereCondition = WhereCondition & " where TR_TY =2 and Out_Date is not null and First_Weigth_Scale_ID =" & cfrmMain.ScaleNo & " and convert (datetime,T.Out_Date)   between '" & TimeFrom & "' and '" & TimeNextDayTo & "' "

                Else
                    TimeFrom = timFrom.TimeValue
                    TimeFrom = TimeFrom.Replace("م", "PM")
                    TimeFrom = TimeFrom.Replace("ص", "AM")
                    TimeTo = timTO.TimeValue
                    TimeTo = TimeTo.Replace("م", "PM")
                    TimeTo = TimeTo.Replace("ص", "AM")
                    WhereCondition = ""
                    WhereCondition = WhereCondition & " where TR_TY =2 and Out_Date is not null and First_Weigth_Scale_ID =" & cfrmMain.ScaleNo & " and convert (datetime,T.Out_Date)   between '" & TimeFrom & "' and '" & TimeTo & "' "
                End If
            End If
Report:
            Dim ConnPath, ConnString As String
            ConnPath = Application.StartupPath & "\ConnString.txt"
            ConnString = GetConString(ConnPath)
            Dim Con As New SqlConnection(ConnString)

            Dim Adp As New SqlDataAdapter(" set dateformat dmy SELECT T.CID,D.Dealer_Name,G.Good_Name,IT.Issue_To_Name,C.CarBoard_No,CI.TruckBoard_No,T.Dealer_ID,T.Issue_To_ID " _
                                         & ",T.First_Weigth,T.Second_Weight,Out_Date " _
                                         & "FROM tblTransactions as T join tblGood as G on T.Good_ID =G.Good_ID  " _
                                         & "join tblCarInfo as CI on CI.Car_Info_ID=T.Car_Info_ID  " _
                                         & "join tblCar as C on C.Car_ID =T.Car_ID " _
                                         & "join tblDealer as D on D.Dealer_ID=T.Dealer_ID " _
                                         & "join tblIssueTo as IT on IT.Issue_To_ID=T.Issue_To_ID  " & WhereCondition, Con)

            Dim DT As New DataTable
            Adp.Fill(DT)
            Dim Rpt As New RptTransaction
            Rpt.SetDataSource(DT)
            Rpt.SetParameterValue("Date", Now.Date)
            Rpt.SetParameterValue("DateFrom", dtpAllFrom.Value.Date)
            Rpt.SetParameterValue("DateTo", dtpAllTo.Value.Date)
            If rdbIssueTo.Checked Then
                Rpt.SetParameterValue("IssueTo", CboIssueTO.Text)
            Else
                Rpt.SetParameterValue("IssueTo", "الكل")
            End If

            Rpt.SetParameterValue("Scale", "الكل")
            Rpt.SetParameterValue("Shift", "الكل")
            Rpt.SetParameterValue("User", "الكل")
            If rdbCustomer.Checked Then
                Rpt.SetParameterValue("Customers", cboCustomers.Text)
            Else
                Rpt.SetParameterValue("Customers", "الكل")
            End If

            crvAllReports.ReportSource = Rpt

            ''''''''''' Send Email with report after exporting it in pdf file ''''''''''
            ''''''''''' Its working code with no error

            '' '' '' '' ''Try
            '' '' '' '' ''    Dim file As File
            '' '' '' '' ''    If file.Exists("c:\Report.pdf") Then
            '' '' '' '' ''        file.Delete("c:\Report.pdf")
            '' '' '' '' ''    End If
            '' '' '' '' ''    Dim CrExportOptions As ExportOptions
            '' '' '' '' ''    Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()
            '' '' '' '' ''    Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()
            '' '' '' '' ''    CrDiskFileDestinationOptions.DiskFileName = "c:\Report.pdf"
            '' '' '' '' ''    CrExportOptions = Rpt.ExportOptions
            '' '' '' '' ''    With CrExportOptions
            '' '' '' '' ''        .ExportDestinationType = ExportDestinationType.DiskFile
            '' '' '' '' ''        .ExportFormatType = ExportFormatType.PortableDocFormat
            '' '' '' '' ''        .DestinationOptions = CrDiskFileDestinationOptions
            '' '' '' '' ''        .FormatOptions = CrFormatTypeOptions
            '' '' '' '' ''    End With

            '' '' '' '' ''    'If file.Exists("c:\Report.pdf") Then
            '' '' '' '' ''    '    file.Delete("c:\Report.pdf")
            '' '' '' '' ''    '    Rpt.Export()
            '' '' '' '' ''    'Else
            '' '' '' '' ''    Rpt.Export()

            '' '' '' '' ''    'End If
            '' '' '' '' ''    Dim m As New MailMessage()
            '' '' '' '' ''    m.From = New MailAddress("Alex4Prog@gmail.com", "الميزان")
            '' '' '' '' ''    m.[To].Add(New MailAddress("tharwat2020@hotmail.com", "Smart Scale Report"))
            '' '' '' '' ''    m.Subject = "تقرير الوزنات"
            '' '' '' '' ''    m.Body = ""
            '' '' '' '' ''    m.Attachments.Add(New Attachment("c:\Report.pdf"))
            '' '' '' '' ''    '  m.IsBodyHtml .. if the body is html document 
            '' '' '' '' ''    Dim c As New SmtpClient("smtp.gmail.com", 587)
            '' '' '' '' ''    c.Credentials = New System.Net.NetworkCredential("Alex4Prog@gmail.com", "a4p123456789")
            '' '' '' '' ''    c.EnableSsl = True
            '' '' '' '' ''    c.Send(m)

            '' '' '' '' ''    m.Dispose()
            '' '' '' '' ''    'Dim c As New SmtpClient("smtp.gmail.com", 587)

            '' '' '' '' ''Catch ex As Exception

            '' '' '' '' ''End Try
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Catch ex As Exception
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

    Private Sub rdbDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbDate.CheckedChanged
        'If rdbDate.Checked Then
        If rdbDate.Checked Then
            rdbDate.ForeColor = Color.Black
            chkTime.Checked = False
        Else
            rdbDate.ForeColor = Color.White
        End If

    End Sub

    Private Sub rdbTime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTime.CheckedChanged

        If rdbTime.Checked Then
            timFrom.Visible = True
            timTO.Visible = True
            Label1.Visible = True
            chkNextDay.Visible = True
            rdbTime.ForeColor = Color.Black
        Else
            timFrom.Visible = False
            timTO.Visible = False
            Label1.Visible = False
            chkNextDay.Visible = False
            rdbTime.ForeColor = Color.White
        End If

    End Sub

    Private Sub rdbGoods_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbGoods.CheckedChanged

        If rdbGoods.Checked Then
            cboGoods.Visible = True
            rdbGoods.ForeColor = Color.Black
            lblProdFrom.Visible = True
            lblProdTo.Visible = True
            dtpProdFrom.Visible = True
            dtpProdTo.Visible = True
        Else
            cboGoods.Visible = False
            rdbGoods.ForeColor = Color.White
            lblProdFrom.Visible = False
            lblProdTo.Visible = False
            dtpProdFrom.Visible = False
            dtpProdTo.Visible = False
        End If

    End Sub

    Private Sub rdbCar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCar.CheckedChanged

        If rdbCar.Checked Then
            cboCar.Visible = True
            rdbCar.ForeColor = Color.Black
            lblCarFrom.Visible = True
            lblCarTo.Visible = True
            dtpCarFrom.Visible = True
            dtpCarTo.Visible = True
        Else
            cboCar.Visible = False
            rdbCar.ForeColor = Color.White
            lblCarFrom.Visible = False
            lblCarTo.Visible = False
            dtpCarFrom.Visible = False
            dtpCarTo.Visible = False
        End If

    End Sub

    Private Sub rdbTrans_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTrans.CheckedChanged

        If rdbTrans.Checked Then
            lblTransFrom.Visible = True
            lblTransto.Visible = True
            txtTransFrom.Visible = True
            txtTransTo.Visible = True
            rdbTrans.ForeColor = Color.Black
            txtTransFrom.Focus()
            chkTime.Checked = False
        Else
            lblTransFrom.Visible = False
            lblTransto.Visible = False
            txtTransFrom.Visible = False
            txtTransTo.Visible = False
            rdbTrans.ForeColor = Color.White
        End If

    End Sub

    Private Sub txtTransFrom_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
    txtTransFrom.KeyPress, txtTransTo.KeyPress

        Try
            Dim M As Boolean = Char.IsNumber(e.KeyChar) Or Char.IsControl(e.KeyChar)
            If Not M = True Then
                e.Handled = True
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub frmAllReports_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        gfrmChoose.Show()
    End Sub

    Private Sub chkNextDay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNextDay.CheckedChanged

        If chkNextDay.Checked Then
            chkNextDay.ForeColor = Color.Black
        Else
            chkNextDay.ForeColor = Color.White
        End If

    End Sub

    Private Sub BtnSearch_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.MouseEnter
        sender.BackColor = Color.SkyBlue
    End Sub

    Private Sub BtnSearch_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.MouseLeave
        sender.BackColor = Color.Gainsboro
    End Sub

    Private Sub frmAllReports_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Enter Then
            BtnSearch_Click(sender, e)
        End If

    End Sub

    Private Sub chkTime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTime.CheckedChanged

        If chkTime.Checked Then
            timFrom.Visible = True
            timTO.Visible = True
            Label1.Visible = True
            chkNextDay.Visible = True
            chkTime.ForeColor = Color.Black
            rdbDate.Checked = False
            rdbTrans.Checked = False
            rdbPeriod.Checked = False
        Else
            timFrom.Visible = False
            timTO.Visible = False
            Label1.Visible = False
            chkNextDay.Visible = False
            chkTime.ForeColor = Color.White
        End If

    End Sub

    Private Sub rdbPeriod_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPeriod.CheckedChanged

        If rdbPeriod.Checked Then
            lblAllFrom.Visible = True
            lblAllTo.Visible = True
            dtpAllFrom.Visible = True
            dtpAllTo.Visible = True
            rdbPeriod.ForeColor = Color.Black
            txtTransFrom.Focus()
            chkTime.Checked = False
        Else
            lblAllFrom.Visible = False
            lblAllTo.Visible = False
            dtpAllFrom.Visible = False
            dtpAllTo.Visible = False
            rdbPeriod.ForeColor = Color.White
        End If

    End Sub

    Private Sub txtTransFrom_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTransFrom.KeyDown

        If e.KeyCode = Keys.Enter Then
            ProcessTabKey(True)
        End If

    End Sub

    Private Sub rdbCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCustomer.CheckedChanged
        If rdbCustomer.Checked = True Then
            rdbCustomer.ForeColor = Color.Black
            cboCustomers.Visible = True
            lblCustomersFrom.Visible = True
            dtpCustomerFrom.Visible = True
            lblCustomersTo.Visible = True
            dtpCustomerTo.Visible = True
        Else
            rdbCustomer.ForeColor = Color.White
            cboCustomers.Visible = False
            lblCustomersFrom.Visible = False
            dtpCustomerFrom.Visible = False
            lblCustomersTo.Visible = False
            dtpCustomerTo.Visible = False
        End If
    End Sub

    Private Sub rdbIssueTo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbIssueTo.CheckedChanged

        If rdbIssueTo.Checked = True Then
            rdbIssueTo.ForeColor = Color.Black
            CboIssueTO.Visible = True
            lblIssueToFrom.Visible = True
            dtpIssueToFrom.Visible = True
            lblIssueToTo.Visible = True
            dtpIssueToTo.Visible = True
        Else
            rdbIssueTo.ForeColor = Color.White
            CboIssueTO.Visible = False
            lblIssueToFrom.Visible = False
            dtpIssueToFrom.Visible = False
            lblIssueToTo.Visible = False
            dtpIssueToTo.Visible = False
        End If
    End Sub

    Private Sub chkDateForCars_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDateForCars.CheckedChanged
        If chkDateForCars.Checked = True Then
            dtpCarFrom.Enabled = True
            dtpCarFrom.Enabled = True
            dtpCarTo.Enabled = True
        Else
            dtpCarFrom.Enabled = False
            dtpCarFrom.Enabled = False
            dtpCarTo.Enabled = False
        End If
    End Sub
End Class