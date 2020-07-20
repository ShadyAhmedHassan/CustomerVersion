<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class cfrmLogin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(cfrmLogin))
        Me.pnlChangePassword = New System.Windows.Forms.Panel
        Me.btnChangePassword = New System.Windows.Forms.Button
        Me.txtNewPasswordConfirmation = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtNewPassword = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.lnkChangePassword = New System.Windows.Forms.LinkLabel
        Me.cboEmpName = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnLogin = New System.Windows.Forms.Button
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.pnlPassword = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblMsg = New System.Windows.Forms.Label
        Me.PicBoxUser = New System.Windows.Forms.PictureBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cboScale = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.pnlChangePassword.SuspendLayout()
        Me.pnlPassword.SuspendLayout()
        CType(Me.PicBoxUser, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlChangePassword
        '
        Me.pnlChangePassword.Controls.Add(Me.btnChangePassword)
        Me.pnlChangePassword.Controls.Add(Me.txtNewPasswordConfirmation)
        Me.pnlChangePassword.Controls.Add(Me.Label5)
        Me.pnlChangePassword.Controls.Add(Me.txtNewPassword)
        Me.pnlChangePassword.Controls.Add(Me.Label4)
        Me.pnlChangePassword.Location = New System.Drawing.Point(10, 208)
        Me.pnlChangePassword.Name = "pnlChangePassword"
        Me.pnlChangePassword.Size = New System.Drawing.Size(345, 84)
        Me.pnlChangePassword.TabIndex = 4
        Me.pnlChangePassword.Visible = False
        '
        'btnChangePassword
        '
        Me.btnChangePassword.BackColor = System.Drawing.Color.AliceBlue
        Me.btnChangePassword.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChangePassword.Location = New System.Drawing.Point(3, 44)
        Me.btnChangePassword.Name = "btnChangePassword"
        Me.btnChangePassword.Size = New System.Drawing.Size(141, 33)
        Me.btnChangePassword.TabIndex = 4
        Me.btnChangePassword.Text = "تغيير كلمة السر"
        Me.btnChangePassword.UseVisualStyleBackColor = False
        '
        'txtNewPasswordConfirmation
        '
        Me.txtNewPasswordConfirmation.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.txtNewPasswordConfirmation.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewPasswordConfirmation.Location = New System.Drawing.Point(147, 44)
        Me.txtNewPasswordConfirmation.MaxLength = 8
        Me.txtNewPasswordConfirmation.Name = "txtNewPasswordConfirmation"
        Me.txtNewPasswordConfirmation.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtNewPasswordConfirmation.Size = New System.Drawing.Size(96, 33)
        Me.txtNewPasswordConfirmation.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(249, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 34)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "تأكيد كلمة السر الجديدة:"
        '
        'txtNewPassword
        '
        Me.txtNewPassword.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.txtNewPassword.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewPassword.Location = New System.Drawing.Point(147, 5)
        Me.txtNewPassword.MaxLength = 8
        Me.txtNewPassword.Name = "txtNewPassword"
        Me.txtNewPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtNewPassword.Size = New System.Drawing.Size(96, 33)
        Me.txtNewPassword.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(268, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 30)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "كلمة السر الجديدة:"
        '
        'lnkChangePassword
        '
        Me.lnkChangePassword.AutoSize = True
        Me.lnkChangePassword.Location = New System.Drawing.Point(5, 41)
        Me.lnkChangePassword.Name = "lnkChangePassword"
        Me.lnkChangePassword.Size = New System.Drawing.Size(76, 13)
        Me.lnkChangePassword.TabIndex = 14
        Me.lnkChangePassword.TabStop = True
        Me.lnkChangePassword.Text = "تغيير كلمة السر"
        '
        'cboEmpName
        '
        Me.cboEmpName.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.cboEmpName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cboEmpName.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEmpName.FormattingEnabled = True
        Me.cboEmpName.Location = New System.Drawing.Point(11, 105)
        Me.cboEmpName.Name = "cboEmpName"
        Me.cboEmpName.Size = New System.Drawing.Size(243, 33)
        Me.cboEmpName.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(267, 115)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "إسم الموظف:"
        '
        'btnLogin
        '
        Me.btnLogin.BackColor = System.Drawing.Color.AliceBlue
        Me.btnLogin.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogin.Location = New System.Drawing.Point(3, 3)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(141, 33)
        Me.btnLogin.TabIndex = 2
        Me.btnLogin.Text = "تسجيل الدخول"
        Me.btnLogin.UseVisualStyleBackColor = False
        '
        'txtPassword
        '
        Me.txtPassword.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.txtPassword.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(147, 4)
        Me.txtPassword.MaxLength = 8
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(96, 33)
        Me.txtPassword.TabIndex = 2
        '
        'pnlPassword
        '
        Me.pnlPassword.Controls.Add(Me.btnLogin)
        Me.pnlPassword.Controls.Add(Me.Label2)
        Me.pnlPassword.Controls.Add(Me.lnkChangePassword)
        Me.pnlPassword.Controls.Add(Me.txtPassword)
        Me.pnlPassword.Location = New System.Drawing.Point(10, 145)
        Me.pnlPassword.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.pnlPassword.Name = "pnlPassword"
        Me.pnlPassword.Size = New System.Drawing.Size(345, 58)
        Me.pnlPassword.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(263, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "كلمة السر:"
        '
        'lblMsg
        '
        Me.lblMsg.AutoSize = True
        Me.lblMsg.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMsg.Location = New System.Drawing.Point(110, 4)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(295, 13)
        Me.lblMsg.TabIndex = 0
        Me.lblMsg.Text = "برجاء ادخال اسم المستخدم وكلمه السر  بطريقه صحيحة"
        Me.lblMsg.Visible = False
        '
        'PicBoxUser
        '
        Me.PicBoxUser.Image = Global.SmartScale.My.Resources.Resources.TOLEDO
        Me.PicBoxUser.Location = New System.Drawing.Point(365, 30)
        Me.PicBoxUser.Name = "PicBoxUser"
        Me.PicBoxUser.Size = New System.Drawing.Size(133, 114)
        Me.PicBoxUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicBoxUser.TabIndex = 28
        Me.PicBoxUser.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(295, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "الميزان :"
        Me.Label3.Visible = False
        '
        'cboScale
        '
        Me.cboScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboScale.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.cboScale.FormattingEnabled = True
        Me.cboScale.Location = New System.Drawing.Point(10, 69)
        Me.cboScale.Name = "cboScale"
        Me.cboScale.Size = New System.Drawing.Size(244, 27)
        Me.cboScale.TabIndex = 0
        Me.cboScale.Visible = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label6.Location = New System.Drawing.Point(0, 298)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label6.Size = New System.Drawing.Size(506, 24)
        Me.Label6.TabIndex = 33
        Me.Label6.Text = "Version 02.02.27- Date: 13-05-2020"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.AliceBlue
        Me.Button1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(214, 30)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(141, 33)
        Me.Button1.TabIndex = 34
        Me.Button1.Text = "تحميل كل الموازين"
        Me.Button1.UseVisualStyleBackColor = False
        Me.Button1.Visible = False
        '
        'cfrmLogin
        '
        Me.AcceptButton = Me.btnLogin
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(506, 322)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboScale)
        Me.Controls.Add(Me.pnlChangePassword)
        Me.Controls.Add(Me.PicBoxUser)
        Me.Controls.Add(Me.cboEmpName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pnlPassword)
        Me.Controls.Add(Me.lblMsg)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MaximizeBox = False
        Me.Name = "cfrmLogin"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "الصادر"
        Me.pnlChangePassword.ResumeLayout(False)
        Me.pnlChangePassword.PerformLayout()
        Me.pnlPassword.ResumeLayout(False)
        Me.pnlPassword.PerformLayout()
        CType(Me.PicBoxUser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlChangePassword As System.Windows.Forms.Panel
    Friend WithEvents btnChangePassword As System.Windows.Forms.Button
    Friend WithEvents txtNewPasswordConfirmation As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtNewPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lnkChangePassword As System.Windows.Forms.LinkLabel
    Friend WithEvents PicBoxUser As System.Windows.Forms.PictureBox
    Friend WithEvents cboEmpName As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents pnlPassword As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboScale As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
