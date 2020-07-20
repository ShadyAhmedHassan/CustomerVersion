<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEdit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEdit))
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtCarNo = New System.Windows.Forms.TextBox
        Me.cboDealer = New System.Windows.Forms.ComboBox
        Me.cboDriver = New System.Windows.Forms.ComboBox
        Me.cboGoodType = New System.Windows.Forms.ComboBox
        Me.txtFirstScale = New System.Windows.Forms.TextBox
        Me.txtSecondScale = New System.Windows.Forms.TextBox
        Me.cboIssueTo = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblFirstScale = New System.Windows.Forms.Label
        Me.lblSecondScale = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.txtManualEdit1 = New System.Windows.Forms.TextBox
        Me.chkManualEdit = New System.Windows.Forms.CheckBox
        Me.btnRead1stScale = New System.Windows.Forms.Button
        Me.txtManualEdit2 = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.BtnCancel = New System.Windows.Forms.Button
        Me.btnEdit = New System.Windows.Forms.Button
        Me.txtLicence = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtCarInfo = New System.Windows.Forms.TextBox
        Me.cboCarCity = New System.Windows.Forms.ComboBox
        Me.cboCarInfoCity = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.OutTime = New DateAndTimeControls.CompositeDateTimeControl
        Me.chkEditOutTime = New System.Windows.Forms.CheckBox
        Me.lblTransNumber = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label21
        '
        Me.Label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label21.Font = New System.Drawing.Font("Simplified Arabic", 20.0!, System.Drawing.FontStyle.Bold)
        Me.Label21.Location = New System.Drawing.Point(105, 9)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(251, 45)
        Me.Label21.TabIndex = 15
        Me.Label21.Text = "تعديــل بيانــات"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(362, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 22)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "رقم السياره"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(362, 134)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 22)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "اسم العميل"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(362, 170)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 22)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "اسم السائق"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(362, 242)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 22)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "نوع الحموله"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(362, 278)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 22)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "الوزنه الاولى"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(362, 313)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 22)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "الوزنه الثانيه"
        '
        'txtCarNo
        '
        Me.txtCarNo.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCarNo.Location = New System.Drawing.Point(221, 64)
        Me.txtCarNo.Name = "txtCarNo"
        Me.txtCarNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtCarNo.Size = New System.Drawing.Size(135, 29)
        Me.txtCarNo.TabIndex = 19
        Me.txtCarNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cboDealer
        '
        Me.cboDealer.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDealer.FormattingEnabled = True
        Me.cboDealer.Location = New System.Drawing.Point(105, 134)
        Me.cboDealer.Name = "cboDealer"
        Me.cboDealer.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cboDealer.Size = New System.Drawing.Size(251, 30)
        Me.cboDealer.TabIndex = 20
        '
        'cboDriver
        '
        Me.cboDriver.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDriver.FormattingEnabled = True
        Me.cboDriver.Location = New System.Drawing.Point(105, 170)
        Me.cboDriver.Name = "cboDriver"
        Me.cboDriver.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cboDriver.Size = New System.Drawing.Size(251, 30)
        Me.cboDriver.TabIndex = 21
        '
        'cboGoodType
        '
        Me.cboGoodType.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGoodType.FormattingEnabled = True
        Me.cboGoodType.Location = New System.Drawing.Point(105, 242)
        Me.cboGoodType.Name = "cboGoodType"
        Me.cboGoodType.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cboGoodType.Size = New System.Drawing.Size(251, 30)
        Me.cboGoodType.TabIndex = 22
        '
        'txtFirstScale
        '
        Me.txtFirstScale.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFirstScale.Location = New System.Drawing.Point(-11, 246)
        Me.txtFirstScale.Name = "txtFirstScale"
        Me.txtFirstScale.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtFirstScale.Size = New System.Drawing.Size(123, 29)
        Me.txtFirstScale.TabIndex = 23
        Me.txtFirstScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtFirstScale.Visible = False
        '
        'txtSecondScale
        '
        Me.txtSecondScale.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSecondScale.Location = New System.Drawing.Point(-11, 281)
        Me.txtSecondScale.Name = "txtSecondScale"
        Me.txtSecondScale.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtSecondScale.Size = New System.Drawing.Size(123, 29)
        Me.txtSecondScale.TabIndex = 24
        Me.txtSecondScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtSecondScale.Visible = False
        '
        'cboIssueTo
        '
        Me.cboIssueTo.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboIssueTo.FormattingEnabled = True
        Me.cboIssueTo.Location = New System.Drawing.Point(105, 206)
        Me.cboIssueTo.Name = "cboIssueTo"
        Me.cboIssueTo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cboIssueTo.Size = New System.Drawing.Size(249, 30)
        Me.cboIssueTo.TabIndex = 25
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(362, 207)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(81, 22)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "المرسل اليه"
        '
        'lblFirstScale
        '
        Me.lblFirstScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFirstScale.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFirstScale.Location = New System.Drawing.Point(232, 278)
        Me.lblFirstScale.Name = "lblFirstScale"
        Me.lblFirstScale.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblFirstScale.Size = New System.Drawing.Size(123, 29)
        Me.lblFirstScale.TabIndex = 26
        Me.lblFirstScale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSecondScale
        '
        Me.lblSecondScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSecondScale.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSecondScale.Location = New System.Drawing.Point(232, 313)
        Me.lblSecondScale.Name = "lblSecondScale"
        Me.lblSecondScale.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblSecondScale.Size = New System.Drawing.Size(123, 29)
        Me.lblSecondScale.TabIndex = 27
        Me.lblSecondScale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(164, 313)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(53, 30)
        Me.Button1.TabIndex = 28
        Me.Button1.Text = "قراءه"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtManualEdit1
        '
        Me.txtManualEdit1.Enabled = False
        Me.txtManualEdit1.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.txtManualEdit1.Location = New System.Drawing.Point(242, 411)
        Me.txtManualEdit1.MaxLength = 6
        Me.txtManualEdit1.Name = "txtManualEdit1"
        Me.txtManualEdit1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtManualEdit1.Size = New System.Drawing.Size(114, 29)
        Me.txtManualEdit1.TabIndex = 29
        Me.txtManualEdit1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkManualEdit
        '
        Me.chkManualEdit.AutoSize = True
        Me.chkManualEdit.BackColor = System.Drawing.Color.Transparent
        Me.chkManualEdit.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.chkManualEdit.ForeColor = System.Drawing.Color.White
        Me.chkManualEdit.Location = New System.Drawing.Point(12, 411)
        Me.chkManualEdit.Name = "chkManualEdit"
        Me.chkManualEdit.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkManualEdit.Size = New System.Drawing.Size(100, 26)
        Me.chkManualEdit.TabIndex = 30
        Me.chkManualEdit.Text = "تعديل يدوى"
        Me.chkManualEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkManualEdit.UseVisualStyleBackColor = False
        '
        'btnRead1stScale
        '
        Me.btnRead1stScale.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnRead1stScale.Location = New System.Drawing.Point(164, 280)
        Me.btnRead1stScale.Name = "btnRead1stScale"
        Me.btnRead1stScale.Size = New System.Drawing.Size(53, 30)
        Me.btnRead1stScale.TabIndex = 31
        Me.btnRead1stScale.Text = "قراءه"
        Me.btnRead1stScale.UseVisualStyleBackColor = True
        Me.btnRead1stScale.Visible = False
        '
        'txtManualEdit2
        '
        Me.txtManualEdit2.Enabled = False
        Me.txtManualEdit2.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.txtManualEdit2.Location = New System.Drawing.Point(122, 411)
        Me.txtManualEdit2.Name = "txtManualEdit2"
        Me.txtManualEdit2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtManualEdit2.Size = New System.Drawing.Size(114, 29)
        Me.txtManualEdit2.TabIndex = 32
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(266, 386)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(90, 22)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "الوزنه الاولى"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(148, 387)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 22)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "الوزنه الثانيه"
        '
        'BtnCancel
        '
        Me.BtnCancel.BackColor = System.Drawing.Color.AliceBlue
        Me.BtnCancel.Font = New System.Drawing.Font("Simplified Arabic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.BtnCancel.Image = Global.SmartScale.My.Resources.Resources.BtnDelete_Image
        Me.BtnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnCancel.Location = New System.Drawing.Point(112, 463)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(115, 37)
        Me.BtnCancel.TabIndex = 17
        Me.BtnCancel.Text = "الغاء"
        Me.BtnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnCancel.UseVisualStyleBackColor = False
        '
        'btnEdit
        '
        Me.btnEdit.BackColor = System.Drawing.Color.AliceBlue
        Me.btnEdit.Font = New System.Drawing.Font("Simplified Arabic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnEdit.Image = Global.SmartScale.My.Resources.Resources.BtnSave_Image
        Me.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEdit.Location = New System.Drawing.Point(233, 463)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(115, 37)
        Me.btnEdit.TabIndex = 16
        Me.btnEdit.Tag = "Car"
        Me.btnEdit.Text = "تعديل"
        Me.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEdit.UseVisualStyleBackColor = False
        '
        'txtLicence
        '
        Me.txtLicence.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.txtLicence.Location = New System.Drawing.Point(11, 170)
        Me.txtLicence.Name = "txtLicence"
        Me.txtLicence.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtLicence.Size = New System.Drawing.Size(88, 29)
        Me.txtLicence.TabIndex = 39
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(13, 145)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(85, 22)
        Me.Label14.TabIndex = 38
        Me.Label14.Text = "رقم الرخصه"
        '
        'txtCarInfo
        '
        Me.txtCarInfo.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.txtCarInfo.Location = New System.Drawing.Point(221, 99)
        Me.txtCarInfo.Name = "txtCarInfo"
        Me.txtCarInfo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtCarInfo.Size = New System.Drawing.Size(135, 29)
        Me.txtCarInfo.TabIndex = 40
        '
        'cboCarCity
        '
        Me.cboCarCity.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.cboCarCity.FormattingEnabled = True
        Me.cboCarCity.Location = New System.Drawing.Point(11, 61)
        Me.cboCarCity.Name = "cboCarCity"
        Me.cboCarCity.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cboCarCity.Size = New System.Drawing.Size(134, 30)
        Me.cboCarCity.TabIndex = 41
        '
        'cboCarInfoCity
        '
        Me.cboCarInfoCity.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.cboCarInfoCity.FormattingEnabled = True
        Me.cboCarInfoCity.Location = New System.Drawing.Point(12, 97)
        Me.cboCarInfoCity.Name = "cboCarInfoCity"
        Me.cboCarInfoCity.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cboCarInfoCity.Size = New System.Drawing.Size(133, 30)
        Me.cboCarInfoCity.TabIndex = 42
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(363, 102)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(92, 22)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "رقم المقطوره"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(151, 100)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(64, 22)
        Me.Label11.TabIndex = 18
        Me.Label11.Text = "المحافظه"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.Location = New System.Drawing.Point(151, 64)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(64, 22)
        Me.Label12.TabIndex = 18
        Me.Label12.Text = "المحافظه"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(355, 349)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(97, 25)
        Me.Label13.TabIndex = 18
        Me.Label13.Text = "تاريخ الخروج"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'OutTime
        '
        Me.OutTime.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.OutTime.BackColor = System.Drawing.Color.Transparent
        Me.OutTime.DateCustom = "MMM-dd-yyyy"
        Me.OutTime.DateFormat = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.OutTime.DateTimeValue = New Date(2009, 5, 13, 20, 0, 0, 0)
        Me.OutTime.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OutTime.Location = New System.Drawing.Point(168, 349)
        Me.OutTime.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.OutTime.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.OutTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.OutTime.Name = "OutTime"
        Me.OutTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.OutTime.Size = New System.Drawing.Size(188, 33)
        Me.OutTime.TabIndex = 43
        Me.OutTime.TimeCustom = "hh:mm tt"
        Me.OutTime.TimeFormat = System.Windows.Forms.DateTimePickerFormat.Custom
        '
        'chkEditOutTime
        '
        Me.chkEditOutTime.AutoSize = True
        Me.chkEditOutTime.BackColor = System.Drawing.Color.Transparent
        Me.chkEditOutTime.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEditOutTime.ForeColor = System.Drawing.Color.White
        Me.chkEditOutTime.Location = New System.Drawing.Point(8, 349)
        Me.chkEditOutTime.Name = "chkEditOutTime"
        Me.chkEditOutTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkEditOutTime.Size = New System.Drawing.Size(151, 26)
        Me.chkEditOutTime.TabIndex = 44
        Me.chkEditOutTime.Text = "تعديل تاريخ الخروج"
        Me.chkEditOutTime.UseVisualStyleBackColor = False
        '
        'lblTransNumber
        '
        Me.lblTransNumber.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblTransNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTransNumber.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransNumber.ForeColor = System.Drawing.Color.Red
        Me.lblTransNumber.Location = New System.Drawing.Point(4, 24)
        Me.lblTransNumber.Name = "lblTransNumber"
        Me.lblTransNumber.Size = New System.Drawing.Size(95, 23)
        Me.lblTransNumber.TabIndex = 51
        Me.lblTransNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.White
        Me.Label19.Location = New System.Drawing.Point(24, 5)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(48, 15)
        Me.Label19.TabIndex = 50
        Me.Label19.Text = "بطاقه رقم"
        '
        'frmEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.BackgroundImage = Global.SmartScale.My.Resources.Resources.blue
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(461, 510)
        Me.Controls.Add(Me.lblTransNumber)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.chkEditOutTime)
        Me.Controls.Add(Me.cboCarInfoCity)
        Me.Controls.Add(Me.cboCarCity)
        Me.Controls.Add(Me.txtCarInfo)
        Me.Controls.Add(Me.txtLicence)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtManualEdit2)
        Me.Controls.Add(Me.btnRead1stScale)
        Me.Controls.Add(Me.chkManualEdit)
        Me.Controls.Add(Me.txtManualEdit1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.lblSecondScale)
        Me.Controls.Add(Me.lblFirstScale)
        Me.Controls.Add(Me.cboIssueTo)
        Me.Controls.Add(Me.txtSecondScale)
        Me.Controls.Add(Me.txtFirstScale)
        Me.Controls.Add(Me.cboGoodType)
        Me.Controls.Add(Me.cboDriver)
        Me.Controls.Add(Me.cboDealer)
        Me.Controls.Add(Me.txtCarNo)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.OutTime)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCarNo As System.Windows.Forms.TextBox
    Friend WithEvents cboDealer As System.Windows.Forms.ComboBox
    Friend WithEvents cboDriver As System.Windows.Forms.ComboBox
    Friend WithEvents cboGoodType As System.Windows.Forms.ComboBox
    Friend WithEvents txtFirstScale As System.Windows.Forms.TextBox
    Friend WithEvents txtSecondScale As System.Windows.Forms.TextBox
    Friend WithEvents cboIssueTo As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblFirstScale As System.Windows.Forms.Label
    Friend WithEvents lblSecondScale As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtManualEdit1 As System.Windows.Forms.TextBox
    Friend WithEvents chkManualEdit As System.Windows.Forms.CheckBox
    Friend WithEvents btnRead1stScale As System.Windows.Forms.Button
    Friend WithEvents txtManualEdit2 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtLicence As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtCarInfo As System.Windows.Forms.TextBox
    Friend WithEvents cboCarCity As System.Windows.Forms.ComboBox
    Friend WithEvents cboCarInfoCity As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents OutTime As DateAndTimeControls.CompositeDateTimeControl
    Friend WithEvents chkEditOutTime As System.Windows.Forms.CheckBox
    Friend WithEvents lblTransNumber As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
End Class
