Public Class clsIssueToO

    Inherits clsDatabaseObjectO

#Region "Declarations"
    Private mstrIssueTo_Telephone As String
    Private mstrIssueTo_Mobile As String
    Private mstrIssueTo_Fax As String
    Private mstrIssueTo_Addresss As String
    Private mstrIssueTo_Field As String
    Private m As String
    Private mintScaleID As Integer
#End Region

#Region "Properties"
    Public Property IssueTo_Telephone() As String
        Get
            Return mstrIssueTo_Telephone
        End Get
        Set(ByVal Value As String)
            mstrIssueTo_Telephone = Value
        End Set
    End Property
    Public Property IssueTo_Mobile() As String
        Get
            Return mstrIssueTo_Mobile
        End Get
        Set(ByVal Value As String)
            mstrIssueTo_Mobile = Value
        End Set
    End Property
    Public Property IssueTo_Fax() As String
        Get
            Return mstrIssueTo_Fax
        End Get
        Set(ByVal Value As String)
            mstrIssueTo_Fax = Value
        End Set
    End Property
    Public Property IssueTo_Address() As String
        Get
            Return mstrIssueTo_Addresss
        End Get
        Set(ByVal Value As String)
            mstrIssueTo_Addresss = Value
        End Set
    End Property
    Public Property IssueTo_Field() As String
        Get
            Return mstrIssueTo_Field
        End Get
        Set(ByVal Value As String)
            mstrIssueTo_Field = Value
        End Set
    End Property
    Public Property ScaleID() As Integer
        Get
            Return mintScaleID
        End Get
        Set(ByVal value As Integer)
            mintScaleID = value
        End Set
    End Property
#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblIssueTo"
        Me.TableIDFieldName = "Issue_To_ID"
        Me.TableNameFieldName = "Issue_To_Name"

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "Issue_To_Name,IssueTo_Telephone,IssueTo_Mobile,IssueTo_Fax,IssueTo_Address,IssueTo_Field,ScaleID"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return clsDBStrings.SingleQot(Name) _
        & "," & clsDBStrings.SingleQot(IssueTo_Telephone) _
        & "," & clsDBStrings.SingleQot(IssueTo_Mobile) _
        & "," & clsDBStrings.SingleQot(IssueTo_Fax) _
        & "," & clsDBStrings.SingleQot(IssueTo_Address) _
        & "," & clsDBStrings.SingleQot(IssueTo_Field) _
        & "," & (ScaleID)
    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "Issue_To_Name=" & clsDBStrings.SingleQot(Name) _
        & ",IssueTo_Telephone=" & clsDBStrings.SingleQot(IssueTo_Telephone) _
        & ",IssueTo_Mobile=" & clsDBStrings.SingleQot(IssueTo_Mobile) _
        & ",IssueTo_Fax=" & clsDBStrings.SingleQot(IssueTo_Fax) _
        & ",IssueTo_Address=" & clsDBStrings.SingleQot(IssueTo_Address) _
        & ",IssueTo_Field=" & clsDBStrings.SingleQot(IssueTo_Field) _
                & ",ScaleID=" & ScaleID
    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.Name = CStr(rdrReader.Item("Issue_To_Name").ToString())
        Me.IssueTo_Telephone = CType(rdrReader.Item("IssueTo_Telephone").ToString(), String)
        Me.IssueTo_Mobile = CType(rdrReader.Item("IssueTo_Mobile").ToString(), String)
        Me.IssueTo_Fax = CType(rdrReader.Item("IssueTo_Fax").ToString(), String)
        Me.IssueTo_Address = CType(rdrReader.Item("IssueTo_Address").ToString(), String)
        Me.IssueTo_Field = CType(rdrReader.Item("IssueTo_Field").ToString(), String)
        Me.ScaleID = CType(rdrReader.Item("ScaleID").ToString(), Integer)
    End Function


#End Region

End Class

Public Class clsIssueToS

    Inherits clsDatabaseObjectS

    Public Sub New()
        Me.TableName = "tblIssueTo"
        Me.TableIDFieldName = "Issue_To_ID"
        Me.TableNameFieldName = "Issue_To_Name"

    End Sub

End Class