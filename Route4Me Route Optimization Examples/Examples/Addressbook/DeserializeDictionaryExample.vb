Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Runtime.Serialization.Json
Imports System.Runtime.Serialization

Imports System.IO
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Common
Imports System.Configuration
Imports System.Data.OleDb
Imports System.Web.Script


Namespace Route4MeSDKTest.Examples

    Partial Public NotInheritable Class Route4MeExamples
        Public Sub DeserializeDictionaryExample()
            Dim json As String
            json = My.Computer.FileSystem.ReadAllText("Data/JSON/dict_convert.json")
            Dim settings = New System.Runtime.Serialization.Json.DataContractJsonSerializerSettings()

            'Dim jsSerializer = New DataContractJsonSerializer(GetType(AddressBookContactsResponse))
            Dim jsSerializer = New Serialization.JavaScriptSerializer()

            Dim Data As AddressBookContactsResponse = jsSerializer.Deserialize(Of AddressBookContactsResponse)(json)

            Console.WriteLine("Total:" & Data.total)

        End Sub
    End Class
End Namespace
