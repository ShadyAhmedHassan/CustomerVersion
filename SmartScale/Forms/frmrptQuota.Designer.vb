<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmrptQuota
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmrptQuota))
        Me.btnSearchQuota = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtpReportQuota = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.crvQuota = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.CboLocation = New System.Windows.Forms.ComboBox
        Me.ChkLocation = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'btnSearchQuota
        '
        Me.btnSearchQuota.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnSearchQuota.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnSearchQuota.Image = CType(resources.GetObject("btnSearchQuota.Image"), System.Drawing.Image)
        Me.btnSearchQuota.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearchQuota.Location = New System.Drawing.Point(12, 54)
        Me.btnSearchQuota.Name = "btnSearchQuota"
        Me.btnSearchQuota.Size = New System.Drawing.Size(120, 35)
        Me.btnSearchQuota.TabIndex = 41
        Me.btnSearchQuota.Text = "بـحـث"
        Me.btnSearchQuota.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSearchQuota.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label2.Location = New System.Drawing.Point(584, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 29)
        Me.Label2.TabIndex = 42
        Me.Label2.Text = "التاريـــخ"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpReportQuota
        '
        Me.dtpReportQuota.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dtpReportQuota.CalendarFont = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpReportQuota.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpReportQuota.Location = New System.Drawing.Point(379, 61)
        Me.dtpReportQuota.Name = "dtpReportQuota"
        Me.dtpReportQuota.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dtpReportQuota.RightToLeftLayout = True
        Me.dtpReportQuota.Size = New System.Drawing.Size(199, 26)
        Me.dtpReportQuota.TabIndex = 43
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label1.Location = New System.Drawing.Point(425, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(216, 29)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "تقرير الكوتة اليومية"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'crvQuota
        '
        Me.crvQuota.ActiveViewIndex = -1
        Me.crvQuota.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.crvQuota.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crvQuota.DisplayGroupTree = False
        Me.crvQuota.Location = New System.Drawing.Point(12, 95)
        Me.crvQuota.Name = "crvQuota"
        Me.crvQuota.SelectionFormula = ""
        Me.crvQuota.ShowGroupTreeButton = False
        Me.crvQuota.ShowRefreshButton = False
        Me.crvQuota.Size = New System.Drawing.Size(964, 476)
        Me.crvQuota.TabIndex = 45
        Me.crvQuota.ViewTimeSelectionFormula = ""
        '
        'CboLocation
        '
        Me.CboLocation.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboLocation.FormattingEnabled = True
        Me.CboLocation.Location = New System.Drawing.Point(10, 15)
        Me.CboLocation.Name = "CboLocation"
        Me.CboLocation.Size = New System.Drawing.Size(182, 25)
        Me.CboLocation.TabIndex = 76
        Me.CboLocation.Visible = False
        '
        'ChkLocation
        '
        Me.ChkLocation.BackColor = System.Drawing.Color.Transparent
        Me.ChkLocation.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkLocation.ForeColor = System.Drawing.Color.Black
        Me.ChkLocation.Location = New System.Drawing.Point(198, 14)
        Me.ChkLocation.Name = "ChkLocation"
        Me.ChkLocation.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ChkLocation.Size = New System.Drawing.Size(75, 28)
        Me.ChkLocation.TabIndex = 87
        Me.ChkLocation.Text = "الموقع"
        Me.ChkLocation.UseVisualStyleBackColor = False
        Me.ChkLocation.Visible = False
        '
        'frmrptQuota
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(988, 575)
        Me.Controls.Add(Me.ChkLocation)
        Me.Controls.Add(Me.CboLocation)
        Me.Controls.Add(Me.crvQuota)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtpReportQuota)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnSearchQuota)
        Me.Name = "frmrptQuota"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmrptQuota"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSearchQuota As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpReportQuota As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents crvQuota As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents CboLocation As System.Windows.Forms.ComboBox
    Friend WithEvents ChkLocation As System.Windows.Forms.CheckBox
End Class
