Public Class clsDailyQuotaO
    Inherits clsDatabaseObjectO
#Region "Declarations"

    Private mintDebitNote As clsDebitNoteO
    Private mdecQuota As Decimal
    Private mdateDateQuota As Date
    Private mstrNotes As String
    Private mblnFinished As Boolean
    Private mdecOutGoingQty As Decimal

#End Region

#Region "Properties"

    Public Property DebitNoteID() As clsDebitNoteO
        Get
            Return mintDebitNote
        End Get
        Set(ByVal Value As clsDebitNoteO)
            mintDebitNote = Value
        End Set
    End Property

    Public Property Quota() As Decimal
        Get
            Return mdecQuota
        End Get
        Set(ByVal Value As Decimal)
            mdecQuota = Value
        End Set
    End Property

    Public Property DateQuota() As Date
        Get
            Return mdateDateQuota
        End Get
        Set(ByVal Value As Date)
            mdateDateQuota = Value
        End Set
    End Property

    Public Property Notes() As String
        Get
            Return mstrNotes
        End Get
        Set(ByVal Value As String)
            mstrNotes = Value
        End Set
    End Property

    Public Property Finished() As Boolean
        Get
            Return mblnFinished
        End Get
        Set(ByVal Value As Boolean)
            mblnFinished = Value
        End Set
    End Property

    Public Property OutGoingQty() As Decimal
        Get
            Return mdecOutGoingQty
        End Get
        Set(ByVal Value As Decimal)
            mdecOutGoingQty = Value
        End Set
    End Property

#End Region

#Region "Methods"
    Public Sub New()

        Me.TableName = "tblDailyQuota"
        Me.TableIDFieldName = "QuotaID"
        Me.TableNameFieldName = ""

        mintDebitNote = New clsDebitNoteO
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "DebitNoteID,Quota,DateQuota,Notes,Finished,OutGoingQty"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return (DebitNoteID.ID) _
         & "," & (Quota) _
         & "," & clsDBStrings.ToDBDate(DateQuota) _
         & "," & clsDBStrings.SingleQot(Notes) _
         & "," & clsDBStrings.ToDBBoolean(Finished) _
         & "," & (OutGoingQty)


    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "DebitNoteID=" & (DebitNoteID.ID) _
         & ",Quota=" & (Quota) _
         & ",DateQuota=" & clsDBStrings.ToDBDate(DateQuota) _
         & ",Notes=" & clsDBStrings.SingleQot(Notes) _
         & ",Finished=" & clsDBStrings.ToDBBoolean(Finished) _
         & ",OutGoingQty=" & (OutGoingQty)

    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.DebitNoteID.ID = CType(rdrReader.Item("DebitNoteID").ToString(), Integer)
        Me.Quota = CType(rdrReader.Item("Quota").ToString(), Decimal)
        Me.DateQuota = CType(rdrReader.Item("DateQuota").ToString(), Date)
        Me.Notes = CType(rdrReader.Item("Notes").ToString(), String)
        Me.Finished = CType(rdrReader.Item("Finished").ToString(), Boolean)
        Me.OutGoingQty = CType(rdrReader.Item("OutGoingQty").ToString(), Decimal)

    End Function
#End Region
End Class
Public Class clsDailyQuotaS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblDailyQuota"
        Me.TableIDFieldName = "QuotaID"
        Me.TableNameFieldName = ""

    End Sub
End Class
