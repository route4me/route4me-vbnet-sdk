Imports System.Collections.Generic
Imports System.Dynamic
Imports System.Text
Imports System.Reflection
Imports System.Linq
Imports System.Runtime.InteropServices

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' For creating dynamic Route4Me data classes with the specified properties only.
    ''' For resolving update problem: If Not serialize null values, it's impossible to set null,
    ''' if enable null values, actual values could be rewritten with null values.
    ''' </summary>
    Public Class Route4MeDynamicClass
        Inherits DynamicObject

        Private ReadOnly _dynamicProperties As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()

        ''' <summary>
        ''' Tries to set member
        ''' </summary>
        ''' <param name="binder">Member binder</param>
        ''' <param name="value">Object value</param>
        ''' <returns>True, if a member binded successfully</returns>
        Public Overrides Function TrySetMember(ByVal binder As SetMemberBinder, ByVal value As Object) As Boolean
            _dynamicProperties.Add(binder.Name, value)
            Return True
        End Function

        ''' <summary>
        ''' Getter of the dynamic properties
        ''' </summary>
        Public ReadOnly Property DynamicProperties As Dictionary(Of String, Object)
            Get
                Return _dynamicProperties
            End Get
        End Property

        Public Overrides Function TryGetMember(ByVal binder As GetMemberBinder, <Out> ByRef result As Object) As Boolean
            Return _dynamicProperties.TryGetValue(binder.Name, result)
        End Function

        ''' <summary>
        ''' Tries to set member
        ''' </summary>
        ''' <returns>Multiline property name & value pairs</returns>
        Public Overrides Function ToString() As String
            Dim sb = New StringBuilder()

            For Each [property] In _dynamicProperties
                sb.AppendLine($"Property '{[property].Key}' = '{[property].Value}'")
            Next

            Return sb.ToString()
        End Function

        ''' <summary>
        ''' Copy a specified list of the properties of the Route4Me object to the dynamic class.
        ''' </summary>
        ''' <param name="r4mObject">Route4Me object</param>
        ''' <param name="propertyNames">A specified list of the Route4Me class properties</param>
        ''' <param name="errorString">Error string</param>
        Public Sub CopyPropertiesFromClass(ByVal r4mObject As Object, ByVal propertyNames As List(Of String), <Out> ByRef errorString As String)
            errorString = ""

            For Each propertyName In propertyNames
                Dim propInfo = If(r4mObject?.[GetType]()?.GetProperty(propertyName), Nothing)

                If propInfo Is Nothing Then Continue For

                Dim customAttribute = If(propInfo?.CustomAttributes?.First(), Nothing)

                If customAttribute Is Nothing Then Continue For

                Dim typedValue = If(customAttribute?.NamedArguments?.FirstOrDefault().TypedValue.Value?.ToString(), Nothing)

                If typedValue Is Nothing Then Continue For

                If typedValue = "IgnoreDataMemberAttribute" Then Continue For

                If Not _dynamicProperties.ContainsKey(typedValue) Then _dynamicProperties.Add(typedValue, r4mObject.[GetType]().GetProperty(propertyName).GetValue(r4mObject))
            Next
        End Sub
    End Class
End Namespace

