<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class cfrmChoose
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(cfrmChoose))
        Me.btnCustQnty = New System.Windows.Forms.Button
        Me.btnManEditRpt = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.BtncfrmAddNew = New System.Windows.Forms.Button
        Me.BtncfrmReport = New System.Windows.Forms.Button
        Me.BtncfrmMain = New System.Windows.Forms.Button
        Me.btnReturns = New System.Windows.Forms.Button
        Me.BtnCollectedMotions = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnCustQnty
        '
        Me.btnCustQnty.BackColor = System.Drawing.Color.Gainsboro
        Me.btnCustQnty.Font = New System.Drawing.Font("Times New Roman", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustQnty.Image = Global.SmartScale.My.Resources.Resources.yen
        Me.btnCustQnty.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCustQnty.Location = New System.Drawing.Point(20, 149)
        Me.btnCustQnty.Name = "btnCustQnty"
        Me.btnCustQnty.Size = New System.Drawing.Size(221, 50)
        Me.btnCustQnty.TabIndex = 6
        Me.btnCustQnty.Text = "تقرير الكميات المتعاقد عليها"
        Me.btnCustQnty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCustQnty.UseVisualStyleBackColor = False
        '
        'btnManEditRpt
        '
        Me.btnManEditRpt.BackColor = System.Drawing.Color.Gainsboro
        Me.btnManEditRpt.Font = New System.Drawing.Font("Times New Roman", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnManEditRpt.Image = Global.SmartScale.My.Resources.Resources.edit
        Me.btnManEditRpt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnManEditRpt.Location = New System.Drawing.Point(20, 84)
        Me.btnManEditRpt.Name = "btnManEditRpt"
        Me.btnManEditRpt.Size = New System.Drawing.Size(221, 50)
        Me.btnManEditRpt.TabIndex = 5
        Me.btnManEditRpt.Text = "تقرير الحركات المعدلة يدويا"
        Me.btnManEditRpt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnManEditRpt.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Gainsboro
        Me.Button1.Font = New System.Drawing.Font("Times New Roman", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Image = Global.SmartScale.My.Resources.Resources.text_align_right
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(20, 17)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(221, 50)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "تقارير حركات اليوم"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'BtncfrmAddNew
        '
        Me.BtncfrmAddNew.BackColor = System.Drawing.Color.Gainsboro
        Me.BtncfrmAddNew.Font = New System.Drawing.Font("Times New Roman", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtncfrmAddNew.Image = Global.SmartScale.My.Resources.Resources.kdict
        Me.BtncfrmAddNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtncfrmAddNew.Location = New System.Drawing.Point(259, 84)
        Me.BtncfrmAddNew.Name = "BtncfrmAddNew"
        Me.BtncfrmAddNew.Size = New System.Drawing.Size(221, 50)
        Me.BtncfrmAddNew.TabIndex = 1
        Me.BtncfrmAddNew.Text = "البيانــات الاساسيــة"
        Me.BtncfrmAddNew.UseVisualStyleBackColor = False
        '
        'BtncfrmReport
        '
        Me.BtncfrmReport.BackColor = System.Drawing.Color.Gainsboro
        Me.BtncfrmReport.Font = New System.Drawing.Font("Times New Roman", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtncfrmReport.Image = Global.SmartScale.My.Resources.Resources.desktop
        Me.BtncfrmReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtncfrmReport.Location = New System.Drawing.Point(259, 149)
        Me.BtncfrmReport.Name = "BtncfrmReport"
        Me.BtncfrmReport.Size = New System.Drawing.Size(221, 50)
        Me.BtncfrmReport.TabIndex = 2
        Me.BtncfrmReport.Text = "تقرير حركات سابقة"
        Me.BtncfrmReport.UseVisualStyleBackColor = False
        '
        'BtncfrmMain
        '
        Me.BtncfrmMain.BackColor = System.Drawing.Color.Gainsboro
        Me.BtncfrmMain.Font = New System.Drawing.Font("Times New Roman", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtncfrmMain.Image = Global.SmartScale.My.Resources.Resources.arrow_up_blue
        Me.BtncfrmMain.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtncfrmMain.Location = New System.Drawing.Point(257, 17)
        Me.BtncfrmMain.Name = "BtncfrmMain"
        Me.BtncfrmMain.Size = New System.Drawing.Size(221, 50)
        Me.BtncfrmMain.TabIndex = 0
        Me.BtncfrmMain.Text = "العمليات الصادرة - العملاء"
        Me.BtncfrmMain.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtncfrmMain.UseVisualStyleBackColor = False
        '
        'btnReturns
        '
        Me.btnReturns.BackColor = System.Drawing.Color.Gainsboro
        Me.btnReturns.Font = New System.Drawing.Font("Times New Roman", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReturns.Location = New System.Drawing.Point(20, 211)
        Me.btnReturns.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnReturns.Name = "btnReturns"
        Me.btnReturns.Size = New System.Drawing.Size(221, 50)
        Me.btnReturns.TabIndex = 7
        Me.btnReturns.Text = "المرتجعات"
        Me.btnReturns.UseVisualStyleBackColor = False
        '
        'BtnCollectedMotions
        '
        Me.BtnCollectedMotions.BackColor = System.Drawing.Color.Gainsboro
        Me.BtnCollectedMotions.Font = New System.Drawing.Font("Times New Roman", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCollectedMotions.Image = Global.SmartScale.My.Resources.Resources.desktop
        Me.BtnCollectedMotions.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnCollectedMotions.Location = New System.Drawing.Point(257, 211)
        Me.BtnCollectedMotions.Name = "BtnCollectedMotions"
        Me.BtnCollectedMotions.Size = New System.Drawing.Size(221, 50)
        Me.BtnCollectedMotions.TabIndex = 8
        Me.BtnCollectedMotions.Text = "تقرير الحركات مجمعه"
        Me.BtnCollectedMotions.UseVisualStyleBackColor = False
        '
        'cfrmChoose
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.BackgroundImage = Global.SmartScale.My.Resources.Resources.blue
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(498, 280)
        Me.Controls.Add(Me.BtnCollectedMotions)
        Me.Controls.Add(Me.btnReturns)
        Me.Controls.Add(Me.btnCustQnty)
        Me.Controls.Add(Me.btnManEditRpt)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.BtncfrmAddNew)
        Me.Controls.Add(Me.BtncfrmReport)
        Me.Controls.Add(Me.BtncfrmMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "cfrmChoose"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "الصادر"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtncfrmMain As System.Windows.Forms.Button
    Friend WithEvents BtncfrmReport As System.Windows.Forms.Button
    Friend WithEvents BtncfrmAddNew As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnManEditRpt As System.Windows.Forms.Button
    Friend WithEvents btnCustQnty As System.Windows.Forms.Button
    Friend WithEvents btnReturns As System.Windows.Forms.Button
    Friend WithEvents BtnCollectedMotions As System.Windows.Forms.Button
End Class
