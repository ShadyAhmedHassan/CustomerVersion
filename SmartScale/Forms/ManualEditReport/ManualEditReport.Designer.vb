<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ManualEditReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ManualEditReport))
        Me.Label3 = New System.Windows.Forms.Label
        Me.crvManEditReports = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.BtnSearch = New System.Windows.Forms.Button
        Me.lblAllTo = New System.Windows.Forms.Label
        Me.lblAllFrom = New System.Windows.Forms.Label
        Me.dtpAllTo = New System.Windows.Forms.DateTimePicker
        Me.dtpAllFrom = New System.Windows.Forms.DateTimePicker
        Me.chklstBoxScale = New System.Windows.Forms.CheckedListBox
        Me.cboLocation = New System.Windows.Forms.ComboBox
        Me.ChkLocation = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label3.Location = New System.Drawing.Point(1086, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(281, 36)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "تقرير بالحركات المعدله يدويا"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'crvManEditReports
        '
        Me.crvManEditReports.ActiveViewIndex = -1
        Me.crvManEditReports.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.crvManEditReports.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crvManEditReports.DisplayGroupTree = False
        Me.crvManEditReports.Location = New System.Drawing.Point(-2, 140)
        Me.crvManEditReports.Name = "crvManEditReports"
        Me.crvManEditReports.SelectionFormula = ""
        Me.crvManEditReports.ShowGotoPageButton = False
        Me.crvManEditReports.ShowGroupTreeButton = False
        Me.crvManEditReports.ShowRefreshButton = False
        Me.crvManEditReports.Size = New System.Drawing.Size(1383, 424)
        Me.crvManEditReports.TabIndex = 15
        Me.crvManEditReports.ViewTimeSelectionFormula = ""
        '
        'BtnSearch
        '
        Me.BtnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.BtnSearch.Image = CType(resources.GetObject("BtnSearch.Image"), System.Drawing.Image)
        Me.BtnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnSearch.Location = New System.Drawing.Point(12, 99)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(120, 35)
        Me.BtnSearch.TabIndex = 16
        Me.BtnSearch.Text = "بـحـث"
        Me.BtnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnSearch.UseVisualStyleBackColor = True
        '
        'lblAllTo
        '
        Me.lblAllTo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblAllTo.AutoSize = True
        Me.lblAllTo.BackColor = System.Drawing.Color.Transparent
        Me.lblAllTo.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAllTo.ForeColor = System.Drawing.Color.White
        Me.lblAllTo.Location = New System.Drawing.Point(668, 84)
        Me.lblAllTo.Name = "lblAllTo"
        Me.lblAllTo.Size = New System.Drawing.Size(29, 22)
        Me.lblAllTo.TabIndex = 33
        Me.lblAllTo.Text = "الى"
        '
        'lblAllFrom
        '
        Me.lblAllFrom.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblAllFrom.AutoSize = True
        Me.lblAllFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblAllFrom.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAllFrom.ForeColor = System.Drawing.Color.White
        Me.lblAllFrom.Location = New System.Drawing.Point(899, 81)
        Me.lblAllFrom.Name = "lblAllFrom"
        Me.lblAllFrom.Size = New System.Drawing.Size(28, 22)
        Me.lblAllFrom.TabIndex = 34
        Me.lblAllFrom.Text = "من"
        '
        'dtpAllTo
        '
        Me.dtpAllTo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dtpAllTo.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpAllTo.Location = New System.Drawing.Point(495, 79)
        Me.dtpAllTo.Name = "dtpAllTo"
        Me.dtpAllTo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dtpAllTo.RightToLeftLayout = True
        Me.dtpAllTo.Size = New System.Drawing.Size(167, 29)
        Me.dtpAllTo.TabIndex = 31
        '
        'dtpAllFrom
        '
        Me.dtpAllFrom.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dtpAllFrom.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpAllFrom.Location = New System.Drawing.Point(723, 77)
        Me.dtpAllFrom.Name = "dtpAllFrom"
        Me.dtpAllFrom.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dtpAllFrom.RightToLeftLayout = True
        Me.dtpAllFrom.Size = New System.Drawing.Size(170, 29)
        Me.dtpAllFrom.TabIndex = 32
        '
        'chklstBoxScale
        '
        Me.chklstBoxScale.BackColor = System.Drawing.Color.DarkGray
        Me.chklstBoxScale.CheckOnClick = True
        Me.chklstBoxScale.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chklstBoxScale.FormattingEnabled = True
        Me.chklstBoxScale.Location = New System.Drawing.Point(315, 9)
        Me.chklstBoxScale.Name = "chklstBoxScale"
        Me.chklstBoxScale.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chklstBoxScale.Size = New System.Drawing.Size(163, 109)
        Me.chklstBoxScale.TabIndex = 75
        '
        'cboLocation
        '
        Me.cboLocation.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLocation.FormattingEnabled = True
        Me.cboLocation.Location = New System.Drawing.Point(7, 7)
        Me.cboLocation.Name = "cboLocation"
        Me.cboLocation.Size = New System.Drawing.Size(172, 25)
        Me.cboLocation.TabIndex = 87
        Me.cboLocation.Visible = False
        '
        'ChkLocation
        '
        Me.ChkLocation.BackColor = System.Drawing.Color.Transparent
        Me.ChkLocation.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkLocation.ForeColor = System.Drawing.Color.White
        Me.ChkLocation.Location = New System.Drawing.Point(185, 6)
        Me.ChkLocation.Name = "ChkLocation"
        Me.ChkLocation.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ChkLocation.Size = New System.Drawing.Size(75, 28)
        Me.ChkLocation.TabIndex = 88
        Me.ChkLocation.Text = "الموقع"
        Me.ChkLocation.UseVisualStyleBackColor = False
        '
        'ManualEditReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.BackgroundImage = Global.SmartScale.My.Resources.Resources.blue
        Me.ClientSize = New System.Drawing.Size(1379, 564)
        Me.Controls.Add(Me.ChkLocation)
        Me.Controls.Add(Me.cboLocation)
        Me.Controls.Add(Me.chklstBoxScale)
        Me.Controls.Add(Me.lblAllTo)
        Me.Controls.Add(Me.lblAllFrom)
        Me.Controls.Add(Me.dtpAllTo)
        Me.Controls.Add(Me.dtpAllFrom)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.crvManEditReports)
        Me.Controls.Add(Me.Label3)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ManualEditReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents crvManEditReports As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents BtnSearch As System.Windows.Forms.Button
    Friend WithEvents lblAllTo As System.Windows.Forms.Label
    Friend WithEvents lblAllFrom As System.Windows.Forms.Label
    Friend WithEvents dtpAllTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpAllFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents chklstBoxScale As System.Windows.Forms.CheckedListBox
    Friend WithEvents cboLocation As System.Windows.Forms.ComboBox
    Friend WithEvents ChkLocation As System.Windows.Forms.CheckBox
End Class
