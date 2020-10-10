Imports System

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' The class creates custom attribute IsReadOnly for marking 
    ''' the Route4Me object properties as read-only.
    ''' </summary>
    <AttributeUsage(AttributeTargets.[Property] Or AttributeTargets.Method Or AttributeTargets.Field)>
    Public Class ReadOnlyAttribute
        Inherits Attribute

        Private _isReadOnly As Boolean

        Public Sub New(ByVal IsReadOnly As Boolean)
            _isReadOnly = IsReadOnly
        End Sub

        Public Overridable Property IsReadOnly As Boolean
            Get
                Return _isReadOnly
            End Get
            Set(ByVal value As Boolean)
                _isReadOnly = value
            End Set
        End Property
    End Class
End Namespace
