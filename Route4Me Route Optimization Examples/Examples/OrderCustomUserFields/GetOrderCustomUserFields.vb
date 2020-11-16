Imports Route4MeSDKLibrary.Route4MeSDK

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get the order custom user fields.
        ''' </summary>
        Public Sub GetOrderCustomUserFields()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim errorString As String = Nothing
            Dim orderCustomUserFields = route4Me.GetOrderCustomUserFields(errorString)

            PrintOrderCustomField(orderCustomUserFields, errorString)
        End Sub
    End Class
End Namespace
