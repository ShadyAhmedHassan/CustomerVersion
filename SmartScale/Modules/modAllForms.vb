Imports System.IO

Module modAllForms
    Public gEmailFromO As New clsEmailFrom
    Public gToEmailO As New clsToEmails
    Public gPortO As New ClsPorts
    Public gMessages As New ClsMessages
    Public gCountry As New clsCountryO
    Public gProtypeO As New clsProtypeO
    Public gGoodtypeO As New clsGoodtypeO
    Public gDailyQuotaWOContractO As New clsDailyQuotaWOContarctO
    Public gDailyQuotaWOContractGoodO As New clsDailyQuotaWOContarctGoodO
    Public gCompanyO As New clsCompanyO
    Public gPictureO As New clsPictureO
    Public gUserO As New clsUserO
    Public gShiftO As New clsShiftO
    Public gIssueToO As New clsIssueToO
    Public gDriverO As New clsDriverO
    Public gCarInfoO As New clsCarInfoO
    Public gCarO As New clsCarO
    Public gDealerO As New clsDealerO
    Public gCityO As New clsCityO
    Public gTransactionO As New clsTransactionsO
    ' 20/01/2020
    Public gUpdateTransactionO As New clsUpdateTransactionO
    Public gUpdateTransactionsS As New clsUpdateTransactionS
    Public gUpdateTransactionMstO As New clsUpDateTransactionMstO

    Public gGoodO As New clsGoodO
    Public gSlipo As New clsSlipO
    Public gScaleO As New clsScaleO
    Public gPermissionO As New clsPermissionO
    Public gUserPermissionO As New ClsUserPermissionO
    Public gRole As New clstblRolesO
    Public gControlRole As New clstblControlRolesO
    Public gContractorO As New clsContractorO
    Public gReturnsO As New clsReturnsO

    ''' Elsaman 11/12/2019 '''
    Public gContractO As New clsContractO
    Public gContractDetO As New clsContractDetO
    Public gGoodCategories As New clsGoodCategoriesO
    Public gDailyQuota As New clsDailyQuotaO
    Public gTransQuotaO As New clstblTransQuotaO
    Public gTransQuotaWOContractO As New clsTransQuotaWOContractO
    ' 6/2/2020 Eng Ahmad Fawzy
    Public gDebiteNoteO As New clsDebitNoteO
    Public gDebiteNoteS As New clsDebitNoteS
    ''  09/02/2020
    Public gDebitNoteQuantityO As New clsDebitNoteQuantityO
    Public gDebitNoteQuantityS As New clsDebitNoteQuantityS

    'Public gAdo As New ClsAdo("IT", "SmartScaleDB_1", "Amr", "123")
    Public gAdo As New ClsAdo(GetServerName(), GetDataBaseName(), GetUserID(), GetPassword())

    'Public GObjConection As New DBLib.clsDBConnection("server=IT;database=SmartScaleDB_1;user id=amr;password=123")
    Public GObjConection As New DBLib.clsDBConnection(GetConString(Application.StartupPath & "\ConnString.txt"))
    Public gLogin As New clsLogin
    Public gMethods As New clsMainMethods

    'Global variable storing the User Name
    Public gUserName As String
    Public gUserFullName As String
    'Global variable storing the User Picture
    Public gUserPic As Image
    'Global variable storing the User Login Time
    Public gLoginTime As DateTime
    'Global variable storing the User ID
    Public gUserID As Integer
    'Global variable storing the Slip Rate
    Public gWeightRate As Decimal = 0
    'Global Flag to define the save type [insert / update]
    Public gIsUpdate As Boolean = False
    'Public SIsUpdate As Boolean = False
    'Global variable holding the Error Message 
    Public gMsg As String
    'Global Flag to define the save type [insert / update]
    Public gIsNewTrans As Boolean
    'Global Flag to define the Current Mode [New Transaction - Search]
    Public gIsSearch As Boolean = False
    'Global variable holding the Last Driver ID
    Public LastDriver As Short
    'Global Flag to define the Update Mode In cFrmAddNew [Modify - Remove(IsDeleted=1)]
    Public gIsRemove As Boolean

    ' ~~~~~ TH ~~~~~~~~~~~~~ START ~~~
    Public ShiftID As Integer
    Public SaveOrCancel As Integer   ' 1 = save , 0 = cancel
    ' ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    Public gCarCity As String
    Public gCarCityID As Long
    Public gCarInfoID As Long
    Public gContractorID As Integer
    Public gTruckCity As String
    Public gTruckCityID As Long
    Public gTransID As Integer
    Public gTruckCtrls() As Control
    Public grptpth As New clsReportPath


    Public Function GetConString(ByVal FullPath As String, Optional ByRef ErrInfo As String = "") As String

        Dim strContents As String
        Dim objReader As StreamReader
        Try

            objReader = New StreamReader(FullPath)
            strContents = objReader.ReadToEnd()
            objReader.Close()
            Return strContents
        Catch Ex As Exception
            ErrInfo = Ex.Message
        End Try
    End Function

    Public Function GetServerName() As String

        Dim ServerName As String
        Dim objReader As StreamReader
        Try
            objReader = New StreamReader(Application.StartupPath & "\ConnString.txt")
            ServerName = objReader.ReadToEnd()
            ServerName = ServerName.Substring(0, ServerName.LastIndexOf(";"))
            ServerName = ServerName.Substring(0, ServerName.LastIndexOf(";"))
            ServerName = ServerName.Substring(0, ServerName.LastIndexOf(";"))
            ServerName = ServerName.Substring(ServerName.LastIndexOf("=") + 1)
            'ServerName = ServerName.Substring(0, ServerName.LastIndexOf("\SQLEXPRESS"))
            objReader.Close()
            Return ServerName
        Catch Ex As Exception
            'ErrInfo = Ex.Message
        End Try
    End Function

    Public Function GetDataBaseName() As String

        Dim DataBaseName As String
        Dim objReader As StreamReader
        Try
            objReader = New StreamReader(Application.StartupPath & "\ConnString.txt")
            DataBaseName = objReader.ReadToEnd()
            DataBaseName = DataBaseName.Substring(0, DataBaseName.LastIndexOf(";"))
            DataBaseName = DataBaseName.Substring(0, DataBaseName.LastIndexOf(";"))
            DataBaseName = DataBaseName.Substring(DataBaseName.LastIndexOf("=") + 1)
            objReader.Close()
            Return DataBaseName
        Catch Ex As Exception
            'ErrInfo = Ex.Message
        End Try
    End Function

    Public Function GetUserID() As String

        Dim UserID As String
        Dim objReader As StreamReader
        Try
            objReader = New StreamReader(Application.StartupPath & "\ConnString.txt")
            UserID = objReader.ReadToEnd()
            UserID = UserID.Substring(0, UserID.LastIndexOf(";"))
            UserID = UserID.Substring(UserID.LastIndexOf("=") + 1)
            objReader.Close()
            Return UserID
        Catch Ex As Exception
            'ErrInfo = Ex.Message
        End Try
    End Function

    Public Function GetPassword() As String

        Dim Password As String
        Dim objReader As StreamReader
        Try
            objReader = New StreamReader(Application.StartupPath & "\ConnString.txt")
            Password = objReader.ReadToEnd()
            Password = Password.Substring(Password.LastIndexOf("=") + 1)
            objReader.Close()
            Return Password
        Catch Ex As Exception
            'ErrInfo = Ex.Message
        End Try
    End Function

End Module
