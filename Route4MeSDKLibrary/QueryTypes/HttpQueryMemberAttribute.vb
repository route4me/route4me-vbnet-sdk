Namespace Route4MeSDK.QueryTypes

    Public NotInheritable Class HttpQueryMemberAttribute
        Inherits Attribute
#Region "Properties"

        ''' <summary>
        ''' The serialized argument name, if specifed overrides the property name
        ''' </summary>
        Public Property Name As String

        ''' <summary>
        ''' Specifies whether to emit the property value, if its value is a default value
        ''' </summary>
        Public Property EmitDefaultValue As Boolean

        ''' <summary>
        ''' Specifies the default value, that is used when emiting the property value
        ''' If not specified null is used as a default value
        ''' </summary>
        Public Property DefaultValue As Object

#End Region

#Region "Methods"

        Public Sub New()
            EmitDefaultValue = True
        End Sub

#End Region
    End Class
End Namespace
