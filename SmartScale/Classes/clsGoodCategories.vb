Public Class clsGoodCategoriesO
    Inherits clsDatabaseObjectO
#Region "Declarations"

    Private mintParentID As Integer
    Private mintType As Integer
#End Region

#Region "Properties"

    Public Property ParentID() As Integer
        Get
            Return mintParentID
        End Get
        Set(ByVal Value As Integer)
            mintParentID = Value
        End Set
    End Property

    Public Property Type() As Integer
        Get
            Return mintType
        End Get
        Set(ByVal Value As Integer)
            mintType = Value
        End Set
    End Property

#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblGoodCategories"
        Me.TableIDFieldName = "CategoryID"
        Me.TableNameFieldName = "CategoryName"

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "CategoryName,ParentID,Type"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return "," & (ParentID) _
         & "," & (Type)

    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return ",ParentID" & (ParentID) _
         & ",Type" & (Type)

    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.ParentID = CType(rdrReader.Item("ParentID").ToString(), Integer)
        Me.Type = CType(rdrReader.Item("Type").ToString(), Integer)
    End Function
#End Region
End Class
Public Class clsGoodCategoriesS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblGoodCategories"
        Me.TableIDFieldName = "CategoryID"
        Me.TableNameFieldName = "CategoryName"

    End Sub
End Class
