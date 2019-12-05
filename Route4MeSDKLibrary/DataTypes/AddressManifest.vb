Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class AddressManifest
        <DataMember(Name:="running_service_time", EmitDefaultValue:=False)>
        Public Property RunningServiceTime As Long?

        <DataMember(Name:="running_travel_time", EmitDefaultValue:=False)>
        Public Property RunningTravelTime As Long?

        <DataMember(Name:="running_wait_time", EmitDefaultValue:=False)>
        Public Property RunningWaitTime As Long?

        <DataMember(Name:="running_distance", EmitDefaultValue:=False)>
        Public Property RunningDistance As Double?

        <DataMember(Name:="fuel_from_start", EmitDefaultValue:=False)>
        Public Property FuelFromStart As Double?

        <DataMember(Name:="fuel_cost_from_start", EmitDefaultValue:=False)>
        Public Property FuelCostFromStart As Double?

        <DataMember(Name:="projected_arrival_time_ts", EmitDefaultValue:=False)>
        Public Property ProjectedArrivalTimeTs As Long?

        <DataMember(Name:="projected_departure_time_ts", EmitDefaultValue:=False)>
        Public Property ProjectedDepartureTimeTs As Long?

        <DataMember(Name:="actual_arrival_time_ts", EmitDefaultValue:=False)>
        Public Property ActualArrivalTimeTs As Long?

        <DataMember(Name:="actual_departure_time_ts", EmitDefaultValue:=False)>
        Public Property ActualDepartureTimeTs As Long?

        <DataMember(Name:="estimated_arrival_time_ts", EmitDefaultValue:=False)>
        Public Property EstimatedArrivalTimeTs As Long?

        <DataMember(Name:="estimated_departure_time_ts", EmitDefaultValue:=False)>
        Public Property EstimatedDepartureTimeTs As Long?

        <DataMember(Name:="scheduled_arrival_time_ts", EmitDefaultValue:=False)>
        Public Property ScheduledArrivalTimeTs As Long?

        <DataMember(Name:="scheduled_departure_time_ts", EmitDefaultValue:=False)>
        Public Property ScheduledDepartureTimeTs As Long?

        <DataMember(Name:="time_impact", EmitDefaultValue:=False)>
        Public Property TimeImpact As Long?

        <DataMember(Name:="udu_running_distance", EmitDefaultValue:=False)>
        Public Property UduRunningDistance As Double?
    End Class
End Namespace