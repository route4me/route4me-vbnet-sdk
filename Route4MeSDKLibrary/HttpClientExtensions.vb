Imports System.Threading
Imports System.Runtime.CompilerServices
Imports System.Net.Http

Namespace System.Net.Http
    Module HttpClientExtensions

        ''' <summary>
        ''' Send a PATCH request to the specified Uri as an asynchronous operation.
        ''' </summary>
        ''' 
        ''' <returns>
        ''' Returns <see cref="T:System.Threading.Tasks.Task`1"/>.The task object representing the asynchronous operation.
        ''' </returns>
        ''' <param name="client">The instantiated Http Client <see cref="HttpClient"/></param>
        ''' <param name="requestUri">The Uri the request Is sent to.</param>
        ''' <param name="content">The HTTP request content sent to the server.</param>
        ''' <exception cref="T:System.ArgumentNullException">The <paramref name="client"/> was null.</exception>
        ''' <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri"/> was null.</exception>
        <Extension()>
        Function PatchAsync(ByVal client As HttpClient, ByVal requestUri As String, ByVal content As HttpContent) As Task(Of HttpResponseMessage)
            Return client.PatchAsync(CreateUri(requestUri), content)
        End Function

        ''' <summary>
        ''' Send a PATCH request to the specified Uri as an asynchronous operation.
        ''' </summary>
        ''' 
        ''' <returns>
        ''' Returns <see cref="T:System.Threading.Tasks.Task`1"/>.The task object representing the asynchronous operation.
        ''' </returns>
        ''' <param name="client">The instantiated Http Client <see cref="HttpClient"/></param>
        ''' <param name="requestUri">The Uri the request Is sent to.</param>
        ''' <param name="content">The HTTP request content sent to the server.</param>
        ''' <exception cref="T:System.ArgumentNullException">The <paramref name="client"/> was null.</exception>
        ''' <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri"/> was null.</exception>
        <Extension()>
        Function PatchAsync(ByVal client As HttpClient, ByVal requestUri As Uri, ByVal content As HttpContent) As Task(Of HttpResponseMessage)
            Return client.PatchAsync(requestUri, content, CancellationToken.None)
        End Function

        ''' <summary>
        ''' Send a PATCH request with a cancellation token as an asynchronous operation.
        ''' </summary>
        ''' 
        ''' <returns>
        ''' Returns <see cref="T:System.Threading.Tasks.Task`1"/>.The task object representing the asynchronous operation.
        ''' </returns>
        ''' <param name="client">The instantiated Http Client <see cref="HttpClient"/></param>
        ''' <param name="requestUri">The Uri the request Is sent to.</param>
        ''' <param name="content">The HTTP request content sent to the server.</param>
        ''' <param name="cancellationToken">A cancellation token that can be used by other objects Or threads to receive notice of cancellation.</param>
        ''' <exception cref="T:System.ArgumentNullException">The <paramref name="client"/> was null.</exception>
        ''' <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri"/> was null.</exception>
        <Extension()>
        Function PatchAsync(ByVal client As HttpClient, ByVal requestUri As String, ByVal content As HttpContent, ByVal cancellationToken As CancellationToken) As Task(Of HttpResponseMessage)
            Return client.PatchAsync(CreateUri(requestUri), content, cancellationToken)
        End Function

        ''' <summary>
        ''' Send a PATCH request with a cancellation token as an asynchronous operation.
        ''' </summary>
        ''' 
        ''' <returns>
        ''' Returns <see cref="T:System.Threading.Tasks.Task`1"/>.The task object representing the asynchronous operation.
        ''' </returns>
        ''' <param name="client">The instantiated Http Client <see cref="HttpClient"/></param>
        ''' <param name="requestUri">The Uri the request Is sent to.</param>
        ''' <param name="content">The HTTP request content sent to the server.</param>
        ''' <param name="cancellationToken">A cancellation token that can be used by other objects Or threads to receive notice of cancellation.</param>
        ''' <exception cref="T:System.ArgumentNullException">The <paramref name="client"/> was null.</exception>
        ''' <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri"/> was null.</exception>
        <Extension()>
        Function PatchAsync(ByVal client As HttpClient, ByVal requestUri As Uri, ByVal content As HttpContent, ByVal cancellationToken As CancellationToken) As Task(Of HttpResponseMessage)
            Return client.SendAsync(New HttpRequestMessage(New HttpMethod("PATCH"), requestUri) With {
                .Content = content
            }, cancellationToken)
        End Function

        Private Function CreateUri(ByVal uri As String) As Uri
            Return If(String.IsNullOrEmpty(uri), Nothing, New Uri(uri, UriKind.RelativeOrAbsolute))
        End Function
    End Module
End Namespace
