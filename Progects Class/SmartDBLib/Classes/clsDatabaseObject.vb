Option Explicit On
Imports Kernel
Imports DBLib

Public Class clsDatabaseObjectO

    Private mstrTableName As String = ""
    Private mstrTableIDFieldName As String = ""
    Private mstrTableNameFieldName As String = ""
    Private mlngID As String = ""
    Private mstrName As String = ""
    Private mstrID As String = ""


    Public Overridable Property ID() As String
        Get
            'On Error Resume Next
            Return mlngID
        End Get
        Set(ByVal value As String)
            mlngID = value
        End Set
    End Property

    Public Overridable Property IDStr() As String
        Get
            Return mstrID
        End Get
        Set(ByVal value As String)
            mstrID = value
        End Set
    End Property

    Public Overridable Property Name() As String
        Get
            'On Error Resume Next
            Return mstrName
        End Get
        Set(ByVal value As String)
            'On Error Resume Next
            mstrName = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return Me.Name
    End Function


#Region "Table Description Properties"

    Protected Property TableName() As String
        Get
            Return mstrTableName
        End Get
        Set(ByVal value As String)
            mstrTableName = value
        End Set
    End Property
    Protected Property TableIDFieldName() As String
        Get
            Return mstrTableIDFieldName
        End Get
        Set(ByVal value As String)
            mstrTableIDFieldName = value
        End Set
    End Property
    Protected Property TableNameFieldName() As String
        Get
            Return mstrTableNameFieldName
        End Get
        Set(ByVal value As String)
            mstrTableNameFieldName = value
        End Set
    End Property
#End Region

#Region "Insert And Update"
    'تجهيز أسماء الحقول المراد ادخالها
    Protected Overridable Function OnGetInsertFieldNames() As String
        'On Error Resume Next
        If Me.TableNameFieldName.Length > 0 Then
            Return "[" & Me.TableNameFieldName & "]"
        End If
    End Function
    'تجهيز القيم التى ستدخل
    Protected Overridable Function OnGetInsertValues() As String
        If Me.TableNameFieldName.Length > 0 Then
            Return clsDBStrings.SingleQot(Me.Name)
        End If
    End Function
    'لعمل التعديل فى حقل الإسم
    ' و اذا كان أكثر من حقل فيتم عمل أوفرريد
    Protected Overridable Function OnGetUpdateValues() As String
        If Me.TableNameFieldName.Length > 0 Then
            Return "[" & Me.TableName & "].[" & Me.TableNameFieldName & "]=" & clsDBStrings.SingleQot(Me.Name)
        End If
    End Function
    'هنا يتم عمل الإدخال و الحذف

    '---------------------------------- Flages ---------------------

    Protected Overridable Function OnBeforeSave() As Boolean
        Return True
    End Function

    Protected Overridable Function OnAfterSave() As Boolean
        Return True
    End Function


    Public Overridable Function Save() As Boolean

        'On Error Resume Next

        If Me.ID = 0 Then
            'Add Mode
            If Me.OnBeforeSave Then
                Try
                    Dim stf, stv As String
                    stf = OnGetInsertFieldNames()
                    stv = OnGetInsertValues()
                    Dim st As String
                    st = "set dateformat ymd Insert into [" & Me.TableName & "]" & " (" & OnGetInsertFieldNames() & ")" _
                                                                        & " VALUES (" _
                                                                        & OnGetInsertValues() _
                                                                        & ")"
                    st = st
                Catch ex As Exception

                End Try

                If gconConnection.ExecuteNonQuery("set dateformat ymd Insert into [" & Me.TableName & "]" & " (" & OnGetInsertFieldNames() & ")" _
                                                    & " VALUES (" _
                                                    & OnGetInsertValues() _
                                                    & ")") > 0 Then

                    Me.ID = gconConnection.GetMaxFieldValueLong(Me.TableName, Me.TableIDFieldName)
                    Save = (Me.ID > 0) 'ID Changed -- Save is OK
                    Save = (Save And Me.OnAfterSave())
                Else
                    Call clsLogFile.OnErrorWriteToLog(glogLogFile, TypeName(Me), "Save", Err, "Dml statment:" & gconConnection.LastExecutionSQL)
                End If
            End If
        Else
            'Edit mode
            If gconConnection.ExecuteNonQuery("set dateformat ymd Update [" & Me.TableName & "]" _
                                                & " Set " _
                                                & OnGetUpdateValues() _
                                                & " Where [" & Me.TableName & "].[" & Me.TableIDFieldName & "]=" & Me.ID.ToString() _
                                                ) > 0 Then
                Save = True
            Else
                Call Err.Raise(vbObjectError + 1, , "No records to be updated")
                Call clsLogFile.OnErrorWriteToLog(glogLogFile, TypeName(Me), "Save", Err, "Dml statment:" & gconConnection.LastExecutionSQL)
            End If
        End If
    End Function

    Public Overridable Function NOTIdentitySave() As Boolean

        'On Error Resume Next
        If gconConnection.ExecuteScaler("select count(*) from [" & Me.TableName & "] Where [" & Me.TableName & "].[" & Me.TableIDFieldName & "] ='" & Me.IDStr & "'") = 0 Then


            'Add Mode
            If Me.OnBeforeSave Then
                Try
                    'If gconConnection.ExecuteScaler("select count(*) from [" & Me.TableName & "]") = 0 Then
                    '    Me.ID = 1
                    'Else
                    '    Me.ID = gconConnection.GetMaxFieldValue(Me.TableName, Me.mstrTableIDFieldName) + 1

                    'End If

                    'Dim stf, stv As String
                    'stf = OnGetInsertFieldNames()
                    'stv = OnGetInsertValues()
                    Dim str As String
                    'str = "set dateformat ymd Insert into [" & Me.TableName & "]" & " (" & OnGetInsertFieldNames() & ")" _
                    '                                                    & " VALUES (" _
                    '                                                    & OnGetInsertValues() _
                    '                                                    & ")"


                Catch ex As Exception

                End Try

                If gconConnection.ExecuteNonQuery("set dateformat ymd Insert into [" & Me.TableName & "]" & " (" & OnGetInsertFieldNames() & ")" _
                                                    & " VALUES (" _
                                                    & OnGetInsertValues() _
                                                    & ")") > 0 Then

                    'NOTIdentitySave = (Me.IDStr > 0) 'ID Changed -- NOTIdentitySave is OK
                    'NOTIdentitySave = (NOTIdentitySave() And Me.OnAfterSave())
                Else
                    Call clsLogFile.OnErrorWriteToLog(glogLogFile, TypeName(Me), "NOTIdentitySave", Err, "Dml statment:" & gconConnection.LastExecutionSQL)
                End If
            End If
        Else
            'Edit mode
            Dim uu As String = ""
            uu = "set dateformat ymd Update [" & Me.TableName & "]" _
                                                & " Set " _
                                                & OnGetUpdateValues() _
                                                & " Where [" & Me.TableName & "].[" & Me.TableIDFieldName & "]='" & Me.IDStr() & "'"

            If gconConnection.ExecuteNonQuery("set dateformat ymd Update [" & Me.TableName & "]" _
                                                & " Set " _
                                                & OnGetUpdateValues() _
                                                & " Where [" & Me.TableName & "].[" & Me.TableIDFieldName & "]='" & Me.IDStr() & "'" _
                                                ) > 0 Then
                NOTIdentitySave = True
            Else
                Call Err.Raise(vbObjectError + 1, , "No records to be updated")
                Call clsLogFile.OnErrorWriteToLog(glogLogFile, TypeName(Me), "NOTIdentitySave", Err, "Dml statment:" & gconConnection.LastExecutionSQL)
            End If
        End If
    End Function


#End Region

#Region "Binding"
    '------------------------------------------------ Flags ------------------------------------
    Protected Overridable Function OnAfterRefresh() As Boolean
        Return True
    End Function
    '---------------------------------------------------------------------------------------------------
    'إحضار الصف المراد عمل البيندج له
    Protected Overridable Function OnGetRefreshSQL() As String
        'On Error Resume Next
        Return "Select * from [" & mstrTableName & "] where [" & mstrTableName & "].[" & mstrTableIDFieldName & "]=" & ID.ToString()
    End Function
    Protected Overridable Function OnGetRefreshSQLstrID() As String
        'On Error Resume Next
        Return "Select * from [" & mstrTableName & "] where [" & mstrTableName & "].[" & mstrTableIDFieldName & "]=" & IDStr.ToString()
    End Function
    'إحضار الحقول التى سيعمل لها بيندج
    Protected Friend Overridable Function OnRefreshFields(ByRef rdrReader As SqlClient.SqlDataReader) As Boolean
        'On Error Resume Next
        If Me.TableNameFieldName.Length > 0 Then
            'Name field in use
            Me.Name = rdrReader.Item(TableNameFieldName).ToString()
        End If
    End Function
    'هنا سيتم تركيب القيم على الحقول
    Public Overridable Function Refresh() As Boolean
        Dim rdrReader As SqlClient.SqlDataReader = Nothing
        Dim strSelectQuery As String = ""
        'On Error Resume Next
        rdrReader = gconConnection.ExecuteReader(OnGetRefreshSQL())
        If rdrReader IsNot Nothing Then
            If rdrReader.Read() Then
                'There is data
                Refresh = True
                '------------------------------------------------------------
                'Copy Values to Properties
                Call OnRefreshFields(rdrReader)
                '------------------------------------------------------------
            End If

            rdrReader.Close()
            rdrReader = Nothing
        End If

        If Refresh = True Then
            Return OnAfterRefresh()
        End If
    End Function

    Public Overridable Function RefreshStrID() As Boolean
        Dim rdrReader As SqlClient.SqlDataReader = Nothing
        Dim strSelectQuery As String = ""
        'On Error Resume Next
        rdrReader = gconConnection.ExecuteReader(OnGetRefreshSQLstrID())
        If rdrReader IsNot Nothing Then
            If rdrReader.Read() Then
                'There is data
                RefreshStrID = True
                '------------------------------------------------------------
                'Copy Values to Properties
                Call OnRefreshFields(rdrReader)
                '------------------------------------------------------------
            End If

            rdrReader.Close()
            rdrReader = Nothing
        End If

        'If RefreshStrID() = True Then
        '    Return OnAfterRefresh()
        'End If
    End Function

#End Region

#Region "Remove"
    Public Overridable Function Remove() As Boolean
        Dim objTable As clsDatabaseObjectS
        'On Error Resume Next
        objTable = New clsDatabaseObjectS
        objTable.TableName = Me.TableName
        objTable.TableIDFieldName = Me.TableIDFieldName
        Call objTable.Remove(Me.ID)
        objTable = Nothing
        Return True
    End Function

#End Region

End Class

Public Class clsDatabaseObjectS
    Private mstrTableName As String = ""
    Private mstrTableIDFieldName As String = ""
    Private mstrTableNameFieldName As String = ""


#Region "Table Description Properties"
    Protected Friend Property TableName() As String
        Get
            Return mstrTableName
        End Get
        Set(ByVal value As String)
            mstrTableName = value
        End Set
    End Property
    Protected Friend Property TableIDFieldName() As String
        Get
            Return mstrTableIDFieldName
        End Get
        Set(ByVal value As String)
            mstrTableIDFieldName = value
        End Set
    End Property

    Protected Friend Property TableNameFieldName() As String
        Get
            Return mstrTableNameFieldName
        End Get
        Set(ByVal value As String)
            mstrTableNameFieldName = value
        End Set
    End Property
#End Region


    Public Overridable Function Remove(ByVal lngID As Long) As Boolean
        'On Error Resume Next
        If Not OnBeforeRemove(lngID) Then
            Return False
        End If

        If (gconConnection.ExecuteNonQuery("Delete from [" & Me.TableName & "] Where [" & Me.TableName & "].[" & Me.TableIDFieldName & "]=" & lngID.ToString()) > 0) Then
            Return Me.OnAfterRemove(lngID)
        Else
            Call Err.Raise(vbObjectError + 1, , "No records to be deleted")
            Call clsLogFile.OnErrorWriteToLog(glogLogFile, TypeName(Me), "Remove", Err, "Dml statment:" & gconConnection.LastExecutionSQL)
        End If

    End Function

    '----------------------------------------- Flags -------------------------------------

    Protected Overridable Function OnBeforeRemove(ByVal lngID As Long) As Boolean
        Return True
    End Function

    Protected Overridable Function OnAfterRemove(ByVal lngID As Long) As Boolean
        Return True
    End Function

    Public Overridable Sub FillList(ByVal objList As Object)
        'On Error Resume Next
        Call gconConnection.FillList(objList, "Select [" & Me.TableIDFieldName & "]" _
                                     & ",[" & Me.TableNameFieldName & "]" _
                                     & " from [" & Me.TableName & "]" _
                                     & " order by [" & Me.TableNameFieldName & "]" _
                                     , Me.TableNameFieldName, Me.TableIDFieldName)
    End Sub

    ''' New
    Public Overridable Sub FillList(ByVal objList As Object, ByVal strsql As Object)
        'On Error Resume Next
        Call gconConnection.FillList(objList, strsql, Me.TableNameFieldName, Me.TableIDFieldName)
    End Sub
    Public Overridable Sub FillList(ByVal objList As Object, ByVal strsql As Object, ByVal DisMem As String, ByVal ValMem As String)
        'On Error Resume Next
        Call gconConnection.FillList(objList, strsql, DisMem, ValMem)
    End Sub
    ''' New
    Public Overridable Sub FillGrid(ByVal objList As Object, ByVal StrSql As String)
        'On Error Resume Next
        Call gconConnection.FillGrid(objList, StrSql)
    End Sub
    Public Overridable Function ExecuteNonQuery(ByVal strsql As Object, Optional ByVal intTimeOut As Integer = 15)
        'On Error Resume Next
        Return gconConnection.ExecuteNonQuery(strsql, intTimeOut)
    End Function


    Public Overridable Function ExecuteScaler(ByVal strsql As Object)
        'On Error Resume Next
        Return gconConnection.ExecuteScaler(strsql)
    End Function
    Public Overridable Function ExecuteReader(ByVal strSQL As String) As Object
        'On Error Resume Next
        Return gconConnection.ExecuteReader(strSQL)
    End Function


    Public Overridable Function ExecuteReaderToArray(ByVal strSQL As String, Optional ByVal obj As Object = Nothing, Optional ByVal colname As String = "") As Object
        'On Error Resume Next
        gconConnection.ExecuteReaderToArray(strSQL, , obj, colname)

        'Try
        '    Dim rdrdr As SqlClient.SqlDataReader = Nothing
        '    rdrdr = ExecuteReader(strSQL)
        '    If rdrdr IsNot Nothing Then
        '        If rdrdr.IsClosed = True Then
        '            rdrdr.NextResult()
        '        End If
        '        Do While rdrdr.Read()
        '            obj.Add(rdrdr.Item(colname))
        '        Loop
        '        rdrdr.Close()
        '        rdrdr = Nothing
        '    End If
        Return obj
        'Catch ex As Exception

        'End Try

    End Function

    Public Function ExecuteReaderToArrayWithOutArray(ByVal strSQL As String, Optional ByVal colname As String = "") As Object
        'On Error Resume Next
        Dim obj As New ArrayList
        Dim rdrdr As SqlClient.SqlDataReader = Nothing
        rdrdr = ExecuteReader(strSQL)
        If rdrdr IsNot Nothing Then
            Do While rdrdr.Read()
                obj.Add(rdrdr.Item(colname))
            Loop
            'There is data
            '------------------------------------------------------------
            'Copy Values to Properties
            '------------------------------------------------------------
            rdrdr.Close()
            rdrdr = Nothing
        End If
        Return obj
    End Function
    Public Overridable Function GetMaxFieldValue(ByVal strtablename As Object, ByVal strFieldName As String)
        'On Error Resume Next
        Return gconConnection.GetMaxFieldValue(strtablename, strFieldName)
    End Function
    Public Overridable Function GetMaxFieldValueLong(ByVal strtablename As Object, ByVal strFieldName As String)
        'On Error Resume Next
        Return gconConnection.GetMaxFieldValueLong(strtablename, strFieldName)
    End Function
    Public Overridable Function GetRecordCount(ByVal strsql As String)
        'On Error Resume Next
        Return gconConnection.GetRecordCount(strsql)
    End Function
    Public Overridable Function SingleQot(ByVal strString As String) As String
        'On Error Resume Next
        Return clsDBStrings.SingleQot(strString)
    End Function

    Public Overridable Function ToDBDate(ByVal datDate As Date) As String
        'On Error Resume Next
        Return clsDBStrings.ToDBDate(datDate)
    End Function
    Public Overridable Function ToDBBoolean(ByVal blnBooleean As Boolean) As String
        'On Error Resume Next
        Return clsDBStrings.ToDBBoolean(blnBooleean)
    End Function
    Public Overridable Function StoredProcedure(ByVal strsql As Object)
        'On Error Resume Next
        Return gconConnection.ExecuteNonQuery(strsql)
    End Function
    Public Overridable Function FillDS(ByVal strSQL As String)
        'On Error Resume Next
        Return gconConnection.FillDS(strSQL)
    End Function
    Public Overridable Function FillDS(ByVal strSQL As String, ByVal ds As DataSet, Optional ByVal srcTable As String = "") As DataSet
        'On Error Resume Next
        Return gconConnection.FillDS(strSQL, ds, srcTable)
    End Function
    Public Overridable Function FillDT(ByVal strSQL As String) As DataTable
        'On Error Resume Next
        Return gconConnection.FillDT(strSQL)
    End Function
    Public Overridable Function FillDT(ByVal strSQL As String, ByVal DT As DataTable) As DataTable
        'On Error Resume Next
        Return gconConnection.FillDT(strSQL, DT)
    End Function
    Public Overridable Sub FillListManual(ByVal combo As Object, ByVal ParamArray DisplayValues() As Object)
        'On Error Resume Next
        Call gconConnection.FillListManual(combo, DisplayValues)
    End Sub
End Class
