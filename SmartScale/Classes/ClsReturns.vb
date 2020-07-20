Public Class clsReturnsO
    Inherits clsDatabaseObjectO
#Region "Declarations"

    Private mdatIn_Date As String
    Private mdatOut_Date As String
    Private mintIn_Shift_ID As clsShiftO
    Private mdatIn_Shift_Date As String

    Private mintCar_ID As clsCarO
    Private mintDriver_ID As clsDriverO
    Private mintCar_Info_ID As clsCarInfoO
    Private mintDealer_ID As clsDealerO

    Private mintIssue_To_ID As clsIssueToO
    Private mdecFirst_Weigth As Decimal
    Private mblnFirst_Weight_IsEmpty As Boolean
    Private mintFirst_Weigth_Scale_ID As clsScaleO
    Private mintFirst_Weight_User_ID As clsUserO
    Private mdecSecond_Weight As Decimal
    Private mblnSecond_Weight_IsEmpty As Boolean
    Private mintSecond_Weight_User_ID As clsUserO
    Private mintSecond_Weight_Scale_ID As clsScaleO
    Private mintGood_ID As clsGoodO
    Private mdecSlip_Rate As Decimal
    Private mdatSlip_Charged_Date As String
    Private mintSlip_Charged_User_ID As Integer
    Private minLocationID As Integer

#End Region

#Region "Properties"
    Public Property In_Date() As String
        Get
            Return mdatIn_Date
        End Get
        Set(ByVal Value As String)
            mdatIn_Date = Value
        End Set
    End Property
    Public Property Out_Date() As String
        Get
            Return mdatOut_Date
        End Get
        Set(ByVal Value As String)
            mdatOut_Date = Value
        End Set
    End Property
    Public Property In_Shift_ID() As clsShiftO
        Get
            Return mintIn_Shift_ID
        End Get
        Set(ByVal Value As clsShiftO)
            mintIn_Shift_ID = Value
        End Set
    End Property
    Public Property In_Shift_Date() As String
        Get
            Return mdatIn_Shift_Date
        End Get
        Set(ByVal Value As String)
            mdatIn_Shift_Date = Value
        End Set
    End Property

    Public Property Car_ID() As clsCarO
        Get
            Return mintCar_ID
        End Get
        Set(ByVal Value As clsCarO)
            mintCar_ID = Value
        End Set
    End Property
    Public Property Driver_ID() As clsDriverO
        Get
            Return mintDriver_ID
        End Get
        Set(ByVal Value As clsDriverO)
            mintDriver_ID = Value
        End Set
    End Property
    Public Property Car_Info_ID() As clsCarInfoO
        Get
            Return mintCar_Info_ID
        End Get
        Set(ByVal Value As clsCarInfoO)
            mintCar_Info_ID = Value
        End Set
    End Property
    Public Property Dealer_ID() As clsDealerO
        Get
            Return mintDealer_ID
        End Get
        Set(ByVal Value As clsDealerO)
            mintDealer_ID = Value
        End Set
    End Property
    Public Property Issue_To_ID() As clsIssueToO
        Get
            Return mintIssue_To_ID
        End Get
        Set(ByVal Value As clsIssueToO)
            mintIssue_To_ID = Value
        End Set
    End Property
    Public Property First_Weigth() As Decimal
        Get
            Return mdecFirst_Weigth
        End Get
        Set(ByVal Value As Decimal)
            mdecFirst_Weigth = Value
        End Set
    End Property
    Public Property First_Weight_IsEmpty() As Boolean
        Get
            Return mblnFirst_Weight_IsEmpty
        End Get
        Set(ByVal Value As Boolean)
            mblnFirst_Weight_IsEmpty = Value
        End Set
    End Property
    Public Property First_Weigth_Scale_ID() As clsScaleO
        Get
            Return mintFirst_Weigth_Scale_ID
        End Get
        Set(ByVal Value As clsScaleO)
            mintFirst_Weigth_Scale_ID = Value
        End Set
    End Property
    Public Property First_Weight_User_ID() As clsUserO
        Get
            Return mintFirst_Weight_User_ID
        End Get
        Set(ByVal Value As clsUserO)
            mintFirst_Weight_User_ID = Value
        End Set
    End Property
    Public Property Second_Weight() As Decimal
        Get
            Return mdecSecond_Weight
        End Get
        Set(ByVal Value As Decimal)
            mdecSecond_Weight = Value
        End Set
    End Property
    Public Property Second_Weight_IsEmpty() As Boolean
        Get
            Return mblnSecond_Weight_IsEmpty
        End Get
        Set(ByVal Value As Boolean)
            mblnSecond_Weight_IsEmpty = Value
        End Set
    End Property
    Public Property Second_Weight_User_ID() As clsUserO
        Get
            Return mintSecond_Weight_User_ID
        End Get
        Set(ByVal Value As clsUserO)
            mintSecond_Weight_User_ID = Value
        End Set
    End Property
    Public Property Second_Weight_Scale_ID() As clsScaleO
        Get
            Return mintSecond_Weight_Scale_ID
        End Get
        Set(ByVal Value As clsScaleO)
            mintSecond_Weight_Scale_ID = Value
        End Set
    End Property
    Public Property Good_ID() As clsGoodO
        Get
            Return mintGood_ID
        End Get
        Set(ByVal Value As clsGoodO)
            mintGood_ID = Value
        End Set
    End Property
    Public Property Slip_Rate() As Decimal
        Get
            Return mdecSlip_Rate
        End Get
        Set(ByVal Value As Decimal)
            mdecSlip_Rate = Value
        End Set
    End Property
    Public Property Slip_Charged_Date() As String
        Get
            Return mdatSlip_Charged_Date
        End Get
        Set(ByVal Value As String)
            mdatSlip_Charged_Date = Value
        End Set
    End Property
    Public Property Slip_Charged_User_ID() As Integer
        Get
            Return mintSlip_Charged_User_ID
        End Get
        Set(ByVal Value As Integer)
            mintSlip_Charged_User_ID = Value
        End Set
    End Property

    Public Property LocationID() As Integer
        Get
            Return minLocationID
        End Get
        Set(ByVal value As Integer)
            minLocationID = value
        End Set
    End Property

#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblReturns"
        Me.TableIDFieldName = "Trans_ID"
        Me.TableNameFieldName = ""

        mintIn_Shift_ID = New clsShiftO
        mintCar_ID = New clsCarO
        mintDriver_ID = New clsDriverO
        mintCar_Info_ID = New clsCarInfoO
        mintIssue_To_ID = New clsIssueToO
        mintFirst_Weigth_Scale_ID = New clsScaleO
        mintFirst_Weight_User_ID = New clsUserO
        mintSecond_Weight_User_ID = New clsUserO
        mintSecond_Weight_Scale_ID = New clsScaleO
        mintGood_ID = New clsGoodO
        mintDealer_ID = New clsDealerO
    End Sub
    Protected Overrides Sub Finalize()
        mintIn_Shift_ID = Nothing
        mintDealer_ID = Nothing
        mintCar_ID = Nothing
        mintDriver_ID = Nothing
        mintCar_Info_ID = Nothing
        mintIssue_To_ID = Nothing
        mintFirst_Weigth_Scale_ID = Nothing
        mintFirst_Weight_User_ID = Nothing
        mintSecond_Weight_User_ID = Nothing
        mintSecond_Weight_Scale_ID = Nothing
        mintGood_ID = Nothing
        minLocationID = Nothing

        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "In_Date,Out_Date,In_Shift_ID,In_Shift_Date,Car_ID,Driver_ID,Car_Info_ID,Dealer_ID,Issue_To_ID,First_Weigth,First_Weight_IsEmpty,First_Weigth_Scale_ID,First_Weight_User_ID,Second_Weight,Second_Weight_IsEmpty,Second_Weight_User_ID,Second_Weight_Scale_ID,Good_ID,Slip_Rate,Slip_Charged_Date,Slip_Charged_User_ID,LocationID"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return clsDBStrings.ToDBDate(In_Date) _
        & "," & IIf(Out_Date = DateTime.MinValue, "NULL", clsDBStrings.ToDBDate(Out_Date)) _
        & "," & (In_Shift_ID.ID) _
        & "," & clsDBStrings.ToDBDate(In_Shift_Date) _
        & "," & (Car_ID.ID) _
        & "," & (Driver_ID.ID) _
        & "," & (Car_Info_ID.ID) _
        & "," & (Dealer_ID.ID) _
        & "," & (Issue_To_ID.ID) _
        & "," & (First_Weigth) _
        & "," & clsDBStrings.ToDBBoolean(First_Weight_IsEmpty) _
        & "," & (First_Weigth_Scale_ID.ID) _
        & "," & (First_Weight_User_ID.ID) _
        & "," & (Second_Weight) _
        & "," & clsDBStrings.ToDBBoolean(Second_Weight_IsEmpty) _
        & "," & (Second_Weight_User_ID.ID) _
        & "," & (Second_Weight_Scale_ID.ID) _
        & "," & clsDBStrings.SingleQot(Good_ID.ID) _
        & "," & (Slip_Rate) _
        & "," & clsDBStrings.ToDBDate(Slip_Charged_Date) _
        & "," & (Slip_Charged_User_ID) _
        & "," & clsDBStrings.SingleQot(LocationID)

    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "In_Date=" & clsDBStrings.ToDBDate(In_Date) _
        & ",Out_Date=" & IIf(Out_Date = DateTime.MinValue, "NULL", clsDBStrings.ToDBDate(Out_Date)) _
        & ",In_Shift_ID=" & (In_Shift_ID.ID) _
        & ",In_Shift_Date=" & clsDBStrings.ToDBDate(In_Shift_Date) _
        & ",Car_ID=" & (Car_ID.ID) _
        & ",Driver_ID=" & (Driver_ID.ID) _
        & ",Car_Info_ID=" & (Car_Info_ID.ID) _
         & ",Dealer_ID=" & (Dealer_ID.ID) _
        & ",Issue_To_ID=" & (Issue_To_ID.ID) _
        & ",First_Weigth=" & (First_Weigth) _
        & ",First_Weight_IsEmpty=" & clsDBStrings.ToDBBoolean(First_Weight_IsEmpty) _
        & ",First_Weigth_Scale_ID=" & (First_Weigth_Scale_ID.ID) _
        & ",First_Weight_User_ID=" & (First_Weight_User_ID.ID) _
        & ",Second_Weight=" & (Second_Weight) _
        & ",Second_Weight_IsEmpty=" & clsDBStrings.ToDBBoolean(Second_Weight_IsEmpty) _
        & ",Second_Weight_User_ID=" & (Second_Weight_User_ID.ID) _
        & ",Second_Weight_Scale_ID=" & (Second_Weight_Scale_ID.ID) _
        & ",Good_ID=" & (Good_ID.ID) _
        & ",Slip_Rate=" & (Slip_Rate) _
        & ",Slip_Charged_Date=" & clsDBStrings.ToDBDate(Slip_Charged_Date) _
        & ",Slip_Charged_User_ID=" & (Slip_Charged_User_ID) _
        & ",LocationID=" & (LocationID)

    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.In_Date = CType(rdrReader.Item("In_Date").ToString(), String)
        Me.Out_Date = CType(rdrReader.Item("Out_Date").ToString(), String)
        Me.In_Shift_ID.ID = CType(rdrReader.Item("In_Shift_ID").ToString(), Integer)
        Me.In_Shift_Date = CType(rdrReader.Item("In_Shift_Date").ToString(), String)
        Me.Car_ID.ID = CType(rdrReader.Item("Car_ID").ToString(), Integer)
        Me.Driver_ID.ID = CType(rdrReader.Item("Driver_ID").ToString(), Integer)
        Me.Car_Info_ID.ID = CType(rdrReader.Item("Car_Info_ID").ToString(), Integer)
        Me.Dealer_ID.ID = CType(rdrReader.Item("Dealer_ID").ToString(), Integer)
        Me.Issue_To_ID.ID = CType(rdrReader.Item("Issue_To_ID").ToString(), Integer)
        Me.First_Weigth = CType(rdrReader.Item("First_Weigth").ToString(), Decimal)
        Me.First_Weight_IsEmpty = CType(rdrReader.Item("First_Weight_IsEmpty").ToString(), Boolean)
        Me.First_Weigth_Scale_ID.ID = CType(rdrReader.Item("First_Weigth_Scale_ID").ToString(), Integer)
        Me.First_Weight_User_ID.ID = CType(rdrReader.Item("First_Weight_User_ID").ToString(), Integer)
        Me.Second_Weight = CType(rdrReader.Item("Second_Weight").ToString(), Decimal)
        Me.Second_Weight_IsEmpty = CType(rdrReader.Item("Second_Weight_IsEmpty").ToString(), Boolean)
        Me.Second_Weight_User_ID.ID = CType(rdrReader.Item("Second_Weight_User_ID").ToString(), Integer)
        Me.Second_Weight_Scale_ID.ID = CType(rdrReader.Item("Second_Weight_Scale_ID").ToString(), Integer)
        Me.Good_ID.ID = CType(rdrReader.Item("Good_ID").ToString(), String)
        Me.Slip_Rate = CType(rdrReader.Item("Slip_Rate").ToString(), Decimal)
        Me.Slip_Charged_Date = CType(rdrReader.Item("Slip_Charged_Date").ToString(), String)
        Me.Slip_Charged_User_ID = CType(rdrReader.Item("Slip_Charged_User_ID").ToString(), Integer)
        If (IsDBNull(rdrReader.Item("LocationID"))) Then
            Me.LocationID = 0
        Else : Me.LocationID = CType(rdrReader.Item("LocationID").ToString(), Integer)
        End If

    End Function

#End Region

End Class
Public Class clsReturnsS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblReturns"
        Me.TableIDFieldName = "Trans_ID"
        Me.TableNameFieldName = ""
    End Sub
End Class