Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Quobject.SocketIoClientDotNet.EngineIoClientDotNet.Modules
Imports IO = Quobject.SocketIoClientDotNet.Client.IO

Namespace Route4MeSDK.FastProcessing
    Public Class Connection
        Public ReadOnly TIMEOUT As Integer = 300000

        Shared Sub New()
            LogManager.SetupLogManager()
        End Sub

        Protected Function CreateOptions() As IO.Options
            Dim log = LogManager.GetLogger([Global].CallerName())
            Dim config = ConfigBase.Load()
            Dim options = New IO.Options()
            options.Hostname = config.server.hostname
            options.ForceNew = True
            options.Secure = True
            options.Port = If(options.Secure, 443, 8080)
            options.Reconnection = True
            Return options
        End Function

        Protected Function CreateUri() As String
            Dim options = CreateOptions()
            Dim uri = If(ConnectionConstants.url IsNot Nothing, ConnectionConstants.url, String.Format("{0}://{1}:{2}/{3}/", If(options.Secure, "https", "http"), options.Hostname, options.Port, ConnectionConstants.ROUTE))
            Return uri
        End Function

        Protected Function CreateOptionsSecure() As IO.Options
            Dim log = LogManager.GetLogger([Global].CallerName())
            Dim config = ConfigBase.Load()
            Dim options = New IO.Options()
            options.Port = config.server.ssl_port
            options.Hostname = config.server.hostname
            log.Info("Please add to your hosts file: 127.0.0.1 " & options.Hostname)
            options.Secure = True
            options.IgnoreServerCertificateValidation = True
            Return options
        End Function
    End Class

    Public Class ConfigBase
        Public Property version As String
        Public Property server As ConfigServer

        Public Shared Function Load() As ConfigBase
            Dim result = New ConfigBase() With {
                .server = New ConfigServer()
            }
            result.server.hostname = ConnectionConstants.HOSTNAME
            result.server.port = ConnectionConstants.PORT
            result.server.ssl_port = ConnectionConstants.SSL_PORT
            Return result
        End Function
    End Class

    Public Class ConfigServer
        Public Property hostname As String
        Public Property port As Integer
        Public Property ssl_port As Integer
    End Class
End Namespace

