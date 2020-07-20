<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpDateQouta
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpDateQouta))
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtpDayDate = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.btnSearchQuota = New System.Windows.Forms.Button
        Me.TransID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NetWhight = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DNoteID = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.QoutaDate = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.Label3 = New System.Windows.Forms.Label
        Me.cboCustID = New System.Windows.Forms.ComboBox
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(382, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "تعديل الكوتات"
        '
        'dtpDayDate
        '
        Me.dtpDayDate.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDayDate.Location = New System.Drawing.Point(521, 40)
        Me.dtpDayDate.Name = "dtpDayDate"
        Me.dtpDayDate.Size = New System.Drawing.Size(164, 26)
        Me.dtpDayDate.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(691, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 24)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "التاريخ"
        '
        'DataGridView1
        '
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TransID, Me.NetWhight, Me.DNoteID, Me.QoutaDate})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.Location = New System.Drawing.Point(4, 74)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DataGridView1.Size = New System.Drawing.Size(857, 375)
        Me.DataGridView1.TabIndex = 3
        '
        'btnSearchQuota
        '
        Me.btnSearchQuota.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnSearchQuota.Image = CType(resources.GetObject("btnSearchQuota.Image"), System.Drawing.Image)
        Me.btnSearchQuota.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearchQuota.Location = New System.Drawing.Point(133, 38)
        Me.btnSearchQuota.Name = "btnSearchQuota"
        Me.btnSearchQuota.Size = New System.Drawing.Size(120, 30)
        Me.btnSearchQuota.TabIndex = 2
        Me.btnSearchQuota.Text = "بـحـث"
        Me.btnSearchQuota.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSearchQuota.UseVisualStyleBackColor = True
        '
        'TransID
        '
        Me.TransID.HeaderText = "رقم الكارته"
        Me.TransID.Name = "TransID"
        Me.TransID.ReadOnly = True
        '
        'NetWhight
        '
        Me.NetWhight.HeaderText = "صافى الوزنه"
        Me.NetWhight.Name = "NetWhight"
        Me.NetWhight.ReadOnly = True
        '
        'DNoteID
        '
        Me.DNoteID.HeaderText = "رقم المطالبه"
        Me.DNoteID.Name = "DNoteID"
        Me.DNoteID.ReadOnly = True
        '
        'QoutaDate
        '
        Me.QoutaDate.HeaderText = "تاريخ الكوته"
        Me.QoutaDate.Name = "QoutaDate"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(457, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 24)
        Me.Label3.TabIndex = 43
        Me.Label3.Text = "العميل"
        '
        'cboCustID
        '
        Me.cboCustID.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCustID.FormattingEnabled = True
        Me.cboCustID.Location = New System.Drawing.Point(273, 40)
        Me.cboCustID.Name = "cboCustID"
        Me.cboCustID.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cboCustID.Size = New System.Drawing.Size(178, 27)
        Me.cboCustID.TabIndex = 1
        '
        'frmUpDateQouta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(862, 451)
        Me.Controls.Add(Me.cboCustID)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnSearchQuota)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtpDayDate)
        Me.Controls.Add(Me.Label1)
        Me.KeyPreview = True
        Me.Name = "frmUpDateQouta"
        Me.Text = "frmUpDateQouta"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpDayDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents btnSearchQuota As System.Windows.Forms.Button
    Friend WithEvents TransID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NetWhight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DNoteID As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents QoutaDate As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboCustID As System.Windows.Forms.ComboBox
End Class
