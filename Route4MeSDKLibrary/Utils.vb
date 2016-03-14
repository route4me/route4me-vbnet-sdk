Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.IO
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Runtime.Serialization.Json
Imports System.Text

Namespace Route4MeSDK
    ''' <summary>
    ''' Route4Me C# SDK helper methods
    ''' </summary>
    Public Module R4MeUtils
        Sub New()
        End Sub
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
        Public Function SerializeObjectToJson(obj As Object) As String
            Dim settings = New DataContractJsonSerializerSettings() With { _
                .UseSimpleDictionaryFormat = True _
            }
            Dim writer = New DataContractJsonSerializer(obj.[GetType](), settings)
            Dim result As String = Nothing

            Using memoryStream = New MemoryStream()
                writer.WriteObject(memoryStream, obj)

                result = Encoding.[Default].GetString(memoryStream.ToArray())
            End Using

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
            Dim origin As New DateTime(1970, 1, 1, 0, 0, 0, _
                0, DateTimeKind.Utc)
            Return origin.AddSeconds(timestamp)
        End Function
    End Module
End Namespace

