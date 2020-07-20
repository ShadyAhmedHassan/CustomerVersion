Imports System.Data.DataTable

'''' 27/12/2018 ''''


Public Class frmrptCarInsideSearch

    Public Shared SearchOrCancel As Integer = 0

    Private Sub frmrptCarInsideSearch_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.Click
        gTransID = 0
    End Sub


    Private Sub frmrptCarInsideSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        RefillCarInside()
        gAdo.CtrlItemsLoad("LocationID", "LocationName", "tblLocation ORDER BY LocationID", cboLocation, "", "")

    End Sub
    Sub RefillCarInside()
        Try
            Dim Statement As String = " set dateformat dmy SELECT convert(nvarchar(max),CID) as 'رقم العملية',D.Dealer_Name as 'العميل' " & _
                                      " ,G.Good_Name as 'البضاعة',IT.Issue_To_Name as 'المرسل اليه',C.CarBoard_No as 'رقم السيارة' " & _
                                      " ,CI.TruckBoard_No as 'المقطورة',T.First_Weigth as 'الوزنة الاولى' " & _
                                      " ,T.Second_Weight as 'الوزنة الثانية',Out_Date as 'تاريخ الخروج' " & _
                                      " ,(T.First_Weigth - T.Second_Weight) as 'الصافي' FROM tblTransactions as T join tblGood as G on T.Good_ID =G.Good_ID " & _
                                      " join tblCarInfo as CI on CI.Car_Info_ID=T.Car_Info_ID  join tblCar as C on C.Car_ID =T.Car_ID " & _
                                      " join tblDealer as D on D.Dealer_ID=T.Dealer_ID join tblIssueTo as IT on IT.Issue_To_ID=T.Issue_To_ID " & _
                                      " where  TR_TY =2 and out_date is null  " & (If(cboLocation.SelectedValue <> Nothing And ChkLocation.Checked, " and T.LocationID=" & cboLocation.SelectedValue, "")) & _
                                      " Union  SELECT 'إذن مرتجع رقم : ' + convert(nvarchar(max),Trans_ID) as CID " & _
                                      " ,D.Dealer_Name,G.Good_Name,IT.Issue_To_Name,C.CarBoard_No,CI.TruckBoard_No " & _
                                      " ,T.First_Weigth,T.Second_Weight,Out_Date ,(T.First_Weigth - T.Second_Weight) as Total " & _
                                      " FROM tblReturns as T join tblGood as G on T.Good_ID =G.Good_ID " & _
                                      " join tblCarInfo as CI on CI.Car_Info_ID=T.Car_Info_ID  join tblCar as C on C.Car_ID =T.Car_ID " & _
                                      " join tblDealer as D on D.Dealer_ID=T.Dealer_ID join tblIssueTo as IT on IT.Issue_To_ID=T.Issue_To_ID " & _
                                      " and TR_TY =2 and out_date is null " & (If(cboLocation.SelectedValue <> Nothing And ChkLocation.Checked, " and T.LocationID=" & cboLocation.SelectedValue, "")) & " order by [رقم العملية] "

            GObjDataBaseS.FillGrid(DataGridView1, Statement)


            '''' منع الترتيب من الجرده ولكن الترتيب من قاعده البيانات ''''
            For i = 0 To DataGridView1.Columns.Count - 1

                DataGridView1.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable

            Next


            '''' (هنا ترقيم العمود الذي اسمه مسلسل (م ''''
            For y = 0 To DataGridView1.Rows.Count - 1
                DataGridView1(0, y).Value = y + 1
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick

        '' Stop Code by Elsaman 31/12/2019 

        ''''' Elsaman ''''
        ''''' Update in code in 24/4/2019 ''''
        'Try

        '    gTransID = CInt(DataGridView1.CurrentRow.Cells(1).Value)


        '    'Dim frm As New cfrmMain(x)

        '    ' frm.txtTransID.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString()
        '    'CID = frm.txtTransID.Text

        '    Me.Hide()
        '    'frm.ShowDialog()
        '    Me.Dispose()
        'Catch ex As Exception

        'End Try

        


    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        '''' عمل تظليل الصف بالكامل ''''
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        '''' اضافة عمود م مسلسل للداتا جريد و ترقيمه ''''
        '''' هذا الكود ''''
        'Dim col As New DataGridViewTextBoxColumn()
        'With col
        '    .Name = "مسلسل"
        '    .HeaderText = "م"
        '    .Width = 50
        'End With

        '''' او هذا الكود المختصر ''''
        DataGridView1.Columns.Insert(0, New DataGridViewTextBoxColumn() With {.Name = "مسلسل", .HeaderText = "م", .Width = 50})


    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim frm As New frmrptCarsInside
        frm.ShowDialog()
    End Sub

    Private Sub cboLocation_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboLocation.Leave
        RefillCarInside()
    End Sub

    Private Sub cboLocation_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboLocation.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            DataGridView1.Focus()
        End If
    End Sub

    Private Sub ChkLocation_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkLocation.CheckedChanged
        If (ChkLocation.Checked) Then
            cboLocation.Visible = True
        Else : cboLocation.Visible = False
        End If
    End Sub

    Private Sub ChkLocation_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkLocation.Leave

    End Sub

    Private Sub ChkLocation_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ChkLocation.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            RefillCarInside()
        End If
    End Sub
End Class