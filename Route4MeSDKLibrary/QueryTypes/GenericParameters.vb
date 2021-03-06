﻿Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports System.Collections.Specialized
Imports System.Reflection
Imports System.Runtime.Serialization
Imports System.Web

Namespace Route4MeSDK.QueryTypes
    ''' <summary>
    ''' Helper class, for easy REST query parameters string generation
    ''' 1. Use GenericParameters.Serialize() to generate the query string
    ''' 2. Use GenericParameters.ParametersCollection for adding query parameters
    ''' 3. Inherit this class, to create usable parameters holders
    '''    Add an attribute [HttpQueryMemberAttribute] on each property for serializing it automatically
    ''' 4. Modify ConvertBooleansToInteger.GenericParameters to serialize bool and bool? as "0" and "1"
    ''' Important: You have to add here all derived classes, that are serealized as json as a KnownType
    ''' </summary>
    <DataContract>
    <KnownType(GetType(OptimizationParameters))>
    <KnownType(GetType(AddressBookContact))>
    <KnownType(GetType(ActivityParameters))>
    <KnownType(GetType(AddressBookParameters))>
    <KnownType(GetType(AddressParameters))>
    <KnownType(GetType(GPSParameters))>
    <KnownType(GetType(NoteParameters))>
    <KnownType(GetType(RouteParameters))>
    <KnownType(GetType(RouteParametersQuery))>
    <KnownType(GetType(AvoidanceZoneParameters))>
    <KnownType(GetType(AvoidanceZoneQuery))>
    <KnownType(GetType(GeocodingParameters))>
    <KnownType(GetType(MemberParameters))>
    <KnownType(GetType(VehicleV4Parameters))>
    <KnownType(GetType(VehicleV4Response))>
    <KnownType(GetType(OrderCustomFieldParameters))>
    <KnownType(GetType(OrderFilterParameters))>
    Public Class GenericParameters
#Region "Fields"

        <IgnoreDataMember>
        Public ParametersCollection As New NameValueCollection()

        <IgnoreDataMember>
        Public Property ConvertBooleansToInteger() As Boolean
            Get
                Return m_ConvertBooleansToInteger
            End Get
            Protected Set(value As Boolean)
                m_ConvertBooleansToInteger = value
            End Set
        End Property
        Private m_ConvertBooleansToInteger As Boolean

#End Region

#Region "Methods"

        Public Sub New()
            Me.PrepareForSerialization()
        End Sub

        Public Sub PrepareForSerialization()
            ConvertBooleansToInteger = True
            If ParametersCollection Is Nothing Then
                ParametersCollection = New NameValueCollection()
            End If
        End Sub

        Public Function Serialize(Optional apiKey As String = Nothing) As String
            Dim paramsCollection = HttpUtility.ParseQueryString(String.Empty)

            paramsCollection.Add(ParametersCollection)

            Dim properties = [GetType]().GetProperties()

            'For Each [property] As Object In properties
            For Each [property] As PropertyInfo In properties
                'Dim attribute = TryCast([property].GetCustomAttribute(GetType(HttpQueryMemberAttribute)), HttpQueryMemberAttribute)
                Dim attribute As HttpQueryMemberAttribute = [property].GetCustomAttribute(GetType(HttpQueryMemberAttribute))
                'var attribute = property.GetCustomAttribute(typeof(HttpQueryMemberAttribute)) as HttpQueryMemberAttribute;

                If attribute IsNot Nothing Then
                    Dim valueObj = [property].GetValue(Me)
                    Dim value = If(valueObj IsNot Nothing, valueObj.ToString(), "null")

                    If ConvertBooleansToInteger AndAlso valueObj IsNot Nothing AndAlso ([property].PropertyType = GetType(Boolean) OrElse [property].PropertyType = GetType(Boolean?)) Then
                        value = If(CBool(valueObj), "1", "0")
                    End If

                    Dim name = If(attribute.Name, [property].Name)
                    Dim emit = valueObj <> attribute.DefaultValue OrElse attribute.EmitDefaultValue

                    If emit Then
                        paramsCollection.Add(name, value)
                    End If
                End If
            Next

            Dim apiKeyStr As String = If(String.IsNullOrEmpty(apiKey), "?", String.Format("?api_key={0}", apiKey))
            Dim result As String = If(paramsCollection.Count > 0, String.Format("{0}&{1}", apiKeyStr, paramsCollection.ToString()), apiKeyStr)

            Return result
        End Function

#End Region
    End Class
End Namespace
