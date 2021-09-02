Imports System.Reflection
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Serialization

Namespace Route4MeSDK
    Public Class DataContractResolver
        Inherits DefaultContractResolver

        Public Property MandatoryFields As String()

        Public Sub New()

        End Sub

        Public Sub New(ByVal mandatoryFields As String())
            mandatoryFields = mandatoryFields
        End Sub

        Protected Overrides Function CreateProperty(ByVal member As MemberInfo, ByVal memberSerialization As MemberSerialization) As JsonProperty
            Dim [property] = MyBase.CreateProperty(member, memberSerialization)
            [property].DefaultValueHandling = DefaultValueHandling.Include

            If (If(MandatoryFields?.Length, 0)) > 0 Then
                Dim shouldSerialized As Boolean = If(MandatoryFields.Contains([property].PropertyName), True, False)
                [property].ShouldSerialize = Function(o) shouldSerialized
            End If

            Return [property]
        End Function
    End Class
End Namespace