<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmrptAccounts
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmrptAccounts))
        Me.crv = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.cboGoods = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.dtTo = New System.Windows.Forms.DateTimePicker
        Me.cboDealer = New System.Windows.Forms.ComboBox
        Me.DTRepot = New System.Windows.Forms.DateTimePicker
        Me.CboReportIssueTo = New System.Windows.Forms.ComboBox
        Me.CboReportUser = New System.Windows.Forms.ComboBox
        Me.CboReportScale = New System.Windows.Forms.ComboBox
        Me.CboReportShift = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkTime = New System.Windows.Forms.CheckBox
        Me.chkNextDay = New System.Windows.Forms.CheckBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.timFrom = New DateAndTimeControls.CompositeDateTimeControl
        Me.timTO = New DateAndTimeControls.CompositeDateTimeControl
        Me.BtnSearch = New System.Windows.Forms.Button
        Me.CheckBoxGood = New System.Windows.Forms.CheckBox
        Me.chklstBoxGood = New System.Windows.Forms.CheckedListBox
        Me.cboLocation = New System.Windows.Forms.ComboBox
        Me.ChkLocation = New System.Windows.Forms.CheckBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.chklstBoxScale = New System.Windows.Forms.CheckedListBox
        Me.SuspendLayout()
        '
        'crv
        '
        Me.crv.ActiveViewIndex = -1
        Me.crv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.crv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crv.Location = New System.Drawing.Point(4, 229)
        Me.crv.Name = "crv"
        Me.crv.SelectionFormula = ""
        Me.crv.Size = New System.Drawing.Size(1158, 520)
        Me.crv.TabIndex = 0
        Me.crv.ViewTimeSelectionFormula = ""
        '
        'cboGoods
        '
        Me.cboGoods.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.cboGoods.FormattingEnabled = True
        Me.cboGoods.Location = New System.Drawing.Point(691, 49)
        Me.cboGoods.Name = "cboGoods"
        Me.cboGoods.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cboGoods.Size = New System.Drawing.Size(166, 27)
        Me.cboGoods.TabIndex = 39
        Me.cboGoods.Visible = False
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label11.Location = New System.Drawing.Point(863, 47)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(75, 29)
        Me.Label11.TabIndex = 38
        Me.Label11.Text = "الصنف"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label11.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(1560, 12)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(28, 22)
        Me.Label10.TabIndex = 37
        Me.Label10.Text = "من"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(917, 14)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(29, 22)
        Me.Label9.TabIndex = 36
        Me.Label9.Text = "الى"
        '
        'dtTo
        '
        Me.dtTo.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.dtTo.Location = New System.Drawing.Point(748, 12)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dtTo.RightToLeftLayout = True
        Me.dtTo.Size = New System.Drawing.Size(160, 26)
        Me.dtTo.TabIndex = 35
        '
        'cboDealer
        '
        Me.cboDealer.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.cboDealer.FormattingEnabled = True
        Me.cboDealer.Location = New System.Drawing.Point(850, 158)
        Me.cboDealer.Name = "cboDealer"
        Me.cboDealer.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cboDealer.Size = New System.Drawing.Size(142, 27)
        Me.cboDealer.TabIndex = 34
        Me.cboDealer.Visible = False
        '
        'DTRepot
        '
        Me.DTRepot.CalendarFont = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTRepot.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTRepot.Location = New System.Drawing.Point(951, 12)
        Me.DTRepot.Name = "DTRepot"
        Me.DTRepot.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DTRepot.RightToLeftLayout = True
        Me.DTRepot.Size = New System.Drawing.Size(160, 26)
        Me.DTRepot.TabIndex = 33
        '
        'CboReportIssueTo
        '
        Me.CboReportIssueTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboReportIssueTo.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboReportIssueTo.FormattingEnabled = True
        Me.CboReportIssueTo.Location = New System.Drawing.Point(697, 89)
        Me.CboReportIssueTo.Name = "CboReportIssueTo"
        Me.CboReportIssueTo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CboReportIssueTo.Size = New System.Drawing.Size(160, 27)
        Me.CboReportIssueTo.TabIndex = 32
        Me.CboReportIssueTo.Visible = False
        '
        'CboReportUser
        '
        Me.CboReportUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboReportUser.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboReportUser.FormattingEnabled = True
        Me.CboReportUser.Location = New System.Drawing.Point(850, 198)
        Me.CboReportUser.Name = "CboReportUser"
        Me.CboReportUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CboReportUser.Size = New System.Drawing.Size(142, 27)
        Me.CboReportUser.TabIndex = 31
        Me.CboReportUser.Visible = False
        '
        'CboReportScale
        '
        Me.CboReportScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboReportScale.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboReportScale.FormattingEnabled = True
        Me.CboReportScale.Location = New System.Drawing.Point(944, 86)
        Me.CboReportScale.Name = "CboReportScale"
        Me.CboReportScale.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CboReportScale.Size = New System.Drawing.Size(151, 27)
        Me.CboReportScale.TabIndex = 30
        Me.CboReportScale.Visible = False
        '
        'CboReportShift
        '
        Me.CboReportShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboReportShift.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboReportShift.FormattingEnabled = True
        Me.CboReportShift.Location = New System.Drawing.Point(945, 47)
        Me.CboReportShift.Name = "CboReportShift"
        Me.CboReportShift.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CboReportShift.Size = New System.Drawing.Size(150, 27)
        Me.CboReportShift.TabIndex = 29
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label5.Location = New System.Drawing.Point(863, 87)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 29)
        Me.Label5.TabIndex = 28
        Me.Label5.Text = "المرسل إليه"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label5.Visible = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label8.Location = New System.Drawing.Point(998, 158)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 29)
        Me.Label8.TabIndex = 26
        Me.Label8.Text = "العملاء"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label8.Visible = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label4.Location = New System.Drawing.Point(998, 196)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 29)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "المستخدمين"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label4.Visible = False
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label3.Location = New System.Drawing.Point(1414, 84)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 29)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "الميزان"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label2.Location = New System.Drawing.Point(1414, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 29)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "الوردية"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label1.Location = New System.Drawing.Point(1385, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 29)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "التاريخ"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkTime
        '
        Me.chkTime.BackColor = System.Drawing.Color.Transparent
        Me.chkTime.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTime.ForeColor = System.Drawing.Color.Black
        Me.chkTime.Location = New System.Drawing.Point(1025, 120)
        Me.chkTime.Name = "chkTime"
        Me.chkTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkTime.Size = New System.Drawing.Size(100, 28)
        Me.chkTime.TabIndex = 45
        Me.chkTime.Text = "الوقت"
        Me.chkTime.UseVisualStyleBackColor = False
        Me.chkTime.Visible = False
        '
        'chkNextDay
        '
        Me.chkNextDay.AutoSize = True
        Me.chkNextDay.BackColor = System.Drawing.Color.Transparent
        Me.chkNextDay.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNextDay.ForeColor = System.Drawing.Color.White
        Me.chkNextDay.Location = New System.Drawing.Point(753, 125)
        Me.chkNextDay.Name = "chkNextDay"
        Me.chkNextDay.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkNextDay.Size = New System.Drawing.Size(86, 23)
        Me.chkNextDay.TabIndex = 44
        Me.chkNextDay.Text = "اليوم التالى"
        Me.chkNextDay.UseVisualStyleBackColor = False
        Me.chkNextDay.Visible = False
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(657, 9)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(71, 28)
        Me.Label12.TabIndex = 42
        Me.Label12.Visible = False
        '
        'timFrom
        '
        Me.timFrom.BackColor = System.Drawing.Color.Transparent
        Me.timFrom.DateCustom = "MMM-dd-yyyy"
        Me.timFrom.DateFormat = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.timFrom.DateTimeValue = New Date(2009, 5, 13, 7, 59, 0, 0)
        Me.timFrom.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.timFrom.Location = New System.Drawing.Point(933, 123)
        Me.timFrom.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.timFrom.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.timFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.timFrom.Name = "timFrom"
        Me.timFrom.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.timFrom.Size = New System.Drawing.Size(181, 25)
        Me.timFrom.TabIndex = 41
        Me.timFrom.TimeCustom = "hh:mm tt"
        Me.timFrom.TimeFormat = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.timFrom.Visible = False
        '
        'timTO
        '
        Me.timTO.BackColor = System.Drawing.Color.Transparent
        Me.timTO.DateCustom = "MMM-dd-yyyy"
        Me.timTO.DateFormat = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.timTO.DateTimeValue = New Date(2009, 5, 13, 20, 0, 0, 0)
        Me.timTO.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.timTO.Location = New System.Drawing.Point(846, 124)
        Me.timTO.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.timTO.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.timTO.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.timTO.Name = "timTO"
        Me.timTO.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.timTO.Size = New System.Drawing.Size(176, 25)
        Me.timTO.TabIndex = 43
        Me.timTO.TimeCustom = "hh:mm tt"
        Me.timTO.TimeFormat = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.timTO.Visible = False
        '
        'BtnSearch
        '
        Me.BtnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.BtnSearch.Image = CType(resources.GetObject("BtnSearch.Image"), System.Drawing.Image)
        Me.BtnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnSearch.Location = New System.Drawing.Point(4, 188)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(120, 35)
        Me.BtnSearch.TabIndex = 40
        Me.BtnSearch.Text = "بـحـث"
        Me.BtnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnSearch.UseVisualStyleBackColor = True
        '
        'CheckBoxGood
        '
        Me.CheckBoxGood.Location = New System.Drawing.Point(560, 12)
        Me.CheckBoxGood.Name = "CheckBoxGood"
        Me.CheckBoxGood.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CheckBoxGood.Size = New System.Drawing.Size(78, 25)
        Me.CheckBoxGood.TabIndex = 46
        Me.CheckBoxGood.Text = "الصنف"
        Me.CheckBoxGood.UseVisualStyleBackColor = True
        '
        'chklstBoxGood
        '
        Me.chklstBoxGood.CheckOnClick = True
        Me.chklstBoxGood.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chklstBoxGood.FormattingEnabled = True
        Me.chklstBoxGood.Location = New System.Drawing.Point(222, 12)
        Me.chklstBoxGood.Name = "chklstBoxGood"
        Me.chklstBoxGood.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chklstBoxGood.Size = New System.Drawing.Size(332, 172)
        Me.chklstBoxGood.TabIndex = 47
        '
        'cboLocation
        '
        Me.cboLocation.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLocation.FormattingEnabled = True
        Me.cboLocation.Location = New System.Drawing.Point(12, 13)
        Me.cboLocation.Name = "cboLocation"
        Me.cboLocation.Size = New System.Drawing.Size(123, 25)
        Me.cboLocation.TabIndex = 83
        Me.cboLocation.Visible = False
        '
        'ChkLocation
        '
        Me.ChkLocation.BackColor = System.Drawing.Color.Transparent
        Me.ChkLocation.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkLocation.ForeColor = System.Drawing.Color.Black
        Me.ChkLocation.Location = New System.Drawing.Point(141, 13)
        Me.ChkLocation.Name = "ChkLocation"
        Me.ChkLocation.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ChkLocation.Size = New System.Drawing.Size(75, 28)
        Me.ChkLocation.TabIndex = 88
        Me.ChkLocation.Text = "الموقع"
        Me.ChkLocation.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label6.Location = New System.Drawing.Point(1532, 46)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 28)
        Me.Label6.TabIndex = 89
        Me.Label6.Text = "الورديه"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label7.Location = New System.Drawing.Point(1532, 86)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 27)
        Me.Label7.TabIndex = 90
        Me.Label7.Text = "الميزان"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(1117, 15)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(28, 22)
        Me.Label13.TabIndex = 91
        Me.Label13.Text = "من"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label14.Location = New System.Drawing.Point(1101, 45)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(61, 29)
        Me.Label14.TabIndex = 92
        Me.Label14.Text = "الوردية"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chklstBoxScale
        '
        Me.chklstBoxScale.BackColor = System.Drawing.Color.DarkGray
        Me.chklstBoxScale.CheckOnClick = True
        Me.chklstBoxScale.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chklstBoxScale.FormattingEnabled = True
        Me.chklstBoxScale.Location = New System.Drawing.Point(12, 49)
        Me.chklstBoxScale.Name = "chklstBoxScale"
        Me.chklstBoxScale.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chklstBoxScale.Size = New System.Drawing.Size(163, 109)
        Me.chklstBoxScale.TabIndex = 94
        '
        'frmrptAccounts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1166, 749)
        Me.Controls.Add(Me.chklstBoxScale)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.ChkLocation)
        Me.Controls.Add(Me.cboLocation)
        Me.Controls.Add(Me.chklstBoxGood)
        Me.Controls.Add(Me.CheckBoxGood)
        Me.Controls.Add(Me.chkTime)
        Me.Controls.Add(Me.chkNextDay)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.timFrom)
        Me.Controls.Add(Me.timTO)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.cboGoods)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.dtTo)
        Me.Controls.Add(Me.cboDealer)
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
        Me.Controls.Add(Me.crv)
        Me.KeyPreview = True
        Me.Name = "frmrptAccounts"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "تقرير بونات الكارتات"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents crv As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents cboGoods As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboDealer As System.Windows.Forms.ComboBox
    Friend WithEvents DTRepot As System.Windows.Forms.DateTimePicker
    Friend WithEvents CboReportIssueTo As System.Windows.Forms.ComboBox
    Friend WithEvents CboReportUser As System.Windows.Forms.ComboBox
    Friend WithEvents CboReportScale As System.Windows.Forms.ComboBox
    Friend WithEvents CboReportShift As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkTime As System.Windows.Forms.CheckBox
    Friend WithEvents chkNextDay As System.Windows.Forms.CheckBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents timFrom As DateAndTimeControls.CompositeDateTimeControl
    Friend WithEvents timTO As DateAndTimeControls.CompositeDateTimeControl
    Friend WithEvents BtnSearch As System.Windows.Forms.Button
    Friend WithEvents CheckBoxGood As System.Windows.Forms.CheckBox
    Friend WithEvents chklstBoxGood As System.Windows.Forms.CheckedListBox
    Friend WithEvents cboLocation As System.Windows.Forms.ComboBox
    Friend WithEvents ChkLocation As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents chklstBoxScale As System.Windows.Forms.CheckedListBox
End Class
