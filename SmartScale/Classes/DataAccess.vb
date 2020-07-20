Imports System.IO
Imports System.Data
Imports System.Data.DataColumn
Imports System.Data.SqlClient
Public Class ClsAdo
    Dim con As New SqlConnection
#Region "Constructors"
    Public Sub New()
        Dim app As New System.Configuration.AppSettingsReader
        Try
            If Not con.State = ConnectionState.Open Then
                con.ConnectionString = app.GetValue("con", GetType(String))
                OpeningCon()
            Else
                'con.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub New(ByVal serverName As String, ByVal database As String)
        con.ConnectionString = ("server= " & serverName & " ;database = " & database & ";integrated security= true")
    End Sub
    Public Sub New(ByVal serverName As String, ByVal database As String, ByVal userid As String, ByVal password As String)
        con.ConnectionString = ("server= " & serverName & " ;database = " & database & "; user id = " & userid & " ; password = " & password)
    End Sub
#End Region

#Region "Enumerators"
    Enum Datatype
        Characters
        Numeric
    End Enum

#End Region

#Region "Structures"
    Structure param
        Dim ParamName As String
        Dim ParamValue As String
    End Structure
#End Region

#Region "Connection"
    Sub OpeningCon()
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
       
    End Sub

    Sub ClosingCon()
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
    End Sub
#End Region

#Region "Execution In Connected Mode"
    'To get output from DB
    Function CmdExec(ByVal CommandText As String, ByVal Isstored As Boolean, _
                     ByVal parameters() As param, ByVal out As Boolean) As Integer

        Try
            Dim Cmd As New SqlCommand
            With Cmd
                .Connection = con
                If Isstored = True Then
                    .CommandType = CommandType.StoredProcedure
                Else
                    .CommandType = CommandType.Text

                End If
                .CommandText = CommandText

            End With
            If out = True Then

                Dim param As New SqlParameter
                For i As Integer = 0 To UBound(parameters)
                    If parameters(i).ParamName = "@out" Then
                        Cmd.Parameters.AddWithValue(parameters(i).ParamName, parameters(i).ParamValue).Direction = ParameterDirection.Output
                    Else
                        Cmd.Parameters.AddWithValue(parameters(i).ParamName, parameters(i).ParamValue)
                    End If
                Next
            Else
                For i As Integer = 0 To UBound(parameters)
                    Cmd.Parameters.AddWithValue(parameters(i).ParamName, parameters(i).ParamValue)
                Next
            End If
            OpeningCon()
            Dim x As Long
            CmdExec = Cmd.ExecuteNonQuery
            x = Cmd.Parameters("@out").Value
            Return x
            If CmdExec = 0 Then
                Throw New Exception("Action Failed Or No Result Of Your Statment")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            ClosingCon()
        End Try
    End Function
    'Fetching Data using DataReader Without Parameters
    Public Function CmdExecReader(ByVal CmdTxt As String) As SqlDataReader
        Return CmdExecReader(CmdTxt, False)
    End Function
    'Fetching Data using procedures and parameters

    Public Function CmdExecReader(ByVal CommandText As String, ByVal Isstored As Boolean, _
                           Optional ByVal parameters() As param = Nothing) As SqlDataReader
        Try
            Dim Cmd As New SqlCommand
            With Cmd
                .Connection = con
                If Isstored = True Then
                    .CommandType = CommandType.StoredProcedure

                    For i As Integer = 0 To UBound(parameters)
                        Cmd.Parameters.AddWithValue(parameters(i).ParamName, parameters(i).ParamValue)
                    Next
                Else
                    .CommandType = CommandType.Text

                End If
                .CommandText = CommandText

            End With
            ' 16/12/2019
            ClosingCon()
            OpeningCon()


            Return Cmd.ExecuteReader

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function FillDataTable(ByVal SQL As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand
            Dim da As New SqlDataAdapter
            With cmd
                .Connection = con
                .CommandType = CommandType.Text
                .CommandText = SQL

            End With
            OpeningCon()
            da.SelectCommand = cmd
            da.Fill(dt)
            ClosingCon()
            Return dt

        Catch ex As Exception

        End Try
    End Function


    Public Function FillDT(ByVal CmdTxt As String) As DataTable
        Return FillDT(CmdTxt, False)
    End Function
    'Fetching Data using procedures and parameters
    Public Function FillDT(ByVal CommandText As String, ByVal Isstored As Boolean, _
                                               Optional ByVal parameters() As param = Nothing) As DataTable
        Try
            Dim Cmd As New SqlCommand
            With Cmd
                .Connection = con
                If Isstored = True Then
                    .CommandType = CommandType.StoredProcedure

                    For i As Integer = 0 To UBound(parameters)
                        Cmd.Parameters.AddWithValue(parameters(i).ParamName, parameters(i).ParamValue)
                    Next
                Else
                    .CommandType = CommandType.Text

                End If
                .CommandText = CommandText

            End With

            Dim DT As New DataTable

            DT.Columns.Add("Shift_ID")
            DT.Columns.Add("Shift_Start_Date")
            DT.Columns.Add("Shift_End_Date")


            OpeningCon()


            Dim DR As SqlDataReader = Cmd.ExecuteReader()
            Do While DR.Read

                DT.Rows.Add(DR.GetValue(0), DR.GetValue(1), DR.GetValue(2))

            Loop
            DR.Close()
            ClosingCon()
            Return DT
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function FillDTGeneral(ByVal CommandText As String) As DataTable
        Try
            Dim Cmd As New SqlCommand
            With Cmd
                .Connection = con

                .CommandText = CommandText

            End With

            Dim DT As New DataTable

            DT.Columns.Add("ControlID")
            DT.Columns.Add("ControlCaption")
            OpeningCon()


            Dim DR As SqlDataReader = Cmd.ExecuteReader()
            Do While DR.Read

                DT.Rows.Add(DR.GetValue(0), DR.GetValue(1))

            Loop
            DR.Close()
            ClosingCon()
            Return DT
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function FillDTGeneral2(ByVal CommandText As String) As DataTable
        Try
            Dim Cmd As New SqlCommand
            With Cmd
                .Connection = con

                .CommandText = CommandText

            End With

            Dim DT As New DataTable

            DT.Columns.Add("ControlID")
            DT.Columns.Add("ControlCaption")
            DT.Columns.Add("ParentID")
            DT.Columns.Add("ControlName")

            OpeningCon()


            Dim DR As SqlDataReader = Cmd.ExecuteReader()
            Do While DR.Read

                DT.Rows.Add(DR.GetValue(0), DR.GetValue(1), DR.GetValue(2), DR.GetValue(3))

            Loop
            DR.Close()
            ClosingCon()
            Return DT
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function FillDTGeneral3(ByVal CommandText As String) As DataTable
        Try
            Dim Cmd As New SqlCommand
            With Cmd
                .Connection = con

                .CommandText = CommandText

            End With

            Dim DT As New DataTable

            DT.Columns.Add("Controls_RolesID")
            DT.Columns.Add("ControlCaption")
            DT.Columns.Add("ParentID")
            DT.Columns.Add("ControlName")

            OpeningCon()


            Dim DR As SqlDataReader = Cmd.ExecuteReader()
            Do While DR.Read

                DT.Rows.Add(DR.GetValue(0), DR.GetValue(1), DR.GetValue(2), DR.GetValue(3))

            Loop
            DR.Close()
            ClosingCon()
            Return DT
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function




    Public Function FillDTWith_TblShift(ByVal CommandText As String, ByVal Isstored As Boolean, _
                                               Optional ByVal parameters() As param = Nothing) As DataTable
        Try
            Dim Cmd As New SqlCommand
            With Cmd
                .Connection = con
                If Isstored = True Then
                    .CommandType = CommandType.StoredProcedure

                    For i As Integer = 0 To UBound(parameters)
                        Cmd.Parameters.AddWithValue(parameters(i).ParamName, parameters(i).ParamValue)
                    Next
                Else
                    .CommandType = CommandType.Text

                End If
                .CommandText = CommandText

            End With

            Dim DT As New DataTable
            DT.Columns.Add("Shift_Name")
            DT.Columns.Add("Shift_Start_Time")
            DT.Columns.Add("Shift_Start_Date")
            DT.Columns.Add("Shift_End_Date")

            OpeningCon()


            Dim DR As SqlDataReader = Cmd.ExecuteReader()
            Do While DR.Read

                DT.Rows.Add(DR.GetValue(0), DR.GetValue(1), DR.GetValue(2), DR.GetValue(3))

            Loop
            DR.Close()
            ClosingCon()
            Return DT
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function GetTransactionDT(ByVal TransID As Integer) As DataTable
        Try
            Dim Cmd As New SqlCommand
            With Cmd
                .Connection = con

                .CommandType = CommandType.Text

                .CommandText = ("select Driver_ID,Car_Info_ID,Issue_To_ID,Slip_Rate,Good_ID, " _
                                & "Dealer_ID,First_Weight_IsEmpty,In_Date,First_Weigth,Second_Weight from tblTransactions where First_Weigth_Scale_ID=" & cfrmMain.ScaleNo & " and TR_TY=2 and CID=" & TransID)

            End With

            Dim DT As New DataTable

            DT.Columns.Add("Driver_ID")
            DT.Columns.Add("Car_Info_ID")
            DT.Columns.Add("Issue_TO_ID")
            DT.Columns.Add("Slip_Rate")
            DT.Columns.Add("Good_ID")
            DT.Columns.Add("Dealer_ID")
            DT.Columns.Add("First_Weight_IsEmpty")
            DT.Columns.Add("In_Date")
            DT.Columns.Add("First_Weigth")
            DT.Columns.Add("Second_Weight")
            OpeningCon()


            Dim DR As SqlDataReader = Cmd.ExecuteReader()
            Do While DR.Read

                DT.Rows.Add(DR.GetValue(0), DR.GetValue(1), DR.GetValue(2), DR.GetValue(3), _
                            DR.GetValue(4), DR.GetValue(5), DR.GetValue(6), DR.GetValue(7), DR.GetValue(8), DR.GetValue(9))

            Loop
            DR.Close()
            ClosingCon()
            Return DT
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    'Fetching Scalar Value Without Parameters

    Public Function CmdExecScalar(ByVal CmdTxt As String) As Object
        Return CmdExecScalar(CmdTxt, False)
    End Function

    'Fetching Scalar Value Without Parameters
    Public Function CmdExecScalar(ByVal CommandText As String, ByVal Isstored As Boolean, _
                           Optional ByVal parameters() As param = Nothing) As Object
        Try
            Dim Cmd As New SqlCommand
            With Cmd
                .Connection = con
                If Isstored = True Then
                    .CommandType = CommandType.StoredProcedure

                    For i As Integer = 0 To UBound(parameters)
                        Cmd.Parameters.AddWithValue(parameters(i).ParamName, parameters(i).ParamValue)
                    Next
                Else
                    .CommandType = CommandType.Text
                End If
                .CommandText = CommandText

            End With
            ClosingCon()
            OpeningCon()
            CmdExecScalar = Cmd.ExecuteScalar
            Return CmdExecScalar
        Catch ex As Exception
            Throw New Exception(ex.Message)
            ClosingCon()
        Finally
            ClosingCon()
        End Try
    End Function

    Public Function IFExists(ByVal CommandText As String) As Boolean
        Try
            Dim Cmd As New SqlCommand
            With Cmd
                .Connection = con
                .CommandType = CommandType.Text
                .CommandText = "set DateFormat dmy If EXists(" + CommandText + ") select 1 else select 0 "


            End With
            ClosingCon()
            OpeningCon()
            Dim Result As String = Cmd.ExecuteScalar()
            If (Result = "1") Then
                Return True
            Else : Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
            ClosingCon()
            Return False
        Finally
            ClosingCon()
        End Try
    End Function

    Function CmdExec(ByVal CommandText As String) As Integer
        Try
            Dim Cmd As New SqlCommand
            With Cmd
                .Connection = con
                .CommandType = CommandType.Text
                .CommandText = CommandText
            End With
            OpeningCon()
            CmdExec = Cmd.ExecuteNonQuery
            If CmdExec = 0 Then
                Try
                    Dim FILE_NAME As String = Application.StartupPath & "\ErrorFailed.txt"
                    If System.IO.File.Exists(FILE_NAME) = True Then
                        Dim objWriter As StreamWriter
                        objWriter = File.AppendText(FILE_NAME)
                        objWriter.WriteLine(vbCrLf & " The SQL Query1 = " & CommandText)
                        objWriter.Close()
                    End If
                Catch ex As Exception
                End Try
                Throw New Exception(" Action Failed Or No Result Of Your Statment")
                Return 0
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            ClosingCon()
        End Try
    End Function

    Function CmdExec(ByVal CommandText As String, ByVal Isstored As Boolean, ByVal parameters() As param) As Integer
        Try
            Dim Cmd As New SqlCommand
            With Cmd
                .Connection = con
                If Isstored = True Then
                    .CommandType = CommandType.StoredProcedure
                Else
                    .CommandType = CommandType.Text

                End If
                .CommandText = CommandText

            End With
            For i As Integer = 0 To UBound(parameters)
                Cmd.Parameters.AddWithValue(parameters(i).ParamName, parameters(i).ParamValue)
            Next
            OpeningCon()
            CmdExec = Cmd.ExecuteNonQuery
            If CmdExec = 0 Then
                Try
                    Dim FILE_NAME As String = Application.StartupPath & "\ErrorFailed.txt"
                    If System.IO.File.Exists(FILE_NAME) = True Then
                        Dim objWriter As StreamWriter
                        objWriter = File.AppendText(FILE_NAME)
                        objWriter.WriteLine(vbCrLf & " The SQL Query2 = " & CommandText)
                        objWriter.Close()
                    End If
                Catch ex As Exception
                End Try
                Throw New Exception("Action Failed Or No Result Of Your Statment")

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            'ClosingCon()

        End Try

    End Function

    Function CmdExec(ByVal CommandText As String, ByVal Isstored As Boolean) As Integer
        Try
            Dim Cmd As New SqlCommand
            With Cmd
                .Connection = con
                If Isstored = True Then
                    .CommandType = CommandType.StoredProcedure
                Else
                    .CommandType = CommandType.Text

                End If
                .CommandText = CommandText

            End With

            OpeningCon()
            CmdExec = Cmd.ExecuteNonQuery
            If CmdExec = 0 Then
                Try
                    Dim FILE_NAME As String = Application.StartupPath & "\ErrorFailed.txt"
                    If System.IO.File.Exists(FILE_NAME) = True Then
                        Dim objWriter As StreamWriter
                        objWriter = File.AppendText(FILE_NAME)
                        objWriter.WriteLine(vbCrLf & " The SQL Query3 = " & CommandText)
                        objWriter.Close()
                    End If
                Catch ex As Exception
                End Try

                Throw New Exception("Action Failed Or No Result Of Your Statment")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            ClosingCon()

        End Try

    End Function

    Public Function GetMax(ByVal ColumnID As String, ByVal tbl_Name As String) As Integer
        If IsDBNull(CmdExecScalar(" Select Max(" & ColumnID & ") From " & tbl_Name, False)) Then
        Else
            Dim Maxint As Integer = CmdExecScalar(" Select Max(" & ColumnID & ") From " & tbl_Name, False)
            Return Maxint
        End If

    End Function

    Public Function GetCount(ByVal ColumnName As String, ByVal TblName As String, ByVal ConditionVal As Object, _
                             Optional ByVal ValueType As Datatype = Datatype.Characters) As Integer
        Dim Value As String = ""

        Select Case ValueType
            Case Datatype.Characters
                Value = "'" & CStr(ConditionVal) & "'"
            Case Datatype.Numeric
                Value = CStr(ConditionVal)
        End Select

        Dim intCount As Integer = CmdExecScalar("Select Count(" & ColumnName & ") From " & TblName & " Where " & _
                                                ColumnName & " = " & Value, False)
        Return intCount
    End Function
    Public Function GetCount2(ByVal TblName As String, ByVal ColumnName1 As String, ByVal ConditionVal1 As Object, ByVal ColumnName2 As String, ByVal ConditionVal2 As Object, _
                          Optional ByVal ValueType1 As Datatype = Datatype.Characters, Optional ByVal ValueType2 As Datatype = Datatype.Characters) As Integer
        Dim Value1 As String = ""
        Dim Value2 As String = ""

        Select Case ValueType1
            Case Datatype.Characters
                Value1 = "'" & CStr(ConditionVal1) & "'"
            Case Datatype.Numeric
                Value1 = CStr(ConditionVal1)
        End Select

        Select Case ValueType2
            Case Datatype.Characters
                Value2 = "'" & CStr(ConditionVal2) & "'"
            Case Datatype.Numeric
                Value2 = CStr(ConditionVal2)
        End Select

        Dim intCount As Integer = CmdExecScalar("Select Count(*) From " & TblName & " Where " & _
                                                ColumnName1 & " = " & Value1 & " AND " & ColumnName2 & " = " & Value2, False)
        Return intCount
    End Function

    Public Function GetColumnID(ByVal ColumnID As String, ByVal tblName As String, ByVal ConditionCol As String, _
                                ByVal ConditionVal As Object, _
                                Optional ByVal ValueType As Datatype = Datatype.Characters) As String
        Select Case ValueType
            Case Datatype.Characters
                ConditionVal = "'" & CStr(ConditionVal) & "'"
            Case Datatype.Numeric
                ConditionVal = CStr(ConditionVal)
        End Select
        Dim lngID As String = CmdExecScalar("Select " & ColumnID & " From " & tblName & " Where " & _
                                           ConditionCol & " = " & ConditionVal, False)

        Return lngID
    End Function
#End Region

#Region "Execution In Disconnected Mode"
    Function SelectData(ByVal SqlCommand As String, ByVal UseTable As Boolean, ByVal TableName As String) As DataSet
        Try
            Dim Adap As New SqlDataAdapter(SqlCommand, con)
            Dim DS As New DataSet
            If UseTable = True Then
                Adap.Fill(DS, TableName)
            Else
                Adap.Fill(DS)
            End If

            Return DS
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Function SelectData(ByVal SqlCommand As String, ByVal IsStoredProsedure As Boolean, ByVal parameters() As param) As DataSet
        Try
            Dim Adap As New SqlDataAdapter(SqlCommand, con)
            Dim DS As New DataSet
            If IsStoredProsedure = True Then
                Adap.SelectCommand.CommandType = CommandType.StoredProcedure
            Else
                Adap.SelectCommand.CommandType = CommandType.Text
            End If

            For i As Short = 0 To UBound(parameters)
                Adap.SelectCommand.Parameters.AddWithValue(parameters(i).ParamName, parameters(i).ParamValue)
            Next
            Adap.Fill(DS)
            Return DS

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Function SelectData(ByVal SqlCommand As String, ByVal IsStoredProsedure As Boolean) As DataSet
        Try
            Dim Adap As New SqlDataAdapter(SqlCommand, con)
            Dim DS As New DataSet
            If IsStoredProsedure = True Then
                Adap.SelectCommand.CommandType = CommandType.StoredProcedure
            Else
                Adap.SelectCommand.CommandType = CommandType.Text
            End If

            Adap.Fill(DS)
            Return DS

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Function ToDecimal(ByVal SrtVal As String) As Decimal
        Return Decimal.Parse(If(SrtVal.Trim() = "", "0", SrtVal.Trim()))

    End Function

    Public Function UpdateData(ByVal SqlStatement As String, ByVal DS As DataSet) As Boolean
        Try
            Dim Adap As New SqlDataAdapter(SqlStatement, con)
            Dim BuildCmd As New SqlCommandBuilder(Adap)
            Adap.Update(DS)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function WriteXml(ByVal FilePath As String, ByVal DS As DataSet) As Boolean
        Try
            DS.WriteXml(FilePath)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ReadXml(ByVal FilePath As String) As DataSet
        Dim DS As New DataSet
        DS.ReadXml(FilePath)
        Return DS
    End Function

#End Region

#Region "ComboBox Manipulation"

    Public Function CtrlItemsLoad(ByVal ID_Field As String, ByVal Name_Field As String, ByVal Tbl_Name As String, _
                             ByVal TargetCtrl As Object, Optional ByVal JoinCondition As String = "", _
                             Optional ByVal WhereCondition As String = "", _
                             Optional ByVal SelectLastItem As Boolean = False, _
                              Optional ByVal SelectFirstItem As Boolean = False, _
                             Optional ByVal AddNewItem_InTheFirst As String = "") As Object
        Try
            ' 16/12/2019

            Dim TSqlStatement As String = "set dateformat dmy Select " & ID_Field & "," & Name_Field & " From " & Tbl_Name
            If JoinCondition <> "" Then
                TSqlStatement &= " " & JoinCondition & ""
            End If
            If WhereCondition <> "" Then
                TSqlStatement &= " Where " & WhereCondition & ""
            End If

            Dim DR As SqlDataReader = CmdExecReader(TSqlStatement)
            Dim DT As New DataTable
            DT.Columns.Add("ID")
            DT.Columns.Add("Name")
            If AddNewItem_InTheFirst <> "" Then
                DT.Rows.Add(0, AddNewItem_InTheFirst)
            End If

            Do While DR.Read
                DT.Rows.Add(DR.GetValue(0), DR.GetValue(1))
            Loop

            DR.Close()
            ClosingCon()
            'Dim x As Integer
            'x = DT.Rows(0)(0).ToString
            'TargetCtrl = Nothing
            'MsgBox(DT.Columns(0).ToString)
            TargetCtrl.ValueMember = DT.Columns(0).ToString
            TargetCtrl.DisplayMember = DT.Columns(1).ToString

            'MsgBox(Tbl_Name)

            TargetCtrl.DataSource = DT
            If SelectFirstItem = True Then
                TargetCtrl.SelectedIndex = 0
            End If
            If SelectLastItem = True Then
                TargetCtrl.SelectedIndex = TargetCtrl.Items.Count - 1
            End If
            Return TargetCtrl
        Catch ex As Exception

        End Try
    End Function

    Function GetSlipRates(ByVal TargetCtrl As Object, ByVal TSqlStatement As String, ByVal ToolTip1 As ToolTip) As Object

        Dim DR As SqlDataReader = CmdExecReader(TSqlStatement)
        Dim DT As New DataTable

        DT.Columns.Add("ID")
        DT.Columns.Add("Name")
        Dim Max As Decimal
        Dim Rate As Decimal
        Dim Min As Decimal

        Do While DR.Read
            Max = DR.GetValue(0)
            Rate = DR.GetValue(1)
            Min = DR.GetValue(2)
        Loop

        DT.Rows.Add(0, Max)
        DT.Rows.Add(0, Rate)
        DT.Rows.Add(0, Min)
        DT.Rows.Add(0, "رجوع")
        DR.Close()
        ClosingCon()

        TargetCtrl.ValueMember = DT.Columns(0).ToString
        TargetCtrl.DisplayMember = DT.Columns(1).ToString
        TargetCtrl.DataSource = DT

        ToolTip1.SetToolTip(TargetCtrl, "أعلى سعر =  " & Max & ", السعر العادى =  " & Rate & ", أقل سعر =  " & Min)

    End Function

    ''' <summary>
    ''' This sub load control depend on select from combobox 
    ''' </summary>
    ''' <param name="Field_Name">coulmn contain value will be loaded in control</param>
    ''' <param name="tbl_Name">table name that contain Filed_Name </param>
    ''' <param name="cbo">ComboBox that contain value will use to load control </param>
    ''' <param name="Where_Field_Code">column name that contain ComboBox value member </param>
    ''' <param name="TargetCtrl"> Traget control will be dispalyed target value </param>
    ''' <remarks></remarks>
    ''' 

    Sub checkExist(ByVal Field_Name As String, ByVal tbl_Name As String, ByVal cbo As ComboBox, _
                   ByVal Where_Field_Code As String, ByVal TargetCtrl As Control)
        If cbo.Items.Count = 0 Then Exit Sub
        Dim Result As Object
        Dim str As String = "select " & Field_Name & " from " & tbl_Name & " where " & _
                             Where_Field_Code & "=" & cbo.SelectedValue & ""
        Result = CmdExecScalar(str, False)
        If Not Result Is Nothing Then
            If TypeOf TargetCtrl Is TextBox Then
                CType(TargetCtrl, TextBox).ReadOnly = True
                TargetCtrl.Text = ""
                TargetCtrl.Text = CStr(Result)
            ElseIf TypeOf TargetCtrl Is ComboBox Then
                CType(TargetCtrl, ComboBox).Text = ""
                CType(TargetCtrl, ComboBox).SelectedText = CStr(Result)
            End If
        Else
            If TypeOf TargetCtrl Is TextBox Then
                CType(TargetCtrl, TextBox).ReadOnly = False
                TargetCtrl.Text = ""
            ElseIf TypeOf TargetCtrl Is ComboBox Then
                CType(TargetCtrl, ComboBox).Enabled = True
                CType(TargetCtrl, ComboBox).Text = ""
            End If
        End If
    End Sub

    Sub checkExist(ByVal Field_Name As String, ByVal tbl_Name As String, ByVal cbo As ComboBox, _
                   ByVal FK_field_Name As String, ByVal FK_tbl_Name As String, _
                   ByVal Where_Field_Code As String, ByVal TargetCtrl As Control, _
                   ByVal Where_FK_Field_Name As String)
        Dim Result As Object
        Dim str As String = "select " & Field_Name & " from " & tbl_Name & " where " & Where_Field_Code & "=(select " & _
        FK_field_Name & " from " & FK_tbl_Name & " where " & Where_FK_Field_Name & " = " & Val(cbo.SelectedValue) & ")"

        Result = CmdExecScalar(str, False)
        If Not Result Is Nothing Then
            If TypeOf TargetCtrl Is TextBox Then
                TargetCtrl.Text = ""
                TargetCtrl.Text = CStr(Result)
            ElseIf TypeOf TargetCtrl Is ComboBox Then
                CType(TargetCtrl, ComboBox).Text = ""
                CType(TargetCtrl, ComboBox).SelectedText = CStr(Result)
            End If
        Else
            If TypeOf TargetCtrl Is TextBox Then
                CType(TargetCtrl, TextBox).ReadOnly = False
                TargetCtrl.Text = ""
            ElseIf TypeOf TargetCtrl Is ComboBox Then
                CType(TargetCtrl, ComboBox).Enabled = True
                CType(TargetCtrl, ComboBox).Text = ""
            End If
        End If
    End Sub

    Sub checkExist(ByVal Field_Name As String, ByVal tbl_Name As String, ByVal ctr As Control, ByVal FK_field_Name As String, _
                   ByVal FK_tbl_Name As String, ByVal Where_Field_Code As String, ByVal TargetCtrl As Control, ByVal Where_FK_Field_Name As String)
        Dim str As String = ""
        If TypeOf ctr Is ComboBox Then
            str = "select " & Field_Name & " from " & tbl_Name & " where " & Where_Field_Code & "=(select " & FK_field_Name & " from " & FK_tbl_Name & " where " & Where_FK_Field_Name & " = " & CType(ctr, ComboBox).SelectedValue & ")"
        ElseIf TypeOf ctr Is TextBox Then
            str = "select " & Field_Name & " from " & tbl_Name & " where " & Where_Field_Code & "=(select " & FK_field_Name & " from " & FK_tbl_Name & " where " & Where_FK_Field_Name & " = " & CType(ctr, TextBox).Text & ")"
        End If
        Dim dr As SqlDataReader = CmdExecReader(str, False)
        If dr.Read() Then
            str = dr.GetValue(0)
        End If
        If dr.HasRows = True Then
            dr.Close()
            ClosingCon()
            If TypeOf TargetCtrl Is TextBox Then
                CType(TargetCtrl, TextBox).ReadOnly = True
                TargetCtrl.Text = str
            ElseIf TypeOf TargetCtrl Is ComboBox Then
                CType(TargetCtrl, ComboBox).Text = ""
                CType(TargetCtrl, ComboBox).SelectedText = str
            End If
        Else
            If TypeOf TargetCtrl Is TextBox Then
                CType(TargetCtrl, TextBox).ReadOnly = False
                TargetCtrl.Text = ""
            ElseIf TypeOf TargetCtrl Is ComboBox Then
                CType(TargetCtrl, ComboBox).Enabled = True
                CType(TargetCtrl, ComboBox).Text = ""
            End If
            dr.Close()
            ClosingCon()
        End If
    End Sub

#End Region

End Class

Public Class clsMainMethods

#Region "Enumerators"
    Enum FrmHeight
        Original
        ControlHeight
    End Enum

    Enum FrmMode
        FrmMain
        FrmAddNew
    End Enum
#End Region

#Region "Main Methods"

    Public Function FrmResize(ByVal OriginalHeight As Short, ByVal CtrlHeight As Short, _
                              ByVal ResizeTo As FrmHeight) As Short

        Dim FrmHeight As Short = 0
        Select Case ResizeTo
            Case clsMainMethods.FrmHeight.Original
                FrmHeight = OriginalHeight
            Case clsMainMethods.FrmHeight.ControlHeight
                FrmHeight = OriginalHeight - CtrlHeight
        End Select

        Return FrmHeight
    End Function

    Public Function BinaryPicLoad(ByVal PicClmn As String, ByVal PicTbl As String, ByVal PicCondition As String, _
                         ByVal PictureBox As PictureBox) As Boolean
        Dim imagedata() As Byte
        Dim imageBytedata As MemoryStream

        imagedata = gAdo.CmdExecScalar("SELECT " & PicClmn & " FROM " & PicTbl & " WHERE " & PicCondition, _
                                  False, Nothing)
        If Not imagedata Is Nothing Then
            imageBytedata = New MemoryStream(imagedata)
            PictureBox.Image = Image.FromStream(imageBytedata)
            Return True
        Else
            Return False
        End If
    End Function

    Function BinaryPicStore(ByVal PicPath As String, ByVal PictureBox As PictureBox)

        If File.Exists(PicPath) = True Then
            PictureBox.Load(PicPath)
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage
            'TO Upload the Image to SQL
            Dim fs As New FileStream(Trim(PicPath), FileMode.Open)
            Dim Data() As Byte = New [Byte](fs.Length) {}
            fs.Read(Data, 0, fs.Length)

            Dim ConnPath, ConnString As String
            ConnPath = Application.StartupPath & "\ConnString.txt"
            ConnString = GetConString(ConnPath)

            Dim con As New System.Data.SqlClient.SqlConnection(ConnString)

            con.Open()
            Dim cmd As New System.Data.SqlClient.SqlCommand("insert into tblpicture values (@Photo)", con)
            cmd.Parameters.Add("@Photo", Data)
            cmd.ExecuteNonQuery()
            con.Close()
            fs.Close()

            Return gAdo.GetMax("Pic_ID", "tblPicture")

        End If

    End Function

    Public Sub ClearObj(ByVal Frm As Form, ByVal CtrlType As String)
        Dim Ctrl As New Control
        Dim SubCtrl As New Control
        For Each Ctrl In Frm.Controls
            If Ctrl.GetType.ToString = "System.Windows.Forms." & CtrlType Then
                Ctrl.Text = ""
            Else
                If Ctrl.GetType.ToString = "System.Windows.Forms.Panel" _
                    Or Ctrl.GetType.ToString = "System.Windows.Forms.GroupBox" _
                    Or Ctrl.GetType.ToString = "System.Windows.Forms.TabControl" Then

                    For Each SubCtrl In Ctrl.Controls
                        If SubCtrl.GetType.ToString = "System.Windows.Forms." & CtrlType Then
                            SubCtrl.Text = ""
                            If (CtrlType = "ComboBox") Then
                                DirectCast(SubCtrl, ComboBox).SelectedIndex = -1
                            End If

                        End If
                    Next
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Automatically Check The Second Specified RadioButton When The First Specified RadioButton Checked
    ''' </summary>
    ''' <param name="FrstRad">RadioButton Checked By User</param>
    ''' <param name="ScndRad">Target RadioButton That Automatically Checked When First RadioButton Checked</param>
    ''' <remarks></remarks>
    Public Sub RadioChecked(ByVal FrstRad As RadioButton, ByVal ScndRad As RadioButton)
        Dim container() As Object = {FrstRad.Parent, ScndRad.Parent}
        If FrstRad.Checked = True Then
            ScndRad.Checked = True

            For i As Short = 0 To UBound(container)
                For Each Ctrl As Object In container(i).controls
                    If TypeOf Ctrl Is RadioButton Then
                        If Ctrl.Name <> FrstRad.Name And Ctrl.Name <> ScndRad.Name Then
                            Ctrl.checked = False
                        End If
                    End If
                Next
            Next
        End If
    End Sub

    ''' <summary>
    ''' Show The Specified Message Text For predifend Specific Time
    ''' </summary>
    ''' <param name="StartVal">Starting Value For Needed Time</param>
    ''' <param name="EndVal">End Value For Needed Time</param>
    ''' <param name="IncrVal">Amount Of Increased Time [Defining Speed Of Message Show]</param>
    ''' <param name="MsgCtrl">The Control To Hold The Specified Message In Its Text Property</param>
    ''' <param name="Msg">The Message To Show</param>
    ''' <param name="Tmr">Timer Control</param>
    ''' <param name="MsgRemove">Define Whether The Message Is Temporary Or permanent</param>
    ''' <remarks></remarks>
    Public Sub InstantMsg(ByVal StartVal As Short, ByVal EndVal As Short, ByVal IncrVal As Short, _
                          ByVal MsgCtrl As Object, ByVal Msg As String, ByVal Tmr As Timer, _
                          Optional ByVal MsgRemove As Boolean = True)
        If MsgRemove = True Then
            If StartVal >= EndVal Then Exit Sub
            Static i As Short = StartVal
            If i < EndVal Then
                i += IncrVal
                MsgCtrl.text = Msg
            Else
                Tmr.Enabled = False
                MsgCtrl.text = ""
            End If
        Else
            MsgCtrl.text = Msg
            Tmr.Enabled = False
        End If
    End Sub

    Public Function GetDateStatus(ByVal carInfoID As Integer, ByVal transIDCtrl As Control, ByVal InDateCtrl As Control, _
                                  ByVal OutDateCtrl As Control) As Boolean
        gTransID = gAdo.GetMax("Trans_ID", "tblTransactions") + 1
        Dim dr As SqlDataReader = gAdo.CmdExecReader("Select In_Date ,Out_Date , Trans_ID From tblTransactions Where Car_Info_ID =" & _
                                                carInfoID, False)

        Do While dr.Read
            If IsDBNull(dr.GetValue(0)) = False And IsDBNull(dr.GetValue(1)) = True Then
                transIDCtrl.Text = dr.GetValue(2)
                InDateCtrl.Text = dr.GetValue(0)
                OutDateCtrl.Text = "00/00/00 00:00:00"
                InDateCtrl.Enabled = True
                OutDateCtrl.Enabled = False
                dr.Close()
                Return True
            ElseIf IsDBNull(dr.GetValue(0)) = True And IsDBNull(dr.GetValue(1)) = False Then
                transIDCtrl.Text = CStr(gTransID)
                OutDateCtrl.Text = dr.GetValue(1)
                InDateCtrl.Enabled = False
                OutDateCtrl.Enabled = True
                dr.Close()
                Return False
            ElseIf IsDBNull(dr.GetValue(0)) = False And IsDBNull(dr.GetValue(1)) = False Then
                transIDCtrl.Text = CStr(gTransID)
                InDateCtrl.Text = dr.GetValue(0)
                OutDateCtrl.Text = dr.GetValue(1)
                InDateCtrl.Enabled = True
                OutDateCtrl.Enabled = True
                dr.Close()
                Return False
            ElseIf IsDBNull(dr.GetValue(0)) = True And IsDBNull(dr.GetValue(1)) = True Then
                transIDCtrl.Text = CStr(gTransID)
                InDateCtrl.Enabled = False
                OutDateCtrl.Enabled = False
                dr.Close()
                Return False
            End If
        Loop
    End Function

    Public Function GetSlip(ByVal Weight As Decimal) As Decimal
        Dim RESULT As Decimal = gAdo.CmdExecScalar("SELECT  [Slip_Rate] FROM [tblSlip]Where [Slip_IsDeleted] =0 and " & (Weight / 1000) & " Between  [Slip_MinRange] and [Slip_MaxRange]", False)

        Return RESULT
    End Function




#Region "Shift"

    ''' <summary>
    ''' if KnowShift_Date=false ------  Get Shift_Name and Shift_Start_Time 
    ''' if KnowShift_Date=True  ------ Get Date( From and To) to use it in DataGridview1 Bind when Press BtnSwitchView
    ''' </summary>
    ''' <param name="KnowShift_Date"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 

    Public Function GetShifName_And_SearchDate(ByVal KnowShift_Date As Boolean) As Object

        Dim NowTime As String = Mid(Now, 12, 11)
        Dim AMorPM As String
        AMorPM = NowTime.Substring(NowTime.LastIndexOf(" ") + 1)
        If NowTime.Contains("م") Then
            NowTime = NowTime.Replace("م", "PM")
        Else
            NowTime = NowTime.Replace("ص", "AM")
        End If
        'NowTime = NowTime.Substring(0, NowTime.LastIndexOf(" "))
        Dim Shift_Name As String = ""
        Dim Shift_Start_Time As DateTime
        Dim Shift_Start_date, Shift_End_date As Boolean
        Dim DT As New DataTable



        Dim DtFirstShift As New DataTable
        Dim DtlastShift As New DataTable
        Dim SearchFrom_Date, SearchTo_Date As String
        Dim Reset_date As DateTime
        Dim Ar(1) As String

        Dim DR As SqlClient.SqlDataReader = gAdo.CmdExecReader("set dateformat dmy select Shift_Name,Shift_Start_time " _
                                                               & " from tblShift " _
                                                               & "where CONVERT(datetime, Shift_Start_Time) < '01/01/1900 " & NowTime _
                                                               & "' and CONVERT(datetime, Shift_End_Time) >'01/01/1900 " & NowTime & "'and Shift_IsDeleted='false'")


        'Fetching To know if where are in the Same Day Or in The Next Day
        'Here We are in the next day So SQl Statment return Null
        If DR.HasRows = False Then
            DR.Close()
            gAdo.ClosingCon()
            'Here we See The Next Day in SQl Statment to Return Value
            DT = gAdo.FillDTWith_TblShift("set dateformat dmy select Shift_Name ,Shift_Start_time,  " _
                                               & " Shift_Start_date,Shift_End_date from tblShift " _
                                               & "where CONVERT(datetime, Shift_Start_Time) < '02/01/1900 " & NowTime _
                                               & "' and CONVERT(datetime, Shift_End_Time) >'02/01/1900 " & NowTime & "'and Shift_IsDeleted='false'", False)

            Shift_Name = DT.Rows(0).Item(0).ToString
            Shift_Start_Time = Mid(DT.Rows(0).Item(1).ToString, 12, 11)
            Shift_Start_date = DT.Rows(0).Item(2).ToString
            Shift_End_date = DT.Rows(0).Item(3).ToString
        Else
            'Here We are in the Same day So SQl Statment return Value
            Do While DR.Read
                Shift_Name = DR.GetValue(0)
                Shift_Start_Time = CDate(DR.GetValue(1)).ToShortTimeString
                'Shift_Start_date = DR.GetValue(2)
                'Shift_End_date = DR.GetValue(3)
            Loop
            DR.Close()
            gAdo.ClosingCon()

        End If
        'to Put The Shift_Name IN Label
        If KnowShift_Date = False Then
            'Here we don't want to return the Shift_Start_date
            'We return Shift_Name with the Shift_Start_Time
            If Shift_Start_Time.ToShortTimeString.EndsWith("PM") Then
                Return Shift_Name & " " & Shift_Start_Time.ToShortTimeString.Replace("PM", " م ")
            Else
                Return Shift_Name & " " & Shift_Start_Time.ToShortTimeString.Replace("AM", " ص ")

            End If

        Else
            '#################################((( Get two Values Search_From & Search_to )))################################
            'To Get Date( From & To) to use it in DataGridview1 Bind when Press BtnSwitchView

            'Get Main Day (From_Date) by return the Now date to its Main Value (it's Mean that we see the Main day is today or yesterday)
            If Shift_Start_date = False And Shift_End_date = False Then
                'the Same day (Today)
                Reset_date = Now.Date
            ElseIf Shift_Start_date = True And Shift_End_date = True Then
                'the day Before (Yesterday)
                Reset_date = Now.AddDays(-1).Date
            ElseIf Shift_Start_date = False And Shift_End_date = True Then
                Dim NightOrDay As String = Mid(Now, 21, 2)
                If NightOrDay = "ص" Then
                    'and from 12:00:00 Am to Shift_End_Date  The day Before (yesterday)
                    Reset_date = Now.AddDays(-1).Date
                Else
                    'from Shift_Start_Date to 11:59:59 Pm the same day (Today)
                    Reset_date = Now.Date
                End If
            End If


            DtFirstShift.Columns.Add("Shift_Start_Date")
            DtFirstShift.Columns.Add("Shift_Start_Time")
            DtlastShift.Columns.Add("Shift_End_Date")
            DtlastShift.Columns.Add("Shift_End_Time")

            'Get  Shift_Start_Date and Shift_Start_Time for the first Shift
            Dim DRFirstShift As SqlClient.SqlDataReader = gAdo.CmdExecReader("select Shift_Start_Date,Shift_Start_Time from dbo.tblShift where Shift_IsFirst = 1")
            Do While DRFirstShift.Read
                DtFirstShift.Rows.Add(DRFirstShift.GetValue(0), DRFirstShift.GetValue(1))
            Loop
            DRFirstShift.Close()
            gAdo.ClosingCon()

            'Get  Shift_Start_Date and Shift_Start_Time for the Last Shift
            Dim DRLastshift As SqlClient.SqlDataReader = gAdo.CmdExecReader("select Shift_End_Date,Shift_End_Time from dbo.tblShift where Shift_IsFirst=10")
            Do While DRLastshift.Read
                DtlastShift.Rows.Add(DRLastshift.GetValue(0), DRLastshift.GetValue(1))
            Loop
            DRLastshift.Close()
            gAdo.ClosingCon()

            'Calculate Search_From date and Serch_to date
            If DtFirstShift.Rows(0).Item(0).ToString = False Then
                SearchFrom_Date = Reset_date & " " & Mid(DtFirstShift.Rows(0).Item(1).ToString, 12, 11)
                Ar(0) = SearchFrom_Date
            End If
            If DtlastShift.Rows(0).Item(0).ToString = False Then
                SearchTo_Date = Reset_date & " " & Mid(DtlastShift.Rows(0).Item(1).ToString, 12, 6) & "59.998" & Mid(DtFirstShift.Rows(0).Item(1).ToString, 20, 3)
                Ar(1) = SearchTo_Date
            Else
                SearchTo_Date = Reset_date.AddDays(+1) & " " & Mid(DtlastShift.Rows(0).Item(1).ToString, 12, 6) & "59.998" & Mid(DtFirstShift.Rows(0).Item(1).ToString, 20, 3)
                Ar(1) = SearchTo_Date
            End If
            'Use Array to return two values (Search_From & Search_TO) that Use to Get data in DataGridView when Press BtnSwitchView
            Return Ar

        End If


    End Function

    Public Function GetShifName_And_SearchDateFT(ByVal KnowShift_Date As Boolean, ByVal DateF As DateTime, ByVal DateTo As DateTime) As Object

        Dim NowTime As String = Mid(Now, 12, 11)
        Dim AMorPM As String
        AMorPM = NowTime.Substring(NowTime.LastIndexOf(" ") + 1)
        If NowTime.Contains("م") Then
            NowTime = NowTime.Replace("م", "PM")
        Else
            NowTime = NowTime.Replace("ص", "AM")
        End If
        'NowTime = NowTime.Substring(0, NowTime.LastIndexOf(" "))
        Dim Shift_Name As String = ""
        Dim Shift_Start_Time As DateTime
        Dim Shift_Start_date, Shift_End_date As Boolean
        Dim DT As New DataTable



        Dim DtFirstShift As New DataTable
        Dim DtlastShift As New DataTable
        Dim SearchFrom_Date, SearchTo_Date As String
        Dim Reset_date As DateTime
        Dim Ar(1) As String

        Dim DR As SqlClient.SqlDataReader = gAdo.CmdExecReader("set dateformat dmy select Shift_Name,Shift_Start_time " _
                                                               & " from tblShift " _
                                                               & "where CONVERT(datetime, Shift_Start_Time) < '01/01/1900 " & NowTime _
                                                               & "' and CONVERT(datetime, Shift_End_Time) >'01/01/1900 " & NowTime & "'and Shift_IsDeleted='false'")


        'Fetching To know if where are in the Same Day Or in The Next Day
        'Here We are in the next day So SQl Statment return Null
        If DR.HasRows = False Then
            DR.Close()
            gAdo.ClosingCon()
            'Here we See The Next Day in SQl Statment to Return Value
            DT = gAdo.FillDTWith_TblShift("set dateformat dmy select Shift_Name ,Shift_Start_time,  " _
                                               & " Shift_Start_date,Shift_End_date from tblShift " _
                                               & "where CONVERT(datetime, Shift_Start_Time) < '02/01/1900 " & NowTime _
                                               & "' and CONVERT(datetime, Shift_End_Time) >'02/01/1900 " & NowTime & "'and Shift_IsDeleted='false'", False)

            Shift_Name = DT.Rows(0).Item(0).ToString
            Shift_Start_Time = Mid(DT.Rows(0).Item(1).ToString, 12, 11)
            Shift_Start_date = DT.Rows(0).Item(2).ToString
            Shift_End_date = DT.Rows(0).Item(3).ToString
        Else
            'Here We are in the Same day So SQl Statment return Value
            Do While DR.Read
                Shift_Name = DR.GetValue(0)
                Shift_Start_Time = CDate(DR.GetValue(1)).ToShortTimeString
                'Shift_Start_date = DR.GetValue(2)
                'Shift_End_date = DR.GetValue(3)
            Loop
            DR.Close()
            gAdo.ClosingCon()

        End If
        'to Put The Shift_Name IN Label
        If KnowShift_Date = False Then
            'Here we don't want to return the Shift_Start_date
            'We return Shift_Name with the Shift_Start_Time
            If Shift_Start_Time.ToShortTimeString.EndsWith("PM") Then
                Return Shift_Name & " " & Shift_Start_Time.ToShortTimeString.Replace("PM", " م ")
            Else
                Return Shift_Name & " " & Shift_Start_Time.ToShortTimeString.Replace("AM", " ص ")

            End If

        Else
            '#################################((( Get two Values Search_From & Search_to )))################################
            'To Get Date( From & To) to use it in DataGridview1 Bind when Press BtnSwitchView

            'Get Main Day (From_Date) by return the Now date to its Main Value (it's Mean that we see the Main day is today or yesterday)
            If Shift_Start_date = False And Shift_End_date = False Then
                'the Same day (Today)
                Reset_date = Now.Date
            ElseIf Shift_Start_date = True And Shift_End_date = True Then
                'the day Before (Yesterday)
                Reset_date = Now.AddDays(-1).Date
            ElseIf Shift_Start_date = False And Shift_End_date = True Then
                Dim NightOrDay As String = Mid(Now, 21, 2)
                If NightOrDay = "ص" Then
                    'and from 12:00:00 Am to Shift_End_Date  The day Before (yesterday)
                    Reset_date = Now.AddDays(-1).Date
                Else
                    'from Shift_Start_Date to 11:59:59 Pm the same day (Today)
                    Reset_date = Now.Date
                End If
            End If


            DtFirstShift.Columns.Add("Shift_Start_Date")
            DtFirstShift.Columns.Add("Shift_Start_Time")
            DtlastShift.Columns.Add("Shift_End_Date")
            DtlastShift.Columns.Add("Shift_End_Time")

            'Get  Shift_Start_Date and Shift_Start_Time for the first Shift
            Dim DRFirstShift As SqlClient.SqlDataReader = gAdo.CmdExecReader("select Shift_Start_Date,Shift_Start_Time from dbo.tblShift where Shift_IsFirst = 1")
            Do While DRFirstShift.Read
                DtFirstShift.Rows.Add(DRFirstShift.GetValue(0), DRFirstShift.GetValue(1))
            Loop
            DRFirstShift.Close()
            gAdo.ClosingCon()

            'Get  Shift_Start_Date and Shift_Start_Time for the Last Shift
            Dim DRLastshift As SqlClient.SqlDataReader = gAdo.CmdExecReader("select Shift_End_Date,Shift_End_Time from dbo.tblShift where Shift_IsFirst=10")
            Do While DRLastshift.Read
                DtlastShift.Rows.Add(DRLastshift.GetValue(0), DRLastshift.GetValue(1))
            Loop
            DRLastshift.Close()
            gAdo.ClosingCon()
            'Dim hh As String
            ''Calculate Search_From date and Serch_to date
            'hh = DtFirstShift.Rows(0).Item(2).ToString
            If DtFirstShift.Rows(0).Item(0).ToString = False Then
                SearchFrom_Date = DateF & " " & Mid(DtFirstShift.Rows(0).Item(1).ToString, 12, 11)
                Ar(0) = SearchFrom_Date
            End If
            If DtlastShift.Rows(0).Item(0).ToString = False Then
                SearchTo_Date = DateTo & " " & Mid(DtlastShift.Rows(0).Item(1).ToString, 12, 6) & "59.998" & Mid(DtFirstShift.Rows(0).Item(1).ToString, 20, 3)
                Ar(1) = SearchTo_Date
            Else
                SearchTo_Date = DateTo.AddDays(+1) & " " & Mid(DtlastShift.Rows(0).Item(1).ToString, 12, 6) & "59.998" & Mid(DtFirstShift.Rows(0).Item(1).ToString, 20, 3)
                Ar(1) = SearchTo_Date
            End If
            'Use Array to return two values (Search_From & Search_TO) that Use to Get data in DataGridView when Press BtnSwitchView
            Return Ar

        End If


    End Function


    Public Function MorniningShift(ByVal KnowShift_Date As Boolean, ByVal DateF As DateTime, ByVal DateTo As DateTime) As Object

        Dim NowTime As String = Mid(Now, 12, 11)
        Dim AMorPM As String
        AMorPM = NowTime.Substring(NowTime.LastIndexOf(" ") + 1)
        If NowTime.Contains("م") Then
            NowTime = NowTime.Replace("م", "PM")
        Else
            NowTime = NowTime.Replace("ص", "AM")
        End If
        'NowTime = NowTime.Substring(0, NowTime.LastIndexOf(" "))
        Dim Shift_Name As String = ""
        Dim Shift_Start_Time As DateTime
        Dim Shift_Start_date, Shift_End_date As Boolean
        Dim DT As New DataTable



        Dim DtFirstShift As New DataTable
        Dim DtlastShift As New DataTable
        Dim SearchFrom_Date, SearchTo_Date As String
        Dim Reset_date As DateTime
        Dim Ar(1) As String

        Dim DR As SqlClient.SqlDataReader = gAdo.CmdExecReader("set dateformat dmy select Shift_Name,Shift_Start_time " _
                                                               & " from tblShift " _
                                                               & "where CONVERT(datetime, Shift_Start_Time) < '01/01/1900 " & NowTime _
                                                               & "' and CONVERT(datetime, Shift_End_Time) >'01/01/1900 " & NowTime & "'and Shift_IsDeleted='false'")


        'Fetching To know if where are in the Same Day Or in The Next Day
        'Here We are in the next day So SQl Statment return Null
        If DR.HasRows = False Then
            DR.Close()
            gAdo.ClosingCon()
            'Here we See The Next Day in SQl Statment to Return Value
            DT = gAdo.FillDTWith_TblShift("set dateformat dmy select Shift_Name ,Shift_Start_time,  " _
                                               & " Shift_Start_date,Shift_End_date from tblShift " _
                                               & "where CONVERT(datetime, Shift_Start_Time) < '02/01/1900 " & NowTime _
                                               & "' and CONVERT(datetime, Shift_End_Time) >'02/01/1900 " & NowTime & "'and Shift_IsDeleted='false'", False)

            Shift_Name = DT.Rows(0).Item(0).ToString
            Shift_Start_Time = Mid(DT.Rows(0).Item(1).ToString, 12, 11)
            Shift_Start_date = DT.Rows(0).Item(2).ToString
            Shift_End_date = DT.Rows(0).Item(3).ToString
        Else
            'Here We are in the Same day So SQl Statment return Value
            Do While DR.Read
                Shift_Name = DR.GetValue(0)
                Shift_Start_Time = CDate(DR.GetValue(1)).ToShortTimeString
                'Shift_Start_date = DR.GetValue(2)
                'Shift_End_date = DR.GetValue(3)
            Loop
            DR.Close()
            gAdo.ClosingCon()

        End If
        'to Put The Shift_Name IN Label
        If KnowShift_Date = False Then
            'Here we don't want to return the Shift_Start_date
            'We return Shift_Name with the Shift_Start_Time
            If Shift_Start_Time.ToShortTimeString.EndsWith("PM") Then
                Return Shift_Name & " " & Shift_Start_Time.ToShortTimeString.Replace("PM", " م ")
            Else
                Return Shift_Name & " " & Shift_Start_Time.ToShortTimeString.Replace("AM", " ص ")

            End If

        Else
            '#################################((( Get two Values Search_From & Search_to )))################################
            'To Get Date( From & To) to use it in DataGridview1 Bind when Press BtnSwitchView

            'Get Main Day (From_Date) by return the Now date to its Main Value (it's Mean that we see the Main day is today or yesterday)
            If Shift_Start_date = False And Shift_End_date = False Then
                'the Same day (Today)
                Reset_date = Now.Date
            ElseIf Shift_Start_date = True And Shift_End_date = True Then
                'the day Before (Yesterday)
                Reset_date = Now.AddDays(-1).Date
            ElseIf Shift_Start_date = False And Shift_End_date = True Then
                Dim NightOrDay As String = Mid(Now, 21, 2)
                If NightOrDay = "ص" Then
                    'and from 12:00:00 Am to Shift_End_Date  The day Before (yesterday)
                    Reset_date = Now.AddDays(-1).Date
                Else
                    'from Shift_Start_Date to 11:59:59 Pm the same day (Today)
                    Reset_date = Now.Date
                End If
            End If


            DtFirstShift.Columns.Add("Shift_Start_Date")
            DtFirstShift.Columns.Add("Shift_Start_Time")
            DtlastShift.Columns.Add("Shift_End_Date")
            DtlastShift.Columns.Add("Shift_End_Time")

            'Get  Shift_Start_Date and Shift_Start_Time for the first Shift
            Dim DRFirstShift As SqlClient.SqlDataReader = gAdo.CmdExecReader("select Shift_Start_Date,Shift_Start_Time from dbo.tblShift where Shift_IsFirst = 1")
            Do While DRFirstShift.Read
                DtFirstShift.Rows.Add(DRFirstShift.GetValue(0), DRFirstShift.GetValue(1))
            Loop
            DRFirstShift.Close()
            gAdo.ClosingCon()

            'Get  Shift_Start_Date and Shift_Start_Time for the Last Shift
            Dim DRLastshift As SqlClient.SqlDataReader = gAdo.CmdExecReader("select Shift_Start_Date,Shift_Start_Time from dbo.tblShift where Shift_IsFirst=10")
            Do While DRLastshift.Read
                DtlastShift.Rows.Add(DRLastshift.GetValue(0), DRLastshift.GetValue(1))
            Loop
            DRLastshift.Close()
            gAdo.ClosingCon()

            'Calculate Search_From date and Serch_to date
            If DtFirstShift.Rows(0).Item(0).ToString = False Then
                SearchFrom_Date = DateF & " " & Mid(DtFirstShift.Rows(0).Item(1).ToString, 12, 11)
                Ar(0) = SearchFrom_Date
            End If
            If DtlastShift.Rows(0).Item(0).ToString = False Then
                SearchTo_Date = DateTo & " " & Mid(DtlastShift.Rows(0).Item(1).ToString, 12, 11)
                Ar(1) = SearchTo_Date
            End If
            'If DtlastShift.Rows(0).Item(0).ToString = False Then
            '    SearchTo_Date = DateTo & " " & Mid(DtlastShift.Rows(0).Item(1).ToString, 12, 6) & "59.998" & Mid(DtFirstShift.Rows(0).Item(1).ToString, 20, 3)
            '    Ar(1) = SearchTo_Date
            'Else
            '    SearchTo_Date = DateTo.AddDays(+1) & " " & Mid(DtlastShift.Rows(0).Item(1).ToString, 12, 6) & "59.998" & Mid(DtFirstShift.Rows(0).Item(1).ToString, 20, 3)
            '    Ar(1) = SearchTo_Date
            'End If
            'Use Array to return two values (Search_From & Search_TO) that Use to Get data in DataGridView when Press BtnSwitchView
            Return Ar

        End If


    End Function


    Public Function EveninigShift(ByVal KnowShift_Date As Boolean, ByVal DateF As DateTime, ByVal DateTo As DateTime) As Object

        Dim NowTime As String = Mid(Now, 12, 11)
        Dim AMorPM As String
        AMorPM = NowTime.Substring(NowTime.LastIndexOf(" ") + 1)
        If NowTime.Contains("م") Then
            NowTime = NowTime.Replace("م", "PM")
        Else
            NowTime = NowTime.Replace("ص", "AM")
        End If
        'NowTime = NowTime.Substring(0, NowTime.LastIndexOf(" "))
        Dim Shift_Name As String = ""
        Dim Shift_Start_Time As DateTime
        Dim Shift_Start_date, Shift_End_date As Boolean
        Dim DT As New DataTable



        Dim DtFirstShift As New DataTable
        Dim DtlastShift As New DataTable
        Dim SearchFrom_Date, SearchTo_Date As String
        Dim Reset_date As DateTime
        Dim Ar(1) As String

        Dim DR As SqlClient.SqlDataReader = gAdo.CmdExecReader("set dateformat dmy select Shift_Name,Shift_Start_time " _
                                                               & " from tblShift " _
                                                               & "where CONVERT(datetime, Shift_Start_Time) < '01/01/1900 " & NowTime _
                                                               & "' and CONVERT(datetime, Shift_End_Time) >'01/01/1900 " & NowTime & "'and Shift_IsDeleted='false'")


        'Fetching To know if where are in the Same Day Or in The Next Day
        'Here We are in the next day So SQl Statment return Null
        If DR.HasRows = False Then
            DR.Close()
            gAdo.ClosingCon()
            'Here we See The Next Day in SQl Statment to Return Value
            DT = gAdo.FillDTWith_TblShift("set dateformat dmy select Shift_Name ,Shift_Start_time,  " _
                                               & " Shift_Start_date,Shift_End_date from tblShift " _
                                               & "where CONVERT(datetime, Shift_Start_Time) < '02/01/1900 " & NowTime _
                                               & "' and CONVERT(datetime, Shift_End_Time) >'02/01/1900 " & NowTime & "'and Shift_IsDeleted='false'", False)

            Shift_Name = DT.Rows(0).Item(0).ToString
            Shift_Start_Time = Mid(DT.Rows(0).Item(1).ToString, 12, 11)
            Shift_Start_date = DT.Rows(0).Item(2).ToString
            Shift_End_date = DT.Rows(0).Item(3).ToString
        Else
            'Here We are in the Same day So SQl Statment return Value
            Do While DR.Read
                Shift_Name = DR.GetValue(0)
                Shift_Start_Time = CDate(DR.GetValue(1)).ToShortTimeString
                'Shift_Start_date = DR.GetValue(2)
                'Shift_End_date = DR.GetValue(3)
            Loop
            DR.Close()
            gAdo.ClosingCon()

        End If
        'to Put The Shift_Name IN Label
        If KnowShift_Date = False Then
            'Here we don't want to return the Shift_Start_date
            'We return Shift_Name with the Shift_Start_Time
            If Shift_Start_Time.ToShortTimeString.EndsWith("PM") Then
                Return Shift_Name & " " & Shift_Start_Time.ToShortTimeString.Replace("PM", " م ")
            Else
                Return Shift_Name & " " & Shift_Start_Time.ToShortTimeString.Replace("AM", " ص ")

            End If

        Else
            '#################################((( Get two Values Search_From & Search_to )))################################
            'To Get Date( From & To) to use it in DataGridview1 Bind when Press BtnSwitchView

            'Get Main Day (From_Date) by return the Now date to its Main Value (it's Mean that we see the Main day is today or yesterday)
            If Shift_Start_date = False And Shift_End_date = False Then
                'the Same day (Today)
                Reset_date = Now.Date
            ElseIf Shift_Start_date = True And Shift_End_date = True Then
                'the day Before (Yesterday)
                Reset_date = Now.AddDays(-1).Date
            ElseIf Shift_Start_date = False And Shift_End_date = True Then
                Dim NightOrDay As String = Mid(Now, 21, 2)
                If NightOrDay = "ص" Then
                    'and from 12:00:00 Am to Shift_End_Date  The day Before (yesterday)
                    Reset_date = Now.AddDays(-1).Date
                Else
                    'from Shift_Start_Date to 11:59:59 Pm the same day (Today)
                    Reset_date = Now.Date
                End If
            End If


            DtFirstShift.Columns.Add("Shift_Start_Date")
            DtFirstShift.Columns.Add("Shift_Start_Time")
            DtlastShift.Columns.Add("Shift_End_Date")
            DtlastShift.Columns.Add("Shift_End_Time")

            'Get  Shift_Start_Date and Shift_Start_Time for the first Shift
            Dim DRFirstShift As SqlClient.SqlDataReader = gAdo.CmdExecReader("select Shift_End_Date,Shift_End_Time from dbo.tblShift where Shift_IsFirst = 10")
            Do While DRFirstShift.Read
                DtFirstShift.Rows.Add(DRFirstShift.GetValue(0), DRFirstShift.GetValue(1))
            Loop
            DRFirstShift.Close()
            gAdo.ClosingCon()

            'Get  Shift_Start_Date and Shift_Start_Time for the Last Shift
            Dim DRLastshift As SqlClient.SqlDataReader = gAdo.CmdExecReader("select Shift_End_Date,Shift_End_Time from dbo.tblShift where Shift_IsFirst=1")
            Do While DRLastshift.Read
                DtlastShift.Rows.Add(DRLastshift.GetValue(0), DRLastshift.GetValue(1))
            Loop
            DRLastshift.Close()
            gAdo.ClosingCon()

            'Calculate Search_From date and Serch_to date
            'If DtFirstShift.Rows(0).Item(0).ToString = False Then
            '    SearchFrom_Date = DateF & " " & Mid(DtFirstShift.Rows(0).Item(1).ToString, 12, 11)
            '    Ar(0) = SearchFrom_Date
            'End If

            'If DtFirstShift.Rows(0).Item(0).ToString = False Then
            '    SearchTo_Date = DateTo & " " & Mid(DtlastShift.Rows(0).Item(1).ToString, 12, 6) & "59.998" & Mid(DtFirstShift.Rows(0).Item(1).ToString, 20, 3)
            '    Ar(1) = SearchTo_Date
            'Else

        End If
        Dim s1, s2, s3, s4 As String
        SearchFrom_Date = DateTo & " " & Mid(DtlastShift.Rows(0).Item(1).ToString, 12, 15)
        's3 = DateTo & " " & Mid(DtlastShift.Rows(0).Item(1).ToString, 12, 15)
        Ar(0) = SearchFrom_Date
        s1 = DtlastShift.Rows(0).Item(1).ToString
        SearchTo_Date = DateF.AddDays(+1) & " " & Mid(DtFirstShift.Rows(0).Item(1).ToString, 12, 15)

        'SearchFrom_Date = DateF & " " & Mid(DtFirstShift.Rows(0).Item(1).ToString, 12, 6) & "59.998" & Mid(DtlastShift.Rows(0).Item(1).ToString, 20, 3)
        Ar(1) = SearchTo_Date
        'Else
        'SearchFrom_Date = DateF.AddDays(+1) & " " & Mid(DtlastShift.Rows(0).Item(1).ToString, 12, 6) & "59.998" & Mid(DtFirstShift.Rows(0).Item(1).ToString, 20, 3)
        'Ar(1) = SearchFrom_Date
        'End If
        If DtlastShift.Rows(0).Item(0).ToString = False Then

            'Use Array to return two values (Search_From & Search_TO) that Use to Get data in DataGridView when Press BtnSwitchView
            Return Ar

        End If


    End Function
#End Region

#Region "Car"
    ''' <summary>
    ''' Used To Change Labels [Date - Weight] Color According To Existance In Database
    ''' </summary>
    ''' <param name="IsNew">Parameter Evaluates True In Case Of New transaction</param>
    ''' <remarks></remarks>
    Public Sub ChangeCtrlColor(ByVal lblInDate As Control, ByVal lblFrstWeight As Control, _
                               ByVal lblOutDate As Control, ByVal lblScndWeight As Control, ByVal IsNew As Boolean)
        If IsNew = True Then
            lblInDate.ForeColor = Color.Navy
            lblFrstWeight.ForeColor = Color.Navy
            lblOutDate.ForeColor = Color.DarkGray
            lblScndWeight.ForeColor = Color.DarkGray
        Else
            lblInDate.ForeColor = Color.DarkGray
            lblFrstWeight.ForeColor = Color.DarkGray
            lblOutDate.ForeColor = Color.Navy
            lblScndWeight.ForeColor = Color.Navy
        End If
    End Sub

    Public Function GetCarData(ByVal CarBoardNo As Control, ByVal CarCityCtrl As ComboBox, _
                                     ByVal TrucksCtrl As ComboBox, _
                                     ByVal DriverCtrl As ComboBox, _
                                     ByVal GoodsCtrl As ComboBox, _
                                     ByVal IssueToCtrl As ComboBox, _
                                     ByVal TrucksCityCtrl As ComboBox, _
                                     ByVal SlipCtrl As ComboBox, _
                                     ByVal DealerCtrl As ComboBox, _
                                       ByVal lblInDate As Label, _
                                     ByVal lblFrstWeight As Label, _
                                     ByVal lblScndWeight As Label, _
                                      ByVal radFrstEmptyCtrl As RadioButton, _
                                      ByVal radFrstFullCtrl As RadioButton, ByVal Getdata As Boolean) As Boolean

        If Getdata = True Then
            CarCityCtrl.SelectedIndex = -1
            TrucksCityCtrl.SelectedIndex = -1
            IssueToCtrl.SelectedIndex = -1

            GoodsCtrl.SelectedIndex = -1
            DriverCtrl.SelectedIndex = -1
            TrucksCtrl.SelectedIndex = -1
            SlipCtrl.SelectedIndex = -1

            'Found Carboard Number, Fetching the CarID to select Matching city
            Dim CarID As Long = gAdo.GetColumnID("Car_ID", "tblCar", "CarBoard_No", CarBoardNo.Text)
            'CarCityCtrl.Text = gAdo.CmdExecScalar("Select City_Name From tblCity join tblcar " & _
            '                                      "on City_ID=CarBoard_City_ID where Car_ID=" & CarID)

            CType(CarCityCtrl, ComboBox).SelectedValue = gAdo.CmdExecScalar("Select City_ID From tblCity join tblcar " & _
                                                  "on City_ID=CarBoard_City_ID where Car_ID=" & CarID)


            'Get Trans_ID By Car_ID 
            Dim TransID As Integer = gAdo.CmdExecScalar("select isnull(max(CID),0) from dbo.tblTransactions where TR_TY=2 and Car_ID = " & _
                                                                       CarID)


            'Get ((Car_Info_ID , Driver_ID , Issue_to_ID , Slip_Rate)) from Transaction Using Trans_ID
            Dim DT As DataTable = gAdo.GetTransactionDT(gTransID)

            If DT.Rows.Count = 0 Then
                Exit Function
            End If
            'Get TruckBoard_No
            TrucksCtrl.SelectedValue = DT.Rows(0).Item("Car_Info_ID").ToString()
            'Get Truck City
            CType(TrucksCityCtrl, ComboBox).SelectedValue = gAdo.CmdExecScalar("Select City_ID From tblCity join tblcarInfo " & _
                                                  "on City_ID=TruckBoard_City_ID where Car_Info_ID=" & DT.Rows(0).Item("Car_Info_ID").ToString())

            'Get Driver_Name
            DriverCtrl.SelectedValue = DT.Rows(0).Item("Driver_ID").ToString()

            'Get Issue_to_Name
            IssueToCtrl.SelectedValue = DT.Rows(0).Item("Dealer_ID").ToString()


            'Get Issue_to_Name
            IssueToCtrl.SelectedValue = DT.Rows(0).Item("Issue_To_ID").ToString()

            'Get Slip_Rate
            SlipCtrl.Text = DT.Rows(0).Item("Slip_Rate").ToString()

            'Get Good
            'gAdo.CtrlItemsLoad("Good_ID", "Good_Name", "tblGood ORDER BY Good_Name", GoodsCtrl)

            GoodsCtrl.SelectedValue = DT.Rows(0).Item("Good_ID").ToString()



            'Get Dealer
            DealerCtrl.SelectedValue = DT.Rows(0).Item("Dealer_ID").ToString()


            'Return InDate 
            lblInDate.Text = DT.Rows(0).Item("In_Date")

            'Return First Weight 
            lblFrstWeight.Text = DT.Rows(0).Item("First_Weigth") & " KG"
            lblScndWeight.Text = DT.Rows(0).Item("Second_Weight") & "KG"

            'Check Type of first weight and apply it on specified RadioButton
            If DT.Rows(0).Item("First_Weight_IsEmpty").ToString = True Then
                radFrstEmptyCtrl.Checked = True
            Else
                radFrstFullCtrl.Checked = True
            End If

            ' ~~~~~~~~
            ' هنا هاقرا على القراءه التانيه
            ' ~~~~~~~~

            Return True

        Else
            'New Car

            'Clear Controls Text
            'CarCityCtrl.Text = ""
            'TrucksCityCtrl.Text = ""
            'IssueToCtrl.Text = ""
            'DealerCtrl.Text = ""
            'GoodsCtrl.Text = ""
            'DriverCtrl.Text = ""
            'TrucksCtrl.Text = ""
            'SlipCtrl.Text = ""

            'Make this To Refresh combo selectIndexChange
            'CarCityCtrl.SelectedIndex = -1
            'TrucksCityCtrl.SelectedIndex = -1
            'IssueToCtrl.SelectedIndex = -1
            'DealerCtrl.SelectedIndex = -1
            'GoodsCtrl.SelectedIndex = -1
            'DriverCtrl.SelectedIndex = -1
            'TrucksCtrl.SelectedIndex = -1
            'SlipCtrl.SelectedIndex = -1
            'radFrstEmptyCtrl.Checked = True

            ' ~~~~~~~~
            ' هنا هاقرا على القراءه الاولى
            ' ~~~~~~~~

            Return False
        End If


    End Function


    Public Function IsNewCar(ByVal CarBoardNoCtrl As TextBox, ByVal CarCityCtrl As ComboBox, _
                                 ByVal TrucksCtrl As ComboBox, _
                                 ByVal DriverCtrl As ComboBox, _
                                 ByVal GoodsCtrl As ComboBox, _
                                 ByVal IssueToCtrl As ComboBox, _
                                 ByVal TrucksCityCtrl As ComboBox, _
                                 ByVal SlipCtrl As ComboBox, _
                                 ByVal DealerCtrl As ComboBox, _
                                 ByVal radFrstEmptyCtrl As RadioButton, _
                                 ByVal radFrstFullCtrl As RadioButton, ByVal TransIDCtrl As Control, _
                                 ByVal lblInDate As Control, ByVal lblFrstWeight As Control, _
                                 ByVal lblOutDate As Control, ByVal lblScndWeight As Control) As Boolean
        'Unlock All Controls
        gMethods.IsLock(False, gTruckCtrls)

        'Get Car ID
        gCarO.ID = gAdo.CmdExecScalar("select Car_ID from tblcar where CarBoard_No='" & CarBoardNoCtrl.Text.Trim & "'")
        'Fetching the CarID
        If gCarO.ID <> 0 Then
            'Exist Car
            Dim TSql = "Select Out_Date From tblTransactions Where Trans_ID=(" & _
                               "Select max(Trans_ID) From tblTransactions Where Car_ID=" & gCarO.ID & ")"
            'Fetching the Transaction
            If IsDBNull(gAdo.CmdExecScalar(TSql)) = False Then
                'New Transaction
                ' ~~~~~~~~~~~~~~~~~~~~~~~~~~~ 7-9-2010
                Dim TransID As Integer
                'TransIDCtrl.Text = gAdo.GetMax("Trans_ID", "tblTransactions")
                TransID = gAdo.CmdExecScalar("select count(trans_id) from tbltransactions where trans_id=" & cfrmMain.SharedTxtTransID.Text)
                If TransID = 0 Then
                    ChangeCtrlColor(lblInDate, lblFrstWeight, lblOutDate, lblScndWeight, True)
                    Exit Function
                End If
                ' ~~~~~~~~~~~~~~~~~~~~~~~ end 7-9-2010
                gTransactionO.ID = 0
                'Show Trans_ID in TxtTransID
                ' the next line commented in 10/10/2010 by TH  due to usage of two scales
                'TransIDCtrl.Text = gAdo.GetMax("Trans_ID", "tblTransactions") '+ 1
                ' end the next line commented in 10/10/2010 by TH  due to usage of two scales
                'To Change Lbl Color
                ChangeCtrlColor(lblInDate, lblFrstWeight, lblOutDate, lblScndWeight, True)
                IsLock(TransIDCtrl, True)
                'gCarO.ID = 0
                'Get Last Data (History)
                GetCarData(CarBoardNoCtrl, CarCityCtrl, TrucksCtrl, DriverCtrl, GoodsCtrl, _
                                  IssueToCtrl, TrucksCityCtrl, SlipCtrl, DealerCtrl, lblInDate, _
                                  lblFrstWeight, lblScndWeight, radFrstEmptyCtrl, radFrstFullCtrl, True)

                'Lock [Truck City]
                gMethods.IsLock(TrucksCityCtrl, True)
                'Lock [Car City]
                gMethods.IsLock(CarCityCtrl, True)
                'Waiting For scale to give weight
                'If lblFrstWeight.Text <> 0 Then

                'End If
                ' 15 -8 -2010
                'lblFrstWeight.Text = CStr(0 & " KG")
                'lblScndWeight.Text = CStr(0 & " KG")
                '---------------------------------15 -8 -2010
                'Set InDate to Now and OutDate to Null
                lblInDate.Text = Now
                gTransactionO.In_Date = Now

                gTransactionO.Out_Date = DateTime.MinValue
                lblOutDate.Text = ""
                'Flg To Insert Transactions
                gIsUpdate = False

                Return True

            Else
                'Exist Transaction Doesn't Have The Second Scale Weight
                'Get Trans_ID By Car_ID 

                'gTransactionO.ID = gAdo.CmdExecScalar("select max(Trans_ID) from dbo.tblTransactions where Car_ID = " & _
                'gCarO.ID)
                'TransIDCtrl.Text = gTransactionO.ID
                'Show Trans_ID in TxtTransID

                'To Change Lbl Color
                ChangeCtrlColor(lblInDate, lblFrstWeight, lblOutDate, lblScndWeight, False)

                'IsLock(TransIDCtrl, True)

                'Get Last Data
                If gIsUpdate = False Then
                    GetCarData(CarBoardNoCtrl, CarCityCtrl, TrucksCtrl, DriverCtrl, GoodsCtrl, _
                                     IssueToCtrl, TrucksCityCtrl, SlipCtrl, DealerCtrl, lblInDate, _
                                     lblFrstWeight, lblScndWeight, radFrstEmptyCtrl, radFrstFullCtrl, True)
                End If


                'Assign Now for OutDate 
                lblOutDate.Text = Now
                gTransactionO.Out_Date = Now
                'Give Null for Second weight waiting for scale to give the weight
                lblScndWeight.Text = CStr(0) & " KG"
                'Flg To Update Transactions

                'Lock Controls [Truck No. - Truck City - Driver - License No.]

                'Lock [Car City ]

                'gMethods.IsLock(True, gTruckCtrls)
                'gMethods.IsLock(True, CarCityCtrl)
                gIsUpdate = True

                Return False
            End If

        Else
            'New Car
            'To Change Lbl Color
            gTransactionO.ID = 0
            ChangeCtrlColor(lblInDate, lblFrstWeight, lblOutDate, lblScndWeight, True)

            GetCarData(CarBoardNoCtrl, CarCityCtrl, TrucksCtrl, DriverCtrl, GoodsCtrl, _
                                 IssueToCtrl, TrucksCityCtrl, SlipCtrl, DealerCtrl, lblInDate, _
                                 lblFrstWeight, lblScndWeight, radFrstEmptyCtrl, radFrstFullCtrl, False)


            lblInDate.Text = Now
            lblOutDate.Text = ""
            'Waiting For scale to give weight
            'lblFrstWeight.Text = CStr(0 & " KG")
            'lblScndWeight.Text = CStr(0 & " KG")
            ' Unlock [Car City ]
            gMethods.IsLock(False, CarCityCtrl)
            'Flg To Insert Transactions
            gIsUpdate = False

        End If

    End Function
#End Region

#Region "Truck"

    Public Function IsTruckExist(ByVal CboTruckBoardNo As ComboBox, ByVal TruckCityCtrl As Control) As Boolean
        'Unlock Truck Ciry
        IsLock(False, TruckCityCtrl)
        'Fetching the Truck Board Name
        If CboTruckBoardNo.FindStringExact(CboTruckBoardNo.Text.Trim) >= 0 Then
            'Found Truck Board No
            'Get Driver ID

            Try
                gCarInfoO.ID = CboTruckBoardNo.SelectedValue
            Catch ex As Exception
                gCarInfoO.ID = gAdo.GetColumnID("Car_Info_ID", "tblCarInfo", "TruckBoard_No", CboTruckBoardNo.Text.Trim)
            End Try
            TruckCityCtrl.Text = ""
            'Get Truck City Name
            TruckCityCtrl.Text = gAdo.CmdExecScalar("Select City_Name From tblCity join tblcarInfo " & _
                                                            "on City_ID=TruckBoard_City_ID where Car_Info_ID=" & gCarInfoO.ID)
            'lock CboTruck_City 
            ''IsLock(TruckCityCtrl, True)
        Else

            TruckCityCtrl.Text = ""
            gCarInfoO.ID = 0

        End If

    End Function

#End Region

#Region "Driver"

    Public Function IsDriverExist(ByVal CboDriver As ComboBox, ByVal LicenseCtrl As Control) As Boolean
        'Unlock Driver txtlicenceNO
        gMethods.IsLock(False, LicenseCtrl)

        'Fetching the Driver Name
        If CboDriver.FindStringExact(CboDriver.Text.Trim) >= 0 Then
            'Found Driver Name in CboDriver
            'Get Licence_NO

            'COMEHERE
            'LicenseCtrl.Text = gAdo.CmdExecScalar("Select Driver_License_No From tblDriver Where Driver_Name = '" & CboDriver.Text.Trim & "'")
            
            Try

            
                If (gAdo.CmdExecScalar("Select COUNT(*) From tblDriver Where Driver_Name = '" & CboDriver.Text.Trim & "'")) > 1 Then

                    LicenseCtrl.Text = gAdo.CmdExecScalar("Select Driver_License_No From tblDriver Where Driver_ID = '" & CboDriver.SelectedValue & "'")
                Else
                    LicenseCtrl.Text = gAdo.CmdExecScalar("Select Driver_License_No From tblDriver Where Driver_Name = '" & CboDriver.Text.Trim & "'")
                End If


                'lock  txtLicence =_NO
                IsLock(LicenseCtrl, True)
            Catch ex As Exception
                LicenseCtrl.Text = ""
            End Try
        Else
            ' Driver Doesn't Exist

            LicenseCtrl.Text = ""
            IsLock(LicenseCtrl, False)

        End If

    End Function

#End Region

#Region "User Permission Grid"


    Public Shared Sub UserPermissionGrid(ByVal GRD As DataGridView)

        GRD.Columns.Clear()
        Dim User_Permission_ID As New System.Windows.Forms.DataGridViewButtonColumn
        Dim Permission_ID As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim Permission_Name As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim Allow As New System.Windows.Forms.DataGridViewCheckBoxColumn

        GRD.Font = New System.Drawing.Font("Arial", 12.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        GRD.RightToLeft = Windows.Forms.RightToLeft.Yes
        GRD.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() _
        {User_Permission_ID, Permission_ID, Permission_Name, Allow})
        GRD.Font = New System.Drawing.Font("Arial", 12.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))

        User_Permission_ID.HeaderText = "رقم الصلاحيه"
        User_Permission_ID.DataPropertyName = "User_Permission_ID"
        User_Permission_ID.Name = "User_Permission_ID"
        User_Permission_ID.Text = "  "
        User_Permission_ID.Width = 100
        User_Permission_ID.Visible = False

        Permission_ID.HeaderText = "كود البيـــان"
        Permission_ID.DataPropertyName = "Permission_ID"
        Permission_ID.Name = "Permission_ID"
        'Permission_ID.Text = "  "
        Permission_ID.Width = 100
        Permission_ID.Visible = False

        Permission_Name.HeaderText = "البيـــان"
        Permission_Name.DataPropertyName = "Permission_Name"
        Permission_Name.Name = "Permission_Name"
        Permission_Name.Width = 280
        Permission_Name.ReadOnly = True

        Allow.HeaderText = "الصلاحيـه"
        Allow.DataPropertyName = "Allow"
        Allow.Name = "Allow"
        Allow.Width = 70

    End Sub

#End Region
#Region "UserPermission"

    Sub setUserPermission(ByVal ID As Integer)
        Dim I As Integer
        'Here the User is New And Limited
        If gAdo.CmdExecScalar("select count(*) from tblUserPermission where User_ID= " & ID) = 0 Then
            For I = 1 To 15
                gAdo.CmdExec("Insert tblUserPermission values(" & ID & "," & I & ",0)")
            Next

        Else

        End If

    End Sub

#End Region

    Public Sub RandNumbers(ByVal TargetCtrl As Control, ByVal BaseCtrl As Control, ByVal NetCtrl As Control, ByVal subtractCtrl As Control)
        Dim rnd As Random = New Random()
        TargetCtrl.Text = rnd.Next(11111, 55555555)
        BaseCtrl.Text = TargetCtrl.Text
        Dim result As Integer = Val(TargetCtrl.Text) - Val(subtractCtrl.Text)
        If result > 0 Then
            NetCtrl.Text = result
        Else
            NetCtrl.Text = result * -1
        End If
    End Sub
#End Region

#Region "Controls Lock Mechanism"
    Public Sub UnlockCtrls(ByVal Frm As Form)
        Dim ctrl As New Control
        Dim SubCtrl As New Control
        For Each ctrl In Frm.Controls
            If ctrl.GetType.ToString = "System.Windows.Forms." & "ComboBox" Then
                If CType(ctrl, ComboBox).Tag = "lock" Then
                    CType(ctrl, ComboBox).DropDownStyle = ComboBoxStyle.DropDown
                    'AddHandler ctrl.KeyPress, AddressOf UnLock_Keypress
                End If
            ElseIf ctrl.GetType.ToString = "System.Windows.Forms." & "TextBox" Then
                If CType(ctrl, TextBox).Tag = "lock" Then
                    CType(ctrl, TextBox).ReadOnly = False
                End If
            Else
                If TypeOf ctrl Is Panel _
                        OrElse TypeOf ctrl Is TabControl _
                        OrElse TypeOf ctrl Is GroupBox Then
                    UnLock_Ctrl_PTG(ctrl)
                End If
            End If
        Next
    End Sub

    Public Sub lockCtrls(ByVal Frm As Form)
        Dim ctrl As New Control
        Dim SubCtrl As New Control
        For Each ctrl In Frm.Controls
            If ctrl.GetType.ToString = "System.Windows.Forms." & "ComboBox" Then
                If CType(ctrl, ComboBox).Tag = "lock" Then
                    CType(ctrl, ComboBox).DropDownStyle = ComboBoxStyle.Simple
                    AddHandler ctrl.KeyPress, AddressOf Lock_Keypress
                End If
            ElseIf ctrl.GetType.ToString = "System.Windows.Forms." & "TextBox" Then
                If CType(ctrl, TextBox).Tag = "lock" Then
                    CType(ctrl, TextBox).ReadOnly = True
                    CType(ctrl, TextBox).BackColor = Color.White
                End If
            Else
                If TypeOf ctrl Is Panel _
                        OrElse TypeOf ctrl Is TabControl _
                        OrElse TypeOf ctrl Is GroupBox Then
                    Lock_Ctrl_PTG(ctrl)
                End If
            End If
        Next
    End Sub

    Public Sub IsLock(ByVal Ctrl As Control, ByVal Lock As Boolean)
        If Lock = True Then
            If TypeOf Ctrl Is TextBox Then
                CType(Ctrl, TextBox).ReadOnly = True
            ElseIf TypeOf Ctrl Is ComboBox Then
                CType(Ctrl, ComboBox).DropDownStyle = ComboBoxStyle.Simple
                AddHandler Ctrl.KeyDown, AddressOf Lock_KeyDown
                AddHandler Ctrl.KeyPress, AddressOf Lock_Keypress
            End If
        Else
            If TypeOf Ctrl Is TextBox Then
                CType(Ctrl, TextBox).ReadOnly = False
            ElseIf TypeOf Ctrl Is ComboBox Then
                CType(Ctrl, ComboBox).DropDownStyle = ComboBoxStyle.DropDown
                RemoveHandler Ctrl.KeyDown, AddressOf Lock_KeyDown
                RemoveHandler Ctrl.KeyPress, AddressOf Lock_Keypress
            End If
        End If
    End Sub

    Public Sub IsLock(ByVal Lock As Boolean, ByVal ParamArray Ctrl() As Control)
        For i As Short = 0 To UBound(Ctrl)
            If Lock = True Then
                If TypeOf Ctrl(i) Is TextBox Then
                    CType(Ctrl(i), TextBox).ReadOnly = True
                ElseIf TypeOf Ctrl(i) Is ComboBox Then
                    CType(Ctrl(i), ComboBox).DropDownStyle = ComboBoxStyle.Simple
                    AddHandler Ctrl(i).KeyDown, AddressOf Lock_KeyDown
                    AddHandler Ctrl(i).KeyPress, AddressOf Lock_Keypress
                End If
            Else
                If TypeOf Ctrl(i) Is TextBox Then
                    CType(Ctrl(i), TextBox).ReadOnly = False
                ElseIf TypeOf Ctrl(i) Is ComboBox Then
                    CType(Ctrl(i), ComboBox).DropDownStyle = ComboBoxStyle.DropDown
                    RemoveHandler Ctrl(i).KeyDown, AddressOf Lock_KeyDown
                    RemoveHandler Ctrl(i).KeyPress, AddressOf Lock_Keypress
                End If
            End If
        Next
    End Sub

    Private Sub Lock_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If TypeOf sender Is ComboBox Then
            e.Handled = True
        End If
    End Sub

    Private Sub Lock_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.Delete, Keys.Down, Keys.Up, Keys.PageDown, Keys.PageUp
                e.Handled = True
        End Select
    End Sub

    Public Sub Lock_Ctrl_PTG(ByVal cntrl As Control)
        Dim SubCtrl As Control

        For Each SubCtrl In cntrl.Controls
            If TypeOf SubCtrl Is ComboBox Then
                If CType(SubCtrl, ComboBox).Tag = "lock" Then
                    CType(SubCtrl, ComboBox).DropDownStyle = ComboBoxStyle.Simple
                    AddHandler SubCtrl.KeyPress, AddressOf Lock_Keypress
                End If
            ElseIf TypeOf SubCtrl Is TextBox Then
                If CType(SubCtrl, TextBox).Tag = "lock" Then
                    CType(SubCtrl, TextBox).ReadOnly = True
                    CType(SubCtrl, TextBox).BackColor = Color.White
                End If
            ElseIf TypeOf SubCtrl Is Panel _
           OrElse TypeOf SubCtrl Is TabControl _
        OrElse TypeOf SubCtrl Is GroupBox Then
                Lock_Ctrl_PTG(SubCtrl)
            End If
        Next
    End Sub

    Public Sub UnLock_Ctrl_PTG(ByVal cntrl As Control)
        Dim SubCtrl As Control
        For Each SubCtrl In cntrl.Controls
            If TypeOf SubCtrl Is ComboBox Then
                If CType(SubCtrl, ComboBox).Tag = "lock" Then
                    CType(SubCtrl, ComboBox).DropDownStyle = ComboBoxStyle.DropDown
                    'AddHandler SubCtrl.KeyPress, AddressOf UnLock_Keypress
                End If
            ElseIf TypeOf SubCtrl Is TextBox Then
                If CType(SubCtrl, TextBox).Tag = "lock" Then
                    CType(SubCtrl, TextBox).ReadOnly = False
                End If

            ElseIf TypeOf SubCtrl Is Panel _
           OrElse TypeOf SubCtrl Is TabControl _
        OrElse TypeOf SubCtrl Is GroupBox Then
                UnLock_Ctrl_PTG(SubCtrl)
            End If
        Next
    End Sub

#End Region

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

End Class




