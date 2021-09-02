Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes.V5

    ''' <summary>
    ''' Advanced constraints for route optimization process.
    ''' Set algorithm type to OPTIMIZATION_STATE_IN_QUEUE (9).
    ''' </summary>
    <DataContract>
    Public NotInheritable Class RouteAdvancedConstraints

        ''' <summary>
        ''' Maximum cargo volume per route
        ''' </summary>
        <DataMember(Name:="max_cargo_volume", EmitDefaultValue:=False)>
        Public Property MaximumCargoVolume As Double?

        ''' <summary>
        ''' Vehicle capacity.
        ''' <para>How much total cargo can be transported per route (units, e.g. cubic meters)</para>
        ''' </summary>
        <DataMember(Name:="max_capacity", EmitDefaultValue:=False)>
        Public Property MaximumCapacity As Integer?

        ''' <summary>
        ''' Legacy feature which permits a user to request an example number of optimized routes.
        ''' </summary>
        <DataMember(Name:="members_count", EmitDefaultValue:=False)>
        Public Property MembersCount As Integer?

        ''' <summary>
        ''' An array of the available time windows (e.g. [ [25200, 75000 ] )
        ''' </summary>
        <DataMember(Name:="available_time_windows", EmitDefaultValue:=False)>
        Public Property AvailableTimeWindows As List(Of Integer())

        ''' <summary>
        ''' The driver tags specified in a team member's custom data.
        ''' (e.g. "driver skills": 
        ''' ["Class A CDL", "Class B CDL", "Forklift", "Skid Steer Loader", "Independent Contractor"]
        ''' </summary>
        <DataMember(Name:="tags", EmitDefaultValue:=False)>
        Public Property Tags As String()

        ''' <summary>
        ''' An array of the skilled driver IDs.
        ''' </summary>
        <DataMember(Name:="route4me_members_id", EmitDefaultValue:=False)>
        Public Property Route4meMembersId As Integer()

        ''' <summary>
        ''' An array of the skilled driver IDs.
        ''' </summary>
        <DataMember(Name:="location_sequence_pattern", EmitDefaultValue:=False)>
        Public Property LocationSequencePattern As LocationSequencePattern()

    End Class


    <DataContract>
    Public Class LocationSequencePattern

        ''' <summary>
        ''' Address alias
        ''' </summary>
        <DataMember(Name:="alias", EmitDefaultValue:=False)>
        Public Property [Alias] As String

        ''' <summary>
        ''' Route destination address
        ''' </summary>
        <DataMember(Name:="address")>
        Public Property AddressString As String

        ''' <summary>
        ''' The latitude of this address
        ''' </summary>
        <DataMember(Name:="lat")>
        Public Property Latitude As Double

        ''' <summary>
        ''' The longitude of this address
        ''' </summary>
        <DataMember(Name:="lng")>
        Public Property Longitude As Double

        ''' <summary>
        ''' The expected amount of time that will be spent at this address by the driver/user.
        ''' </summary>
        <DataMember(Name:="time", EmitDefaultValue:=False)>
        Public Property Time As Long?

        ''' <summary>
        ''' Route destination ID
        ''' </summary>
        <DataMember(Name:="route_destination_id", EmitDefaultValue:=False)>
        Public Property RouteDestinationId As Integer?
    End Class

End Namespace