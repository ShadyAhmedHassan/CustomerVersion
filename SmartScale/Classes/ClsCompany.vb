Public Class clsCompanyO
    Inherits clsDatabaseObjectO
#Region "Declarations"
    Private mintCompany_Pic_ID As clsPictureO
    Private mstrCompany_Address As String
    Private mstrCompany_Telephone As String
    Private mstrPassWord As String
    Private mstrManPass As String


#End Region

#Region "Properties"

    Public Property Company_Pic_ID() As clsPictureO
        Get
            Return mintCompany_Pic_ID
        End Get
        Set(ByVal Value As clsPictureO)
            mintCompany_Pic_ID = Value
        End Set
    End Property
    Public Property Company_Address() As String
        Get
            Return mstrCompany_Address
        End Get
        Set(ByVal Value As String)
            mstrCompany_Address = Value
        End Set
    End Property
    Public Property Company_Telephone() As String
        Get
            Return mstrCompany_Telephone
        End Get
        Set(ByVal Value As String)
            mstrCompany_Telephone = Value
        End Set
    End Property
    Public Property Password() As String
        Get
            Return mstrPassWord
        End Get
        Set(ByVal value As String)
            mstrPassWord = value
        End Set
    End Property
    Public Property ManPass() As String
        Get
            Return mstrManPass
        End Get
        Set(ByVal value As String)
            mstrManPass = value
        End Set
    End Property

#End Region

#Region "Methods"
    Public Sub New()

        Me.TableName = "tblCompany"
        Me.TableIDFieldName = "Company_ID"
        Me.TableNameFieldName = "Company_Name"
        mintCompany_Pic_ID = New clsPictureO

    End Sub
    Protected Overrides Sub Finalize()
        mintCompany_Pic_ID = Nothing
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        If Company_Pic_ID.ID = 0 Then
            Return "Company_Name,Company_Address,Company_Telephone,Password,ManPass"
        Else
            Return "Company_Name,Company_Pic_ID,Company_Address,Company_Telephone,Password,ManPass"
        End If
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        If Company_Pic_ID.ID = 0 Then
            Return clsDBStrings.SingleQot(Name) _
                    & "," & clsDBStrings.SingleQot(Company_Address) _
                    & "," & clsDBStrings.SingleQot(Company_Telephone) _
                    & "," & clsDBStrings.SingleQot(Password) _
                    & "," & clsDBStrings.SingleQot(ManPass)
        Else
            Return clsDBStrings.SingleQot(Name) _
                    & "," & (Company_Pic_ID.ID) _
                    & "," & clsDBStrings.SingleQot(Company_Address) _
                    & "," & clsDBStrings.SingleQot(Company_Telephone) _
                    & "," & clsDBStrings.SingleQot(Password) _
                    & "," & clsDBStrings.SingleQot(ManPass)
        End If


    End Function
    Protected Overrides Function OnGetUpdateValues() As String

        If Company_Pic_ID.ID = 0 Then
            Return "Company_Name=" & clsDBStrings.SingleQot(Name) _
                   & ",Company_Pic_ID= Null" _
                   & ",Company_Address=" & clsDBStrings.SingleQot(Company_Address) _
                   & ",Company_Telephone=" & clsDBStrings.SingleQot(Company_Telephone) _
                   & ",Password=" & clsDBStrings.SingleQot(Password) _
                   & ",ManPass=" & clsDBStrings.SingleQot(ManPass)
        Else
            Return "Company_Name=" & clsDBStrings.SingleQot(Name) _
                  & ",Company_Pic_ID=" & (Company_Pic_ID.ID) _
                  & ",Company_Address=" & clsDBStrings.SingleQot(Company_Address) _
                  & ",Company_Telephone=" & clsDBStrings.SingleQot(Company_Telephone) _
                  & ",Password=" & clsDBStrings.SingleQot(Password) _
                   & ",ManPass=" & clsDBStrings.SingleQot(ManPass)
        End If

    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean

        Me.Name = CStr(rdrReader.Item("Company_Name").ToString())
        If rdrReader.Item("Company_Pic_ID").ToString = Nothing Then
            Me.Company_Pic_ID.ID = 0
        Else
            Me.Company_Pic_ID.ID = CType(rdrReader.Item("Company_Pic_ID").ToString(), Integer)
        End If
        Me.Company_Address = CType(rdrReader.Item("Company_Address").ToString(), String)
        Me.Company_Telephone = CType(rdrReader.Item("Company_Telephone").ToString(), String)
        Me.Password = CType(rdrReader.Item("Password").ToString(), String)
        Me.ManPass = CType(rdrReader.Item("ManPass").ToString(), String)
    End Function

#End Region

End Class

Public Class clsCompanyS

    Inherits clsDatabaseObjectS

    Public Sub New()

        Me.TableName = "tblCompany"
        Me.TableIDFieldName = "Company_ID"
        Me.TableNameFieldName = "Company_Name"

    End Sub

End Class