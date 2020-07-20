Public Class clsUserO
    Inherits clsDatabaseObjectO
#Region "Declarations"
    Private mstrUser_Password As String
    Private mintUser_Pic_ID As clsPictureO
    Private mblnUser_IsDeleted As Boolean
    Private mstrUser_NickName As String
    Private mblnCanUpdate As Boolean
    Private mstrUserType As String
#End Region

#Region "Properties"
    Public Property User_NickName() As String
        Get
            Return mstrUser_NickName
        End Get
        Set(ByVal value As String)
            mstrUser_NickName = value
        End Set
    End Property

    Public Property User_Password() As String
        Get
            Return mstrUser_Password
        End Get
        Set(ByVal Value As String)
            mstrUser_Password = Value
        End Set
    End Property
    Public Property User_Pic_ID() As clsPictureO
        Get
            Return mintUser_Pic_ID
        End Get
        Set(ByVal Value As clsPictureO)
            mintUser_Pic_ID = Value
        End Set
    End Property
    Public Property User_IsDeleted() As Boolean
        Get
            Return mblnUser_IsDeleted
        End Get
        Set(ByVal Value As Boolean)
            mblnUser_IsDeleted = Value
        End Set
    End Property

    Public Property CanUpdate() As Boolean
        Get
            Return mblnCanUpdate
        End Get
        Set(ByVal Value As Boolean)
            mblnCanUpdate = Value
        End Set
    End Property


    Public Property UserType() As String
        Get
            Return mstrUserType
        End Get
        Set(ByVal Value As String)
            mstrUserType = Value
        End Set
    End Property

#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblUser"
        Me.TableIDFieldName = "User_ID"
        Me.TableNameFieldName = "User_Name"
        mintUser_Pic_ID = New clsPictureO
    End Sub
    Protected Overrides Sub Finalize()
        mintUser_Pic_ID = Nothing
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        If User_Pic_ID.ID = 0 Then
            Return "User_Name,User_NickName,User_Password,User_IsDeleted,CanUpdate,User_Type"
        Else
            Return "User_Name,User_NickName,User_Password,User_Pic_ID,User_IsDeleted,CanUpdate,User_Type"
        End If
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        If User_Pic_ID.ID = 0 Then
            Return clsDBStrings.SingleQot(Name) _
                   & "," & clsDBStrings.SingleQot(User_NickName) _
                   & "," & clsDBStrings.SingleQot(User_Password) _
                   & "," & clsDBStrings.ToDBBoolean(User_IsDeleted) _
                     & "," & clsDBStrings.ToDBBoolean(CanUpdate) _
                        & "," & clsDBStrings.SingleQot(UserType)
        Else
            Return clsDBStrings.SingleQot(Name) _
                   & "," & clsDBStrings.SingleQot(User_NickName) _
                   & "," & clsDBStrings.SingleQot(User_Password) _
                   & "," & (User_Pic_ID.ID) _
                   & "," & clsDBStrings.ToDBBoolean(User_IsDeleted) _
                   & "," & clsDBStrings.ToDBBoolean(CanUpdate) _
                         & "," & clsDBStrings.SingleQot(UserType)
        End If

    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        If User_Pic_ID.ID = 0 Then
            Return "User_Name=" & clsDBStrings.SingleQot(Name) _
                          & ",User_NickName=" & clsDBStrings.SingleQot(User_NickName) _
                          & ",User_Password=" & clsDBStrings.SingleQot(User_Password) _
                          & ",User_Pic_ID=Null" _
                          & ",User_IsDeleted=" & clsDBStrings.ToDBBoolean(User_IsDeleted) _
             & ",CanUpdate=" & clsDBStrings.ToDBBoolean(CanUpdate) _
              & ",User_Type=" & clsDBStrings.SingleQot(UserType)
        Else
            Return "User_Name=" & clsDBStrings.SingleQot(Name) _
               & ",User_NickName=" & clsDBStrings.SingleQot(User_NickName) _
               & ",User_Password=" & clsDBStrings.SingleQot(User_Password) _
               & ",User_Pic_ID=" & (User_Pic_ID.ID) _
               & ",User_IsDeleted=" & clsDBStrings.ToDBBoolean(User_IsDeleted) _
                 & ",CanUpdate=" & clsDBStrings.ToDBBoolean(CanUpdate) _
                 & ",User_Type=" & clsDBStrings.SingleQot(UserType)

        End If


    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.Name = CStr(rdrReader.Item("User_Name").ToString())
        Me.User_NickName = CType(rdrReader.Item("User_NickName").ToString(), String)
        Me.User_Password = CType(rdrReader.Item("User_Password").ToString(), String)
        If rdrReader.Item("User_Pic_ID").ToString = Nothing Then
            User_Pic_ID.ID = 0
        Else
            Me.User_Pic_ID.ID = CType(rdrReader.Item("User_Pic_ID").ToString(), Integer)
        End If
        '    Me.User_IsDeleted = CType(rdrReader.Item("User_IsDeleted").ToString(), Boolean)
        Me.CanUpdate = CType(rdrReader.Item("CanUpdate").ToString, Boolean)
        Me.UserType = CType(rdrReader.Item("User_Type").ToString(), String)
    End Function


#End Region

End Class
Public Class clsUserS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblUser"
        Me.TableIDFieldName = "User_ID"
        Me.TableNameFieldName = "User_Name"

    End Sub
End Class