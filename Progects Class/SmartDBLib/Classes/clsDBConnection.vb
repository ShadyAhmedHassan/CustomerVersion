Imports System.Windows.Forms
Imports System.Windows.Forms.Control
Imports Kernel

Public Class clsConnectionTransaction

    Private mtrnTransaction As SqlTransaction = Nothing
    Private mconConnection As SqlConnection = Nothing
    Private mlngAffectedRecords As Long = 0

#Region "Constructors / Finalizer"
    ''' <summary>
    ''' New is declared as friend in order to be not creatable 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub New()
    End Sub

    Protected Overrides Sub Finalize()
        Me.Connection = Nothing 'This will automatically free mtrnTransaction object too
        MyBase.Finalize()
    End Sub
#End Region

#Region "Internal Properties"
    ''' <summary>
    ''' SQL Client Connection is assgined by the clsDBConnection here
    ''' This will be used in case of that clsDBConnection 
    ''' Internaly closes the connection or create/open a new one.
    ''' </summary>
    ''' <value>sqlConnection created by the clsDBConnection (mconConnection)</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property Connection() As SqlConnection
        Get
            Return mconConnection
        End Get
        Set(ByVal value As SqlConnection)
            Transaction = Nothing
            mconConnection = value
        End Set
    End Property

    ''' <summary>
    ''' Internally used to access the trasaction and used by clsDBConnection::Execute
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>When set this will dispose old transaction and reset the number of affected records</remarks>
    Friend Property Transaction() As SqlTransaction
        Get
            Return mtrnTransaction
        End Get
        Set(ByVal value As SqlTransaction)
            'On Error Resume Next
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            'Free memory of old transaction
            If mtrnTransaction IsNot Nothing Then
                mtrnTransaction.Dispose()
                mtrnTransaction = Nothing
            End If
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

            'Reset number of affected records
            Call AssignNumberOfAffectedRecords(0)

            'Assign transaction
            mtrnTransaction = value
        End Set
    End Property
#End Region

#Region "Transaction Commands"
    ''' <summary>
    ''' Begin a new transaction
    ''' </summary>
    ''' <remarks>Raises error in case of that another transaction is active</remarks>
    Public Sub Begin()
        If Not Me.Active Then

            Try
                Transaction = mconConnection.BeginTransaction()

            Catch ex As Exception
                Call clsLogFile.OnErrorWriteToLog(glogLogFile, TypeName(Me), "Begin", ex.Message, 0)
            End Try

        Else
            'Another transaction is active -- raise error
            Try
                'The following line will go to the exception handler (Catch ex)
                Call Err.Raise(vbObjectError + 1, , "Can not begin a transaction while there is an active transaction")
                ' |
                ' |
                '\ /
            Catch ex As Exception
            End Try
            'Error generated from the above line is handled there
            'Now write the error to log file
            Call clsLogFile.OnErrorWriteToLog(glogLogFile, TypeName(Me), "Begin", Err)

            'Return error to the caller method
            'Throw Exception

            'DO NOT WRITE CODE AFTER THIS LINE
            'Unreachable code goes here
            'End Try
        End If
    End Sub

    ''' <summary>
    ''' Commits active transaction
    ''' </summary>
    ''' <returns>true in case of transactions were committed, false in case of error or transaction has no affected records</returns>
    ''' <remarks></remarks>
    Public Function Commit() As Boolean
        'On Error Resume Next
        Call Err.Clear()
        If Me.Active Then

            Call Transaction.Commit()
            Commit = (Err.Number = 0) 'Commit will equal true if error number =0 otherwise it will equal false
            Try
                Call clsLogFile.OnErrorWriteToLog(glogLogFile, TypeName(Me), "Commit", Err)
            Catch ex As Exception
            End Try

            If Commit Then
                'If commit=true? (no errors?)
                'Free memory from current transaction , this will automatically make Active property = false
                Transaction = Nothing
            End If
        End If
    End Function

    ''' <summary>
    ''' Rollback a transaction
    ''' </summary>
    ''' <returns>true in case of transactions were rollback, false in case of error or transaction has no affected records</returns>
    ''' <remarks></remarks>
    Public Function Rollback() As Boolean
        'On Error Resume Next
        Call Err.Clear()
        If Me.Active Then

            Call Transaction.Rollback()
            Rollback = (Err.Number = 0) 'Rollback will equal true if error number =0 otherwise it will equal false
            Try
                Call clsLogFile.OnErrorWriteToLog(glogLogFile, TypeName(Me), "Rollback", Err)
            Catch ex As Exception
            End Try

            If Rollback Then
                'If rollback=true? (no errors?)
                'Free memory from current transaction , this will automatically make Active property = false
                Transaction = Nothing
            End If
        End If
    End Function
#End Region

#Region "State"

    ''' <summary>
    ''' Determines whether transaction has been begun by Begin() method
    ''' </summary>
    ''' <value></value>
    ''' <returns>true if transaction has been begun by Begin() method</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Active() As Boolean
        Get
            Return Transaction IsNot Nothing
        End Get
    End Property

    ''' <summary>
    ''' Number of affected records by this transaction, usually comes from clsDBConnection::Execute method
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Value is assgined by AssignNumberOfAffectedRecords() or IncNumberOfAffectedRecords() methods</remarks>
    Public ReadOnly Property AffectedRecords() As Long
        Get
            Return mlngAffectedRecords
        End Get
    End Property

    ''' <summary>
    ''' Sets affected number of records by the current transaction
    ''' </summary>
    ''' <param name="lngNumber"></param>
    ''' <remarks>Usually used to zerofy number of affected results or by the IncNumberOfAffectedRecords() method</remarks>
    Friend Sub AssignNumberOfAffectedRecords(ByVal lngNumber As Long)
        mlngAffectedRecords = lngNumber
    End Sub

    ''' <summary>
    ''' Increments number of affected records by the current transaction
    ''' </summary>
    ''' <param name="lngAddedValue"></param>
    ''' <remarks>Usually comes from clsDBConnection::Execute method</remarks>
    Friend Sub IncNumberOfAffectedRecords(ByVal lngAddedValue As Long)
        If Not Me.Active Then Exit Sub
        Call Me.AssignNumberOfAffectedRecords(Me.AffectedRecords + lngAddedValue)
    End Sub

#End Region

End Class

Public Class clsDBConnection
    Private mstrConnectionString As String = ""
    Private mconConnection As Data.SqlClient.SqlConnection = Nothing
    Private mblnIsOpen As Boolean = False
    Private mobjTransaction As clsConnectionTransaction = Nothing
    Private mstrLastExecutionSQL As String = ""

    Private Enum enmExcuteMode
        Scalar
        Reader
        NonQuery
        Procedure
        ReaderArray
    End Enum

#Region "Threading"
    Delegate Function mydlg(ByVal str As String) As Boolean
    REM 3 create object from delegation 
    Dim x As New mydlg(AddressOf Open)

    REM 4 create boolean to transfer data from worked  thread  to main thread
    Dim b As Boolean
    REM 5 create action that will be done when come back to main thread


    REM 6 create sub to send reuslt  and delegation from return to main thread
    Sub result(ByVal ir As IAsyncResult)
        b = x.EndInvoke(ir)
        'If b = True Then
        '    MessageBox.Show("Done")
        'Else
        '    MessageBox.Show("NO DATA FOUND")
        'End If
        ' Back to the main thred
        'Dim mt As New MethodInvoker(AddressOf updateui)
        'Dim MM As New Control
        'MM.Invoke(mt)
    End Sub
#End Region

    Public Sub New()
        'On Error Resume Next
        mobjTransaction = New clsConnectionTransaction()
        'Dim cb As New AsyncCallback(AddressOf result)
        'x.BeginInvoke(ConnectionString, cb, Nothing)
    End Sub
    Public Sub New(ByVal strConnectionString As String)
        Me.New()
        'On Error Resume Next
        Me.ConnectionString = strConnectionString
    End Sub
    Public Sub New(ByVal strDatabase As String, ByVal strServerName As String, Optional ByVal strLoginUserName As String = "sa", Optional ByVal strUserPassword As String = "sa")
        Me.New(clsDBStrings.BuildSQLConnectionString(strDatabase, strServerName, strLoginUserName, strUserPassword))
    End Sub
    Public Property ConnectionString() As String
        Get
            Return mstrConnectionString
        End Get
        Set(ByVal value As String)
            Call Me.Close()
            mstrConnectionString = value
        End Set
    End Property
    Public Function Open(Optional ByVal strConnectionString As String = "") As Boolean
        'On Error Resume Next
        If strConnectionString.Length > 0 Then
            Me.ConnectionString = strConnectionString
        End If
        Call Err.Clear()
        mconConnection = New SqlClient.SqlConnection(Me.ConnectionString)
        Call mconConnection.Open()
        Try
            Call clsLogFile.OnErrorWriteToLog(glogLogFile, TypeName(Me), "Open", Err)
        Catch ex As Exception
        End Try
        mblnIsOpen = (Err.Number = 0)

        If mblnIsOpen Then
            mobjTransaction.Connection = mconConnection
        End If

        Return mblnIsOpen
    End Function
    Public Sub Close()
        'On Error Resume Next
        If mconConnection IsNot Nothing Then
            If mconConnection.State = ConnectionState.Open Then
                Call mconConnection.Close()
            End If
            Call mconConnection.Dispose()
            mconConnection = Nothing
            mobjTransaction.Connection = Nothing
        End If
        mblnIsOpen = False
    End Sub
    Public Function ReOpen()
        'On Error Resume Next
        Call Me.Close()
        Return Me.Open(Me.ConnectionString)
    End Function
    Public ReadOnly Property IsOpen()
        Get
            'On Error Resume Next
            Return mblnIsOpen
        End Get
    End Property

    Private Function Execute(ByVal strSQL As String, ByVal envMode As enmExcuteMode, Optional ByVal intTimeOut As Integer = 15, Optional ByVal obj As Object = Nothing, Optional ByVal colname As String = "") As Object
        Dim cmdCommand As New SqlClient.SqlCommand
        'Dim cmdCommand As SqlClient.SqlCommand
        Try
            'cmdCommand.Connection=
            'On Error Resume Next
            mconConnection = New SqlClient.SqlConnection
            mconConnection.ConnectionString = ConnectionString
            If mconConnection.State = ConnectionState.Closed Then
                mconConnection.Open()
            End If
            mstrLastExecutionSQL = strSQL
            'cmdCommand = mconConnection.CreateCommand
            cmdCommand.Connection = mconConnection
            'If Me.Transaction.Active Then
            '    cmdCommand.Transaction = Me.Transaction.Transaction
            'End If
            cmdCommand.CommandText = strSQL
            cmdCommand.CommandTimeout = intTimeOut
            Call Err.Clear()
            Select Case envMode
                Case enmExcuteMode.NonQuery
                    Execute = cmdCommand.ExecuteNonQuery()
                    'Me.Transaction.IncNumberOfAffectedRecords(Execute)
                    mconConnection.Close()
                    mconConnection = Nothing
                Case enmExcuteMode.Reader
                    Execute = cmdCommand.ExecuteReader()
                Case enmExcuteMode.Scalar
                    Execute = cmdCommand.ExecuteScalar()
                    mconConnection.Close()
                    mconConnection = Nothing
                Case enmExcuteMode.Procedure
                    cmdCommand.CommandType = CommandType.StoredProcedure
                    Execute = cmdCommand.ExecuteNonQuery
                    'Me.Transaction.IncNumberOfAffectedRecords(Execute)
                Case enmExcuteMode.ReaderArray
                    obj.clear()
                    Dim dr As SqlClient.SqlDataReader
                    dr = cmdCommand.ExecuteReader()
                    While dr.Read
                        obj.Add(dr.Item(colname))
                    End While
                    dr.Close()
                    Execute = obj
            End Select
            Call clsLogFile.OnErrorWriteToLog(glogLogFile, TypeName(Me), "Execute", Err, "SQL statment:" & strSQL)
            cmdCommand.Dispose()
            cmdCommand = Nothing
        Catch ex As Exception
            cmdCommand.Dispose()
            cmdCommand = Nothing
            Throw ex
        End Try
    End Function
    Public Function ExecuteReaderToArray(ByVal strSQL As String, Optional ByVal intTimeOut As Integer = 30, Optional ByVal obj As Object = Nothing, Optional ByVal colname As String = "") As Object
        Dim cmdCommand As New SqlClient.SqlCommand
        'Dim cmdCommand As SqlClient.SqlCommand
        Try
            'cmdCommand.Connection=
            'On Error Resume Next
            mconConnection = New SqlClient.SqlConnection
            mconConnection.ConnectionString = ConnectionString
            If mconConnection.State = ConnectionState.Closed Then
                mconConnection.Open()
            End If
            mstrLastExecutionSQL = strSQL
            'cmdCommand = mconConnection.CreateCommand
            cmdCommand.Connection = mconConnection
            'If Me.Transaction.Active Then
            '    cmdCommand.Transaction = Me.Transaction.Transaction
            'End If
            cmdCommand.CommandText = strSQL
            cmdCommand.CommandTimeout = intTimeOut
            Call Err.Clear()

            Dim rdrdr As SqlClient.SqlDataReader = Nothing
            rdrdr = cmdCommand.ExecuteReader()
            If rdrdr IsNot Nothing Then
                Do While rdrdr.Read()
                    obj.Add(rdrdr.Item(colname))
                Loop
                rdrdr.Close()
                rdrdr = Nothing
            End If
            Return obj
            Call clsLogFile.OnErrorWriteToLog(glogLogFile, TypeName(Me), "Execute", Err, "SQL statment:" & strSQL)
            cmdCommand.Dispose()
            cmdCommand = Nothing
            If mconConnection.State = ConnectionState.Open Then
                mconConnection.Close()
            End If
            mconConnection.Dispose()
            mconConnection = Nothing
        Catch ex As Exception
            cmdCommand.Dispose()
            cmdCommand = Nothing
            cmdCommand.ExecuteReader.Close()
            'MsgBox(ex.Message)
        End Try
    End Function


    Public ReadOnly Property LastExecutionSQL() As String
        Get
            Return mstrLastExecutionSQL
        End Get
    End Property
    Public Function ExecuteReader(ByVal strSQL As String, Optional ByVal intTimeOut As Integer = 15) As Object
        'On Error Resume Next
        Return Me.Execute(strSQL, enmExcuteMode.Reader, intTimeOut)
    End Function

    Public Function ExecuteNonQuery(ByVal strDML As String, Optional ByVal intTimeOut As Integer = 15) As Integer
        'On Error Resume Next
        Return CInt(Me.Execute(strDML, enmExcuteMode.NonQuery, intTimeOut))
    End Function
    Public Function ExecuteScaler(ByVal strSQL As String, Optional ByVal intTimeOut As Integer = 15) As Object
        'On Error Resume Next
        Return Me.Execute(strSQL, enmExcuteMode.Scalar, intTimeOut)
    End Function
    Public Function StoredProcedure(ByVal SPName As String, Optional ByVal intTimeOut As Integer = 15) As Object
        'On Error Resume Next
        Return Me.Execute(SPName, enmExcuteMode.Procedure, intTimeOut)
    End Function
    Public Function GetRecordCount(ByVal strSQL As String) As Long
        Dim strQuery As String = ""
        'On Error Resume Next

        If strSQL.Trim().ToLower().StartsWith("select ") = False Then
            strSQL = "Select * from " & strSQL
        End If
        ''''                  Note                            '''''''''
        Return Me.ExecuteScaler("Select Count(1) from (" & strSQL & ") qryCount")
    End Function
    Public Function GetMaxFieldValue(ByVal strTableName As String, ByVal strFieldName As String) As Object
        Dim strQuery As String = ""
        'On Error Resume Next
        Return Me.ExecuteScaler("Select Max([" & strFieldName & "]) from [" & strTableName & "]")
    End Function
    Public Function GetMaxFieldValueLong(ByVal strTableName As String, ByVal strFieldName As String) As Long
        'On Error Resume Next
        Return CLng(GetMaxFieldValue(strTableName, strFieldName))
    End Function
    Public Function FillDS(ByVal strSQL As String)
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        If cn.State = ConnectionState.Closed Then
            cn.ConnectionString = mstrConnectionString
            cn.Open()
        End If
        cm.Connection = cn
        cm.CommandText = strSQL
        cm.ExecuteNonQuery()
        da.SelectCommand = cm
        da.Fill(ds)
        cm.Cancel()
        cm.Dispose()
        da.Dispose()
        cn.Close()
        Return ds
    End Function
    Public Function FillDS(ByVal strSQL As String, ByVal Ds As DataSet, Optional ByVal srcTable As String = "")
        Dim da As New SqlDataAdapter
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        If cn.State = ConnectionState.Closed Then
            cn.ConnectionString = mstrConnectionString
            cn.Open()
        End If
        cm.Connection = cn
        cm.CommandText = strSQL
        cm.ExecuteNonQuery()
        da.SelectCommand = cm
        If srcTable = "" Then
            da.Fill(Ds)
        Else
            da.Fill(Ds, srcTable)
        End If
        cm.Cancel()
        cm.Dispose()
        cn.Close()
        da.Dispose()
        Return Ds
    End Function
    Public Function FillDT(ByVal strSQL As String)
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        If cn.State = ConnectionState.Closed Then
            cn.ConnectionString = mstrConnectionString
            cn.Open()
        End If
        cm.Connection = cn
        cm.CommandText = strSQL
        cm.ExecuteNonQuery()
        da.SelectCommand = cm
        da.Fill(dt)
        cm.Cancel()
        cm.Dispose()
        cn.Close()
        da.Dispose()
        Return dt
    End Function

    Public Function FillDT(ByVal strSQL As String, ByVal DT As DataTable)
        Dim da As New SqlDataAdapter
        DT.Clear()
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        If cn.State = ConnectionState.Closed Then
            cn.ConnectionString = mstrConnectionString
            cn.Open()
        End If
        cm.Connection = cn
        cm.CommandText = strSQL
        cm.ExecuteNonQuery()
        da.SelectCommand = cm
        da.Fill(DT)
        cm.Cancel()
        cm.Dispose()
        cn.Close()
        da.Dispose()
        Return DT
    End Function
    Public Sub FillListManual(ByVal combo As Object, ByVal ParamArray DisplayValues() As Object)
        Dim i As Integer
        For i = 0 To DisplayValues.GetUpperBound(0)
            combo.items.add(DisplayValues(i))
        Next i
    End Sub
    Public Function FillList(ByRef objList As Object, ByVal strSQL As String, _
                            ByVal strDisplayColumn As String, ByVal strValueColumn As String) As Object
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        If cn.State = ConnectionState.Closed Then
            cn.ConnectionString = mstrConnectionString
            cn.Open()
        End If
        cm.Connection = cn
        cm.CommandText = strSQL
        cm.ExecuteNonQuery()
        da.SelectCommand = cm
        da.Fill(ds)
        objList.DataSource = ds
        objList.DisplayMember = ds.Tables(0).TableName + "." + strDisplayColumn
        objList.ValueMember = ds.Tables(0).TableName + "." + strValueColumn
        cm.Cancel()
        cm.Dispose()
        cn.Close()
        da.Dispose()
        Return objList


    End Function
    Public Overloads Function FillGrid(ByRef objList As Object, ByVal strSQL As String) As Object
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        If cn.State = ConnectionState.Closed Then
            cn.ConnectionString = mstrConnectionString
            cn.Open()
        End If
        cm.Connection = cn
        cm.CommandText = strSQL
        cm.ExecuteNonQuery()
        da.SelectCommand = cm
        da.Fill(ds)
        objList.DataSource = ds.Tables(0)
        cm.Cancel()
        cm.Dispose()
        cn.Close()
        da.Dispose()
        Return objList
    End Function

    Public ReadOnly Property Transaction() As clsConnectionTransaction
        Get
            Return mobjTransaction
        End Get

    End Property
    Protected Overrides Sub Finalize()
        mobjTransaction = Nothing
        MyBase.Finalize()
    End Sub
End Class
