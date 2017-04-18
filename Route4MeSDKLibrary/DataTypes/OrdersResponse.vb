Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract> _
    Public NotInheritable Class OrdersResponse
        Inherits GenericParameters
        <DataMember(Name:="results", EmitDefaultValue:=False)> _
        Public Property results() As Order()
            Get
                Return m_results
            End Get
            Set(value As Order())
                m_results = Value
            End Set
        End Property
        Private m_results As Order()

        <DataMember(Name:="total", EmitDefaultValue:=False)> _
        Public Property total() As System.Nullable(Of Integer)
            Get
                Return m_total
            End Get
            Set(value As System.Nullable(Of Integer))
                m_total = Value
            End Set
        End Property
        Private m_total As System.Nullable(Of Integer)
    End Class
End Namespace
