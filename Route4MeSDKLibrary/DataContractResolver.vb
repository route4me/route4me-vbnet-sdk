Imports System.Reflection
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Serialization

Namespace Route4MeSDK
    Public Class DataContractResolver
        Inherits DefaultContractResolver

        Protected Overrides Function CreateProperty(ByVal member As MemberInfo, ByVal memberSerialization As MemberSerialization) As JsonProperty
            Dim [property] = MyBase.CreateProperty(member, memberSerialization)
            [property].NullValueHandling = NullValueHandling.Include
            [property].DefaultValueHandling = DefaultValueHandling.Include
            [property].ShouldSerialize = Function(o) True
            Return [property]
        End Function
    End Class
End Namespace