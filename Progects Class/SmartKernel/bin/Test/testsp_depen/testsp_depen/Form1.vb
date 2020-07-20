Imports System.Data.SqlClient
Public Class Form1
    Dim con As New SqlConnection("Data Source=IT\SQLEXPRESS;Initial Catalog=bodytalk;User ID=amr;Password=123")
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim com As New SqlCommand("sp_depends tblcode", con)
        Dim dr As SqlDataReader
        Dim tt As New DataTable
        tt.Columns.Add("ss")
        tt.Columns.Add("dd")
        con.Open()
        dr = com.ExecuteReader
        Do While dr.Read
            Dim ar(1)
            dr.GetValues(ar)
            tt.Rows.Add(ar)
        Loop
        con.Close()
        Me.DataGridView1.DataSource = tt

    End Sub
End Class
