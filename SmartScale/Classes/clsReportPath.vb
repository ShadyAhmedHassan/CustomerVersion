Imports CrystalDecisions
Imports System.IO

Public Class clsReportPath

    Public Sub ReportPath(ByVal rpt As Object, ByVal RptViwer As Object, ByVal IntegratedSecurity As Boolean)
        Dim Str_ServerName, Str_DatabaseName, Str_UserID, Str_Password As String

        Dim ConstrData1 As String = "Data Source=" & Environment.MachineName.ToString & "\SQLEXPRESS;initial catalog=elbasset_v2;"
        Str_ServerName = (ConstrData1.Substring(ConstrData1.IndexOf("=") + 1, ConstrData1.IndexOf(";") - ConstrData1.IndexOf("=") - 1)).Trim
        ConstrData1 = ConstrData1.Substring(ConstrData1.IndexOf(";") + 1)
        Str_DatabaseName = (ConstrData1.Substring(ConstrData1.IndexOf("=") + 1, ConstrData1.IndexOf(";") - ConstrData1.IndexOf("=") - 1)).Trim
        ConstrData1 = ConstrData1.Substring(ConstrData1.IndexOf(";") + 1)
        'Str_UserID = (ConstrData1.Substring(ConstrData1.IndexOf("=") + 1, ConstrData1.IndexOf(";") - ConstrData1.IndexOf("=") - 1)).Trim
        ConstrData1 = ConstrData1.Substring(ConstrData1.IndexOf(";") + 1)
        'Str_Password = (ConstrData1.Substring(ConstrData1.IndexOf("=") + 1, ConstrData1.IndexOf(";") - ConstrData1.IndexOf("=") - 1)).Trim

        Dim tbCurrent As CrystalDecisions.CrystalReports.Engine.Table
        Dim tliCurrent As CrystalDecisions.Shared.TableLogOnInfo
        For Each tbCurrent In rpt.Database.Tables
            tliCurrent = tbCurrent.LogOnInfo
            With tliCurrent.ConnectionInfo
                .ServerName = Str_ServerName
                .DatabaseName = Str_DatabaseName
                .UserID = ""
                .Password = ""
                .IntegratedSecurity = True
            End With
            tbCurrent.ApplyLogOnInfo(tliCurrent)
        Next tbCurrent
        RptViwer.ReportSource = rpt
        RptViwer.Show()
    End Sub

    Public Sub ReportPath(ByVal rpt As Object, ByVal RptViwer As Object)

        Dim Str_ServerName, Str_DatabaseName, Str_UserID, Str_Password As String
        'Dim ConstrData1 As String = "Data Source=" & ServerName & "\SQLEXPRESS;initial catalog=" & DataBase & ";User ID=" & Str_UserID & ";Password=" & Str_Password & ""

        Dim ConstrData1 As String = GetDBConnection()
        Str_ServerName = (ConstrData1.Substring(ConstrData1.IndexOf("=") + 1, ConstrData1.IndexOf(";") - ConstrData1.IndexOf("=") - 1)).Trim
        ConstrData1 = ConstrData1.Substring(ConstrData1.IndexOf(";") + 1)
        Str_DatabaseName = (ConstrData1.Substring(ConstrData1.IndexOf("=") + 1, ConstrData1.IndexOf(";") - ConstrData1.IndexOf("=") - 1)).Trim
        ConstrData1 = ConstrData1.Substring(ConstrData1.IndexOf(";") + 1)
        Str_UserID = (ConstrData1.Substring(ConstrData1.IndexOf("=") + 1, ConstrData1.IndexOf(";") - ConstrData1.IndexOf("=") - 1)).Trim
        ConstrData1 = ConstrData1.Substring(ConstrData1.IndexOf(";") + 1)
        Str_Password = (ConstrData1.Substring(ConstrData1.IndexOf("=") + 1)).Trim ', ConstrData1.IndexOf(";") - ConstrData1.IndexOf("=") + 1)

        Dim tbCurrent As CrystalDecisions.CrystalReports.Engine.Table
        Dim tliCurrent As CrystalDecisions.Shared.TableLogOnInfo
        For Each tbCurrent In rpt.Database.Tables
            tliCurrent = tbCurrent.LogOnInfo
            With tliCurrent.ConnectionInfo
                .ServerName = Str_ServerName
                .DatabaseName = Str_DatabaseName
                .UserID = Str_UserID
                .Password = Str_Password
                .IntegratedSecurity = False
            End With
            tbCurrent.ApplyLogOnInfo(tliCurrent)
        Next tbCurrent



        'For Each tbCurrent In rpt.subreports.Database.Tables

        'Next

        RptViwer.ReportSource = rpt
        RptViwer.Show()
    End Sub

    Public Function GetDBConnection() As String

        Dim Conn As String
        Dim objReader As StreamReader
        Try
            objReader = New StreamReader(Application.StartupPath & "\ConnString.txt")
            Conn = objReader.ReadToEnd()
            objReader.Close()
            Return Conn
        Catch Ex As Exception
            'ErrInfo = Ex.Message
        End Try
    End Function

End Class
