Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class GeocodingParameters
        Inherits GenericParameters

        <HttpQueryMemberAttribute(Name:="addresses", EmitDefaultValue:=False)> _
        Public Property Addresses() As String
            Get
                Return m_Addresses
            End Get
            Set(value As String)
                m_Addresses = value
            End Set
        End Property
        Private m_Addresses As String

        <HttpQueryMemberAttribute(Name:="format", EmitDefaultValue:=False)> _
        Public Property Format() As String
            Get
                Return m_Format
            End Get
            Set(value As String)
                m_Format = value
            End Set
        End Property
        Private m_Format As String

    End Class
End Namespace