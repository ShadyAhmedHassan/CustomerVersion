<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class cfrmDrivers
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
        Me.PnlDriver = New System.Windows.Forms.Panel
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.TxtDriverName = New System.Windows.Forms.TextBox
        Me.TxtDriverLicence = New System.Windows.Forms.TextBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.BtnDriverDelete = New System.Windows.Forms.Button
        Me.BtnDriverSave = New System.Windows.Forms.Button
        Me.BtnDriverAddNew = New System.Windows.Forms.Button
        Me.LstDrivers = New System.Windows.Forms.ListBox
        Me.TxtDriverSearch = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.PnlDriver.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'PnlDriver
        '
        Me.PnlDriver.Controls.Add(Me.Label27)
        Me.PnlDriver.Controls.Add(Me.Label28)
        Me.PnlDriver.Controls.Add(Me.TxtDriverName)
        Me.PnlDriver.Controls.Add(Me.TxtDriverLicence)
        Me.PnlDriver.Controls.Add(Me.Panel3)
        Me.PnlDriver.Controls.Add(Me.LstDrivers)
        Me.PnlDriver.Controls.Add(Me.TxtDriverSearch)
        Me.PnlDriver.Controls.Add(Me.Label4)
        Me.PnlDriver.Controls.Add(Me.Label7)
        Me.PnlDriver.Location = New System.Drawing.Point(12, 12)
        Me.PnlDriver.Name = "PnlDriver"
        Me.PnlDriver.Size = New System.Drawing.Size(827, 466)
        Me.PnlDriver.TabIndex = 24
        '
        'Label27
        '
        Me.Label27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label27.Font = New System.Drawing.Font("Simplified Arabic", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label27.Location = New System.Drawing.Point(249, 14)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(225, 47)
        Me.Label27.TabIndex = 8
        Me.Label27.Text = "بيـانـات الســائـق"
        '
        'Label28
        '
        Me.Label28.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label28.Font = New System.Drawing.Font("Simplified Arabic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label28.Location = New System.Drawing.Point(721, 45)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(62, 31)
        Me.Label28.TabIndex = 7
        Me.Label28.Text = "بـحـث"
        '
        'TxtDriverName
        '
        Me.TxtDriverName.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.TxtDriverName.Location = New System.Drawing.Point(214, 170)
        Me.TxtDriverName.Name = "TxtDriverName"
        Me.TxtDriverName.Size = New System.Drawing.Size(153, 26)
        Me.TxtDriverName.TabIndex = 0
        '
        'TxtDriverLicence
        '
        Me.TxtDriverLicence.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.TxtDriverLicence.Location = New System.Drawing.Point(214, 216)
        Me.TxtDriverLicence.Name = "TxtDriverLicence"
        Me.TxtDriverLicence.Size = New System.Drawing.Size(153, 26)
        Me.TxtDriverLicence.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.BtnDriverDelete)
        Me.Panel3.Controls.Add(Me.BtnDriverSave)
        Me.Panel3.Controls.Add(Me.BtnDriverAddNew)
        Me.Panel3.Location = New System.Drawing.Point(90, 415)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(461, 43)
        Me.Panel3.TabIndex = 2
        '
        'BtnDriverDelete
        '
        Me.BtnDriverDelete.Font = New System.Drawing.Font("Simplified Arabic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BtnDriverDelete.Image = Global.SmartScale.My.Resources.Resources.BtnDelete_Image
        Me.BtnDriverDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnDriverDelete.Location = New System.Drawing.Point(3, 3)
        Me.BtnDriverDelete.Name = "BtnDriverDelete"
        Me.BtnDriverDelete.Size = New System.Drawing.Size(115, 37)
        Me.BtnDriverDelete.TabIndex = 2
        Me.BtnDriverDelete.Text = "حذف"
        Me.BtnDriverDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnDriverDelete.UseVisualStyleBackColor = True
        '
        'BtnDriverSave
        '
        Me.BtnDriverSave.Font = New System.Drawing.Font("Simplified Arabic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BtnDriverSave.Image = Global.SmartScale.My.Resources.Resources.BtnSave_Image
        Me.BtnDriverSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnDriverSave.Location = New System.Drawing.Point(173, 3)
        Me.BtnDriverSave.Name = "BtnDriverSave"
        Me.BtnDriverSave.Size = New System.Drawing.Size(115, 37)
        Me.BtnDriverSave.TabIndex = 0
        Me.BtnDriverSave.Tag = "Driver"
        Me.BtnDriverSave.Text = "حفظ"
        Me.BtnDriverSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnDriverSave.UseVisualStyleBackColor = True
        '
        'BtnDriverAddNew
        '
        Me.BtnDriverAddNew.Font = New System.Drawing.Font("Simplified Arabic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BtnDriverAddNew.Image = Global.SmartScale.My.Resources.Resources.BtnNew_Image
        Me.BtnDriverAddNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnDriverAddNew.Location = New System.Drawing.Point(343, 3)
        Me.BtnDriverAddNew.Name = "BtnDriverAddNew"
        Me.BtnDriverAddNew.Size = New System.Drawing.Size(115, 37)
        Me.BtnDriverAddNew.TabIndex = 1
        Me.BtnDriverAddNew.Text = "أضافه"
        Me.BtnDriverAddNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnDriverAddNew.UseVisualStyleBackColor = True
        '
        'LstDrivers
        '
        Me.LstDrivers.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LstDrivers.FormattingEnabled = True
        Me.LstDrivers.ItemHeight = 19
        Me.LstDrivers.Location = New System.Drawing.Point(577, 117)
        Me.LstDrivers.Name = "LstDrivers"
        Me.LstDrivers.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.LstDrivers.Size = New System.Drawing.Size(206, 289)
        Me.LstDrivers.TabIndex = 4
        '
        'TxtDriverSearch
        '
        Me.TxtDriverSearch.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.TxtDriverSearch.Location = New System.Drawing.Point(577, 80)
        Me.TxtDriverSearch.Name = "TxtDriverSearch"
        Me.TxtDriverSearch.Size = New System.Drawing.Size(206, 26)
        Me.TxtDriverSearch.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label4.Font = New System.Drawing.Font("Simplified Arabic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(392, 168)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(121, 28)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "أسم السائق"
        '
        'Label7
        '
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label7.Font = New System.Drawing.Font("Simplified Arabic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(392, 214)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(121, 28)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "رقم الرخصه"
        '
        'cfrmDrivers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(852, 482)
        Me.Controls.Add(Me.PnlDriver)
        Me.Name = "cfrmDrivers"
        Me.ShowIcon = False
        Me.PnlDriver.ResumeLayout(False)
        Me.PnlDriver.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PnlDriver As System.Windows.Forms.Panel
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents TxtDriverName As System.Windows.Forms.TextBox
    Friend WithEvents TxtDriverLicence As System.Windows.Forms.TextBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents BtnDriverDelete As System.Windows.Forms.Button
    Friend WithEvents BtnDriverSave As System.Windows.Forms.Button
    Friend WithEvents BtnDriverAddNew As System.Windows.Forms.Button
    Friend WithEvents LstDrivers As System.Windows.Forms.ListBox
    Friend WithEvents TxtDriverSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class
