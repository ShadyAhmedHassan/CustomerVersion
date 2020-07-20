<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Reports
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Reports))
        Me.btnCarsInside = New System.Windows.Forms.Button
        Me.btnPreReports = New System.Windows.Forms.Button
        Me.btnAllReports = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.btnReportQuota = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnCarsInside
        '
        Me.btnCarsInside.BackColor = System.Drawing.Color.Gainsboro
        Me.btnCarsInside.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCarsInside.Image = Global.SmartScale.My.Resources.Resources.truck
        Me.btnCarsInside.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCarsInside.Location = New System.Drawing.Point(19, 189)
        Me.btnCarsInside.Name = "btnCarsInside"
        Me.btnCarsInside.Size = New System.Drawing.Size(550, 133)
        Me.btnCarsInside.TabIndex = 3
        Me.btnCarsInside.Text = "السيارات الموجودة بالداخل"
        Me.btnCarsInside.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.btnCarsInside.UseVisualStyleBackColor = False
        '
        'btnPreReports
        '
        Me.btnPreReports.BackColor = System.Drawing.Color.Gainsboro
        Me.btnPreReports.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreReports.Image = Global.SmartScale.My.Resources.Resources.desktop
        Me.btnPreReports.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreReports.Location = New System.Drawing.Point(348, 17)
        Me.btnPreReports.Name = "btnPreReports"
        Me.btnPreReports.Size = New System.Drawing.Size(221, 50)
        Me.btnPreReports.TabIndex = 4
        Me.btnPreReports.Text = "تقرير حركات سابقة"
        Me.btnPreReports.UseVisualStyleBackColor = False
        '
        'btnAllReports
        '
        Me.btnAllReports.BackColor = System.Drawing.Color.Gainsboro
        Me.btnAllReports.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAllReports.Image = Global.SmartScale.My.Resources.Resources.text_align_right
        Me.btnAllReports.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAllReports.Location = New System.Drawing.Point(19, 17)
        Me.btnAllReports.Name = "btnAllReports"
        Me.btnAllReports.Size = New System.Drawing.Size(323, 50)
        Me.btnAllReports.TabIndex = 5
        Me.btnAllReports.Text = "تقارير حركات اليوم"
        Me.btnAllReports.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Gainsboro
        Me.Button1.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Image = Global.SmartScale.My.Resources.Resources.text_align_right
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(19, 129)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(323, 50)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "كارتة الميزان"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'btnReportQuota
        '
        Me.btnReportQuota.BackColor = System.Drawing.Color.Gainsboro
        Me.btnReportQuota.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReportQuota.Image = Global.SmartScale.My.Resources.Resources.text_align_right
        Me.btnReportQuota.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReportQuota.Location = New System.Drawing.Point(348, 73)
        Me.btnReportQuota.Name = "btnReportQuota"
        Me.btnReportQuota.Size = New System.Drawing.Size(221, 50)
        Me.btnReportQuota.TabIndex = 6
        Me.btnReportQuota.Text = "تقرير الكوتـــة"
        Me.btnReportQuota.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Gainsboro
        Me.Button2.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Image = Global.SmartScale.My.Resources.Resources.text_align_right
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.Location = New System.Drawing.Point(348, 130)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(221, 50)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "الكارتات المعدله"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.Gainsboro
        Me.Button3.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Image = Global.SmartScale.My.Resources.Resources.text_align_right
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button3.Location = New System.Drawing.Point(19, 73)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(323, 50)
        Me.Button3.TabIndex = 8
        Me.Button3.Text = "تقرير الكوتـــة المتبقية للمبيعات"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Reports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SmartScale.My.Resources.Resources.blue
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(574, 334)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnReportQuota)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnAllReports)
        Me.Controls.Add(Me.btnPreReports)
        Me.Controls.Add(Me.btnCarsInside)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Reports"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تقارير"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCarsInside As System.Windows.Forms.Button
    Friend WithEvents btnPreReports As System.Windows.Forms.Button
    Friend WithEvents btnAllReports As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnReportQuota As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
End Class
