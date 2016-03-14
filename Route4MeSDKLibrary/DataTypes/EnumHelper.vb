Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Namespace Route4MeSDK.DataTypes
    Public Module EnumHelper
        Sub New()
        End Sub
        'Public Shared Function GetEnumDescription(value As String) As String
        '    Dim type As Type = GetType(T)
        '    Dim name = [Enum].GetNames(type).Where(Function(f) f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).[Select](Function(d) d).FirstOrDefault()
        '    'Dim name1 = [Enum].GetNames(type).Where(
        '    'Console.WriteLine(name1.Length)
        '    If name Is Nothing Then
        '        Return String.Empty
        '    End If
        '    Dim field = type.GetField(name)
        '    Dim customAttribute = field.GetCustomAttributes(GetType(DescriptionAttribute), False)
        '    Return If(customAttribute.Length > 0, DirectCast(customAttribute(0), DescriptionAttribute).Description, name)
        'End Function
        <Extension()> Public Function GetEnumDescription(ByVal EnumConstant As [Enum]) As String
            Dim attr() As DescriptionAttribute = DirectCast(EnumConstant.GetType().GetField(EnumConstant.ToString()).GetCustomAttributes(GetType(DescriptionAttribute), False), DescriptionAttribute())
            Return If(attr.Length > 0, attr(0).Description, EnumConstant.ToString)
        End Function
    End Module
End Namespace
