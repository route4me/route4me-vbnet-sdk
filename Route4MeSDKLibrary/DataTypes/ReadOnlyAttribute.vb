Imports System

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' The class creates custom attribute IsReadOnly for marking 
    ''' the Route4Me object properties as read-only.
    ''' </summary>
    <AttributeUsage(AttributeTargets.[Property] Or AttributeTargets.Method Or AttributeTargets.Field)>
    Public Class ReadOnlyAttribute
        Inherits Attribute

        Private _ReadOnly As Boolean

        Public Sub New(ByVal [ReadOnly] As Boolean)
            _ReadOnly = [ReadOnly]
        End Sub

        Public Overridable Property [ReadOnly] As Boolean
            Get
                Return _ReadOnly
            End Get
            Set(ByVal value As Boolean)
                _ReadOnly = value
            End Set
        End Property
    End Class
End Namespace
