Public Class ClsMessages
    Inherits clsDatabaseObjectO


    Private mintMessageCountry As Integer

    Public Property MessageCountry() As Integer
        Get
            Return mintMessageCountry
        End Get
        Set(ByVal Value As Integer)
            mintMessageCountry = Value
        End Set
    End Property

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblMessages"
        Me.TableIDFieldName = "Msg_ID"
        Me.TableNameFieldName = "Message"

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "Message,MessageCountry"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return clsDBStrings.SingleQot(Name) _
         & "," & (MessageCountry)
    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "Message=" & clsDBStrings.SingleQot(Name) _
          & ",MessageCountry=" & (MessageCountry)
    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.Name = CStr(rdrReader.Item("Message").ToString())
        Me.MessageCountry = CType(rdrReader.Item("MessageCountry").ToString(), String)
    End Function


#End Region
End Class

Public Class ClsMessagesS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblMessages"
        Me.TableIDFieldName = "Msg_ID"
        Me.TableNameFieldName = "Message"

    End Sub
End Class
