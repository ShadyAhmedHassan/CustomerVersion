<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class cfrmReports
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(cfrmReports))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.CboReportShift = New System.Windows.Forms.ComboBox
        Me.CboReportScale = New System.Windows.Forms.ComboBox
        Me.CboReportUser = New System.Windows.Forms.ComboBox
        Me.CboReportIssueTo = New System.Windows.Forms.ComboBox
        Me.DTRepot = New System.Windows.Forms.DateTimePicker
        Me.CrvTransactionRepot = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label8 = New System.Windows.Forms.Label
        Me.cboDealer = New System.Windows.Forms.ComboBox
        Me.dtTo = New System.Windows.Forms.DateTimePicker
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.BtnSearch = New System.Windows.Forms.Button
        Me.Label11 = New System.Windows.Forms.Label
        Me.cboGoods = New System.Windows.Forms.ComboBox
        Me.chkTime = New System.Windows.Forms.CheckBox
        Me.chkNextDay = New System.Windows.Forms.CheckBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.timFrom = New DateAndTimeControls.CompositeDateTimeControl
        Me.timTO = New DateAndTimeControls.CompositeDateTimeControl
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Font = New System.Drawing.Font("Simplified Arabic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label1.Location = New System.Drawing.Point(830, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "التاريخ"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Font = New System.Drawing.Font("Simplified Arabic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label2.Location = New System.Drawing.Point(583, 89)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 29)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "الورديه"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label3.Font = New System.Drawing.Font("Simplified Arabic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label3.Location = New System.Drawing.Point(830, 87)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 29)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "الميزان"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label4.Font = New System.Drawing.Font("Simplified Arabic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label4.Location = New System.Drawing.Point(330, 87)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 29)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "المستخدمين"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label5.Font = New System.Drawing.Font("Simplified Arabic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label5.Location = New System.Drawing.Point(830, 47)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 29)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "المرسل إليه"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CboReportShift
        '
        Me.CboReportShift.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CboReportShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboReportShift.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboReportShift.FormattingEnabled = True
        Me.CboReportShift.Location = New System.Drawing.Point(411, 89)
        Me.CboReportShift.Name = "CboReportShift"
        Me.CboReportShift.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CboReportShift.Size = New System.Drawing.Size(166, 27)
        Me.CboReportShift.TabIndex = 5
        '
        'CboReportScale
        '
        Me.CboReportScale.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CboReportScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboReportScale.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboReportScale.FormattingEnabled = True
        Me.CboReportScale.Location = New System.Drawing.Point(664, 87)
        Me.CboReportScale.Name = "CboReportScale"
        Me.CboReportScale.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CboReportScale.Size = New System.Drawing.Size(160, 27)
        Me.CboReportScale.TabIndex = 6
        '
        'CboReportUser
        '
        Me.CboReportUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CboReportUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboReportUser.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboReportUser.FormattingEnabled = True
        Me.CboReportUser.Location = New System.Drawing.Point(182, 89)
        Me.CboReportUser.Name = "CboReportUser"
        Me.CboReportUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CboReportUser.Size = New System.Drawing.Size(142, 27)
        Me.CboReportUser.TabIndex = 7
        '
        'CboReportIssueTo
        '
        Me.CboReportIssueTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CboReportIssueTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboReportIssueTo.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboReportIssueTo.FormattingEnabled = True
        Me.CboReportIssueTo.Location = New System.Drawing.Point(664, 49)
        Me.CboReportIssueTo.Name = "CboReportIssueTo"
        Me.CboReportIssueTo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CboReportIssueTo.Size = New System.Drawing.Size(160, 27)
        Me.CboReportIssueTo.TabIndex = 8
        '
        'DTRepot
        '
        Me.DTRepot.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DTRepot.CalendarFont = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTRepot.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTRepot.Location = New System.Drawing.Point(634, 13)
        Me.DTRepot.Name = "DTRepot"
        Me.DTRepot.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DTRepot.RightToLeftLayout = True
        Me.DTRepot.Size = New System.Drawing.Size(160, 26)
        Me.DTRepot.TabIndex = 9
        '
        'CrvTransactionRepot
        '
        Me.CrvTransactionRepot.ActiveViewIndex = -1
        Me.CrvTransactionRepot.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CrvTransactionRepot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrvTransactionRepot.DisplayGroupTree = False
        Me.CrvTransactionRepot.Location = New System.Drawing.Point(-2, 171)
        Me.CrvTransactionRepot.Name = "CrvTransactionRepot"
        Me.CrvTransactionRepot.SelectionFormula = ""
        Me.CrvTransactionRepot.ShowGroupTreeButton = False
        Me.CrvTransactionRepot.ShowRefreshButton = False
        Me.CrvTransactionRepot.Size = New System.Drawing.Size(969, 339)
        Me.CrvTransactionRepot.TabIndex = 10
        Me.CrvTransactionRepot.ViewTimeSelectionFormula = ""
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(17, 15)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 12
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(17, 44)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(100, 20)
        Me.TextBox2.TabIndex = 13
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(134, 18)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(58, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "من الساعه"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(134, 51)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(61, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "إلى الساعه"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.TextBox2)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Location = New System.Drawing.Point(10, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(200, 78)
        Me.Panel1.TabIndex = 16
        Me.Panel1.Visible = False
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label8.Font = New System.Drawing.Font("Simplified Arabic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label8.Location = New System.Drawing.Point(330, 49)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 29)
        Me.Label8.TabIndex = 3
        Me.Label8.Text = "العملاء"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboDealer
        '
        Me.cboDealer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboDealer.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.cboDealer.FormattingEnabled = True
        Me.cboDealer.Location = New System.Drawing.Point(182, 49)
        Me.cboDealer.Name = "cboDealer"
        Me.cboDealer.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cboDealer.Size = New System.Drawing.Size(142, 27)
        Me.cboDealer.TabIndex = 17
        '
        'dtTo
        '
        Me.dtTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtTo.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.dtTo.Location = New System.Drawing.Point(439, 11)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dtTo.RightToLeftLayout = True
        Me.dtTo.Size = New System.Drawing.Size(160, 26)
        Me.dtTo.TabIndex = 18
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(606, 13)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(29, 22)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "الى"
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(796, 15)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(28, 22)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "من"
        '
        'BtnSearch
        '
        Me.BtnSearch.Font = New System.Drawing.Font("Simplified Arabic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.BtnSearch.Image = CType(resources.GetObject("BtnSearch.Image"), System.Drawing.Image)
        Me.BtnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnSearch.Location = New System.Drawing.Point(12, 130)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(120, 35)
        Me.BtnSearch.TabIndex = 11
        Me.BtnSearch.Text = "بـحـث"
        Me.BtnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnSearch.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label11.Font = New System.Drawing.Font("Simplified Arabic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label11.Location = New System.Drawing.Point(583, 49)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(75, 29)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "الصنف"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboGoods
        '
        Me.cboGoods.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboGoods.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.cboGoods.FormattingEnabled = True
        Me.cboGoods.Location = New System.Drawing.Point(411, 51)
        Me.cboGoods.Name = "cboGoods"
        Me.cboGoods.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cboGoods.Size = New System.Drawing.Size(166, 27)
        Me.cboGoods.TabIndex = 22
        '
        'chkTime
        '
        Me.chkTime.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkTime.BackColor = System.Drawing.Color.Transparent
        Me.chkTime.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTime.ForeColor = System.Drawing.Color.White
        Me.chkTime.Location = New System.Drawing.Point(643, 129)
        Me.chkTime.Name = "chkTime"
        Me.chkTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkTime.Size = New System.Drawing.Size(100, 28)
        Me.chkTime.TabIndex = 36
        Me.chkTime.Text = "الوقت"
        Me.chkTime.UseVisualStyleBackColor = False
        '
        'chkNextDay
        '
        Me.chkNextDay.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkNextDay.AutoSize = True
        Me.chkNextDay.BackColor = System.Drawing.Color.Transparent
        Me.chkNextDay.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNextDay.ForeColor = System.Drawing.Color.White
        Me.chkNextDay.Location = New System.Drawing.Point(372, 135)
        Me.chkNextDay.Name = "chkNextDay"
        Me.chkNextDay.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkNextDay.Size = New System.Drawing.Size(86, 23)
        Me.chkNextDay.TabIndex = 35
        Me.chkNextDay.Text = "اليوم التالى"
        Me.chkNextDay.UseVisualStyleBackColor = False
        Me.chkNextDay.Visible = False
        '
        'Label12
        '
        Me.Label12.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(664, 127)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(71, 28)
        Me.Label12.TabIndex = 33
        Me.Label12.Visible = False
        '
        'timFrom
        '
        Me.timFrom.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.timFrom.BackColor = System.Drawing.Color.Transparent
        Me.timFrom.DateCustom = "MMM-dd-yyyy"
        Me.timFrom.DateFormat = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.timFrom.DateTimeValue = New Date(2009, 5, 13, 7, 59, 0, 0)
        Me.timFrom.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.timFrom.Location = New System.Drawing.Point(552, 130)
        Me.timFrom.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.timFrom.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.timFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.timFrom.Name = "timFrom"
        Me.timFrom.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.timFrom.Size = New System.Drawing.Size(181, 25)
        Me.timFrom.TabIndex = 32
        Me.timFrom.TimeCustom = "hh:mm tt"
        Me.timFrom.TimeFormat = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.timFrom.Visible = False
        '
        'timTO
        '
        Me.timTO.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.timTO.BackColor = System.Drawing.Color.Transparent
        Me.timTO.DateCustom = "MMM-dd-yyyy"
        Me.timTO.DateFormat = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.timTO.DateTimeValue = New Date(2009, 5, 13, 20, 0, 0, 0)
        Me.timTO.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.timTO.Location = New System.Drawing.Point(465, 131)
        Me.timTO.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.timTO.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.timTO.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.timTO.Name = "timTO"
        Me.timTO.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.timTO.Size = New System.Drawing.Size(176, 25)
        Me.timTO.TabIndex = 34
        Me.timTO.TimeCustom = "hh:mm tt"
        Me.timTO.TimeFormat = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.timTO.Visible = False
        '
        'cfrmReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.BackgroundImage = Global.SmartScale.My.Resources.Resources.blue
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(962, 510)
        Me.Controls.Add(Me.chkTime)
        Me.Controls.Add(Me.chkNextDay)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.timFrom)
        Me.Controls.Add(Me.timTO)
        Me.Controls.Add(Me.cboGoods)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.dtTo)
        Me.Controls.Add(Me.cboDealer)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.CrvTransactionRepot)
        Me.Controls.Add(Me.DTRepot)
        Me.Controls.Add(Me.CboReportIssueTo)
        Me.Controls.Add(Me.CboReportUser)
        Me.Controls.Add(Me.CboReportScale)
        Me.Controls.Add(Me.CboReportShift)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "cfrmReports"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents CboReportShift As System.Windows.Forms.ComboBox
    Friend WithEvents CboReportScale As System.Windows.Forms.ComboBox
    Friend WithEvents CboReportUser As System.Windows.Forms.ComboBox
    Friend WithEvents CboReportIssueTo As System.Windows.Forms.ComboBox
    Friend WithEvents DTRepot As System.Windows.Forms.DateTimePicker
    Friend WithEvents CrvTransactionRepot As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents BtnSearch As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cboDealer As System.Windows.Forms.ComboBox
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cboGoods As System.Windows.Forms.ComboBox
    Friend WithEvents chkTime As System.Windows.Forms.CheckBox
    Friend WithEvents chkNextDay As System.Windows.Forms.CheckBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents timFrom As DateAndTimeControls.CompositeDateTimeControl
    Friend WithEvents timTO As DateAndTimeControls.CompositeDateTimeControl
End Class
