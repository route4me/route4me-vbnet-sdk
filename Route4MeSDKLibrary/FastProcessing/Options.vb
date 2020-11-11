Imports Quobject.SocketIoClientDotNet.EngineIoClientDotNet.Client

Namespace Route4MeSDK.FastProcessing
    Public Class Options
        Inherits Transport.Options

        Public ForceNew As Boolean = True
        Public Multiplex As Boolean = True
        Public Host As String
        Public QueryString As String
        Public RememberUpgrade As Boolean
        Public Transports As Immutable.ImmutableList(Of String)
        Public Upgrade As Boolean
        Public Reconnection As Boolean = True
        Public ReconnectionAttempts As Integer
        Public ReconnectionDelay As Long
        Public ReconnectionDelayMax As Long
        Public Timeout As Long = -1
        Public AutoConnect As Boolean = True

        Public Sub New()
        End Sub

    End Class
End Namespace
