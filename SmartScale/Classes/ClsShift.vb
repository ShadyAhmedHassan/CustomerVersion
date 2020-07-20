Public Class clsShiftO
    Inherits clsDatabaseObjectO
#Region "Declarations"

    Private mdatShift_Start_Time As String
    Private mdatShift_End_Time As String
    Private mblnShift_IsDeleted As Boolean
    Private mblnShift_Start_Date As Boolean
    Private mblnShift_End_Date As Boolean
    Private mblnShift_IsFirst As Integer

#End Region

#Region "Properties"

    Public Property Shift_Start_Time() As String
        Get
            Return mdatShift_Start_Time
        End Get
        Set(ByVal Value As String)
            mdatShift_Start_Time = Value
        End Set
    End Property
    Public Property Shift_End_Time() As String
        Get
            Return mdatShift_End_Time
        End Get
        Set(ByVal Value As String)
            mdatShift_End_Time = Value
        End Set
    End Property
    Public Property Shift_IsDeleted() As Boolean
        Get
            Return mblnShift_IsDeleted
        End Get
        Set(ByVal Value As Boolean)
            mblnShift_IsDeleted = Value
        End Set
    End Property
    Public Property Shift_Start_Date() As Boolean
        Get
            Return mblnShift_Start_Date
        End Get
        Set(ByVal Value As Boolean)
            mblnShift_Start_Date = Value
        End Set
    End Property
    Public Property Shift_End_Date() As Boolean
        Get
            Return mblnShift_End_Date
        End Get
        Set(ByVal Value As Boolean)
            mblnShift_End_Date = Value
        End Set
    End Property
    Public Property Shift_IsFirst() As Integer
        Get
            Return mblnShift_IsFirst
        End Get
        Set(ByVal Value As Integer)
            mblnShift_IsFirst = Value
        End Set
    End Property

#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblShift"
        Me.TableIDFieldName = "Shift_ID"
        Me.TableNameFieldName = "Shift_Name"

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "Shift_Name,Shift_Start_Time,Shift_Start_Date,Shift_End_Time,Shift_End_Date,Shift_IsDeleted,Shift_IsFirst"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return clsDBStrings.SingleQot(Name) _
        & ",'" & (Shift_Start_Time) & "'" _
        & "," & clsDBStrings.ToDBBoolean(Shift_Start_Date) _
        & ",'" & (Shift_End_Time) & "'" _
        & "," & clsDBStrings.ToDBBoolean(Shift_End_Date) _
        & "," & clsDBStrings.ToDBBoolean(Shift_IsDeleted) _
        & "," & (Shift_IsFirst)

    End Function
    Protected Overrides Function OnGetUpdateValues() As String

        Return "Shift_Name=" & clsDBStrings.SingleQot(Name) _
        & ",Shift_Start_Time=" & "'" & (Shift_Start_Time) & "'" _
        & ",Shift_Start_Date=" & clsDBStrings.ToDBBoolean(Shift_Start_Date) _
        & ",Shift_End_Time=" & "'" & (Shift_End_Time) & "'" _
        & ",Shift_End_Date=" & clsDBStrings.ToDBBoolean(Shift_End_Date) _
        & ",Shift_IsDeleted=" & clsDBStrings.ToDBBoolean(Shift_IsDeleted) _
        & ",Shift_IsFirst=" & (Shift_IsFirst)

    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.Name = CStr(rdrReader.Item("Shift_Name").ToString())
        Me.Shift_Start_Time = CType(rdrReader.Item("Shift_Start_Time").ToString(), Date)
        Me.Shift_Start_Date = CType(rdrReader.Item("Shift_Start_Date").ToString(), Boolean)
        Me.Shift_End_Time = CType(rdrReader.Item("Shift_End_Time").ToString(), Date)
        Me.Shift_End_Date = CType(rdrReader.Item("Shift_End_Date").ToString(), Boolean)
        Me.Shift_IsDeleted = CType(rdrReader.Item("Shift_IsDeleted").ToString(), Boolean)
        Me.Shift_IsFirst = CType(rdrReader.Item("Shift_IsFirst").ToString(), Integer)

    End Function

#End Region

End Class
Public Class clsShiftS

    Inherits clsDatabaseObjectS

    Public Sub New()

        Me.TableName = "tblShift"
        Me.TableIDFieldName = "Shift_ID"
        Me.TableNameFieldName = "Shift_Name"

    End Sub
End Class