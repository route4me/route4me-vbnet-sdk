Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Namespace Route4MeSDK.DataTypes.V5
    Public Module EnumHelper
        Sub New()
        End Sub

        <Extension()> Public Function GetEnumDescription(ByVal EnumConstant As [Enum]) As String
            Dim attr() As DescriptionAttribute = DirectCast(EnumConstant.GetType().GetField(EnumConstant.ToString()).GetCustomAttributes(GetType(DescriptionAttribute), False), DescriptionAttribute())
            Return If(attr.Length > 0, attr(0).Description, EnumConstant.ToString)
        End Function
    End Module
End Namespace
