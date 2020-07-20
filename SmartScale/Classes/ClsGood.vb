Public Class clsGoodO
    Inherits clsDatabaseObjectO
#Region "Declarations"
    Dim mstrID As String
    Dim mintType As Integer
    Dim mintProType As Integer
    Dim mintGoodType As Integer
#End Region

#Region "Properties"
    'Public Property ID() As String
    '    Get
    '        Return mstrID
    '    End Get
    '    Set(ByVal value As String)
    '        mstrID = value
    '    End Set
    'End Property

    Public Property Type() As Integer
        Get
            Return mintType
        End Get
        Set(ByVal value As Integer)
            mintType = value
        End Set
    End Property

    Public Property ProType() As Integer
        Get
            Return mintProType
        End Get
        Set(ByVal value As Integer)
            mintProType = value
        End Set
    End Property

    Public Property GoodType() As Integer
        Get
            Return mintGoodType
        End Get
        Set(ByVal value As Integer)
            mintGoodType = value
        End Set
    End Property

#End Region

#Region "Methods"
    Public Sub New()

        Me.TableName = "tblGood"
        Me.TableIDFieldName = "Good_ID"
        Me.TableNameFieldName = "Good_Name"

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "Good_ID,Good_Name,Type,ProtypeID,GoodtypeID"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return clsDBStrings.SingleQot(IDStr) _
         & "," & clsDBStrings.SingleQot(Name) _
         & "," & (Type) _
         & "," & (ProType) _
         & "," & (GoodType)

    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "Good_ID=" & clsDBStrings.SingleQot(IDStr) _
         & ",Good_Name=" & clsDBStrings.SingleQot(Name) _
         & ",Type=" & (Type) _
         & ",GoodTypeID=" & (GoodType) _
         & ",ProtypeID=" & (ProType)

    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.Name = CStr(rdrReader.Item("Good_Name").ToString())
        Me.IDStr = CType(rdrReader.Item("Good_ID").ToString(), String)
        Me.Type = CType(rdrReader.Item("Type").ToString(), Integer)
        Me.ProType = CType(rdrReader.Item("ProtypeID").ToString(), Integer)
        Me.GoodType = CType(rdrReader.Item("GoodtypeID").ToString(), Integer)
    End Function


#End Region

End Class
Public Class clsGoodS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblGood"
        Me.TableIDFieldName = "Good_ID"
        Me.TableNameFieldName = "Good_Name"

    End Sub
End Class