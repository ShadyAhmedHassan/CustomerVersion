<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmrptCustQnty
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmrptCustQnty))
        Me.dtTo = New System.Windows.Forms.DateTimePicker
        Me.dtFrom = New System.Windows.Forms.DateTimePicker
        Me.crvRptCustQnty = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.BtnSearch = New System.Windows.Forms.Button
        Me.Label21 = New System.Windows.Forms.Label
        Me.cboDealers = New System.Windows.Forms.ComboBox
        Me.cboGoods = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblTo = New System.Windows.Forms.Label
        Me.chkDealer = New System.Windows.Forms.CheckBox
        Me.chkGood = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'dtTo
        '
        Me.dtTo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dtTo.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtTo.Location = New System.Drawing.Point(274, 77)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dtTo.RightToLeftLayout = True
        Me.dtTo.Size = New System.Drawing.Size(171, 29)
        Me.dtTo.TabIndex = 0
        '
        'dtFrom
        '
        Me.dtFrom.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dtFrom.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtFrom.Location = New System.Drawing.Point(561, 77)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dtFrom.RightToLeftLayout = True
        Me.dtFrom.Size = New System.Drawing.Size(171, 29)
        Me.dtFrom.TabIndex = 1
        '
        'crvRptCustQnty
        '
        Me.crvRptCustQnty.ActiveViewIndex = -1
        Me.crvRptCustQnty.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.crvRptCustQnty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crvRptCustQnty.DisplayGroupTree = False
        Me.crvRptCustQnty.Location = New System.Drawing.Point(-1, 198)
        Me.crvRptCustQnty.Name = "crvRptCustQnty"
        Me.crvRptCustQnty.SelectionFormula = ""
        Me.crvRptCustQnty.ShowGroupTreeButton = False
        Me.crvRptCustQnty.ShowRefreshButton = False
        Me.crvRptCustQnty.Size = New System.Drawing.Size(896, 321)
        Me.crvRptCustQnty.TabIndex = 2
        Me.crvRptCustQnty.ViewTimeSelectionFormula = ""
        '
        'BtnSearch
        '
        Me.BtnSearch.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.BtnSearch.Font = New System.Drawing.Font("Simplified Arabic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.BtnSearch.Image = CType(resources.GetObject("BtnSearch.Image"), System.Drawing.Image)
        Me.BtnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnSearch.Location = New System.Drawing.Point(12, 157)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(120, 35)
        Me.BtnSearch.TabIndex = 12
        Me.BtnSearch.Text = "بـحـث"
        Me.BtnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnSearch.UseVisualStyleBackColor = True
        '
        'Label21
        '
        Me.Label21.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label21.Font = New System.Drawing.Font("Simplified Arabic", 20.0!, System.Drawing.FontStyle.Bold)
        Me.Label21.Location = New System.Drawing.Point(304, 9)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(285, 45)
        Me.Label21.TabIndex = 16
        Me.Label21.Text = "تقرير الكميات المتعاقد عليها"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboDealers
        '
        Me.cboDealers.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cboDealers.Enabled = False
        Me.cboDealers.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDealers.FormattingEnabled = True
        Me.cboDealers.Location = New System.Drawing.Point(561, 130)
        Me.cboDealers.Name = "cboDealers"
        Me.cboDealers.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cboDealers.Size = New System.Drawing.Size(171, 30)
        Me.cboDealers.TabIndex = 17
        '
        'cboGoods
        '
        Me.cboGoods.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cboGoods.Enabled = False
        Me.cboGoods.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGoods.FormattingEnabled = True
        Me.cboGoods.Location = New System.Drawing.Point(33, 24)
        Me.cboGoods.Name = "cboGoods"
        Me.cboGoods.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cboGoods.Size = New System.Drawing.Size(171, 30)
        Me.cboGoods.TabIndex = 17
        Me.cboGoods.Visible = False
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(738, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 22)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "من "
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(738, 133)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 22)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "العميل"
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(210, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 22)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "الصنف"
        Me.Label3.Visible = False
        '
        'lblTo
        '
        Me.lblTo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblTo.AutoSize = True
        Me.lblTo.BackColor = System.Drawing.Color.Transparent
        Me.lblTo.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.ForeColor = System.Drawing.Color.White
        Me.lblTo.Location = New System.Drawing.Point(451, 80)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(29, 22)
        Me.lblTo.TabIndex = 18
        Me.lblTo.Text = "إلى"
        '
        'chkDealer
        '
        Me.chkDealer.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkDealer.AutoSize = True
        Me.chkDealer.BackColor = System.Drawing.Color.Transparent
        Me.chkDealer.ForeColor = System.Drawing.Color.White
        Me.chkDealer.Location = New System.Drawing.Point(791, 138)
        Me.chkDealer.Name = "chkDealer"
        Me.chkDealer.Size = New System.Drawing.Size(15, 14)
        Me.chkDealer.TabIndex = 19
        Me.chkDealer.UseVisualStyleBackColor = False
        '
        'chkGood
        '
        Me.chkGood.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkGood.AutoSize = True
        Me.chkGood.BackColor = System.Drawing.Color.Transparent
        Me.chkGood.ForeColor = System.Drawing.Color.White
        Me.chkGood.Location = New System.Drawing.Point(268, 33)
        Me.chkGood.Name = "chkGood"
        Me.chkGood.Size = New System.Drawing.Size(15, 14)
        Me.chkGood.TabIndex = 20
        Me.chkGood.UseVisualStyleBackColor = False
        Me.chkGood.Visible = False
        '
        'frmrptCustQnty
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.BackgroundImage = Global.SmartScale.My.Resources.Resources.blue
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(892, 516)
        Me.Controls.Add(Me.chkGood)
        Me.Controls.Add(Me.chkDealer)
        Me.Controls.Add(Me.lblTo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboGoods)
        Me.Controls.Add(Me.cboDealers)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.crvRptCustQnty)
        Me.Controls.Add(Me.dtFrom)
        Me.Controls.Add(Me.dtTo)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmrptCustQnty"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents crvRptCustQnty As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents BtnSearch As System.Windows.Forms.Button
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cboDealers As System.Windows.Forms.ComboBox
    Friend WithEvents cboGoods As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents chkDealer As System.Windows.Forms.CheckBox
    Friend WithEvents chkGood As System.Windows.Forms.CheckBox
End Class
