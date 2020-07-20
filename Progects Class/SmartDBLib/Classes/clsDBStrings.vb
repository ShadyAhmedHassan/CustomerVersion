﻿Public Class clsDBStrings

    Public Shared Function SingleQot(ByVal strString As String) As String
        'On Error Resume Next
        strString = strString.Replace(vbCrLf, "'+chr(13)+chr(10)+'")
        strString = strString.Replace(vbCr, "'+chr(13)+'")
        strString = strString.Replace(vbLf, "'+chr(10)+'")
        strString = strString.Replace("'", "''")

        SingleQot = "N'" + strString + "'"
    End Function

    Public Shared Function ToDBDate(ByVal datDate As Date) As String
        'On Error Resume Next
        Return SingleQot(datDate.ToString("yyyy/MM/dd"))
    End Function
    Public Shared Function ToDBBoolean(ByVal blnBooleean As Boolean) As String
        'On Error Resume Next
        Return IIf(blnBooleean = True, "1", "0")
    End Function

    Public Shared Function BuildSQLConnectionString(ByVal strDatabase As String, Optional ByVal strServerName As String = "(local)", Optional ByVal strLoginUserName As String = "sa", Optional ByVal strUserPassword As String = "sa") As String
        Return "Data Source=" & strServerName & ";Initial Catalog=" & strDatabase & ";User ID=" & strLoginUserName & ";Password=" & strUserPassword
    End Function
End Class
