<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAllReports
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAllReports))
        Me.crvAllReports = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.Label3 = New System.Windows.Forms.Label
        Me.rdbTime = New System.Windows.Forms.RadioButton
        Me.rdbGoods = New System.Windows.Forms.RadioButton
        Me.rdbCar = New System.Windows.Forms.RadioButton
        Me.rdbDate = New System.Windows.Forms.RadioButton
        Me.timFrom = New DateAndTimeControls.CompositeDateTimeControl
        Me.cboGoods = New System.Windows.Forms.ComboBox
        Me.cboCar = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.timTO = New DateAndTimeControls.CompositeDateTimeControl
        Me.rdbTrans = New System.Windows.Forms.RadioButton
        Me.txtTransFrom = New System.Windows.Forms.TextBox
        Me.txtTransTo = New System.Windows.Forms.TextBox
        Me.lblTransFrom = New System.Windows.Forms.Label
        Me.lblTransto = New System.Windows.Forms.Label
        Me.BtnSearch = New System.Windows.Forms.Button
        Me.chkNextDay = New System.Windows.Forms.CheckBox
        Me.dtpCarFrom = New System.Windows.Forms.DateTimePicker
        Me.dtpCarTo = New System.Windows.Forms.DateTimePicker
        Me.dtpProdFrom = New System.Windows.Forms.DateTimePicker
        Me.dtpProdTo = New System.Windows.Forms.DateTimePicker
        Me.lblProdFrom = New System.Windows.Forms.Label
        Me.lblProdTo = New System.Windows.Forms.Label
        Me.lblCarFrom = New System.Windows.Forms.Label
        Me.lblCarTo = New System.Windows.Forms.Label
        Me.chkTime = New System.Windows.Forms.CheckBox
        Me.dtpAllFrom = New System.Windows.Forms.DateTimePicker
        Me.dtpAllTo = New System.Windows.Forms.DateTimePicker
        Me.lblAllFrom = New System.Windows.Forms.Label
        Me.lblAllTo = New System.Windows.Forms.Label
        Me.rdbPeriod = New System.Windows.Forms.RadioButton
        Me.rdbCustomer = New System.Windows.Forms.RadioButton
        Me.rdbIssueTo = New System.Windows.Forms.RadioButton
        Me.cboCustomers = New System.Windows.Forms.ComboBox
        Me.CboIssueTO = New System.Windows.Forms.ComboBox
        Me.dtpIssueToFrom = New System.Windows.Forms.DateTimePicker
        Me.dtpIssueToTo = New System.Windows.Forms.DateTimePicker
        Me.dtpCustomerFrom = New System.Windows.Forms.DateTimePicker
        Me.dtpCustomerTo = New System.Windows.Forms.DateTimePicker
        Me.lblCustomersTo = New System.Windows.Forms.Label
        Me.lblIssueToTo = New System.Windows.Forms.Label
        Me.lblCustomersFrom = New System.Windows.Forms.Label
        Me.lblIssueToFrom = New System.Windows.Forms.Label
        Me.chkDateForCars = New System.Windows.Forms.CheckBox
        Me.CboRecieveType = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.chklstBoxScale = New System.Windows.Forms.CheckedListBox
        Me.cboLocation = New System.Windows.Forms.ComboBox
        Me.ChkLocation = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'crvAllReports
        '
        Me.crvAllReports.ActiveViewIndex = -1
        Me.crvAllReports.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.crvAllReports.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crvAllReports.DisplayGroupTree = False
        Me.crvAllReports.Location = New System.Drawing.Point(-1, 293)
        Me.crvAllReports.Name = "crvAllReports"
        Me.crvAllReports.SelectionFormula = ""
        Me.crvAllReports.ShowRefreshButton = False
        Me.crvAllReports.Size = New System.Drawing.Size(1550, 373)
        Me.crvAllReports.TabIndex = 1
        Me.crvAllReports.ViewTimeSelectionFormula = ""
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label3.Location = New System.Drawing.Point(1355, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(180, 36)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "اختار طريقه البحث"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'rdbTime
        '
        Me.rdbTime.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rdbTime.AutoSize = True
        Me.rdbTime.BackColor = System.Drawing.Color.Transparent
        Me.rdbTime.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbTime.ForeColor = System.Drawing.Color.White
        Me.rdbTime.Location = New System.Drawing.Point(1355, 73)
        Me.rdbTime.Name = "rdbTime"
        Me.rdbTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.rdbTime.Size = New System.Drawing.Size(67, 28)
        Me.rdbTime.TabIndex = 14
        Me.rdbTime.Text = "الوقت"
        Me.rdbTime.UseVisualStyleBackColor = False
        Me.rdbTime.Visible = False
        '
        'rdbGoods
        '
        Me.rdbGoods.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rdbGoods.AutoSize = True
        Me.rdbGoods.BackColor = System.Drawing.Color.Transparent
        Me.rdbGoods.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbGoods.ForeColor = System.Drawing.Color.White
        Me.rdbGoods.Location = New System.Drawing.Point(1114, 86)
        Me.rdbGoods.Name = "rdbGoods"
        Me.rdbGoods.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.rdbGoods.Size = New System.Drawing.Size(76, 28)
        Me.rdbGoods.TabIndex = 14
        Me.rdbGoods.Text = "الصنف"
        Me.rdbGoods.UseVisualStyleBackColor = False
        '
        'rdbCar
        '
        Me.rdbCar.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rdbCar.AutoSize = True
        Me.rdbCar.BackColor = System.Drawing.Color.Transparent
        Me.rdbCar.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbCar.ForeColor = System.Drawing.Color.White
        Me.rdbCar.Location = New System.Drawing.Point(1112, 120)
        Me.rdbCar.Name = "rdbCar"
        Me.rdbCar.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.rdbCar.Size = New System.Drawing.Size(78, 28)
        Me.rdbCar.TabIndex = 14
        Me.rdbCar.Text = "السياره"
        Me.rdbCar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdbCar.UseVisualStyleBackColor = False
        '
        'rdbDate
        '
        Me.rdbDate.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rdbDate.AutoSize = True
        Me.rdbDate.BackColor = System.Drawing.Color.Transparent
        Me.rdbDate.Checked = True
        Me.rdbDate.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbDate.ForeColor = System.Drawing.Color.White
        Me.rdbDate.Location = New System.Drawing.Point(1036, 17)
        Me.rdbDate.Name = "rdbDate"
        Me.rdbDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.rdbDate.Size = New System.Drawing.Size(154, 28)
        Me.rdbDate.TabIndex = 14
        Me.rdbDate.TabStop = True
        Me.rdbDate.Text = "جميع حركات اليوم"
        Me.rdbDate.UseVisualStyleBackColor = False
        '
        'timFrom
        '
        Me.timFrom.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.timFrom.BackColor = System.Drawing.Color.Transparent
        Me.timFrom.DateCustom = "MMM-dd-yyyy"
        Me.timFrom.DateFormat = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.timFrom.DateTimeValue = New Date(2009, 5, 13, 7, 59, 0, 0)
        Me.timFrom.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.timFrom.Location = New System.Drawing.Point(994, 52)
        Me.timFrom.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.timFrom.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.timFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.timFrom.Name = "timFrom"
        Me.timFrom.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.timFrom.Size = New System.Drawing.Size(181, 25)
        Me.timFrom.TabIndex = 18
        Me.timFrom.TimeCustom = "hh:mm tt"
        Me.timFrom.TimeFormat = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.timFrom.Visible = False
        '
        'cboGoods
        '
        Me.cboGoods.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cboGoods.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGoods.FormattingEnabled = True
        Me.cboGoods.Location = New System.Drawing.Point(915, 86)
        Me.cboGoods.Name = "cboGoods"
        Me.cboGoods.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cboGoods.Size = New System.Drawing.Size(158, 27)
        Me.cboGoods.TabIndex = 19
        Me.cboGoods.Visible = False
        '
        'cboCar
        '
        Me.cboCar.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cboCar.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCar.FormattingEnabled = True
        Me.cboCar.Location = New System.Drawing.Point(915, 120)
        Me.cboCar.Name = "cboCar"
        Me.cboCar.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cboCar.Size = New System.Drawing.Size(158, 27)
        Me.cboCar.TabIndex = 20
        Me.cboCar.Visible = False
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(1077, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 28)
        Me.Label1.TabIndex = 21
        Me.Label1.Visible = False
        '
        'timTO
        '
        Me.timTO.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.timTO.BackColor = System.Drawing.Color.Transparent
        Me.timTO.DateCustom = "MMM-dd-yyyy"
        Me.timTO.DateFormat = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.timTO.DateTimeValue = New Date(2009, 5, 13, 20, 0, 0, 0)
        Me.timTO.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.timTO.Location = New System.Drawing.Point(907, 53)
        Me.timTO.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.timTO.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.timTO.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.timTO.Name = "timTO"
        Me.timTO.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.timTO.Size = New System.Drawing.Size(176, 25)
        Me.timTO.TabIndex = 22
        Me.timTO.TimeCustom = "hh:mm tt"
        Me.timTO.TimeFormat = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.timTO.Visible = False
        '
        'rdbTrans
        '
        Me.rdbTrans.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rdbTrans.AutoSize = True
        Me.rdbTrans.BackColor = System.Drawing.Color.Transparent
        Me.rdbTrans.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold)
        Me.rdbTrans.ForeColor = System.Drawing.Color.White
        Me.rdbTrans.Location = New System.Drawing.Point(1114, 154)
        Me.rdbTrans.Name = "rdbTrans"
        Me.rdbTrans.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.rdbTrans.Size = New System.Drawing.Size(75, 28)
        Me.rdbTrans.TabIndex = 23
        Me.rdbTrans.TabStop = True
        Me.rdbTrans.Text = "بطاقات"
        Me.rdbTrans.UseVisualStyleBackColor = False
        '
        'txtTransFrom
        '
        Me.txtTransFrom.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTransFrom.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.txtTransFrom.Location = New System.Drawing.Point(945, 154)
        Me.txtTransFrom.Name = "txtTransFrom"
        Me.txtTransFrom.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtTransFrom.Size = New System.Drawing.Size(128, 26)
        Me.txtTransFrom.TabIndex = 24
        Me.txtTransFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtTransFrom.Visible = False
        '
        'txtTransTo
        '
        Me.txtTransTo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTransTo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.txtTransTo.Location = New System.Drawing.Point(776, 155)
        Me.txtTransTo.Name = "txtTransTo"
        Me.txtTransTo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtTransTo.Size = New System.Drawing.Size(131, 26)
        Me.txtTransTo.TabIndex = 25
        Me.txtTransTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtTransTo.Visible = False
        '
        'lblTransFrom
        '
        Me.lblTransFrom.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblTransFrom.AutoSize = True
        Me.lblTransFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblTransFrom.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblTransFrom.ForeColor = System.Drawing.Color.White
        Me.lblTransFrom.Location = New System.Drawing.Point(1079, 157)
        Me.lblTransFrom.Name = "lblTransFrom"
        Me.lblTransFrom.Size = New System.Drawing.Size(25, 19)
        Me.lblTransFrom.TabIndex = 26
        Me.lblTransFrom.Text = "من"
        Me.lblTransFrom.Visible = False
        '
        'lblTransto
        '
        Me.lblTransto.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblTransto.AutoSize = True
        Me.lblTransto.BackColor = System.Drawing.Color.Transparent
        Me.lblTransto.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblTransto.ForeColor = System.Drawing.Color.White
        Me.lblTransto.Location = New System.Drawing.Point(913, 158)
        Me.lblTransto.Name = "lblTransto"
        Me.lblTransto.Size = New System.Drawing.Size(26, 19)
        Me.lblTransto.TabIndex = 26
        Me.lblTransto.Text = "الى"
        Me.lblTransto.Visible = False
        '
        'BtnSearch
        '
        Me.BtnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.BtnSearch.Image = CType(resources.GetObject("BtnSearch.Image"), System.Drawing.Image)
        Me.BtnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnSearch.Location = New System.Drawing.Point(12, 252)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(120, 35)
        Me.BtnSearch.TabIndex = 12
        Me.BtnSearch.Text = "بـحـث"
        Me.BtnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnSearch.UseVisualStyleBackColor = True
        '
        'chkNextDay
        '
        Me.chkNextDay.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkNextDay.AutoSize = True
        Me.chkNextDay.BackColor = System.Drawing.Color.Transparent
        Me.chkNextDay.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNextDay.ForeColor = System.Drawing.Color.White
        Me.chkNextDay.Location = New System.Drawing.Point(814, 57)
        Me.chkNextDay.Name = "chkNextDay"
        Me.chkNextDay.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkNextDay.Size = New System.Drawing.Size(86, 23)
        Me.chkNextDay.TabIndex = 27
        Me.chkNextDay.Text = "اليوم التالى"
        Me.chkNextDay.UseVisualStyleBackColor = False
        Me.chkNextDay.Visible = False
        '
        'dtpCarFrom
        '
        Me.dtpCarFrom.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dtpCarFrom.Enabled = False
        Me.dtpCarFrom.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.dtpCarFrom.Location = New System.Drawing.Point(742, 122)
        Me.dtpCarFrom.Name = "dtpCarFrom"
        Me.dtpCarFrom.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dtpCarFrom.RightToLeftLayout = True
        Me.dtpCarFrom.Size = New System.Drawing.Size(136, 26)
        Me.dtpCarFrom.TabIndex = 28
        Me.dtpCarFrom.Visible = False
        '
        'dtpCarTo
        '
        Me.dtpCarTo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dtpCarTo.Enabled = False
        Me.dtpCarTo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.dtpCarTo.Location = New System.Drawing.Point(568, 121)
        Me.dtpCarTo.Name = "dtpCarTo"
        Me.dtpCarTo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dtpCarTo.RightToLeftLayout = True
        Me.dtpCarTo.Size = New System.Drawing.Size(136, 26)
        Me.dtpCarTo.TabIndex = 28
        Me.dtpCarTo.Visible = False
        '
        'dtpProdFrom
        '
        Me.dtpProdFrom.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dtpProdFrom.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.dtpProdFrom.Location = New System.Drawing.Point(742, 88)
        Me.dtpProdFrom.Name = "dtpProdFrom"
        Me.dtpProdFrom.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dtpProdFrom.RightToLeftLayout = True
        Me.dtpProdFrom.Size = New System.Drawing.Size(136, 26)
        Me.dtpProdFrom.TabIndex = 29
        Me.dtpProdFrom.Visible = False
        '
        'dtpProdTo
        '
        Me.dtpProdTo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dtpProdTo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.dtpProdTo.Location = New System.Drawing.Point(568, 87)
        Me.dtpProdTo.Name = "dtpProdTo"
        Me.dtpProdTo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dtpProdTo.RightToLeftLayout = True
        Me.dtpProdTo.Size = New System.Drawing.Size(136, 26)
        Me.dtpProdTo.TabIndex = 29
        Me.dtpProdTo.Visible = False
        '
        'lblProdFrom
        '
        Me.lblProdFrom.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblProdFrom.AutoSize = True
        Me.lblProdFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblProdFrom.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblProdFrom.ForeColor = System.Drawing.Color.White
        Me.lblProdFrom.Location = New System.Drawing.Point(884, 92)
        Me.lblProdFrom.Name = "lblProdFrom"
        Me.lblProdFrom.Size = New System.Drawing.Size(25, 19)
        Me.lblProdFrom.TabIndex = 30
        Me.lblProdFrom.Text = "من"
        Me.lblProdFrom.Visible = False
        '
        'lblProdTo
        '
        Me.lblProdTo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblProdTo.AutoSize = True
        Me.lblProdTo.BackColor = System.Drawing.Color.Transparent
        Me.lblProdTo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblProdTo.ForeColor = System.Drawing.Color.White
        Me.lblProdTo.Location = New System.Drawing.Point(710, 92)
        Me.lblProdTo.Name = "lblProdTo"
        Me.lblProdTo.Size = New System.Drawing.Size(26, 19)
        Me.lblProdTo.TabIndex = 30
        Me.lblProdTo.Text = "الى"
        Me.lblProdTo.Visible = False
        '
        'lblCarFrom
        '
        Me.lblCarFrom.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblCarFrom.AutoSize = True
        Me.lblCarFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblCarFrom.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblCarFrom.ForeColor = System.Drawing.Color.White
        Me.lblCarFrom.Location = New System.Drawing.Point(884, 126)
        Me.lblCarFrom.Name = "lblCarFrom"
        Me.lblCarFrom.Size = New System.Drawing.Size(25, 19)
        Me.lblCarFrom.TabIndex = 30
        Me.lblCarFrom.Text = "من"
        Me.lblCarFrom.Visible = False
        '
        'lblCarTo
        '
        Me.lblCarTo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblCarTo.AutoSize = True
        Me.lblCarTo.BackColor = System.Drawing.Color.Transparent
        Me.lblCarTo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblCarTo.ForeColor = System.Drawing.Color.White
        Me.lblCarTo.Location = New System.Drawing.Point(710, 126)
        Me.lblCarTo.Name = "lblCarTo"
        Me.lblCarTo.Size = New System.Drawing.Size(26, 19)
        Me.lblCarTo.TabIndex = 30
        Me.lblCarTo.Text = "الى"
        Me.lblCarTo.Visible = False
        '
        'chkTime
        '
        Me.chkTime.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkTime.AutoSize = True
        Me.chkTime.BackColor = System.Drawing.Color.Transparent
        Me.chkTime.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTime.ForeColor = System.Drawing.Color.White
        Me.chkTime.Location = New System.Drawing.Point(1122, 51)
        Me.chkTime.Name = "chkTime"
        Me.chkTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkTime.Size = New System.Drawing.Size(68, 28)
        Me.chkTime.TabIndex = 31
        Me.chkTime.Text = "الوقت"
        Me.chkTime.UseVisualStyleBackColor = False
        '
        'dtpAllFrom
        '
        Me.dtpAllFrom.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dtpAllFrom.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.dtpAllFrom.Location = New System.Drawing.Point(788, 20)
        Me.dtpAllFrom.Name = "dtpAllFrom"
        Me.dtpAllFrom.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dtpAllFrom.RightToLeftLayout = True
        Me.dtpAllFrom.Size = New System.Drawing.Size(136, 26)
        Me.dtpAllFrom.TabIndex = 29
        Me.dtpAllFrom.Visible = False
        '
        'dtpAllTo
        '
        Me.dtpAllTo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dtpAllTo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.dtpAllTo.Location = New System.Drawing.Point(614, 19)
        Me.dtpAllTo.Name = "dtpAllTo"
        Me.dtpAllTo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dtpAllTo.RightToLeftLayout = True
        Me.dtpAllTo.Size = New System.Drawing.Size(136, 26)
        Me.dtpAllTo.TabIndex = 29
        Me.dtpAllTo.Visible = False
        '
        'lblAllFrom
        '
        Me.lblAllFrom.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblAllFrom.AutoSize = True
        Me.lblAllFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblAllFrom.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblAllFrom.ForeColor = System.Drawing.Color.White
        Me.lblAllFrom.Location = New System.Drawing.Point(930, 24)
        Me.lblAllFrom.Name = "lblAllFrom"
        Me.lblAllFrom.Size = New System.Drawing.Size(25, 19)
        Me.lblAllFrom.TabIndex = 30
        Me.lblAllFrom.Text = "من"
        Me.lblAllFrom.Visible = False
        '
        'lblAllTo
        '
        Me.lblAllTo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblAllTo.AutoSize = True
        Me.lblAllTo.BackColor = System.Drawing.Color.Transparent
        Me.lblAllTo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblAllTo.ForeColor = System.Drawing.Color.White
        Me.lblAllTo.Location = New System.Drawing.Point(756, 24)
        Me.lblAllTo.Name = "lblAllTo"
        Me.lblAllTo.Size = New System.Drawing.Size(26, 19)
        Me.lblAllTo.TabIndex = 30
        Me.lblAllTo.Text = "الى"
        Me.lblAllTo.Visible = False
        '
        'rdbPeriod
        '
        Me.rdbPeriod.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rdbPeriod.AutoSize = True
        Me.rdbPeriod.BackColor = System.Drawing.Color.Transparent
        Me.rdbPeriod.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbPeriod.ForeColor = System.Drawing.Color.White
        Me.rdbPeriod.Location = New System.Drawing.Point(976, 19)
        Me.rdbPeriod.Name = "rdbPeriod"
        Me.rdbPeriod.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.rdbPeriod.Size = New System.Drawing.Size(54, 26)
        Me.rdbPeriod.TabIndex = 32
        Me.rdbPeriod.TabStop = True
        Me.rdbPeriod.Text = "فتره"
        Me.rdbPeriod.UseVisualStyleBackColor = False
        '
        'rdbCustomer
        '
        Me.rdbCustomer.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rdbCustomer.AutoSize = True
        Me.rdbCustomer.BackColor = System.Drawing.Color.Transparent
        Me.rdbCustomer.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold)
        Me.rdbCustomer.ForeColor = System.Drawing.Color.White
        Me.rdbCustomer.Location = New System.Drawing.Point(1120, 188)
        Me.rdbCustomer.Name = "rdbCustomer"
        Me.rdbCustomer.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.rdbCustomer.Size = New System.Drawing.Size(70, 28)
        Me.rdbCustomer.TabIndex = 33
        Me.rdbCustomer.TabStop = True
        Me.rdbCustomer.Text = "العميل"
        Me.rdbCustomer.UseVisualStyleBackColor = False
        '
        'rdbIssueTo
        '
        Me.rdbIssueTo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rdbIssueTo.AutoSize = True
        Me.rdbIssueTo.BackColor = System.Drawing.Color.Transparent
        Me.rdbIssueTo.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold)
        Me.rdbIssueTo.ForeColor = System.Drawing.Color.White
        Me.rdbIssueTo.Location = New System.Drawing.Point(1082, 222)
        Me.rdbIssueTo.Name = "rdbIssueTo"
        Me.rdbIssueTo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.rdbIssueTo.Size = New System.Drawing.Size(108, 28)
        Me.rdbIssueTo.TabIndex = 34
        Me.rdbIssueTo.TabStop = True
        Me.rdbIssueTo.Text = "المرسل اليه"
        Me.rdbIssueTo.UseVisualStyleBackColor = False
        '
        'cboCustomers
        '
        Me.cboCustomers.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cboCustomers.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.cboCustomers.FormattingEnabled = True
        Me.cboCustomers.Location = New System.Drawing.Point(915, 192)
        Me.cboCustomers.Name = "cboCustomers"
        Me.cboCustomers.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cboCustomers.Size = New System.Drawing.Size(158, 27)
        Me.cboCustomers.TabIndex = 35
        Me.cboCustomers.Visible = False
        '
        'CboIssueTO
        '
        Me.CboIssueTO.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.CboIssueTO.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.CboIssueTO.FormattingEnabled = True
        Me.CboIssueTO.Location = New System.Drawing.Point(915, 226)
        Me.CboIssueTO.Name = "CboIssueTO"
        Me.CboIssueTO.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CboIssueTO.Size = New System.Drawing.Size(158, 27)
        Me.CboIssueTO.TabIndex = 36
        Me.CboIssueTO.Visible = False
        '
        'dtpIssueToFrom
        '
        Me.dtpIssueToFrom.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dtpIssueToFrom.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.dtpIssueToFrom.Location = New System.Drawing.Point(742, 227)
        Me.dtpIssueToFrom.Name = "dtpIssueToFrom"
        Me.dtpIssueToFrom.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dtpIssueToFrom.RightToLeftLayout = True
        Me.dtpIssueToFrom.Size = New System.Drawing.Size(136, 26)
        Me.dtpIssueToFrom.TabIndex = 28
        Me.dtpIssueToFrom.Visible = False
        '
        'dtpIssueToTo
        '
        Me.dtpIssueToTo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dtpIssueToTo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.dtpIssueToTo.Location = New System.Drawing.Point(568, 226)
        Me.dtpIssueToTo.Name = "dtpIssueToTo"
        Me.dtpIssueToTo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dtpIssueToTo.RightToLeftLayout = True
        Me.dtpIssueToTo.Size = New System.Drawing.Size(136, 26)
        Me.dtpIssueToTo.TabIndex = 28
        Me.dtpIssueToTo.Visible = False
        '
        'dtpCustomerFrom
        '
        Me.dtpCustomerFrom.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dtpCustomerFrom.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.dtpCustomerFrom.Location = New System.Drawing.Point(742, 193)
        Me.dtpCustomerFrom.Name = "dtpCustomerFrom"
        Me.dtpCustomerFrom.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dtpCustomerFrom.RightToLeftLayout = True
        Me.dtpCustomerFrom.Size = New System.Drawing.Size(136, 26)
        Me.dtpCustomerFrom.TabIndex = 29
        Me.dtpCustomerFrom.Visible = False
        '
        'dtpCustomerTo
        '
        Me.dtpCustomerTo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dtpCustomerTo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.dtpCustomerTo.Location = New System.Drawing.Point(568, 192)
        Me.dtpCustomerTo.Name = "dtpCustomerTo"
        Me.dtpCustomerTo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dtpCustomerTo.RightToLeftLayout = True
        Me.dtpCustomerTo.Size = New System.Drawing.Size(136, 26)
        Me.dtpCustomerTo.TabIndex = 29
        Me.dtpCustomerTo.Visible = False
        '
        'lblCustomersTo
        '
        Me.lblCustomersTo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblCustomersTo.AutoSize = True
        Me.lblCustomersTo.BackColor = System.Drawing.Color.Transparent
        Me.lblCustomersTo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblCustomersTo.ForeColor = System.Drawing.Color.White
        Me.lblCustomersTo.Location = New System.Drawing.Point(710, 197)
        Me.lblCustomersTo.Name = "lblCustomersTo"
        Me.lblCustomersTo.Size = New System.Drawing.Size(26, 19)
        Me.lblCustomersTo.TabIndex = 30
        Me.lblCustomersTo.Text = "الى"
        Me.lblCustomersTo.Visible = False
        '
        'lblIssueToTo
        '
        Me.lblIssueToTo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblIssueToTo.AutoSize = True
        Me.lblIssueToTo.BackColor = System.Drawing.Color.Transparent
        Me.lblIssueToTo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblIssueToTo.ForeColor = System.Drawing.Color.White
        Me.lblIssueToTo.Location = New System.Drawing.Point(710, 231)
        Me.lblIssueToTo.Name = "lblIssueToTo"
        Me.lblIssueToTo.Size = New System.Drawing.Size(26, 19)
        Me.lblIssueToTo.TabIndex = 30
        Me.lblIssueToTo.Text = "الى"
        Me.lblIssueToTo.Visible = False
        '
        'lblCustomersFrom
        '
        Me.lblCustomersFrom.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblCustomersFrom.AutoSize = True
        Me.lblCustomersFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblCustomersFrom.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblCustomersFrom.ForeColor = System.Drawing.Color.White
        Me.lblCustomersFrom.Location = New System.Drawing.Point(882, 197)
        Me.lblCustomersFrom.Name = "lblCustomersFrom"
        Me.lblCustomersFrom.Size = New System.Drawing.Size(25, 19)
        Me.lblCustomersFrom.TabIndex = 30
        Me.lblCustomersFrom.Text = "من"
        Me.lblCustomersFrom.Visible = False
        '
        'lblIssueToFrom
        '
        Me.lblIssueToFrom.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblIssueToFrom.AutoSize = True
        Me.lblIssueToFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblIssueToFrom.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblIssueToFrom.ForeColor = System.Drawing.Color.White
        Me.lblIssueToFrom.Location = New System.Drawing.Point(882, 231)
        Me.lblIssueToFrom.Name = "lblIssueToFrom"
        Me.lblIssueToFrom.Size = New System.Drawing.Size(25, 19)
        Me.lblIssueToFrom.TabIndex = 30
        Me.lblIssueToFrom.Text = "من"
        Me.lblIssueToFrom.Visible = False
        '
        'chkDateForCars
        '
        Me.chkDateForCars.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkDateForCars.AutoSize = True
        Me.chkDateForCars.BackColor = System.Drawing.Color.Transparent
        Me.chkDateForCars.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDateForCars.ForeColor = System.Drawing.Color.White
        Me.chkDateForCars.Location = New System.Drawing.Point(1213, 119)
        Me.chkDateForCars.Name = "chkDateForCars"
        Me.chkDateForCars.Size = New System.Drawing.Size(55, 26)
        Me.chkDateForCars.TabIndex = 37
        Me.chkDateForCars.Text = "فتره"
        Me.chkDateForCars.UseVisualStyleBackColor = False
        '
        'CboRecieveType
        '
        Me.CboRecieveType.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboRecieveType.FormattingEnabled = True
        Me.CboRecieveType.Items.AddRange(New Object() {"الكل", "العامرية", "الإسكندرية"})
        Me.CboRecieveType.Location = New System.Drawing.Point(566, 52)
        Me.CboRecieveType.Name = "CboRecieveType"
        Me.CboRecieveType.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CboRecieveType.Size = New System.Drawing.Size(142, 30)
        Me.CboRecieveType.TabIndex = 71
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label13.Location = New System.Drawing.Point(714, 52)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(85, 29)
        Me.Label13.TabIndex = 72
        Me.Label13.Text = "مكان التسليم"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(204, 252)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(120, 35)
        Me.Button1.TabIndex = 73
        Me.Button1.Text = "إرسال إيميل"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.UseVisualStyleBackColor = False
        Me.Button1.Visible = False
        '
        'chklstBoxScale
        '
        Me.chklstBoxScale.BackColor = System.Drawing.Color.DarkGray
        Me.chklstBoxScale.CheckOnClick = True
        Me.chklstBoxScale.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chklstBoxScale.FormattingEnabled = True
        Me.chklstBoxScale.Location = New System.Drawing.Point(302, 5)
        Me.chklstBoxScale.Name = "chklstBoxScale"
        Me.chklstBoxScale.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chklstBoxScale.Size = New System.Drawing.Size(163, 109)
        Me.chklstBoxScale.TabIndex = 74
        '
        'cboLocation
        '
        Me.cboLocation.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLocation.FormattingEnabled = True
        Me.cboLocation.Location = New System.Drawing.Point(22, 16)
        Me.cboLocation.Name = "cboLocation"
        Me.cboLocation.Size = New System.Drawing.Size(172, 25)
        Me.cboLocation.TabIndex = 85
        Me.cboLocation.Visible = False
        '
        'ChkLocation
        '
        Me.ChkLocation.BackColor = System.Drawing.Color.Transparent
        Me.ChkLocation.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkLocation.ForeColor = System.Drawing.Color.White
        Me.ChkLocation.Location = New System.Drawing.Point(200, 15)
        Me.ChkLocation.Name = "ChkLocation"
        Me.ChkLocation.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ChkLocation.Size = New System.Drawing.Size(75, 28)
        Me.ChkLocation.TabIndex = 86
        Me.ChkLocation.Text = "الموقع"
        Me.ChkLocation.UseVisualStyleBackColor = False
        '
        'frmAllReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.BackgroundImage = Global.SmartScale.My.Resources.Resources.blue
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1536, 605)
        Me.Controls.Add(Me.ChkLocation)
        Me.Controls.Add(Me.cboLocation)
        Me.Controls.Add(Me.chklstBoxScale)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CboRecieveType)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.chkDateForCars)
        Me.Controls.Add(Me.CboIssueTO)
        Me.Controls.Add(Me.cboCustomers)
        Me.Controls.Add(Me.rdbIssueTo)
        Me.Controls.Add(Me.rdbCustomer)
        Me.Controls.Add(Me.rdbPeriod)
        Me.Controls.Add(Me.chkTime)
        Me.Controls.Add(Me.lblIssueToTo)
        Me.Controls.Add(Me.lblCarTo)
        Me.Controls.Add(Me.lblAllTo)
        Me.Controls.Add(Me.lblCustomersTo)
        Me.Controls.Add(Me.lblProdTo)
        Me.Controls.Add(Me.lblIssueToFrom)
        Me.Controls.Add(Me.lblCarFrom)
        Me.Controls.Add(Me.lblCustomersFrom)
        Me.Controls.Add(Me.lblAllFrom)
        Me.Controls.Add(Me.lblProdFrom)
        Me.Controls.Add(Me.dtpAllTo)
        Me.Controls.Add(Me.dtpCustomerTo)
        Me.Controls.Add(Me.dtpProdTo)
        Me.Controls.Add(Me.dtpAllFrom)
        Me.Controls.Add(Me.dtpCustomerFrom)
        Me.Controls.Add(Me.dtpProdFrom)
        Me.Controls.Add(Me.dtpIssueToTo)
        Me.Controls.Add(Me.dtpCarTo)
        Me.Controls.Add(Me.dtpIssueToFrom)
        Me.Controls.Add(Me.dtpCarFrom)
        Me.Controls.Add(Me.chkNextDay)
        Me.Controls.Add(Me.lblTransto)
        Me.Controls.Add(Me.lblTransFrom)
        Me.Controls.Add(Me.txtTransTo)
        Me.Controls.Add(Me.txtTransFrom)
        Me.Controls.Add(Me.rdbTrans)
        Me.Controls.Add(Me.cboCar)
        Me.Controls.Add(Me.cboGoods)
        Me.Controls.Add(Me.rdbCar)
        Me.Controls.Add(Me.rdbGoods)
        Me.Controls.Add(Me.rdbDate)
        Me.Controls.Add(Me.rdbTime)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.crvAllReports)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.timFrom)
        Me.Controls.Add(Me.timTO)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmAllReports"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "                  "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents crvAllReports As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents BtnSearch As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents rdbTime As System.Windows.Forms.RadioButton
    Friend WithEvents rdbGoods As System.Windows.Forms.RadioButton
    Friend WithEvents rdbCar As System.Windows.Forms.RadioButton
    Friend WithEvents rdbDate As System.Windows.Forms.RadioButton
    Friend WithEvents timFrom As DateAndTimeControls.CompositeDateTimeControl
    Friend WithEvents cboGoods As System.Windows.Forms.ComboBox
    Friend WithEvents cboCar As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents timTO As DateAndTimeControls.CompositeDateTimeControl
    Friend WithEvents rdbTrans As System.Windows.Forms.RadioButton
    Friend WithEvents txtTransFrom As System.Windows.Forms.TextBox
    Friend WithEvents txtTransTo As System.Windows.Forms.TextBox
    Friend WithEvents lblTransFrom As System.Windows.Forms.Label
    Friend WithEvents lblTransto As System.Windows.Forms.Label
    Friend WithEvents chkNextDay As System.Windows.Forms.CheckBox
    Friend WithEvents dtpCarFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpCarTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpProdFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpProdTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblProdFrom As System.Windows.Forms.Label
    Friend WithEvents lblProdTo As System.Windows.Forms.Label
    Friend WithEvents lblCarFrom As System.Windows.Forms.Label
    Friend WithEvents lblCarTo As System.Windows.Forms.Label
    Friend WithEvents chkTime As System.Windows.Forms.CheckBox
    Friend WithEvents dtpAllFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpAllTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblAllFrom As System.Windows.Forms.Label
    Friend WithEvents lblAllTo As System.Windows.Forms.Label
    Friend WithEvents rdbPeriod As System.Windows.Forms.RadioButton
    Friend WithEvents rdbCustomer As System.Windows.Forms.RadioButton
    Friend WithEvents rdbIssueTo As System.Windows.Forms.RadioButton
    Friend WithEvents cboCustomers As System.Windows.Forms.ComboBox
    Friend WithEvents CboIssueTO As System.Windows.Forms.ComboBox
    Friend WithEvents dtpIssueToFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpIssueToTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpCustomerFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpCustomerTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblCustomersTo As System.Windows.Forms.Label
    Friend WithEvents lblIssueToTo As System.Windows.Forms.Label
    Friend WithEvents lblCustomersFrom As System.Windows.Forms.Label
    Friend WithEvents lblIssueToFrom As System.Windows.Forms.Label
    Friend WithEvents chkDateForCars As System.Windows.Forms.CheckBox
    Friend WithEvents CboRecieveType As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents chklstBoxScale As System.Windows.Forms.CheckedListBox
    Friend WithEvents cboLocation As System.Windows.Forms.ComboBox
    Friend WithEvents ChkLocation As System.Windows.Forms.CheckBox
End Class
