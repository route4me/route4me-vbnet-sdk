Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Configuration
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Runtime.Serialization.Json
Imports System.Text
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports Route4MeSDKLibrary.Route4MeSDK.Route4MeManager

Namespace Route4MeSDK
    ''' <summary>
    ''' Route4Me C# SDK helper methods
    ''' </summary>
    Public Module R4MeUtils
        Sub New()
        End Sub

        ''' <summary>
        ''' List of the standard types
        ''' </summary>
        Public lsStandardTypes As List(Of String) = New List(Of String)() From {
            {"String"},
            {"Boolean"},
            {"String[]"},
            {"Nullable`1"},
            {"Int32"},
            {"Double"},
            {"Int16"},
            {"Int64"},
            {"Single"},
            {"Decimal"}
        }

        ''' <summary>
        ''' Reads JSON object for a stream
        ''' Any DataContractJsonSerializer can be thrown
        ''' </summary>
        <Extension> _
        Public Function ReadObject(Of T)(stream As Stream) As T
            Dim settings = New DataContractJsonSerializerSettings() With { _
                .UseSimpleDictionaryFormat = True _
            }
            Dim parser = New DataContractJsonSerializer(GetType(T), settings)

            Return DirectCast(parser.ReadObject(stream), T)
        End Function

        <Extension()>
        Public Function ReadObjectNew(Of T)(ByVal stream As Stream) As T
            Dim jsonSettings = New JsonSerializerSettings() With {
                .ContractResolver = New DataContractResolver()
            }
            Dim reader As StreamReader = New StreamReader(stream)
            Dim text As String = reader.ReadToEnd()

            If GetType(T) = GetType(GetAddressBookContactsResponse) Then
                Dim pattern As String = String.Concat("\""schedule\""", ":({[\s\S\n\d\w]*}),", """")
                Dim rgx As Regex = New Regex(pattern)
                Dim rslt As Match = rgx.Match(text)

                If rslt.Success Then
                    text = text.Replace(rslt.Groups(1).ToString(), "[" & rslt.Groups(1).ToString() & "]")
                End If
            End If

            Return JsonConvert.DeserializeObject(Of T)(text, jsonSettings)
        End Function

        Public Function ReadObjectNew(Of T)(ByVal jsonText As String) As T
            Dim jsonSettings = New JsonSerializerSettings() With {
        .ContractResolver = New DataContractResolver()
    }
            Return JsonConvert.DeserializeObject(Of T)(jsonText, jsonSettings)
        End Function

        ''' <summary>
        ''' Reads a stream to a string
        ''' </summary>
        <Extension> _
        Public Function ReadString(stream As Stream) As String
            Dim reader As New StreamReader(stream)

            Dim result As String = reader.ReadToEnd()

            Return result
        End Function

        ''' <summary>
        ''' Serialized an object to a string as JSON
        ''' Any DataContractJsonSerializer can be thrown
        ''' </summary>
        ''' <param name="obj">An object to serialize</param>
        ''' <returns></returns>
        Public Function SerializeObjectToJson(ByVal obj As Object) As String
            Dim settings = New DataContractJsonSerializerSettings() With {
                .UseSimpleDictionaryFormat = True
            }
            Dim writer = New DataContractJsonSerializer(obj.[GetType](), settings)
            Dim result As String = Nothing

            Using memoryStream = New MemoryStream()
                If obj Is Nothing Then Return result

                Try
                    writer.WriteObject(memoryStream, obj)
                Catch ex As Exception
                    result = SerializeObjectToJson(obj, True)
                    Return result
                End Try

                result = Encoding.UTF8.GetString(memoryStream.ToArray())
            End Using

            Return result
        End Function

        Public Function SerializeObjectToJson(ByVal obj As Object, ByVal ignoreNullValues As Boolean) As String
            Dim jsonSettings = New JsonSerializerSettings() With {
                .ContractResolver = New DataContractResolver()
            }
            Dim result As String = JsonConvert.SerializeObject(obj, Formatting.None, jsonSettings)
            Return result
        End Function

        ''' <summary>
        ''' Returns the DescriptionAttribute of a enum value
        ''' </summary>
        Public Function Description(enumValue As [Enum]) As String
            Dim field As FieldInfo = enumValue.[GetType]().GetField(enumValue.ToString())

            Dim attribute__1 = TryCast(Attribute.GetCustomAttribute(field, GetType(DescriptionAttribute)), DescriptionAttribute)
            Dim result As String = If(attribute__1 Is Nothing, enumValue.ToString(), attribute__1.Description)
            'Dim result As String = If(attribute__1 Is Nothing, enumValue.ToString(), AddressOf attribute__1.Description)

            Return result
        End Function

        ''' <summary>
        ''' IEnumerable extension method, performs 'action' for each IEnumerable item
        ''' source value can be null
        ''' </summary>
        Public Sub ForEach(Of T)(source As IEnumerable(Of T), action As Action(Of T))
            If source Is Nothing Then
                Return
            End If

            For Each item As Object In source
                action(item)
            Next
        End Sub

        ''' <summary>
        ''' Convert DateTime to Unix epoch time
        ''' </summary>
        Public Function ConvertToUnixTimestamp([date] As DateTime) As Long
            Dim origin As New DateTime(1970, 1, 1, 0, 0, 0, _
                0, DateTimeKind.Utc)
            Dim diff As TimeSpan = [date].ToUniversalTime() - origin
            Return CLng(Math.Floor(diff.TotalSeconds))
        End Function

        ''' <summary>
        ''' Convert DateTime from Unix epoch time
        ''' </summary>
        Public Function ConvertFromUnixTimestamp(timestamp As Long) As DateTime
            Dim origin As New DateTime(1970, 1, 1, 0, 0, 0,
                0, DateTimeKind.Utc)
            Return origin.AddSeconds(timestamp)
        End Function

        ''' <summary>
        ''' Creates deep clone of the Route4Me object
        ''' </summary>
        ''' <typeparam name="T">Route4Me object type</typeparam>
        ''' <param name="obj">Route4Me object</param>
        ''' <returns>Route4Me object clone</returns>
        Public Function ObjectDeepClone(Of T As Class)(ByVal obj As T) As T
            Dim clonedObject As T = Nothing

            Try
                Dim jsonString = SerializeObjectToJson(obj, False)
                Dim stream = StringToStream(jsonString)
                clonedObject = ReadObjectNew(Of T)(stream)
            Catch __unusedException1__ As Exception
                clonedObject = Nothing
            End Try

            Return clonedObject
        End Function

        Public Function StringToStream(ByVal src As String) As Stream
            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(src)
            Return New MemoryStream(byteArray)
        End Function

        ''' <summary>
        ''' Compares two Route4Me objects with equal types And returns a list
        ''' of the property names with different values.
        ''' </summary>
        ''' <param name="modifiedObject">Modified Route4Me object</param>
        ''' <param name="initialObject">Initial Route4Me object</param>
        ''' <param name="errorString">Error string</param>
        ''' <param name="excludeReadonly"></param>
        ''' <returns>List of the property names</returns>
        Public Function GetPropertiesWithDifferentValues(ByVal modifiedObject As Object, ByVal initialObject As Object, ByRef errorString As String, ByVal Optional excludeReadonly As Boolean = True) As List(Of String)
            Dim propNames = New List(Of String)()
            errorString = ""

            Try
                Dim jsonModifiedObject = JsonConvert.SerializeObject(modifiedObject)
                Dim jsonInitialObject = JsonConvert.SerializeObject(initialObject)
                If jsonModifiedObject.Equals(jsonInitialObject) Then Return propNames
            Catch ex As Exception
                errorString = ex.Message
                Return propNames
            End Try

            If modifiedObject Is Nothing Then
                errorString = "The modified object should not be null"
                Return Nothing
            End If

            Dim properties = modifiedObject.[GetType]().GetProperties()

            If initialObject Is Nothing Then
                Return properties.[Select](Function(x) x.Name).ToList()
            End If

            If modifiedObject.[GetType]() <> initialObject.[GetType]() Then
                errorString = "The objects should have equal types"
                Return Nothing
            End If

            For Each propInfo In properties
                If CheckIfPropertyHasIgnoreAttribute(propInfo) Then Continue For
                If excludeReadonly AndAlso CheckIfPropertyHasReadOnlyAttribute(propInfo) Then Continue For
                Dim modifiedObjectPropertyValue As Object = propInfo.GetValue(modifiedObject)
                Dim initialObjectPropertyValue As Object = propInfo.GetValue(initialObject)

                If modifiedObjectPropertyValue Is Nothing Then
                    If initialObjectPropertyValue IsNot Nothing Then propNames.Add(propInfo.Name)
                    Continue For
                End If

                If initialObjectPropertyValue Is Nothing Then
                    If modifiedObjectPropertyValue IsNot Nothing Then propNames.Add(propInfo.Name)
                    Continue For
                End If

                Try
                    Dim jsonModifiedObjectPropertyValue = JsonConvert.SerializeObject(modifiedObjectPropertyValue)
                    Dim jsonInitialObjectPropertyValue = JsonConvert.SerializeObject(initialObjectPropertyValue)
                    If jsonModifiedObjectPropertyValue.Equals(jsonInitialObjectPropertyValue) Then Continue For
                Catch __unusedException1__ As Exception
                    Continue For
                End Try

                propNames.Add(propInfo.Name)
            Next

            Return propNames
        End Function

        ''' <summary>
        ''' Checks if the property value Is Dictionary type.
        ''' </summary>
        ''' <param name="propValue">The property value</param>
        ''' <returns>True, if the property value Is the Dictionary type.</returns>
        Public Function IsPropertyDictionary(ByVal propValue As Object) As Boolean
            If propValue Is Nothing Then Return False
            Dim isDictionary As Boolean = TypeOf propValue Is IDictionary
            Dim isGenericType As Boolean = isDictionary AndAlso propValue.[GetType]().IsGenericType
            Return isDictionary AndAlso isGenericType
        End Function

        ''' <summary>
        ''' Checks if the property value Is Object type.
        ''' </summary>
        ''' <param name="propValue">The property value</param>
        ''' <returns>True, if the property value Is Object type.</returns>
        Public Function IsPropertyObject(ByVal propValue As Object) As Boolean
            If propValue Is Nothing Then Return False
            If IsPropertyArray(propValue) Then Return False
            If IsPropertyDictionary(propValue) Then Return False

            If propValue.[GetType]().IsClass Then
                Dim propType As String = propValue.[GetType]().ToString().Replace("System.", "")
                Return If(lsStandardTypes.Contains(propType), False, True)
            End If

            Return False
        End Function

        ''' <summary>
        ''' Checks if the property value Is Array type.
        ''' </summary>
        ''' <param name="propValue">The property value</param>
        ''' <returns>True, if the property value Is Array type.</returns>
        Public Function IsPropertyArray(ByVal propValue As Object) As Boolean
            If propValue Is Nothing Then Return False
            Return propValue.[GetType]().IsArray
        End Function

        ''' <summary>
        ''' Checks if a property has attribute IgnoreDataMember
        ''' </summary>
        ''' <param name="propInfo">A property to be checked</param>
        ''' <returns>True if the property has attribute IgnoreDataMember</returns>
        Public Function CheckIfPropertyHasIgnoreAttribute(ByVal propInfo As PropertyInfo) As Boolean
            Dim ignoreProperties = propInfo.GetCustomAttributes(False).ToDictionary(Function(a) a.[GetType]().Name, Function(a) a)
            Return If(ignoreProperties.Keys.Contains("IgnoreDataMemberAttribute"), True, False)
        End Function

        ''' <summary>
        ''' Checks if a Route4Me object property has read-only attribute.
        ''' </summary>
        ''' <param name="propInfo">Route4Me object property info</param>
        ''' <returns>True, if a Route4Me object property Is read-only</returns>
        Public Function CheckIfPropertyHasReadOnlyAttribute(ByVal propInfo As PropertyInfo) As Boolean
            Dim attributes = propInfo.GetCustomAttributes(False).ToDictionary(Function(a) a.[GetType]().Name, Function(a) a)

            If Not attributes.ContainsKey("ReadOnlyAttribute") Then Return False

            Dim [ReadOnly] As Object = Nothing

            attributes.TryGetValue("ReadOnlyAttribute", [ReadOnly])

            Dim isReadOnlyValue = If([ReadOnly] IsNot Nothing, (CType([ReadOnly], Route4MeSDK.DataTypes.ReadOnlyAttribute)).ReadOnly, False)

            Return isReadOnlyValue
        End Function

        ''' <summary>
        ''' Returns numeration of the Route4Me object proeprties.
        ''' </summary>
        ''' <typeparam name="T">Class type</typeparam>
        ''' <returns>Property postions in the class</returns>
        Public Function GetPropertyPositions(Of T As Class)() As Dictionary(Of String, Integer)
            Dim properties = GetType(T).GetProperties()
            Dim propertyPositions = New Dictionary(Of String, Integer)()

            For i As Integer = 0 To properties.Length - 1
                propertyPositions.Add(properties(i).Name, i)
            Next

            Return propertyPositions
        End Function

        ''' <summary>
        ''' Returns ordered property names.
        ''' </summary>
        ''' <typeparam name="T">Type of the Route4Me object</typeparam>
        ''' <param name="propertyNames">List of the property name</param>
        ''' <param name="errorString">Error string</param>
        ''' <returns>Ordered list of the property names</returns>
        Public Function OrderPropertiesByPosition(Of T As Class)(ByVal propertyNames As List(Of String), ByRef errorString As String) As List(Of String)
            errorString = ""
            Dim propertyPositions = GetPropertyPositions(Of T)()
            Dim orderedPropertyNames = New List(Of String)()

            For Each propKey In propertyPositions.Keys
                For Each propName In propertyNames
                    If propKey = propName Then
                        orderedPropertyNames.Add(propKey)
                        Exit For
                    End If
                Next

                If orderedPropertyNames.Count = propertyNames.Count Then Exit For
            Next

            If orderedPropertyNames.Count < propertyNames.Count Then errorString = "Some of the properties have the wrong name"
            Return orderedPropertyNames
        End Function

        ''' <summary>
        ''' Converts an input object to type TValue.
        ''' </summary>
        ''' <typeparam name="TValue">Target type</typeparam>
        ''' <param name="obj">An object to be converted to a TValue type</param>
        ''' <param name="errorString">Error string</param>
        ''' <returns>An object of TValue type</returns>
        Public Function ToObject(Of TValue)(ByVal obj As Object, ByRef errorString As String) As TValue
            errorString = ""
            If obj Is Nothing Then Return Nothing

            Try
                Dim json = JsonConvert.SerializeObject(obj)

                If json = "[]" Then Return Nothing

                Dim objectValue = JsonConvert.DeserializeObject(Of TValue)(json)

                Return objectValue
            Catch ex As Exception
                errorString = ex.Message
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Converts one standard type object to other.
        ''' </summary>
        ''' <typeparam name="T">Destincation type for converting to</typeparam>
        ''' <param name="value">Input value of the object type</param>
        ''' <returns>Converted value of the type T</returns>
        Public Function ConvertObjectToType(Of T As Structure)(ByRef value As Object) As T
            Dim result As T = Nothing

            If value Is Nothing Then Return result

            Dim destinationType As Type = result.[GetType]()

            If Nullable.GetUnderlyingType(destinationType) IsNot Nothing Then destinationType = Nullable.GetUnderlyingType(destinationType)

            Dim convertObjectType As Type = If(value?.[GetType](), Nothing)

            If destinationType Is Nothing OrElse convertObjectType Is Nothing Then Return result

            If destinationType = GetType(Object) Then Return result

            If TypeOf value Is IConvertible Then
                Try
                    If destinationType = GetType(Boolean) Then
                        result = CType(CObj((CType(value, IConvertible)).ToBoolean(CultureInfo.CurrentCulture)), T)
                    ElseIf destinationType = GetType(Byte) Then
                        result = CType(CObj((CType(value, IConvertible)).ToByte(CultureInfo.CurrentCulture)), T)
                    ElseIf destinationType = GetType(Char) Then
                        result = CType(CObj((CType(value, IConvertible)).ToChar(CultureInfo.CurrentCulture)), T)
                    ElseIf destinationType = GetType(DateTime) Then
                        result = CType(CObj((CType(value, IConvertible)).ToDateTime(CultureInfo.CurrentCulture)), T)
                    ElseIf destinationType = GetType(Decimal) Then
                        result = CType(CObj((CType(value, IConvertible)).ToDecimal(CultureInfo.CurrentCulture)), T)
                    ElseIf destinationType = GetType(Double) Then
                        result = CType(CObj((CType(value, IConvertible)).ToDouble(CultureInfo.CurrentCulture)), T)
                    ElseIf destinationType = GetType(Int16) Then
                        result = CType(CObj((CType(value, IConvertible)).ToInt16(CultureInfo.CurrentCulture)), T)
                    ElseIf destinationType = GetType(Int32) Then
                        result = CType(CObj((CType(value, IConvertible)).ToInt32(CultureInfo.CurrentCulture)), T)
                    ElseIf destinationType = GetType(Int64) Then
                        result = CType(CObj((CType(value, IConvertible)).ToInt64(CultureInfo.CurrentCulture)), T)
                    ElseIf destinationType = GetType(SByte) Then
                        result = CType(CObj((CType(value, IConvertible)).ToSByte(CultureInfo.CurrentCulture)), T)
                    ElseIf destinationType = GetType(Single) Then
                        result = CType(CObj((CType(value, IConvertible)).ToSingle(CultureInfo.CurrentCulture)), T)
                    ElseIf destinationType = GetType(UInt16) Then
                        result = CType(CObj((CType(value, IConvertible)).ToUInt16(CultureInfo.CurrentCulture)), T)
                    ElseIf destinationType = GetType(UInt32) Then
                        result = CType(CObj((CType(value, IConvertible)).ToUInt32(CultureInfo.CurrentCulture)), T)
                    ElseIf destinationType = GetType(UInt64) Then
                        result = CType(CObj((CType(value, IConvertible)).ToUInt64(CultureInfo.CurrentCulture)), T)
                    ElseIf destinationType = GetType(String) Then
                        result = CType(CObj((CType(value, IConvertible)).ToString(CultureInfo.CurrentCulture)), T)
                    End If

                Catch
                    Return result
                End Try
            End If

            Return result
        End Function

        ''' <summary>
        ''' Converts a standard type object to standard property type
        ''' (e.g. if first Is Long type, second: Int32, converts to Int32)
        ''' </summary>
        ''' <param name="value">An object to be converted to the target object type</param>
        ''' <param name="targetProperty">A property with the standard type</param>
        ''' <returns>Converted object to the target standard type</returns>
        Public Function ConvertObjectToPropertyType(ByVal value As Object, ByVal targetProperty As PropertyInfo) As Object
            Dim destinationType As Type = If(targetProperty?.PropertyType, Nothing)

            If Nullable.GetUnderlyingType(targetProperty.PropertyType) IsNot Nothing Then destinationType = Nullable.GetUnderlyingType(targetProperty.PropertyType)

            Dim convertObjectType As Type = If(value?.[GetType](), Nothing)

            If destinationType Is Nothing OrElse convertObjectType Is Nothing Then Return Nothing
            If targetProperty.PropertyType.Name.ToLower().Contains("dictionary") Then Return value

            If destinationType = GetType(Object) Then
                If IsPropertyDictionary(value) Then
                    value = CType((CObj(value)), Dictionary(Of String, String))
                    Return value
                Else
                    Return Nothing
                End If
            End If

            Dim result As Object = Nothing

            If destinationType.BaseType.Name = "Enum" OrElse destinationType.BaseType.Name = "Array" Then
                Return value
            End If

            If TypeOf value Is IConvertible Then
                Try
                    If destinationType = GetType(Boolean) Then
                        result = (CType(value, IConvertible)).ToBoolean(CultureInfo.CurrentCulture)
                    ElseIf destinationType = GetType(Byte) Then
                        result = (CType(value, IConvertible)).ToByte(CultureInfo.CurrentCulture)
                    ElseIf destinationType = GetType(Char) Then
                        result = (CType(value, IConvertible)).ToChar(CultureInfo.CurrentCulture)
                    ElseIf destinationType = GetType(DateTime) Then
                        result = (CType(value, IConvertible)).ToDateTime(CultureInfo.CurrentCulture)
                    ElseIf destinationType = GetType(Decimal) Then
                        result = (CType(value, IConvertible)).ToDecimal(CultureInfo.CurrentCulture)
                    ElseIf destinationType = GetType(Double) Then
                        result = (CType(value, IConvertible)).ToDouble(CultureInfo.CurrentCulture)
                    ElseIf destinationType = GetType(Int16) Then
                        result = (CType(value, IConvertible)).ToInt16(CultureInfo.CurrentCulture)
                        Return True
                    ElseIf destinationType = GetType(Int32) Then
                        result = (CType(value, IConvertible)).ToInt32(CultureInfo.CurrentCulture)
                    ElseIf destinationType = GetType(Int64) Then
                        result = (CType(value, IConvertible)).ToInt64(CultureInfo.CurrentCulture)
                    ElseIf destinationType = GetType(SByte) Then
                        result = (CType(value, IConvertible)).ToSByte(CultureInfo.CurrentCulture)
                    ElseIf destinationType = GetType(Single) Then
                        result = (CType(value, IConvertible)).ToSingle(CultureInfo.CurrentCulture)
                    ElseIf destinationType = GetType(UInt16) Then
                        result = (CType(value, IConvertible)).ToUInt16(CultureInfo.CurrentCulture)
                    ElseIf destinationType = GetType(UInt32) Then
                        result = (CType(value, IConvertible)).ToUInt32(CultureInfo.CurrentCulture)
                    ElseIf destinationType = GetType(UInt64) Then
                        result = (CType(value, IConvertible)).ToUInt64(CultureInfo.CurrentCulture)
                    ElseIf destinationType = GetType(String) Then
                        result = (CType(value, IConvertible)).ToString(CultureInfo.CurrentCulture)
                    End If

                Catch
                    Return result
                End Try
            End If

            Return result
        End Function

        ''' <summary>
        ''' Read a setting parameter from App.config by specified key
        ''' </summary>
        ''' <param name="key">A seting key</param>
        Public Function ReadSetting(key As String) As String
            Try
                Dim appSettings = ConfigurationManager.AppSettings
                Dim result As String = appSettings(key)

                If IsNothing(result) Then
                    Return "Not found"
                End If

                Return result
            Catch e As ConfigurationErrorsException
                Return "Error reading app settings"
            End Try
        End Function
    End Module
End Namespace

