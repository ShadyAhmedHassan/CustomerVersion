Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim x As New Kernel.clsLogFile("Test.log")
        On Error Resume Next
        'Dim d As Integer = 0
        'For i As Integer = 1 To 50
        '    d = Integer.MaxValue
        '    d += 1
        '    Kernel.clsLogFile.OnErrorWriteToLog(x, TypeName(Me), "Form1_Load", Err)
        '    Call x.WriteLine("تجربة #" & i.ToString(), True)
        'Next



        'Dim objDLL As New DBLib.clsDLL
        'objDLL.LogFile = New Kernel.clsLogFile("Test.log")
        'objDLL = Nothing
        Dim myarry As New ArrayList
        Dim c As New DBLib.clsDBConnection("Data Source=sultan\SQLEXPRESS;Initial Catalog=bodytalk;User ID=amr;Password=123")
        Dim s As New DBLib.clsDatabaseObjectS
        If c.Open = True Then

        Else
            Exit Sub
        End If
        Dim ss As Integer = c.ExecuteScaler("select count(*) from products000")



        Call x.Close()
        x = Nothing

     

        Dim a As New ArrayList
        a = s.ExecuteReaderToArrayWithOutArray("select * from products where productid < 5", "productid")


        's.ExecuteReaderToArray("select * from products", , myarry, "productid")

        c.Open()
        c.Transaction.Begin()
        If c.ExecuteNonQuery("Insert into tblTest5555  (ID) VALUES (1)") Then
            MsgBox(c.Transaction.AffectedRecords)
            If c.ExecuteNonQuery("Insert into tblTest5555  (ID) VALUES (2)") Then
                MsgBox(c.Transaction.AffectedRecords)
                If c.ExecuteNonQuery("Insert into tblTest5555  (ID) VALUES ('555dgdf')") Then
                    MsgBox(c.ExecuteScaler("select max(id) from tblTest5555"))
                    MsgBox(c.Transaction.AffectedRecords)
                    MsgBox(c.Transaction.Active)
                    c.Transaction.Commit()
                    MsgBox(c.ExecuteScaler("select max(id) from tblTest5555"))
                    MsgBox(c.Transaction.Active)
                Else
                    MsgBox(c.Transaction.Active)
                    c.Transaction.Rollback()
                    MsgBox(c.Transaction.Active)
                    MsgBox(c.ExecuteScaler("select max(id) from tblTest5555"))
                End If
            End If
        End If

        c.Close()



    End Sub
End Class
