<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmrptCarsInside
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmrptCarsInside))
        Me.crvCarsInside = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.cboDealer = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.BtnSearch = New System.Windows.Forms.Button
        Me.cboLocation = New System.Windows.Forms.ComboBox
        Me.ChkLocation = New System.Windows.Forms.CheckBox
        Me.chklstBoxScale = New System.Windows.Forms.CheckedListBox
        Me.SuspendLayout()
        '
        'crvCarsInside
        '
        Me.crvCarsInside.ActiveViewIndex = -1
        Me.crvCarsInside.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.crvCarsInside.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crvCarsInside.DisplayGroupTree = False
        Me.crvCarsInside.Location = New System.Drawing.Point(0, 137)
        Me.crvCarsInside.Name = "crvCarsInside"
        Me.crvCarsInside.SelectionFormula = ""
        Me.crvCarsInside.ShowGroupTreeButton = False
        Me.crvCarsInside.ShowRefreshButton = False
        Me.crvCarsInside.Size = New System.Drawing.Size(1272, 529)
        Me.crvCarsInside.TabIndex = 0
        Me.crvCarsInside.ViewTimeSelectionFormula = ""
        '
        'cboDealer
        '
        Me.cboDealer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboDealer.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.cboDealer.FormattingEnabled = True
        Me.cboDealer.Location = New System.Drawing.Point(895, 9)
        Me.cboDealer.Name = "cboDealer"
        Me.cboDealer.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cboDealer.Size = New System.Drawing.Size(284, 27)
        Me.cboDealer.TabIndex = 19
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label8.Location = New System.Drawing.Point(1185, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 27)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "العملاء"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnSearch
        '
        Me.BtnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.BtnSearch.Image = CType(resources.GetObject("BtnSearch.Image"), System.Drawing.Image)
        Me.BtnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnSearch.Location = New System.Drawing.Point(15, 102)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(120, 29)
        Me.BtnSearch.TabIndex = 21
        Me.BtnSearch.Text = "بـحـث"
        Me.BtnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnSearch.UseVisualStyleBackColor = True
        '
        'cboLocation
        '
        Me.cboLocation.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLocation.FormattingEnabled = True
        Me.cboLocation.Location = New System.Drawing.Point(15, 10)
        Me.cboLocation.Name = "cboLocation"
        Me.cboLocation.Size = New System.Drawing.Size(146, 25)
        Me.cboLocation.TabIndex = 85
        Me.cboLocation.Visible = False
        '
        'ChkLocation
        '
        Me.ChkLocation.BackColor = System.Drawing.Color.Transparent
        Me.ChkLocation.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkLocation.ForeColor = System.Drawing.Color.Black
        Me.ChkLocation.Location = New System.Drawing.Point(167, 10)
        Me.ChkLocation.Name = "ChkLocation"
        Me.ChkLocation.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ChkLocation.Size = New System.Drawing.Size(75, 28)
        Me.ChkLocation.TabIndex = 90
        Me.ChkLocation.Text = "الموقع"
        Me.ChkLocation.UseVisualStyleBackColor = False
        '
        'chklstBoxScale
        '
        Me.chklstBoxScale.BackColor = System.Drawing.Color.DarkGray
        Me.chklstBoxScale.CheckOnClick = True
        Me.chklstBoxScale.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chklstBoxScale.FormattingEnabled = True
        Me.chklstBoxScale.Location = New System.Drawing.Point(384, 9)
        Me.chklstBoxScale.Name = "chklstBoxScale"
        Me.chklstBoxScale.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chklstBoxScale.Size = New System.Drawing.Size(163, 109)
        Me.chklstBoxScale.TabIndex = 91
        '
        'frmrptCarsInside
        '
        Me.AccessibleRole = System.Windows.Forms.AccessibleRole.Clock
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1272, 666)
        Me.Controls.Add(Me.chklstBoxScale)
        Me.Controls.Add(Me.ChkLocation)
        Me.Controls.Add(Me.cboLocation)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.cboDealer)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.crvCarsInside)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmrptCarsInside"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تقرير السيارات بالداخل"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents crvCarsInside As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents cboDealer As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents BtnSearch As System.Windows.Forms.Button
    Friend WithEvents cboLocation As System.Windows.Forms.ComboBox
    Friend WithEvents ChkLocation As System.Windows.Forms.CheckBox
    Friend WithEvents chklstBoxScale As System.Windows.Forms.CheckedListBox
End Class
