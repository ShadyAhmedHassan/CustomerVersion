Public Class clsDebitNoteQuantityO
    Inherits clsDatabaseObjectO
#Region "Declarations"

    Private mstrNotes As String
    Private mintDebitNoteID As clsDebitNoteO
    Private mintGoodID As clsGoodO

#End Region

#Region "Properties"


    Public Property DebitNoteID() As clsDebitNoteO
        Get
            Return mintDebitNoteID
        End Get
        Set(ByVal Value As clsDebitNoteO)
            mintDebitNoteID = Value
        End Set
    End Property

    Public Property GoodID() As clsGoodO
        Get
            Return mintGoodID
        End Get
        Set(ByVal Value As clsGoodO)
            mintGoodID = Value
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
#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblDebitNoteQuantity"
        Me.TableIDFieldName = "DebitNoteQuantityID"
        Me.TableNameFieldName = ""

        mintDebitNoteID = New clsDebitNoteO
        mintGoodID = New clsGoodO

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "DebitNoteID,GoodID,Notes"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return (mintDebitNoteID.ID) _
         & "," & clsDBStrings.SingleQot(mintGoodID.ID) _
         & "," & clsDBStrings.SingleQot(Notes)
    End Function
    Protected Overrides Function OnGetUpdateValues() As String

        Return "DebitNoteID=" & mintDebitNoteID.ID _
         & ",GoodID=" & clsDBStrings.SingleQot(mintGoodID.ID) _
         & ",Notes=" & clsDBStrings.SingleQot(Notes)

    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean

        Me.DebitNoteID.ID = CType(rdrReader.Item("DebitNoteID").ToString(), Integer)
        Me.GoodID.ID = CType(rdrReader.Item("GoodID").ToString(), String)
        Me.Notes = CType(rdrReader.Item("Notes").ToString(), String)

    End Function
#End Region
End Class
Public Class clsDebitNoteQuantityS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblDebitNoteQuantity"
        Me.TableIDFieldName = "DebitNoteQuantityID"
        Me.TableNameFieldName = ""

    End Sub
End Class
