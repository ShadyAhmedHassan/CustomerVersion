Public Class clsDebitNoteO
    Inherits clsDatabaseObjectO
#Region "Declarations"

    Private mstrDebiteNote As String

    Private mblnFinished As Boolean
    Private mstrNotes As String
    Private mdecTotalQuantity As Decimal
    Private mdecOutgoingQuantity As Decimal

    Private mintContract As clsContractO
#End Region

#Region "Properties"

    Public Property DebitNote() As String
        Get
            Return mstrDebiteNote
        End Get
        Set(ByVal Value As String)
            mstrDebiteNote = Value
        End Set
    End Property

    Public Property ContractID() As clsContractO
        Get
            Return mintContract
        End Get
        Set(ByVal Value As clsContractO)
            mintContract = Value
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

    Public Property OutgoingQty() As Decimal
        Get
            Return mdecOutgoingQuantity
        End Get
        Set(ByVal Value As Decimal)
            mdecOutgoingQuantity = Value
        End Set
    End Property

    Public Property TotalQty() As Decimal
        Get
            Return mdecTotalQuantity
        End Get
        Set(ByVal Value As Decimal)
            mdecTotalQuantity = Value
        End Set
    End Property

#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblDebitNote"
        Me.TableIDFieldName = "DebitNoteID"
        Me.TableNameFieldName = ""

        mintContract = New clsContractO

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "DebitNote,ContractID,finish,Notes,TotalQuantity,OutgoingQuantity"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return clsDBStrings.SingleQot(DebitNote) _
         & "," & (mintContract.ID) _
         & "," & clsDBStrings.ToDBBoolean(Finished) _
         & "," & clsDBStrings.SingleQot(Notes) _
         & "," & clsDBStrings.SingleQot(TotalQty) _
         & "," & clsDBStrings.SingleQot(OutgoingQty)

    End Function
    Protected Overrides Function OnGetUpdateValues() As String

        Return "DebitNote=" & clsDBStrings.SingleQot(DebitNote) _
         & ",ContractID=" & mintContract.ID _
         & ",Finish=" & clsDBStrings.ToDBBoolean(Finished) _
         & ",Notes=" & clsDBStrings.SingleQot(Notes) _
         & ",TotalQuantity=" & clsDBStrings.SingleQot(TotalQty) _
         & ",OutgoingQuantity=" & clsDBStrings.SingleQot(OutgoingQty)
         
    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean

        Me.DebitNote = CType(rdrReader.Item("DebitNote").ToString(), String)
        Me.ContractID.ID = CType(rdrReader.Item("ContractID").ToString(), Integer)
        Me.Notes = CType(rdrReader.Item("Notes").ToString(), String)
        Me.Finished = CType(rdrReader.Item("Finish").ToString(), Boolean)
        Me.TotalQty = CType(rdrReader.Item("TotalQuantity").ToString(), Decimal)
        Me.OutgoingQty = CType(rdrReader.Item("OutgoingQuantity").ToString(), Decimal)

    End Function
#End Region
End Class
Public Class clsDebitNoteS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblDebitNote"
        Me.TableIDFieldName = "DebitNoteID"
        Me.TableNameFieldName = ""

    End Sub
End Class
