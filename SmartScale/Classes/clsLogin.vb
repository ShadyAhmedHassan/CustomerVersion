Option Explicit On
Public Class clsLogin
    Private mdatLoginTime As Date = DateTime.MinValue

    Public Property LoginTime() As Date
        Get
            Return mdatLoginTime
        End Get
        Set(ByVal value As Date)
            mdatLoginTime = value
        End Set
    End Property

#Region "Methods"
    Public Function F_Login(ByVal UserNickName As String, ByVal Password As String) As String
        'Dim Scalar As String = gAdo.CmdExecScalar("Select User_password From TblUser Where [User_IsDeleted]=0 And [User_Id]=" & UserId, False, Nothing)
        'If Scalar.ToLower() = Password.ToLower() Then
        '    Return Scalar
        'Else
        '    Return ""
        'End If



        Dim Scalar As String = gAdo.CmdExecScalar("Select User_password From TblUser Where [User_IsDeleted]=0 And [User_NickName]='" & UserNickName & "'", False, Nothing)
        If Scalar = Nothing Then
            Return "Error Name"

        ElseIf Scalar.ToLower() = Password.ToLower() Then
            Return Scalar
        Else
            Return "Error Password"
        End If
    End Function

    Public Function ChangePassword(ByVal UserID As Integer, ByVal NewPassword As String) As Boolean
        Dim bool As Boolean = False
        If gAdo.CmdExec("update tblUser set User_Password ='" & NewPassword & "' where User_IsDeleted=0 And [User_Id]=" & UserID) <> 0 Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region
End Class
