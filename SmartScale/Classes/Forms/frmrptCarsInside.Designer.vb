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
        Me.SuspendLayout()
        '
        'crvCarsInside
        '
        Me.crvCarsInside.ActiveViewIndex = -1
        Me.crvCarsInside.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crvCarsInside.DisplayGroupTree = False
        Me.crvCarsInside.Dock = System.Windows.Forms.DockStyle.Fill
        Me.crvCarsInside.Location = New System.Drawing.Point(0, 0)
        Me.crvCarsInside.Name = "crvCarsInside"
        Me.crvCarsInside.SelectionFormula = ""
        Me.crvCarsInside.ShowGroupTreeButton = False
        Me.crvCarsInside.ShowRefreshButton = False
        Me.crvCarsInside.Size = New System.Drawing.Size(892, 666)
        Me.crvCarsInside.TabIndex = 0
        Me.crvCarsInside.ViewTimeSelectionFormula = ""
        '
        'frmrptCarsInside
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(892, 666)
        Me.Controls.Add(Me.crvCarsInside)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmrptCarsInside"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تقرير السيارات بالداخل"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents crvCarsInside As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
