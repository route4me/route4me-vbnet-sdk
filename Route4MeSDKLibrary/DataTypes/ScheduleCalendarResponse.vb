Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Data structure of the schedule calendar response.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class ScheduleCalendarResponse
        Inherits GenericParameters

        ''' <summary>
        ''' The address book contact quantity by dates (dates are in the string format, e.g. 2020-10-18).
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="address_book", EmitDefaultValue:=False)>
        Public Property AddressBook As Dictionary(Of String, Integer)

        ''' <summary>
        ''' The order quantity by dates (dates are in the string format, e.g. 2020-10-18).
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="orders", EmitDefaultValue:=False)>
        Public Property Orders As Dictionary(Of String, Integer)

        ''' <summary>
        ''' The order quantity by dates (dates are in the string format, e.g. 2020-10-18).
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="routes_count", EmitDefaultValue:=False)>
        Public Property RoutesCount As Dictionary(Of String, Integer)
    End Class
End Namespace
