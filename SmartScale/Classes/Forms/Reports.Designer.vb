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
        Me.SuspendLayout()
        '
        'btnCarsInside
        '
        Me.btnCarsInside.BackColor = System.Drawing.Color.Gainsboro
        Me.btnCarsInside.Font = New System.Drawing.Font("Simplified Arabic", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnCarsInside.Image = Global.SmartScale.My.Resources.Resources.truck
        Me.btnCarsInside.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCarsInside.Location = New System.Drawing.Point(137, 96)
        Me.btnCarsInside.Name = "btnCarsInside"
        Me.btnCarsInside.Size = New System.Drawing.Size(221, 87)
        Me.btnCarsInside.TabIndex = 3
        Me.btnCarsInside.Text = "السيارات الموجوده بالداخل"
        Me.btnCarsInside.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.btnCarsInside.UseVisualStyleBackColor = False
        '
        'btnPreReports
        '
        Me.btnPreReports.BackColor = System.Drawing.Color.Gainsboro
        Me.btnPreReports.Font = New System.Drawing.Font("Simplified Arabic", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnPreReports.Image = Global.SmartScale.My.Resources.Resources.desktop
        Me.btnPreReports.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreReports.Location = New System.Drawing.Point(255, 29)
        Me.btnPreReports.Name = "btnPreReports"
        Me.btnPreReports.Size = New System.Drawing.Size(221, 50)
        Me.btnPreReports.TabIndex = 4
        Me.btnPreReports.Text = "تقرير حركات سابقه"
        Me.btnPreReports.UseVisualStyleBackColor = False
        '
        'btnAllReports
        '
        Me.btnAllReports.BackColor = System.Drawing.Color.Gainsboro
        Me.btnAllReports.Font = New System.Drawing.Font("Simplified Arabic", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnAllReports.Image = Global.SmartScale.My.Resources.Resources.text_align_right
        Me.btnAllReports.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAllReports.Location = New System.Drawing.Point(19, 29)
        Me.btnAllReports.Name = "btnAllReports"
        Me.btnAllReports.Size = New System.Drawing.Size(221, 50)
        Me.btnAllReports.TabIndex = 5
        Me.btnAllReports.Text = "تقارير حركات اليوم"
        Me.btnAllReports.UseVisualStyleBackColor = False
        '
        'Reports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SmartScale.My.Resources.Resources.blue
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(494, 196)
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
End Class
