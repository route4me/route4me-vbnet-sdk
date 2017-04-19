Imports System.IO
Imports System.Runtime.Serialization
Imports System.Collections.Generic
Imports System.Runtime.Serialization.Json
Imports System.Data
Imports System.Data.Common
Imports System.Configuration
Imports System.Data.OleDb
Imports System.Data.SqlServerCe
Imports System.Web.Script

Namespace Route4MeSDK.DataTypes
    Public Enum R4M_DataType
        Activity
        Addressbook
        AvoidanceZone
        Member
        Note
        Optimization
        Order
        Route
        Telematics
        Territory
        Vehicle
    End Enum

    Public Enum DB_Type
        MSSQL
        SQLCE
        MySQL
        PostgreSQL
        SQLite
        MS_Access
    End Enum

    Public Class cDatabase
        Private _con As IDbConnection
        Private _cmd As IDbCommand

        Private _transaction As IDbTransaction
        Private _adapter As DbDataAdapter
        Private _dr As IDataReader
        Private _factory As DbProviderFactory
        Private _conStngInstitute As ConnectionStringSettings

        Private _isDisposed As Boolean

        Private sStartupFolder As String

        Public Sub New(db_type__1 As DB_Type)
            For i As Integer = 0 To ConfigurationManager.ConnectionStrings.Count - 1
                Dim sConn As String = ConfigurationManager.ConnectionStrings(i).ConnectionString
                Console.WriteLine(sConn)

            Next

            Select Case db_type__1
                Case DB_Type.MySQL
                    _conStngInstitute = ConfigurationManager.ConnectionStrings("conMySQL")
                    Exit Select
                Case DB_Type.MSSQL
                    _conStngInstitute = ConfigurationManager.ConnectionStrings("conMSSQL")
                    Exit Select
                Case DB_Type.SQLCE
                    _conStngInstitute = ConfigurationManager.ConnectionStrings("conSQLCE")
                    Exit Select
                Case DB_Type.PostgreSQL
                    _conStngInstitute = ConfigurationManager.ConnectionStrings("conPostgreSQL")
                    Exit Select
                Case DB_Type.SQLite
                    '_conStngInstitute = ConfigurationManager.ConnectionStrings["conInstitute"];
                    Exit Select
                Case DB_Type.MS_Access
                    _conStngInstitute = ConfigurationManager.ConnectionStrings("conOLEDB")
                    Exit Select
            End Select

            _factory = DbProviderFactories.GetFactory(_conStngInstitute.ProviderName)

            _con = _factory.CreateConnection()
            _con.ConnectionString = _conStngInstitute.ConnectionString
            _cmd = _con.CreateCommand()

            _adapter = _factory.CreateDataAdapter()

            _isDisposed = False

            sStartupFolder = AppDomain.CurrentDomain.BaseDirectory
        End Sub

        Protected Overridable Sub CleanUp()
            If _con IsNot Nothing Then
                _con.Close()
                _con.Dispose()
            End If

            If _cmd IsNot Nothing Then
                _cmd.Dispose()
            End If

            If _dr IsNot Nothing Then
                _dr.Close()
                _dr.Dispose()
            End If
        End Sub

        Public Sub OpenConnection()
            Try
                If _con.State <> ConnectionState.Open Then
                    _con.Open()
                End If
            Catch ex As Exception
                Console.WriteLine("Connection not established!.. " + ex.Message)
            End Try
        End Sub

        Public Sub CloseConnection()
            If _con.State <> ConnectionState.Closed Then
                _con.Close()
            End If
        End Sub

        ' Parsing of the multi-command SQL texts, ommiting commentaries and extracting of the puare SQL commands;
        ' Note: 
        ' - after semicolon ';' shouldn't be written anything (blank spaces allowed).
        ' - befor '/*' shouldn't be written anything (blank spaces allowed).
        Public Function ExecuteMulticoomandSql(sQuery As String) As Integer
            Dim iRet As Integer = 0
            Try
                sQuery = sQuery.Replace(";", ";^")
                Dim arCommands As String() = sQuery.Split("^"c)
                If _con.State <> ConnectionState.Open Then
                    OpenConnection()
                End If
                _transaction = _con.BeginTransaction(IsolationLevel.Unspecified)
                _cmd.Connection = _con
                _cmd.CommandType = CommandType.Text
                _cmd.Transaction = _transaction
                Dim blComment As Boolean = False
                For Each s0 As String In arCommands
                    Dim arLines As String() = s0.Split(New String() {vbCr & vbLf, vbCr, vbLf}, StringSplitOptions.None)
                    Dim sCurCommand As String = ""
                    For Each s1 As String In arLines
                        Dim s2 As String = s1.Trim()
                        If s2.Length < 1 Then
                            Continue For
                        End If
                        If s2.IndexOf("--") = 0 Then
                            Continue For
                        End If
                        If s2.IndexOf("/*") = 0 Then
                            If s2.IndexOf("*/") = s1.Length - 2 Then
                                blComment = False
                            Else
                                blComment = True
                            End If
                            Continue For
                        End If
                        If s2.Length >= 2 AndAlso s2.IndexOf("*/") = s1.Length - 2 Then
                            blComment = False
                            Continue For
                        End If

                        If Not blComment Then
                            If s2.IndexOf(";") <> s2.Length - 1 Then
                                sCurCommand += s2 + System.Environment.NewLine
                            Else
                                sCurCommand += s2
                                _cmd.CommandText = sCurCommand
                                iRet = _cmd.ExecuteNonQuery()
                                sCurCommand = ""

                            End If

                        End If
                    Next
                Next
                _transaction.Commit()
                iRet = 1
                Return iRet
            Catch ex As Exception
                Console.WriteLine(":( Transaction failed... " & ex.Message)
                _transaction.Rollback()
                iRet = 0
                Return iRet
            End Try

        End Function

        ' Table for correspondance between Route4Me CSV exported file fields and Route4Me API fields
        Public Function GetCsv2ApiDictionary(sTableName As String) As DataTable
            Dim tblDictionary As New DataTable()

            Try
                tblDictionary = fillTable((Convert.ToString("SELECT * FROM csv_to_api_dictionary WHERE table_name='") & sTableName) & "'")
                Return tblDictionary
            Catch ex As Exception
                Console.WriteLine(":( csv_to_api_dictionary table reading failed!.. " & ex.Message)
            End Try

            Return tblDictionary
        End Function

        ' Method for importing an addressbook CSV file (with structure equal to exported by Route4Me web UI CSV file) to an addressbook table on the SQL type server.
        ' sFileName --- CSV file name.
        ' sTableName --- Server addressbook table name.
        ' sIdName --- The name of id column of the server addressbook table (it's differs from address_id, you need it for editing prior updloading to the Route4Me server)
        ' isFirstRowHeader --- If true, first column of the CSV file is header.
        '  
        Public Sub Csv2Table(sFileName As String, sTableName As String, sIdName As String, iFieldsNumber As Integer, isFirstRowHeader As Boolean)
            If Not File.Exists(sFileName) Then
                Console.WriteLine((Convert.ToString("The file ") & sFileName) & " doesn't exist...")
                Return
            End If

            Dim header As String = If(isFirstRowHeader, "Yes", "No")

            Dim pathOnly As String = System.IO.Path.GetDirectoryName(sFileName)
            Dim fileName As String = System.IO.Path.GetFileName(sFileName)

            Dim csvCom As String = (Convert.ToString("SELECT * FROM [") & fileName) & "]"

            Dim tblDictionary As DataTable = GetCsv2ApiDictionary(sTableName)

            Dim tblTempTable As New DataTable()

            Using csvCon As New OleDbConnection((Convert.ToString((Convert.ToString("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=") & pathOnly) & ";Extended Properties=""Text;HDR=") & header) & """")
                Using comCsv As New OleDbCommand(csvCom, csvCon)
                    Using csvAdapter As New OleDbDataAdapter(comCsv)
                        tblTempTable.Locale = System.Globalization.CultureInfo.CurrentCulture
                        csvAdapter.Fill(tblTempTable)
                    End Using
                End Using
            End Using

            For Each row As DataRow In tblTempTable.Rows
                Dim id As Integer = -1

                Dim blNew As Boolean = True

                If row.Table.Columns.Contains(sIdName) Then
                    If Integer.TryParse(row(sIdName).ToString(), id) Then
                        id = Convert.ToInt32(row(sIdName))
                    End If

                    If id > 0 Then
                        blNew = IsNewAddress(sTableName, sIdName, id)
                    End If
                End If
                Dim sList As String = "("
                Dim sValues As String = "Values ("

                If Not blNew Then
                    sList = "SET "

                    For iCol As Integer = 0 To iFieldsNumber - 1
                        Dim isValid As Boolean = IsValidValue(tblTempTable.Columns(iCol), row(iCol))
                        If isValid Then
                            Dim sCsvFieldName As String = tblTempTable.Columns(iCol).ColumnName

                            If sCsvFieldName = sIdName Then
                                Continue For
                            End If

                            Dim arRows As DataRow() = tblDictionary.[Select]((Convert.ToString("r4m_csv_field_name='") & sCsvFieldName) & "'")
                            If arRows.Length = 1 Then
                                Dim sFieldApiName As String = arRows(0)("api_field_name").ToString()
                                Dim sApiFieldType As String = arRows(0)("api_field_type").ToString()
                                Dim sCsvFieldType As String = arRows(0)("csv_field_type").ToString()

                                If sFieldApiName = "day_scheduled_for_YYMMDD" Then

                                    Dim oSchedule = ExceptFields2QueryValue(sFieldApiName, row(iCol))
                                    If oSchedule Is Nothing Then
                                        sList += sFieldApiName & Convert.ToString("=null,")
                                    Else
                                        sList += (sFieldApiName & Convert.ToString("='")) & oSchedule & "',"
                                    End If
                                Else
                                    Select Case tblTempTable.Columns(iCol).DataType.Name
                                        Case "String"
                                            sList += (sFieldApiName & Convert.ToString("=N'")) & row(iCol).ToString() & "',"
                                            Exit Select
                                        Case "Int32"
                                            sList += (sFieldApiName & Convert.ToString("=")) & row(iCol).ToString() & ","


                                            Exit Select
                                        Case "Double"
                                            sList += (sFieldApiName & Convert.ToString("=")) & row(iCol).ToString() & ","
                                            Exit Select
                                        Case "DateTime"
                                            Dim dt1900 As New DateTime(1900, 1, 1, 0, 0, 0)
                                            If DateTime.TryParse(row(iCol).ToString(), dt1900) Then
                                                dt1900 = Convert.ToDateTime(row(iCol))
                                                If sApiFieldType <> sCsvFieldType AndAlso sApiFieldType = "int" Then
                                                    Dim iUnixTime As Long = R4MeUtils.ConvertToUnixTimestamp(dt1900)
                                                    sList += (sFieldApiName & Convert.ToString("=")) & iUnixTime & ","
                                                Else
                                                    If _conStngInstitute.ProviderName = "System.Data.OleDb" Then
                                                        sList += (sFieldApiName & Convert.ToString("=#")) & dt1900.ToString("yyyy-MM-dd HH:mm:ss") & "#,"
                                                    Else
                                                        sList += (sFieldApiName & Convert.ToString("='")) & dt1900.ToString("yyyy-MM-dd HH:mm:ss") & "',"
                                                    End If
                                                End If
                                            End If
                                            Exit Select
                                    End Select

                                End If
                            Else
                                Continue For
                            End If
                        End If
                    Next
                    sList = sList.TrimEnd(","c)

                    If tblTempTable.Columns.Count > 33 Then
                        Dim sbCustom As New System.Text.StringBuilder()
                        sbCustom.Append("{")
                        For iCol As Integer = 33 To tblTempTable.Columns.Count - 1
                            Dim sbCustom1 As New System.Text.StringBuilder()
                            sbCustom1.Append("{")
                            For iCol1 As Integer = 33 To tblTempTable.Columns.Count - 1
                                Dim isValid As Boolean = IsValidValue(tblTempTable.Columns(iCol1), row(iCol1))
                                If isValid Then
                                    If tblTempTable.Columns(iCol1).DataType.Name = "String" Then
                                        Dim sFieldName As String = tblTempTable.Columns(iCol1).ColumnName
                                        Dim sValue As String = row(iCol1).ToString()
                                        sbCustom1.Append((Convert.ToString((Convert.ToString("""") & sFieldName) & """: """) & sValue) & """,")
                                    End If
                                End If
                            Next
                            Dim sCustom As String = sbCustom.ToString()
                            sCustom = sCustom.TrimEnd(","c)
                            sCustom += "}"
                            If sCustom.Length > 3 Then
                                sList += (Convert.ToString("address_custom_data='" & " N'") & sCustom) & "'"
                            End If
                        Next
                    End If

                    Dim sQuery2 As String = (Convert.ToString((Convert.ToString((Convert.ToString("UPDATE ") & sTableName) & " ") & sList) & " WHERE ") & sIdName) & "=" & id

                    Dim iResult2 As Integer = ExecuteNon(sQuery2)

                    If iResult2 > 0 Then
                        Console.WriteLine(Convert.ToString((Convert.ToString(":) The row with ") & sIdName) & "d=" & id & " was updated in the table ") & sTableName)
                    Else
                        Console.WriteLine(Convert.ToString(":( Can not updated the row in the table ") & sTableName)
                    End If
                Else
                    For iCol As Integer = 0 To iFieldsNumber - 1
                        Dim isValid As Boolean = IsValidValue(tblTempTable.Columns(iCol), row(iCol))

                        If isValid Then
                            Dim sCvsFieldName As String = tblTempTable.Columns(iCol).ColumnName
                            Dim arRows As DataRow() = tblDictionary.[Select]((Convert.ToString("r4m_csv_field_name='") & sCvsFieldName) & "'")
                            If arRows.Length = 1 Then
                                Dim sFieldApiName As String = arRows(0)("api_field_name").ToString()
                                Dim sApiFieldType As String = arRows(0)("api_field_type").ToString()
                                Dim sCsvFieldType As String = arRows(0)("csv_field_type").ToString()

                                'sFields += prop.Name + ",";
                                If sFieldApiName = "day_scheduled_for_YYMMDD" Then
                                    sList += sFieldApiName & Convert.ToString(",")
                                    Dim oSchedule = ExceptFields2QueryValue(sFieldApiName, row(iCol))
                                    If oSchedule Is Nothing Then
                                        sValues += "null"
                                    Else
                                        sValues += "'" & oSchedule & "',"
                                    End If
                                Else
                                    Select Case tblTempTable.Columns(iCol).DataType.Name
                                        Case "String"
                                            sList += sFieldApiName & Convert.ToString(",")
                                            sValues += "N'" & row(iCol).ToString() & "',"
                                            Exit Select
                                        Case "Int32"
                                            sList += sFieldApiName & Convert.ToString(",")
                                            sValues += row(iCol).ToString() & ","
                                            Exit Select
                                        Case "Double"
                                            sList += sFieldApiName & Convert.ToString(",")
                                            sValues += row(iCol).ToString() & ","
                                            Exit Select
                                        Case "DateTime"
                                            Dim dt1900 As New DateTime(1900, 1, 1, 0, 0, 0)
                                            If DateTime.TryParse(row(iCol).ToString(), dt1900) Then
                                                dt1900 = Convert.ToDateTime(row(iCol))
                                                sList += sFieldApiName & Convert.ToString(",")
                                                If sApiFieldType <> sCsvFieldType AndAlso sApiFieldType = "int" Then
                                                    Dim iUnixTime As Long = R4MeUtils.ConvertToUnixTimestamp(dt1900)
                                                    sValues += iUnixTime & ","
                                                Else
                                                    If _conStngInstitute.ProviderName = "System.Data.OleDb" Then
                                                        sValues += "#" & dt1900.ToString("yyyy-MM-dd HH:mm:ss") & "#,"
                                                    Else
                                                        sValues += "'" & dt1900.ToString("yyyy-MM-dd HH:mm:ss") & "',"
                                                    End If
                                                End If
                                            End If
                                            Exit Select
                                    End Select

                                End If
                            End If
                        End If
                    Next

                    '#Region "custom fields in case of the addressbook contact, added in csv export as additional columns."
                    If tblTempTable.Columns.Count > 33 Then
                        Dim sbCustom As New System.Text.StringBuilder()
                        sbCustom.Append("{")
                        For iCol As Integer = 33 To tblTempTable.Columns.Count - 1
                            Dim isValid As Boolean = IsValidValue(tblTempTable.Columns(iCol), row(iCol))
                            If isValid Then
                                If tblTempTable.Columns(iCol).DataType.Name = "String" Then
                                    Dim sFieldName As String = tblTempTable.Columns(iCol).ColumnName
                                    Dim sValue As String = row(iCol).ToString()
                                    sbCustom.Append((Convert.ToString((Convert.ToString("""") & sFieldName) + """: """) & sValue) + """,")
                                End If
                            End If
                        Next
                        Dim sCustom As String = sbCustom.ToString()
                        sCustom = sCustom.TrimEnd(","c)
                        sCustom += "}"
                        If sCustom.Length > 3 Then
                            sList += "address_custom_data,"
                            sValues += (Convert.ToString(" N'") & sCustom) + "'"

                        End If
                    End If
                    '#End Region

                    sList = sList.TrimEnd(","c)
                    sList += ")"
                    sValues = sValues.TrimEnd(","c)
                    sValues += ")"
                    Dim sQuery1 As String = Convert.ToString((Convert.ToString((Convert.ToString("INSERT INTO ") & sTableName) & " ") & sList) & " ") & sValues

                    Dim iResult As Integer = ExecuteNon(sQuery1)

                    If iResult > 0 Then
                        Console.WriteLine(Convert.ToString((Convert.ToString(":) New row with ") & sIdName) & "=" & id & " was added to the table ") & sTableName)
                    Else
                        Console.WriteLine(Convert.ToString(":( Can not created new row in the table ") & sTableName)
                    End If
                End If
            Next
        End Sub

        ' Method for exporting addressbook data from SQL type server to the CSV file (with structure equal to the exported by Route4Me web UI CSV file)
        ' sFileName --- CSV file name.
        ' sTableName --- Server addressbook table name.
        ' WithId --- If true, CSV file will have first ID of SQL addressbook table (you need it for editing in CSV file and updating server table using Csv2Table method.
        Public Sub Table2Csv(sFileName As String, sTableName As String, WithId As Boolean)
            If Not CheckDataFolder(sFileName, True) Then
                Return
            End If

            Dim tblTemp As DataTable = fillTable(Convert.ToString("SELECT * FROM ") & sTableName)

            Dim lsCsvContent As New List(Of String)()

            Dim sFileHeader As String = ""
            If WithId Then
                sFileHeader += """" & tblTemp.Columns(0).ColumnName & ""","
            End If

            Dim tblDictionary As DataTable = GetCsv2ApiDictionary(sTableName)

            For Each dictRow As DataRow In tblDictionary.Rows
                sFileHeader += """" & dictRow("r4m_csv_field_name").ToString() & ""","
            Next

            ' Convert JSON string of the custom data to the csv fields (as they are represented in the exported from Route4Me csv file
            For Each row As DataRow In tblTemp.Rows
                If IsValidValue(tblTemp.Columns("address_custom_Data"), row("address_custom_Data")) Then
                    Dim jsSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim dict As Dictionary(Of String, Object) = DirectCast(jsSerializer.DeserializeObject(row("address_custom_Data").ToString()), Dictionary(Of String, Object))

                    For Each kvpair As KeyValuePair(Of String, Object) In dict
                        Dim fRows As DataRow() = tblDictionary.[Select]("r4m_csv_field_name='" & kvpair.Key & "'")

                        If fRows.Length < 1 Then
                            Dim newRow As DataRow = tblDictionary.NewRow()

                            newRow("r4m_csv_field_name") = kvpair.Key
                            newRow("table_name") = "addressbook_v4"
                            newRow("csv_field_nom") = tblDictionary.Rows.Count
                            newRow("api_field_name") = "_cf__" & kvpair.Key

                            tblDictionary.Rows.Add(newRow)

                            sFileHeader += """" & kvpair.Key & ""","

                        End If
                    Next
                End If
            Next

            sFileHeader = sFileHeader.TrimEnd(","c)

            lsCsvContent.Add(sFileHeader)

            For Each row As DataRow In tblTemp.Rows
                Dim sRow As String = ""

                If WithId Then
                    If IsValidValue(tblTemp.Columns(0), row(0)) Then
                        sRow += row(0).ToString() & ","
                    End If
                End If

                For Each dictRow As DataRow In tblDictionary.Rows
                    'string sCsvFieldName = dictRow["r4m_csv_field_name"].ToString();

                    Dim sApiFieldName As String = dictRow("api_field_name").ToString()

                    If sApiFieldName.IndexOf("_cf__") = 0 Then
                        Dim sKeyName As String = sApiFieldName.Substring(5)

                        If IsValidValue(tblTemp.Columns("address_custom_Data"), row("address_custom_Data")) Then
                            Dim jsSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                            Dim dict As Dictionary(Of String, Object) = DirectCast(jsSerializer.DeserializeObject(row("address_custom_Data").ToString()), Dictionary(Of String, Object))

                            Dim blExists As Boolean = False
                            For Each kvpair As KeyValuePair(Of String, Object) In dict
                                If kvpair.Key = sKeyName Then
                                    Dim sVal As String = kvpair.Value.ToString()
                                    sVal = sVal.Replace("""", """""")
                                    sRow += (Convert.ToString("""") & sVal) & ""","
                                    blExists = True
                                    Exit For
                                End If
                            Next

                            If Not blExists Then
                                sRow += ","
                            End If
                        Else
                            sRow += ","
                        End If
                    Else
                        Dim apiCol As DataColumn = tblTemp.Columns(sApiFieldName)

                        If IsValidValue(apiCol, row(apiCol.ColumnName)) Then
                            Select Case apiCol.DataType.ToString()
                                Case "System.String"
                                    Dim sVal As String = row(apiCol.ColumnName).ToString()
                                    sVal = sVal.Replace("""", """""")
                                    sRow += (Convert.ToString("""") & sVal) & ""","
                                    Exit Select
                                Case "System.DdateTime"
                                    sRow += """" & Convert.ToDateTime(row(apiCol.ColumnName)).ToString("yyyy-MM-dd HH:mm:ss") & ""","
                                    Exit Select
                                Case Else
                                    sRow += row(apiCol.ColumnName) & ","
                                    Exit Select

                            End Select
                        Else
                            sRow += ","
                        End If

                    End If
                Next

                sRow = sRow.TrimEnd(","c)
                lsCsvContent.Add(sRow)
            Next

            File.WriteAllLines(sFileName, lsCsvContent.ToArray())

            Console.WriteLine((Convert.ToString("The file ") & sFileName) & " was created. You can fill it with data for upoloading on the server.")
        End Sub

        ' Create data folder if it doesn't exist.
        Private Function CheckDataFolder(file_name As String, blCreateIfNotExists As Boolean) As Boolean
            Try
                Dim iDir As DirectoryInfo = Directory.GetParent(file_name)
                If File.Exists(iDir.FullName) Then

                    Return True
                Else
                    If blCreateIfNotExists Then
                        Directory.CreateDirectory(iDir.FullName)
                    End If
                    Return True

                End If
            Catch ex As Exception
                Console.WriteLine("Creation of the data folder failed... " + ex.Message)
                Return False
            End Try
        End Function

        ' Method FieldValue2QueryValue converts value of the type ttype to value for sqlquery operations (update, insert)
        Private Function FieldValue2QueryValue(ttype As Type, oValue As Object) As String
            Dim sQueryValue As String = ""

            Select Case ttype.Name
                Case "String"
                    sQueryValue = "N'" & oValue.ToString() & "'"
                    Exit Select
                Case "Int32"
                    sQueryValue = oValue.ToString()
                    Exit Select
                Case "Double"
                    sQueryValue = oValue.ToString()
                    Exit Select
                Case "DateTime"
                    Dim dt1900 As New DateTime(1900, 1, 1, 0, 0, 0)
                    If DateTime.TryParse(oValue.ToString(), dt1900) Then
                        dt1900 = Convert.ToDateTime(oValue)
                        If _conStngInstitute.ProviderName = "System.Data.OleDb" Then
                            sQueryValue = "#" & dt1900.ToString("yyyy-MM-dd HH:mm:ss") & "#"
                        Else
                            sQueryValue = "'" & dt1900.ToString("yyyy-MM-dd HH:mm:ss") & "'"
                        End If
                    End If
                    Exit Select
            End Select

            Return sQueryValue
        End Function

        Private Function ExceptFields2QueryValue(PropertyName As String, oValue As Object) As String
            Dim sQueryValue As String = ""
            If oValue Is Nothing Then
                Return "null"
            End If

            Select Case PropertyName
                Case "day_scheduled_for_YYMMDD"
                    Dim dt1900 As New DateTime(1900, 1, 1, 0, 0, 0)
                    If DateTime.TryParse(oValue.ToString(), dt1900) Then
                        dt1900 = Convert.ToDateTime(oValue)
                        Return dt1900.ToShortDateString()
                    Else
                        sQueryValue = Nothing
                    End If
                    Exit Select
                Case "EXT_FIELD_custom_data"
                    Dim sbOrderCustom As New System.Text.StringBuilder()
                    sbOrderCustom.Append("{")
                    For Each kvpair As KeyValuePair(Of String, Object) In DirectCast(oValue, Dictionary(Of String, Object))
                        If kvpair.Value Is Nothing Then
                            sbOrderCustom.Append("""" & kvpair.Key & """: null,")
                        Else
                            sbOrderCustom.Append("""" & kvpair.Key & """: """ + kvpair.Value.ToString() & """,")
                        End If
                    Next
                    sQueryValue = sbOrderCustom.ToString().TrimEnd(","c)
                    sQueryValue += "}"
                    Exit Select
                Case "address_custom_data"
                    Dim sbCustom As New System.Text.StringBuilder()
                    sbCustom.Append("{")
                    For Each kvpair As KeyValuePair(Of String, Object) In DirectCast(oValue, Dictionary(Of String, Object))
                        If kvpair.Value Is Nothing Then
                            sbCustom.Append("""" & kvpair.Key & """: null,")
                        Else
                            sbCustom.Append("""" & kvpair.Key & """: """ & kvpair.Value.ToString() & """,")
                        End If
                    Next
                    sQueryValue = sbCustom.ToString().TrimEnd(","c)
                    sQueryValue += "}"
                    Exit Select
                Case "schedule"
                    Dim serializer As New DataContractJsonSerializer(GetType(IList(Of Schedule)))

                    Using ms As New MemoryStream()
                        serializer.WriteObject(ms, oValue)
                        sQueryValue = System.Text.Encoding.[Default].GetString(ms.ToArray())
                    End Using
                    Exit Select
                Case "schedule_blacklist"
                    Dim sbBlackList As New System.Text.StringBuilder()
                    For Each dt1 As String In DirectCast(oValue, String())
                        sbBlackList.Append((Convert.ToString("""") & dt1) & """,")
                    Next
                    sQueryValue = sbBlackList.ToString()
                    sQueryValue = sQueryValue.TrimEnd(","c)
                    Exit Select

            End Select

            Return sQueryValue
        End Function

        ' Upload JSON response file, generated by the process of getting addressbook contacts by Route4Me API, to the SQL type server.
        Public Sub Json2Table(sFileName As String, sTableName As String, sIdName As String, r4m_dtype As R4M_DataType)
            If Not File.Exists(sFileName) Then
                Console.WriteLine((Convert.ToString("The file ") & sFileName) & " doesn't exist...")
                Return
            End If

            'string pathOnly = System.IO.Path.GetDirectoryName(sFileName);
            'string fileName = System.IO.Path.GetFileName(sFileName);

            Dim jsonContent As String = File.ReadAllText(sFileName)

            'DataTable tblTempTable = new DataTable();

            Dim jsSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

            Select Case r4m_dtype
                Case R4M_DataType.Addressbook
                    'var jsSerializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(AddressBookContactsResponse));
                    Dim Data As AddressBookContactsResponse = jsSerializer.Deserialize(Of AddressBookContactsResponse)(jsonContent)
                    If Data.total = 0 Then
                        Exit Select
                    End If

                    For Each contact As AddressBookContact In Data.results
                        Dim sQuery As String = ""
                        If IsNewAddressID("addressbook_v4", contact.address_id) Then
                            sQuery = "INSERT INTO addressbook_v4 "
                            Dim sFields As String = ""
                            Dim sValues As String = ""
                            For Each prop As System.Reflection.PropertyInfo In GetType(AddressBookContact).GetProperties()
                                If prop.Name = "address_id" Then
                                    Continue For
                                End If
                                If prop.Name = "ConvertBooleansToInteger" Then
                                    Continue For
                                End If
                                If prop.MemberType <> System.Reflection.MemberTypes.[Property] Then
                                    Continue For
                                End If
                                Dim vValue = contact.[GetType]().GetProperty(prop.Name).GetValue(contact, Nothing)
                                If vValue Is Nothing Then
                                    Continue For
                                End If

                                Console.WriteLine("Properyt type=" & prop.PropertyType.Name)

                                sFields += prop.Name & ","
                                If prop.Name = "address_custom_data" OrElse prop.Name = "schedule" OrElse prop.Name = "schedule_blacklist" Then
                                    sValues += "'" & ExceptFields2QueryValue(prop.Name, vValue) & "',"
                                Else
                                    sValues += FieldValue2QueryValue(vValue.[GetType](), vValue) & ","

                                End If
                            Next
                            sFields = sFields.TrimEnd(","c)
                            sValues = sValues.TrimEnd(","c)

                            sFields = (Convert.ToString("(") & sFields) & ")"
                            sValues = (Convert.ToString("(") & sValues) & ")"

                            sQuery += Convert.ToString(sFields & Convert.ToString(" VALUES ")) & sValues
                        Else
                            Dim address_id As Integer = CInt(contact.address_id)
                            sQuery = "UPDATE addressbook_v4 SET "
                            Dim sSet As String = ""
                            For Each prop As System.Reflection.PropertyInfo In GetType(AddressBookContact).GetProperties()
                                If prop.Name = "address_id" Then
                                    Continue For
                                End If
                                If prop.Name = "ConvertBooleansToInteger" Then
                                    Continue For
                                End If
                                If prop.MemberType <> System.Reflection.MemberTypes.[Property] Then
                                    Continue For
                                End If
                                Dim vValue = contact.[GetType]().GetProperty(prop.Name).GetValue(contact, Nothing)
                                If vValue Is Nothing Then
                                    Continue For
                                End If

                                Console.WriteLine("Properyt type=" & prop.PropertyType.Name)
                                If prop.Name = "address_custom_data" OrElse prop.Name = "schedule" OrElse prop.Name = "schedule_blacklist" Then
                                    sSet += prop.Name & "='" & ExceptFields2QueryValue(prop.Name, vValue) & "',"
                                Else
                                    sSet += prop.Name & "=" & FieldValue2QueryValue(vValue.[GetType](), vValue) & ","
                                End If
                            Next
                            sSet = sSet.TrimEnd(","c)
                            sQuery += (sSet & Convert.ToString(" WHERE address_id=")) & address_id
                        End If

                        ExecuteNon(sQuery)
                    Next
                    Exit Select
                Case R4M_DataType.Order
                    Dim ordersData As OrdersResponse = jsSerializer.Deserialize(Of OrdersResponse)(jsonContent)
                    If ordersData.total = 0 Then
                        Exit Select
                    End If

                    For Each order As Order In ordersData.results
                        Dim sQuery As String = ""
                        If IsNewOrderID("orders", order.order_id) Then
                            sQuery = "INSERT INTO orders "
                            Dim sFields As String = ""
                            Dim sValues As String = ""

                            For Each prop As System.Reflection.PropertyInfo In GetType(Order).GetProperties()
                                'if (prop.Name == "order_id") continue;
                                If prop.Name = "ConvertBooleansToInteger" Then
                                    Continue For
                                End If
                                If prop.MemberType <> System.Reflection.MemberTypes.[Property] Then
                                    Continue For
                                End If
                                Dim vValue = order.[GetType]().GetProperty(prop.Name).GetValue(order, Nothing)
                                If vValue Is Nothing Then
                                    Continue For
                                End If

                                Console.WriteLine("Properyt type=" & prop.PropertyType.Name)

                                sFields += prop.Name & ","
                                If prop.Name = "EXT_FIELD_custom_data" Then
                                    sValues += "'" & ExceptFields2QueryValue(prop.Name, vValue) & "',"
                                Else
                                    sValues += FieldValue2QueryValue(vValue.[GetType](), vValue) & ","

                                End If
                            Next
                            sFields = sFields.TrimEnd(","c)
                            sValues = sValues.TrimEnd(","c)

                            sFields = (Convert.ToString("(") & sFields) & ")"
                            sValues = (Convert.ToString("(") & sValues) & ")"


                            sQuery += Convert.ToString(sFields & Convert.ToString(" VALUES ")) & sValues
                        Else
                            Dim order_id As Integer = CInt(order.order_id)
                            sQuery = "UPDATE orders SET "
                            Dim sSet As String = ""

                            For Each prop As System.Reflection.PropertyInfo In GetType(Order).GetProperties()
                                If prop.Name = "order_id" Then
                                    Continue For
                                End If
                                If prop.Name = "ConvertBooleansToInteger" Then
                                    Continue For
                                End If
                                If prop.MemberType <> System.Reflection.MemberTypes.[Property] Then
                                    Continue For
                                End If
                                Dim vValue = order.[GetType]().GetProperty(prop.Name).GetValue(order, Nothing)
                                If vValue Is Nothing Then
                                    Continue For
                                End If

                                Console.WriteLine("Properyt type=" + prop.PropertyType.Name)
                                If prop.Name = "EXT_FIELD_custom_data" Then
                                    sSet += prop.Name & "='" & ExceptFields2QueryValue(prop.Name, vValue) & "',"
                                Else
                                    sSet += prop.Name & "=" & FieldValue2QueryValue(vValue.[GetType](), vValue) & ","
                                End If
                            Next
                            sSet = sSet.TrimEnd(","c)
                            sQuery += (sSet & Convert.ToString(" WHERE order_id=")) & order_id
                        End If
                        ExecuteNon(sQuery)
                    Next

                    Exit Select
                Case R4M_DataType.Route

                    Exit Select
            End Select
        End Sub

        Public Function IsNewAddress(sTableName As String, sIdName As String, AddressId As Integer) As Boolean
            Dim blNew As Boolean = True
            Dim sCom As String = (Convert.ToString((Convert.ToString("SELECT COUNT(*) as rba FROM ") & sTableName) & " WHERE ") & sIdName) & "=" & AddressId
            Dim result As Object = ExecuteScalar(sCom)
            Dim iRows As Integer = -1
            If Integer.TryParse(result.ToString(), iRows) Then
                iRows = Convert.ToInt32(result)
            End If
            If iRows > 0 Then
                blNew = False
            End If
            Return blNew
        End Function

        Public Function IsNewAddressID(sTableName As String, oAddressId As Object) As Boolean
            Dim blNew As Boolean = True
            Dim AddressId As Integer = -1
            If Integer.TryParse(oAddressId.ToString(), AddressId) Then
                AddressId = Convert.ToInt32(oAddressId)
            Else
                Return True
            End If

            Dim sCom As String = (Convert.ToString("SELECT COUNT(*) as rba FROM ") & sTableName) & " WHERE address_id=" & AddressId
            Dim result As Object = ExecuteScalar(sCom)
            Dim iRows As Integer = -1
            If Integer.TryParse(result.ToString(), iRows) Then
                iRows = Convert.ToInt32(result)
            End If
            If iRows > 0 Then
                blNew = False
            End If
            Return blNew
        End Function

        Public Function IsNewOrderID(sTableName As String, oOrderId As Object) As Boolean
            Dim blNew As Boolean = True
            Dim OrderId As Integer = -1
            If Integer.TryParse(oOrderId.ToString(), OrderId) Then
                OrderId = Convert.ToInt32(oOrderId)
            Else
                Return True
            End If

            Dim sCom As String = (Convert.ToString("SELECT COUNT(*) as rba FROM ") & sTableName) & " WHERE order_id=" & OrderId
            Dim result As Object = ExecuteScalar(sCom)
            Dim iRows As Integer = -1
            If Integer.TryParse(result.ToString(), iRows) Then
                iRows = Convert.ToInt32(result)
            End If
            If iRows > 0 Then
                blNew = False
            End If
            Return blNew
        End Function

        Public Function ExecuteScalar(sQuery As String) As Object
            Dim result As Object = Nothing
            Try
                Dim iResult As Integer = -1
                OpenConnection()
                _cmd.CommandText = sQuery
                result = _cmd.ExecuteScalar()

                If Integer.TryParse(result.ToString(), iResult) Then
                    iResult = Convert.ToInt32(result)
                End If
                Return iResult
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                Return 0
            Finally
                CloseConnection()
            End Try

        End Function

        Public Function ExecuteNon(sQuery As String) As Integer
            Try
                Dim iResult As Integer = -1
                _cmd.CommandText = sQuery
                OpenConnection()
                iResult = _cmd.ExecuteNonQuery()

                Return iResult
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                Return 0
            Finally
                CloseConnection()
            End Try

        End Function

        Public Function IsValidValue(col As DataColumn, value As Object) As Boolean
            Dim isValid As Boolean = False

            Dim sType As String = col.DataType.Name

            Select Case sType
                Case "Int32"
                    Dim i_val As Integer = -1
                    If Integer.TryParse(value.ToString(), i_val) Then
                        isValid = True
                    End If
                    Exit Select
                Case "String"
                    If value.ToString().Length > 0 Then
                        isValid = True
                    End If
                    Exit Select
                Case "Double"
                    Dim d_val As Double = 0
                    If Double.TryParse(value.ToString(), d_val) Then
                        isValid = True
                    End If
                    Exit Select
                Case "DateTime"
                    Dim dt1908 As New DateTime(1899, 1, 1, 0, 0, 0)
                    If DateTime.TryParse(value.ToString(), dt1908) Then
                        isValid = True
                    End If
                    Exit Select
            End Select

            Return isValid
        End Function

        Public Function fillTable(sSQLSelect As String) As DataTable
            Dim dtbElements As New DataTable()

            Try
                OpenConnection()

                _cmd.CommandText = sSQLSelect
                _adapter.SelectCommand = DirectCast(_cmd, DbCommand)

                _adapter.Fill(dtbElements)

                Return dtbElements
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                Return dtbElements
            Finally
                CloseConnection()
            End Try

        End Function

    End Class

End Namespace
