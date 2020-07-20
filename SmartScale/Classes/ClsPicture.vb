Public Class clsPictureO
    Inherits clsDatabaseObjectO
#Region "Declarations"
    Private mobjPicture As Object

#End Region

#Region "Properties"
    Public Property Picture() As Object
        Get
            Return mobjPicture
        End Get
        Set(ByVal Value As Object)
            mobjPicture = Value
        End Set
    End Property

#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblPicture"
        Me.TableIDFieldName = "Pic_ID"
        Me.TableNameFieldName = ""
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "Picture"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return (Picture)

    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "Picture=" & (Picture)

    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.Picture = CType(rdrReader.Item("Picture").ToString(), Object)

    End Function


#End Region

End Class
Public Class clsPictureS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblPicture"
        Me.TableIDFieldName = "Pic_ID"
        Me.TableNameFieldName = ""
    End Sub
End Class